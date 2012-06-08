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
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;
using Scada.Client.SL.SystemManagerService;
using Scada.Model.Entity;
using Scada.Client.SL.DeviceRealTimeService;


namespace Scada.Client.SL.Modules.BingMaps
{
    public partial class WeatherBar : UserControl
    {
        DeviceRealTimeServiceClient deviceRealTimeService = ServiceManager.GetDeviceRealTimeService();

        public WeatherBar()
        {
            InitializeComponent();
        }

        void deviceRealTimeService_GetWeatherDataReceived(object sender, GetWeatherDataReceivedEventArgs e)
        {
            if (e.Error == null)
            {
                MessageBox.Show("天气预报实时");
                Weather result = BinaryObjTransfer.BinaryDeserialize<Weather>(e.data);
                this.City.Text = result.City;

                this.TodayTemp.Text = result.TodayCurTemp;
                this.TodayDescription.Text = result.TodayWeather;
                this.TodayRange.Text = string.Format("{0}-{1}", result.TodayMinTemp, result.TodayMaxTemp);

                this.ConditionsOverlay.Source = new BitmapImage(new Uri(string.Format("Resources/{0}", result.ImageName), UriKind.Relative));

                this.TomorrowName.Text = "今天";
                this.TomorrowHi.Text = result.TodayMaxTemp;
                this.TomorrowLo.Text = result.TodayMinTemp;
                this.TomorrowImage.Source = new BitmapImage(new Uri(string.Format("Resources/{0}", result.TomorrowSmallImage), UriKind.Relative));

                this.DayAfterName.Text = "明天";
                this.DayAfterHi.Text = result.TomorrowMaxTemp;
                this.DayAfterLo.Text = result.TomorrowMinTemp;
                this.DayAfterImage.Source = new BitmapImage(new Uri(string.Format("Resources/{0}", result.TomorrowSmallImage), UriKind.Relative));

                this.TwoDaysAwayName.Text = "后天";
                this.TwoDaysAwayHi.Text = result.AfterTomorrowMaxTemp;
                this.TwoDaysAwayLo.Text = result.AfterTomorrowMinTemp;
                this.TwoDaysAwayImage.Source = new BitmapImage(new Uri(string.Format("Resources/{0}", result.AfterTomorrowSmallImage), UriKind.Relative));
            }
            else
            {
                this.City.Text = "气象信息读取失败。";
            }
        }

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            deviceRealTimeService.GetWeatherDataReceived += new EventHandler<GetWeatherDataReceivedEventArgs>(deviceRealTimeService_GetWeatherDataReceived);
            deviceRealTimeService.GetWeatherDataMsgAsync();
        }
    }
}
