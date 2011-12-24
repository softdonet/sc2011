using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using Scada.Client.SL.WeatherWebService;
using Scada.Client.SL.CommClass;

namespace Scada.Client.SL.Modules.BingMaps
{
    public partial class WeatherBar : UserControl
    {
        Dictionary<string, string> dicWeather = new Dictionary<string, string>();
        Dictionary<string, string> dicWeatherBig = new Dictionary<string, string>();
        WeatherWebService.WeatherWebServiceSoapClient ws;
       
        public WeatherBar()
        {
            InitializeComponent();
            ws = ServiceManager.GetWeatherWebService();
           // ws.GetWeatherCompleted += new EventHandler<WeatherWebService.GetWeatherCompletedEventArgs>(ws_GetWeatherCompleted);
        }

        void ws_GetWeatherCompleted(object sender, WeatherWebService.GetWeatherCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
                this.City.Text = "气象信息读取失败。";
                return;
            }
            ArrayOfString array = e.Result;
            this.City.Text = array[1];
            int index = array[10].IndexOf("气温");
            string temp = array[10].Substring(index).Split('；')[0].Split('：')[1].Trim();

            this.TodayTemp.Text = temp;
            this.TodayDescription.Text = array[6].Split(' ')[1];
            this.TodayRange.Text = array[5].Replace("/", " - ");

            this.ConditionsOverlay.Source = new BitmapImage(new Uri(string.Format("Resources/{0}", GetBigImageByString(array[6].Split(' ')[1])), UriKind.Relative));

            this.TomorrowName.Text = array[6].Split(' ')[0];
            this.TomorrowHi.Text = array[5].Split('/')[1];
            this.TomorrowLo.Text = array[5].Split('/')[0];
            this.TomorrowImage.Source = new BitmapImage(new Uri(string.Format("Resources/{0}", GetImageByString(array[6].Split(' ')[1])), UriKind.Relative));


            this.DayAfterName.Text = array[13].Split(' ')[0];
            this.DayAfterHi.Text = array[12].Split('/')[1];
            this.DayAfterLo.Text = array[12].Split('/')[0];
            this.DayAfterImage.Source = new BitmapImage(new Uri(string.Format("Resources/{0}", GetImageByString(array[13].Split(' ')[1])), UriKind.Relative));

            this.TwoDaysAwayName.Text = array[18].Split(' ')[0];
            this.TwoDaysAwayHi.Text = array[12].Split('/')[1];
            this.TwoDaysAwayLo.Text = array[12].Split('/')[0];
            this.TwoDaysAwayImage.Source = new BitmapImage(new Uri(string.Format("Resources/{0}", GetImageByString(array[18].Split(' ')[1])), UriKind.Relative));
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
         
            InitData();
            ws.GetWeatherAsync("北京");
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
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

        private string GetImageByString(string name)
        {
            if (name.IndexOf("转") > -1)
                name = name.Split('转')[0];
            string imgName = "";
            dicWeather.TryGetValue(name, out imgName);
            return imgName;
        }

        private string GetBigImageByString(string name)
        {
            if (name.IndexOf("转") > -1)
                name = name.Split('转')[0];
            string imgName = "";
            dicWeatherBig.TryGetValue(name, out imgName);
            return imgName;
        }
    }
}
