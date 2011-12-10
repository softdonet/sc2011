using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace Scada.Model.DB
{
    /// <summary>
    /// ¿‡UserRole°£
    /// </summary>
    public class UserRole
    {
        private Guid _id;
        private Guid _userid;
        private Guid _roleid;
        /// <summary>
        /// 
        /// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }
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
        public Guid RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
    }
}

