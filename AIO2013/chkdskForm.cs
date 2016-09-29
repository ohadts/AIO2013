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
    public partial class chkdskForm : Form
    {
        public Boolean xBtnNotPressed = false;
        public string _host;
        public chkdskForm(string host)
        {
            _host = host;
            InitializeComponent();
            label1.Text = "Please choose a drive from " + host + ":";


              try
            {
                ManagementObjectSearcher searcher = 
                    new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2", 
                    "SELECT * FROM Win32_LogicalDisk"); 

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    comboBox1.Items.Add(queryObj["DeviceID"].ToString());
                }
                comboBox1.SelectedItem = "C:";
            }
            catch (ManagementException e)
            {
                MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
                this.Dispose();
            }
        
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!xBtnNotPressed)
            {
                this.DialogResult = DialogResult.Abort;
            }
            
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            xBtnNotPressed = true;
            this.Dispose();
        }

        private void schedual_btn_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(comboBox1.SelectedItem.ToString()))
            {
                try
                {
                    ManagementObject classInstance =
                        new ManagementObject("\\\\" + _host + "\\root\\CIMV2",
                        "Win32_LogicalDisk.DeviceID='" + comboBox1.SelectedItem +"'",
                        null);

                    // Obtain in-parameters for the method
                    ManagementBaseObject inParams =
                        classInstance.GetMethodParameters("Chkdsk");

                    // Add the input parameters.
                    if (recoveryCheckBox.Checked)
                    {
                        inParams["RecoverBadSectors"] = true;
                        inParams["FixErrors"] = true;
                    }
                    else
                    {
                        inParams["RecoverBadSectors"] = false;
                        inParams["FixErrors"] = true;
                    }
                    inParams["OkToRunAtBootUp"] = true;
                    inParams["SkipFolderCycle"] = false;
                    inParams["VigorousIndexCheck"] = false;

                    // Execute the method and obtain the return values.
                    ManagementBaseObject outParams =
                        classInstance.InvokeMethod("Chkdsk", inParams, null);

                    if (Convert.ToInt32(outParams["ReturnValue"]) == 1)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        if(Convert.ToInt32(outParams["ReturnValue"]) == 2)
                        {
                            this.DialogResult = DialogResult.Yes;
                        }
                        else
                        {
                            this.DialogResult = DialogResult.No;
                        }
                    }


                }
                catch (ManagementException err)
                {
                    MessageBox.Show("An error occurred while trying to execute the WMI method: " + err.Message);
                }
                xBtnNotPressed = true;
                this.Dispose();
            }
            else
            {
                xBtnNotPressed = true;
                MessageBox.Show("Please select a drive from the list !");
            }
        }

        private void fixErrCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(!(fixErrCheckBox.Checked || recoveryCheckBox.Checked))
            {
                schedual_btn.Enabled = false;
            }
            else
            {
                schedual_btn.Enabled = true;
            }
        }
    }
}
