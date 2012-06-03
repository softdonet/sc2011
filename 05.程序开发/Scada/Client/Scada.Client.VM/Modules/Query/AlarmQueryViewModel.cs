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

        #endregion


        #region Constructor

        public AlarmQueryViewModel()
        {
            this.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(-1);
            this.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.ListDeviceTreeViewCompleted += new EventHandler<ListDeviceTreeViewCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();

            this.queryCommand = new DelegateCommand(new Action(this.Query));
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.GetAlarmQueryInfoCompleted += new EventHandler<GetAlarmQueryInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_GetAlarmQueryInfoCompleted);
        }

        void scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted(object sender, ListDeviceTreeViewCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var result = BinaryObjTransfer.BinaryDeserialize<List<DeviceTreeNode>>(e.Result);
                DeviceTreeNode temp = new DeviceTreeNode();
                temp.NodeKey = Guid.Empty;
                temp.NodeType = -1;
                temp.NodeValue = "清空选择";
                result.Insert(0, temp);
                DeviceTreeSource = result;
            }
            else
            {
                MessageBox.Show("获取数据失败！");
            }
        }

        void scadaDeviceServiceSoapClient_GetAlarmQueryInfoCompleted(object sender, GetAlarmQueryInfoCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                List<DeviceAlarm> result = BinaryObjTransfer.BinaryDeserialize<List<DeviceAlarm>>(e.Result);
                DeviceAlarmList = result;
            }
            else
            {
                MessageBox.Show("获取数据失败！");
            }
        }
        #endregion

        public void Query()
        {
            if (SelectDeviceTreeNode == null || SelectDeviceTreeNode.NodeKey == Guid.Empty)
            {
                DeviceAlarmList = new List<DeviceAlarm>();
                return;
            }
            scadaDeviceServiceSoapClient.GetAlarmQueryInfoAsync(SelectDeviceTreeNode.NodeKey, SelectDeviceTreeNode.NodeType, StartDate, EndDate);
        }

        /// <summary>
        /// 设备树数据源
        /// </summary>
        private List<DeviceTreeNode> deviceTreeSource;
        public List<DeviceTreeNode> DeviceTreeSource
        {
            get { return deviceTreeSource; }
            set
            {
                deviceTreeSource = value;
                this.RaisePropertyChanged("DeviceTreeSource");
            }
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
                endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
                this.RaisePropertyChanged("EndDate");
            }
        }



    }
}
