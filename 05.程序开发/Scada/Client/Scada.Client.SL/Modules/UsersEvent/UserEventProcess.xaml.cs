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
       
        public UserEventProcess()
        {
            InitializeComponent();
        }
        public UserEventProcess(Guid id)
        {
            InitializeComponent();
            myGuid = id;

            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            this._scadaDeviceServiceSoapClient.GetListStepInfoCompleted += new EventHandler<GetListStepInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_GetListStepInfoCompleted);
            this._scadaDeviceServiceSoapClient.GetListStepInfoAsync();
        }

        void scadaDeviceServiceSoapClient_GetListStepInfoCompleted(object sender, GetListStepInfoCompletedEventArgs e)
        {
            List<StepInfo> stepInfo = BinaryObjTransfer.BinaryDeserialize<List<StepInfo>>(e.Result);
            cmbStep1.ItemsSource = stepInfo;
            cmbStep2.ItemsSource = stepInfo;
            cmbStep3.ItemsSource = stepInfo;
            cmbStep4.ItemsSource = stepInfo;
            cmbStep5.ItemsSource = stepInfo;

        }
    }
}
