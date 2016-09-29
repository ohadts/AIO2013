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
    public partial class shutdownForm : Form
    {

        public int ReturnValue1 { get; set; }
        
        public shutdownForm(string host)
        {
            InitializeComponent();
            this.Text = "Shutdown options for: " + host;
            
        }

        private void shtdwnOkBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            if (shtdwnRadio.Checked == true)
                ReturnValue1 = 5;
            if (rstrtRadio.Checked == true)
                ReturnValue1 = 6;
            if (lgffRadio.Checked == true)
                ReturnValue1 = 4;
            this.Close();
        }

        private void shtdwnCancleBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Dispose(true);
        }
    }
}
