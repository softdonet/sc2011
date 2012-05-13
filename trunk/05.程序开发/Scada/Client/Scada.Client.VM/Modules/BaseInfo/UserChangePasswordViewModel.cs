using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Prism.Commands;
using Scada.Client.VM.ScadaDeviceService;
using Scada.Client.VM.CommClass;
using Scada.Model.Entity;
using Scada.Utility.Common.SL;

namespace Scada.Client.VM.Modules.BaseInfo
{
    public class UserChangePasswordViewModel : NotificationObject
    {
        public DelegateCommand ChangePasswordCommand { get; set; }

        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;

        public static bool FirstMd5 = false;
        string Md5Pwd = string.Empty;
        //private User CurrnUser;
        public UserChangePasswordViewModel(User myUser)
        {
            if (myUser!=null)
            {
                User = myUser;
            }
            else
            {
                MessageBox.Show("获取用户信息失败!");
                return;
            }
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            scadaDeviceServiceSoapClient.ChangePasswordCompleted += new EventHandler<ChangePasswordCompletedEventArgs>(scadaDeviceServiceSoapClient_ChangePasswordCompleted);
            this.ChangePasswordCommand = new DelegateCommand(new Action(this.ChangePassword));
        }

        void scadaDeviceServiceSoapClient_ChangePasswordCompleted(object sender, ChangePasswordCompletedEventArgs e)
        {
            bool flag = e.Result;
            if (flag)
            {
                MessageBox.Show("修改密码成功!");
                scadaDeviceServiceSoapClient.GetUserListAsync();//刷新列表
            }
            else
            {
                MessageBox.Show("修改密码失败!");
            }
        }


        public void ChangePassword()
        {
            if (!CheckPassword())
            {
                return;
            }
            string userValue = BinaryObjTransfer.BinarySerialize(User);
            scadaDeviceServiceSoapClient.ChangePasswordAsync(userValue);

        }
        /// <summary>
        /// 检查密码是否匹配：原始密码，新密码
        /// </summary>
        /// <returns></returns>
        private Boolean CheckPassword()
        {
           
            if (!string.IsNullOrEmpty(User.UserInputOldPassword))
            {
               
                //if (!FirstMd5)
                //{
                    //Md5Pwd = DecEncMd5Util.Hash(user.UserInputOldPassword);
                //    FirstMd5 = true;

                //}
                if (user.Password != Md5Pwd)
                {
                    MessageBox.Show("原始密码不正确!");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("原始密码不能为空!");
                return false;
            }
            if (user.NewPassword!=user.SurePassword)
            {
                MessageBox.Show("确认密码和新密码不一致,请重新输入!");
                //user.NewPassword = string.Empty;
                //user.SurePassword = string.Empty;
                return false;
            }
            return true;
        }

        #region 属性

        private User user;

        public User User
        {
            get { return user; }
            set
            {
                user = value;
                this.RaisePropertyChanged("User");
                this.RaisePropertyChanged("LoginID");
                this.RaisePropertyChanged("Password");
                this.RaisePropertyChanged("UserInputOldPassword");
                this.RaisePropertyChanged("NewPassword");
                this.RaisePropertyChanged("SurePassword");
            }
        }


        public string LoginID
        {
            get { return User.LoginID; }
            set { User.LoginID = value; }
        }

        public string Password
        {
            get { return User.Password; }
            set
            {
                User.Password = value;
                //this.RaisePropertyChanged("Password");
            }
        }

        public string UserInputOldPassword
        {

            get { return User.UserInputOldPassword; }
            set
            {
                User.UserInputOldPassword = value;
                //this.RaisePropertyChanged("UserInputOldPassword");
            }
        }
        public string NewPassword
        {

            get { return User.NewPassword; }
            set
            {
                User.NewPassword = value;
                //this.RaisePropertyChanged("NewPassword");
            }
        }

        public string SurePassword
        {

            get { return User.SurePassword; }
            set
            {
                User.SurePassword = value;
                //this.RaisePropertyChanged("SurePassword");
            }
        }

        #endregion
    }
}
