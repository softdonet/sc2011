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
using SCADA.UI.Modules.BingMaps;
using SCADA.UI.Modules.Device;
using SCADA.UI.Modules.Alarm;

namespace SCADA.UI
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            //初始页面加载地图
            this.ViewHost.Child = new  MapIndex();
            //注册导航点击事件
            Header.Secmenu.SelectionChanged += new SelectionChangedEventHandler(Secmenu_SelectionChanged);
            Header.menu1.Checked += new RoutedEventHandler(menu_Checked);
            Header.menu2.Checked += new RoutedEventHandler(menu_Checked);
            Header.menu3.Checked += new RoutedEventHandler(menu_Checked);
            Header.menu4.Checked += new RoutedEventHandler(menu_Checked);
            Header.menu5.Checked += new RoutedEventHandler(menu_Checked);
            Header.menu6.Checked += new RoutedEventHandler(menu_Checked);
            Header.menu7.Checked += new RoutedEventHandler(menu_Checked);
        }

        void menu_Checked(object sender, RoutedEventArgs e)
        {
            var rdo = sender as RadioButton;
            if (rdo != null)
            {
                switch (rdo.Name)
                {
                        //地图信息
                    case "menu1":
                        this.ViewHost.Child = new MapIndex();
                        break;
                        //设备列表
                    case "menu2":
                        this.ViewHost.Child = new DeviceList();
                        break;
                        //设备告警
                    case "menu3":
                        this.ViewHost.Child =new  AlarmList();
                        break;
                    case "menu4":
                       
                        break;
                    case "menu5":
                       
                        break;
                    case "menu6":
                       
                        break;
                    case "menu7":
                        
                        break;

                   
                }
            }
        }

        void Secmenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string link = (Header.Secmenu.SelectedItem as ListBoxItem).Tag.ToString().Trim();
            Header.menu2.IsChecked = true;
            switch (link)
            {
                case "SecMenu1":
                    this.ViewHost.Child = new MapIndex();
                    break;
                case "SecMenu2":
                    //this.ViewHost.Child = new CustomersIndex();
                    break;
                case "SecMenu3":
                    //this.ViewHost.Child = new ArrearChangeIndex();
                    break;
                case "SecMenu4":
                    //this.ViewHost.Child = new DepartmentView();
                    break;

                    //RadioButton
            }
        }
    }
}
