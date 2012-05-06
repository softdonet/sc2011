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
using Scada.Client.VM.Modules.BaseInfo;
using Scada.Model.Entity;

namespace Scada.Client.SL.Modules.BaseInfo
{
    public partial class UserManage : UserControl
    {
        UserManageViewModel userManageViewModel;
        public UserManage()
        {
            InitializeComponent();

            userManageViewModel = new UserManageViewModel();
            this.DataContext = userManageViewModel;
            //userManageViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(userManageViewModel_PropertyChanged);
        }

        void userManageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "UserList")
            {
                this.RadGridViewUser.ItemsSource = userManageViewModel.UserList;
            }
        }

        private void RadGridViewAlarm_RowActivated(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            this.userManageViewModel.User = e.Row.DataContext as User;
        }
    }
}
