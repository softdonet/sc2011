using System;
using System.Text;
namespace Scada.Model.DB.SL
{
    /// <summary>
    /// ÀàDepartment¡£
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