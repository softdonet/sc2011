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
using Scada.Client.VM.Modules.BingMaps;

namespace Scada.Client.SL.Modules.BingMaps
{
    public partial class MapIndex : UserControl
    {

        private static MapIndex instance;
        public static MapIndex GetInstance()
        {
            if (instance == null)
            {
                instance = new MapIndex();
            }
       
            return instance;
        }


        MapIndexViewModel mapVM = null;
        public MapIndex()
        {
            InitializeComponent();
            InitMap();
            MyContent.CloseBtn += new EventHandler(MyContent_CloseBtn);

            myMapLayerDevice = new MapLayer();
            myMapLayerDeviceAvg = new MapLayer();
            map.Children.Add(myMapLayerDeviceAvg);
            map.Children.Add(myMapLayerDevice);
            mapVM = new MapIndexViewModel();
            mapVM.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(mapVM_PropertyChanged);

            //    DeviceRealTimeServiceClient deviceRealTimeService = ServiceManager.GetDeviceRealTimeService();
            //    deviceRealTimeService.GetRealTimeDataReceived += new EventHandler<GetRealTimeDataReceivedEventArgs>(deviceRealTimeService_GetRealTimeDataReceived);
            //    deviceRealTimeService.GetAlarmDataReceived += new EventHandler<GetAlarmDataReceivedEventArgs>(deviceRealTimeService_GetAlarmDataReceived);
            //    deviceRealTimeService.GetCallDataReceived += new EventHandler<GetCallDataReceivedEventArgs>(deviceRealTimeService_GetCallDataReceived);
        }

        bool falg = false;
        void mapVM_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(falg)
            {
                return ;
            }

            if (e.PropertyName == "DeviceRealTimeTree")
            {
                foreach (var item in mapVM.DeviceRealTimeTree)
                {
                    pushPinDevice myPushPin = new pushPinDevice();
                    myPushPin.DataContext = item;
                    MapLayer.SetPosition(myPushPin, new Location(item.Longitude.Value, item.Dimensionality.Value));
                    myPushPin.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);
                    switch (item.NodeType)
                    {
                        case 1:
                            break;
                        case 2:
                            myMapLayerDeviceAvg.Children.Add(myPushPin);
                            break;
                        case 3:
                            myMapLayerDevice.Children.Add(myPushPin);
                            break;
                        default:
                            break;
                    }
                }
                falg = true;
            }
        }

        //void deviceRealTimeService_GetCallDataReceived(object sender, GetCallDataReceivedEventArgs e)
        //{
        //    this.txtCall.Text = e.data;
        //}

        //void deviceRealTimeService_GetAlarmDataReceived(object sender, GetAlarmDataReceivedEventArgs e)
        //{
        //    this.txtAlarm.Text = e.data;
        //}

        //void deviceRealTimeService_GetRealTimeDataReceived(object sender, GetRealTimeDataReceivedEventArgs e)
        //{
        //    //this.txtRealTime.Text = e.data;
        //}

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
