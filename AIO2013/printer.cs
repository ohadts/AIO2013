using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AIO2013
{

    
    


    public class printer
    {
        private string[] PrinterStatuses = { "", "Error", "Unknown", "Idle", "Printing", "Warmup", "Stopped Printing", "Offline" };
        private string _name;
        private string _port;
        private string _share;
        private bool _default;
        private string _status;



        public printer(string name, string port, string share, int printerStatus , bool Default)
        {
            _name = name;
            _port = port;
            _share = share;
            _default = Default;
            _status = PrinterStatuses[printerStatus];

        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string port
        {
            get { return _port; }
            set { _port = value; }
        }

        public string share
        {
            get { return _share; }
            set { _share = value; }
        }

        public string status
        {
            get { return _status; }
            set { _status = value; }
        }

        public bool Default
        {
            get { return _default; }
            set { _default = value; }
        }
    }
}
