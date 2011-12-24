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
using Scada.Client.SL.Modules.BingMaps;
using Scada.Client.SL.Modules.Query;
using Scada.Client.SL.Modules.BaseInfo;
using Scada.Client.SL.Modules.Device;
using Scada.Client.SL.Modules.Alarm;

namespace Scada.Client.SL
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            //初始页面加载地图
            this.ViewHost.Child = new MapIndex();
            //注册主导航菜单点击事件
            Header.menuMap.Checked += new RoutedEventHandler(menu_Checked);
            Header.menuSearch.Checked += new RoutedEventHandler(menu_Checked);
            Header.menuCompare.Checked += new RoutedEventHandler(menu_Checked);
            Header.menuAlertList.Checked += new RoutedEventHandler(menu_Checked);
            // Header.menuStatistics.Checked += new RoutedEventHandler(menu_Checked);
            Header.menuSysSettings.Checked += new RoutedEventHandler(menu_Checked);
            Header.menuDeviceList.Checked += new RoutedEventHandler(menu_Checked);
            //注册子菜单点击事件
            Header.lstbCompare.SelectionChanged += new SelectionChangedEventHandler(lstbCompare_SelectionChanged);
            Header.lstbSysSettings.SelectionChanged += new SelectionChangedEventHandler(lstbSysSettings_SelectionChanged);
            Header.lstbSearch.SelectionChanged += new SelectionChangedEventHandler(lstbSearch_SelectionChanged);
        }
        DeviceList seviceList = new DeviceList();
        /// <summary>
        /// 主菜单选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void menu_Checked(object sender, RoutedEventArgs e)
        {
            var rdoButton = sender as RadioButton;
            if (rdoButton != null)
            {
                switch (rdoButton.Name)
                {
                    //地图信息
                    case "menuMap":
                        this.ViewHost.Child = new MapIndex();
                        break;
                    //设备列表
                    case "menuDeviceList":
                        this.ViewHost.Child = seviceList;
                        break;
                    //设备告警
                    case "menuAlertList":
                        this.ViewHost.Child = new AlarmList();
                        break;

                    ///统计分析移到对比分析的子菜单下

                    ////统计分析
                    //case "menuStatistics":
                    //    this.ViewHost.Child = new StatisticsAnalyse();

                    //    break;
                }
            }
        }

        #region 子菜单选择事件

        /// <summary>
        /// 查询子菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lstbSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string link = (Header.lstbSearch.SelectedItem as ListBoxItem).Name.ToString().Trim();
            Header.menuSearch.IsChecked = true;
            switch (link)
            {
                //数据分析
                case "childMenuDataSearch":
                    this.ViewHost.Child = new DeviceListQuery();
                    break;
            //    //系统告警日志
            //    case "childMenuAlertSearch":
            //        this.ViewHost.Child = new SCADA.UI.Modules.Query.QueryAlarm();
            //        break;
            //    //维护日志
            //    case "childMenuRepairLogSearch":
            //        this.ViewHost.Child = new SCADA.UI.Modules.Query.QueryLog();
            //        break;
            }
        }

        /// <summary>
        /// 系统配置子菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lstbSysSettings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string link = (Header.lstbSysSettings.SelectedItem as ListBoxItem).Name.ToString().Trim();
            Header.menuSysSettings.IsChecked = true;
            switch (link)
            {
                //设备信息管理
                case "childMenuDeviceManage":
                    this.ViewHost.Child = new DeviceManage();
                    break;
                //维护人员管理
                case "childMenuRepairUserManage":
                    this.ViewHost.Child = new RepairUserManage();
                    break;
                //系统参数配置
                case "childMenuSysConfig":
                    this.ViewHost.Child = new SysConfig();
                    break;

                //设备日志管理
                case "childMenuDevideLog":
                    this.ViewHost.Child = new DevideLog();
                    break;
                    //数据库备份管理
                case "childMenuDatabaseBak":
                    this.ViewHost.Child = new DatabaseBackUp();
                    break;
            }
        }

        /// <summary>
        /// 分析比较子菜单点击事件
        /// 图表分析子菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void lstbCompare_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string link = (Header.lstbCompare.SelectedItem as ListBoxItem).Name.ToString().Trim();
            //Header.menuCompare.IsChecked = true;
            //switch (link)
            //{
            //    //按日期对比
            //    case "childMenuByDateCompare":
            //        this.ViewHost.Child = new CompareByTime();
            //        break;
            //    //按设备对比
            //    case "childMenuDeviceCompare":
            //        this.ViewHost.Child = new CompareByDevice();

            //        break;
            //    //统计分析
            //    case "childMenuStatistics":
            //        this.ViewHost.Child = new StatisticsAnalyseNew();

            //        break;
            //}
        }

        #endregion
    }
}
