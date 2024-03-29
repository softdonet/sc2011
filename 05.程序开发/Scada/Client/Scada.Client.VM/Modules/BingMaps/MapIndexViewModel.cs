﻿using System;
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
using Scada.Client.VM.SystemManagerService;

namespace Scada.Client.VM.Modules.BingMaps
{
    public class MapIndexViewModel : NotificationObject
    {
        ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;
        public SystemGlobalParameter _sysGlobalPar;
        private SystemManagerServiceSoapClient _systemManagerServiceSoapClient;
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

        DeviceRealTimeServiceClient deviceRealTimeService = null;

        public MapIndexViewModel()
        {
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.ListDeviceTreeViewCompleted += new EventHandler<ListDeviceTreeViewCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();
            deviceRealTimeService = ServiceManager.GetDeviceRealTimeService();
            deviceRealTimeService.GetRealTimeDataReceived += new EventHandler<GetRealTimeDataReceivedEventArgs>(deviceRealTimeService_GetRealTimeDataReceived);
            //主动获取数据
            deviceRealTimeService.GetAlarmDataListCompleted += (sender, e) => { };
            deviceRealTimeService.GetRealTimeDataListCompleted += (sender, e) => { };
            deviceRealTimeService.GetUserEventDataListCompleted += (sender, e) => { };
           

            InitSysConfig();
        }

        public void GetData()
        {
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();
            //deviceRealTimeService.GetRealTimeDataListAsync();
            //deviceRealTimeService.GetAlarmDataListAsync();
            //deviceRealTimeService.GetUserEventDataListAsync();
        }

        /// <summary>
        /// 初始化系统配置
        /// </summary>
        void InitSysConfig()
        {
            //获取设备服务
            this._systemManagerServiceSoapClient = ServiceManager.GetSystemManagerService();
            //全局参数
            this._systemManagerServiceSoapClient.GetSystemGlobalParameterCompleted += (obj, e) =>
            {
                if (e.Error == null)
                    _sysGlobalPar = BinaryObjTransfer.BinaryDeserialize<SystemGlobalParameter>(e.Result);
            };
            this._systemManagerServiceSoapClient.GetSystemGlobalParameterAsync();
            this._systemManagerServiceSoapClient.UpdateSystemGlobalParameterCompleted += (obj, e) =>
            {
                if (e.Error == null)
                {
                    Boolean result = Convert.ToBoolean(e.Result);
                    if (result)
                        MessageBox.Show("修改成功,下次登录后生效！");
                }
                else
                    MessageBox.Show("修改失败！");
            };
        }

        /// <summary>
        /// 修改系统配置
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="zoomLevel"></param>
        public void UpdataConfig(decimal longitude, decimal latitude, int zoomLevel)
        {
            _sysGlobalPar.DefaultLongitude = longitude;
            _sysGlobalPar.DefaultLatitude = latitude;
            _sysGlobalPar.DefaultZoomLevel = zoomLevel;
            string sysGlobal = BinaryObjTransfer.BinarySerialize(this._sysGlobalPar);
            this._systemManagerServiceSoapClient.UpdateSystemGlobalParameterAsync(sysGlobal);
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
                        //PushPinDeviceViewModel tem2 = new PushPinDeviceViewModel();
                        //tem2.NodeKey = item2.NodeKey;
                        //tem2.NodeValue = item2.NodeValue;
                        //tem2.NodeType = item2.NodeType;
                        //tem2.Temperature = item2.Temperature1;
                        //tem2.Status = item2.Status;
                        ////tem3.Longitude = item3.NodeChild.Select(o => o.Longitude).Average();
                        ////tem3.Dimensionality = item3.NodeChild.Select(o => o.Dimensionality).Average();
                        //tem2.InstallPlace = item2.InstallPlace;
                        //tempList.Add(tem2);

                        foreach (var item3 in item2.NodeChild)
                        {

                            PushPinDeviceViewModel tem3 = new PushPinDeviceViewModel();
                            tem3.NodeKey = item3.NodeKey;
                            tem3.NodeValue = item3.NodeValue;
                            tem3.NodeType = item3.NodeType;
                            tem3.Temperature = item3.Temperature1;
                            tem3.Status = item3.Status;
                            //tem3.Longitude = item3.NodeChild.Select(o => o.Longitude).Average();
                            //tem3.Dimensionality = item3.NodeChild.Select(o => o.Dimensionality).Average();
                            tem3.InstallPlace = item3.InstallPlace;
                            tempList.Add(tem3);
                            foreach (var item4 in item3.NodeChild)
                            {
                                PushPinDeviceViewModel tem = new PushPinDeviceViewModel();
                                tem.NodeKey = item4.NodeKey;
                                tem.NodeValue = item4.NodeValue;
                                tem.NodeType = item4.NodeType;
                                tem.Temperature = item4.Temperature1;
                                tem.Status = item4.Status;
                                tem.Longitude = item4.Longitude;
                                tem.Dimensionality = item4.Dimensionality;
                                tem.InstallPlace = item4.InstallPlace;
                                tem.Temperature = item4.Temperature1;
                                tem.Electricity = item4.Electricity;
                                tempList.Add(tem);
                            }
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
                            PushPinDeviceViewModel tem3 = new PushPinDeviceViewModel();
                            tem3.NodeKey = item3.NodeKey;
                            tem3.NodeValue = item3.NodeValue;
                            tem3.NodeType = item3.NodeType;
                            tem3.Temperature = 0;
                            tem3.Status = (int)DeviceStates.Escape;
                            tem3.Longitude = item3.NodeChild.Select(o => o.Longitude).Average();
                            tem3.Dimensionality = item3.NodeChild.Select(o => o.Latitude).Average();
                            tem3.InstallPlace = item3.InstallPlace;
                            tempList.Add(tem3);
                            foreach (var item4 in item3.NodeChild)
                            {
                                PushPinDeviceViewModel tem = new PushPinDeviceViewModel();
                                tem.NodeKey = item4.NodeKey;
                                tem.NodeValue = item4.NodeValue;
                                tem.NodeType = item4.NodeType;
                                tem.Temperature = 0;
                                tem.Status = (int)DeviceStates.Escape;
                                tem.Longitude = item4.Longitude;
                                tem.Dimensionality = item4.Latitude;
                                tem.InstallPlace = item4.InstallPlace;
                                tem.High = item4.High;
                                tem.Comment = item4.Comment;
                                tem.MaintenanceName = item4.MaintenanceName;
                                tem.Mobile = item4.Mobile;
                                if (tem.Longitude.HasValue && tem.Dimensionality.HasValue)
                                {
                                    tempList.Add(tem);
                                }
                            }
                        }
                    }
                }
                DeviceRealTimeTree = tempList;
                if (BaseDataResviceEvent != null)
                {
                    this.BaseDataResviceEvent(this, EventArgs.Empty);
                    deviceRealTimeService.GetRealTimeDataListAsync();
                    deviceRealTimeService.GetAlarmDataListAsync();
                    deviceRealTimeService.GetUserEventDataListAsync();
                }
            }
            else
            {
                MessageBox.Show("获取地图设备信息失败！");
            }
        }
    }
}
