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
        /// <summary>
        /// 广播信息
        /// </summary>
        public string Broadcast { get; set; }
        /// <summary>
        /// 天气预报城市
        /// </summary>
        public string WeatherCity { get; set; }
        /// <summary>
        /// 地图默认放大级别
        /// </summary>
        public int? DefaultZoomLevel { get; set; }
        /// <summary>
        /// 默认中心点经度
        /// </summary>
        public decimal? DefaultLongitude { get; set; }
        /// <summary>
        /// 默认中心点纬度
        /// </summary>
        public decimal? DefaultLatitude { get; set; }
        /// <summary>
        /// 首页是否显示地图工具
        /// </summary>
        public bool? IsShowTool { get; set; }

        /// <summary>
        /// 图表最大温度值
        /// </summary>
        public int ChartMaxTemp
        {
            get
            {
                return 40;
            }
            set { }
        }

        /// <summary>
        /// 图表最小温度值
        /// </summary>
        public int ChartMinTemp
        {
            get
            {
                return 10;
            }
            set{}
        }
        /// <summary>
        /// 图表高温分界线
        /// </summary>
        public int ChartHighTemp
        {
            get
            {
                return 30;
            }
            set { }
        }

        /// <summary>
        /// 图表低温分界线
        /// </summary>
        public int ChartLowTemp
        {
            get
            {
                return 20;
            }
            set { }
        }


    }

}
