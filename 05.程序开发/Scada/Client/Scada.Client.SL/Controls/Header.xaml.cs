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

    }
}
