using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace AIO2013
{
    public partial class aboutForm : Form
    {
        //private object testfunc;
        public aboutForm()
        {
            InitializeComponent();
            
                checkForUpdates();
            verLbl.Text = Properties.Settings.Default.version.ToString();



        }

        private void checkForUpdates()
        {
            var worker = new BackgroundWorker();

            worker.DoWork += (sender, argss) =>
            {
                try
                {
                    argss.Result = new WebClient().DownloadString("http://hfnt152/aio2013/versions.txt");

                }
                catch (Exception ex)
                {

                    argss.Result = ex.Message;
                }
             };

            worker.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Error != null)
                {
                    latestVer.Text = "Error: " + e.Error.Message;
                }
                else
                {
                    
                    latestVer.Text = e.Result.ToString();
                    pictureBox1.Visible = false;
                    if(verLbl.Text != latestVer.Text)
                    {
                        latestLink.Text = "Download";
                        latestLink.Click += new EventHandler(latestLink_Click);
                        wtsNewLink.Text = "What's new";
                        wtsNewLink.Click += new EventHandler(wtsNewLink_Click);
                    }
                    else
                    {
                        noNeedLbl.Visible = true;
                    }
                }
            };

            

            worker.RunWorkerAsync();
           

        }

        void wtsNewLink_Click(object sender, EventArgs e)
        {
            try
            {
                string wtsNewStr = "http://hfnt152/aio2013/whatsnew.txt";
                Process wtsNewProcess = new Process();
                wtsNewProcess.StartInfo.FileName = wtsNewStr;

                wtsNewProcess.Start();

            }
            catch (Exception wtsNewEx)
            {

                MessageBox.Show(wtsNewEx.Message);
            }
        }

        void latestLink_Click(object sender, EventArgs e)
        {
            try
            {
                string latestStr = "http://hfnt152/aio2013/aio2013-latest.zip";
                Process latestProcess = new Process();
                latestProcess.StartInfo.FileName = latestStr;

                latestProcess.Start();

            }
            catch (Exception latestEx)
            {

                MessageBox.Show(latestEx.Message);
            }
        }

       
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process mailto = new Process();
            mailto.StartInfo.FileName = "mailto: ohadts@gmail.com";
            mailto.Start();  
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (
                               (Directory.Exists(
                                   System.IO.Path.GetDirectoryName(
                                       System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Resources")) !=
                               true)
            {
                MessageBox.Show("Unable to find 'Resources' Directory !");
            }
            else
            {
                if (
                    (File.Exists(
                        System.IO.Path.GetDirectoryName(
                            System.Reflection.Assembly.GetExecutingAssembly().Location) +
                        "\\Resources\\CPOL.htm")) != true)
                {
                    MessageBox.Show("Unable to find License file !");
                }
                else
                {
                    try
                    {
                        string license =
                        System.IO.Path.GetDirectoryName(
                            System.Reflection.Assembly.GetExecutingAssembly().Location) +
                        "\\Resources\\CPOL.htm";
                        Process licenseProcess = new Process();
                        licenseProcess.StartInfo.FileName = license;
                        licenseProcess.Start();
                    }
                    catch (Exception licenseEx)
                    {
                        MessageBox.Show(licenseEx.Message);
                    }
                    
                }
            }
        }

   

        

        
    }
}
