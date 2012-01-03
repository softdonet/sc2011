using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollecting.Common
{
    /// <summary>
    /// 天气预报数据块
    /// </summary>
    public class WeatherDataBlock
    {
        /// <summary>
        /// 今日天气标识
        /// </summary>
        public byte TodayMark { get; set; }
        /// <summary>
        /// 今日天气情况
        /// </summary>
        public string TodayWeather { get; set; }
        /// <summary>
        /// 明日天气标识
        /// </summary>
        public byte TomorrowMark { get; set; }
        /// <summary>
        /// 明日天气情况
        /// </summary>
        public string TomorrowWeather { get; set; }

        public byte[] ToByte()
        {
            return null;
        }

    }
}
