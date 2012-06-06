using System;
using System.Collections.Generic;


namespace Scada.Model.Entity
{


    public class SystemParameterManage
    {

        public Guid ParameterID { get; set; }

        public String ParameterKey { get; set; }

        public float ParameterValue { get; set; }


    }


    public class SystemGlobalParameter
    {

        public Int32 ConnectType { get; set; }

        public String ConnectName { get; set; }

        public String MainDNS { get; set; }

        public String SecondDNS { get; set; }

        public String Domain { get; set; }

        public Int32 Port { get; set; }

        public string Broadcast { get; set; }
        public string WeatherCity { get; set; }
        public int? DefaultZoomLevel { get; set; }
        public decimal? DefaultLongitude { get; set; }
        public decimal? DefaultLatitude { get; set; }
        public bool? IsShowTool { get; set; }
    }

}
