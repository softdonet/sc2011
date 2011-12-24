using System;
using System.Linq;
using System.Collections.Generic;


using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Browser;
using System.Windows.Media.Animation;


using Scada.Client.SL.Controls;
using Scada.Client.SL.CommClass;




namespace Scada.Client.SL
{

    /// <summary>
    /// 登录界面
    /// </summary>
    public partial class LoginPage : UserControl
    {

        #region 变量声明

        private Login login;

        #endregion

        #region 构造函数

        public LoginPage()
        {
            InitializeComponent();
            login = new Login();
            this.MasterContainer.Child = login;
            login.myKeyDownEvent += new RoutedEventHandler(login_myKeyDowmEvent);
        }

        #endregion

        #region 事件处理

        private void login_myKeyDowmEvent(object sender, RoutedEventArgs e)
        {

            //1)Check UserName
            if (string.IsNullOrEmpty(login.txbName.Text))
            {
                ScadaMessageBox.ShowWarnMessage("用户名不能为空！", "用户登录信息");
                this.login.txbName.Focus();
                return;
            }

            //2)Check PassWord
            if (string.IsNullOrEmpty(login.txtPassWord.Password))
            {
                ScadaMessageBox.ShowWarnMessage("密码不能为空！", "用户登录信息");
                this.login.txtPassWord.Focus();
                return;
            }

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

        #endregion


    }

}
