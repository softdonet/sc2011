using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.ViewModel;
using Scada.Client.VM.DeviceRealTimeService;
using Scada.Client.VM.ScadaDeviceService;
using Scada.Model.Entity;
using Scada.Client.VM.CommClass;
using System.Collections.ObjectModel;

namespace Scada.Client.VM.Modules.BaseInfo
{
    public class DeviceManageViewModel : NotificationObject
    {
        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;
        DeviceInfo deviceInfo;
        string myDeviceId;
        public DeviceManageViewModel()
        {
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            //scadaDeviceServiceSoapClient.ListDeviceTreeViewCompleted += new EventHandler<ListDeviceTreeViewCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            //scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();

            
        }

        public DeviceManageViewModel(string DeviceId)
        {
            myDeviceId = DeviceId;
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.ViewDeviceInfoCompleted += new EventHandler<ViewDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted);
            scadaDeviceServiceSoapClient.ViewDeviceInfoAsync(DeviceId);

        }


        #region 设备树

        void scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted(object sender, ListDeviceTreeViewCompletedEventArgs e)
        {
            if (e.Error==null)
            {
                ObservableCollection<DeviceTreeNode> result = BinaryObjTransfer.BinaryDeserialize<ObservableCollection<DeviceTreeNode>>(e.Result);
            }
        }

        private ObservableCollection<DeviceTreeNode> deviceTreeNodeList;

        public ObservableCollection<DeviceTreeNode> DeviceTreeNodeList
        {
            get { return deviceTreeNodeList; }
            set 
            { 
                deviceTreeNodeList = value;
                this.RaisePropertyChanged("DeviceTreeNodeList");
            }
        }

        #endregion

        void scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted(object sender, ViewDeviceInfoCompletedEventArgs e)
        {
            string result = e.Result;
            if (e.Error == null)
            {
                deviceInfo = BinaryObjTransfer.BinaryDeserialize<DeviceInfo>(e.Result);
                DeviceInfoList = deviceInfo;
            }
        }


        private DeviceInfo _deviceInfoList;

        public DeviceInfo DeviceInfoList
        {
            get { return _deviceInfoList; }
            set
            {
                _deviceInfoList = value;
                this.RaisePropertyChanged("DeviceInfoList");
            }
        }


    }
}
