﻿using System;
using System.Linq;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Controls;


using Scada.Client.SL.Controls;
using Scada.Client.SL.Modules.BingMaps;
using Scada.Client.SL.Modules.Query;
using Scada.Client.SL.Modules.BaseInfo;
using Scada.Client.SL.Modules.Device;
using Scada.Client.SL.Modules.Alarm;
using Scada.Client.SL.Modules.UsersEvent;
using Scada.Client.SL.Modules.DiagramAnalysis;
using Scada.Client.SL.ScadaDeviceService;
using Scada.Client.SL.CommClass;
using Scada.Model.Entity;
using Scada.Client.SL.SystemManagerService;



namespace Scada.Client.SL
{

    /// <summary>
    /// 主界面管理
    /// </summary>
    public partial class MainPage : UserControl
    {

        #region 变量声明

        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient;
     
        #endregion


        #region 构造函数

        public MainPage()
        {

            InitializeComponent();
            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            this._scadaDeviceServiceSoapClient.GetUserMenuTreeListCompleted +=
                                            new EventHandler<GetUserMenuTreeListCompletedEventArgs>(scadaDeviceServiceSoapClient_GetUserMenuTreeListCompleted);

            //默认管理员全部权限
            if (App.CurUser.LoginID != "admin")
            {
                //加载权限
                this._scadaDeviceServiceSoapClient.GetUserMenuTreeListAsync(App.CurUser.UserID.ToString());
            }

            //加载菜单
            this.InitMenu();
            if (App.CurUser != null)
            {
                Header.txtLoginContent.Text = "欢迎 " + App.CurUser.LoginID.ToString() + "(" + App.CurUser.UserName + ")" + " 登录";
            }


        }

        private void InitMenu()
        {

            //初始页面加载地图
            this.ViewHost.Child = MapIndex.GetInstance();

            //注册主导航菜单点击事件
            Header.menuMap.Checked += new RoutedEventHandler(menu_Checked);
            Header.menuSearch.Checked += new RoutedEventHandler(menu_Checked);
            Header.menuCompare.Checked += new RoutedEventHandler(menu_Checked);
            Header.menuAlertList.Checked += new RoutedEventHandler(menu_Checked);
            Header.menuUserEvent.Checked += new RoutedEventHandler(menu_Checked);
            Header.menuSysSettings.Checked += new RoutedEventHandler(menu_Checked);
            Header.menuDeviceList.Checked += new RoutedEventHandler(menu_Checked);

            //注册子菜单点击事件
            Header.lstbCompare.SelectionChanged += new SelectionChangedEventHandler(lstbCompare_SelectionChanged);
            Header.lstbSysSettings.SelectionChanged += new SelectionChangedEventHandler(lstbSysSettings_SelectionChanged);
            Header.lstbSearch.SelectionChanged += new SelectionChangedEventHandler(lstbSearch_SelectionChanged);
        }

        private void scadaDeviceServiceSoapClient_GetUserMenuTreeListCompleted(object sender, GetUserMenuTreeListCompletedEventArgs e)
        {

            if (e.Error == null)
            {
                string msgInfo = e.Result;
                List<UserMenu> userMenu = BinaryObjTransfer.BinaryDeserialize<List<UserMenu>>(msgInfo);
                if (userMenu.Count() == 0) { return; }
                this.Header.CurrentUserAuthorization(App.CurrMenu, userMenu);
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        #endregion


        #region 主菜单选择事件

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
                        this.ViewHost.Child = MapIndex.GetInstance();
                        break;
                    //设备列表
                    case "menuDeviceList":
                        this.ViewHost.Child = DeviceList.GetInstance();
                        break;
                    //设备告警
                    case "menuAlertList":
                        this.ViewHost.Child = AlarmList.GetInstance();
                        break;
                    //用户事件
                    case "menuUserEvent":
                        this.ViewHost.Child = UserEvent.GetInstance();
                        break;

                }
            }
        }

        #endregion


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
                //设备查询
                case "childMenuDataSearch":
                    this.ViewHost.Child = DeviceListQuery.GetInstance(); ;
                    break;
                //系统告警日志
                case "childMenuAlertSearch":
                    this.ViewHost.Child = AlarmQuery.GetInstance();
                    break;
                //用户事件查询
                case "childMenuUserEventSearch":
                    this.ViewHost.Child = UserEventQuery.GetInstance();
                    break;
                //维护日志
                case "childMenuRepairLogSearch":
                    this.ViewHost.Child = new LogQuery();
                    break;
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

                ////设备日志管理
                //case "childMenuDevideLog":
                //    this.ViewHost.Child = new DevideLog();
                //    break;
                ////数据库备份管理
                //case "childMenuDatabaseBak":
                //    this.ViewHost.Child = new DatabaseBackUp();
                //    break;

                //用户权限管理
                case "childMenuUserManage":
                    this.ViewHost.Child = new UserManage();
                    break;
                //菜单管理
                case "childMenuManage":
                    this.ViewHost.Child = new UserMenuManage();
                    break;

                //修改密码
                case "childMenuUserChangePassword":
                    this.ViewHost.Child = new UserChangePassword();
                    break;
                //广播信息
                case "BroadcastInfoManage":
                    this.ViewHost.Child = new BroadcastInfoManage();
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
            string link = (Header.lstbCompare.SelectedItem as ListBoxItem).Name.ToString().Trim();
            Header.menuCompare.IsChecked = true;
            switch (link)
            {
                //按日期对比
                case "childMenuByDateCompare":
                    this.ViewHost.Child = CompareByDevice.GetInstance();
                    break;
                //按设备对比
                case "childMenuDeviceCompare":
                    this.ViewHost.Child = CompareByTime.GetInstance();

                    break;
                //统计分析
                case "childMenuStatistics":
                    // this.ViewHost.Child =new  SCADA.UI.Modules.Statistics.StatisticsAnalyseNew();//StatisticsAnalyse.GetInstance();

                    break;
            }
        }

        #endregion


    }
}
