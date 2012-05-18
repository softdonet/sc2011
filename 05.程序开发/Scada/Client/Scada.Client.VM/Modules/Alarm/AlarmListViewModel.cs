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
using Scada.Client.VM.ScadaDeviceService;
using Scada.Client.VM.CommClass;
using Scada.Client.VM.DeviceRealTimeService;
using System.Linq;
using System.Collections.ObjectModel;

namespace Scada.Client.VM.Modules.Alarm
{
    /// <summary>
    /// 数据实时列表：告警信息
    /// </summary>
    public class AlarmListViewModel : NotificationObject
    {
        DeviceRealTimeServiceClient deviceAlarmService = ServiceManager.GetDeviceRealTimeService();
        public AlarmListViewModel()
        {
            deviceAlarmService.GetAlarmDataReceived += new EventHandler<GetAlarmDataReceivedEventArgs>(deviceAlarmService_GetAlarmDataReceived);
            deviceAlarmService.GetAlarmDataListCompleted += (sender, e) => { };
            deviceAlarmService.GetAlarmDataListAsync();
        }

        /// <summary>
        /// 手动获取数据
        /// </summary>
        public void GetData()
        {
            deviceAlarmService.GetAlarmDataListAsync();
        }

        void deviceAlarmService_GetAlarmDataReceived(object sender, GetAlarmDataReceivedEventArgs e)
        {
            if (e.Error == null)
            {
                List<DeviceAlarm> result = BinaryObjTransfer.BinaryDeserialize<List<DeviceAlarm>>(e.data);
                List<DeviceAlarmViewModel> davmList = new List<DeviceAlarmViewModel>();
                foreach (var item in result)
                {
                    DeviceAlarmViewModel davm = new DeviceAlarmViewModel();
                    davm.DeviceAlarm = item;
                    davmList.Add(davm);
                }
                DeviceAlarmList = davmList;
                DeviceAlarmListTop = davmList;
            }
            else
            {
                MessageBox.Show("获取实时信息失败!");
            }
        }

        private List<DeviceAlarmViewModel> deviceAlarmList;
        public List<DeviceAlarmViewModel> DeviceAlarmList
        {
            get { return deviceAlarmList; }
            set
            {
                deviceAlarmList = value;
                this.RaisePropertyChanged("DeviceAlarmList");
            }
        }

        private List<DeviceAlarmViewModel> deviceAlarmListTop;
        public List<DeviceAlarmViewModel> DeviceAlarmListTop
        {
            get { return deviceAlarmListTop; }
            set
            {
                deviceAlarmListTop = value;
                deviceAlarmListTop = new List<DeviceAlarmViewModel>(deviceAlarmListTop.OrderBy(e => e.DeviceAlarm.StartTime).Take(4));
                this.RaisePropertyChanged("DeviceAlarmListTop");
            }
        }
    }
}


