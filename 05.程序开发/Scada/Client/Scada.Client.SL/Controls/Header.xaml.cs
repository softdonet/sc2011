using System;
using System.Linq;
using System.Collections.Generic;


using System.Windows;
using System.Windows.Controls;


using Scada.Model.Entity;



namespace Scada.Client.SL.Controls
{


    //主界面共通部分
    public partial class Header : UserControl
    {

        #region 变量声明

        private List<MenuTree> _menuTree;
        private List<UserMenu> _userMenu;

        #endregion

        #region 构造函数

        public Header()
        {
            InitializeComponent();

            Application.Current.Host.Content.FullScreenChanged += new EventHandler(Content_FullScreenChanged);

        }


        #endregion

        #region 开放方法

        public void CurrentUserAuthorization(List<MenuTree> menuTree, List<UserMenu> userMenu)
        {
            this._menuTree = menuTree;
            this._userMenu = userMenu;
            this.SetMenuItemEnable();
        }

        #endregion

        #region 私有方法

        private void UnCheckAll()
        {

        }

        private void CheckUser()
        {

        }

        #endregion

        #region 事件处理

        #region 设置Silverlight全屏代码

        private void Content_FullScreenChanged(object sender, EventArgs e)
        {
            if (Application.Current.Host.Content.IsFullScreen)
            {
                btnFull.Content = "退出全屏";
                ToolTipService.SetToolTip(btnFull, "点击退出全屏");
            }
            else
            {
                btnFull.Content = "全屏模式";
                ToolTipService.SetToolTip(btnFull, "点击放大到全屏");
            }
        }

        private void btnFull_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Host.Content.IsFullScreen)
            {
                Application.Current.Host.Content.IsFullScreen = false;
                btnFull.Content = "全屏模式";
                ToolTipService.SetToolTip(btnFull, "点击放大到全屏");
            }
            else
            {
                Application.Current.Host.Content.IsFullScreen = true;
                btnFull.Content = "退出全屏";
                ToolTipService.SetToolTip(btnFull, "点击退出全屏");
            }
        }
        #endregion

        private void UnAllMenuEnable(Boolean isEnable)
        {

            this.menuMap.IsEnabled = isEnable;
            this.menuDeviceList.IsEnabled = isEnable;
            this.menuAlertList.IsEnabled = isEnable;
            this.menuUserEvent.IsEnabled = isEnable;
            this.menuSearch.IsEnabled = isEnable;
            this.menuCompare.IsEnabled = isEnable;
            this.menuSysSettings.IsEnabled = isEnable;

            this.childMenuDataSearch.IsEnabled = isEnable;
            this.childMenuAlertSearch.IsEnabled = isEnable;
            this.childMenuUserEventSearch.IsEnabled = isEnable;

            this.childMenuByDateCompare.IsEnabled = isEnable;
            this.childMenuDeviceCompare.IsEnabled = isEnable;

            this.childMenuDeviceManage.IsEnabled = isEnable;
            this.childMenuRepairUserManage.IsEnabled = isEnable;
            this.childMenuSysConfig.IsEnabled = isEnable;

            this.childMenuUserManage.IsEnabled = isEnable;
            this.childMenuManage.IsEnabled = isEnable;


        }

        private void MenuItemEnable(string menuKey)
        {
            if (menuKey == "menuMap")
                this.menuMap.IsEnabled = true;
            else if (menuKey == "menuDeviceList")
                this.menuDeviceList.IsEnabled = true;
            else if (menuKey == "menuAlertList")
                this.menuAlertList.IsEnabled = true;
            else if (menuKey == "menuUserEvent")
                this.menuUserEvent.IsEnabled = true;
            else if (menuKey == "menuSearch")
                this.menuSearch.IsEnabled = true;
            else if (menuKey == "menuCompare")
                this.menuCompare.IsEnabled = true;
            else if (menuKey == "menuSysSettings")
                this.menuSysSettings.IsEnabled = true;

            else if (menuKey == "childMenuDataSearch")
                this.childMenuDataSearch.IsEnabled = true;
            else if (menuKey == "childMenuAlertSearch")
                this.childMenuAlertSearch.IsEnabled = true;
            else if (menuKey == "childMenuUserEventSearch")
                this.childMenuUserEventSearch.IsEnabled = true;

            else if (menuKey == "childMenuByDateCompare")
                this.childMenuByDateCompare.IsEnabled = true;
            else if (menuKey == "childMenuDeviceCompare")
                this.childMenuDeviceCompare.IsEnabled = true;

            else if (menuKey == "childMenuDeviceManage")
                this.childMenuDeviceManage.IsEnabled = true;
            else if (menuKey == "childMenuRepairUserManage")
                this.childMenuRepairUserManage.IsEnabled = true;
            else if (menuKey == "childMenuSysConfig")
                this.childMenuSysConfig.IsEnabled = true;
            else if (menuKey == "childMenuUserManage")
                this.childMenuUserManage.IsEnabled = true;
            else if (menuKey == "childMenuManage")
                this.childMenuManage.IsEnabled = true;

        }

        private void SetMenuItemEnable()
        {

            //全部置灰
            this.UnAllMenuEnable(false);

            //授权置亮
            foreach (UserMenu item in this._userMenu)
            {
                MenuTree menuTree = this._menuTree.First(x => x.MenuId == item.MenuId);
                if (menuTree != null)
                {
                    this.MenuItemEnable(menuTree.Remark);
                }
            }
        }

        #endregion

    }
}
