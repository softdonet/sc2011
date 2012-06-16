using System;
using System.Linq;
using System.Collections.Generic;


using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;



using Scada.Model.Entity;
using Scada.Client.SL.ScadaDeviceService;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.SystemManagerService;
using Scada.Utility.Common.SL;




namespace Scada.Client.SL.Modules.BaseInfo
{

    /// <summary>
    /// 系统参数配置
    /// </summary>
    public partial class SysConfig : UserControl
    {

        #region 变量声明


        private SystemGlobalParameter _sysGlobalPar;

        private SystemManagerServiceSoapClient _systemManagerServiceSoapClient;

        #endregion


        #region 构造函数

        public SysConfig()
        {

            InitializeComponent();

            //获取设备服务
            this._systemManagerServiceSoapClient = ServiceManager.GetSystemManagerService();


            //全局参数
            this._systemManagerServiceSoapClient.GetSystemGlobalParameterCompleted
                                     += new EventHandler<GetSystemGlobalParameterCompletedEventArgs>
                                                (scadaDeviceServiceSoapClient_GetSystemGlobalParameterCompleted);
            this._systemManagerServiceSoapClient.GetSystemGlobalParameterAsync();

            this._systemManagerServiceSoapClient.UpdateSystemGlobalParameterCompleted
               += new EventHandler<UpdateSystemGlobalParameterCompletedEventArgs>
                   (scadaDeviceServiceSoapClient_UpdateSystemGlobalParameterCompleted);

        }

        #endregion


        #region 事件管理


        private void butOk_Click(object sender, RoutedEventArgs e)
        {


            //Check ConnectName
            string connectName = this.txtConnectName.Text;
            if (connectName.Length > 0)
            {
                if (!RegularValidate.JudgeLetterIsValid(connectName))
                {
                    ScadaMessageBox.ShowWarnMessage("您输入的信息必须是字母", "提示信息");
                    this.txtConnectName.Focus();
                    return;
                }
            }

            //Check DNS
            string dns = this.txtMainDNS.Text;
            if (dns.Length > 0)
            {
                if (!RegularValidate.JudgeIPAddressIsValid(dns))
                {
                    ScadaMessageBox.ShowWarnMessage("您输入的信息不符合IP", "提示信息");
                    this.txtMainDNS.Focus();
                    return;
                }
            }

            string secdns = this.txtSecondDNS.Text;
            if (secdns.Length > 0)
            {
                if (!RegularValidate.JudgeIPAddressIsValid(secdns))
                {
                    ScadaMessageBox.ShowWarnMessage("您输入的信息不符合备用IP", "提示信息");
                    this.txtSecondDNS.Focus();
                    return;
                }
            }

            string domain = this.txtDomain.Text;
            _sysGlobalPar.ConnectType = this.cmbConnectType.SelectedIndex;
            _sysGlobalPar.ConnectName = connectName;
            _sysGlobalPar.MainDNS = dns;
            _sysGlobalPar.SecondDNS = secdns;
            _sysGlobalPar.Domain = domain;
            _sysGlobalPar.Port = Convert.ToInt32(this.txtPort.Value);
            _sysGlobalPar.Title = this.txtTitle.Text;
            _sysGlobalPar.ChartMaxTemp = Convert.ToInt32(this.txtChartMaxTemp.Value);
            _sysGlobalPar.ChartHighTemp = Convert.ToInt32(this.txtChartHighTemp.Value);
            _sysGlobalPar.ChartLowTemp = Convert.ToInt32(this.txtChartLowTemp.Value);
            _sysGlobalPar.ChartMinTemp = Convert.ToInt32(this.txtChartMinTemp.Value);
            _sysGlobalPar.IsShowTool = this.chkShowTool.IsChecked;
            _sysGlobalPar.WeatherCity = this.txtWeatherCity.Text;

            string sysGlobal = BinaryObjTransfer.BinarySerialize(this._sysGlobalPar);
            this._systemManagerServiceSoapClient.UpdateSystemGlobalParameterAsync(sysGlobal);
        }


        private void scadaDeviceServiceSoapClient_UpdateSystemGlobalParameterCompleted(object sender,
                                                                    UpdateSystemGlobalParameterCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Boolean result = Convert.ToBoolean(e.Result);
                if (result)
                    ScadaMessageBox.ShowWarnMessage("修改成功，下次登录生效！", "提示信息");
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        private void scadaDeviceServiceSoapClient_UpdateSystemParameterManageCompleted(object sender,
                                                                            UpdateSystemParameterManageCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Boolean result = Convert.ToBoolean(e.Result);
                if (result)
                    ScadaMessageBox.ShowWarnMessage("修改成功", "提示信息");
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");

        }


        #endregion


        #region 私有方法


        private void scadaDeviceServiceSoapClient_GetSystemGlobalParameterCompleted(object sender,
                                                                                GetSystemGlobalParameterCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                _sysGlobalPar = BinaryObjTransfer.BinaryDeserialize<SystemGlobalParameter>(e.Result);
                if (_sysGlobalPar == null) { return; }
                this.cmbConnectType.SelectedIndex = _sysGlobalPar.ConnectType;
                this.txtConnectName.Text = _sysGlobalPar.ConnectName;
                this.txtMainDNS.Text = _sysGlobalPar.MainDNS;
                this.txtSecondDNS.Text = _sysGlobalPar.SecondDNS;
                this.txtDomain.Text = _sysGlobalPar.Domain;
                this.txtPort.Value = _sysGlobalPar.Port;
                this.txtTitle.Text = _sysGlobalPar.Title;
                this.txtChartMaxTemp.Value = _sysGlobalPar.ChartMaxTemp;
                this.txtChartHighTemp.Value = _sysGlobalPar.ChartHighTemp;
                this.txtChartLowTemp.Value = _sysGlobalPar.ChartLowTemp;
                this.txtChartMinTemp.Value = _sysGlobalPar.ChartMinTemp;
                this.txtWeatherCity.Text = _sysGlobalPar.WeatherCity;
                this.chkShowTool.IsChecked = _sysGlobalPar.IsShowTool.GetValueOrDefault(false);
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        #endregion
    }
}
