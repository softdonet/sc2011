using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace Scada.Model.DB
{
    /// <summary>
    /// 类DeviceTree。
    /// </summary>
    public class DeviceTree
    {
        private Guid _id;
        private string _name;
        private int? _parentid;
        private Guid _adminid;
        /// <summary>
        /// Guid
        /// </summary>
        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 分区管理员
        /// </summary>
        public Guid AdminID
        {
            set { _adminid = value; }
            get { return _adminid; }
        }
    }
}

