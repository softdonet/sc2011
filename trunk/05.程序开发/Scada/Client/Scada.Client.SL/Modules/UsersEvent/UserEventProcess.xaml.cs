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
using Scada.Client.SL.ScadaDeviceService;
using Scada.Client.SL.CommClass;
using Scada.Model.Entity;

namespace Scada.Client.SL.Modules.UsersEvent
{
    public partial class UserEventProcess : UserControl
    {
        #region 变量声明
        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient=null;
        public Guid myGuid { get; set; }
        #endregion
        #region 构造函数

      
        public UserEventProcess()
        {
            InitializeComponent();
        }

        public UserEventProcess(Guid id,int? state)
        {
            InitializeComponent();
            if (true)
            {
                setControlSate(false);
            }
            

            myGuid = id;

            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            this._scadaDeviceServiceSoapClient.GetListStepInfoCompleted += new EventHandler<GetListStepInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_GetListStepInfoCompleted);
            this._scadaDeviceServiceSoapClient.GetListStepInfoAsync();
        }
        #endregion

        #region 事件处理

        void scadaDeviceServiceSoapClient_GetListStepInfoCompleted(object sender, GetListStepInfoCompletedEventArgs e)
        {
            List<StepInfo> stepInfo = BinaryObjTransfer.BinaryDeserialize<List<StepInfo>>(e.Result);
            cmbStep1.ItemsSource = stepInfo;
            cmbStep2.ItemsSource = stepInfo;
            cmbStep3.ItemsSource = stepInfo;
            cmbStep4.ItemsSource = stepInfo;
            cmbStep5.ItemsSource = stepInfo;

        }

        /// <summary>
        /// 设置界面控件是否Enabled
        /// </summary>
        /// <param name="flag"></param>
        private void setControlSate(bool flag)
        {
            cmbStep1.IsEnabled = flag;
            cmbStep2.IsEnabled = flag;
            cmbStep3.IsEnabled = flag;
            cmbStep4.IsEnabled = flag;
            cmbStep5.IsEnabled = flag;

            txtStep1.IsEnabled = flag;
            txtStep2.IsEnabled = flag;
            txtStep3.IsEnabled = flag;
            txtStep4.IsEnabled = flag;
            txtStep5.IsEnabled = flag;

            btnStep1.IsEnabled = flag;
            btnStep2.IsEnabled = flag;
            btnStep3.IsEnabled = flag;
            btnStep4.IsEnabled = flag;
            btnStep5.IsEnabled = flag;
        }

        private void tbtnEnter_Checked(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                setControlSate(true);
            }
        }
        #endregion
    }
}
