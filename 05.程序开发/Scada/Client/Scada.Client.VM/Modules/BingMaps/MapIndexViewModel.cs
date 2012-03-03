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
using Scada.Client.VM.ScadaDeviceService;
using Scada.Model.Entity;
using Scada.Client.VM.CommClass;
using System.Collections.Generic;
using System.Linq;
using Scada.Client.VM.DeviceRealTimeService;
using Scada.Model.Entity.Enums;

namespace Scada.Client.VM.Modules.BingMaps
{
    public class MapIndexViewModel : NotificationObject
    {
        ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;

        /// <summary>
        /// 基础设备数据
        /// </summary>
        private List<PushPinDeviceViewModel> deviceRealTimeTree;
        public List<PushPinDeviceViewModel> DeviceRealTimeTree
        {
            get { return deviceRealTimeTree; }
            set
            {
                deviceRealTimeTree = value;
                this.RaisePropertyChanged("DeviceRealTimeTree");
            }
        }

        /// <summary>
        /// 登陆用户
        /// </summary>
        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                userName = value;
                this.RaisePropertyChanged("UserName");
            }
        }

        /// <summary>
        /// 登陆时间
        /// </summary>
        private DateTime loginTime;
        public DateTime LoginTime
        {
            get { return loginTime; }
            set
            {
                loginTime = value;
                this.RaisePropertyChanged("LoginTime");
            }
        }



        public MapIndexViewModel()
        {
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.ListDeviceTreeViewCompleted += new EventHandler<ListDeviceTreeViewCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();
            DeviceRealTimeServiceClient deviceRealTimeService = ServiceManager.GetDeviceRealTimeService();
            deviceRealTimeService.GetRealTimeDataReceived += new EventHandler<GetRealTimeDataReceivedEventArgs>(deviceRealTimeService_GetRealTimeDataReceived);
        }

        public event EventHandler RealTimeDataResviceEvent;
        public event EventHandler BaseDataResviceEvent;
        /// <summary>
        /// 实时数据到达事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void deviceRealTimeService_GetRealTimeDataReceived(object sender, GetRealTimeDataReceivedEventArgs e)
        {
            if (e.Error == null)
            {
                //获取实时数据
                List<DeviceRealTimeTree> result = BinaryObjTransfer.BinaryDeserialize<List<DeviceRealTimeTree>>(e.data);
                List<PushPinDeviceViewModel> tempList = new List<PushPinDeviceViewModel>();
                foreach (var item1 in result)
                {
                    foreach (var item2 in item1.NodeChild)
                    {
                        foreach (var item3 in item2.NodeChild)
                        {
                            PushPinDeviceViewModel tem = new PushPinDeviceViewModel();
                            tem.NodeKey = item3.NodeKey;
                            tem.NodeValue = item3.NodeValue;
                            tem.NodeType = item3.NodeType;
                            tem.Temperature = item3.Temperature;
                            tem.Status = item3.Status;
                            tem.Longitude = item3.Longitude;
                            tem.Dimensionality = item3.Dimensionality;
                            tem.InstallPlace = item3.InstallPlace;
                            tempList.Add(tem);
                        }
                    }
                }
                DeviceRealTimeTree = tempList;
                if (RealTimeDataResviceEvent != null)
                {
                    this.RealTimeDataResviceEvent(this, EventArgs.Empty);
                }
            }
            else
            {
                MessageBox.Show("获取实时数据失败");
            }
        }

        /// <summary>
        /// 获取基础设备数据
        /// </summary>
        void scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted(object sender, ListDeviceTreeViewCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //获取所有设备
                var result = BinaryObjTransfer.BinaryDeserialize<List<DeviceTreeNode>>(e.Result);
                List<PushPinDeviceViewModel> tempList = new List<PushPinDeviceViewModel>();
                foreach (var item1 in result)
                {
                    foreach (var item2 in item1.NodeChild)
                    {
                        foreach (var item3 in item2.NodeChild)
                        {
                            PushPinDeviceViewModel tem = new PushPinDeviceViewModel();
                            tem.NodeKey = item3.NodeKey;
                            tem.NodeValue = item3.NodeValue;
                            tem.NodeType = item3.NodeType;
                            tem.Temperature = 0;
                            tem.Status = (int)DeviceStates.Escape;
                            tem.Longitude = item3.Longitude;
                            tem.Dimensionality = item3.Dimensionality;
                            tem.InstallPlace = item3.InstallPlace;
                            if (tem.Longitude.HasValue && tem.Dimensionality.HasValue)
                            {
                                tempList.Add(tem);
                            }
                        }
                    }
                }
                DeviceRealTimeTree = tempList;
                if (BaseDataResviceEvent != null)
                {
                    this.BaseDataResviceEvent(this, EventArgs.Empty);
                }
            }
            else
            {
                MessageBox.Show("获取地图设备信息失败！");
            }
        }
    }
}
