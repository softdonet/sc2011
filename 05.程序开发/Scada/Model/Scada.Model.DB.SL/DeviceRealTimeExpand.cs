using System;


namespace Scada.Model.DB.SL
{


    public partial class DeviceRealTimeExpand
    {

        //设备编号
        private string _deviceno;

        //安装位置
        private string _installplace;


        public string DeviceNo
        {
            set { _deviceno = value; }
            get { return _deviceno; }
        }

        public string InstallPlace
        {
            set { _installplace = value; }
            get { return _installplace; }
        }


    }

}
