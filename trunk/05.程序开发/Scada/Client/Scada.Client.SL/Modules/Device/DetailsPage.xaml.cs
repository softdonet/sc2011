using System;
using System.Linq;
using System.Collections.Generic;


using System.Net;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;


using Scada.Model.Entity;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;
using Scada.Client.VM.Modules.Device;






namespace Scada.Client.SL.Modules.Device
{


    /// <summary>
    /// 设备详细信息
    /// </summary>
    public partial class DetailsPage : UserControl
    {


        #region 变量声明

        private Guid _deviceKey;

        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient;

        #endregion


        #region 构造函数

        public DetailsPage()
        {
            InitializeComponent();
        }

        public DetailsPage(Guid deviceKey)
        {
            InitializeComponent();
            this._deviceKey = deviceKey;
            this.Init();

            DetailsPageViewModel detailsPageViewModel = new DetailsPageViewModel(deviceKey);
            this.DataContext = detailsPageViewModel;
            detailsPageViewModel.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "DeviceInfo")
                {
                    if (detailsPageViewModel.DeviceInfo != null)
                        this.myTemperature.Temperature = detailsPageViewModel.DeviceInfo.RealTimeTemperature;
                }
            };
        }

        #endregion


        #region 界面初期化

        private void Init()
        {

            //获取设备服务
            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();


            ////设备当天温度
            //this._scadaDeviceServiceSoapClient.GetDeviceOnlyDayCompleted +=
            //    new EventHandler<GetDeviceOnlyDayCompletedEventArgs>(scadaDeviceServiceSoapClient_GetDeviceOnlyDayCompleted);
            //this._scadaDeviceServiceSoapClient.GetDeviceOnlyDayAsync(this._deviceKey.ToString());


            ////设备历史温度
            //this._scadaDeviceServiceSoapClient.GetDeviceOnlyMonthCompleted +=
            //            new EventHandler<GetDeviceOnlyMonthCompletedEventArgs>(scadaDeviceServiceSoapClient_GetDeviceOnlyMonthCompleted);
            //this._scadaDeviceServiceSoapClient.GetDeviceOnlyMonthAsync(this._deviceKey.ToString());

        }

        #endregion


        #region 私有方法

        private void scadaDeviceServiceSoapClient_GetDeviceOnlyDayCompleted(object sender, GetDeviceOnlyDayCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string msgInfo = e.Result;
                List<ChartSource> devicTreeList = BinaryObjTransfer.BinaryDeserialize<List<ChartSource>>(msgInfo);
                if (devicTreeList != null && devicTreeList.Count() == 0) { return; }
                this.chartDayTemperature.SetDeviceTemperature(devicTreeList);
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }


        private void scadaDeviceServiceSoapClient_GetDeviceOnlyMonthCompleted(object sender, GetDeviceOnlyMonthCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string msgInfo = e.Result;
                List<ChartSource> devicTreeList = BinaryObjTransfer.BinaryDeserialize<List<ChartSource>>(msgInfo);
                if (devicTreeList != null && devicTreeList.Count() == 0) { return; }
                this.chartMonthTemperature.SetDeviceTemperature(devicTreeList);
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }


        #endregion


    }

}
