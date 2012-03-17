using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;


namespace NetData
{
    /// <summary>
    /// 天气预报数据块
    /// </summary>
    public class WeatherDataBlock
    {
        public WeatherDataBlock()
        {

        }

        public WeatherDataBlock(byte[] data)
        {
            TodayWeatherLength = BitConverter.ToUInt16(data, 0);
            TodayWeather = StringHelper.GetDefulatStringByByteArr(data, 2, TodayWeatherLength);
        }
       
        /// <summary>
        /// 今日天气情况长度
        /// </summary>
        public ushort TodayWeatherLength { get; set; }

        /// <summary>
        /// 今日天气情况
        /// </summary>
        public string TodayWeather { get; set; }

        public byte[] ToByte()
        {
            List<byte> result = new List<byte>();
            result.AddRange(BitConverter.GetBytes(StringHelper.GetLength(TodayWeather)));
            result.AddRange(StringHelper.GetCharactersByte(TodayWeather,50));
            return result.ToArray();
        }
    }
}
