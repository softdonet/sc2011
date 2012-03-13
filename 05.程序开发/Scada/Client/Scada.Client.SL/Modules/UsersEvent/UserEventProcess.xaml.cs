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
using System.Windows.Controls.Primitives;

namespace Scada.Client.SL.Modules.UsersEvent
{
    public partial class UserEventProcess : UserControl
    {
        #region 变量声明
        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient=null;
        private UserEventModel userEventModel;
        public Guid myGuid { get; set; }
        public int?state;

        private bool isBtnEnter;
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
        public UserEventProcess(UserEventModel userEventModel)
        {
            InitializeComponent();
            this.userEventModel = userEventModel;

            myGuid = userEventModel.ID;
            state = userEventModel.State;

            if (state==1)
            {
                tbtnEnter.IsEnabled = true;
                setSteptState(false);
            }
            else if (state==2)
            {
                tbtnEnter.IsEnabled = false;
            }
            else
            {
                tbtnEnter.IsEnabled = false;
                tbtnClose.IsEnabled = false;
            }

            ///从服务上获取下拉框数据
            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            this._scadaDeviceServiceSoapClient.GetListStepInfoCompleted += new EventHandler<GetListStepInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_GetListStepInfoCompleted);
            this._scadaDeviceServiceSoapClient.GetListStepInfoAsync();

            ///查找用户表中是否处理过该用户事件，有的话，返回结果对象
            //this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            this._scadaDeviceServiceSoapClient.GetUserEventKeyInfoCompleted+=new EventHandler<GetUserEventKeyInfoCompletedEventArgs>(_scadaDeviceServiceSoapClient_GetUserEventKeyInfoCompleted);
            this._scadaDeviceServiceSoapClient.GetUserEventKeyInfoAsync(userEventModel.ID);
            
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

        void _scadaDeviceServiceSoapClient_GetUserEventKeyInfoCompleted(object sender, GetUserEventKeyInfoCompletedEventArgs e)
        {
            List<UserEventDealDetail> eventDealDetail = BinaryObjTransfer.BinaryDeserialize<List<UserEventDealDetail>>(e.Result);
            //UserEventDealDetail[] ArrEventDealDetail = eventDealDetail.ToArray();
            if (userEventModel.State == 1)
            {
                tbtnEnter.IsEnabled = true;
                return;
            }
            else
            {
                if (eventDealDetail == null)
                {
                    tbtnEnter.IsEnabled = false;
                    setStept1State();
                    return;
                }
            }
            foreach (UserEventDealDetail item in eventDealDetail)
            {

                switch (item.StepNo)
                {
                    case 1:
                        cmbStep1.SelectedIndex = getSelectIndex(item.StepName);
                        txtStep1.Text = item.Memo;
                        //txtblk1.Text = "确认人：" + item.OperatorId + " 确认时间：" + item.DealTime;
                        txtblk1.Text = "确认人：Admin"  + " 确认时间：" + item.DealTime;
                        setStept2State();
                        break;
                    case 2:
                        cmbStep2.SelectedIndex = getSelectIndex(item.StepName);
                        txtStep2.Text = item.Memo;
                        txtblk2.Text = "确认人：Admin"  + " 确认时间：" + item.DealTime;
                        setStept3State();
                        break;
                    case 3:
                        cmbStep3.SelectedIndex = getSelectIndex(item.StepName);
                        txtStep3.Text = item.Memo;
                        txtblk3.Text = "确认人：Admin" + " 确认时间：" + item.DealTime;
                        setStept4State();

                        break;
                    case 4:
                        cmbStep4.SelectedIndex = getSelectIndex(item.StepName);
                        txtStep4.Text = item.Memo;
                        txtblk4.Text = "确认人：Admin"  + " 确认时间：" + item.DealTime;
                        setStept5State();
                        break;
                    case 5:
                        cmbStep5.SelectedIndex = getSelectIndex(item.StepName);
                        txtStep5.Text = item.Memo;
                        txtblk5.Text = "确认人：Admin"  + " 确认时间：" + item.DealTime;
                        setSteptState(false);
                        break;

                    default:
                        setSteptState(false);
                        break;
                }
            }
           
            //int? maxStept = eventDealDetail.Max(p => p.StepNo);
            //Type type = typeof(UserEventProcess);
            //System.Reflection.MethodInfo methodInfo = type.GetMethod("setStept" + maxStept + "State");
            //object obj = Activator.CreateInstance(type);
            //methodInfo.Invoke(obj, null);
        }

        private int getSelectIndex(string stepName)
        {
            int flag = -1;
            List<StepInfo> list = this.cmbStep1.ItemsSource as List<StepInfo>;
            if (list==null)
            {
                return flag;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].StepName == stepName)
                {
                    flag = i;
                    break;
                }
            }
            return flag;
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
            isBtnEnter = true;
            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            _scadaDeviceServiceSoapClient.UpdateEventStateCompleted += new EventHandler<UpdateEventStateCompletedEventArgs>(_scadaDeviceServiceSoapClient_UpdateEventStateCompleted);
            _scadaDeviceServiceSoapClient.UpdateEventStateAsync(myGuid, 2);
            
            //if (true)
            //{
            //    setControlSate(true);
            //}
        }

        void _scadaDeviceServiceSoapClient_UpdateEventStateCompleted(object sender, UpdateEventStateCompletedEventArgs e)
        {
            //ToggleButton tbtn = sender as ToggleButton;

            bool flag = e.Result;
            if (flag)
            {
                //判断是否点击的是进入按钮还是关闭按钮
                if (isBtnEnter)
                {
                    tbtnEnter.IsEnabled = false;
                    setStept1State();
                }
                else
                {
                    tbtnClose.IsEnabled = false;
                    setSteptState(false);
                }
               
            }
            else
            {
                if (isBtnEnter)
                {
                    tbtnEnter.IsEnabled = true;
                    setSteptState(false);
                }
                else
                {
                    tbtnClose.IsEnabled = true;
                }
               
            }
        }

        UserEventDealDetail eventDealDetail;
        private void btnStep_Click(object sender, RoutedEventArgs e)
        {
            eventDealDetail = new UserEventDealDetail();
            eventDealDetail.ID = Guid.NewGuid();
            eventDealDetail.EventID = userEventModel.ID;
            eventDealDetail.Operator= new Guid("B1EE865D-E279-431F-97CD-2BADC04A850D");
            eventDealDetail.DealTime = DateTime.Now;


            Button btn = sender as Button;
            string Name = btn.Name;
            switch (Name)
            {
                case "btnStep1":
                    eventDealDetail.StepNo = 1;
                    if (cmbStep1.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择用户事件类型!");
                        return;
                    }
                    eventDealDetail.StepName = ((StepInfo)(cmbStep1.SelectionBoxItem)).StepName;
                    eventDealDetail.Memo = txtStep1.Text.Trim();
                    txtblk1.Text = "确认人：" + "admin" + " 确认时间：" + DateTime.Now;
                    setStept2State();
                    break;
                case "btnStep2":
                    eventDealDetail.StepNo = 2;
                    if (cmbStep2.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择用户事件类型!");
                        return;
                    }
                    eventDealDetail.StepName = ((StepInfo)(cmbStep2.SelectionBoxItem)).StepName;
                    eventDealDetail.Memo = txtStep2.Text.Trim();
                    txtblk2.Text = "确认人：" + "admin" + " 确认时间：" + DateTime.Now;
                    setStept3State();
                    break;
                case "btnStep3":
                    eventDealDetail.StepNo = 3;
                    if (cmbStep3.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择用户事件类型!");
                        return;
                    }
                    eventDealDetail.StepName = ((StepInfo)(cmbStep3.SelectionBoxItem)).StepName;
                    eventDealDetail.Memo = txtStep3.Text.Trim();
                    txtblk3.Text = "确认人：" + "admin" + " 确认时间：" + DateTime.Now;
                    setStept4State();
                    break;

                case "btnStep4":
                    eventDealDetail.StepNo = 4;
                    if (cmbStep4.SelectedIndex==-1)
                    {
                        MessageBox.Show("请选择用户事件类型!");
                        return;
                    }
                    eventDealDetail.StepName = ((StepInfo)(cmbStep4.SelectionBoxItem)).StepName;
                    eventDealDetail.Memo = txtStep4.Text.Trim();
                    txtblk4.Text = "确认人：" + "admin" + " 确认时间：" + DateTime.Now;
                    setStept5State();
                    break;
                case "btnStep5":
                    eventDealDetail.StepNo = 5;
                    if (cmbStep5.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择用户事件类型!");
                        return;
                    }
                    eventDealDetail.StepName = ((StepInfo)(cmbStep5.SelectionBoxItem)).StepName;
                    eventDealDetail.Memo = txtStep5.Text.Trim();
                    txtblk5.Text = "确认人：" + "admin" + " 确认时间：" + DateTime.Now;
                    setSteptState(false);
                    break;
                default:
                    break;
            }
            string strSerializer = BinaryObjTransfer.BinarySerialize<UserEventDealDetail>(eventDealDetail);

            _scadaDeviceServiceSoapClient.ProcessingStepNoAsync(strSerializer);

        }

        private void tbtnClose_Checked(object sender, RoutedEventArgs e)
        {
            //
            isBtnEnter = false;
            if (btnStep5.IsEnabled == false)
            {
                this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
                _scadaDeviceServiceSoapClient.UpdateEventStateCompleted += new EventHandler<UpdateEventStateCompletedEventArgs>(_scadaDeviceServiceSoapClient_UpdateEventStateCompleted);
                _scadaDeviceServiceSoapClient.UpdateEventStateAsync(myGuid, 3);

            }
            else
            {
                MessageBox.Show("请完成以上步骤!", "提示", MessageBoxButton.OK);
            }
        }

      
        
        #endregion



    }
}
