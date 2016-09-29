using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;
using System.Windows.Forms;

namespace AIO2013
{
    class longPeriodPingClass
    {
        
        public longPeriodPingClass(string hostname, int minutes, string logLocation)
        {
            _hostname = hostname;
            _minutes = minutes; 
            _logLocation = logLocation;
            _seconds = minutes * 60;

            if (File.Exists(logLocation))
            {
                if (MessageBox.Show("File exists, overwrite?", "File Exists: " + logLocation, MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    hasCanceled = true;
                }
                else
                {
                    DoWork();
                }
                
            }
            else
            {
                DoWork();
            }
           
        }

        public static bool hasCanceled = false;
        private string _hostname;
        private int _minutes;
        private string _logLocation;
        private int _seconds;

        public string pingResults;
        private object threadLock = new object();


        private void DoWork()
        {
            try
            {
                StreamWriter logFile = new StreamWriter(_logLocation);
                for (int i = 0; i < _seconds; i++)
                {
                    logFile.WriteLine(pingFunction(_hostname));
                }
                logFile.Close();
                logFile.Dispose();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        //this function simulates "work" by simply counting from 1 to totalSeconds
        public string pingFunction(string host)
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
                int timeout = 600;
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
                        pingResults = ("Error!\r\nEmpty value in text box.");
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

                return pingResults;


            }
        }
    }
}
