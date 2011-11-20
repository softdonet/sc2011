using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MES.CommClass
{
    public class ScanDataModel
    {
        public string SEQ { get; set; }
        public string BODYNO { get; set; }
        string _scanTime = null;
        public string ScanTime
        {
            get { return Convert.ToDateTime(_scanTime).ToString("MM-dd HH:mm:ss"); }
            set { _scanTime = value; }
        }
    }
}
