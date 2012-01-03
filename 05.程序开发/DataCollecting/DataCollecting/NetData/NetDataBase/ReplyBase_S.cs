﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataCollecting.Common;

namespace DataCollecting.NetData
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
