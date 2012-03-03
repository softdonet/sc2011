using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.Model.Entity
{
    public class Weather
    {
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 实时温度图片名称
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// 今日日期
        /// </summary>
        public string TodayDate { get; set; }
        /// <summary>
        /// 今日实时温度
        /// </summary>
        public string TodayCurTemp { get; set; }
        /// <summary>
        /// 今日最高温度
        /// </summary>
        public string TodayMaxTemp { get; set; }
        /// <summary>
        /// 今日最低温度
        /// </summary>
        public string TodayMinTemp { get; set; }
        /// <summary>
        /// 今日天气
        /// </summary>
        public string TodayWeather { get; set; }
        /// <summary>
        /// 今日天气情况描述
        /// </summary>
        public string TodayWeatherDescribe { get; set; }
        /// <summary>
        /// 今日小图片
        /// </summary>
        public string TodaySmallImage { get; set; }



        /// <summary>
        /// 明日日期
        /// </summary>
        public string TomorrowDate { get; set; }
        /// <summary>
        /// 明日实时温度
        /// </summary>
        public string TomorrowCurTemp { get; set; }
        /// <summary>
        /// 明日最高温度
        /// </summary>
        public string TomorrowMaxTemp { get; set; }
        /// <summary>
        /// 明日最低温度
        /// </summary>
        public string TomorrowMinTemp { get; set; }
        /// <summary>
        /// 明日天气
        /// </summary>
        public string TomorrowWeather { get; set; }
        /// <summary>
        /// 明日天气情况描述
        /// </summary>
        public string TomorrowWeatherDescribe { get; set; }
        /// <summary>
        /// 明日小图片
        /// </summary>
        public string TomorrowSmallImage { get; set; }



        /// <summary>
        /// 后天日期
        /// </summary>
        public string AfterTomorrowDate { get; set; }
        /// <summary>
        /// 后日实时温度
        /// </summary>
        public string AfterTomorrowCurTemp { get; set; }
        /// <summary>
        /// 后日最高温度
        /// </summary>
        public string AfterTomorrowMaxTemp { get; set; }
        /// <summary>
        /// 后日最低温度
        /// </summary>
        public string AfterTomorrowMinTemp { get; set; }
        /// <summary>
        /// 后日天气
        /// </summary>
        public string AfterTomorrowWeather { get; set; }
        /// <summary>
        /// 后日天气情况描述
        /// </summary>
        public string AfterTomorrowWeatherDescribe { get; set; }
        /// <summary>
        /// 后日小图片
        /// </summary>
        public string AfterTomorrowSmallImage { get; set; }
    }
}
