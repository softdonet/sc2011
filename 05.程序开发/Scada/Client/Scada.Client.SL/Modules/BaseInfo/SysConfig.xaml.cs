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
            //this._systemManagerServiceSoapClient.GetSystemGlobalParameterAsync();


            //系统参数
            this._systemManagerServiceSoapClient.GetSystemParameterManageCompleted
                                    += new EventHandler<GetSystemParameterManageCompletedEventArgs>
                                                (scadaDeviceServiceSoapClient_GetSystemParameterManageCompleted);
            this._systemManagerServiceSoapClient.GetSystemParameterManageAsync();


        }

        #endregion


        #region 事件管理


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            object obj = this.txtEarlyTimeOut.Value;
            if (obj != null)
                this._sysParManage[0].ParameterValue = (float)obj;

            obj = this.txtEarlyAlarm.Value;
            if (obj != null)
                this._sysParManage[1].ParameterValue = (float)obj;

            obj = this.txtEarlyNormal.Value;
            if (obj != null)
                this._sysParManage[2].ParameterValue = (float)obj;

            obj = this.txtDefDns.Value;
            if (obj != null)
                this._sysParManage[3].ParameterValue = (float)obj;

            string sysManage = BinaryObjTransfer.BinarySerialize(this._sysParManage);
            this._systemManagerServiceSoapClient.UpdateSystemParameterManageCompleted
                += new EventHandler<UpdateSystemParameterManageCompletedEventArgs>
                    (scadaDeviceServiceSoapClient_UpdateSystemParameterManageCompleted);
            this._systemManagerServiceSoapClient.UpdateSystemParameterManageAsync(sysManage);


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
                SystemGlobalParameter globalPar =
                            BinaryObjTransfer.BinaryDeserialize<SystemGlobalParameter>(e.Result);

            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");

        }

        private void scadaDeviceServiceSoapClient_GetSystemParameterManageCompleted(object sender,
                                                                            GetSystemParameterManageCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                List<SystemParameterManage> sysParMan = BinaryObjTransfer.BinaryDeserialize<List<SystemParameterManage>>(e.Result);
                if (sysParMan == null) { return; }

                this.txtEarlyTimeOut.Value = sysParMan[0].ParameterValue;
                this.txtEarlyTimeOut.Tag = sysParMan[0].ParameterID;

                this.txtEarlyAlarm.Value = sysParMan[1].ParameterValue;
                this.txtEarlyAlarm.Tag = sysParMan[1].ParameterID;

                this.txtEarlyNormal.Value = sysParMan[2].ParameterValue;
                this.txtEarlyNormal.Tag = sysParMan[2].ParameterID;

                this.txtDefDns.Value = sysParMan[3].ParameterValue;
                this.txtDefDns.Tag = sysParMan[3].ParameterID;

            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }


        #endregion



    }
}
