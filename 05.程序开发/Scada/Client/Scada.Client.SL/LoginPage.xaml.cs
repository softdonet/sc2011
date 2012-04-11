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


using Scada.Model.Entity;
using Scada.Client.SL.Controls;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;
using Scada.Model.Entity.Common;




namespace Scada.Client.SL
{

    /// <summary>
    /// 登录界面
    /// </summary>
    public partial class LoginPage : UserControl
    {

        #region 变量声明

        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient;

        private Login login;

        #endregion

        #region 构造函数

        public LoginPage()
        {
            InitializeComponent();
            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            this._scadaDeviceServiceSoapClient.LogInCompleted += new EventHandler<LogInCompletedEventArgs>(scadaDeviceServiceSoapClient_LogInCompleted);

            login = new Login();
            this.MasterContainer.Child = login;
            login.myKeyDownEvent += new RoutedEventHandler(login_myKeyDowmEvent);
        }

        #endregion

        #region 事件处理


        private void scadaDeviceServiceSoapClient_LogInCompleted(object sender, LogInCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string msgInfo = e.Result;
                LoginResult loginResult = BinaryObjTransfer.BinaryDeserialize<LoginResult>(msgInfo);

                if (loginResult.loginResultType == LoginResultType.成功)
                {
                    App.CurUser = loginResult.sysUser;
                    this.MasterContainer.Child = new MainPage();
                }
                else if (loginResult.loginResultType == LoginResultType.密码错误)
                {
                    ScadaMessageBox.ShowWarnMessage("密码错误！", "警告信息");
                    login.txtPassWord.Password = string.Empty;
                    login.txtPassWord.Focus();
                }
                else if (loginResult.loginResultType == LoginResultType.账户无效)
                {
                    ScadaMessageBox.ShowWarnMessage("系统无此账户！", "警告信息");
                    login.txbName.Text = string.Empty;
                    login.txtPassWord.Password = string.Empty;
                }
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

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

            this._scadaDeviceServiceSoapClient.LogInAsync(login.txbName.Text,
                                                            login.txtPassWord.Password,
                                                                "127.0.0.1");

        }

        #endregion


    }

}
