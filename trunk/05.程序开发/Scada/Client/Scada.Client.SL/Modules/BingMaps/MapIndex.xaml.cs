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
using Microsoft.Maps.MapControl;
using Microsoft.Maps.MapControl.Navigation;
using Scada.Client.SL.DeviceRealTimeService;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.WeatherWebService;

namespace Scada.Client.SL.Modules.BingMaps
{
    public partial class MapIndex : UserControl
    {
        public MapIndex()
        {
            InitializeComponent();
            InitMap();
            MyContent.CloseBtn += new EventHandler(MyContent_CloseBtn);
            DeviceRealTimeServiceClient deviceRealTimeService = ServiceManager.GetDeviceRealTimeService();
            deviceRealTimeService.GetRealTimeDataReceived += new EventHandler<GetRealTimeDataReceivedEventArgs>(deviceRealTimeService_GetRealTimeDataReceived);
            deviceRealTimeService.GetAlarmDataReceived += new EventHandler<GetAlarmDataReceivedEventArgs>(deviceRealTimeService_GetAlarmDataReceived);
            deviceRealTimeService.GetCallDataReceived += new EventHandler<GetCallDataReceivedEventArgs>(deviceRealTimeService_GetCallDataReceived);
        }

        void deviceRealTimeService_GetCallDataReceived(object sender, GetCallDataReceivedEventArgs e)
        {
            this.txtCall.Text = e.data.Element("Device").Value;
        }

        void deviceRealTimeService_GetAlarmDataReceived(object sender, GetAlarmDataReceivedEventArgs e)
        {
            this.txtAlarm.Text = e.data.Element("Device").Value;
        }

        void deviceRealTimeService_GetRealTimeDataReceived(object sender, GetRealTimeDataReceivedEventArgs e)
        {
            this.txtRealTime.Text = e.data.Element("Device").Value;
        }

        void MyContent_CloseBtn(object sender, EventArgs e)
        {
            Storyboard2.Begin();
            ViewHost.Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// 初始化地图（自定义导航、加载中文地图瓦片）
        /// </summary>
        private void InitMap()
        {
            //自定义导航
            map.MapForeground.TemplateApplied += delegate(object sender, EventArgs args)
            {
                map.MapForeground.NavigationBar.TemplateApplied += delegate(object obj, EventArgs e)
                {
                    //清除导航菜单上现有项
                    NavigationBar navBar = map.MapForeground.NavigationBar;
                    navBar.HorizontalPanel.Children.Clear();
                    //添加自定义导航菜单项
                    ChangeMapModeButton btnRoad = new ChangeMapModeButton(new RoadMode(), "路况模式", "点击导航到路况模式");
                    btnRoad.IsChecked = false;
                    navBar.HorizontalPanel.Children.Add(btnRoad);
                    ChangeMapModeButton btnAerial = new ChangeMapModeButton(new AerialMode(true), "卫星模式", "点击导航到卫星模式");
                    btnAerial.IsChecked = true;
                    navBar.HorizontalPanel.Children.Add(btnAerial);
                };
            };
            //初始化中文地图模式
            ChinaMode chinaMode = new ChinaMode();
            MapTileLayer tileChinaLayer = chinaMode.GetChinaTileLayer();
            map.Children.Add(tileChinaLayer);
            this.map.Mode = chinaMode;
            map.ZoomLevel = 4;
            //使中文地图适应模式的切换
            map.ModeChanged += delegate(object sender, Microsoft.Maps.MapControl.MapEventArgs e)
            {
                if (map.Mode.GetType().Name != "RoadMode")
                {
                    tileChinaLayer.Visibility = Visibility.Collapsed;
                }
                else
                {
                    this.map.Mode = chinaMode;
                    tileChinaLayer.Visibility = Visibility.Visible;
                }
            };
            //根据不同放大级别 显示相应图层,放大级别大于4，显示标注，否则显示星星
            this.map.ViewChangeEnd += (s, e) =>
            {
                if (map.ZoomLevel > 10)
                {
                    this.myMapLayerDevice.Visibility = Visibility.Visible;
                    this.myMapLayerDeviceAvg.Visibility = Visibility.Collapsed;
                }
                else
                {

                    this.myMapLayerDeviceAvg.Visibility = Visibility.Visible;
                    this.myMapLayerDevice.Visibility = Visibility.Collapsed;
                }

            };

        }

        MapLayer myMapLayerDevice = null;
        MapLayer myMapLayerDeviceAvg = null;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            map.ZoomLevel = 12;
            double weidu = (39.9487 + 39.90705 + 39.98698 + 39.96754 + 39.88405) / 5.0;
            double jindu = (116.45072 + 116.37995 + 116.36773 + 116.36932 + 116.33072) / 5.0;
            map.Center = new Location(weidu, jindu);
            myMapLayerDevice = new MapLayer();
            myMapLayerDeviceAvg = new MapLayer();

            map.Children.Add(myMapLayerDeviceAvg);
            map.Children.Add(myMapLayerDevice);

            pushPinDevice myPushPin0 = new pushPinDevice();
            myPushPin0.DevState = DeviceState.Normal;
            myPushPin0.DeviceName = "P0000";
            myPushPin0.DeviceTemp = "20℃";
            myMapLayerDevice.Children.Add(myPushPin0);
            MapLayer.SetPosition(myPushPin0, new Location(39.9487, 116.45072));
            myPushPin0.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);

            pushPinDevice myPushPin1 = new pushPinDevice();
            myPushPin1.DevState = DeviceState.Normal;
            myPushPin1.DeviceName = "P0001";
            myPushPin1.DeviceTemp = "21℃";
            myMapLayerDevice.Children.Add(myPushPin1);
            MapLayer.SetPosition(myPushPin1, new Location(39.90705, 116.37995));
            myPushPin1.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);

            pushPinDevice myPushPin2 = new pushPinDevice();
            myPushPin2.DevState = DeviceState.Normal;
            myPushPin2.DeviceName = "P0002";
            myPushPin2.DeviceTemp = "22℃";
            myMapLayerDevice.Children.Add(myPushPin2);
            myPushPin2.DevState = DeviceState.Escape;
            MapLayer.SetPosition(myPushPin2, new Location(39.98698, 116.36773));
            myPushPin2.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);

            pushPinDevice myPushPin3 = new pushPinDevice();
            myPushPin3.DevState = DeviceState.Normal;
            myPushPin3.DeviceName = "P0003";
            myPushPin3.DeviceTemp = "23℃";
            myMapLayerDevice.Children.Add(myPushPin3);
            MapLayer.SetPosition(myPushPin3, new Location(39.96754, 116.36932));
            myPushPin3.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);

            pushPinDevice myPushPin4 = new pushPinDevice();
            myPushPin4.DevState = DeviceState.Alert;
            myPushPin4.DeviceName = "P0004";
            myPushPin4.DeviceTemp = "24℃";
            myMapLayerDevice.Children.Add(myPushPin4);
            MapLayer.SetPosition(myPushPin4, new Location(39.92405, 116.33072));
            myPushPin4.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);


            pushPinDevice myPushPin6 = new pushPinDevice();
            myPushPin6.DevState = DeviceState.Alert;
            myPushPin6.DeviceName = "P0005";
            myPushPin6.DeviceTemp = "25℃";
            myMapLayerDevice.Children.Add(myPushPin6);
            MapLayer.SetPosition(myPushPin6, new Location(39.99205, 116.31072));
            myPushPin6.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);



            pushPinDevice myPushPin7 = new pushPinDevice();
            myPushPin7.DevState = DeviceState.Normal;
            myPushPin7.DeviceName = "P0007";
            myPushPin7.DeviceTemp = "23℃";
            myMapLayerDevice.Children.Add(myPushPin7);
            MapLayer.SetPosition(myPushPin7, new Location(39.97405, 116.39972));
            myPushPin7.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);

            pushPinDevice myPushPin8 = new pushPinDevice();
            myPushPin8.DevState = DeviceState.Normal;
            myPushPin8.DeviceName = "P0008";
            myPushPin8.DeviceTemp = "28℃";
            myMapLayerDevice.Children.Add(myPushPin8);
            MapLayer.SetPosition(myPushPin8, new Location(39.9687, 116.42072));
            myPushPin8.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);

            pushPinDevice myPushPin5 = new pushPinDevice();
            myPushPin5.DevState = DeviceState.Alert;
            myPushPin5.DeviceName = "片区1";
            myPushPin5.DeviceTemp = "65℃";
            myMapLayerDeviceAvg.Children.Add(myPushPin5);
            MapLayer.SetPosition(myPushPin5, new Location(39.92405, 116.33072));
            myPushPin5.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);
        }

        void myPushPin_onclickDetails(object sender, RoutedEventArgs e)
        {
            Storyboard1.Begin();
            //MyContent.Content = new DetailsPage();
            //MyContent.Title = "设备详细信息";
        }

        /// <summary>
        /// 图例折叠效果
        /// </summary>
        public bool flag = false;
        private void picbtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (flag)
            {
                this.showWeather.Begin();
            }
            else
            {
                this.hidWeather.Begin();

            }
            flag = !flag;
        }

        private void zhedie_Click(object sender, RoutedEventArgs e)
        {
            if (this.MainGrid.ColumnDefinitions[2].Width == new GridLength(240))
            {
                this.ZheDieStoryboardHidden.Begin();

                this.MainGrid.ColumnDefinitions[2].Width = new GridLength(0);
            }
            else
            {
                this.MainGrid.ColumnDefinitions[2].Width = new GridLength(240);
                this.ZheDieStoryboardShow.Begin();


            }
        }

    }
}
