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
    public partial class dameware_cred : Form
    {
        public dameware_cred()
        {
            InitializeComponent();
            usernameTxtbox.Enter += new EventHandler(usernameTxtbox_Enter);
            passwordTxtbox.Enter += new EventHandler(passwordTxtbox_Enter);
            domainTxtbox.Enter += new EventHandler(domainTxtbox_Enter);
            localChkbox.Checked = true;
            
        }

        void domainTxtbox_Enter(object sender, EventArgs e)
        {
            domainTxtbox.ForeColor = Color.Black;
            domainTxtbox.Text = "";
        }

        void passwordTxtbox_Enter(object sender, EventArgs e)
        {
            passwordTxtbox.ForeColor = Color.Black;
            passwordTxtbox.Text = "";
            passwordTxtbox.PasswordChar = '*';
        }

        void usernameTxtbox_Enter(object sender, EventArgs e)
        {
            usernameTxtbox.ForeColor = Color.Black;
            usernameTxtbox.Text = "";
        }

        private void localChkbox_CheckStateChanged(object sender, EventArgs e)
        {

            domainTxtbox.Enabled = !localChkbox.Checked;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(displayNameTxtbox.Text) || 
                string.IsNullOrEmpty(usernameTxtbox.Text) ||
                usernameTxtbox.Text == "Username" ||
                string.IsNullOrEmpty(passwordTxtbox.Text) ||
                passwordTxtbox.Text == "Password" ||
                (!localChkbox.Checked && domainTxtbox.Text == "Domain") ||
                !localChkbox.Checked && string.IsNullOrEmpty(domainTxtbox.Text))
            {
                validateLabel.Visible = true;
            }
            else
            {
                if(localChkbox.Checked)
                {
                    domainTxtbox.Text = ".";
                }
                settingsForm.dwSaveNewCred(displayNameTxtbox.Text, usernameTxtbox.Text, passwordTxtbox.Text,domainTxtbox.Text);
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }
    }
}
