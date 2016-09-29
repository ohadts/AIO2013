using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace Teboscreen
{

    public partial class ControlPanel : Form
    {

        string ScreenPath;
        private static bool TipsShowing;

        private Form m_InstanceRef = null;
        public Form InstanceRef
        {

            get
            {

                return m_InstanceRef;

            }
            set
            {

                m_InstanceRef = value;

            }

        }

        public ControlPanel()
        {

            InitializeComponent();
            ScreenShot.saveToClipboard = false;


        }

        public ControlPanel(bool Save)
        {

            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form_Close);

        }

        public void key_press(object sender, KeyEventArgs e)
        {

            keyTest(e);

        }


        private void keyTest(KeyEventArgs e)
        {

            if (e.KeyCode.ToString() == "S")
            {

                screenCapture(true);

            }

        }


        private void Form_Close(object sender, FormClosedEventArgs e)
        {

            Application.Exit();

        }

        private void bttCaptureArea_Click(object sender, EventArgs e)
        {

            this.Hide();
            Form1 form1 = new Form1();
            form1.InstanceRef = this;
            form1.ShowDialog();
            this.Dispose(true);

        }

        public void screenCapture(bool showCursor)
        {

            Point curPos = new Point(Cursor.Position.X, Cursor.Position.Y);
            Size curSize = new Size();
            curSize.Height = Cursor.Current.Size.Height;
            curSize.Width = Cursor.Current.Size.Width;

            ScreenPath = "";

            if (!ScreenShot.saveToClipboard)
            {

                saveFileDialog1.DefaultExt = "png";
                saveFileDialog1.Filter = "png files (*.png)|*.png|bmp files (*.bmp)|*.bmp|jpg files (*.jpg)|*.jpg|gif files (*.gif)|*.gif|tiff files (*.tiff)|*.tiff";
                saveFileDialog1.Title = "Save screenshot to...";
                saveFileDialog1.ShowDialog();
                ScreenPath = saveFileDialog1.FileName;

            }

            

            if (ScreenPath!=""||ScreenShot.saveToClipboard)
            {

                //Conceal this form while the screen capture takes place
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                this.TopMost = false;
                

                //Allow 250 milliseconds for the screen to repaint itself (we don't want to include this form in the capture)
                System.Threading.Thread.Sleep(250);

                Rectangle bounds = Screen.GetBounds(Screen.GetBounds(Point.Empty));
                string fi = "";

                if (ScreenPath != "")
                {

                    fi = new FileInfo(ScreenPath).Extension;

                }

                ScreenShot.CaptureImage(showCursor, curSize, curPos, Point.Empty, Point.Empty, bounds, ScreenPath, fi);
                          
                //The screen has been captured and saved to a file so bring this form back into the foreground
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                this.TopMost = true;

                if (ScreenShot.saveToClipboard)
                {

                    MessageBox.Show("Screen saved to clipboard", "AIO2013", MessageBoxButtons.OK);
                    this.Dispose(true);

                }
                else
                {

                    MessageBox.Show("Screen saved to file", "AIO2013", MessageBoxButtons.OK);
                    this.Dispose(true);

                }


            }


        }

        private void bttCaptureScreen_Click(object sender, EventArgs e)
        {

            screenCapture(false);

        }

        

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {

            this.KeyUp += new KeyEventHandler(key_press);

            System.Text.Encoding Encoder = System.Text.ASCIIEncoding.Default;
            Byte[] buffer = new byte[] { (byte)149 };
            string bullet = System.Text.Encoding.GetEncoding(1252).GetString(buffer);

           

        }

        private void saveToClipboard_CheckedChanged(object sender, EventArgs e)
        {

            ScreenShot.saveToClipboard = saveToClipboard.Checked;
            
        }

        private void saveToClipboard_KeyUp(object sender, KeyEventArgs e)
        {

            keyTest(e);

        }

        private void bttCaptureArea_KeyUp(object sender, KeyEventArgs e)
        {

            keyTest(e);

        }

        private void bttTips_KeyUp(object sender, KeyEventArgs e)
        {

            keyTest(e);

        }

        private void bttCaptureScreen_KeyUp(object sender, KeyEventArgs e)
        {

            keyTest(e);

        }

        private void txtTips_KeyUp(object sender, KeyEventArgs e)
        {

            keyTest(e);

        }

    }
}