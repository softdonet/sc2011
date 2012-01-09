using System;
using System.Collections.Generic;




namespace Scada.Model.Entity
{

    /// <summary>
    /// �豸����ά����Ա��Ϣ
    /// </summary>
    public class DeviceMaintenancePeople
    {

        //��ʶ
        private Guid _id;

        private MaintenancePeople _maintenanceInfo;

        //�豸��ʶ
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

