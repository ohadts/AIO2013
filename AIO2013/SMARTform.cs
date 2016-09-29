using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace AIO2013
{
    

    public partial class SMARTform : Form
    {
        public Dictionary<int, HDD> dicDrives = new Dictionary<int, HDD>();
        public SMARTform(string host)
        {
            InitializeComponent();
            
            try
            {

                // retrieve list of drives on computer (this will return both HDD's and CDROM's and Virtual CDROM's)                    
               // var dicDrives = new Dictionary<int, HDD>();

                var wdSearcher = new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2", "SELECT * FROM Win32_DiskDrive");

                // extract model and interface information
                int iDriveIndex = 0;
                foreach (ManagementObject drive in wdSearcher.Get())
                {
                    var hdd = new HDD();
                    hdd.Model = drive["Model"].ToString().Trim();
                    hdd.Type = drive["InterfaceType"].ToString().Trim();
                    dicDrives.Add(iDriveIndex, hdd);
                    iDriveIndex++;
                }

                var pmsearcher = new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2", "SELECT * FROM Win32_PhysicalMedia");

                // retrieve hdd serial number
                iDriveIndex = 0;
                foreach (ManagementObject drive in pmsearcher.Get())
                {
                    // because all physical media will be returned we need to exit
                    // after the hard drives serial info is extracted
                    if (iDriveIndex >= dicDrives.Count)
                        break;

                    dicDrives[iDriveIndex].Serial = drive["SerialNumber"] == null
                                                        ? "None"
                                                        : drive["SerialNumber"].ToString().Trim();
                    iDriveIndex++;
                }

                // get wmi access to hdd 
                var searcher = new ManagementObjectSearcher("\\\\" + host + "\\root\\wmi", "Select * from Win32_DiskDrive");
               // searcher.Scope = new ManagementScope(@"\root\wmi");

                // check if SMART reports the drive is failing
                searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictStatus");
                iDriveIndex = 0;
                foreach (ManagementObject drive in searcher.Get())
                {
                    dicDrives[iDriveIndex].IsOK = (bool) drive.Properties["PredictFailure"].Value == false;
                    iDriveIndex++;
                }

                // retrive attribute flags, value worste and vendor data information
                searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictData");
                iDriveIndex = 0;
                foreach (ManagementObject data in searcher.Get())
                {
                    Byte[] bytes = (Byte[]) data.Properties["VendorSpecific"].Value;
                    for (int i = 0; i < 30; ++i)
                    {
                        try
                        {
                            int id = bytes[i*12 + 2];

                            int flags = bytes[i*12 + 4];
                                // least significant status byte, +3 most significant byte, but not used so ignored.
                            //bool advisory = (flags & 0x1) == 0x0;
                            bool failureImminent = (flags & 0x1) == 0x1;
                            //bool onlineDataCollection = (flags & 0x2) == 0x2;

                            int value = bytes[i*12 + 5];
                            int worst = bytes[i*12 + 6];
                            int vendordata = BitConverter.ToInt32(bytes, i*12 + 7);
                            if (id == 0) continue;

                            var attr = dicDrives[iDriveIndex].Attributes[id];
                            attr.Current = value;
                            attr.Worst = worst;
                            attr.Data = vendordata;
                            attr.IsOK = failureImminent == false;
                        }
                        catch
                        {
                            // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
                        }
                    }
                    iDriveIndex++;
                }

                // retreive threshold values foreach attribute
                searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictThresholds");
                iDriveIndex = 0;
                foreach (ManagementObject data in searcher.Get())
                {
                    Byte[] bytes = (Byte[]) data.Properties["VendorSpecific"].Value;
                    for (int i = 0; i < 30; ++i)
                    {
                        try
                        {

                            int id = bytes[i*12 + 2];
                            int thresh = bytes[i*12 + 3];
                            if (id == 0) continue;

                            var attr = dicDrives[iDriveIndex].Attributes[id];
                            attr.Threshold = thresh;
                        }
                        catch
                        {
                            // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
                        }
                    }

                    iDriveIndex++;
                }
                
                //Print
                int dcount = 0;
                 foreach (var drive in dicDrives)
                 {
                     HDcomboBox.Items.Add("Drive " + dcount + ": " + drive.Value.Serial + " - " + drive.Value.Model + " - " + drive.Value.Type);
                     dcount++;
                 }
                if(HDcomboBox.Items.Count == 1)
                {
                    HDcomboBox.SelectedIndex = 0;
                }

            }

            catch (ManagementException e)
              {
                MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
                this.Dispose();
              }
    
        }

        private void HDcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            notOkLabel.Visible = false;
            criticalLabel.Visible = false;

            if (HDcomboBox.SelectedIndex<0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {

                int x = 0;

            foreach (var drive in dicDrives)
            {
                if (x == HDcomboBox.SelectedIndex)
                {
                    int i = 0;
                    foreach (var attr in drive.Value.Attributes)
                    { 
                        
                        if (attr.Value.HasData)
                        {
                            dataGridView1.Rows.Add(1);
                            dataGridView1.Rows[i].Cells[0].Value = attr.Value.Attribute;
                            dataGridView1.Rows[i].Cells[1].Value = attr.Value.Current;
                            dataGridView1.Rows[i].Cells[2].Value = attr.Value.Worst;
                            dataGridView1.Rows[i].Cells[3].Value = attr.Value.Threshold;
                            dataGridView1.Rows[i].Cells[4].Value = attr.Value.Data;
                            dataGridView1.Rows[i].Cells[5].Value = ((attr.Value.IsOK) ? "OK" : "");
                            if (!attr.Value.IsOK)
                            {
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Salmon;
                                notOkLabel.Visible = true;
                            }
                            if ((attr.Value.Attribute == "End-to-End error" ||
                                attr.Value.Attribute == "Offline scan uncorrectable count" ||
                                attr.Value.Attribute == "Reallocated sector count" ||
                                attr.Value.Attribute == "Soft read error rate" ||
                                attr.Value.Attribute == "Spinup retry count") && attr.Value.Data > 1)
                            {
                                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                                criticalLabel.Visible = true;
                            }
                                
                            i++;
                        }
                    }
                    if (drive.Value.IsOK)
                    {
                        pictureBox1.Image =
                            global::AIO2013.Properties.Resources._2000px_ok_x_nuvola_green_1_;
                    }
                    else
                    {
                        pictureBox1.Image =
                            global::AIO2013.Properties.Resources._2000px_ok_x_nuvola_green_2_;
                        healthLabal.ForeColor = Color.Red;
                    }
                }
                
                x++;
            } 
                 

            }
            
        }
    }

    public class HDD
    {

        public int Index { get; set; }
        public bool IsOK { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Serial { get; set; }

        public Dictionary<int, Smart> Attributes = new Dictionary<int, Smart>()
                                                       {
                                                           {0x00, new Smart("Invalid")},
                                                           {0x01, new Smart("Raw read error rate")},
                                                           {0x02, new Smart("Throughput performance")},
                                                           {0x03, new Smart("Spinup time")},
                                                           {0x04, new Smart("Start/Stop count")},
                                                           {0x05, new Smart("Reallocated sector count")},
                                                           {0x06, new Smart("Read channel margin")},
                                                           {0x07, new Smart("Seek error rate")},
                                                           {0x08, new Smart("Seek timer performance")},
                                                           {0x09, new Smart("Power-on hours count")},
                                                           {0x0A, new Smart("Spinup retry count")},
                                                           {0x0B, new Smart("Calibration retry count")},
                                                           {0x0C, new Smart("Power cycle count")},
                                                           {0x0D, new Smart("Soft read error rate")},
                                                           {0xB8, new Smart("End-to-End error")},
                                                           {0xBE, new Smart("Airflow Temperature")},
                                                           {0xBF, new Smart("G-sense error rate")},
                                                           {0xC0, new Smart("Power-off retract count")},
                                                           {0xC1, new Smart("Load/Unload cycle count")},
                                                           {0xC2, new Smart("HDD temperature")},
                                                           {0xC3, new Smart("Hardware ECC recovered")},
                                                           {0xC4, new Smart("Reallocation count")},
                                                           {0xC5, new Smart("Current pending sector count")},
                                                           {0xC6, new Smart("Offline scan uncorrectable count")},
                                                           {0xC7, new Smart("UDMA CRC error rate")},
                                                           {0xC8, new Smart("Write error rate")},
                                                           {0xC9, new Smart("Soft read error rate")},
                                                           {0xCA, new Smart("Data Address Mark errors")},
                                                           {0xCB, new Smart("Run out cancel")},
                                                           {0xCC, new Smart("Soft ECC correction")},
                                                           {0xCD, new Smart("Thermal asperity rate (TAR)")},
                                                           {0xCE, new Smart("Flying height")},
                                                           {0xCF, new Smart("Spin high current")},
                                                           {0xD0, new Smart("Spin buzz")},
                                                           {0xD1, new Smart("Offline seek performance")},
                                                           {0xDC, new Smart("Disk shift")},
                                                           {0xDD, new Smart("G-sense error rate")},
                                                           {0xDE, new Smart("Loaded hours")},
                                                           {0xDF, new Smart("Load/unload retry count")},
                                                           {0xE0, new Smart("Load friction")},
                                                           {0xE1, new Smart("Load/Unload cycle count")},
                                                           {0xE2, new Smart("Load-in time")},
                                                           {0xE3, new Smart("Torque amplification count")},
                                                           {0xE4, new Smart("Power-off retract count")},
                                                           {0xE6, new Smart("GMR head amplitude")},
                                                           {0xE7, new Smart("Temperature")},
                                                           {0xF0, new Smart("Head flying hours")},
                                                           {0xFA, new Smart("Read error retry rate")},
                                                           /* slot in any new codes you find in here */
                                                       };

    }

    public class Smart
    {
        public bool HasData
        {
            get
            {
                if (Current == 0 && Worst == 0 && Threshold == 0 && Data == 0)
                    return false;
                return true;
            }
        }

        public string Attribute { get; set; }
        public int Current { get; set; }
        public int Worst { get; set; }
        public int Threshold { get; set; }
        public int Data { get; set; }
        public bool IsOK { get; set; }

        public Smart()
        {

        }

        public Smart(string attributeName)
        {
            this.Attribute = attributeName;
        }
    }
}
