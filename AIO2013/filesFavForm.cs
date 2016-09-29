using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AIO2013
{
    //TODO: split into object?
    public partial class filesFavForm : Form
    {

        public string newPkgString;
        public singleFavoritePackage slot1;
        public singleFavoritePackage slot2;
        public singleFavoritePackage slot3;
        public singleFavoritePackage slot4;
        public singleFavoritePackage slot5;
        public singleFavoritePackage slot6;


        public filesFavForm()
        {
            slot1 = new singleFavoritePackage(Properties.Settings.Default.fileLink1);
            slot2 = new singleFavoritePackage(Properties.Settings.Default.fileLink2);
            slot3 = new singleFavoritePackage(Properties.Settings.Default.fileLink3);
            slot4 = new singleFavoritePackage(Properties.Settings.Default.fileLink4);
            slot5 = new singleFavoritePackage(Properties.Settings.Default.fileLink5);
            slot6 = new singleFavoritePackage(Properties.Settings.Default.fileLink6);

            
            InitializeComponent();
            comboBox1.ImageList = imageList1;
            


            string[] copyPackages = Properties.Settings.Default.copyPackages.Split('|');
            foreach (string copyPackage in copyPackages)
            {
                if (!string.IsNullOrEmpty(copyPackage))
                {
                    if (Convert.ToBoolean(copyPackage.Split(';')[1]))
                    {
                        comboBox1.Items.Add(new ComboBoxExItem(copyPackage.Split(';')[0], 1));
                    }
                    else
                    {
                        comboBox1.Items.Add(new ComboBoxExItem(copyPackage.Split(';')[0], 2));
                    }
                }
            }

            if(!string.IsNullOrEmpty(Properties.Settings.Default.fileLink1))
            {
                //filesFavoritePackage slot1 = new filesFavoritePackage(Properties.Settings.Default.fileLink1);
                emptySlt1.Text = slot1._name;
                pictureBox1.Visible = true;


            }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.fileLink2))
            {
                //filesFavoritePackage slot2 = new filesFavoritePackage(Properties.Settings.Default.fileLink2);
                emptySlt2.Text = slot2._name;
                pictureBox2.Visible = true;

            }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.fileLink3))
            {
                //filesFavoritePackage slot3 = new filesFavoritePackage(Properties.Settings.Default.fileLink3);
                emptySlt3.Text = slot3._name;
                pictureBox3.Visible = true;

            } 
            if (!string.IsNullOrEmpty(Properties.Settings.Default.fileLink4))
            {
                //filesFavoritePackage slot4 = new filesFavoritePackage(Properties.Settings.Default.fileLink4);
                emptySlt4.Text = slot4._name;
                pictureBox4.Visible = true;

            }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.fileLink5))
            {
                //filesFavoritePackage slot4 = new filesFavoritePackage(Properties.Settings.Default.fileLink4);
                emptySlt5.Text = slot5._name;
                pictureBox5.Visible = true;

            }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.fileLink6))
            {
                //filesFavoritePackage slot4 = new filesFavoritePackage(Properties.Settings.Default.fileLink4);
                emptySlt6.Text = slot6._name;
                pictureBox6.Visible = true;

            }
            
        }

        private void emptySlt1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                singleFavoritePackage slot1 = new singleFavoritePackage(newPkgString);
                Properties.Settings.Default.fileLink1 = newPkgString;
                emptySlt1.Text = slot1._name;
                pictureBox1.Visible = true;

            }
        }

        private void emptySlt2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                singleFavoritePackage slot2 = new singleFavoritePackage(newPkgString);
                Properties.Settings.Default.fileLink2 = newPkgString;
                emptySlt2.Text = slot2._name;
                pictureBox2.Visible = true;

            }
        }

        private void emptySlt3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                singleFavoritePackage slot3 = new singleFavoritePackage(newPkgString);
                Properties.Settings.Default.fileLink3 = newPkgString;
                emptySlt3.Text = slot3._name;
                pictureBox3.Visible = true;

            }
        }

        private void emptySlt4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                singleFavoritePackage slot4 = new singleFavoritePackage(newPkgString);
                Properties.Settings.Default.fileLink4 = newPkgString;
                emptySlt4.Text = slot4._name;
                pictureBox4.Visible = true;

            }
        }
        private void emptySlt5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                singleFavoritePackage slot5 = new singleFavoritePackage(newPkgString);
                Properties.Settings.Default.fileLink5 = newPkgString;
                emptySlt5.Text = slot5._name;
                pictureBox5.Visible = true;

            }
        }
        private void emptySlt6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                singleFavoritePackage slot6 = new singleFavoritePackage(newPkgString);
                Properties.Settings.Default.fileLink6 = newPkgString;
                emptySlt6.Text = slot6._name;
                pictureBox6.Visible = true;

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            slot1 = null;
            Properties.Settings.Default.fileLink1 = "";
            emptySlt1.Text = "Empty slot 1";
            pictureBox1.Visible = false ;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            slot2 = null;
            Properties.Settings.Default.fileLink2 = "";
            emptySlt2.Text = "Empty slot 2";
            pictureBox2.Visible = false;

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            slot3 = null;
            Properties.Settings.Default.fileLink3 = "";
            emptySlt3.Text = "Empty slot 3";
            pictureBox3.Visible = false;

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            slot4 = null;
            Properties.Settings.Default.fileLink4 = "";
            emptySlt4.Text = "Empty slot 4";
            pictureBox4.Visible = false;

        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            slot5 = null;
            Properties.Settings.Default.fileLink5 = "";
            emptySlt5.Text = "Empty slot 5";
            pictureBox5.Visible = false;

        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            slot6 = null;
            Properties.Settings.Default.fileLink6 = "";
            emptySlt6.Text = "Empty slot 6";
            pictureBox6.Visible = false;

        }

        

        private void filesFavForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pictureBox1.Visible)
            {
                singleFavoritePackage slot1 = new singleFavoritePackage(Properties.Settings.Default.fileLink1);

                Properties.Settings.Default.fileLink1 = slot1.getFullPackageString();

            }
            if (pictureBox2.Visible)
            {
                singleFavoritePackage slot2 = new singleFavoritePackage(Properties.Settings.Default.fileLink2);

                Properties.Settings.Default.fileLink2 = slot2.getFullPackageString();
            }
            if (pictureBox3.Visible)
            {
                singleFavoritePackage slot3 = new singleFavoritePackage(Properties.Settings.Default.fileLink3);

                Properties.Settings.Default.fileLink3 = slot3.getFullPackageString();
            }
            if (pictureBox4.Visible)
            {
                singleFavoritePackage slot4 = new singleFavoritePackage(Properties.Settings.Default.fileLink4);

                Properties.Settings.Default.fileLink4 = slot4.getFullPackageString();
            }
            if (pictureBox5.Visible)
            {
                singleFavoritePackage slot5 = new singleFavoritePackage(Properties.Settings.Default.fileLink5);

                Properties.Settings.Default.fileLink5 = slot5.getFullPackageString();
            }
            if (pictureBox6.Visible)
            {
                singleFavoritePackage slot6 = new singleFavoritePackage(Properties.Settings.Default.fileLink6);

                Properties.Settings.Default.fileLink6 = slot6.getFullPackageString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] copyPackages = Properties.Settings.Default.copyPackages.Split('|');
            foreach (string copyPackage in copyPackages)
            {
                if (!string.IsNullOrEmpty(copyPackage))
                {
                    if(copyPackage.Split(';')[0] == comboBox1.SelectedItem.ToString())
                    {
                        newPkgString = copyPackage;
                    }
                    
                }
            }
            
        }
    }

    public class singleFavoritePackage{
        public string _name;
        public Boolean _isCopy;
        public Boolean _isFile;
        public string _srcPath;
        public string _destPath;



        public singleFavoritePackage(string name,Boolean isCopy, Boolean isFile, string srcPath, string destPath){
            _name = name;
            _isCopy = isCopy;
            _isFile = isFile;
            _srcPath = srcPath;
            _destPath = destPath;

        }

        public singleFavoritePackage(string packageString){
            if (!string.IsNullOrEmpty(packageString))
            {
                string[] set = packageString.Split(';');
                _name = set[0];
                _isCopy = Convert.ToBoolean(set[1]);
                _isFile = Convert.ToBoolean(set[2]);
                _srcPath = set[3];
                _destPath = set[4];
               
            }
        }

        public string getFullPackageString(){
            return _name + ";" + _isCopy + ";" + _isFile + ";" + _srcPath + ";" + _destPath;
        }

    }

    
}
