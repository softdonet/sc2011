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

namespace Scada.Client.VM.Modules.Alarm
{
    /// <summary>
    /// 数据实时列表：告警信息
    /// </summary>
    public class DeviceAlarmViewModel : NotificationObject
    {
        #region Variable

        private List<DeviceAlarm> deviceAlarm;

        #endregion

        #region Constructor

        public DeviceAlarmViewModel()
        {
            DeviceRealTimeServiceClient deviceAlarmService = ServiceManager.GetDeviceRealTimeService();
            deviceAlarmService.GetAlarmDataReceived += new EventHandler<GetAlarmDataReceivedEventArgs>(deviceAlarmService_GetAlarmDataReceived);
        }

        void deviceAlarmService_GetAlarmDataReceived(object sender, GetAlarmDataReceivedEventArgs e)
        {
            if (e.Error == null)
            {
                List<DeviceAlarm> result = BinaryObjTransfer.BinaryDeserialize<List<DeviceAlarm>>(e.data);
                DeviceAlarmList = result;
                DeviceAlarmListTop = result;
            }
            else
            {
                MessageBox.Show("获取实时信息失败!");
            }
        }

        private List<DeviceAlarm> deviceAlarmList;

        public List<DeviceAlarm> DeviceAlarmList
        {
            get { return deviceAlarmList; }
            set
            {
                deviceAlarmList = value;
                this.RaisePropertyChanged("DeviceAlarmList");
            }
        }

        private List<DeviceAlarm> deviceAlarmListTop;

        public List<DeviceAlarm> DeviceAlarmListTop
        {
            get { return deviceAlarmListTop; }
            set 
            { 
                deviceAlarmListTop = value;
                deviceAlarmListTop = deviceAlarmListTop.OrderBy(e => e.StartTime).Take(4).ToList();
                this.RaisePropertyChanged("DeviceAlarmListTop");
            }
        }

    }
        #endregion


}


