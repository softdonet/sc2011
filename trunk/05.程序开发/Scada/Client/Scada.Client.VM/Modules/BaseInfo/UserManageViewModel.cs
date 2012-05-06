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

using Microsoft.Practices.Prism.Commands;
using System.Collections.Generic;
using Scada.Model.Entity;
using Microsoft.Practices.Prism.ViewModel;
using Scada.Utility.Common.SL.Enums;
using Scada.Model.Entity.Enums;
using Scada.Client.VM.ScadaDeviceService;
using Scada.Client.VM.CommClass;
using System.Linq;

namespace Scada.Client.VM.Modules.BaseInfo
{
    public class UserManageViewModel : NotificationObject
    {
        #region Variable

        public DelegateCommand AddCommand { get; set; } //1
        public DelegateCommand DeleteCommand { get; set; }//2
        public DelegateCommand UpdateCommand { get; set; }//3
        public DelegateCommand SearchCommand { get; set; }//4

        public DelegateCommand ResertPwdCommand { get; set; }//5

        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;

        bool flag;
        private static readonly int CommandID = 4;//表示执行的是查询命令
        #endregion

        #region Constructor
      
        public UserManageViewModel()
        {
            LoadUserStatus();

            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            //scadaDeviceServiceSoapClient.GetUserListAsync();
            //scadaDeviceServiceSoapClient.GetUserListCompleted += (sender, e) =>
            //    {
            //        string result = e.Result;
            //        if (e.Error == null)
            //        {
            //            UserList = BinaryObjTransfer.BinaryDeserialize<List<User>>(e.Result);
            //        }
            //    };

            scadaDeviceServiceSoapClient.GetUserListCompleted += new EventHandler<GetUserListCompletedEventArgs>(scadaDeviceServiceSoapClient_GetUserListCompleted);
            scadaDeviceServiceSoapClient.GetUserListAsync();

            scadaDeviceServiceSoapClient.CheckUserByLoginIDCompleted += new EventHandler<CheckUserByLoginIDCompletedEventArgs>(scadaDeviceServiceSoapClient_CheckUserByLoginIDCompleted);
            scadaDeviceServiceSoapClient.AddUserCompleted += new EventHandler<AddUserCompletedEventArgs>(scadaDeviceServiceSoapClient_AddUserCompleted);
            this.AddCommand = new DelegateCommand(new Action(this.AddUser));

            scadaDeviceServiceSoapClient.DelUserCompleted+=new EventHandler<DelUserCompletedEventArgs>(scadaDeviceServiceSoapClient_DelUserCompleted);
            this.DeleteCommand = new DelegateCommand(new Action(this.DelUser));

            scadaDeviceServiceSoapClient.UpdateUserCompleted += new EventHandler<UpdateUserCompletedEventArgs>(scadaDeviceServiceSoapClient_UpdateUserCompleted);
            this.UpdateCommand = new DelegateCommand(new Action(this.UpdateUser));

            scadaDeviceServiceSoapClient.SearchUserCompleted += new EventHandler<SearchUserCompletedEventArgs>(scadaDeviceServiceSoapClient_SearchUserCompleted);
            this.SearchCommand = new DelegateCommand(new Action(this.SearchUser));

            scadaDeviceServiceSoapClient.ResertUserPwdCompleted += new EventHandler<ResertUserPwdCompletedEventArgs>(scadaDeviceServiceSoapClient_ResertUserPwdCompleted);
            this.ResertPwdCommand = new DelegateCommand(new Action(ResertUserPwd));
        }
        #endregion


        #region Method And Event
        
        void scadaDeviceServiceSoapClient_SearchUserCompleted(object sender, SearchUserCompletedEventArgs e)
        {
            if (e.Error==null)
            {
                List<User> result = BinaryObjTransfer.BinaryDeserialize<List<User>>(e.Result);
                UserList = UserListChangeStatus(result);
            }
        }
        private void SearchUser()
        {
            if (UserList==null)
            {
                UserList = new List<User>();
                return;
            }
            scadaDeviceServiceSoapClient.SearchUserAsync(User.LoginID, User.UserName, User.Status);
        }


        void scadaDeviceServiceSoapClient_ResertUserPwdCompleted(object sender, ResertUserPwdCompletedEventArgs e)
        {
            bool flag = e.Result;
            if (flag)
            {
                MessageBox.Show("重置密码成功!");
                scadaDeviceServiceSoapClient.GetUserListAsync();//刷新列表
            }
            else
            {
                MessageBox.Show("重置密码失败!");
            }
        }
        private void ResertUserPwd()
        {
            scadaDeviceServiceSoapClient.ResertUserPwdAsync(User.UserID);
            scadaDeviceServiceSoapClient.GetUserListAsync();
        }
       

        void scadaDeviceServiceSoapClient_UpdateUserCompleted(object sender, UpdateUserCompletedEventArgs e)
        {
            bool flag = e.Result;
            if (flag)
            {
                MessageBox.Show("修改用户信息成功!");
                scadaDeviceServiceSoapClient.GetUserListAsync();//刷新列表
            }
            else
            {
                MessageBox.Show("修改用户信息失败!");
            }
        }
        private void UpdateUser()
        {
            if (!CheckInputTextValue()) return;
            string userInfo = BinaryObjTransfer.BinarySerialize(User);
            scadaDeviceServiceSoapClient.UpdateUserAsync(userInfo);
        }

        void scadaDeviceServiceSoapClient_DelUserCompleted(object sender, DelUserCompletedEventArgs e)
        {
            bool flag = e.Result;
            if (flag)
            {
                MessageBox.Show("删除用户信息成功!");
                scadaDeviceServiceSoapClient.GetUserListAsync();//刷新列表
            }
            else
            {
                MessageBox.Show("删除用户信息失败!");
            }
        }
        private void DelUser()
        {
            scadaDeviceServiceSoapClient.DelUserAsync(User.UserID);
        }

        void scadaDeviceServiceSoapClient_AddUserCompleted(object sender, AddUserCompletedEventArgs e)
        {
            bool flag = e.Result;
            if (flag)
            {
                MessageBox.Show("添加用户成功!");
                scadaDeviceServiceSoapClient.GetUserListAsync();//刷新列表
            }
            else
            {
                MessageBox.Show("添加用户失败!");
            }
        }
        private void AddUser()
        {
            if (!CheckInputTextValue()) return;

            scadaDeviceServiceSoapClient.CheckUserByLoginIDAsync(User.LoginID);
        }


        void scadaDeviceServiceSoapClient_CheckUserByLoginIDCompleted(object sender, CheckUserByLoginIDCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                flag = e.Result;
            }
            if (flag == true)
            {
                MessageBox.Show("该登录用户名称已存在，请输入其他的名字!");
                return;
            }
            else
            {
                //添加新用户
                User.UserID = Guid.NewGuid();
                string userInfo = BinaryObjTransfer.BinarySerialize(User);
                scadaDeviceServiceSoapClient.AddUserAsync(userInfo);
            }
        }


        

    

        void scadaDeviceServiceSoapClient_GetUserListCompleted(object sender, GetUserListCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                List<User> result  = BinaryObjTransfer.BinaryDeserialize<List<User>>(e.Result);
                UserList = UserListChangeStatus(result);
            }
        }

        #endregion


        #region 静态数据源

        private void LoadUserStatus()
        {
            List<User> UserTempt = new List<User>();
            UserTempt.Clear();
            UserTempt.Add(new User() { Status = 'A', StatusName = EnumHelper.Display<UserStatus>(1) });
            UserTempt.Add(new User() { Status = 'B', StatusName = EnumHelper.Display<UserStatus>(2) });
            UserStatus = UserTempt;
        }

        /// <summary>
        /// 把对应的字母转化为文字：A，启用 B 锁定
        /// </summary>
        /// <param name="UserTemp"></param>
        /// <returns></returns>
        private List<User> UserListChangeStatus(List<User> UserTemp)
        {
            foreach (User item in UserTemp)
            {
                switch (item.Status)
                {
                    case 'A':
                        item.StatusName = EnumHelper.Display<UserStatus>(1);
                        break;
                    case 'B':
                        item.StatusName = EnumHelper.Display<UserStatus>(2);
                        break;
                    default:
                        item.StatusName = "未知";
                        break;
                }
            }
            return UserTemp;
        }

        /// <summary>
        /// 检测界面输入不能为空
        /// </summary>
        /// <returns></returns>
        private bool CheckInputTextValue()
        {
            if (string.IsNullOrEmpty(User.LoginID))
            {
                MessageBox.Show("用户ID不能为空!");
                return false;
            }
            else if (User.Status == null)
            {
                MessageBox.Show("用户状态不能为空!");
                return false;
            }
            return true;
        }
        #endregion


        #region Property

        private List<User> userStatus;
        public List<User> UserStatus
        {
            get { return userStatus; }
            set
            {
                userStatus = value;
                this.RaisePropertyChanged("UserStatus");
                this.RaisePropertyChanged("Status");
            }
        }

        private List<User> userList;
        public List<User> UserList
        {
            get { return userList; }
            set
            {
                userList = value;
                this.RaisePropertyChanged("UserList");
            }
        }

        private User user;

        public User User
        {
            get { return user; }
            set
            {
                user = value;
                this.RaisePropertyChanged("User");
                SelectUserStatus = user;

                this.RaisePropertyChanged("LoginID");
                this.RaisePropertyChanged("UserName");
                this.RaisePropertyChanged("Status");
            }
        }


        private User selectUserStatus;

        public User SelectUserStatus
        {
            get
            {
                if (User != null)
                {
                    selectUserStatus = UserStatus.Where(e => e.Status == User.Status).SingleOrDefault();
                }
                return selectUserStatus;
            }
            set
            {
                selectUserStatus = value;
                this.RaisePropertyChanged("SelectUserStatus");
                if (User!=null&&selectUserStatus!=null)
                {
                    User.Status = selectUserStatus.Status;
                }
            }
        }

        public string LoginID
        {
            get
            {
                if (User==null)
                {
                    User = new User();
                }
                return User.LoginID;
            }
            set
            {
                if (CommandID != 4)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new Exception("用户ID不能为空!");
                    }
                }
                User.LoginID = value;
                this.RaisePropertyChanged("LoginID");
            }
        }

        public string UserName
        {
            get
            {
                if (User==null)
                {
                    User = new User();
                }
                return User.UserName;
            }
            set
            {
                User.UserName = value;
                this.RaisePropertyChanged("UserName");
            }
        }

        public char Status
        {
            get
            {
                if (User==null)
                {
                    User = new User();
                }
                return User.Status;
            }
            set
            {
                User.Status = value;
                this.RaisePropertyChanged("Status");
            }
        }

        #endregion

    }
}
