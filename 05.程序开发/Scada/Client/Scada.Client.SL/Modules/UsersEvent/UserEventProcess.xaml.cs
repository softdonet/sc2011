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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">处理的事件编号</param>
        /// <param name="state">1 未处理,2 正在处理,3 处理完毕</param>
        public UserEventProcess(Guid id,int? state)
        {
            InitializeComponent();
            if (state==1)
            {
                tbtnEnter.IsEnabled = true;
                setSteptState(false);
            }
            else
            {
                tbtnEnter.IsEnabled = false;
            }
            
            myGuid = id;

            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            this._scadaDeviceServiceSoapClient.GetListStepInfoCompleted += new EventHandler<GetListStepInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_GetListStepInfoCompleted);
            this._scadaDeviceServiceSoapClient.GetListStepInfoAsync();


        }
        #endregion

        #region 事件处理

        /// <summary>
        /// 初始化下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        private void setSteptState (bool flag)
        {
            //tbtnEnter.IsEnabled = !flag;

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

        private void setStept1State()
        {
            cmbStep1.IsEnabled = true;
            txtStep1.IsEnabled = true;
            btnStep1.IsEnabled = true;

            cmbStep2.IsEnabled = false;
            txtStep2.IsEnabled = false;
            btnStep2.IsEnabled = false;

            cmbStep3.IsEnabled = false;
            txtStep3.IsEnabled = false;
            btnStep3.IsEnabled = false;

            cmbStep4.IsEnabled = false;
            txtStep4.IsEnabled = false;
            btnStep4.IsEnabled = false;

            cmbStep5.IsEnabled = false;
            txtStep5.IsEnabled = false;
            btnStep5.IsEnabled = false;

            
        }

        private void setStept2State()
        {
            cmbStep1.IsEnabled = false;
            txtStep1.IsEnabled = false;
            btnStep1.IsEnabled = false;

            cmbStep2.IsEnabled = true;
            txtStep2.IsEnabled = true;
            btnStep2.IsEnabled = true;

            cmbStep3.IsEnabled = false;
            txtStep3.IsEnabled = false;
            btnStep3.IsEnabled = false;

            cmbStep4.IsEnabled = false;
            txtStep4.IsEnabled = false;
            btnStep4.IsEnabled = false;

            cmbStep5.IsEnabled = false;
            txtStep5.IsEnabled = false;
            btnStep5.IsEnabled = false;


        }

        private void setStept3State()
        {
            cmbStep1.IsEnabled = false;
            txtStep1.IsEnabled = false;
            btnStep1.IsEnabled = false;

            cmbStep2.IsEnabled = false;
            txtStep2.IsEnabled = false;
            btnStep2.IsEnabled = false;

            cmbStep3.IsEnabled = true;
            txtStep3.IsEnabled = true;
            btnStep3.IsEnabled = true;

            cmbStep4.IsEnabled = false;
            txtStep4.IsEnabled = false;
            btnStep4.IsEnabled = false;

            cmbStep5.IsEnabled = false;
            txtStep5.IsEnabled = false;
            btnStep5.IsEnabled = false;


        }

        private void setStept4State()
        {
            cmbStep1.IsEnabled = false;
            txtStep1.IsEnabled = false;
            btnStep1.IsEnabled = false;

            cmbStep2.IsEnabled = false;
            txtStep2.IsEnabled = false;
            btnStep2.IsEnabled = false;

            cmbStep3.IsEnabled = false;
            txtStep3.IsEnabled = false;
            btnStep3.IsEnabled = false;

            cmbStep4.IsEnabled = true;
            txtStep4.IsEnabled = true;
            btnStep4.IsEnabled = true;

            cmbStep5.IsEnabled = false;
            txtStep5.IsEnabled = false;
            btnStep5.IsEnabled = false;


        }

        private void setStept5State()
        {
            cmbStep1.IsEnabled = false;
            txtStep1.IsEnabled = false;
            btnStep1.IsEnabled = false;

            cmbStep2.IsEnabled = false;
            txtStep2.IsEnabled = false;
            btnStep2.IsEnabled = false;

            cmbStep3.IsEnabled = false;
            txtStep3.IsEnabled = false;
            btnStep3.IsEnabled = false;

            cmbStep4.IsEnabled = false;
            txtStep4.IsEnabled = false;
            btnStep4.IsEnabled = false;

            cmbStep5.IsEnabled = true;
            txtStep5.IsEnabled = true;
            btnStep5.IsEnabled = true;


        }

        private void setControlSate(int t)
        {
            for (int i = 1; i <= 5; i++)
            {
                
            }
            Button b= GetChildObject<Button>(this.tbtnEnter,"aa");
        }

        /// <summary>
        /// 查询指定的子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetChildObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            T grandChild = null;

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)child;
                }
                else
                {
                    grandChild = GetChildObject<T>(child, name);
                    if (grandChild != null)
                        return grandChild;
                }
            }
            return null;
        }
    


        private void tbtnEnter_Checked(object sender, RoutedEventArgs e)
        {
            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            _scadaDeviceServiceSoapClient.UpdateEventStateCompleted += new EventHandler<UpdateEventStateCompletedEventArgs>(_scadaDeviceServiceSoapClient_UpdateEventStateCompleted);
            _scadaDeviceServiceSoapClient.UpdateEventStateAsync(myGuid);
            
            //if (true)
            //{
            //    setControlSate(true);
            //}
        }

        void _scadaDeviceServiceSoapClient_UpdateEventStateCompleted(object sender, UpdateEventStateCompletedEventArgs e)
        {
            bool flag = e.Result;
            if (flag)
            {
                tbtnEnter.IsEnabled = false;
                setStept1State();
            }
            else
            {
                tbtnEnter.IsEnabled = true;
            }
        }

        EventDealDetail eventDealDetail;
        private void btnStep_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string Name = btn.Name;
            switch (Name)
            {
                case "btnStep1":
                    eventDealDetail=new EventDealDetail();
                    eventDealDetail.StepNo=1;
                    eventDealDetail.StepName=cmbStep1.SelectedValue.ToString();
                    eventDealDetail.Memo = ((StepInfo)(cmbStep1.SelectionBoxItem)).StepName;
                    eventDealDetail.Operator=new Guid();
                    eventDealDetail.DealTime=DateTime.Now;
                    string strSerializer=BinaryObjTransfer.BinarySerialize<EventDealDetail>(eventDealDetail);

                    _scadaDeviceServiceSoapClient.ProcessingStepNoAsync(strSerializer);

                    setStept2State();
                    break;
                case "btnStep2":

                    setStept3State();
                    break;
                case "btnStep3":

                    setStept4State();
                    break;
                    
                case "btnStep4":

                    setStept5State();
                    break;
                case"btnStep5":
                    setSteptState(false);
                    break;
                default:
                    break;
            }
        }
      
        
        #endregion


    }
}
