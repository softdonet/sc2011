using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;


using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;

using Microsoft.Practices.Prism.ViewModel;

using Scada.Model.Entity;
using Scada.Client.VM.CommClass;
using Scada.Client.VM.DeviceRealTimeService;





namespace Scada.Client.VM.Modules.Device
{


    /// <summary>
    /// 数据实时列表VM层
    /// </summary>
    public class DeviceListViewModel : NotificationObject
    {

        #region 变量声明

        private List<DeviceRealTimeTree> deviceRealTimeList;

        #endregion


        #region 构造函数

        public DeviceListViewModel()
        {
            DeviceRealTimeServiceClient deviceRealTimeService = ServiceManager.GetDeviceRealTimeService();
            deviceRealTimeService.GetRealTimeDataReceived += new EventHandler<GetRealTimeDataReceivedEventArgs>(deviceRealTimeService_GetRealTimeDataReceived);
            //主动获取数据
            deviceRealTimeService.GetRealTimeDataListCompleted += (sender, e) => { };
            deviceRealTimeService.GetRealTimeDataListAsync();
        }

        #endregion


        #region 开放方法

        public List<DeviceRealTimeTree> DeviceRealTimeList
        {
            get { return deviceRealTimeList; }
            set
            {
                deviceRealTimeList = value;
                this.RaisePropertyChanged("DeviceRealTimeList");
            }
        }

        #endregion


        #region 私有方法

        private void deviceRealTimeService_GetRealTimeDataReceived(object sender, GetRealTimeDataReceivedEventArgs e)
        {
            if (e.Error == null)
            {
                List<DeviceRealTimeTree> result = BinaryObjTransfer.BinaryDeserialize<List<DeviceRealTimeTree>>(e.data);
                //注意：此处必须赋值给属性，而非字段，才可实现 MVVM
                DeviceRealTimeList = result;
            }
            else
            {
                MessageBox.Show("获取实时数据失败");
            }
        }

        #endregion


    }
}
