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
using SCADA.UI.Modules.Device;

namespace SCADA.UI.Modules.BingMaps
{
    public partial class MapIndex : UserControl
    {
        public MapIndex()
        {
            InitializeComponent();
            InitMap();
            MyContent.CloseBtn += new EventHandler(MyContent_CloseBtn);
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
        }

        MapLayer myMapLayerDevice = null;
        MapLayer myMapLayerDeviceAvg = null;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            map.ZoomLevel = 10;
            map.Center = new Location(34.27, 108.95);
            myMapLayerDevice = new MapLayer();
            myMapLayerDeviceAvg = new MapLayer();
            map.Children.Add(myMapLayerDevice);
            map.Children.Add(myMapLayerDeviceAvg);

            pushPinDevice myPushPin0 = new pushPinDevice();
            myPushPin0.DevState = DeviceState.Escape;
            myPushPin0.DeviceName = "P0000";
            myPushPin0.DeviceTemp = "29℃";
            myMapLayerDevice.Children.Add(myPushPin0);
            MapLayer.SetPosition(myPushPin0, new Location(34.27, 108.95));
            myPushPin0.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);

            pushPinDevice myPushPin1 = new pushPinDevice();
            myPushPin1.DevState = DeviceState.Normal;
            myPushPin1.DeviceName = "P0001";
            myPushPin1.DeviceTemp = "25℃";
            myMapLayerDevice.Children.Add(myPushPin1);
            MapLayer.SetPosition(myPushPin1, new Location(34.37, 108.11));
            myPushPin1.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);

            pushPinDevice myPushPin2 = new pushPinDevice();
            myPushPin2.DevState = DeviceState.Normal;
            myPushPin2.DeviceName = "P0002";
            myPushPin2.DeviceTemp = "45℃";
            myMapLayerDevice.Children.Add(myPushPin2);
            myPushPin2.DevState = DeviceState.Escape;
            MapLayer.SetPosition(myPushPin2, new Location(34.25, 108.23));
            myPushPin2.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);

            pushPinDevice myPushPin3= new pushPinDevice();
            myPushPin3.DevState = DeviceState.Escape;
            myPushPin3.DeviceName = "P0003";
            myPushPin3.DeviceTemp = "41℃";
            myMapLayerDevice.Children.Add(myPushPin3);
            MapLayer.SetPosition(myPushPin3, new Location(34.10, 108.35));
            myPushPin3.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);

            pushPinDevice myPushPin4 = new pushPinDevice();
            myPushPin4.DevState = DeviceState.Alert;
            myPushPin4.DeviceName = "P0004";
            myPushPin4.DeviceTemp = "65℃";
            myMapLayerDevice.Children.Add(myPushPin4);
            MapLayer.SetPosition(myPushPin4, new Location(34.09, 108.65));
            myPushPin4.onclickDetails += new RoutedEventHandler(myPushPin_onclickDetails);
        }

        void myPushPin_onclickDetails(object sender, RoutedEventArgs e)
        {
            Storyboard1.Begin();
            MyContent.Content = new DetailsPage();
            MyContent.Title = "设备详细信息";
        }

        /// <summary>
        /// 图例折叠效果
        /// </summary>
        public bool flag = false;
        private void picbtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //if (!flag)
            //{
            //    this.PicHiddenStoryboard.Begin();
            //}
            //else
            //{
            //    this.PicShowStoryboard.Begin();

            //}
            //flag = !flag;
        }

    }
}
