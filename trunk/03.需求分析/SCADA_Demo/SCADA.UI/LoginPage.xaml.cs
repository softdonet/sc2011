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
using System.Windows.Browser;
using SCADA.UI.Controls;
namespace SCADA.UI
{
    public partial class LoginPage : UserControl
    { 
        Login login;
        public LoginPage()
        {
            InitializeComponent();
            login = new Login();
            this.MasterContainer.Child = login;
            login.myKeyDownEvent += new RoutedEventHandler(login_myKeyDowmEvent);
        }

        void login_myKeyDowmEvent(object sender, RoutedEventArgs e)
        {
            if (login.txbName.Text == "admin" && login.txtPassWord.Password == "admin")
            {
                this.MasterContainer.Child = new MainPage();
            }
            else
            {
                MessageBox.Show("用户名或密码错误!，请重新输入");
                login.txbName.Text = string.Empty;
                login.txtPassWord.Password = string.Empty;
                return;
            }
        }
    }
}
