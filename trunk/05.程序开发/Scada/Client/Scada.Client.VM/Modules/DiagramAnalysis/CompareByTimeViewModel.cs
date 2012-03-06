using System;
using System.Linq;
using System.Collections.Generic;


using System.Net;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;


using Microsoft.Practices.Prism.ViewModel;


using Scada.Model.Entity;
using Scada.Client.VM.ScadaDeviceService;
using Scada.Client.VM.CommClass;



namespace Scada.Client.VM.Modules.DiagramAnalysis
{


    /// <summary>
    /// 按日期对比(同设备不同时间段)VM层
    /// </summary>
    public class CompareByTimeViewModel : NotificationObject
    {


        #region 变量声明

        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient;

        #endregion


        #region 构造函数

        public CompareByTimeViewModel()
        {

            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.GetDeviceTreeListCompleted +=
                                            new EventHandler<GetDeviceTreeListCompletedEventArgs>
                                            (scadaDeviceServiceSoapClient_GetDeviceTreeListCompleted);
            scadaDeviceServiceSoapClient.GetDeviceTreeListAsync(); 

        }

        #endregion


        #region 开放方法

        private List<DeviceRealTimeTree> devicTreeList;
        public List<DeviceRealTimeTree> DeviceTreeList
        {
            get { return devicTreeList; }
            set
            {
                devicTreeList = value;
                this.RaisePropertyChanged("DeviceTreeList");
            }
        }


        #endregion


        #region 私有方法

        private void scadaDeviceServiceSoapClient_GetDeviceTreeListCompleted(object sender, GetDeviceTreeListCompletedEventArgs e)
        {
            if (e.Error == null)
                devicTreeList = BinaryObjTransfer.BinaryDeserialize<List<DeviceRealTimeTree>>(e.Result);
            else
                MessageBox.Show("获取数据失败！");
        }


        #endregion

    }

}
