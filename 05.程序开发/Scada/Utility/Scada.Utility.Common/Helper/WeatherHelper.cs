using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scada.Model.Entity;

namespace Scada.Utility.Common.Helper
{
    /// <summary>
    /// 天气预报帮助类
    /// yanghk at 2012-3-2
    /// </summary>
    public static class WeatherHelper
    {
        static Dictionary<string, string> dicWeather = new Dictionary<string, string>();
        static Dictionary<string, string> dicWeatherBig = new Dictionary<string, string>();


        public static Weather GetWeather(string[] array)
        {
            if (!(dicWeather.Any() && dicWeatherBig.Any()))
            {
                InitData();
            }
            Weather wt = new Weather();
            wt.City = array[1];
            int index = array[10].IndexOf("气温");
            //当天气温
            wt.TodayCurTemp = array[10].Substring(index).Split('；')[0].Split('：')[1].Trim();
            wt.ImageName = GetBigImageByString(array[6].Split(' ')[1]);

            wt.TodayDate = array[6].Split(' ')[0];
            wt.TodayMaxTemp = array[5].Split('/')[1];
            wt.TodayMinTemp = array[5].Split('/')[0];
            wt.TodayWeather = array[6].Split(' ')[1];
            wt.TodaySmallImage = GetImageByString(array[6].Split(' ')[1]);

            wt.TomorrowDate = array[13].Split(' ')[0];
            wt.TomorrowMaxTemp = array[12].Split('/')[1];
            wt.TomorrowMinTemp = array[12].Split('/')[0];
            wt.TomorrowWeather = array[13].Split(' ')[1] ;
            wt.TomorrowSmallImage = GetImageByString(array[13].Split(' ')[1]);

            wt.AfterTomorrowDate = array[18].Split(' ')[0];
            wt.AfterTomorrowMaxTemp = array[17].Split('/')[1];
            wt.AfterTomorrowMinTemp = array[17].Split('/')[0];
            wt.AfterTomorrowWeather = array[18].Split(' ')[1];
            wt.AfterTomorrowSmallImage = GetImageByString(array[18].Split(' ')[1]);
            return wt;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private static void InitData()
        {
            dicWeather.Add("未知	", "29.png");
            dicWeather.Add("晴", "31.png");
            dicWeather.Add("多云", "29.png");
            dicWeather.Add("阴", "26.png");
            dicWeather.Add("阵雨", "5.png");
            dicWeather.Add("雷阵雨", "1.png");
            dicWeather.Add("雷阵雨并伴有冰雹", "17.png");
            dicWeather.Add("雨夹雪", "8.png");
            dicWeather.Add("小雨", "8.png");
            dicWeather.Add("中雨", "8.png");
            dicWeather.Add("大雨", "8.png");
            dicWeather.Add("暴雨", "8.png");
            dicWeather.Add("大暴雨", "8.png");
            dicWeather.Add("特大暴雨", "8.png");
            dicWeather.Add("阵雪", "8.png");
            dicWeather.Add("小雪", "13.png");
            dicWeather.Add("中雪", "13.png");
            dicWeather.Add("大雪", "13.png");
            dicWeather.Add("暴雪", "13.png");
            dicWeather.Add("雾", "19.png");
            dicWeather.Add("冻雨", "8.png");
            dicWeather.Add("沙尘暴", "29.png");
            dicWeather.Add("小雨-中雨", "8.png");
            dicWeather.Add("中雨-大雨", "8.png");
            dicWeather.Add("大雨-暴雨", "8.png");
            dicWeather.Add("暴雨-大暴雨", "8.png");
            dicWeather.Add("大暴雨-特大暴雨", "8.png");
            dicWeather.Add("小雪-中雪", "13.png");
            dicWeather.Add("中雪-大雪", "13.png");
            dicWeather.Add("大雪-暴雪", "13.png");
            dicWeather.Add("浮尘", "29.png");
            dicWeather.Add("扬沙", "29.png");
            dicWeather.Add("强沙尘暴", "29.png");
            dicWeather.Add("小到中雨", "8.png");
            dicWeather.Add("中到大雨", "8.png");
            dicWeather.Add("大到暴雨", "8.png");
            dicWeather.Add("小到中雪", "13.png");
            dicWeather.Add("中到大雪", "13.png");
            dicWeather.Add("大到暴雪", "13.png");

            dicWeatherBig.Add("未知	", "undocked_partly-cloudy.png");
            dicWeatherBig.Add("晴", "undocked_sun.png");
            dicWeatherBig.Add("多云", "undocked_partly-cloudy.png");
            dicWeatherBig.Add("阴", "undocked_cloudy.png");
            dicWeatherBig.Add("阵雨", "undocked_few-showers.png");
            dicWeatherBig.Add("雷阵雨", "undocked_thunderstorm.png");
            dicWeatherBig.Add("雷阵雨并伴有冰雹", "undocked_hail.png");
            dicWeatherBig.Add("雨夹雪", "undocked_rainy.png");
            dicWeatherBig.Add("小雨", "undocked_rainy.png");
            dicWeatherBig.Add("中雨", "undocked_rainy.png");
            dicWeatherBig.Add("大雨", "undocked_rainy.png");
            dicWeatherBig.Add("暴雨", "undocked_rainy.png");
            dicWeatherBig.Add("大暴雨", "undocked_rainy.png");
            dicWeatherBig.Add("特大暴雨", "undocked_rainy.png");
            dicWeatherBig.Add("阵雪", "undocked_rainy.png");
            dicWeatherBig.Add("小雪", "undocked_snow.png");
            dicWeatherBig.Add("中雪", "undocked_snow.png");
            dicWeatherBig.Add("大雪", "undocked_snow.png");
            dicWeatherBig.Add("暴雪", "undocked_snow.png");
            dicWeatherBig.Add("雾", "undocked_foggy.png");
            dicWeatherBig.Add("冻雨", "undocked_rainy.png");
            dicWeatherBig.Add("沙尘暴", "undocked_partly-cloudy.png");
            dicWeatherBig.Add("小雨-中雨", "undocked_rainy.png");
            dicWeatherBig.Add("中雨-大雨", "undocked_rainy.png");
            dicWeatherBig.Add("大雨-暴雨", "undocked_rainy.png");
            dicWeatherBig.Add("暴雨-大暴雨", "undocked_rainy.png");
            dicWeatherBig.Add("大暴雨-特大暴雨", "undocked_rainy.png");
            dicWeatherBig.Add("小雪-中雪", "undocked_snow.png");
            dicWeatherBig.Add("中雪-大雪", "undocked_snow.png");
            dicWeatherBig.Add("大雪-暴雪", "undocked_snow.png");
            dicWeatherBig.Add("浮尘", "undocked_partly-cloudy.png");
            dicWeatherBig.Add("扬沙", "undocked_partly-cloudy.png");
            dicWeatherBig.Add("强沙尘暴", "undocked_partly-cloudy.png");
            dicWeatherBig.Add("小到中雨", "undocked_rainy.png");
            dicWeatherBig.Add("中到大雨", "undocked_rainy.png");
            dicWeatherBig.Add("大到暴雨", "undocked_rainy.png");
            dicWeatherBig.Add("小到中雪", "undocked_snow.png");
            dicWeatherBig.Add("中到大雪", "undocked_snow.png");
            dicWeatherBig.Add("大到暴雪", "undocked_snow.png");
        }

        private static string GetImageByString(string name)
        {
            if (name.IndexOf("转") > -1)
                name = name.Split('转')[0];
            string imgName = "";
            dicWeather.TryGetValue(name, out imgName);
            return imgName;
        }

        private static string GetBigImageByString(string name)
        {
            if (name.IndexOf("转") > -1)
                name = name.Split('转')[0];
            string imgName = "";
            dicWeatherBig.TryGetValue(name, out imgName);
            return imgName;
        }
    }
}
