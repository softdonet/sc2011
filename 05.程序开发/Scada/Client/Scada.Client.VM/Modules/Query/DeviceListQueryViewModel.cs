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
using Scada.Model.Entity;
using System.Collections.Generic;
using Scada.Client.VM.ScadaDeviceService;
using Microsoft.Practices.Prism.Commands;
using Scada.Client.VM.CommClass;

namespace Scada.Client.VM.Modules.Query
{
    /// <summary>
    /// 设备实时数据查询VM
    /// yanghk at 2012-2-14
    /// </summary>
    public class DeviceListQueryViewModel : NotificationObject
    {
        public DelegateCommand QueryCommand { get; set; }
        ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;

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
        /// 列表数据
        /// </summary>
        private List<DeviceRealTime> deviceRealTimeList;
        public List<DeviceRealTime> DeviceRealTimeList
        {
            get { return deviceRealTimeList; }
            set
            {
                deviceRealTimeList = value;
                this.RaisePropertyChanged("DeviceRealTimeList");
            }
        }

        /// <summary>
        /// 选择的设备
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
        /// 开始时间
        /// </summary>
        private DateTime? startDate;
        public DateTime? StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                if (value != null)
                {
                    startDate = new DateTime(Convert.ToDateTime(startDate).Year,
                                                Convert.ToDateTime(startDate).Month,
                                                 Convert.ToDateTime(startDate).Day, 0, 0, 0);
                }
                this.RaisePropertyChanged("StartDate");
            }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        private DateTime? endDate;
        public DateTime? EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                if (value != null)
                {
                    endDate = new DateTime(Convert.ToDateTime(endDate).Year,
                                            Convert.ToDateTime(endDate).Month,
                                            Convert.ToDateTime(endDate).Day, 23, 59, 59);
                }
                this.RaisePropertyChanged("EndDate");
            }
        }

        public DeviceListQueryViewModel()
        {
            this.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            this.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            this.QueryCommand = new DelegateCommand(new Action(this.Query));
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.GetListDeviceInfoCompleted += new EventHandler<GetListDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_GetListDeviceInfoCompleted);
            scadaDeviceServiceSoapClient.ListDeviceTreeViewCompleted += new EventHandler<ListDeviceTreeViewCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();
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

        void scadaDeviceServiceSoapClient_GetListDeviceInfoCompleted(object sender, GetListDeviceInfoCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                List<DeviceRealTime> result = BinaryObjTransfer.BinaryDeserialize<List<DeviceRealTime>>(e.Result);
                DeviceRealTimeList = result;
            }
            else
            {
                MessageBox.Show("获取数据失败！");
            }
        }

        public void Query()
        {
            if (SelectDeviceTreeNode == null || SelectDeviceTreeNode.NodeKey == Guid.Empty)
            {
                DeviceRealTimeList = new List<DeviceRealTime>();
                return;
            }
            scadaDeviceServiceSoapClient.GetListDeviceInfoAsync(SelectDeviceTreeNode.NodeKey, SelectDeviceTreeNode.NodeType, StartDate, EndDate);
        }
    }
}
