using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using BrightIdeasSoftware;


namespace AIO2013
{
    public partial class Open_sessions_query : Form
    {

        List<openSession> masterList;

        public Open_sessions_query()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Use Right-Click On A Row To Copy Content";
            this.olvColumnUser.ImageGetter = new ImageGetterDelegate(this.openSessionColumnImageGetter);

            if (ObjectListView.IsVistaOrLater)
                this.Font = new Font("Segoe UI", 9);
        }
        object openSessionColumnImageGetter (object rowObject) {
            return 0;
        }
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            // Use different font under Vista
            groupBox1.Enabled = false;
            masterList = new List<openSession>();
            objectListView1.SetObjects(masterList);

            ManagementObjectSearcher searcher = WMI.MyWMIQuery.fetch(hostTxtBx.Text);
            if (searcher != null)
            {
                try
                {
                    var sessions = searcher.Get();
                    progressBar1.Maximum = sessions.Count;
                    progressBar1.Step = 1;
                    progressBar1.Value = 0;



                    foreach (ManagementObject queryObj in sessions)
                    {
                        string[] ipAndName = getIpAndHost(queryObj["ComputerName"].ToString());



                        masterList.Add(new openSession(queryObj["UserName"].ToString(), ipAndName[1], ipAndName[0], getSessionType(Convert.ToInt16(queryObj["SessionType"])),
                            Convert.ToInt32(queryObj["ResourcesOpened"]), formatSeconds(Convert.ToDouble(queryObj["ActiveTime"])),
                            formatSeconds(Convert.ToDouble(queryObj["IdleTime"]))));

                        progressBar1.PerformStep();
                    }

                    objectListView1.SetObjects(masterList);
                    button1.Enabled = true;
                    groupBox1.Enabled = true;
                    objectListView1.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
                    objectListView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
                    objectListView1.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                    objectListView1.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
                    objectListView1.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.HeaderSize);
                    objectListView1.AutoResizeColumn(5, ColumnHeaderAutoResizeStyle.HeaderSize);
                    objectListView1.AutoResizeColumn(6, ColumnHeaderAutoResizeStyle.HeaderSize);
                }
                catch (Exception ex)
                {
                    groupBox1.Enabled = false;
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    button1.Enabled = true;
                    toolStripStatusLabel1.Text = "Fetch operation completed";
                }
            }


        }

        private string getSessionType(int type)
        {
            switch (type)
            {
                case 0:
                    return "Guest";
                case 1:
                    return "NoEncryption";
                default:
                    return "Windows";
            }
        }

        private string formatSeconds(double sec)
        {
            TimeSpan t = TimeSpan.FromSeconds(sec);

            string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            t.Hours,
                            t.Minutes,
                            t.Seconds);
            return answer;

        }

        private string[] getIpAndHost(string host)
        {
            string recHost = host;
            string[] result = { "", "" };
            if (pingFunction(host) == "Success")
            {
                IPAddress ip;
                if (IPAddress.TryParse(recHost, out ip))
                {
                    try
                    {
                        result[0] = recHost;
                        IPHostEntry iphost = Dns.GetHostEntry(recHost);
                        result[1] = iphost.HostName;
                    }
                    catch (Exception)
                    {
                        result[0] = "Null";
                        result[1] = "Null";
                    }
                    
                }
                else
                {
                    try
                    {
                        result[1] = recHost;
                        IPHostEntry iphost = Dns.GetHostEntry(recHost);
                        result[0] = iphost.AddressList[0].ToString();
                    }
                    catch (Exception)
                    {
                        result[0] = "Null";
                        result[1] = "Null";
                    }

                }
            }
            else
            {
                result[0] = "Null";
                result[1] = "Null";
            }
            return result;
        }

        public string pingFunction(string hostAdrress)
        {

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            StringBuilder result = new StringBuilder();

            try
            {
                PingReply reply = pingSender.Send(hostAdrress, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    return "Success";
                }
                else
                {
                    return "No connection: " + reply.Status;
                }
            }


            catch (Exception ex)
            {
                if (ex.Message == "Value cannot be null.\r\nParameter name: hostNameOrAddress")
                {
                    return "Error!\r\nEmpty value in text box.";
                }
                else
                {
                    return "Error: " + ex.InnerException.Message;
                }
            }

        }

        private void searchTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            this.objectListView1.ModelFilter = new TextMatchFilter(this.objectListView1, searchTextBox1.Text);
        }

        private void copyUserNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSession selected = (openSession)objectListView1.SelectedObject;
            try
            {
                Clipboard.SetText(selected.user);
                toolStripStatusLabel1.Text = "User name copied to clipboard";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void copyComputerNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSession selected = (openSession)objectListView1.SelectedObject;
            try
            {
                Clipboard.SetText(selected.pcName);
                toolStripStatusLabel1.Text = "Computer name copied to clipboard";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void copyIpAddressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openSession selected = (openSession)objectListView1.SelectedObject;
            try
            {
                Clipboard.SetText(selected.ipAddress);
                toolStripStatusLabel1.Text = "Ip address copied to clipboard";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.objectListView1.ModelFilter = new TextMatchFilter(this.objectListView1, searchTextBox1.Text);
        }

    }

    public class openSession
    {
        public string user;
        public string pcName;
        public string ipAddress;
        public string type;
        public int numOfFiles;
        public string conTime;
        public string idleTime;

        public openSession(string _user, string _pcName, string _ipAddress, string _type, int _numOfFiles, string _conTime, string _idleTime)
        {
            this.user = _user;
            this.pcName = _pcName;
            this.ipAddress = _ipAddress;
            this.type = _type;
            this.numOfFiles = _numOfFiles;
            this.conTime = _conTime;
            this.idleTime = _idleTime;
        }

        public openSession(string _user)
        {
            this.user = _user;
        }
    }




    namespace WMI
    {
        public class MyWMIQuery
        {
            public static ManagementObjectSearcher fetch(string host)
            {
                try
                {
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(@"\\" + host + @"\" + "root\\CIMV2",
                        "SELECT * FROM Win32_ServerSession");
                    return searcher;
                }
                catch (ManagementException e)
                {
                    MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
                    return null;
                }
            }
        }
    }
}