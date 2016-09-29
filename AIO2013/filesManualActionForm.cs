using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AIO2013
{
    public partial class Start_manually_file_action : Form
    {
        public Start_manually_file_action()
        {
            InitializeComponent();
            Properties.Settings.Default.tempPackage = "";
        }

        private void cncleBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void copyRdio_CheckedChanged(object sender, EventArgs e)
        {
            if (copyRdio.Checked)
            {
                srcGroupBox1.Enabled = true;
            }
            else
            {
                srcGroupBox1.Enabled = false;
            }
        }

        private void fileRdio_CheckedChanged(object sender, EventArgs e)
        {
            if (fileRdio.Checked)
            {
                srcCreateDirChkbx.Enabled = false;
                srcPathTxtbx.Text = "";
            }
            else
            {
                srcCreateDirChkbx.Enabled = true;
                srcPathTxtbx.Text = "";
            }
        }

        private void srcBrwsBtn_Click(object sender, EventArgs e)
        {
            if (dirRdio.Checked)
            {
                if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    srcPathTxtbx.Text = folderBrowserDialog1.SelectedPath;
                }
            }
            else
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    srcPathTxtbx.Text = openFileDialog1.FileName;
                }
            }
        }

        private void goBtn_Click(object sender, EventArgs e)
        {
            if (copyRdio.Checked)
            {
                validateCopy();
            }
            else
            {
                validateDelete();
            }
        }


        private void validateDelete()
        {
            Boolean isValid = true;

            if (dstTxtbx.Text == "")
            {
                isValid = false;
                MessageBox.Show("Destination path Can't be EMPTY !");
            }
            if (isValid)
            {
                Properties.Settings.Default.tempPackage = 
                "Entered manually" + ";" + copyRdio.Checked.ToString() + ";" + fileRdio.Checked.ToString() + ";" + "false" + ";" + dstTxtbx.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }



        }

        private void validateCopy()
        {
            Boolean isValid = false;

            if (fileRdio.Checked && File.Exists(srcPathTxtbx.Text))
            {
                isValid = true;
            }
            if (dirRdio.Checked)
            {
                if (srcCreateDirChkbx.Checked)
                {
                    if (Directory.Exists(srcPathTxtbx.Text.Remove(srcPathTxtbx.Text.IndexOf(@"\*.*"), 4)))
                    {
                        isValid = true;
                    }
                }
                else
                {
                    if (Directory.Exists(srcPathTxtbx.Text))
                    {
                        isValid = true;
                    }
                }


            }
            if (!isValid)
            {
                MessageBox.Show("Source path INVALID!");
            }
            if (dstTxtbx.Text == "")
            {
                isValid = false;
                MessageBox.Show("Destination path Can't be EMPTY !");
            }


            if (isValid)
            {
                Properties.Settings.Default.tempPackage =
                "Entered manually" + ";" + copyRdio.Checked.ToString() + ";" + fileRdio.Checked.ToString() + ";" + srcPathTxtbx.Text + ";" + dstTxtbx.Text;
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }



        }

        private void srcCreateDirChkbx_CheckedChanged(object sender, EventArgs e)
        {
            if (srcCreateDirChkbx.Checked)
            {
                srcPathTxtbx.Text = srcPathTxtbx.Text + @"\*.*";
            }
            else
            {
                if (srcPathTxtbx.Text.EndsWith(@"\*.*"))
                {
                    srcPathTxtbx.Text = srcPathTxtbx.Text.Remove(srcPathTxtbx.Text.IndexOf(@"\*.*"), 4);
                }
            }
        }
    }
}
