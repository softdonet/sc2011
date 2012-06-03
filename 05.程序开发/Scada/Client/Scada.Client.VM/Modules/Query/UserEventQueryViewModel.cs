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
using Microsoft.Practices.Prism.Commands;
using Scada.Client.VM.ScadaDeviceService;
using Scada.Model.Entity;
using System.Collections.Generic;
using Scada.Client.VM.CommClass;

namespace Scada.Client.VM.Modules.Query
{
    public class UserEventQueryViewModel : NotificationObject
    {
        #region Variable
        public DelegateCommand queryCommand { get; set; }
        ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;
        #endregion

        #region Constructor
        public UserEventQueryViewModel()
        {
            StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(-1);
            EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.ListDeviceTreeViewCompleted += new EventHandler<ListDeviceTreeViewCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();

            this.queryCommand = new DelegateCommand(this.Query);
            scadaDeviceServiceSoapClient.GetUserEventQueryInfoCompleted += new EventHandler<GetUserEventQueryInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_GetUserEventQueryInfoCompleted);


        }
        #endregion

        void scadaDeviceServiceSoapClient_GetUserEventQueryInfoCompleted(object sender, GetUserEventQueryInfoCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                List<UserEventModel> result = BinaryObjTransfer.BinaryDeserialize<List<UserEventModel>>(e.Result);
                UserEventModelList = result;
            }
            else
            {
                MessageBox.Show("获取数据失败！");
            }
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


        private void Query()
        {
            if (SelectDeviceTreeNode == null || SelectDeviceTreeNode.NodeKey == Guid.Empty)
            {
                UserEventModelList = new List<UserEventModel>();
                return;
            }
            scadaDeviceServiceSoapClient.GetUserEventQueryInfoAsync(SelectDeviceTreeNode.NodeKey, StartDate, EndDate);
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

        private List<UserEventModel> userEventModelList;
        public List<UserEventModel> UserEventModelList
        {
            get { return userEventModelList; }
            set
            {
                userEventModelList = value;
                this.RaisePropertyChanged("UserEventModelList");
            }
        }

        private DateTime startDate;

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0);
                this.RaisePropertyChanged("StartDate");
            }
        }
        private DateTime endDate;

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59);
            }
        }


    }
}
