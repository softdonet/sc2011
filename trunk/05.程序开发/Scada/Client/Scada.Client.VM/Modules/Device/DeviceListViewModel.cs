using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.ViewModel;
using System.Collections.Generic;
using Scada.Model.Entity;
using Scada.Client.VM.DeviceRealTimeService;
using System.Windows.Threading;
using System.Linq;
using Scada.Client.VM.CommClass;

namespace Scada.Client.VM.Modules.Device
{
    /// <summary>
    /// 数据实时列表VM层
    /// yanghk at 2012-2-12
    /// </summary>
    public class DeviceListViewModel : NotificationObject
    {
        public DeviceListViewModel()
        {
            DeviceRealTimeServiceClient deviceRealTimeService = ServiceManager.GetDeviceRealTimeService();
            deviceRealTimeService.GetRealTimeDataReceived += new EventHandler<GetRealTimeDataReceivedEventArgs>(deviceRealTimeService_GetRealTimeDataReceived);
        }

        private void deviceRealTimeService_GetRealTimeDataReceived(object sender, GetRealTimeDataReceivedEventArgs e)
        {
            if (e.Error == null)
            {
                List<DeviceRealTimeTree> result = BinaryObjTransfer.BinaryDeserialize<List<DeviceRealTimeTree>>(e.data);
                DeviceRealTimeList = result.OrderByDescending(d=>d.NodeValue).ToList();
            }
            else
            {
                MessageBox.Show("获取实时数据失败");
            }
        }

        private List<DeviceRealTimeTree> deviceRealTimeList;
        public List<DeviceRealTimeTree> DeviceRealTimeList
        {
            get { return deviceRealTimeList; }
            set
            {
                deviceRealTimeList = value;
                this.RaisePropertyChanged("DeviceRealTimeList");
            }
        }
    }
}
