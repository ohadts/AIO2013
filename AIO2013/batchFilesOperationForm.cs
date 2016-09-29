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
    public partial class batch_files_operation : Form
    {
        public string[] hosts;

        public batch_files_operation()
        {
            InitializeComponent();
            sepComboBx.SelectedIndex = 0;
        }

        private void CnclBtn_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            string seperator;
            switch (sepComboBx.SelectedIndex)
	        {
                case 0:
                    seperator = Environment.NewLine;
                    break;
                case 1:
                    seperator = " ";
                    break;
		        default:
                    seperator = sepComboBx.SelectedItem.ToString();
                    break;
	        }

            hosts = mainTxtBx.Text.Split(seperator.ToCharArray());
            hosts = hosts.Where(x => !string.IsNullOrEmpty(x)).ToArray()
                         .Where(x => x != " ").ToArray();
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void pasteBtn_Click(object sender, EventArgs e)
        {
            mainTxtBx.Text = Clipboard.GetText();
        }
    }
}
