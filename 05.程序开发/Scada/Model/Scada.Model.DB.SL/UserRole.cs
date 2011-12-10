using System;
using System.Text;
namespace Scada.Model.DB.SL
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

