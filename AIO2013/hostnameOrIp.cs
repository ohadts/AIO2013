using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.NetworkInformation;


namespace AIO2013
{



    class hostnameOrIp
    {
       
        public string hostname;
        public string ipAddress;
        public static string recData;
        public ToolStripLabel hostLabel;
        public ToolStripLabel ipLabel;

        


        public void Resolve()
        {


            if (pingFunction(recData) == "Success")
            {
            
            if (IsValidIP(recData)) 
            {
                ipAddress = recData;
                hostname = getDNSName(recData);
            }
            else
            {
                hostname = recData;
                IPHostEntry host = new IPHostEntry();
                try
                {
                    host = Dns.GetHostEntry(recData);
                }
                catch (Exception e)
                {

                    host.HostName = e.Message;
                }

                ipAddress = host.AddressList[0].ToString();
                
            }

            hostLabel.Text = hostname;
            ipLabel.Text = ipAddress;

            }
            else
            {
                hostLabel.Text = pingFunction(recData);
                ipLabel.Text = "Unable to fetch";

            }
           
        }

        public static bool IsValidIP(string address) 
        { 
            IPAddress ip; 
            return IPAddress.TryParse(address, out ip); 
            
        }

        public static string getDNSName(string myIP)
        {
            IPHostEntry host = new IPHostEntry();
            try
            {
                host = Dns.GetHostEntry(myIP);
            }
            catch (Exception e)
            {

                host.HostName = e.Message;
            }
            
             return host.HostName;
            
            
        }

        public string pingFunction(string hostAdrress)
        {

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
                PingReply reply = pingSender.Send(hostAdrress, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    return "Success";
                }
                else
                {
                    return "No connection: " + reply.Status;
                }
            }


            catch (Exception ex)
            {
                if (ex.Message == "Value cannot be null.\r\nParameter name: hostNameOrAddress")
                {
                    return "Error!\r\nEmpty value in text box.";
                }
                else
                {
                    return "Error: " + ex.InnerException.Message;
                }
            }

        }


        
    }
}
