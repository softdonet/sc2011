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
        public AlarmListViewModel()
        {
            DeviceRealTimeServiceClient deviceAlarmService = ServiceManager.GetDeviceRealTimeService();
            deviceAlarmService.GetAlarmDataReceived += new EventHandler<GetAlarmDataReceivedEventArgs>(deviceAlarmService_GetAlarmDataReceived);
            deviceAlarmService.GetAlarmDataListCompleted += (sender, e) => { };
            deviceAlarmService.GetAlarmDataListAsync();
        }



        void deviceAlarmService_GetAlarmDataReceived(object sender, GetAlarmDataReceivedEventArgs e)
        {
            if (e.Error == null)
            {
                List<DeviceAlarm> result = BinaryObjTransfer.BinaryDeserialize<List<DeviceAlarm>>(e.data);
                ObservableCollection<DeviceAlarmViewModel> davmList = new ObservableCollection<DeviceAlarmViewModel>();
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

        private ObservableCollection<DeviceAlarmViewModel> deviceAlarmList;
        public ObservableCollection<DeviceAlarmViewModel> DeviceAlarmList
        {
            get { return deviceAlarmList; }
            set
            {
                deviceAlarmList = value;
                this.RaisePropertyChanged("DeviceAlarmList");
            }
        }

        private ObservableCollection<DeviceAlarmViewModel> deviceAlarmListTop;
        public ObservableCollection<DeviceAlarmViewModel> DeviceAlarmListTop
        {
            get { return deviceAlarmListTop; }
            set
            {
                deviceAlarmListTop = value;
               // deviceAlarmListTop = deviceAlarmListTop.OrderBy(e => e.DeviceAlarm.StartTime).Take(4).ToList();
                deviceAlarmListTop = new ObservableCollection<DeviceAlarmViewModel>(deviceAlarmListTop.OrderBy(e => e.DeviceAlarm.StartTime).Take(4));
                this.RaisePropertyChanged("DeviceAlarmListTop");
            }
        }
    }
}


