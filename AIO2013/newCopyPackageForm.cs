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
    public partial class newDelPackageForm : Form
    {
        public newDelPackageForm()
        {
            InitializeComponent();
        }

        private void cncleBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dirRdio_CheckedChanged(object sender, EventArgs e)
        {
            if (dirRdio.Checked)
            {
                srcCreateDirChkbx.Enabled = true;
                srcPathTxtbx.Text = "";
            }
            else
            {
                srcCreateDirChkbx.Enabled = false;
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

        private void srcCreateDirChkbx_CheckedChanged(object sender, EventArgs e)
        {
            if (srcCreateDirChkbx.Checked)
            {
                srcPathTxtbx.Text = srcPathTxtbx.Text + @"\*.*";
            }else
            {
                if(srcPathTxtbx.Text.EndsWith(@"\*.*"))
                {
                    srcPathTxtbx.Text = srcPathTxtbx.Text.Remove(srcPathTxtbx.Text.IndexOf(@"\*.*"), 4);
                }
            }
        }

        private void OKbtn_Click(object sender, EventArgs e)
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

            if (dstTxtbx.Text == "" || pkgNameTxtbx.Text == "")
            {
                isValid = false;
                MessageBox.Show("Destination path OR Package name Can't be EMPTY !\nPlease go back an fix it");
            }
            if (isValid)
            {
                Boolean isDuplicateFound = false;
                string[] copyPackages = Properties.Settings.Default.copyPackages.Split('|');
                foreach (string copyPackage in copyPackages)
                {
                    if (!string.IsNullOrEmpty(copyPackage) && copyPackage.Split(';')[0] == pkgNameTxtbx.Text)
                    {
                        isDuplicateFound = true;
                    }
                }

                if (isDuplicateFound)
                {
                    MessageBox.Show("A package named " + pkgNameTxtbx.Text + " exist!\n Please delete " + pkgNameTxtbx.Text + " first or choose different package name\nExiting...");
                    this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                }
                else
                {
                    Properties.Settings.Default.copyPackages = Properties.Settings.Default.copyPackages +
                    pkgNameTxtbx.Text + ";" + copyRdio.Checked.ToString() + ";" + fileRdio.Checked.ToString() + ";" + "false" + ";" + dstTxtbx.Text + "|";
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            else
            {

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
                MessageBox.Show("Source path INVALID!\nPlease go back an fix it");
            }
            if (dstTxtbx.Text == "" || pkgNameTxtbx.Text == "")
            {
                isValid = false;
                MessageBox.Show("Destination path OR Package name Can't be EMPTY !\nPlease go back an fix it");
            }


            if (isValid)
            {
                Boolean isDuplicateFound = false;
                string[] copyPackages = Properties.Settings.Default.copyPackages.Split('|');
                foreach (string copyPackage in copyPackages)
                {
                    if (!string.IsNullOrEmpty(copyPackage) && copyPackage.Split(';')[0] == pkgNameTxtbx.Text)
                    {
                        isDuplicateFound = true;
                    }
                }

                if (isDuplicateFound)
                {
                    MessageBox.Show("A package named " + pkgNameTxtbx.Text + " exist!\n Please delete " + pkgNameTxtbx.Text + " first or choose different package name\nExiting...");
                    this.DialogResult = System.Windows.Forms.DialogResult.Abort;
                }
                else
                {
                    Properties.Settings.Default.copyPackages = Properties.Settings.Default.copyPackages +
                    pkgNameTxtbx.Text + ";" + copyRdio.Checked.ToString() + ";" + fileRdio.Checked.ToString() + ";" + srcPathTxtbx.Text + ";" + dstTxtbx.Text + "|";
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                
            }

            
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
    }
}
