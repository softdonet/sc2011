using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.Helper;

namespace DataCollecting.NetData
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
            TodayMark = data[0];
            TodayWeather = StringHelper.GetDefulatStringByByteArr(data, 1, 50);
            TomorrowMark = data[51];
            TomorrowWeather = StringHelper.GetDefulatStringByByteArr(data, 52, 50);
        }
       
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
            List<byte> result = new List<byte>();
            result.Add(TodayMark);
            result.AddRange(GetWeatherInfo(TodayWeather));
            result.Add(TomorrowMark);
            result.AddRange(GetWeatherInfo(TomorrowWeather));
            return result.ToArray();
        }

        /// <summary>
        /// 获取天气情况数组
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private byte[] GetWeatherInfo(string info)
        {
            List<byte> result = new List<byte>();
            byte[] arr = System.Text.Encoding.Default.GetBytes(info);
            if (arr.Length <= 50)
            {
                result.AddRange(arr);
                //补零
                for (int j = 0; j < 50 - arr.Length; j++)
                {
                    result.Add(0x00);
                }
            }
            else
            {
                //大于50直接截断
                byte[] temp = new byte[50];
                Array.Copy(arr, 0, temp, 0, 50);
                result.AddRange(temp);
            }
            return result.ToArray();
        }
    }
}
