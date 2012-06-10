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
using Microsoft.Practices.Prism.Commands;

namespace Scada.Client.VM.Modules.Alarm
{
    /// <summary>
    /// 数据实时列表：告警信息
    /// </summary>
    public class AlarmListViewModel : NotificationObject
    {
        DeviceRealTimeServiceClient deviceAlarmService = ServiceManager.GetDeviceRealTimeService();

        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;

        private DelegateCommand UpdateBatchCommand { get; set; }

        //当用户处理告警事件时，传回到数据库中的四个参数
        public Guid Pid { get; set; }
        public int Count { get; set; }
       public DateTime PdateTime { get; set; }
       public string PgetCommentInfo { get; set; }
       public Guid PuserGuid { get; set; }

       public AlarmListViewModel()
       {
           deviceAlarmService.GetAlarmDataReceived += new EventHandler<GetAlarmDataReceivedEventArgs>(deviceAlarmService_GetAlarmDataReceived);
           deviceAlarmService.GetAlarmDataListCompleted += (sender, e) => { };
           deviceAlarmService.GetAlarmDataListAsync();

           scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
           scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoCompleted += new EventHandler<UpdateDeviceAlarmInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoCompleted);
           if (Pid != new Guid() && PdateTime != null && !string.IsNullOrEmpty(PgetCommentInfo.Trim()) && PuserGuid != new Guid())
           {
               scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoAsync(Pid, PdateTime, PgetCommentInfo, PuserGuid);
           }
           scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoBatchCompleted += new EventHandler<UpdateDeviceAlarmInfoBatchCompletedEventArgs>(scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoBatchCompleted);
           this.UpdateBatchCommand = new DelegateCommand(new Action(this.UpdateBatch));
       }

       void scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoBatchCompleted(object sender, UpdateDeviceAlarmInfoBatchCompletedEventArgs e)
       {
           bool flag = e.Result;
           if (flag)
           {
               MessageBox.Show("批量处理告警成功!");
           }
           else
           {
               MessageBox.Show("批量处理失败,请重新操作!");
           }
       }
       private void UpdateBatch()
       {
           if (Count != null && PdateTime != null && PgetCommentInfo != null && PuserGuid != new Guid())
           {
               scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoBatchAsync(Count, PdateTime, PgetCommentInfo, PuserGuid);
           }
       }

        void scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoCompleted(object sender, UpdateDeviceAlarmInfoCompletedEventArgs e)
        {
            bool flag = e.Result;
            if (flag)
            {
               // MessageBox.Show("修改设备信息成功!");
            }
            else
            {
                MessageBox.Show("处理失败,请重新操作!");
            }
        }

        /// <summary>
        /// 手动获取数据
        /// </summary>
        public void GetData()
        {
            deviceAlarmService.GetAlarmDataListAsync();
        }

        public void UpdateDeviceAlarmInfo()
        {
            if (Pid != new Guid() && PdateTime != null && !string.IsNullOrEmpty(PgetCommentInfo) && PuserGuid != new Guid())
            {
                scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoAsync(Pid, PdateTime, PgetCommentInfo, PuserGuid);
            }
        }
        void deviceAlarmService_GetAlarmDataReceived(object sender, GetAlarmDataReceivedEventArgs e)
        {
            if (e.Error == null)
            {
                List<DeviceAlarm> result = BinaryObjTransfer.BinaryDeserialize<List<DeviceAlarm>>(e.data);
                //List<DeviceAlarmViewModel> davmList = new List<DeviceAlarmViewModel>();
                //foreach (var item in result)
                //{
                //    DeviceAlarmViewModel davm = new DeviceAlarmViewModel();
                //    davm.DeviceAlarm = item;
                //    davmList.Add(davm);
                //}
                //DeviceAlarmList = davmList;
                //DeviceAlarmListTop = davmList;

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
                //测试代码
                //deviceAlarmList = new List<DeviceAlarm>(deviceAlarmList.OrderByDescending(e => e.StartTime).Take(10));
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
                deviceAlarmListTop = new List<DeviceAlarm>(deviceAlarmListTop.OrderByDescending(e => e.StartTime).Take(4));
                this.RaisePropertyChanged("DeviceAlarmListTop");
            }
        }
    }
}


