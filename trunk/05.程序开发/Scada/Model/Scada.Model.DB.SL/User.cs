using System;
using System.Text;
namespace Scada.Model.DB.SL
{
    /// <summary>
    /// ��User��
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
}

