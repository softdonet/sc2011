using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Scada.Model.Entity;
using Scada.Client.SL.SystemManagerService;
using Scada.Client.SL.CommClass;


namespace Scada.Client.SL.Modules.BaseInfo
{
    public partial class BroadcastInfoManage : UserControl
    {
        private SystemGlobalParameter _sysGlobalPar;

        private SystemManagerServiceSoapClient _systemManagerServiceSoapClient;

        public BroadcastInfoManage()
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

        private void scadaDeviceServiceSoapClient_UpdateSystemGlobalParameterCompleted(object sender, UpdateSystemGlobalParameterCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Boolean result = Convert.ToBoolean(e.Result);
                if (result)
                    ScadaMessageBox.ShowWarnMessage("发送成功", "提示信息");
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




        #region 私有方法


        private void scadaDeviceServiceSoapClient_GetSystemGlobalParameterCompleted(object sender,
                                                                                GetSystemGlobalParameterCompletedEventArgs e)
        {

            if (e.Error == null)
            {
                _sysGlobalPar = BinaryObjTransfer.BinaryDeserialize<SystemGlobalParameter>(e.Result);
                if (_sysGlobalPar == null) { return; }
                this.txtBroadcast.Text = _sysGlobalPar.Broadcast;
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");

        }


        #endregion

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (!System.Windows.Browser.HtmlPage.Window.Confirm("确认发送广播信息吗？"))
            {
                return;
            }

            //System.Windows.Browser.HtmlPage.Window.Alert("X，Y坐标不能为空");
            // if (ScadaMessageBox.ShowOKCancelMessage("确认发送广播信息吗？", "提示") == MessageBoxResult.Cancel) { return; }
            _sysGlobalPar.Broadcast = this.txtBroadcast.Text.Trim();
            string sysGlobal = BinaryObjTransfer.BinarySerialize(this._sysGlobalPar);
            this._systemManagerServiceSoapClient.UpdateSystemGlobalParameterAsync(sysGlobal);
        }


    }
}
