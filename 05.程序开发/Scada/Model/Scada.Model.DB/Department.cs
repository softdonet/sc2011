using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace Scada.Model.DB
{
    /// <summary>
    /// ��Department��
    /// </summary>
    public class Department
    {
        private Guid _id;
        private string _departmentname;
        private Guid _parentid;
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
        public string DepartmentName
        {
            set { _departmentname = value; }
            get { return _departmentname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
    }
}