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

        private List<SystemParameterManage> _sysParManage;

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


            //系统参数
            //this._systemManagerServiceSoapClient.GetSystemParameterManageCompleted
            //                        += new EventHandler<GetSystemParameterManageCompletedEventArgs>
            //                                    (scadaDeviceServiceSoapClient_GetSystemParameterManageCompleted);
            //this._systemManagerServiceSoapClient.GetSystemParameterManageAsync();

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
            /*
            if (domain.Length > 0)
            {
                if (!RegularValidate.JudgeRealmNameIsValid(domain))
                {
                    ScadaMessageBox.ShowWarnMessage("您输入的信息不符合域名", "提示信息");
                    this.txtDomain.Focus();
                    return;
                }
            }
            */
            _sysGlobalPar.ConnectType = this.cmbConnectType.SelectedIndex;
            _sysGlobalPar.ConnectName = connectName;
            _sysGlobalPar.MainDNS = dns;
            _sysGlobalPar.SecondDNS = secdns;
            _sysGlobalPar.Domain = domain;
            _sysGlobalPar.Port = Convert.ToInt32(this.txtPort.Value);

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
                    ScadaMessageBox.ShowWarnMessage("修改成功", "提示信息");
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }


        //private void btnSave_Click(object sender, RoutedEventArgs e)
        //{

        //    object obj = this.txtEarlyTimeOut.Value;
        //    if (obj != null)
        //        this._sysParManage[0].ParameterValue = Convert.ToSingle(obj);

        //    obj = this.txtEarlyAlarm.Value;
        //    if (obj != null)
        //        this._sysParManage[1].ParameterValue = Convert.ToSingle(obj);

        //    obj = this.txtEarlyNormal.Value;
        //    if (obj != null)
        //        this._sysParManage[2].ParameterValue = Convert.ToSingle(obj);

        //    obj = this.txtDefDns.Value;
        //    if (obj != null)
        //        this._sysParManage[3].ParameterValue = Convert.ToSingle(obj);

        //    string sysManage = BinaryObjTransfer.BinarySerialize(this._sysParManage);
        //    this._systemManagerServiceSoapClient.UpdateSystemParameterManageCompleted
        //        += new EventHandler<UpdateSystemParameterManageCompletedEventArgs>
        //            (scadaDeviceServiceSoapClient_UpdateSystemParameterManageCompleted);
        //    this._systemManagerServiceSoapClient.UpdateSystemParameterManageAsync(sysManage);


        //}

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


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

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

            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");

        }

        //private void scadaDeviceServiceSoapClient_GetSystemParameterManageCompleted(object sender,
        //                                                                    GetSystemParameterManageCompletedEventArgs e)
        //{
        //    if (e.Error == null)
        //    {
        //        _sysParManage = BinaryObjTransfer.BinaryDeserialize<List<SystemParameterManage>>(e.Result);
        //        if (_sysParManage == null) { return; }

        //        this.txtEarlyTimeOut.Value = _sysParManage[0].ParameterValue;
        //        this.txtEarlyTimeOut.Tag = _sysParManage[0].ParameterID;

        //        this.txtEarlyAlarm.Value = _sysParManage[1].ParameterValue;
        //        this.txtEarlyAlarm.Tag = _sysParManage[1].ParameterID;

        //        this.txtEarlyNormal.Value = _sysParManage[2].ParameterValue;
        //        this.txtEarlyNormal.Tag = _sysParManage[2].ParameterID;

        //        this.txtDefDns.Value = _sysParManage[3].ParameterValue;
        //        this.txtDefDns.Tag = _sysParManage[3].ParameterID;

        //    }
        //    else
        //        ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        //}


        #endregion



    }
}
