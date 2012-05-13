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

namespace Scada.Client.SL.Modules.BaseInfo
{
    public partial class UserChangePassword : UserControl
    {
        public UserChangePassword()
        {
            InitializeComponent();

            UserChangePasswordViewModel userChgPwdVM = new UserChangePasswordViewModel(App.CurUser);
            this.DataContext = userChgPwdVM;
           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            txtOPassword.Password = string.Empty;
            txtNewPassword.Password = string.Empty;
            txtSurePassWords.Password = string.Empty;
        }
    }
}
