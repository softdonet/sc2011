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
           
           // login.btnLogin.Click += new RoutedEventHandler(btnLogin_Click);
            login.myKeyDowmEvent += new Login.myKeyDownHandle(login_myKeyDowmEvent);
        }

        void login_myKeyDowmEvent()
        {
            if (login.txbName.Text == "admin" && login.txtPassWord.Password == "admin")
            {
                this.MasterContainer.Child = new MainPage();
            }
        }
      
    }
}
