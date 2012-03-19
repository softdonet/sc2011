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

        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient;

        #endregion


        #region 构造函数


        public SysConfig()
        {
            
            InitializeComponent();

            //获取设备服务
            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

        }

        #endregion


        #region 事件管理


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion


        #region 私有方法
        #endregion


    }
}
