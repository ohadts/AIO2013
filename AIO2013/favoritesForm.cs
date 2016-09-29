using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BrightIdeasSoftware;

namespace AIO2013
{
    public partial class favoritesForm : Form
    {
        public string selected = "";
        public Boolean doubleClicked = false;
        public int currentMouseOverRow;
        //List<favorite> mainFavList;

        public favoritesForm(string host)
        {
            InitializeComponent();
            hostTxtbox.Text = host;
            loadDatagrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(hostTxtbox.Text))
                MessageBox.Show("Host can't be EMPTY !!!");
            else
            {
                if (!isDuplicate(hostTxtbox.Text))
                {
                    Properties.Settings.Default.favorites += hostTxtbox.Text + ";" + descTxtbox.Text + "|";
                    Properties.Settings.Default.Save();
                    hostTxtbox.Text = "";
                    descTxtbox.Text = "";
                    loadDatagrid();
                }
                else
                {
                    MessageBox.Show(
                        "This host is already in you favorites !! \r\n Delete or edit the existing one instead...");
                }
            }
        }

        private bool isDuplicate(string p)
        {  
            foreach (var fave in Properties.Settings.Default.favorites.Split('|'))
            {
                if (!string.IsNullOrEmpty(fave))
                {
                    if (fave.Split(';')[0] == p)
                        return true;
                }
            }
            return false;
        }

        private void loadDatagrid()
        {
            objectListView1.SetObjects(null);
            List<favorite> mainFavList = new List<favorite>();
            
            //int i = 0;

            foreach (var fave in Properties.Settings.Default.favorites.Split('|'))
            {
                if (!string.IsNullOrEmpty(fave))
                {
                    mainFavList.Add(new favorite(fave.Split(';')[0], fave.Split(';')[1]));
                }
            }
            objectListView1.SetObjects(mainFavList);

            
        }

        private void favorites_FormClosing(object sender, FormClosingEventArgs e)
        {
            updateStrSettings();
            loadDatagrid();
        }

        private void updateStrSettings()
        {
            Properties.Settings.Default.favorites = "";

            foreach (favorite row in objectListView1.Objects)
            {

                if (row != null)
                {
                    Properties.Settings.Default.favorites += row._host + ";" + row._desc + "|";
                }
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objectListView1.RemoveObject(objectListView1.SelectedItem.RowObject);
            updateStrSettings();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            favorite selected = (favorite)objectListView1.SelectedItem.RowObject;
            hostTxtbox.Text = selected._host;
            descTxtbox.Text = selected._desc;
            objectListView1.RemoveObject(selected);
            updateStrSettings();
        }

        private void objectListView1_DoubleClick(object sender, EventArgs e)
        {
            favorite doubleClickedCell = (favorite)objectListView1.SelectedItem.RowObject;
            selected = doubleClickedCell._host;
            doubleClicked = true;
            this.Close();
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

    public class favorite
    {
        public string _host;
        public string _desc;
        public favorite(string host, string desc)
        {
            _host = host;
            _desc = desc;
        }
    
    }
}
