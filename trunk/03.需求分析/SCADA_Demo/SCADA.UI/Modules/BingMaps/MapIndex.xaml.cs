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

namespace SCADA.UI.Modules.BingMaps
{
    public partial class MapIndex : UserControl
    {
        public MapIndex()
        {
            InitializeComponent();
            InitMap();
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
    }
}
