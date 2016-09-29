using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Tracert
{
	public partial class MainForm : Form
	{
		public MainForm(string host)
		{
			InitializeComponent();
            destination.Text = host;
            startTraceFunc();
		}

		private void close_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void startTrace_Click(object sender, EventArgs e)
		{
            startTraceFunc();
		}

        private void startTraceFunc()
        {
            try
            {
                tracert.HostNameOrAddress = destination.Text;
                routeList.Items.Clear();
                tracert.Trace();
                startTrace.Enabled = false;
                destination.Enabled = false;
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message, "Tracert Demo");
            }
        }

		delegate void ThreadSwitch();

		private void OnGetHostEntry(IAsyncResult ar)
		{
			try
			{
				ListViewItem.ListViewSubItem hostNameItem = ar.AsyncState as ListViewItem.ListViewSubItem;
				ThreadSwitch delg = delegate{
					hostNameItem.Text = Dns.EndGetHostEntry(ar).HostName;
				};
				
				this.Invoke(delg);
			}
			catch (SocketException ex)
			{
				Trace.WriteLine(ex.ToString());
			}
		}

		private void tracert_RouteNodeFound(object sender, VRK.Net.RouteNodeFoundEventArgs e)
		{
			ListViewItem item = routeList.Items.Add(e.Node.Address.ToString());

			item.SubItems.Add((item.Index + 1).ToString());
			ListViewItem.ListViewSubItem hostNameItem = item.SubItems.Add(String.Empty);
			item.SubItems.Add(e.Node.Status == IPStatus.Success ? e.Node.RoundTripTime.ToString() : "*");

			if (e.Node.Status == IPStatus.Success)
			{
				Dns.BeginGetHostEntry(e.Node.Address, new AsyncCallback(this.OnGetHostEntry), hostNameItem);
			}
		}

		private void tracert_Done(object sender, EventArgs e)
		{
			startTrace.Enabled = true;
            destination.Enabled = true;
		}
	}
}