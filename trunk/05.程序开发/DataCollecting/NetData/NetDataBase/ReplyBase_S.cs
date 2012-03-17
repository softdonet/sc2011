using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetData
{
    /// <summary>
    /// 服务器回复基类型
    /// </summary>
    public class ReplyBase_S : MessageBase
    {
        public ReplyBase_S()
        { }

        public ReplyBase_S(byte[] data)
            : base(data)
        {
            haveConfigInfo = data[21] == 1 ? true : false;
            haveWeatherInfo = data[22] == 1 ? true : false;
            haveBroadcastInfo = data[23] == 1 ? true : false;
            //数组指针
            int index = 24;
            if (haveConfigInfo)
            {
                byte[] arr = new byte[136];
                Array.Copy(data, index, arr, 0, 136);
                configData = new ConfigDataBlock(arr);
                index += 136;
            }
            if (haveWeatherInfo)
            {
                byte[] arr = new byte[52];
                Array.Copy(data, index, arr, 0, 52);
                weatherData = new WeatherDataBlock(arr);
                index += 52;
            }
            if (haveBroadcastInfo)
            {
                byte[] arr = new byte[52];
                Array.Copy(data, index, arr, 0, 52);
                broadcastData = new BroadcastDataBlock(arr);
                index += 52;
            }
        }

        /// <summary>
        /// 是否含有配置信息
        /// </summary>
        bool haveConfigInfo;
        public bool HaveConfigInfo
        {
            get { return haveConfigInfo; }
            set { haveConfigInfo = value; }
        }
        /// <summary>
        /// 是否含有天气预报信息
        /// </summary>
        bool haveWeatherInfo;
        public bool HaveWeatherInfo
        {
            get { return haveWeatherInfo; }
            set { haveWeatherInfo = value; }
        }
        /// <summary>
        /// 是否含有广播信息
        /// </summary>
        bool haveBroadcastInfo;
        public bool HaveBroadcastInfo
        {
            get { return haveBroadcastInfo; }
            set { haveBroadcastInfo = value; }
        }

        /// <summary>
        /// 配置数据
        /// </summary>
        ConfigDataBlock configData;
        public ConfigDataBlock ConfigData
        {
            get { return configData; }
            set { configData = value; }
        }

        /// <summary>
        /// 天气预报数据
        /// </summary>
        WeatherDataBlock weatherData;
        public WeatherDataBlock WeatherData
        {
            get { return weatherData; }
            set { weatherData = value; }
        }

        /// <summary>
        /// 广播信息
        /// </summary>
        BroadcastDataBlock broadcastData;
        public BroadcastDataBlock BroadcastData
        {
            get { return broadcastData; }
            set { broadcastData = value; }
        }

        protected override void PushBodyByte(List<byte> result)
        {
            result.Add((byte)(haveConfigInfo ? 1 : 0));
            result.Add((byte)(haveWeatherInfo ? 1 : 0));
            result.Add((byte)(haveBroadcastInfo ? 1 : 0));
            if (haveConfigInfo)
            {
                result.AddRange(configData.ToByte());
            }
            if (haveWeatherInfo)
            {
                result.AddRange(weatherData.ToByte());
            }
            if (haveBroadcastInfo)
            {
                result.AddRange(broadcastData.ToByte());
            }
        }
    }
}
