using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace AIO2013
{
    public partial class settingsForm : Form
    {
        public Boolean btnPressed = false;
        public Boolean clrHistory = false;

        public settingsForm()
        {
            InitializeComponent();
            pcAnywherePathTxtBox.Text = Properties.Settings.Default.PcAnyWherePath;
            DWtxtBox.Text = Properties.Settings.Default.DameWarePath;
            SMStextBox1.Text = Properties.Settings.Default.SMSPath;
            portalTextBox1.Text = Properties.Settings.Default.WebPortal;
            quickLink1.Text = Properties.Settings.Default.QuickLinks1;
            quickLink2.Text = Properties.Settings.Default.QuickLinks2;
            quickLink3.Text = Properties.Settings.Default.QuickLinks3;
            quickLink4.Text = Properties.Settings.Default.QuickLinks4;
            quickLink5.Text = Properties.Settings.Default.QuickLinks5;
            quickLink6.Text = Properties.Settings.Default.QuickLinks6;
            CRMtextBox2.Text = Properties.Settings.Default.CRMPath;
            kbTextBox3.Text = Properties.Settings.Default.kbPath;
            ADtextBox1.Text = Properties.Settings.Default.ADPath;
            histNumComboBox.Text = Properties.Settings.Default.historyNum.ToString();
            updatesCheckBox.Checked = Properties.Settings.Default.autoUpdateChk;
            psToolsTxtBox.Text = Properties.Settings.Default.psToolsPath;

            #region populate user shortcuts

            //pop user shortcut 1
            if (!string.IsNullOrEmpty(Properties.Settings.Default.userSC1))
            {
                string[] usr1 = Properties.Settings.Default.userSC1.Split('|');
                usr1Group.Enabled = Convert.ToBoolean(usr1[0]);
                usr1chk.Checked = usr1Group.Enabled;
                usr1PathTxt.Text = usr1[1];
                usr1NameTxt.Text = usr1[2];
                usr1ArgTxt.Text = usr1[3];
            }
            //pop user shortcut 2
            if (!string.IsNullOrEmpty(Properties.Settings.Default.userSC2))
            {
                string[] usr2 = Properties.Settings.Default.userSC2.Split('|');
                usr2Group.Enabled = Convert.ToBoolean(usr2[0]);
                usr2chk.Checked = usr2Group.Enabled;
                usr2PathTxt.Text = usr2[1];
                usr2NameTxt.Text = usr2[2];
                usr2ArgTxt.Text = usr2[3];
            }
            //pop user shortcut 3
            if (!string.IsNullOrEmpty(Properties.Settings.Default.userSC3))
            {
                string[] usr3 = Properties.Settings.Default.userSC3.Split('|');
                usr3Group.Enabled = Convert.ToBoolean(usr3[0]);
                usr3chk.Checked = usr3Group.Enabled;
                usr3PathTxt.Text = usr3[1];
                usr3NameTxt.Text = usr3[2];
                usr3ArgTxt.Text = usr3[3];
            }
            //pop user shortcut 4
            if (!string.IsNullOrEmpty(Properties.Settings.Default.userSC4))
            {
                string[] usr4 = Properties.Settings.Default.userSC4.Split('|');
                usr4Group.Enabled = Convert.ToBoolean(usr4[0]);
                usr4chk.Checked = usr4Group.Enabled;
                usr4PathTxt.Text = usr4[1];
                usr4NameTxt.Text = usr4[2];
                usr4ArgTxt.Text = usr4[3];
            }
            //pop user shortcut 5
            if (!string.IsNullOrEmpty(Properties.Settings.Default.userSC5))
            {
                string[] usr5 = Properties.Settings.Default.userSC5.Split('|');
                usr5Group.Enabled = Convert.ToBoolean(usr5[0]);
                usr5chk.Checked = usr5Group.Enabled;
                usr5PathTxt.Text = usr5[1];
                usr5NameTxt.Text = usr5[2];
                usr5ArgTxt.Text = usr5[3];
            }
            #endregion

            populateDwCred();

        }

        private void populateDwCred()
        {
            #region populate dameware cred

            dwCredCombo.Items.Clear(); ;
            String creds = ToInsecureString(DecryptString(Properties.Settings.Default.dwCred));
            if (!String.IsNullOrEmpty(creds))
            {
                string[] credsArr = creds.Split('|');
                int i = 0;
                foreach (string cred in credsArr)
                {
                    if (!string.IsNullOrEmpty(cred))
                    {
                        dwCredCombo.Items.Add(cred.Split(';')[0]);
                    }
                    i++;
                }

            }

            #endregion
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (btnPressed)
            {
                
            }
            else
            {

                base.OnFormClosing(e);

                if (e.CloseReason == CloseReason.WindowsShutDown) return;

                // Confirm user wants to close
                switch (MessageBox.Show(this, "Are you sure you want to close?\r\n No changes will be saved !", "Closing", MessageBoxButtons.YesNo))
                {
                    case DialogResult.No:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
        }


        public static void dwSaveNewCred(string disName, string userName, string pass, string domain)
        {
            String creds = ToInsecureString(DecryptString(Properties.Settings.Default.dwCred));
            creds = creds + disName + ";" + userName + ";" + pass + ";" + domain + "|";
            Properties.Settings.Default.dwCred = EncryptString(ToSecureString(creds));
            
        }

        private void pcAWBrowseBtn_Click(object sender, EventArgs e)
        {
            
            
            if (pcAWopenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pcAnywherePathTxtBox.Text = pcAWopenFileDialog1.FileName;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pcAnywherePathTxtBox.Text = @"C:\Program Files\Symantec\pcAnywhere\awrem32.exe";
        }

        private void DMbrowseBtn_Click(object sender, EventArgs e)
        {
            if (DWopenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DWtxtBox.Text = DWopenFileDialog1.FileName;
            }
        }

        private void DWdefault_Click(object sender, EventArgs e)
        {
            DWtxtBox.Text = @"c:\Program Files\DameWare Development\DameWare Mini Remote Control\DWRCC.exe";
        }


        private void SMSbrowse_Click(object sender, EventArgs e)
        {
            if (SMSopenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SMStextBox1.Text = SMSopenFileDialog1.FileName;
            }
        }

        private void SMSbuiltin_Click(object sender, EventArgs e)
        {
            SMStextBox1.Text = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Resources\\SMS\\rc.exe";
        }


        private void saveBtn_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.PcAnyWherePath = pcAnywherePathTxtBox.Text;
            Properties.Settings.Default.DameWarePath = DWtxtBox.Text;
            Properties.Settings.Default.SMSPath = SMStextBox1.Text;
            Properties.Settings.Default.WebPortal = portalTextBox1.Text;

            Properties.Settings.Default.QuickLinks1 = quickLink1.Text;
            Properties.Settings.Default.QuickLinks2 = quickLink2.Text;
            Properties.Settings.Default.QuickLinks3 = quickLink3.Text;
            Properties.Settings.Default.QuickLinks4 = quickLink4.Text;
            Properties.Settings.Default.QuickLinks5 = quickLink5.Text;
            Properties.Settings.Default.QuickLinks6 = quickLink6.Text;

            Properties.Settings.Default.CRMPath = CRMtextBox2.Text;
            Properties.Settings.Default.kbPath = kbTextBox3.Text;
            Properties.Settings.Default.ADPath = ADtextBox1.Text;

            Properties.Settings.Default.historyNum = Convert.ToInt32(histNumComboBox.Text);
            Properties.Settings.Default.autoUpdateChk = updatesCheckBox.Checked;
            Properties.Settings.Default.psToolsPath = psToolsTxtBox.Text;
            
            #region saving user shortcuts
            if (usr1chk.Checked)
            {
                Properties.Settings.Default.userSC1 = usr1chk.Checked.ToString() + "|" + usr1PathTxt.Text + "|" +
                                                      usr1NameTxt.Text + "|" + usr1ArgTxt.Text;
            }
            else
            {
                Properties.Settings.Default.userSC1 = "";
            }

            if (usr2chk.Checked)
            {
                Properties.Settings.Default.userSC2 = usr2chk.Checked.ToString() + "|" + usr2PathTxt.Text + "|" +
                                                      usr2NameTxt.Text + "|" + usr2ArgTxt.Text;
            }
            else
            {
                Properties.Settings.Default.userSC2 = "";
            }


            if (usr3chk.Checked)
            {
                Properties.Settings.Default.userSC3 = usr3chk.Checked.ToString() + "|" + usr3PathTxt.Text + "|" +
                                                      usr3NameTxt.Text + "|" + usr3ArgTxt.Text;
            }
            else
            {
                Properties.Settings.Default.userSC3 = "";
            }


            if (usr4chk.Checked)
            {
                Properties.Settings.Default.userSC4 = usr4chk.Checked.ToString() + "|" + usr4PathTxt.Text + "|" +
                                                      usr4NameTxt.Text + "|" + usr4ArgTxt.Text;
            }
            else
            {
                Properties.Settings.Default.userSC4 = "";
            }


            if (usr5chk.Checked)
            {
                Properties.Settings.Default.userSC5 = usr5chk.Checked.ToString() + "|" + usr5PathTxt.Text + "|" +
                                                      usr5NameTxt.Text + "|" + usr5ArgTxt.Text;
            }
            else
            {
                Properties.Settings.Default.userSC5 = "";
            }
            #endregion

            

                Properties.Settings.Default.Save();
                btnPressed = true;
                this.Close();
            


        }

        

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
           
           
        }

        private void portalDefaultBtn_Click_1(object sender, EventArgs e)
        {
            portalTextBox1.Text = "https://google.com";
        }


        private void kbDefaultBtn_Click(object sender, EventArgs e)
        {
            kbTextBox3.Text = "https://support.microsoft.com/";
        }

        private void ADDefBtn_Click(object sender, EventArgs e)
        {
            ADtextBox1.Text = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
                    "\\Resources\\ad.rdp";
        }

        private void ADBrowseBtn_Click(object sender, EventArgs e)
        {
            if (ADOpenFileDislog.ShowDialog() == DialogResult.OK)
            {
                ADtextBox1.Text = ADOpenFileDislog.FileName;
            }
        }

        private void user1Browse_Click(object sender, EventArgs e)
        {
            if(UsrSCopenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Button usrSCBrowse = (Button) sender;
                int index = Convert.ToInt32(usrSCBrowse.Name[4].ToString());

            switch (index)
            {
                case 1:
                    usr1PathTxt.Text = UsrSCopenFileDialog1.FileName;
                    break;
                case 2:
                    usr2PathTxt.Text = UsrSCopenFileDialog1.FileName;
                    break;
                case 3:
                    usr3PathTxt.Text = UsrSCopenFileDialog1.FileName;
                    break;
                case 4:
                    usr4PathTxt.Text = UsrSCopenFileDialog1.FileName;
                    break;
                case 5:
                    usr5PathTxt.Text = UsrSCopenFileDialog1.FileName;
                    break;
                default:
                    break;
            }
            }
        }

        private void usr1chk_CheckStateChanged(object sender, EventArgs e)
        {
            //get the index of the checkbox
            CheckBox chk = (CheckBox) sender;
            int index = Convert.ToInt32(chk.Name[3].ToString());

            //enable or disable the right group by the index
            #region enable/disable by index
            switch (index)
            {
                case 1:
                    if (usr1chk.Checked == true)
                    {
                        usr1Group.Enabled = true;
                    }
                    else
                    {
                        usr1Group.Enabled = false;
                    }
                    break;

                case 2:
                    if (usr2chk.Checked == true)
                    {
                        usr2Group.Enabled = true;
                    }
                    else
                    {
                        usr2Group.Enabled = false;
                    }
                    break;

                case 3:
                    if (usr3chk.Checked == true)
                    {
                        usr3Group.Enabled = true;
                    }
                    else
                    {
                        usr3Group.Enabled = false;
                    }
                    break;

                case 4:
                    if (usr4chk.Checked == true)
                    {
                        usr4Group.Enabled = true;
                    }
                    else
                    {
                        usr4Group.Enabled = false;
                    }
                    break;

                case 5:
                    if (usr5chk.Checked == true)
                    {
                        usr5Group.Enabled = true;
                    }
                    else
                    {
                        usr5Group.Enabled = false;
                    }
                    break;
            #endregion

            }

            
        }

        private void clrHistBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("History deleted", "AIO2013", MessageBoxButtons.OK);
            clrHistory = true;
        }

   #region encrypt functions

        static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Salt Is Not A Password");

     

        public static string EncryptString(System.Security.SecureString input)
        {
           
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }
        #endregion

        private void dxCredAddBtn_Click(object sender, EventArgs e)
        {
            dameware_cred dwAddCred = new dameware_cred();
            dwAddCred.ShowDialog();
            if(dwAddCred.DialogResult == DialogResult.OK)
            {
                populateDwCred();
            }
        }

        private void dxCredDelBtn_Click(object sender, EventArgs e)
        {
            if (dwCredCombo.SelectedIndex<0)
            {
                MessageBox.Show("Please select a credential from the list !");
            }
            else
            {
                 String creds = ToInsecureString(DecryptString(Properties.Settings.Default.dwCred));
                 if (!String.IsNullOrEmpty(creds))
                 {
                     string[] credsArr = creds.Split('|');
                     var list = new List<string>(credsArr);
                     list.RemoveAt(dwCredCombo.SelectedIndex);
                     credsArr = list.ToArray();
                     creds = "";
                     foreach (var cred in credsArr)
                     {
                         if(!string.IsNullOrEmpty(cred))
                         {
                             creds = creds + cred + "|";
                         }
                     }
                    
                     Properties.Settings.Default.dwCred = EncryptString(ToSecureString(creds));
                     populateDwCred();
                 }
            }
           
        }

        private void update_btn_Click(object sender, EventArgs e)
        {
           aboutForm about = new aboutForm();
            about.ShowDialog();
        }


        private void psToolsFolderBrwse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!System.IO.File.Exists(folderBrowserDialog1.SelectedPath + @"\PsExec.exe"))
                {
                    MessageBox.Show("Can't find PsExec.exe, Please check again and choose the right folder");
                }else if (!System.IO.File.Exists(folderBrowserDialog1.SelectedPath + @"\PsInfo.exe"))
                {
                    MessageBox.Show("Can't find PsInfo.exe, Please check again and choose the right folder");
                }
                else
                {
                    psToolsTxtBox.Text = folderBrowserDialog1.SelectedPath;
                }
            }
        }
    }
}
