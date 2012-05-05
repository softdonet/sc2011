using System;
using System.Linq;
using System.Collections.Generic;

using Telerik.Windows.Controls;
using System.Windows.Controls;


using Scada.Model.Entity;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.SystemManagerService;
using Scada.Client.SL.ScadaDeviceService;




namespace Scada.Client.SL.Modules.BaseInfo
{


    /// <summary>
    /// 用户与菜单权限管理
    /// </summary>
    public partial class UserMenuManage : UserControl
    {

        #region 变量声明

        private readonly ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient;

        private List<MenuTree> _menuTree;

        #endregion

        #region 构造函数

        public UserMenuManage()
        {

            InitializeComponent();


            //获取设备服务
            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();


            //获取菜单信息
            this._scadaDeviceServiceSoapClient.GetMenuTreeListCompleted
                  += new EventHandler<GetMenuTreeListCompletedEventArgs>
                      (scadaDeviceServiceSoapClient_GetMenuTreeListCompleted);
            this._scadaDeviceServiceSoapClient.GetMenuTreeListAsync();


            //获取用户信息
            this._scadaDeviceServiceSoapClient.GetUserListCompleted
                                     += new EventHandler<GetUserListCompletedEventArgs>
                                                (scadaDeviceServiceSoapClient_GetUserListCompleted);
            this._scadaDeviceServiceSoapClient.GetUserListAsync();

            //获取用户菜单权限
            this._scadaDeviceServiceSoapClient.GetUserMenuTreeListCompleted
                   += new EventHandler<GetUserMenuTreeListCompletedEventArgs>
                      (scadaDeviceServiceSoapClient_GetUserMenuTreeListCompleted);

        }

        #endregion

        #region 加载基础信息

        private void scadaDeviceServiceSoapClient_GetUserListCompleted(object sender, GetUserListCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string msgInfo = e.Result;
                List<User> devicTreeList = BinaryObjTransfer.BinaryDeserialize<List<User>>(msgInfo);
                this.lstUserInfo.ItemsSource = devicTreeList;

                this.lstUserInfo.SelectionChanged +=
                    new System.Windows.Controls.SelectionChangedEventHandler(this.lstUserInfo_SelectionChanged);
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");

        }

        private void scadaDeviceServiceSoapClient_GetMenuTreeListCompleted(object sender, GetMenuTreeListCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string msgInfo = e.Result;
                _menuTree = BinaryObjTransfer.BinaryDeserialize<List<MenuTree>>(msgInfo);
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        private void SetMenuTreeUnCheck(ItemCollection nodes)
        {
            foreach (RadTreeViewItem item in nodes)
            {
                item.CheckState = System.Windows.Automation.ToggleState.Off;
            }
        }

        private void SetMenuTreeCheck(ItemCollection nodes, string treeItemKey)
        {
            foreach (RadTreeViewItem item in nodes)
            {
                if (item.Name == treeItemKey)
                {
                    item.CheckState = System.Windows.Automation.ToggleState.On;
                }
                SetMenuTreeCheck(item.Items, treeItemKey);
            }
        }

        #endregion


        #region 刷新用户权限

        private void lstUserInfo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            object selObj = this.lstUserInfo.SelectedItem;
            if (!(selObj is User)) { return; }
            User seluser = selObj as User;
            this._scadaDeviceServiceSoapClient.GetUserMenuTreeListAsync(seluser.UserID.ToString());
        }

        private void scadaDeviceServiceSoapClient_GetUserMenuTreeListCompleted(object sender, GetUserMenuTreeListCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string msgInfo = e.Result;

                //UnCheck
                SetMenuTreeUnCheck(this.RadTreeView1.Items);
                //Check
                List<UserMenu> userMenuTree = BinaryObjTransfer.BinaryDeserialize<List<UserMenu>>(msgInfo);
                foreach (UserMenu item in userMenuTree)
                {
                    string treeItem = _menuTree.First(x => x.MenuId == item.MenuId).Remark;
                    SetMenuTreeCheck(this.RadTreeView1.Items, treeItem);
                }
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        #endregion


        #region 保存用户权限
        #endregion



    }
}
