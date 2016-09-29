using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using BrightIdeasSoftware;

namespace AIO2013
{
    public partial class taskmgrForm : Form
    {
        private ListViewColumnSorter lvwColumnSorter;
        private System.Windows.Forms.ContextMenu lvcxtmnu;
        private System.Windows.Forms.MenuItem menuItem_endPro;
        private string _host ;
       // List<process> mainProcList;

        public taskmgrForm(string host)
        {
            InitializeComponent();
            _host = host;
            this.Text = "Tasks in " + _host;

            this.lvcxtmnu = new ContextMenu();
            this.menuItem_endPro = new MenuItem();
            lvwColumnSorter = new ListViewColumnSorter();

            menuItem_endPro.Click += new EventHandler(menuItem_endPro_Click);

            
            menuItem_endPro.Text = "End Process";
            lvcxtmnu.MenuItems.Add(menuItem_endPro);
            objectListView1.ContextMenu = this.lvcxtmnu;
            LoadAllProcessesOnStartup();
        }

        void menuItem_endPro_Click(object sender, EventArgs e)
        {
            if (objectListView1.SelectedItems.Count >= 1)
            {
                try
                {
                    int selectedpid = Convert.ToInt32(objectListView1.SelectedItems[0].SubItems[1].Text.ToString());
                    try
                    {
                        ManagementObject classInstance =
                            new ManagementObject("\\\\" + _host + "\\root\\CIMV2",
                            "Win32_Process.Handle='" + selectedpid + "'",
                            null);

                        // Obtain in-parameters for the method
                        ManagementBaseObject inParams =
                            classInstance.GetMethodParameters("Terminate");



                        // Execute the method and obtain the return values.
                        ManagementBaseObject outParams =
                            classInstance.InvokeMethod("Terminate", inParams, null);


                        if (outParams["ReturnValue"].ToString() == "0")
                        {
                            MessageBox.Show("The process " + objectListView1.SelectedItems[0].SubItems[0].Text.ToString() +
                                            " KILLED !");
                            objectListView1.Items.Clear();
                            LoadAllProcessesOnStartup();
                        }
                    }
                    catch (ManagementException err)
                    {
                        MessageBox.Show("An error occurred while trying to execute the WMI method: " + err.Message);
                    }

                }
                catch (Exception ex3)
                {
                    MessageBox.Show("An error occurred while trying to execute the WMI method: " + ex3.Message);
                }
            }
        }

        private void LoadAllProcessesOnStartup()
        {
            this.olvColumn3.AspectToStringConverter = delegate(object aspect)
            {
                return aspect.ToString() + " KB";
            };

            List<process> mainProcList = new List<process>();
            Process[] processes = null;
            try
            {
                processes = Process.GetProcesses(_host);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Dispose();
            }
            
            foreach (Process p in processes)
            {
                try
                {
                    mainProcList.Add(new process(p.ProcessName, p.Id, p.WorkingSet64 / 1024));
                }
                catch { }
            }
            objectListView1.SetObjects(mainProcList);
            procCount.Text = "Processes : " + processes.Length.ToString();
            
                
            
        }

        private void searchTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.objectListView1.ModelFilter = new TextMatchFilter(this.objectListView1, searchTextBox1.Text);
        }

        private void searchTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            this.objectListView1.ModelFilter = new TextMatchFilter(this.objectListView1, searchTextBox1.Text);
        }

        
     
    }


    public class process
    {
        public string name;
        public int id;
        public long memory;
        public process(string _name, int _id, long _memory)
        {
            name = _name;
            id = _id;
            memory = _memory;
        }

    }
}
