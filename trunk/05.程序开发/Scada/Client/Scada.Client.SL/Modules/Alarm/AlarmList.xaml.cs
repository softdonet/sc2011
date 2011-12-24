using System;
using System.Linq;
using System.Collections.Generic;



using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;



using Scada.Model.Entity;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;






namespace Scada.Client.SL.Modules.Alarm
{


    /// <summary>
    /// 告警信息
    /// </summary>
    public partial class AlarmList : UserControl
    {

        #region 变量声明

        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient;

        #endregion


        #region 构造函数

        public AlarmList()
        {

            InitializeComponent();

            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            this._scadaDeviceServiceSoapClient.GetListDeviceAlarmInfoCompleted +=
                    new EventHandler<GetListDeviceAlarmInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            this._scadaDeviceServiceSoapClient.GetListDeviceAlarmInfoAsync();

        }

        private void scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted(object sender, GetListDeviceAlarmInfoCompletedEventArgs e)
        {
            List<DeviceAlarm> deviceAlam = BinaryObjTransfer.BinaryDeserialize<List<DeviceAlarm>>(e.Result);
            this.RadGridView1.ItemsSource = deviceAlam;
        }

        #endregion


        #region 事件处理
        #endregion



    }
}
