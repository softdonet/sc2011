using System;
using System.Text;
namespace Scada.Model.DB.SL
{
    /// <summary>
    /// ¿‡DeviceMaintenancePeople°£
    /// </summary>
    public class DeviceMaintenancePeople
    {
        private Guid _id;
        private Guid _maintenanceid;
        private Guid _deviceid;
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
        public Guid MaintenanceID
        {
            set { _maintenanceid = value; }
            get { return _maintenanceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Guid DeviceID
        {
            set { _deviceid = value; }
            get { return _deviceid; }
        }
    }
}

