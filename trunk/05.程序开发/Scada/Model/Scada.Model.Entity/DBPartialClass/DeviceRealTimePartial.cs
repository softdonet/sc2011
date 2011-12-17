using System;
using System.Collections.Generic;
namespace Scada.Model.Entity
{

    /// <summary>
    /// 设备实行数据—扩展
    /// </summary>
    public partial class DeviceRealTime
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
