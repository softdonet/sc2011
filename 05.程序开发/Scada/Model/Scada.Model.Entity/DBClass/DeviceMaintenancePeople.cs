using System;
using System.Collections.Generic;




namespace Scada.Model.Entity
{

    /// <summary>
    /// 设备关联维护人员信息
    /// </summary>
    public class DeviceMaintenancePeople
    {

        //标识
        private Guid _id;

        private MaintenancePeople _maintenanceInfo;

        //设备标识
        private Guid _deviceid;


        public Guid ID
        {
            set { _id = value; }
            get { return _id; }
        }

        public MaintenancePeople MaintenancePeopleInfo
        {
            set { _maintenanceInfo = value; }
            get { return _maintenanceInfo; }
        }

        public Guid DeviceID
        {
            set { _deviceid = value; }
            get { return _deviceid; }
        }

    }

}

