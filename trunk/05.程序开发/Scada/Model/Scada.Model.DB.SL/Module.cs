using System;
using System.Text;
namespace Scada.Model.DB.SL
{
    /// <summary>
    /// ¿‡Module°£
    /// </summary>
    public class Module
    {
        private Guid _moduleid;
        private string _modulename;
        /// <summary>
        /// 
        /// </summary>
        public Guid ModuleID
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModuleName
        {
            set { _modulename = value; }
            get { return _modulename; }
        }
    }
}

