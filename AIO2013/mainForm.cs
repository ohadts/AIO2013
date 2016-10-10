using System;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Threading;
using System.Net.NetworkInformation;
using System.Text;
using System.Diagnostics;
using System.Management;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Principal;
using Microsoft.Win32;
using Teboscreen;
using MsdnMag;

namespace AIO2013
{
    //declare the delegate that we'll use to launch our worker function in a separate thread
   // public delegate void workerFunctionDelegate(string hostAdrress);

    //declare the delegate that we'll use to call the function that displays text in our text box
    public delegate void poplateTextBoxDelegate(string text, int appendMethod, Color color);





    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
            if(Properties.Settings.Default.autoUpdateChk)
            {
                check4updates();
            }
            if (Environment.OSVersion.Version.Major < 5.3)
            {
                BatchFilesBtn.Enabled = false;
                toolTip1.SetToolTip(BatchFilesBtn, "Only available from Windows Vista and above");
            }
            populateFilesFav();
        }

        private void check4updates()
        {
            string latestVer;
            var worker = new BackgroundWorker();

            worker.DoWork += (sender, argss) =>
            {
                try
                {
                    argss.Result = new WebClient().DownloadString(Properties.Settings.Default.versionChkUrl);

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
                    UpdateMyrichTextBox("Can't check for updates: " + e.Error.Message, 2, Color.AliceBlue); 
                }
                else
                {

                    latestVer = e.Result.ToString();
                   
                    if (Properties.Settings.Default.version.ToString() != latestVer)
                    {
                        updateForm update = new updateForm();
                        update.ShowDialog();
                    }
                }
            };



            worker.RunWorkerAsync();
           

            //throw new NotImplementedException();
        }

        public static Stack<BackgroundWorker> longPeriodWorkers;
      


        //this function will simply write text to our text box
        //this function will later be called from a worker thread through the use of a delegate using the Invoke method on the form
        private void populateTextBox(string text, int appendMethod, Color color)
        {

            if (appendMethod == 1)
            {

                myrichTextBox.SelectionStart = myrichTextBox.TextLength;
                myrichTextBox.SelectionLength = 0;

                myrichTextBox.SelectionColor = color;
                myrichTextBox.AppendText(text);
                myrichTextBox.SelectionColor = myrichTextBox.ForeColor;

            }
            else
            {
                myrichTextBox.SelectionStart = myrichTextBox.TextLength;
                myrichTextBox.SelectionLength = 0;

                myrichTextBox.SelectionColor = color;
                myrichTextBox.AppendText(text + "\r\n");
                myrichTextBox.SelectionColor = myrichTextBox.ForeColor;
            }




        }

        public string pingResults;
        private object threadLock = new object();

        //this function simulates "work" by simply counting from 1 to totalSeconds
        public void pingFunction(string host)
        {
            lock (threadLock)
            {


                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                pingResults = "";



                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;

                // Create a buffer of 32 bytes of data to be transmitted.
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                StringBuilder result = new StringBuilder();

                try
                {
                    PingReply reply = pingSender.Send(host, timeout, buffer, options);

                    if (reply.Status == IPStatus.Success)
                    {
                        pingResults = ("RoundTrip time: " + reply.RoundtripTime);
                    }
                    else
                    {
                        pingResults = ("No connection: " + reply.Status);
                    }
                }


                catch (Exception ex)
                {
                    if (ex.Message == "Value cannot be null.\r\nParameter name: hostNameOrAddress")
                    {
                        MessageBox.Show("Error!\r\nEmpty value in text box.");
                    }
                    else
                    {
                        pingResults = ("Error: " + ex.InnerException.Message);
                    }
                }


                finally
                {

                    stopWatch.Stop();
                    TimeSpan tsDur = stopWatch.Elapsed;
                    int Sleep = 1000 - tsDur.Milliseconds;
                    Thread.Sleep(Sleep);

                }

                UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox(" Ping " + pingResults, 2, Color.AliceBlue);


            }


        }



        public void fillStatusBar()
        {
            TimeSpan threadTimeOut = new TimeSpan(0, 0, 3);

            hostnameOrIp one = new hostnameOrIp();
            hostnameOrIp.recData = hostnameTextbox.Text;
            one.hostLabel = hostnameStatusStripLabel;
            one.ipLabel = ipaddressStatusStripLabel;

            Thread t = new Thread(new ThreadStart(one.Resolve));
            t.Start();
            t.Join(threadTimeOut);

        }

        private void myrichTextBox_TextChanged(object sender, EventArgs e)
        {
            myrichTextBox.SelectionStart = myrichTextBox.Text.Length; //Set the current caret position to the end 
            myrichTextBox.ScrollToCaret(); //Now scroll it automatically 
        }


        public void UpdateMyrichTextBox(string text, int appendMethod, Color color)
        {

            this.Invoke(new poplateTextBoxDelegate(populateTextBox), new object[] {text, appendMethod, color});
        }

        public void save2history() //save to checkbox history
        {

            string[] historyArray = new string[Properties.Settings.Default.historyNum];

            for (int i = 1; i < Properties.Settings.Default.historyNum; i++)
            {
                if (hostnameTextbox.Items.Count >= i)
                    historyArray[i] = hostnameTextbox.Items[i - 1].ToString();
            }
            string[] no_dup = historyArray.Distinct().ToArray();
            no_dup[0] = hostnameTextbox.Text.ToString();

            hostnameTextbox.Items.Clear();
            for (int i = 0; i < no_dup.Length; i++)
            {
                if (!string.IsNullOrEmpty(no_dup[i]))
                    hostnameTextbox.Items.Add(no_dup[i]);

            }


        }



        public void whosLoggedIn(string host)
        {

            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2",
                                                 "SELECT * FROM Win32_ComputerSystem");

                try
                {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    if (queryObj["UserName"] != null)
                    {
                        UpdateMyrichTextBox(" UserName: " + queryObj["UserName"], 2, Color.AliceBlue);
                    }
                    else
                    {
                        UpdateMyrichTextBox(" No one is logged-on localy !", 2, Color.AliceBlue);
                    }
                }
                }
                catch (Exception e)
                {
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox("An error occurred while querying for WMI data: " + e.Message, 2, Color.AliceBlue);
                }
            }
            catch (ManagementException ex)
            {
                UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox(" An error occurred while querying for WMI data: " + ex.Message, 2, Color.AliceBlue);
            }

        }


        public string whosLoggedInWreturn(string host)
        {
            string username = null;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2",
                                                 "SELECT * FROM Win32_ComputerSystem");
                try
                {   
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj["UserName"] != null)
                    {
                        username = (string) queryObj["UserName"];
                        NoLoggedUser = false;
                    }
                    else
                    {
                        username = "0";
                        NoLoggedUser = true;
                    }
                }

                }
                catch (Exception e)
                {
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" An error occurred while querying for WMI data: " + e.Message, 2, Color.AliceBlue);
                    username = "1";
                } 
            }
            catch (ManagementException ex)
            {
                UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox("An error occurred while querying for WMI data: " + ex.Message, 2, Color.AliceBlue);
                username = "1";
            }
            return username;


        }


        public void modelInfo(string host)
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2",
                                                 "SELECT * FROM Win32_ComputerSystem");
                try
                {

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" Manufacturer: " + queryObj["Manufacturer"], 2, Color.AliceBlue);
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" Model: " + queryObj["Model"], 2, Color.AliceBlue);
                }

                }
                catch (Exception e)
                {
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" An error occurred while querying for WMI data: " + e.Message, 2, Color.AliceBlue);
                }
            }
            catch (ManagementException e)
            {
                UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox(" An error occurred while querying for WMI data: " + e.Message, 2, Color.AliceBlue);
            }

        }

        public void scrnShotFunc(string host)
        {
            string psExec = "";
            if (Properties.Settings.Default.psToolsPath.ToString() == "")
            {
                throw new Exception("PsTools folder must be set in Settings->Misc Tab");
            }
            else
            {
                psExec = Properties.Settings.Default.psToolsPath.ToString() + @"\PsExec.exe";
            }
            string exePath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
            Process compiler = new Process();
            compiler.StartInfo.FileName = psExec;
            compiler.StartInfo.Arguments = "\\\\" + host + " -i -f -c -s \"" + exePath +
                                           "\\Resources\\remoteScreenshot\\scrn.exe\"";
            compiler.StartInfo.UseShellExecute = false;
            compiler.StartInfo.RedirectStandardOutput = true;
            compiler.StartInfo.CreateNoWindow = true;
            
            
            
                

            try
            {
                // Start the process.
                compiler.Start();

               

                int i = 0;
                // Display the process statistics until 
                // the user closes the program. 
                do
                {
                    
                    if (!compiler.HasExited)
                    {
                        i++;
                        if (i ==20)
                        {
                            compiler.Kill();
                        }
                        UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                        UpdateMyrichTextBox(" Waiting for user approval..." + (21-i).ToString() + " seconds.", 2, Color.AliceBlue);
                        
                    }
                } while (!compiler.WaitForExit(1000));
            }
            finally
            {
                if (compiler != null)
                {
                    
                    if (compiler.ExitCode < 0) //aborted by supporter, killing...
                    {
                        killScreenShot(host);
                    }

                    if (compiler.ExitCode == 0) //aborted by user...
                        {
                            UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" User did not Approve !!!", 2, Color.Red);
                        }
                    if (compiler.ExitCode == 98) //aborted by user...
                    {
                        UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                        UpdateMyrichTextBox(" User did not Approve !!!", 2, Color.Red);
                    }
                    if (compiler.ExitCode == 97) //aborted by user...
                    {
                        UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                        UpdateMyrichTextBox(" User pressed 'X' and closed the window !!!", 2, Color.Red);
                    }
                        if (compiler.ExitCode == 100) //user approved !
                        {
                            UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Image recieved! opening save dialog...", 2, Color.Aqua);
                            scrnShotSaveDialog.InitialDirectory =
                                Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            scrnShotSaveDialog.FileName = host + "_Screenshot";
                            scrnShotSaveDialog.ShowDialog();

                            if (scrnShotSaveDialog.FileName != "")
                            {
                                try
                                {
                                    if (File.Exists(scrnShotSaveDialog.FileName))
                                    {
                                        File.Delete(scrnShotSaveDialog.FileName);
                                    }
                                    File.Move(@"\\" + host + @"\c$\windows\temp\test.png", scrnShotSaveDialog.FileName);
                                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                                    UpdateMyrichTextBox(" Image saved. Remote screenshot completed.", 2, Color.Aqua);
                           
                                }
                                catch (Exception exp)
                                {

                                    MessageBox.Show(exp.Message);
                                }
                                
                            }
                        }
                        if (compiler.ExitCode == 99) //some sort of error...
                        {
                            try
                            {
                                if (File.Exists(@"\\" + host + @"\c$\windows\temp\" + host + "_Errorlog.log"))
                                {
                                    File.Move(@"\\" + host + @"\c$\windows\temp\" + host + "_Errorlog.log", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Logs\\" + host + DateTime.Now + "_Errorlog.log");
                                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                                    UpdateMyrichTextBox(" We have an Error ! error log can be found in \"Logs\" directory", 2, Color.Red);
                                }
                                else
                                {
                                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                                    UpdateMyrichTextBox(" We have an Error ! but no log file can be found...", 2, Color.Red);
                                }
                            }

                            catch (Exception ex)
                            {

                                MessageBox.Show(ex.Message);
                            }
                            
                            
                        }
                        if (compiler.ExitCode == 6) //RPC Error...
                        {
                            UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Something went terribly wrong :(    ABORTING!", 2, Color.Red);
                        }

                        compiler.Close();
                    }
                





            }

        }


        public void killScreenShot(string host)
        {
            Process[] processes = null;




            try
            {
                int handle = 0;
                processes = Process.GetProcessesByName("scrn", host);
                foreach (Process p in processes)
                {
                    handle = p.Id;
                }
                ManagementObject classInstance =
                    new ManagementObject("\\\\" + host + "\\root\\CIMV2",
                                         "Win32_Process.Handle='" + handle + "'",
                                         null);

                // Obtain in-parameters for the method
                ManagementBaseObject inParams =
                    classInstance.GetMethodParameters("Terminate");



                // Execute the method and obtain the return values.
                ManagementBaseObject outParams =
                    classInstance.InvokeMethod("Terminate", inParams, null);


                if (outParams["ReturnValue"].ToString() == "0")
                {
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" Opertation Timeout. Remote Screenshot killed !", 2, Color.AliceBlue);


                }
            }
            catch (ManagementException err)
            {
                MessageBox.Show("An error occurred while trying to execute the WMI method: " + err.Message);
            }
        }
           

        public void shutdownFunc(string host, int action)
        {
            Boolean isPCHealthy = true;

            try
            {
                ConnectionOptions options = new ConnectionOptions();
                options.EnablePrivileges = true;
                // To connect to the remote computer using a different account, specify these values:
                // options.Username = "USERNAME";
                // options.Password = "PASSWORD";
                // options.Authority = "ntlmdomain:DOMAIN";

                ManagementScope scope = new ManagementScope(
                  "\\\\" + host + "\\root\\CIMV2", options);
                try
                {
                    scope.Connect();
                }
                catch (Exception e)
                {
                    isPCHealthy = false;
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" An error occurred while while trying to execute the WMI method: " + e.Message, 2, Color.Magenta);
                }

                if (isPCHealthy)
                {
                    SelectQuery query = new SelectQuery("Win32_OperatingSystem");
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(scope, query);

                    //if isnullorEmpty(query) ?
                    foreach (ManagementObject os in searcher.Get())
                    {
                        // Obtain in-parameters for the method
                        ManagementBaseObject inParams =
                            os.GetMethodParameters("Win32Shutdown");

                        // Add the input parameters.
                        inParams["Flags"] = action;

                        // Execute the method and obtain the return values.
                        ManagementBaseObject outParams =
                            os.InvokeMethod("Win32Shutdown", inParams, null);
                    }

                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" Shutdown signal successfully lunch.", 2, Color.AliceBlue);
                }
            }
            catch (ManagementException err)
            {
                UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox(" An error occurred while while trying to execute the WMI method: " + err.Message, 2, Color.Magenta);
            }
            catch (System.UnauthorizedAccessException unauthorizedErr)
            {
                UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox("Connection error: " + unauthorizedErr.Message, 2, Color.Magenta); 
            }

        }


        public void cpuInfo(string host)
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2",
                                                 "SELECT * FROM Win32_Processor");
                try
                {

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" Name: " + queryObj["Name"].ToString().Trim(), 2, Color.AliceBlue);
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" Logical Cores: " + queryObj["NumberOfCores"], 2, Color.AliceBlue);
                }

                }
                catch (Exception e)
                {
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" An error occurred while querying for WMI data: " + e.Message, 2, Color.AliceBlue);
                }    
            }
            catch (ManagementException e)
            {
                UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox(" An error occurred while querying for WMI data: " + e.Message, 2, Color.AliceBlue);
            }


        }



        public void popPrinterList(List<printer> printers)
        {
            this.olvColumn1.ImageGetter = new BrightIdeasSoftware.ImageGetterDelegate(printersColumnImageGetter);
            printersListView.SetObjects(printers);
            if (printers.Count > 0)
            {
                prntLabel.Visible = true;    
            }

        }


        object printersColumnImageGetter(object rowObject)
        {
            printer p = (printer)rowObject;
            if (p.Default)
            {
                if (p.status == "Error" || p.status == "Offline")
                    return 4;
                if (p.status == "Idle")
                    return 0;
                return 5;
            }
            else
            {
                if (p.status == "Error" || p.status == "Offline")
                    return 2;
                if (p.status == "Idle")
                    return 1;
                return 3;
            }
        }

        private void printerProperties_Click(object sender, EventArgs e)
        {
            if (printersListView.SelectedObject == null)
            {
                UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox(" You MUST select a printer from the list !", 2, Color.AliceBlue);

            }
            else
            {
                printer prntr = (printer)printersListView.SelectedObject;
                UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox(" Lunching " + prntr.name + " Properties", 2,
                                    Color.AliceBlue);

                string pcaStr = "rundll32";
                Process pcaProcess = new Process();
                pcaProcess.StartInfo.FileName = pcaStr;
                pcaProcess.StartInfo.Arguments = "printui.dll,PrintUIEntry /p /n \"\\\\" + hostnameStatusStripLabel.Text +
                                                 "\\" + prntr.name + "\"";
                pcaProcess.Start();
            }
        }


        private void printersListView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {







            //MessageBox.Show(printersListView.Rows[printersListView.CurrentRow.Index].Cells[0].Value.ToString());

        }







        public delegate void popPrinterListDelegate(List<printer> printers);

        

        public void fetchPrinters(string host)
        {

            //open printer properties: rundll32 printui.dll,PrintUIEntry /p  /n "\\hfpc1004215\Brother HL-5340D series"


            //set default:
            //strComputer = "." 
            //Set objWMIService = GetObject("winmgmts:\\" & strComputer & "\root\CIMV2") 
            //' Obtain an instance of the the class 
            //' using a key property value.
            //Set objShare = objWMIService.Get("Win32_Printer.DeviceID='Brother MFC-7420 USB Printer'")

            //' no InParameters to define

            //' Execute the method and obtain the return status.
            //' The OutParameters object in objOutParams
            //' is created by the provider.
            //Set objOutParams = objWMIService.ExecMethod("Win32_Printer.DeviceID='Brother MFC-7420 USB Printer'", "SetDefaultPrinter")

            //' List OutParams
            //Wscript.Echo "Out Parameters: "
            //Wscript.echo "ReturnValue: " & objOutParams.ReturnValue

            popPrinterListDelegate popPrinterListDelegate = new popPrinterListDelegate(popPrinterList);

            List<printer> printers = new List<printer>();
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2",
                                                 "SELECT * FROM Win32_Printer");

                string defaultPrinter = getDefaultPrinter(host, whosLoggedInWreturn(host));
                bool isDefault;

                try
                {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    isDefault = checkDefault(defaultPrinter, (string) queryObj["Name"]);
                    printers.Add(new printer((string) queryObj["Name"], (string) queryObj["PortName"],
                                             (string)queryObj["ShareName"], Convert.ToInt32(queryObj["PrinterStatus"]), isDefault));
                }

                }
                catch (Exception e)
                {

                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" An error occurred while querying for WMI data: " + e.Message, 2, Color.AliceBlue);
                }
                

            }
            catch (ManagementException e)
            {
                UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox("An error occurred while querying for WMI data: " + e.Message, 2, Color.AliceBlue);
            }
           

            for (int i = printers.Count - 1; i >= 0; i--)
            {
                printer prntr = printers[i];
                if (prntr.Default)
                {
                    printers.RemoveAt(i);
                    printers.Insert(0, prntr);
                }
            }    
            
            printersListView.Invoke(popPrinterListDelegate, printers);
            

        }

        private bool checkDefault(string defaultPrinter, string printer2check)
        {
            if (defaultPrinter == printer2check)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string getDefaultPrinter(string host, string p)
        {

            string defaultPrinter = null;
            if (p == "0" | p == "1")
            {
                UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox(" Unable to determine default printer: any user logged in ?", 2, Color.AliceBlue);
                return "Error";
            }
            else
            {

                NTAccount f = new NTAccount(p);
                SecurityIdentifier s = (SecurityIdentifier) f.Translate(typeof (SecurityIdentifier));
                String sidString = s.ToString();
                try
                {


                    devicesKey = RegistryKey.OpenRemoteBaseKey(
                        RegistryHive.Users, host).OpenSubKey(
                            sidString + "\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows");



                    string[] list = devicesKey.GetValue("Device").ToString().Split(',');
                    defaultPrinter = list[0];


                }
                catch (Exception)
                {

                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(
                        "Unable to determine default printer: No local printer installed, or unable to connect to remote registry",
                        2, Color.AliceBlue);
                }
                return defaultPrinter;
            }
        }





        public void printersSrvPro(string host)
        {
            string pcaStr = "rundll32";
            Process pcaProcess = new Process();
            pcaProcess.StartInfo.FileName = pcaStr;
            pcaProcess.StartInfo.Arguments = "printui.dll,PrintUIEntry /s /c \"\\\\" + host;
            pcaProcess.Start();
        }

        public void ramInfo(string host)
        {


            try
            {
                ManagementObjectSearcher Availablesearcher =
                    new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2",
                                                 "SELECT * FROM Win32_PerfFormattedData_PerfOS_Memory");

                ManagementObjectSearcher Totalsearcher = new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2",
                                                                                      "SELECT * FROM Win32_ComputerSystem");

                if (Totalsearcher != null)
                {
                    Int64 totalRam = 0;
                    Int64 availableRam = 0;

                    foreach (ManagementObject TotalqueryObj in Totalsearcher.Get())
                    {

                        totalRam = Convert.ToInt64(TotalqueryObj["TotalPhysicalMemory"]);
                    }

                    try
                    {

                        foreach (ManagementObject AvailablequeryObj in Availablesearcher.Get())
                        {


                            availableRam = Convert.ToInt64(AvailablequeryObj["AvailableMBytes"]);
                        }

                    }
                    catch (Exception e)
                    {

                        UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                        UpdateMyrichTextBox(" Error: " + e.Message, 2, Color.AliceBlue);
                    }

                    if (availableRam != 0)
                    {
                        totalRam = totalRam / 1024 / 1024;

                        Int64 usedRam = totalRam - availableRam;

                        UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                        UpdateMyrichTextBox(" Total Physical Memory: " + totalRam + "MB", 2, Color.AliceBlue);
                        UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                        UpdateMyrichTextBox("Used Physical Memory: " + usedRam + "MB", 2, Color.AliceBlue);
                        UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                        UpdateMyrichTextBox(" Free Physical Memory: " + availableRam + "MB", 2, Color.AliceBlue);
                    }
                }
                else
                {
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" Unable to fetch data !!", 2, Color.AliceBlue);
                }

            }
            catch (ManagementException e)
            {
                UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox("An error occurred while querying for WMI data: " + e.Message, 2, Color.AliceBlue);
            }
        }


        public void SMARTfunc(string host)
        {
            SMARTform smart = new SMARTform(host);
            if (!smart.IsDisposed)
            {
                smart.ShowDialog();
                smart.Dispose();
                
            }


        }
        


        public void hddUsage(string host)
        {
            try
            {
                long totalSize = 0;
                long freeSpace = 0;

                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("\\\\" + host + "\\root\\CIMV2",
                                                 "SELECT * FROM Win32_LogicalDisk Where DeviceID = 'C:'");
                try
                {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    freeSpace = Convert.ToInt64(queryObj["FreeSpace"]);
                    totalSize = Convert.ToInt64(queryObj["Size"]);
                }
                }
                catch (Exception e)
                {
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" An error occurred while querying for WMI data: " + e.Message, 2, Color.AliceBlue);
                }

                if (totalSize != 0)
                {
                    totalSize = totalSize/1024/1024;
                    freeSpace = freeSpace/1024/1024;

                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" Total c:\\ Drive Size: " + totalSize + "MB; " + totalSize/1024 + "GB", 2,
                                        Color.AliceBlue);
                    UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" Free space: " + freeSpace + "MB; " + freeSpace/1024 + "GB", 2, Color.AliceBlue);
                }
            }
            catch (ManagementException e)
            {
                UpdateMyrichTextBox(host, 1, TextColor.CurrentColor);
                UpdateMyrichTextBox(" An error occurred while querying for WMI data: " + e.Message, 2, Color.AliceBlue);
            }



        }


        public static Boolean pingQuick(string url_or_IP)
        {
            using (Ping ping = new Ping())
            {

                try
                {

                    PingReply reply = ping.Send(url_or_IP, 100);
                    if (reply.Status == IPStatus.Success)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }




        private void allButtons_Click(object sender, EventArgs e)
        {
            if (hostnameTextbox.Text == "")
            {
                UpdateMyrichTextBox("Error: host or ip string is empty !!!", 2, Color.Red);
            }

            else
            {
                
                DWpanel1.Controls.Clear();
                optionsPanel.Controls.Clear();

                if (hostnameOrIp.recData != hostnameTextbox.Text)
                {
                    save2history();
                    TextColor.NextColor();
                    fillStatusBar();

                }


                Button b = (Button) sender;
                string host2pass = hostnameTextbox.Text;
                switch (b.Name)
                {
                    case "SMARTbtn":

                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching " + host2pass + " S.M.A.R.T Data", 2, Color.AliceBlue);
                            SMARTfunc(host2pass);
                            

                        }


                        break;

                    case "chkdsk_btn":

                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching " + host2pass + " Check Disk", 2, Color.AliceBlue);
                            chkdskFunc(host2pass);


                        }


                        break;

                    case "psInfoBtn":

                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            try
                            {
                                if (Properties.Settings.Default.psToolsPath.ToString() == "")
                                {
                                    throw new Exception("PsTools folder must be set in Settings->Misc Tab");
                                }
                                else
                                {
                                    string psToolsPath = Properties.Settings.Default.psToolsPath.ToString() + @"\PsInfo.exe";
                                    UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                                    UpdateMyrichTextBox(" Lunching \"PsInfo on " + host2pass + "\"", 2, Color.AliceBlue);
                                    string cmdStr = @"c:\Windows\System32\cmd.exe"; // /c ping -t " + hostnameTextbox.Text;
                                    ProcessStartInfo processInfo = new ProcessStartInfo(cmdStr);
                                    processInfo.Arguments = "/K " + psToolsPath + @" \\" + hostnameTextbox.Text;
                                    processInfo.Verb = "runas";
                                    var pcInfoProcess = Process.Start(processInfo);

                                }
                            }
                            catch (Exception psInEx)
                            {

                                UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                                UpdateMyrichTextBox(" Error: " + psInEx.Message, 2, Color.Red);
                            }
                           
                        }
                        break;

                    case "HddUsageButton":

                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            Thread HddUsage = new Thread(() => hddUsage(host2pass));
                            HddUsage.Start();

                        }


                        break;
                        
                    case "buttonPing_t":


                        UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                        UpdateMyrichTextBox(" Lunching \"ping -t " + host2pass + "\"", 2, Color.AliceBlue);
                        string str = @"c:\Windows\System32\cmd.exe"; // /c ping -t " + hostnameTextbox.Text;
                        Process process = new Process();
                        process.StartInfo.FileName = str;
                        process.StartInfo.Arguments = "/c ping -t " + hostnameTextbox.Text;
                        process.Start();

                        break;

                    case "LoggedinButton":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            Thread Loggedin = new Thread(() => whosLoggedIn(host2pass));
                            Loggedin.Start();
                        }

                        break;

                    case "printScrnBtn":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            
                            Thread scrnShot = new Thread(() => scrnShotFunc(host2pass));
                            scrnShot.SetApartmentState(ApartmentState.STA);
                            scrnShot.Start();
                        }

                        break;

                        case "localTaskmgr_btn":
                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching " + host2pass + " local Task Manager", 2, Color.AliceBlue);
                            Thread model = new Thread(() => localTaskmgr(host2pass));
                            model.Start();
                        }

                        break;

                        case "localControl_btn":
                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching " + host2pass + " local Control panel", 2, Color.AliceBlue);
                            Thread model = new Thread(() => localControl(host2pass));
                            model.Start();
                        }

                        break;


                    case "modellButton":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            Thread model = new Thread(() => modelInfo(host2pass));
                            model.Start();
                        }

                        break;

                    case "hardwareInfoButton":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching full hardware information for " + host2pass, 2,
                                                Color.AliceBlue);
                            string HwInfostr =
                                System.IO.Path.GetDirectoryName(
                                    System.Reflection.Assembly.GetExecutingAssembly().Location) +
                                "\\Resources\\hardwareInfo\\index.hta";
                            Process HwInfoprocess = new Process();
                            HwInfoprocess.StartInfo.FileName = HwInfostr;
                            HwInfoprocess.StartInfo.Arguments = " \"" + host2pass + "\"";
                            HwInfoprocess.Start();
                        }

                        break;

                    case "ramButton":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            Thread t = new Thread(() => ramInfo(host2pass));
                            t.Start();
                        }

                        break;


                    case "shutdown_btn":

                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            shutdownForm shutdownForm = new shutdownForm(host2pass);
                            if (shutdownForm.ShowDialog() == DialogResult.OK)
                            {
                                Thread t = new Thread(() => shutdownFunc(host2pass, shutdownForm.ReturnValue1));
                                t.Start();
                                shutdownForm.Dispose();
                            }
                            else
                            {
                                shutdownForm.Dispose();
                            }
                        }

                        break;

                    case "cpuInfoButton":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            Thread t = new Thread(() => cpuInfo(host2pass));
                            t.Start();
                        }

                        break;


                    case "fetchPrintersButton":

                        printersListView.SetObjects(null);
                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            Thread t = new Thread(() => fetchPrinters(host2pass));
                            t.Start();
                        }

                        break;


                    case "prntSrvPro":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            Thread t = new Thread(() => printersSrvPro(host2pass));
                            t.Start();
                        }

                        break;

                    case "clrSpool":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(
                                " Stopping " + hostnameStatusStripLabel.Text + " Print Spooler service...", 2,
                                Color.AliceBlue);



                            try
                            {

                            
                            string clrSpool_str = @"c:\Windows\System32\sc.exe"; // /c ping -t " + hostnameTextbox.Text;
                            Process clrSpool_process = new Process();
                            clrSpool_process.StartInfo.FileName = clrSpool_str;
                            clrSpool_process.StartInfo.Arguments = @"\\" + hostnameTextbox.Text + " stop spooler";
                            clrSpool_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            clrSpool_process.Start();

                            ServiceController spooler = new ServiceController("Print Spooler",
                                                                              hostnameStatusStripLabel.Text);

                            UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" STOPING Spooler service... ", 1,
                                                Color.AliceBlue);
                            TimeSpan t = TimeSpan.FromSeconds(5);
                            spooler.WaitForStatus(ServiceControllerStatus.Stopped, t);



                            if (spooler.Status != ServiceControllerStatus.Stopped)
                            {
                                UpdateMyrichTextBox(" ", 2, TextColor.CurrentColor);
                                UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                                UpdateMyrichTextBox(" Unable to stop spooler service... ABORTING. ", 1,
                                                    Color.Magenta);
                            }
                            else
                            {
                                UpdateMyrichTextBox(" STOPPED !", 2, Color.Red);
                                UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                                UpdateMyrichTextBox(" DELETING all files in Spool Dir... ", 2, Color.AliceBlue);

                                try
                                {
                                    System.IO.DirectoryInfo spoolsDir =
                                        new DirectoryInfo(@"\\" + hostnameStatusStripLabel.Text +
                                                          @"\C$\WINDOWS\system32\spool\PRINTERS");

                                    foreach (FileInfo file in spoolsDir.GetFiles())
                                    {
                                        file.Delete();
                                        UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                                        UpdateMyrichTextBox(" file: " + file.Name + " deleted.", 2, Color.AliceBlue);
                                    }
                                    foreach (DirectoryInfo dir in spoolsDir.GetDirectories())
                                    {
                                        if (dir.Name != "ActMask")
                                        {
                                            dir.Delete(true);
                                            UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                                            UpdateMyrichTextBox(" directory: " + dir.Name + " deleted.", 2,
                                                                Color.AliceBlue);
                                        }
                                    }
                                }
                                catch (Exception clrSpool)
                                {
                                    UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                                    UpdateMyrichTextBox(" Unable to delete all files... ABORTING. ", 2,
                                                        Color.Magenta);
                                    UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                                    UpdateMyrichTextBox(" Reason: " + clrSpool.Message, 2,
                                                        Color.Magenta);

                                }
                                finally
                                {
                                    clrSpool_process.StartInfo.Arguments = @"\\" + hostnameTextbox.Text +
                                                                           " start spooler";
                                    clrSpool_process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                    clrSpool_process.Start();
                                    UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                                    UpdateMyrichTextBox(" STARTING Spooler service... ", 1,
                                                        Color.AliceBlue);
                                    TimeSpan t1 = TimeSpan.FromSeconds(5);
                                    spooler.WaitForStatus(ServiceControllerStatus.Running, t1);
                                    if (spooler.Status == ServiceControllerStatus.Running)
                                    {
                                        UpdateMyrichTextBox(" STARTED !", 2, Color.Lime);

                                    }
                                    else
                                    {
                                        UpdateMyrichTextBox(" ", 2, Color.Lime);
                                        UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                                        UpdateMyrichTextBox(" Unable to start spooler service...!!! ", 2,
                                                            Color.Magenta);
                                    }
                                }
                            }
                            }
                            catch (Exception clrSpoolExp)
                            {

                                UpdateMyrichTextBox(" ", 2, Color.Lime);
                                UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                                UpdateMyrichTextBox(" " + clrSpoolExp.Message, 2,
                                                    Color.Magenta);
                            }
                        }

                        break;

                    case "ping4button":

                        for (int i = 0; i < 4; i++)
                        {
                            Thread ping4 = new Thread(() => pingFunction(host2pass));
                            ping4.Start();

                        }

                        break;




                    case "getNetstat":

                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            try
                            {
                                if (Properties.Settings.Default.psToolsPath.ToString() == "")
                                {
                                    throw new Exception("PsTools folder must be set in Settings->Misc Tab");
                                }
                                else
                                { 
                                    string psToolsPath = Properties.Settings.Default.psToolsPath.ToString() + @"\PsExec.exe";
                                    UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                                    UpdateMyrichTextBox(" Lunching \"netstat on " + host2pass + "\"", 2, Color.AliceBlue);
                                    string cmdStr = @"c:\Windows\System32\cmd.exe"; // /c ping -t " + hostnameTextbox.Text;
                                    ProcessStartInfo processInfo = new ProcessStartInfo(cmdStr);
                                    processInfo.Arguments = "/K " + psToolsPath + @" \\" + hostnameTextbox.Text + " netstat";
                                    processInfo.Verb = "runas";
                                    var netStatProcess = Process.Start(processInfo);
                                   
                                }
                            }
                            catch (Exception netStateEx)
                            {
                                UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                                UpdateMyrichTextBox(" Error: " + netStateEx.Message, 2, Color.Red);
                            }
                           
                            
                        }

                        break;


                    case "longPeriodPing":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            longPeriodPingFunc();
                        }

                        break;

                    case "eventLog":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching " + host2pass + " Event Viewer", 2, Color.AliceBlue);
                            string eventMscStr = @"c:\windows\System32\eventvwr.msc";
                            Process eventMscProcess = new Process();
                            eventMscProcess.StartInfo.FileName = eventMscStr;
                            eventMscProcess.StartInfo.Arguments = "/computer:\\\\" + hostnameTextbox.Text;
                            eventMscProcess.Start();
                        }

                        break;


                    case "services_btn":
                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching " + host2pass + " Services", 2, Color.AliceBlue);
                            string servicesMscStr = @"c:\windows\System32\services.msc";
                            Process servicesMscProcess = new Process();
                            servicesMscProcess.StartInfo.FileName = servicesMscStr;
                            servicesMscProcess.StartInfo.Arguments = "/computer:\\\\" + hostnameTextbox.Text;
                            servicesMscProcess.Start();
                        }
                        break;


                    case "compmgmt_btn":
                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching " + host2pass + " Computer Menagement", 2, Color.AliceBlue);
                            string compMscStr = @"c:\windows\System32\compmgmt.msc";
                            Process compMscProcess = new Process();
                            compMscProcess.StartInfo.FileName = compMscStr;
                            compMscProcess.StartInfo.Arguments = "/computer:\\\\" + hostnameTextbox.Text;
                            compMscProcess.Start();
                        }
                        break;


                    case "taskmgr_btn":
                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching " + host2pass + " Taskmgr", 2, Color.AliceBlue);
                            try
                            {
                                  taskmgrForm taskMGr = new taskmgrForm(host2pass);
                                  taskMGr.ShowDialog();
                            }
                            catch (Exception exp)
                            {

                                UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                                UpdateMyrichTextBox(" Error: " + exp.Message, 2, Color.Red);
                            }
                            
                        }
                        break;


                    case "remoteC":

                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching " + host2pass + " c:\\", 2, Color.AliceBlue);
                            string remoteCStr = @"c:\windows\explorer.exe";
                            Process remoteCProcess = new Process();
                            remoteCProcess.StartInfo.FileName = remoteCStr;
                            remoteCProcess.StartInfo.Arguments = "\\\\" + hostnameTextbox.Text + "\\c$";
                            remoteCProcess.Start();
                        }
                        break;

                    case "ra_btn":
                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching " + host2pass + " Remote Assistance", 2, Color.AliceBlue);
                            string remoteCStr = @"c:\windows\System32\msra.exe";
                            Process remoteCProcess = new Process();
                            remoteCProcess.StartInfo.FileName = remoteCStr;
                            remoteCProcess.StartInfo.Arguments = "/offerra " + hostnameTextbox.Text;
                            remoteCProcess.Start();
                        }
                        break;


                    case "PcAnywhereButton":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching PcAnywhere", 2, Color.AliceBlue);
                            if ((Directory.Exists("C:\\Program Files\\Symantec\\pcAnywhere")) != true)
                            {
                                UpdateMyrichTextBox("Error lunching PcAnywhere: installation dir not found", 2,
                                                    Color.Red);
                            }
                            else
                            {
                                if ((File.Exists("C:\\Program Files\\Symantec\\pcAnywhere\\AWREM32.EXE")) != true)
                                {
                                    UpdateMyrichTextBox("Error lunching PcAnywhere: \"AWREM32.EXE\" file not found", 2,
                                                        Color.Red);
                                }
                                else
                                {

                                    string pcaStr = "C:\\Program Files\\Symantec\\pcAnywhere\\AWREM32.EXE";
                                    Process pcaProcess = new Process();
                                    pcaProcess.StartInfo.FileName = pcaStr;
                                    pcaProcess.StartInfo.Arguments = "\"" +
                                                                     System.IO.Path.GetDirectoryName(
                                                                         System.Reflection.Assembly.GetExecutingAssembly
                                                                             ().Location) + "\\Resources\\Net.chf \" /C" +
                                                                     hostnameTextbox.Text;
                                    pcaProcess.Start();
                                }
                            }
                        }

                        break;


                    case "SmsButton":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching SMS", 2, Color.AliceBlue);
                            if (
                                (Directory.Exists(
                                    System.IO.Path.GetDirectoryName(
                                        System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Resources\\SMS")) !=
                                true)
                            {
                                UpdateMyrichTextBox("Error lunching SMS: SMS dir not found", 2, Color.Red);
                            }
                            else
                            {
                                if (
                                    (File.Exists(
                                        System.IO.Path.GetDirectoryName(
                                            System.Reflection.Assembly.GetExecutingAssembly().Location) +
                                        "\\Resources\\SMS\\rc.exe")) != true)
                                {
                                    UpdateMyrichTextBox("Error lunching SMS: \"rc.exe\" file not found", 2, Color.Red);
                                }
                                else
                                {

                                    string pcaStr =
                                        System.IO.Path.GetDirectoryName(
                                            System.Reflection.Assembly.GetExecutingAssembly().Location) +
                                        "\\Resources\\SMS\\rc.exe";
                                    Process pcaProcess = new Process();
                                    pcaProcess.StartInfo.FileName = pcaStr;
                                    pcaProcess.StartInfo.Arguments = " 1 " + hostnameTextbox.Text;
                                    pcaProcess.Start();
                                }
                            }
                        }

                        break;


                    case "RdpButton":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching RDP", 2, Color.AliceBlue);

                            string pcaStr = "mstsc";
                            Process pcaProcess = new Process();
                            pcaProcess.StartInfo.FileName = pcaStr;
                            pcaProcess.StartInfo.Arguments = " /v " + hostnameTextbox.Text;
                            pcaProcess.Start();


                        }

                        break;




                    case "DamewareButton":


                        if (!pingQuick(hostnameTextbox.Text))
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                        }
                        else
                        {
                            UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Lunching DameWare", 2, Color.AliceBlue);
                            if (
                                (Directory.Exists(
                                    System.IO.Path.GetDirectoryName(
                                        System.Reflection.Assembly.GetExecutingAssembly().Location) +
                                    "\\Resources\\DameWare")) != true)
                            {
                                UpdateMyrichTextBox("Error lunching DameWare: DameWare dir not found", 2, Color.Red);
                            }
                            else
                            {
                                if (
                                    (File.Exists(
                                        System.IO.Path.GetDirectoryName(
                                            System.Reflection.Assembly.GetExecutingAssembly().Location) +
                                        "\\Resources\\DameWare\\DWRCC.EXE")) != true)
                                {
                                    UpdateMyrichTextBox("Error lunching DameWare: \"DWRCC.exe\" file not found", 2,
                                                        Color.Red);
                                }
                                else
                                {
                                    DamewareFunc();

                                }
                            }
                        }

                        break;



                    default:
                        break;
                }
            }
        }

        private void chkdskFunc(string host2pass)
        {
            chkdskForm chkdsk = new chkdskForm(host2pass);
            switch (chkdsk.ShowDialog())
            {
                case DialogResult.OK:
                    UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" chkdsk schedual on next reboot successfully", 2, Color.AliceBlue);
                    break;
                case DialogResult.Yes:
                    UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" chkdsk schedual Failed - Unknown File System", 2, Color.Red);
                    break;
                case DialogResult.No:
                    UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" chkdsk schedual Failed - Unknown error !", 2, Color.Red);
                    break;
                case DialogResult.Abort:
                    UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" chkdsk schedual aborted by user", 2, Color.Crimson);
                    break;
                default:
                    UpdateMyrichTextBox(host2pass, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" chkdsk schedual Failed - Unknown error !", 2, Color.Red);
                    break;


                    
            }
            
        }

        private void localControl(string host2pass)
        {
            try
            {
                if (Properties.Settings.Default.psToolsPath.ToString() == "")
                {
                    throw new Exception("PsTools folder must be set in Settings->Misc Tab");
                }
                else
                {
                    string lConStr = Properties.Settings.Default.psToolsPath.ToString() + @"\PsExec.exe";
                    Process lConProcess = new Process();
                    lConProcess.StartInfo.FileName = lConStr;
                    lConProcess.StartInfo.Arguments = @"\\" + host2pass + " -i -s -d control";
                    lConProcess.StartInfo.CreateNoWindow = true;
                    lConProcess.StartInfo.UseShellExecute = false;
                    lConProcess.Start();
                }
            }
            catch (Exception lConEx)
            {

                UpdateMyrichTextBox(" " + lConEx.Message, 2, Color.Red);
            }

        }

        private void localTaskmgr(string host2pass)
        {
        try
        {
            if (Properties.Settings.Default.psToolsPath.ToString() == "")
            {
                throw new Exception("PsTools folder must be set in Settings->Misc Tab");
            }
            else
            {
                string lTaskStr = Properties.Settings.Default.psToolsPath.ToString() + @"\PsExec.exe";
                Process lTaskProcess = new Process();
                lTaskProcess.StartInfo.FileName = lTaskStr;
                lTaskProcess.StartInfo.Arguments = @"\\" + host2pass + " -i -s -d taskmgr";
                lTaskProcess.StartInfo.CreateNoWindow = true;
                lTaskProcess.StartInfo.UseShellExecute = false;
                lTaskProcess.Start();
            }
        }
            catch (Exception lTaskEx)
            {

                UpdateMyrichTextBox(" " + lTaskEx.Message, 2, Color.Red);
            }

        }



        private void DamewareFunc()
        {
            DWpanel1.Controls.Clear();
            //RadioButton adminKehila = new RadioButton();
            //adminKehila.Checked = true;
            //adminKehila.Text = "Use default:\r\nUser: Administrator\r\nPass: Kehil@2008";
            //adminKehila.Left = 0;
            //adminKehila.Top = 10;
            //System.Drawing.Size size = new System.Drawing.Size(120, 50);
            //adminKehila.Size = size;
            //adminKehila.AutoSize = false;
            //DWpanel1.Controls.Add(adminKehila);


            //RadioButton otherCred = new RadioButton();
            //otherCred.Name = "otherCred";
            //otherCred.Checked = false;
            //otherCred.Text = "Specify\r\ndifferent\r\ncredentials:";
            //otherCred.Top = 10;
            //otherCred.Left = 155;
            //otherCred.AutoSize = true;
            //otherCred.CheckedChanged += new EventHandler(otherCred_CheckedChanged);
            //DWpanel1.Controls.Add(otherCred);

            Label dwCredLabel = new Label();
            dwCredLabel.Text = "Enter the following fields manualy: \r\nOR, use a preset credentials:";
            dwCredLabel.Top = 5;
            dwCredLabel.Left = 0;
            dwCredLabel.Size = new Size(170, 30);
            DWpanel1.Controls.Add(dwCredLabel);

            ComboBox dwCredCbox = new ComboBox();
            dwCredCbox.Name = "dwCredCbox";
            dwCredCbox.Top = 40;
            dwCredCbox.Left = 2;
            dwCredCbox.DropDownStyle = ComboBoxStyle.DropDownList;
            dwCredCbox.Items.Add("");
            dwCredCbox.Size = new Size(140,21);
            String creds = settingsForm.ToInsecureString(settingsForm.DecryptString(Properties.Settings.Default.dwCred));
            if (!String.IsNullOrEmpty(creds))
            {
                string[] credsArr = creds.Split('|');
                int i = 0;
                foreach (string cred in credsArr)
                {
                    if (!string.IsNullOrEmpty(cred))
                    {
                        dwCredCbox.Items.Add(cred.Split(';')[0]);
                    }
                    i++;
                }
            }
            dwCredCbox.SelectedIndexChanged += new EventHandler(dwCredCbox_SelectedIndexChanged);
            
            DWpanel1.Controls.Add(dwCredCbox);

            TextBox dwusrname = new TextBox();
            dwusrname.Top = 5;
            dwusrname.Left = 180;
            dwusrname.Text = "UserName";
            dwusrname.Name = "userName";
            dwusrname.Size = new Size(130,15);
            dwusrname.ForeColor = Color.Gray;
            //dwusrname.Enabled = false;

            dwusrname.Enter += new EventHandler(dwusrname_Enter);

            DWpanel1.Controls.Add(dwusrname);

            TextBox dwpass = new TextBox();
            dwpass.Top = 25;
            dwpass.Left = 180;
            dwpass.Size = new Size(130, 15);
            dwpass.Text = "Password";
            dwpass.Name = "password";
            dwpass.ForeColor = Color.Gray;
            //dwpass.Enabled = false;


            dwpass.Enter += new EventHandler(dwpass_Enter);

            DWpanel1.Controls.Add(dwpass);




            TextBox dwdom = new TextBox();
            dwdom.Top = 45;
            dwdom.Left = 180;
            dwdom.Text = "Domain";
            dwdom.Name = "domain";
            dwdom.Size = new Size(130, 15);
            dwdom.ForeColor = Color.Gray;
            //dwdom.Enabled = false;

            dwdom.Enter += new EventHandler(dwdom_Enter);

            DWpanel1.Controls.Add(dwdom);


            Button dwGoBtn = new Button();
            dwGoBtn.Top = 10;
            dwGoBtn.Left = 370;
            dwGoBtn.Text = "Go!";
            dwGoBtn.Size = new System.Drawing.Size(60, 50);
            dwGoBtn.Click += new EventHandler(dwGoBtn_Click);
            DWpanel1.Controls.Add(dwGoBtn);



            DWpanel1.Visible = true;



        }

        void dwCredCbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dwPresetCred = (ComboBox)DWpanel1.Controls["dwCredCbox"];
            var dwusrNameTxt = (TextBox)DWpanel1.Controls["userName"];
            var dwpassTxt = (TextBox)DWpanel1.Controls["password"];
            dwpassTxt.PasswordChar = '*';
            var dwdomTxt = (TextBox)DWpanel1.Controls["domain"];
            if (dwPresetCred.SelectedIndex > 0)
            {
                String creds = settingsForm.ToInsecureString(settingsForm.DecryptString(Properties.Settings.Default.dwCred));
                if (!String.IsNullOrEmpty(creds))
                {
                    string[] credsArr = creds.Split('|');
                    string[] credSingleArr = credsArr[dwPresetCred.SelectedIndex - 1].Split(';');

                    dwusrNameTxt.Text = credSingleArr[1];
                    dwpassTxt.Text = credSingleArr[2];
                    dwdomTxt.Text = credSingleArr[3];
                }

            }
            else
            {
                dwusrNameTxt.Text = "";
                dwpassTxt.Text = "";
                dwdomTxt.Text = "";
            }
        }

        

        private void dwGoBtn_Click(object sender, EventArgs e)
        {
            
            
                 var dwusrNameTxt = (TextBox) DWpanel1.Controls["userName"];
                 var dwpassTxt = (TextBox) DWpanel1.Controls["password"];
                dwpassTxt.PasswordChar = '*';
                 var dwdomTxt = (TextBox) DWpanel1.Controls["domain"];
                 

                 




            string pcaStr =
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
                    "\\Resources\\DameWare\\DWRCC.EXE";
                Process pcaProcess = new Process();
                pcaProcess.StartInfo.FileName = pcaStr;
                pcaProcess.StartInfo.Arguments = " -c: -h: -m:" + hostnameTextbox.Text + " -u:" + dwusrNameTxt.Text +
                                                 " -p:" + dwpassTxt.Text + " -d:" + dwdomTxt.Text;
                pcaProcess.Start();

           
        }

        private void otherCred_CheckedChanged(object sender, EventArgs e)
        {
            var otherCred = (RadioButton) DWpanel1.Controls["otherCred"];
            var dwusrNameTxt = (TextBox) DWpanel1.Controls["userName"];
            var dwpassTxt = (TextBox) DWpanel1.Controls["password"];
            var dwdomTxt = (TextBox) DWpanel1.Controls["domain"];

            if (otherCred.Checked)
            {

                dwusrNameTxt.Enabled = true;
                dwpassTxt.Enabled = true;
                dwdomTxt.Enabled = true;
            }
            else
            {
                dwusrNameTxt.Enabled = false;
                dwpassTxt.Enabled = false;
                dwdomTxt.Enabled = false;
            }
        }

        private void dwdom_Enter(object sender, EventArgs e)
        {
            var dwdomTxt = (TextBox) DWpanel1.Controls["domain"];
            dwdomTxt.ForeColor = Color.Black;
            dwdomTxt.Text = "";
            var dwPresetCred = (ComboBox)DWpanel1.Controls["dwCredCbox"];
            dwPresetCred.SelectedIndex = 0;
        }

        private void dwpass_Enter(object sender, EventArgs e)
        {
            var dwpassTxt = (TextBox) DWpanel1.Controls["password"];
            dwpassTxt.ForeColor = Color.Black;
            dwpassTxt.PasswordChar = '*';
            dwpassTxt.Text = "";
            var dwPresetCred = (ComboBox)DWpanel1.Controls["dwCredCbox"];
            dwPresetCred.SelectedIndex = 0;
        }


        private void dwusrname_Enter(object sender, EventArgs e)
        {
            var dwusrNameTxt = (TextBox) DWpanel1.Controls["userName"];
            dwusrNameTxt.ForeColor = Color.Black;
            dwusrNameTxt.Text = "";
            var dwPresetCred = (ComboBox)DWpanel1.Controls["dwCredCbox"];
            dwPresetCred.SelectedIndex = 0;
        }

        private void longPeriodPingFunc()
        {
            optionsPanel.Controls.Clear();
            Label longPeriodLabel = new Label();
            longPeriodLabel.Left = 5;
            longPeriodLabel.Top = 8;
            longPeriodLabel.AutoSize = true;
            longPeriodLabel.Text = "Ping '" + hostnameStatusStripLabel.Text + "' for: ";
            longPeriodLabel.Visible = true;
            optionsPanel.Controls.Add(longPeriodLabel);

            ComboBox longPeriodTimeCombo = new ComboBox();
            longPeriodTimeCombo.Left = 153;
            longPeriodTimeCombo.Top = 5;
            longPeriodTimeCombo.Width = 60;
            longPeriodTimeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            longPeriodTimeCombo.Name = "longPeriodTimeCombo";

            Dictionary<string, string> test = new Dictionary<string, string>();
            test.Add("1", "1 min");
            test.Add("5", "5 min");
            test.Add("10", "10 min");
            test.Add("15", "15 min");
            test.Add("30", "30 min");
            test.Add("60", "1 houre");
            longPeriodTimeCombo.DataSource = new BindingSource(test, null);
            longPeriodTimeCombo.DisplayMember = "Value";
            longPeriodTimeCombo.ValueMember = "Key";
            longPeriodTimeCombo.Visible = true;
            longPeriodTimeCombo.SelectedValue = "5 min";
            optionsPanel.Controls.Add(longPeriodTimeCombo);


            Button longPeriodGoButton = new Button();
            longPeriodGoButton.Left = 308;
            longPeriodGoButton.Top = 4;
            longPeriodGoButton.Visible = true;
            longPeriodGoButton.Text = "Go !";
            optionsPanel.Controls.Add(longPeriodGoButton);


            Button longPeriodCancelButton = new Button();
            longPeriodCancelButton.Left = 383;
            longPeriodCancelButton.Top = 4;
            longPeriodCancelButton.Visible = true;
            longPeriodCancelButton.Text = "Cancel";
            optionsPanel.Controls.Add(longPeriodCancelButton);

            CheckBox alertWithPopup = new CheckBox();
            alertWithPopup.Left = 5;
            alertWithPopup.Top = 31;
            alertWithPopup.Checked = true;
            alertWithPopup.Visible = true;
            alertWithPopup.AutoSize = true;
            alertWithPopup.Name = "alertWithPopup";
            alertWithPopup.Text = "Alert me with pop-up when job is finished.";
            optionsPanel.Controls.Add(alertWithPopup);

            CheckBox logLocationDefaultCheckbox = new CheckBox();
            logLocationDefaultCheckbox.Left = 5;
            logLocationDefaultCheckbox.Top = 50;
            logLocationDefaultCheckbox.Visible = true;
            logLocationDefaultCheckbox.AutoSize = true;
            logLocationDefaultCheckbox.Name = "logLocationDefaultCheckbox";
            logLocationDefaultCheckbox.Text = "Dont use dafault location for log file:";
            optionsPanel.Controls.Add(logLocationDefaultCheckbox);

            logLocationDefaultCheckbox.CheckStateChanged +=
                new EventHandler(logLocationDefaultCheckbox_CheckStateChanged);


            TextBox logLocationTextPath = new TextBox();
            logLocationTextPath.Left =205;
            logLocationTextPath.Top = 48;
            logLocationTextPath.Size = new Size(200,21);
            logLocationTextPath.Visible = true;
            logLocationTextPath.AutoSize = false;
            logLocationTextPath.ReadOnly = true;
            logLocationTextPath.Name = "logLocationTextPath";
            logLocationTextPath.Text = Path.GetDirectoryName(Application.ExecutablePath) + "\\" +
                                       hostnameStatusStripLabel.Text + ".log";
            optionsPanel.Controls.Add(logLocationTextPath);





            Button logBrowse = new Button();
            logBrowse.Left = 410;
            logBrowse.Top = 47;
            logBrowse.Width = 50;
            logBrowse.Visible = true;
            logBrowse.Enabled = false;
            logBrowse.Name = "logBrowse";
            logBrowse.Text = "Browse";
            optionsPanel.Controls.Add(logBrowse);

            optionsPanel.Visible = true;

            logBrowse.Click += new EventHandler(logBrowse_Click);

            longPeriodGoButton.Click += new EventHandler(longPeriodGoButton_Click);

        }

        private void logBrowse_Click(object sender, EventArgs e)
        {
            var logLocationTextPath = (TextBox) optionsPanel.Controls["logLocationTextPath"];
            logFileSaveDialog.FileName = Path.GetDirectoryName(Application.ExecutablePath) + "\\" +
                                         hostnameStatusStripLabel.Text + ".log";
            logFileSaveDialog.ShowDialog();

            if (logFileSaveDialog.FileName != "")
            {
                logLocationTextPath.Text = logFileSaveDialog.FileName;
            }
        }

        private void logLocationDefaultCheckbox_CheckStateChanged(object sender, EventArgs e)
        {

            var logLocationDefaultCheckbox = (CheckBox) optionsPanel.Controls["logLocationDefaultCheckbox"];
            var logLocationTextPath = (TextBox) optionsPanel.Controls["logLocationTextPath"];
            var logBrowse = (Button) optionsPanel.Controls["logBrowse"];

            if (logLocationDefaultCheckbox.Checked)
            {
                logLocationTextPath.ReadOnly = false;

                logBrowse.Enabled = true;

                logFileSaveDialog.FileName = hostnameStatusStripLabel.Text;
                logFileSaveDialog.ShowDialog();
                if (logFileSaveDialog.FileName != "")
                {
                    logLocationTextPath.Text = logFileSaveDialog.FileName;
                }
                else
                {
                    logLocationTextPath.Text = Path.GetDirectoryName(Application.ExecutablePath) + "\\" +
                                               hostnameStatusStripLabel.Text + ".log";
                }

            }


            else
            {

                logLocationTextPath.ReadOnly = true;

                logBrowse.Enabled = false;
                logLocationTextPath.Text = Path.GetDirectoryName(Application.ExecutablePath) + "\\" +
                                           hostnameStatusStripLabel.Text + ".log";
            }
        }



        private void tracertBtn_Click(object sender, EventArgs e)
        {
            if (hostnameTextbox.Text == "")
            {
                UpdateMyrichTextBox("Error: host or ip string is empty !!!", 2, Color.Red);
            }

            else
            {
                

                if (hostnameOrIp.recData != hostnameTextbox.Text)
                {
                    save2history();
                    TextColor.NextColor();
                    fillStatusBar();
                }

                optionsPanel.Controls.Clear();

                Tracert.MainForm trcrt = new Tracert.MainForm(hostnameTextbox.Text);
                trcrt.ShowDialog();

             
                optionsPanel.Visible = true;



            }
        }



        public void determineServerName(string host, RadioButton serverName)
        {
            try
            {
                string serverHostname = host.Remove(0, 4);
                serverHostname = serverHostname.Remove(4, serverHostname.Length - 4);

                serverName.Text = "hfnt" + serverHostname;
            }
            catch (Exception)
            {
                serverName.Text = "Unable to determine.";
                serverName.Enabled = false;
            }

        }


        public void determineServerIP(string ipAddress, RadioButton serverIP)
        {
            try
            {
                string ip = ipAddress.Remove(0, 4);
                decimal twoLastAddress = Convert.ToDecimal(ip);
                decimal segment = Math.Truncate(twoLastAddress);



                serverIP.Text = "1.5." + segment.ToString() + ".1";
            }
            catch (Exception)
            {

                serverIP.Text = "Unable to determine.";
                serverIP.Enabled = false;
            }

        }



     

        private void mainHostnameChanged(object sender, EventArgs e)
        {
            hostnameStatusStripLabel.Text = null;
            ipaddressStatusStripLabel.Text = null;
            printersListView.SetObjects(null);
            prntLabel.Visible = false;
            DWpanel1.Controls.Clear();
            optionsPanel.Controls.Clear();
        }






        private void longPeriodGoButton_Click(object sender, EventArgs e)
        {
            CheckBox alertWithPopup = (CheckBox) optionsPanel.Controls["alertWithPopup"];
            bool popup = alertWithPopup.Checked;
            ComboBox longPeriodCombo = (ComboBox) optionsPanel.Controls["longPeriodTimeCombo"];
            int minutes = Convert.ToInt16(((KeyValuePair<string, string>) longPeriodCombo.SelectedItem).Key);
            string hostname = hostnameStatusStripLabel.Text;
            TextBox logLocationTextPath = (TextBox) optionsPanel.Controls["logLocationTextPath"];
            string logLocation = logLocationTextPath.Text;



            var bw = new BackgroundWorker();




            // define the event handlers 
            bw.DoWork += (senderA, args) => { longPeriodFunc(hostname, minutes, logLocation); };
            bw.RunWorkerCompleted += (senderA, args) =>
                                         {
                                             if (args.Error != null) // if an exception occurred during DoWork, 
                                                 MessageBox.Show(hostname + " Error: " + args.Error.ToString());
                                                     // do your error handling here 

                                             // Do whatever else you want to do after the work completed. 
                                             // This happens in the main UI thread. +
                                             if (popup)
                                             {
                                                 MessageBox.Show("pinging " + hostname + " for " + minutes.ToString() +
                                                                 " minutes has finished.");
                                             }

                                             if (longPeriodPingClass.hasCanceled == false)
                                             {
                                                 UpdateMyrichTextBox("pinging ", 1, Color.AliceBlue);
                                                 UpdateMyrichTextBox(hostname, 1, Color.OrangeRed);
                                                 UpdateMyrichTextBox(
                                                     " for " + minutes.ToString() + " minutes has finished.", 2,
                                                     Color.AliceBlue);

                                                 UpdateMyrichTextBox(
                                                     "View the results by clicking here: file:///" +
                                                     logLocationTextPath.Text, 2, Color.Blue);
                                                 myrichTextBox.LinkClicked +=
                                                     new LinkClickedEventHandler(myrichTextBox_LinkClicked);

                                             }
                                             else
                                             {
                                                 UpdateMyrichTextBox("pinging ", 1, Color.AliceBlue);
                                                 UpdateMyrichTextBox(hostname, 1, Color.OrangeRed);
                                                 UpdateMyrichTextBox(
                                                     " for " + minutes.ToString() + " minutes has been CANCELED.", 2,
                                                     Color.AliceBlue);
                                             }




                                         };

            bw.RunWorkerAsync(); // starts the background worker 


        }

        public System.Diagnostics.Process showLog = new System.Diagnostics.Process();


        private void myrichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            showLog = System.Diagnostics.Process.Start("Notepad.exe", e.LinkText.Remove(0, 8));

        }


        public void longPeriodFunc(string hostname, int minutes, string logLocation)
        {


            UpdateMyrichTextBox(hostname, 1, TextColor.CurrentColor);
            UpdateMyrichTextBox(" Starting long ping for " + minutes.ToString() + " minutes.", 2, Color.AliceBlue);
            longPeriodPingClass longPeriodObj = new longPeriodPingClass(hostname, minutes, logLocation);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm settings = new settingsForm();
            settings.ShowDialog();
            if(settings.clrHistory)
            {
                hostnameTextbox.Items.Clear();
            }
            loadQuickLinks();
            loadUserShotcuts();
           

        }

       

        private void portalBtn_Click(object sender, EventArgs e)
        {
            try
        {
            UpdateMyrichTextBox(" Lunching portal", 2, Color.AliceBlue);
            string pcaStr = "iexplore";
            Process pcaProcess = new Process();
            pcaProcess.StartInfo.FileName = pcaStr;
            pcaProcess.StartInfo.Arguments = " " + Properties.Settings.Default.WebPortal;
            pcaProcess.Start();
        }
            catch (Exception portalEx)
            {

                UpdateMyrichTextBox(" " + portalEx.Message, 2, Color.Red);
            }

        }


        public RegistryKey devicesKey { get; set; }

        public bool NoLoggedUser { get; set; }

       

  
    

    private void Form1_Load(object sender, EventArgs e)
        {
            string strLongTermMem = "";
            strLongTermMem = Properties.Settings.Default.history;

            foreach (string pc in strLongTermMem.Split('|'))
            {
                if (!string.IsNullOrEmpty(pc))
                    hostnameTextbox.Items.Add(pc);
            }

        loadQuickLinks();
        loadUserShotcuts();

        }

    private void loadUserShotcuts()
    {
        //btn1
        if(!string.IsNullOrEmpty(Properties.Settings.Default.userSC1))
        {
            userBtn1.Text = Properties.Settings.Default.userSC1.Split('|')[2];
            userBtn1.Enabled = true;
           // userBtn1.Click += new EventHandler(userBtn1_Click);
            
        }
        else
        {
            userBtn1.Enabled = false;
            userBtn1.Text = "User\r\nShortcut 1";
        }
        //btn2
        if (!string.IsNullOrEmpty(Properties.Settings.Default.userSC2))
        {
            userBtn2.Text = Properties.Settings.Default.userSC2.Split('|')[2];
            userBtn2.Enabled = true;
          //  userBtn2.Click += new EventHandler(userBtn1_Click);
        }
        else
        {
            userBtn2.Enabled = false;
            userBtn2.Text = "User\r\nShortcut 2";
        }
        //btn3
        if (!string.IsNullOrEmpty(Properties.Settings.Default.userSC3))
        {
            userBtn3.Text = Properties.Settings.Default.userSC3.Split('|')[2];
            userBtn3.Enabled = true;
           // userBtn3.Click += new EventHandler(userBtn1_Click);
        }
        else
        {
            userBtn3.Enabled = false;
            userBtn3.Text = "User\r\nShortcut 3";
        }
        //btn4
        if (!string.IsNullOrEmpty(Properties.Settings.Default.userSC4))
        {
            userBtn4.Text = Properties.Settings.Default.userSC4.Split('|')[2];
            userBtn4.Enabled = true;
           // userBtn4.Click += new EventHandler(userBtn1_Click);
        }
        else
        {
            userBtn4.Enabled = false;
            userBtn4.Text = "User\r\nShortcut 4";
        }
        //btn5
        if (!string.IsNullOrEmpty(Properties.Settings.Default.userSC5))
        {
            userBtn5.Text = Properties.Settings.Default.userSC5.Split('|')[2];
            userBtn5.Enabled = true;
          //  userBtn5.Click += new EventHandler(userBtn1_Click);

        }
        else
        {
            userBtn5.Enabled = false;
            userBtn5.Text = "User\r\nShortcut 5";
        }
       
    }

    void userBtn1_Click(object sender, EventArgs e)
    {
        Button pressedBtn = (Button) sender;
        int index = Convert.ToInt16(pressedBtn.Name[7].ToString());

        switch (index)
        {
            case 1:
                Process usr1 = new Process();
                usr1.StartInfo.FileName = Properties.Settings.Default.userSC1.Split('|')[1];
                usr1.StartInfo.Arguments = Properties.Settings.Default.userSC1.Split('|')[3];
                try
                {
                    usr1.Start();
                    UpdateMyrichTextBox(" Lunching " + Properties.Settings.Default.userSC1.Split('|')[2], 2, Color.AliceBlue);
                }
                catch (Exception ex)
                {

                    UpdateMyrichTextBox(" " + ex.Message, 2, Color.Red);
                }
                break;
            case 2:
                 Process usr2 = new Process();
                usr2.StartInfo.FileName = Properties.Settings.Default.userSC2.Split('|')[1];
                usr2.StartInfo.Arguments = Properties.Settings.Default.userSC2.Split('|')[3];
                try
                {
                    usr2.Start();
                    UpdateMyrichTextBox(" Lunching " + Properties.Settings.Default.userSC2.Split('|')[2], 2, Color.AliceBlue);
                }
                catch (Exception ex)
                {

                    UpdateMyrichTextBox(" " + ex.Message, 2, Color.Red);
                }
                break;
            case 3:
                 Process usr3 = new Process();
                usr3.StartInfo.FileName = Properties.Settings.Default.userSC3.Split('|')[1];
                usr3.StartInfo.Arguments = Properties.Settings.Default.userSC3.Split('|')[3];
                try
                {
                    usr3.Start();
                    UpdateMyrichTextBox(" Lunching " + Properties.Settings.Default.userSC3.Split('|')[2], 2, Color.AliceBlue);
                }
                catch (Exception ex)
                {

                    UpdateMyrichTextBox(" " + ex.Message, 2, Color.Red);
                }
                break;
            case 4:
                 Process usr4 = new Process();
                usr4.StartInfo.FileName = Properties.Settings.Default.userSC4.Split('|')[1];
                usr4.StartInfo.Arguments = Properties.Settings.Default.userSC4.Split('|')[3];
                try
                {
                    usr4.Start();
                    UpdateMyrichTextBox(" Lunching " + Properties.Settings.Default.userSC4.Split('|')[2], 2, Color.AliceBlue);
                }
                catch (Exception ex)
                {

                    UpdateMyrichTextBox(" " + ex.Message, 2, Color.Red);
                }
                break;
            case 5:
                 Process usr5 = new Process();
                usr5.StartInfo.FileName = Properties.Settings.Default.userSC5.Split('|')[1];
                usr5.StartInfo.Arguments = Properties.Settings.Default.userSC5.Split('|')[3];
                try
                {
                    usr5.Start();
                    UpdateMyrichTextBox(" Lunching " + Properties.Settings.Default.userSC5.Split('|')[2], 2, Color.AliceBlue);
                }
                catch (Exception ex)
                {

                    UpdateMyrichTextBox(" " + ex.Message, 2, Color.Red);
                }
                break;
            default:
                break;
                

        }

        
        
        
       
    }

    public void loadQuickLinks()
    {
        quickLink1.Text = Properties.Settings.Default.QuickLinks1;
        quickLink2.Text = Properties.Settings.Default.QuickLinks2;
        quickLink3.Text = Properties.Settings.Default.QuickLinks3;
        quickLink4.Text = Properties.Settings.Default.QuickLinks4;
        quickLink5.Text = Properties.Settings.Default.QuickLinks5;
        quickLink6.Text = Properties.Settings.Default.QuickLinks6;

    }


    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        Properties.Settings.Default.history = "";
        foreach (var pc in hostnameTextbox.Items)
        {
            Properties.Settings.Default.history += pc.ToString() + "|";

        }
        Properties.Settings.Default.Save();
    }


    private void AD_btn_Click(object sender, EventArgs e)
    {
        try
        {
        UpdateMyrichTextBox(" Lunching Active Directory", 2,Color.AliceBlue);
        string AdStr = Properties.Settings.Default.ADPath;
        Process AdProcess = new Process();
        AdProcess.StartInfo.FileName = AdStr;
        AdProcess.Start();  
        }
        catch (Exception adEx)
        {

            UpdateMyrichTextBox(" " + adEx.Message, 2, Color.Red);
        }
        
    }

    private void CRM_btn_Click(object sender, EventArgs e)
    {
         try
        {
        UpdateMyrichTextBox(" Lunching CRM", 2, Color.AliceBlue);
        string CRMStr = "iexplore";
        Process CRMProcess = new Process();
        CRMProcess.StartInfo.FileName = CRMStr;
        CRMProcess.StartInfo.Arguments = " " + Properties.Settings.Default.CRMPath;
        CRMProcess.StartInfo.LoadUserProfile = true;
        CRMProcess.Start();
        }
         catch (Exception CrmEx)
         {

             UpdateMyrichTextBox(" " + CrmEx.Message, 2, Color.Red);
         }
    }

    private void kb_btn_Click(object sender, EventArgs e)
    {
        try
        {
        UpdateMyrichTextBox(" Lunching Knowledge Base", 2, Color.AliceBlue);
        string seferStr = "iexplore";
        Process seferProcess = new Process();
        seferProcess.StartInfo.FileName = seferStr;
        seferProcess.StartInfo.Arguments = " " + Properties.Settings.Default.kbPath;
        seferProcess.Start();
        }
        catch (Exception kbEx)
        {

            UpdateMyrichTextBox(" " + kbEx.Message, 2, Color.Red);
        }
    }



    private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (mainPanel.SelectedTab.Text == "Printers" && !string.IsNullOrEmpty(hostnameTextbox.Text))
            {
                if (hostnameOrIp.recData != hostnameTextbox.Text)
                {
                    save2history();
                    TextColor.NextColor();
                    fillStatusBar();

                }
                printersListView.SetObjects(null);
                if (!pingQuick(hostnameTextbox.Text))
                {
                    UpdateMyrichTextBox(hostnameTextbox.Text, 1, TextColor.CurrentColor);
                    UpdateMyrichTextBox(" Error: No ping to host or ip !!!", 2, Color.Red);
                }
                else
                {
                    string host2pass = hostnameTextbox.Text;
                    Thread t = new Thread(() => fetchPrinters(host2pass));
                    t.Start();
                }
            }
            if (mainPanel.SelectedTab.Text == "Files")
            {
                copyControls();
            }
            else
            {
                filesPanel.Controls.Clear();
            }
            DWpanel1.Controls.Clear();
            optionsPanel.Controls.Clear();
            
        }
        
        catch (Exception tabEx)
        {

            UpdateMyrichTextBox(hostnameTextbox.Text, 1, TextColor.CurrentColor);
            UpdateMyrichTextBox(" " + tabEx.Message, 2, Color.Red);
        }

    }

    private void quickLink1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        LinkLabel linkPressed = (LinkLabel)sender;
        hostnameTextbox.Text = linkPressed.Text;
        hostnameTextbox.Focus();
        hostnameTextbox.Select(hostnameTextbox.Text.Length, 0);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        favoritesForm fav = new favoritesForm(hostnameTextbox.Text);
        fav.ShowDialog();
        if (fav.doubleClicked)
            hostnameTextbox.Text = fav.selected;
        fav.Dispose();


    }

    private void prntScreenBtn_Click(object sender, EventArgs e)
    {
        this.WindowState = FormWindowState.Minimized;
        Teboscreen.ControlPanel tebo = new ControlPanel();
       
        tebo.ShowDialog();
        tebo.Dispose();
        this.WindowState = FormWindowState.Normal;
    }

    

        //printer properties func
    void prntProp_Click(object sender, EventArgs e)
    {
        printer prntr = (printer)printersListView.SelectedObject;
        UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
        UpdateMyrichTextBox(" Lunching " + prntr.name + " Properties", 2,
                            Color.AliceBlue);

        string prntProStr = "rundll32";
        Process prntProProcess = new Process();
        prntProProcess.StartInfo.FileName = prntProStr;
        prntProProcess.StartInfo.Arguments = "printui.dll,PrintUIEntry /p /n \"\\\\" +
                                             hostnameStatusStripLabel.Text + "\\" +
                                             prntr.name + "\"";
        prntProProcess.Start();
    }

        //delete printer func
        void delPrnt_Click(object sender, EventArgs e)
        {
        printer prntr = (printer)printersListView.SelectedObject;
        UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
        UpdateMyrichTextBox(" Lunching " + prntr.name + " Delete...", 1,
                            Color.AliceBlue);

        if (MessageBox.Show("Are you sure you want to delete " + prntr.name + " ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.No)
        {
            UpdateMyrichTextBox(" ABORTED", 2,
                            Color.Red);
        }
        else
        {
            string prntDelStr = "rundll32";
            Process prntDelProcess = new Process();
            prntDelProcess.StartInfo.FileName = prntDelStr;
            prntDelProcess.StartInfo.Arguments = "printui.dll,PrintUIEntry /dl /n \"\\\\" +
                                                 hostnameStatusStripLabel.Text + "\\" +
                                                 prntr.name + "\"";
            prntDelProcess.Start();
            fetchPrinters(hostnameStatusStripLabel.Text);
            UpdateMyrichTextBox(" DONE", 2,
                            Color.Red);
        }
    }

        //set default printer func
    void setDef_Click(object sender, EventArgs e)
    {
        printer prntr = (printer)printersListView.SelectedObject;
        UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
        UpdateMyrichTextBox(" Setting " + prntr.name + " as Default...", 1,
                            Color.AliceBlue);
           string p = whosLoggedInWreturn(hostnameStatusStripLabel.Text);

                    if (p == "0" | p == "1")
                    {
                        UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                        UpdateMyrichTextBox(" Unable to determine default printer: any user logged in ?", 2,
                                            Color.AliceBlue);

                    }
                    else
                    {
                        NTAccount f = new NTAccount(p);
                        SecurityIdentifier s = (SecurityIdentifier) f.Translate(typeof (SecurityIdentifier));
                        String sidString = s.ToString();
                        try
                        {


                            devicesKey = RegistryKey.OpenRemoteBaseKey(
                                RegistryHive.Users, hostnameStatusStripLabel.Text).OpenSubKey(
                                    sidString + "\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Devices");



                            string secondPart = prntr.name + "," +
                                                devicesKey.GetValue(
                                                    prntr.name).ToString();

                            devicesKey = RegistryKey.OpenRemoteBaseKey(
                               RegistryHive.Users, hostnameStatusStripLabel.Text).OpenSubKey(
                                   sidString + "\\Software\\Microsoft\\Windows NT\\CurrentVersion\\Windows", true);

                            devicesKey.SetValue("Device", secondPart);
                            devicesKey.Close();
                        }
                        catch (Exception e2)
                        {
                            UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
                            UpdateMyrichTextBox(" Error: " + e2.Message, 2,
                                                Color.Red);
                        }
                        finally 
                        {
                            Thread t = new Thread(() => fetchPrinters(hostnameStatusStripLabel.Text));
                            t.Start();
                            
                        }
                    }
                    UpdateMyrichTextBox("DONE !", 2,
                                        Color.AliceBlue);
                
    }

        //view printer queue func
    void viewQueue_Click(object sender, EventArgs e)
    {
        printer prntr = (printer)printersListView.SelectedObject;
        UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
        UpdateMyrichTextBox(" Lunching " + prntr.name + " queue", 2,
                            Color.AliceBlue);

        string prntQStr = "rundll32";
        Process prntQProcess = new Process();
        prntQProcess.StartInfo.FileName = prntQStr;
        prntQProcess.StartInfo.Arguments = "printui.dll,PrintUIEntry /o /n \"\\\\" +
                                           hostnameStatusStripLabel.Text + "\\" +
                                           prntr.name + "\"";
        prntQProcess.Start();
    }

    //send test page func
    void sendTestP_Click(object sender, EventArgs e)
    {
        printer prntr = (printer)printersListView.SelectedObject;
        UpdateMyrichTextBox(hostnameStatusStripLabel.Text, 1, TextColor.CurrentColor);
        UpdateMyrichTextBox(" Sending a test page to " + prntr.name, 2,
                            Color.AliceBlue);

        string prntQStr = "rundll32";
        Process prntQProcess = new Process();
        prntQProcess.StartInfo.FileName = prntQStr;
        prntQProcess.StartInfo.Arguments = "printui.dll,PrintUIEntry /k /n \"\\\\" +
                                           hostnameStatusStripLabel.Text + "\\" +
                                           prntr.name + "\"";
        prntQProcess.Start();
    }

    private void printersListView_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        viewQueue_Click(sender, e);
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
       aboutForm about = new aboutForm();
        about.ShowDialog();
    }

    private void helpFileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            string exePath = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
            string helpStr = exePath + "\\Resources\\AllinOnHELPFile.pdf";
            Process helpProcess = new Process();
            helpProcess.StartInfo.FileName = helpStr;
            
            helpProcess.Start();
            
        }
        catch (Exception helpEx)
        {

            MessageBox.Show(helpEx.Message);
        }
        
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        

            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;
        Environment.Exit(0);
        
    }

    private void clipboard_btn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hostnameTextbox.Text))
        {
            UpdateMyrichTextBox("Can't copy empty string to clipboard !", 2, Color.AliceBlue);
        }
        else
        {
            try
            {
                Clipboard.SetText(hostnameTextbox.Text);
                UpdateMyrichTextBox(hostnameTextbox.Text + " been copied to the clipboard successfully", 2,
                                    Color.AliceBlue);
            }
            catch (Exception clipEx)
            {
                UpdateMyrichTextBox(hostnameTextbox.Text + " unable to copy: " + clipEx.Message, 2,
                                Color.Red);
                
            }
            
        }
        
    }

    private void copyControls()
    {

            filesPanel.Controls.Clear();
            ToolTip ttip = new ToolTip();

            GroupBox copyGB = new GroupBox();
            copyGB.Name = "copyGB";
            copyGB.Text = "Copy files or folders";
            copyGB.Size = new Size(370, 67);

            Label selPkgLbl = new Label();
            selPkgLbl.Text = "Select a Package:";
            selPkgLbl.Top = 38;
            selPkgLbl.Left = 8;
            selPkgLbl.Size = new Size(100, 30);
            copyGB.Controls.Add(selPkgLbl);

            ComboBoxEx copyPkgCombo = new ComboBoxEx();
            copyPkgCombo.ImageList = imageList2;
            copyPkgCombo.Name = "copyPkgCombo";
            copyPkgCombo.Top = 35;
            copyPkgCombo.Left = 110;
            copyPkgCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            ttip.SetToolTip(copyPkgCombo, "Select the package to copy");
            copyPkgCombo.Size = new Size(160, 21);
            copyPkgCombo.Items.Add(new ComboBoxExItem("Enter manually", 0));
            copyPkgCombo.SelectedIndex = 0;
            string[] copyPackages = Properties.Settings.Default.copyPackages.Split('|');
            foreach (string copyPackage in copyPackages)
            {
                if (!string.IsNullOrEmpty(copyPackage))
                {
                    if (Convert.ToBoolean(copyPackage.Split(';')[1]))
                    {
                        copyPkgCombo.Items.Add(new ComboBoxExItem(copyPackage.Split(';')[0], 1));
                    }
                    else
                    {
                        copyPkgCombo.Items.Add(new ComboBoxExItem(copyPackage.Split(';')[0], 2));
                    }
                }
            }
            copyPkgCombo.SelectedIndexChanged += new EventHandler(copyPkgCombo_SelectedIndexChanged);
            copyGB.Controls.Add(copyPkgCombo);


            Button copyGoBtn = new Button();
            copyGoBtn.Name = "copyGoBtn";
            copyGoBtn.Text = "Go";
            copyGoBtn.Top = 23;
            copyGoBtn.Left = 290;
            ttip.SetToolTip(copyGoBtn, "Start the file proccess");
            copyGoBtn.Size = new Size(70, 35);
            copyGoBtn.Click += new EventHandler(copyGoBtn_Click);
            copyGB.Controls.Add(copyGoBtn);

            GroupBox copyPkgMngGb = new GroupBox();
            copyPkgMngGb.Name = "copyPkgMngGb";
            copyPkgMngGb.Text = "Manage packages";
            copyPkgMngGb.Left = 385;
            copyPkgMngGb.Size = new Size(156, 67);
            

            Button copyAddPkgBtn = new Button();
            copyAddPkgBtn.Size = new Size(32, 30);
            copyAddPkgBtn.Top = 23;
            copyAddPkgBtn.Left = 10;
            ttip.SetToolTip(copyAddPkgBtn, "Add a new package dialog");
            copyAddPkgBtn.Image = AIO2013.Properties.Resources.add_icon24;
            copyAddPkgBtn.Click += new EventHandler(copyAddPkgBtn_Click);
            copyPkgMngGb.Controls.Add(copyAddPkgBtn);

            Button copyDelPkgBtn = new Button();
            copyDelPkgBtn.Name = "copyDelPkgBtn";
            copyDelPkgBtn.Size = new Size(32, 30);
            copyDelPkgBtn.Top = 23;
            copyDelPkgBtn.Left = 62;
            ttip.SetToolTip(copyDelPkgBtn, "Delete the currently selected Package!");
            copyDelPkgBtn.Enabled = false;
            copyDelPkgBtn.Image = AIO2013.Properties.Resources.prntDel;
            copyDelPkgBtn.Click += new EventHandler(copyDelPkgBtn_Click);
            copyPkgMngGb.Controls.Add(copyDelPkgBtn);

            Button copyFavPkgBtn = new Button();
            copyFavPkgBtn.Name = "copyFavPkgBtn";
            copyFavPkgBtn.Size = new Size(32, 30);
            copyFavPkgBtn.Top = 23;
            copyFavPkgBtn.Left = 114;
            ttip.SetToolTip(copyFavPkgBtn, "Manage your quick links section\n located above");
            copyFavPkgBtn.Image = AIO2013.Properties.Resources.fav16;
            copyFavPkgBtn.Click += new EventHandler(copyFavPkgBtn_Click);
            copyPkgMngGb.Controls.Add(copyFavPkgBtn);

            filesPanel.Controls.Add(copyPkgMngGb);

            filesPanel.Controls.Add(copyGB);

            filesPanel.Visible = true;


    }

    void copyGoBtn_Click(object sender, EventArgs e)
    {
        if (hosts4batchFiles == null)
        {
            var copyGB = (GroupBox)filesPanel.Controls[1];
            var copyPkgCombo = (ComboBox)copyGB.Controls["copyPkgCombo"];

            if (copyPkgCombo.SelectedIndex == 0)
            {

                Start_manually_file_action manual = new Start_manually_file_action();
                if (manual.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string copyPackage = Properties.Settings.Default.tempPackage;
                    if (Convert.ToBoolean(copyPackage.Split(';')[1]))
                    {
                        emitCopy(copyPackage);
                    }
                    else
                    {
                        emitDelete(copyPackage);
                    }
                }
                else
                {
                    UpdateMyrichTextBox("Enter a manual package dialog canceled", 0, Color.Red);
                }
            }
            else
            {
                string[] copyPackages = Properties.Settings.Default.copyPackages.Split('|');
                foreach (string copyPackage in copyPackages)
                {
                    if (!string.IsNullOrEmpty(copyPackage) && copyPackage.Split(';')[0].ToString() == copyPkgCombo.SelectedItem.ToString())
                    {
                        if (Convert.ToBoolean(copyPackage.Split(';')[1]))
                        {
                            emitCopy(copyPackage);
                        }
                        else
                        {
                            emitDelete(copyPackage);
                        }
                    }
                }
            }
        }
        else
        {
            if (MessageBox.Show("Warning! this operation will happend to all the hosts in the list\n Are you sure you want to procced?", "WARNING", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                cancelBatchGuis();
            }
            else
            {
                var copyGB = (GroupBox)filesPanel.Controls[1];
                var copyPkgCombo = (ComboBox)copyGB.Controls["copyPkgCombo"];

                if (copyPkgCombo.SelectedIndex == 0)
                {

                    Start_manually_file_action manual = new Start_manually_file_action();
                    if (manual.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string copyPackage = Properties.Settings.Default.tempPackage;
                        if (Convert.ToBoolean(copyPackage.Split(';')[1]))
                        {
                            emitCopy(copyPackage, true);
                            
                        }
                        else
                        {
                            emitDelete(copyPackage, true);
                            
                        }
                    }
                    else
                    {
                        cancelBatchGuis();
                        UpdateMyrichTextBox("Enter a manual package dialog canceled", 0, Color.Red);
                    }
                }
                else
                {
                    string[] copyPackages = Properties.Settings.Default.copyPackages.Split('|');
                    foreach (string copyPackage in copyPackages)
                    {
                        if (!string.IsNullOrEmpty(copyPackage) && copyPackage.Split(';')[0].ToString() == copyPkgCombo.SelectedItem.ToString())
                        {
                            if (Convert.ToBoolean(copyPackage.Split(';')[1]))
                            {
                                emitCopy(copyPackage, true);
                               
                            }
                            else
                            {
                                emitDelete(copyPackage, true);
                                
                            }
                        }
                    }
                }
            }
        }
        
    }

    private void cancelBatchGuis()
    {
        var copyGB = (GroupBox)filesPanel.Controls[1];
        var loadedBatchLbl = (BlinkLabel)copyGB.Controls["loadedBatchLbl"];
        var cancelBatchLink = (LinkLabel)copyGB.Controls["cancelBatchLink"];
        copyGB.Controls.Remove(loadedBatchLbl);
        copyGB.Controls.Remove(cancelBatchLink);
        hosts4batchFiles = null;
    }

private void emitDelete(string settingsString, Boolean isBatch = false)
    {
        Boolean errorFound = false;

        if (!isBatch)
        {
            string[] set = settingsString.Split(';');
            string name = set[0];
            Boolean isFile = Convert.ToBoolean(set[2]);
            string destPath = set[4];

            UpdateMyrichTextBox(hostnameTextbox.Text + " - Deleting package " + name, 1, Color.Turquoise);
            UpdateMyrichTextBox(" - STARTED", 2, Color.Honeydew);

            destPath = destPath.Replace("<remotepc>", hostnameTextbox.Text);
            if (destPath.IndexOf("<username>") > 0 || destPath.IndexOf("<userdomain>") > 0)
            {
                string[] userdetails = whosLoggedInWreturn(hostnameTextbox.Text).Split('\\');
                if (userdetails[0] != "0")
                {
                    if (destPath.IndexOf("<username>") > 0)
                    {
                        destPath = destPath.Replace("<username>", userdetails[1]);
                    }
                    else
                    {
                        destPath = destPath.Replace("<userdomain>", userdetails[0]);
                    }
                }
                else
                {
                    UpdateMyrichTextBox(hostnameTextbox.Text + " No one is logged on localy! ABORTING.", 2, Color.Salmon);
                    errorFound = true;
                }
            }
            if (!errorFound)
            {
                if (pingQuick(hostnameTextbox.Text))
                {
                    if (isFile)
                    {
                        try
                        {
                            startDeleteProc(0, @destPath, hostnameTextbox.Text, name);
                        }
                        catch (Exception e)
                        {
                            UpdateMyrichTextBox(hostnameTextbox.Text + " Delete error: " + e.Message, 2, Color.Salmon);
                        }
                    }
                    else
                    {
                        try
                        {
                            startDeleteProc(1, @destPath, hostnameTextbox.Text, name);
                        }
                        catch (Exception e)
                        {
                            UpdateMyrichTextBox(hostnameTextbox.Text + " Delete error: " + e.Message, 2, Color.Salmon);
                        }
                    }
                }
                else
                {
                    UpdateMyrichTextBox(hostnameTextbox.Text + " isn't returning ping! Is the remote pc online?", 2, Color.Red);
                }
            }
        }
        else
        {
            //batch delete here
            string[] set = settingsString.Split(';');
            string name = set[0];
            Boolean isFile = Convert.ToBoolean(set[2]);

            UpdateMyrichTextBox("Batch operation: Deleting package " + name, 1, Color.Turquoise);
            UpdateMyrichTextBox(" - STARTED", 2, Color.Honeydew);
           
            MsdnMag.FileOperation fileOp = new MsdnMag.FileOperation();
            foreach (var host in hosts4batchFiles)
            {
                string destPath = set[4].Replace("<remotepc>", host);

                if (destPath.IndexOf("<username>") > 0 || destPath.IndexOf("<userdomain>") > 0)
                {
                    string[] userdetails = whosLoggedInWreturn(host).Split('\\');
                    if (userdetails[0] != "0")
                    {
                        if (destPath.IndexOf("<username>") > 0)
                        {
                            destPath = destPath.Replace("<username>", userdetails[1]);
                        }
                        else
                        {
                            destPath = destPath.Replace("<userdomain>", userdetails[0]);
                        }
                    }
                    else
                    {
                        UpdateMyrichTextBox(host + " No one is logged on localy! ABORTING.", 2, Color.Salmon);
                        errorFound = true;
                    }
                }
                if (errorFound)
                {
                    errorFound = false;
                }
                else
                {
                    try
                    {
                        fileOp.DeleteItem(@destPath);
                    }
                    catch (Exception e)
                    {
                        UpdateMyrichTextBox(hostnameTextbox.Text + " Delete error: " + e.Message, 2, Color.Salmon);
                    }
                }
            }
            try
            {
                startDeleteProc(2, " ", "Batch operation:", name, fileOp);
            }
            catch (Exception e)
            {
                UpdateMyrichTextBox(name + " Delete error: " + e.Message, 2, Color.Salmon);
            }

            cancelBatchGuis();
        }
    }

public void startDeleteProc(int type, string dest, string pcName, string pkgName, MsdnMag.FileOperation fileop = null)
    {
        var bw = new BackgroundWorker();
        string erroeMsg = "";
        
        // define the event handlers
        bw.DoWork += (sender, args) =>
        {
            // do your lengthy stuff here -- this will happen in a separate thread
            switch (type)
            {
                case 0: //file
                    try
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(@dest, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently);
                    }
                    catch (Exception e)
                    {
                        erroeMsg = e.Message;
                    }
                    break;
                case 1: //dir 
                    try
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(@dest, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently);
                    }
                    catch (Exception e)
                    {
                        erroeMsg = e.Message;
                    }
                    break;
                case 2:
                    try
                    {
                        fileop.PerformOperations();
                    }
                    catch (Exception e)
                    {
                        if (e.Message == "Exception from HRESULT: 0x80270000")
                        {
                            erroeMsg = "The operation was canceled.";
                        }
                        else
                        {
                            erroeMsg = e.Message;
                        }
                    }
                    break;
                default:
                    args.Cancel = true;
                    break;
            }


        };
        bw.RunWorkerCompleted += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(erroeMsg))
            {
                UpdateMyrichTextBox("ERROR: " + erroeMsg, 2, Color.Salmon);  // do your error handling here
            }
            else
            {
                if (args.Error != null)  // if an exception occurred during DoWork,
                    UpdateMyrichTextBox("ERROR: " + args.Error.ToString(), 2, Color.Salmon);  // do your error handling here
            }

            // Do whatever else you want to do after the work completed.
            // This happens in the main UI thread.
            UpdateMyrichTextBox(pcName + " - Delete package " + pkgName, 1, Color.Turquoise);
            UpdateMyrichTextBox(" - COMPLETED!", 2, Color.Green);

        };
        bw.RunWorkerAsync(); // starts the background worker

        // execution continues here in parallel to the background worker
    }

private void emitCopy(string settingsString, Boolean isBatch = false)
    {
        Boolean errorFound = false;

        if (!isBatch)
        {
            string[] set = settingsString.Split(';');
            string name = set[0];
            Boolean isFile = Convert.ToBoolean(set[2]);
            string srcPath = set[3];
            string destPath = set[4];

            UpdateMyrichTextBox(hostnameTextbox.Text + " - Sending package " + name, 1, Color.Turquoise);
            UpdateMyrichTextBox(" - STARTED", 2, Color.Honeydew);

            destPath = destPath.Replace("<remotepc>", hostnameTextbox.Text);
             if (destPath.IndexOf("<username>") > 0 || destPath.IndexOf("<userdomain>") > 0)
            {
                string[] userdetails = whosLoggedInWreturn(hostnameTextbox.Text).Split('\\');
                if (userdetails[0] != "0")
                {
                    if (destPath.IndexOf("<username>") > 0)
                    {
                        destPath = destPath.Replace("<username>", userdetails[1]);
                    }
                    else
                    {
                        destPath = destPath.Replace("<userdomain>", userdetails[0]);
                    }
                }
                else
                {
                    UpdateMyrichTextBox(hostnameTextbox.Text + " No one is logged on localy! ABORTING.", 2, Color.Salmon);
                    errorFound = true;
                }
            }
             if (!errorFound)
             {
                 if (pingQuick(hostnameTextbox.Text))
                 {
                     if (isFile)
                     {
                         try
                         {
                             FileInfo sourceFile = new FileInfo(srcPath);
                             startCopyProc(0, @srcPath, destPath + sourceFile.Name, hostnameTextbox.Text, name);
                         }
                         catch (Exception e)
                         {
                             UpdateMyrichTextBox(hostnameTextbox.Text + " Copy error: " + e.Message, 2, Color.Salmon);
                         }
                     }
                     else
                     {

                         if (srcPath.EndsWith(@"\*.*"))
                         {
                             try
                             {
                                 srcPath = srcPath.Remove(srcPath.IndexOf(@"\*.*"), 4);
                                 startCopyProc(1, @srcPath, @destPath, hostnameTextbox.Text, name);
                             }
                             catch (Exception e)
                             {
                                 UpdateMyrichTextBox(hostnameTextbox.Text + " Copy error: " + e.Message, 2, Color.Salmon);
                             }

                         }
                         else
                         {
                             try
                             {
                                 DirectoryInfo sourceDir = new DirectoryInfo(srcPath);
                                 Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(@destPath + sourceDir.Name);
                                 startCopyProc(1, @srcPath, @destPath + sourceDir.Name, hostnameTextbox.Text, name);
                             }
                             catch (Exception e)
                             {
                                 UpdateMyrichTextBox(hostnameTextbox.Text + " Copy error: " + e.Message, 2, Color.Salmon);
                             }
                         }
                     }
                 }

                 else
                 {
                     UpdateMyrichTextBox(hostnameTextbox.Text + " isn't returning ping! Is the remote pc online?", 2, Color.Red);
                 }
             }

        }
        else
        {
            //copy batch here!
            string[] set = settingsString.Split(';');
            string name = set[0];
            Boolean isFile = Convert.ToBoolean(set[2]);
            string srcPath = set[3];

            UpdateMyrichTextBox("Batch operation: Sending package " + name, 1, Color.Turquoise);
            UpdateMyrichTextBox(" - STARTED", 2, Color.Honeydew);

            MsdnMag.FileOperation fileOp = new MsdnMag.FileOperation();
            foreach (var host in hosts4batchFiles)
            {
                string destPath = set[4];
                destPath = destPath.Replace("<remotepc>", host);

                if (destPath.IndexOf("<username>") > 0 || destPath.IndexOf("<userdomain>") > 0)
                {
                    string[] userdetails = whosLoggedInWreturn(host).Split('\\');
                    if (userdetails[0] != "0")
                    {
                        if (destPath.IndexOf("<username>") > 0)
                        {
                            destPath = destPath.Replace("<username>", userdetails[1]);
                        }
                        else
                        {
                            destPath = destPath.Replace("<userdomain>", userdetails[0]);
                        }
                    }
                    else
                    {
                        UpdateMyrichTextBox(host + " No one is logged on localy! ABORTING.", 2, Color.Salmon);
                        errorFound = true;
                    }
                }
                if (errorFound)
                {
                    errorFound = false;
                }
                else
                {
                    if (isFile)
                    {

                        try
                        {
                            FileInfo sourceFile = new FileInfo(srcPath);
                            fileOp.CopyItem(@srcPath, @destPath, sourceFile.Name);
                        }
                        catch (Exception e)
                        {
                            UpdateMyrichTextBox(host + " Copy error: " + e.Message, 2, Color.Salmon);
                        }
                    }
                    else
                    {
                        if (srcPath.EndsWith(@"\*.*"))
                        {
                            try
                            {
                                string srcDirPath = srcPath.Remove(srcPath.IndexOf(@"\*.*"), 4);
                                string[] files = Directory.GetFiles(srcDirPath);
                                foreach (string file in files)
                                {
                                    FileInfo sourceFile = new FileInfo(file);
                                    fileOp.CopyItem(srcDirPath + @"\" + sourceFile.Name, @destPath, sourceFile.Name);

                                }
                            }
                            catch (Exception e)
                            {

                                UpdateMyrichTextBox(hostnameTextbox.Text + " Copy error: " + e.Message, 2, Color.Salmon);
                            }

                        }
                        else
                        {
                            try
                            {
                                DirectoryInfo sourceDir = new DirectoryInfo(srcPath);
                                fileOp.CopyItem(@srcPath, @destPath, sourceDir.Name);
                            }
                            catch (Exception e)
                            {
                                UpdateMyrichTextBox(hostnameTextbox.Text + " Copy error: " + e.Message, 2, Color.Salmon);
                            }
                        }
                    }
                }
            }
            try
            {
                startCopyProc(2, null, null , "Batch operation:", name, fileOp);
            }
            catch (Exception e)
            {
                UpdateMyrichTextBox(hostnameTextbox.Text + " Copy error: " + e.Message, 2, Color.Salmon);
            }
            cancelBatchGuis();
        }
    }

public void startCopyProc(int type, string src, string dest, string pcName, string pkgName, MsdnMag.FileOperation fileOp = null)
{
    var bw = new BackgroundWorker();
    string erroeMsg = "";

    // define the event handlers
    bw.DoWork += (sender, args) => {
        // do your lengthy stuff here -- this will happen in a separate thread
        switch (type)
        {
            case 0: //file
                try
                {
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(@src, @dest, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                }
                catch (Exception e)
                {
                    erroeMsg = e.Message;
                }
                
                break;
            case 1: //dir content
                try
                {
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(@src, @dest, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                }
                catch (Exception e)
                {
                    erroeMsg = e.Message;
                }   
                break;
            case 2: //batch
                try
                {
                    fileOp.PerformOperations();
                }
                catch (Exception e)
                {
                    if (e.Message == "Exception from HRESULT: 0x80270000")
                    {
                        erroeMsg = "The operation was canceled.";
                    }
                    else
                    {
                        erroeMsg = e.Message;
                    }
                    
                }   
                break;
            default:
                args.Cancel = true;
                break;
        }
        
        
    };
    bw.RunWorkerCompleted += (sender, args) =>
    {
            if (!string.IsNullOrEmpty(erroeMsg))
            {
                UpdateMyrichTextBox("ERROR: " + erroeMsg, 2, Color.Salmon);  // do your error handling here
            }
            else
            {
                if (args.Error != null)  // if an exception occurred during DoWork,
                UpdateMyrichTextBox("ERROR: " + args.Error.ToString(), 2, Color.Salmon);  // do your error handling here
            }
            

        // Do whatever else you want to do after the work completed.
        // This happens in the main UI thread.
        UpdateMyrichTextBox(pcName + " - Sending package " + pkgName , 1, Color.Turquoise);
        UpdateMyrichTextBox(" - COMPLETED!", 2, Color.Green);

    };
    bw.RunWorkerAsync(); // starts the background worker

    // execution continues here in parallel to the background worker
}



    void copyFavPkgBtn_Click(object sender, EventArgs e)
    {
        filesFavForm fav = new filesFavForm();
        fav.ShowDialog();
        populateFilesFav();
    }


    void copyDelPkgBtn_Click(object sender, EventArgs e)
    {
        var copyGB = (GroupBox)filesPanel.Controls[1];
        var copyPkgMngGb = (GroupBox)filesPanel.Controls[0];
        var copyPkgCombo = (ComboBox)copyGB.Controls["copyPkgCombo"];
        var copyOvrwChk = (CheckBox)copyGB.Controls["copyOvrwChk"];
        var copyGoBtn = (Button)copyGB.Controls["copyGoBtn"];
        var copyDelPkgBtn = (Button)copyPkgMngGb.Controls["copyDelPkgBtn"];
        var copyFavPkgBtn = (Button)copyPkgMngGb.Controls["copyFavPkgBtn"];


        string[] copyPackages = Properties.Settings.Default.copyPackages.Split('|');
        Properties.Settings.Default.copyPackages = "";
        string selectedItem = copyPkgCombo.SelectedItem.ToString();
        copyPkgCombo.Items.Clear();
        copyPkgCombo.Items.Add(new ComboBoxExItem("Enter manually", 0));
        copyPkgCombo.SelectedIndex = 0;
            foreach (string copyPackage in copyPackages)
            {
                if (!string.IsNullOrEmpty(copyPackage) && copyPackage.Split(';')[0].ToString() != selectedItem)
                {
                    if (Convert.ToBoolean(copyPackage.Split(';')[1]))
                    {
                        copyPkgCombo.Items.Add(new ComboBoxExItem(copyPackage.Split(';')[0], 1));
                    }
                    else
                    {
                        copyPkgCombo.Items.Add(new ComboBoxExItem(copyPackage.Split(';')[0], 2));
                    }
                    Properties.Settings.Default.copyPackages = copyPackage + "|";
                }
            }
            UpdateMyrichTextBox("Copy Package " + selectedItem + " deleted.", 0, Color.Wheat);
            populateFilesFav(selectedItem);


         
        
    }

    void copyPkgCombo_SelectedIndexChanged(object sender, EventArgs e)
    {

        var copyGB = (GroupBox)filesPanel.Controls[1];
        var copyPkgMngGb = (GroupBox)filesPanel.Controls["copyPkgMngGb"];

        var copyPkgCombo = (ComboBox)copyGB.Controls["copyPkgCombo"];

        var copyOvrwChk = (CheckBox)copyGB.Controls["copyOvrwChk"];
        var copyGoBtn = (Button)copyGB.Controls["copyGoBtn"];
        var copyDelPkgBtn = (Button)copyPkgMngGb.Controls["copyDelPkgBtn"];
        var copyFavPkgBtn = (Button)copyPkgMngGb.Controls["copyFavPkgBtn"];


        if (copyPkgCombo.SelectedIndex == 0)
        {
            copyDelPkgBtn.Enabled = false;
            copyGoBtn.Text = "Go";
        }
        else
        {
            copyDelPkgBtn.Enabled = true;
            string[] copyPackages = Properties.Settings.Default.copyPackages.Split('|');
            string selectedItem = copyPkgCombo.SelectedItem.ToString();

            foreach (string copyPackage in copyPackages)
            {
                if (copyPackage.Split(';')[0] == selectedItem)
                {
                    if (!string.IsNullOrEmpty(copyPackage) && Convert.ToBoolean(copyPackage.Split(';')[1]))
                    {
                        copyGoBtn.Text = "Copy";

                    }
                    if (!string.IsNullOrEmpty(copyPackage) && !Convert.ToBoolean(copyPackage.Split(';')[1]))
                    {
                        copyGoBtn.Text = "Delete";

                    }
                }
            }
        }
           

    }

    void copyAddPkgBtn_Click(object sender, EventArgs e)
    {
        var copyGB = (GroupBox)filesPanel.Controls[1];
        var copyPkgCombo = (ComboBox)copyGB.Controls["copyPkgCombo"];

        newDelPackageForm dialog = new newDelPackageForm();
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            copyPkgCombo.Items.Clear();
            copyPkgCombo.Items.Add(new ComboBoxExItem("Enter manually", 0));
            copyPkgCombo.SelectedIndex = 0;
            string[] copyPackages = Properties.Settings.Default.copyPackages.Split('|');
            foreach (string copyPackage in copyPackages)
            {
                if (!string.IsNullOrEmpty(copyPackage))
                {
                    if (Convert.ToBoolean(copyPackage.Split(';')[1]))
                    {
                        copyPkgCombo.Items.Add(new ComboBoxExItem(copyPackage.Split(';')[0], 1));
                    }
                    else
                    {
                        copyPkgCombo.Items.Add(new ComboBoxExItem(copyPackage.Split(';')[0], 2));
                    }

                }
            }
            UpdateMyrichTextBox("A new package saved !", 0, Color.Wheat);
            populateFilesFav();
        }
        else
        {
            UpdateMyrichTextBox("Add a new package dialog canceled", 0, Color.Red);
        }
        

    }


    private void populateFilesFav(string delete = "")
    {

        if (!string.IsNullOrEmpty(Properties.Settings.Default.fileLink1))
        {
            fileLink1.Text = Properties.Settings.Default.fileLink1.Split(';')[0];
        }
        else
        {
            fileLink1.Text = "";
        }
        if (!string.IsNullOrEmpty(Properties.Settings.Default.fileLink2))
        {
            fileLink2.Text = Properties.Settings.Default.fileLink2.Split(';')[0];
        }
        else
        {
            fileLink2.Text = "";
        }
        if (!string.IsNullOrEmpty(Properties.Settings.Default.fileLink3))
        {
            fileLink3.Text = Properties.Settings.Default.fileLink3.Split(';')[0];
        }
        else
        {
            fileLink3.Text = "";
        }
        if (!string.IsNullOrEmpty(Properties.Settings.Default.fileLink4))
        {
            fileLink4.Text = Properties.Settings.Default.fileLink4.Split(';')[0];
        }
        else
        {
            fileLink4.Text = "";
        }
        if (!string.IsNullOrEmpty(Properties.Settings.Default.fileLink5))
        {
            fileLink5.Text = Properties.Settings.Default.fileLink5.Split(';')[0];
        }
        else
        {
            fileLink5.Text = "";
        }
        if (!string.IsNullOrEmpty(Properties.Settings.Default.fileLink6))
        {
            fileLink6.Text = Properties.Settings.Default.fileLink6.Split(';')[0];
        }
        else
        {
            fileLink6.Text = "";
        }
        if(!string.IsNullOrEmpty(delete))
        {
            if (delete == Properties.Settings.Default.fileLink1.Split(';')[0])
            {
                Properties.Settings.Default.fileLink1 = "";
                fileLink1.Text = "";
            }
            if (delete == Properties.Settings.Default.fileLink2.Split(';')[0])
            {
                Properties.Settings.Default.fileLink2 = "";
                fileLink2.Text = "";
            }
            if (delete == Properties.Settings.Default.fileLink3.Split(';')[0])
            {
                Properties.Settings.Default.fileLink3 = "";
                fileLink3.Text = "";
            }
            if (delete == Properties.Settings.Default.fileLink4.Split(';')[0])
            {
                Properties.Settings.Default.fileLink4 = "";
                fileLink4.Text = "";
            }
            if (delete == Properties.Settings.Default.fileLink5.Split(';')[0])
            {
                Properties.Settings.Default.fileLink4 = "";
                fileLink5.Text = "";
            }
            if (delete == Properties.Settings.Default.fileLink6.Split(';')[0])
            {
                Properties.Settings.Default.fileLink6 = "";
                fileLink6.Text = "";
            }
        }

    }

    public string[] hosts4batchFiles;
    private void batchFilesBtn_Click(object sender, EventArgs e)
    {
        hosts4batchFiles = null;
        batch_files_operation dialog = new batch_files_operation();
        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            hosts4batchFiles = dialog.hosts;
            dialog.Dispose();


            if (hosts4batchFiles.Length > 0)
            {
                var copyGB = (GroupBox)filesPanel.Controls[1];

                BlinkLabel loadedBatchLbl = new BlinkLabel();
                loadedBatchLbl.Name = "loadedBatchLbl";
                loadedBatchLbl.Text = "A LIST FOR BATCH OPERATION IS LOADED!";
                loadedBatchLbl.Size = new System.Drawing.Size(240, 15);
                loadedBatchLbl.Top = 17;
                loadedBatchLbl.Left = 5;
                loadedBatchLbl.StartBlink();
                copyGB.Controls.Add(loadedBatchLbl);

                LinkLabel cancelBatchLink = new LinkLabel();
                cancelBatchLink.Name = "cancelBatchLink";
                cancelBatchLink.Text = "Cancel";
                cancelBatchLink.Top = 17;
                cancelBatchLink.Left = 246;
                cancelBatchLink.Click += new EventHandler(cancelLink_Click);
                copyGB.Controls.Add(cancelBatchLink);
            }
        }
        else
        {
            cancelBatchGuis();
        }
    }

    void cancelLink_Click(object sender, EventArgs e)
    {
        cancelBatchGuis();
    }

    private void fileLink1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        emitCopy(Properties.Settings.Default.fileLink1);
    }

    private void fileLink2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        emitCopy(Properties.Settings.Default.fileLink2);
    }

    private void fileLink3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        emitCopy(Properties.Settings.Default.fileLink3);
    }

    private void fileLink4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        emitCopy(Properties.Settings.Default.fileLink4);
    }
    private void fileLink5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        emitCopy(Properties.Settings.Default.fileLink5);
    }
    private void fileLink6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        emitCopy(Properties.Settings.Default.fileLink6);
    }

    private void button2_Click(object sender, EventArgs e)
    {
        UpdateMyrichTextBox("Lunching Open Sessions Query", 2, Color.AliceBlue);
        Open_sessions_query dialog = new Open_sessions_query();
        dialog.ShowDialog();
    }

    private void viewQueueToolStripMenuItem_Click(object sender, EventArgs e)
    {
        viewQueue_Click(null, null);
    }

    private void setAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
    {
        setDef_Click(null, null);
    }

    private void deletePrinterToolStripMenuItem_Click(object sender, EventArgs e)
    {
        delPrnt_Click(null, null);
    }

    private void sendTestPageToolStripMenuItem_Click(object sender, EventArgs e)
    {
        sendTestP_Click(null, null);
    }

    private void printerPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        prntProp_Click(null, null);
    }


   

   



    }

}






  
   
                
    





