using System;
using System.Collections.Generic;
namespace Scada.Model.Entity
{

    /// <summary>
    /// 类User。
    /// </summary>
    public class User
    {
        private Guid _userid;
        private string _loginid;
        private string _username;
        private Guid _deptid;
        /// <summary>
        /// 
        /// </summary>
        public Guid UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LoginID
        {
            set { _loginid = value; }
            get { return _loginid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
    }

    public enum LoginResultType
    {
        IP地址已锁定 = 0,
        密码错误 = 1,
        成功 = 2,
        账户已锁定 = 3,
        账户无效 = 4,
        IP与账户绑定错误 = 5,
        系统授权数超过登录数 = 6
    }

}

