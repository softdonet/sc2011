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
using Scada.Client.VM.Modules.BingMaps;
using Scada.Model.Entity.Enums;
using Scada.Client.SL.Modules.Device;
using Scada.Client.VM.Modules.Alarm;
using Scada.Client.VM.Modules.UserEventVM;
using Scada.Model.Entity;
using Scada.Client.SL.Modules.UsersEvent;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Scada.Client.SL.Modules.BingMaps
{
    public partial class MapIndex : UserControl
    {
        private AlarmListViewModel alarmVM;
        private UserEventViewModel userEventViewModel;

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
            spPositionInfor.Visibility = App.SysGlobalPar.IsShowTool.GetValueOrDefault(false) ? Visibility.Visible : Visibility.Collapsed;
            InitMap();
            MyContent.CloseBtn += new EventHandler(MyContent_CloseBtn);

            myMapLayerDevice = new MapLayer();
            myMapLayerDeviceGroup = new MapLayer();
            myMapLayerDeviceArea = new MapLayer();
            map.Children.Add(myMapLayerDevice);
            map.Children.Add(myMapLayerDeviceGroup);
            map.Children.Add(myMapLayerDeviceArea);
            map.Children.Remove(this.spPositionInfor);
            map.Children.Add(this.spPositionInfor);

            mapVM = new MapIndexViewModel();
            mapVM.BaseDataResviceEvent += new EventHandler(mapVM_BaseDataResviceEvent);
            mapVM.RealTimeDataResviceEvent += new EventHandler(mapVM_RealTimeDataResviceEvent);

            // DeviceAlarmViewModel deviceAlarmViewModel = new DeviceAlarmViewModel();
            //RadGridViewAlarm.DataContext = deviceAlarmViewModel;
            alarmVM = new AlarmListViewModel();
            RadGridViewAlarm.DataContext = alarmVM;

            userEventViewModel = new UserEventViewModel();
            RadGridViewUserEvent.DataContext = userEventViewModel;
            map.ViewChangeOnFrame += new EventHandler<MapEventArgs>(map_ViewChangeOnFrame);
            map.MouseClick += new EventHandler<MapMouseEventArgs>(map_MouseClick);
        }

        void map_MouseClick(object sender, MapMouseEventArgs e)
        {
            Location loc = map.ViewportPointToLocation(e.ViewportPoint);
            this.txtlongitude.Text = loc.Longitude.ToString();
            this.txtLatitude.Text = loc.Latitude.ToString();
        }



        void map_ViewChangeOnFrame(object sender, MapEventArgs e)
        {
            this.lblZool.Content = "缩放：" + (int)this.map.ZoomLevel;
            this.lbllongitude.Content = "经度：" + this.map.Center.Longitude.ToString();
            this.lblLatitude.Content = "纬度：" + this.map.Center.Latitude.ToString();

        }

        /// <summary>
        /// 实时数据到达更新界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mapVM_RealTimeDataResviceEvent(object sender, EventArgs e)
        {
            foreach (var item in mapVM.DeviceRealTimeTree)
            {
                pushPinDevice pp;
                if (dicPin.TryGetValue(item.NodeKey, out pp))
                {
                    if (pp != null)
                    {
                        pp.DeviceTemp = (item.Temperature.HasValue ? ((int)(item.Temperature.Value)).ToString() : "0") + "℃";
                        pp.DevState = (DeviceStates)item.Status.GetValueOrDefault(1);
                        pp.DeviceName = item.NodeValue;
                    }
                }
            }
        }

        Dictionary<Guid, pushPinDevice> dicPin = new Dictionary<Guid, pushPinDevice>();
        /// <summary>
        /// 基础数据到达，界面描图钉
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mapVM_BaseDataResviceEvent(object sender, EventArgs e)
        {
            dicPin.Clear();
            foreach (var item in mapVM.DeviceRealTimeTree)
            {
                pushPinDevice myPushPin = new pushPinDevice();
                dicPin.Add(item.NodeKey, myPushPin);
                myPushPin.DataContext = item;
                if (item.Longitude != null || item.Dimensionality != null)
                {
                    MapLayer.SetPosition(myPushPin, new Location(item.Longitude.Value, item.Dimensionality.Value));
                }
                //设备详情单击事件
                myPushPin.onclickDetails += (sender1, e1) =>
                {
                    Storyboard1.Begin();
                    MyContent.Content = new DetailsPage(e1.ID);
                    MyContent.Title = "设备详细信息";
                };

                switch (item.NodeType)
                {
                    case 1:
                        myMapLayerDeviceArea.Children.Add(myPushPin);
                        break;
                    case 2:
                        myMapLayerDeviceGroup.Children.Add(myPushPin);
                        break;
                    case 3:
                        myMapLayerDevice.Children.Add(myPushPin);
                        break;
                    case 4:
                        break;
                    default:
                        break;
                }
            }
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
                    this.myMapLayerDeviceGroup.Visibility = Visibility.Collapsed;
                    this.myMapLayerDeviceArea.Visibility = Visibility.Collapsed;
                }
                //else if (map.ZoomLevel > 5)
                //{
                //    this.myMapLayerDeviceGroup.Visibility = Visibility.Visible;
                //    this.myMapLayerDevice.Visibility = Visibility.Collapsed;
                //    this.myMapLayerDeviceArea.Visibility = Visibility.Collapsed;
                //}
                else
                {
                    this.myMapLayerDeviceGroup.Visibility = Visibility.Visible;
                    this.myMapLayerDevice.Visibility = Visibility.Collapsed;
                    //this.myMapLayerDeviceArea.Visibility = Visibility.Visible;

                }
            };
        }

        MapLayer myMapLayerDevice = null;
        MapLayer myMapLayerDeviceGroup = null;
        MapLayer myMapLayerDeviceArea = null;
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            map.ZoomLevel = Convert.ToDouble(App.SysGlobalPar.DefaultZoomLevel);
            double weidu = Convert.ToDouble(App.SysGlobalPar.DefaultLatitude);
            double jindu = Convert.ToDouble(App.SysGlobalPar.DefaultLongitude);
            map.Center = new Location(weidu, jindu);
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

        private Guid id, deviceid;
        private int? state;


        private void hlBtnAlarm_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton hlB = sender as HyperlinkButton;
            //id = (hlB.DataContext as Scada.Model.Entity.DeviceAlarm).ID;
            //id = ((Scada.Client.VM.Modules.Alarm.DeviceAlarmViewModel)(hlB.DataContext)).DeviceAlarm.ID;
            id = ((Scada.Model.Entity.DeviceAlarm)(hlB.DataContext)).ID;

            RadWindow.Prompt("请输入备注：", new EventHandler<WindowClosedEventArgs>(OnClosed));
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            if (e.DialogResult != true) { return; }
            string getCommentInfo = e.PromptResult;
            Guid userGuid = App.CurUser.UserID;
            //   this._scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoAsync(id, DateTime.Now, getCommentInfo, userGuid);
            alarmVM.Pid = id;
            alarmVM.PdateTime = DateTime.Now;
            alarmVM.PgetCommentInfo = getCommentInfo;
            alarmVM.PuserGuid = userGuid;
            alarmVM.UpdateDeviceAlarmInfo();
            //var obj = alarmVM.DeviceAlarmList.SingleOrDefault(p => p.ID == id);//AlarmListVM.DeviceAlarmList.SingleOrDefault(x => x.DeviceAlarm.ID == id);
            //if (obj != null)
            //{
            //    obj.Comment = getCommentInfo;
            //}

            alarmVM.GetData();//重新绑定
        }


        private void hlBtnUserEvent_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton hlB = sender as HyperlinkButton;
            UserEventModel userEventModel = hlB.DataContext as UserEventModel;
            id = (hlB.DataContext as UserEventModel).ID;
            state = (hlB.DataContext as UserEventModel).State;
            Storyboard1.Begin();
            MyContent.Content = new UserEventProcess(userEventModel);
            MyContent.Title = "用户事件流程";
        }

        private void RadGridViewAlarm_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            if (e.Row is GridViewHeaderRow) return;

            //DeviceAlarmViewModel dav = e.Row.DataContext as DeviceAlarmViewModel;
            DeviceAlarm al = e.Row.DataContext as DeviceAlarm;
            HyperlinkButton HlBtn = (e.Row.Cells[RadGridViewAlarm.Columns.Count - 1].Content as FrameworkElement).FindName("hlBtnAlarm") as HyperlinkButton;

            if (string.IsNullOrEmpty(al.DealStatus))//if (string.IsNullOrEmpty(dav.DeviceAlarm.DealStatus))
            {
                HlBtn.IsEnabled = true;
            }
            //TextBlock state = (e.Row.Cells[RadGridViewAlarm.Columns.Count - 2].Content as FrameworkElement) as TextBlock;
            //HyperlinkButton HlBtn = (e.Row.Cells[RadGridViewAlarm.Columns.Count - 1].Content as FrameworkElement).FindName("hlBtnAlarm") as HyperlinkButton;
            //if (string.IsNullOrEmpty(state.Text.Trim()))
            //{
            //    HlBtn.IsEnabled = true;
            //}

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mapVM.UpdataConfig(Convert.ToDecimal(map.Center.Longitude), Convert.ToDecimal(map.Center.Latitude), (int)map.ZoomLevel);
        }
    }
}
