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
using Microsoft.Practices.Prism.Commands;
using Scada.Client.VM.ScadaDeviceService;
using Scada.Client.VM.CommClass;


namespace Scada.Client.VM.Modules.Query
{
    public class AlarmQueryViewModel : NotificationObject
    {
        #region Variable
        public DelegateCommand queryCommand { get; set; }
        ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;


        public List<DeviceAlarm> deviceAlarmQuery;
        #endregion


        #region Constructor

        public AlarmQueryViewModel()
        {
            this.queryCommand = new DelegateCommand(new Action(this.Query));
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            //scadaDeviceServiceSoapClient
        }
        #endregion

        public void Query()
        {
 
        }
        /// <summary>
        /// 选择树节点
        /// </summary>
        private DeviceTreeNode selectDeviceTreeNode;
        public DeviceTreeNode SelectDeviceTreeNode
        {
            get { return selectDeviceTreeNode; }
            set
            {
                selectDeviceTreeNode = value;
                this.RaisePropertyChanged("SelectDeviceTreeNode");
            }
        }

        /// <summary>
        /// 选择设备
        /// </summary>
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

        /// <summary>
        /// 开始时间
        /// </summary>
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                this.RaisePropertyChanged("StartDate");
            }
        }

        /// <summary>
        /// 结束日期
        /// </summary>
        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                this.RaisePropertyChanged("EndDate");
            }
        }



    }
}
