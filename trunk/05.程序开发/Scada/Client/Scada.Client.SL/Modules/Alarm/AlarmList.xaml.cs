using System;
using System.Linq;
using System.Collections.Generic;

using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;

using Scada.Model.Entity;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;

using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Globalization;
using System.Runtime.Serialization;
using Scada.Model.Entity.Enums;
using Scada.Client.VM.Modules.Alarm;






namespace Scada.Client.SL.Modules.Alarm
{


    /// <summary>
    /// 告警信息
    /// </summary>
    public partial class AlarmList : UserControl
    {

        #region 变量声明

        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient;
        private readonly int UpdateCount = 100;

        #endregion

        #region 构造函数

        private static AlarmList instance;
        public static AlarmList GetInstance()
        {
            if (instance == null)
            {
                instance = new AlarmList();
            }
            return instance;
        }
        AlarmListViewModel AlarmListVM = null;
        public AlarmList()
        {
            
            InitializeComponent();

            AlarmListVM = new AlarmListViewModel();
            this.DataContext = AlarmListVM;
            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            this._scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoCompleted +=
                new EventHandler<UpdateDeviceAlarmInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoCompleted);

            this._scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoBatchCompleted += new EventHandler<UpdateDeviceAlarmInfoBatchCompletedEventArgs>(_scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoBatchCompleted);
        }


        #endregion

        #region 事件处理

        private void RadGridView1_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            if (e.Row is GridViewHeaderRow) return;
            //---------------------
            //--------------------------
            DeviceAlarm currentRow = e.Row.DataContext as DeviceAlarm;
            string statetext = currentRow.DealStatus;
            TextBlock state = (e.Row.Cells[RadGridView1.Columns.Count - 3].Content as FrameworkElement) as TextBlock;
            HyperlinkButton hlBtn = (e.Row.Cells[RadGridView1.Columns.Count - 2].Content as FrameworkElement).FindName("hlBtn") as HyperlinkButton;
            if (string.IsNullOrEmpty(statetext))//if (string.IsNullOrEmpty(state.Text.Trim()))//未处理的数据
            {
                //e.Row.Background = new SolidColorBrush(Colors.Red);
                //e.Row.Cells[RadGridView1.Columns.Count - 2].Background = new SolidColorBrush(Colors.White);

              //  e.Row.Background = new SolidColorBrush(Colors.Orange);
              //  e.Row.Cells[RadGridView1.Columns.Count - 2].Background = new SolidColorBrush(Colors.White);
                hlBtn.IsEnabled = true;
               // AddAlert(currentRow.ID, e.Row);
            }

        }
        private Dictionary<Guid, GridViewRowItem> dicDr = new Dictionary<Guid, GridViewRowItem>();
        private void AddAlert(Guid id,GridViewRowItem dr)
        {
            if (!dicDr.ContainsKey(id))
            {
                dicDr.Add(id, dr);
            }
        }
        private void RemoveAlert(Guid id,GridViewRowItem dr)
        {
            dicDr.Remove(id);
        }
        private bool flag = false;
        private void timer_Completed(object sender, EventArgs e)
        {
            //SolidColorBrush col = new SolidColorBrush(Color.FromArgb(255, 254, 63, 23));
            //if (flag)
            //    col = new SolidColorBrush(Color.FromArgb(255, 254, 181, 24));

            SolidColorBrush col = new SolidColorBrush(Colors.Orange);
            if (flag)
                col = new SolidColorBrush(Colors.White);

            foreach (GridViewRowItem dgr in dicDr.Values)
            {
                dgr.Background = col;
            }
            flag = !flag;
            timer.Begin();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //timer.Duration = new TimeSpan(0, 0, 0, 0, 500);
            //timer.Begin();
        }

        private Guid id;
        private void hlBtn_Click(object sender, RoutedEventArgs e)
        {
            DealAllFlag=false;
            HyperlinkButton hlB = sender as HyperlinkButton;
            id = (hlB.DataContext as DeviceAlarm).ID;
            RadWindow.Prompt("请输入备注：", new EventHandler<WindowClosedEventArgs>(OnClosed));
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            if (e.DialogResult != true) { return; }
            if (string.IsNullOrEmpty(e.PromptResult.ToString().Trim()))
            {
                MessageBox.Show("请输入确认信息!");
                return;
            }
            string getCommentInfo = e.PromptResult;
            Guid userGuid = App.CurUser.UserID;

            if (!DealAllFlag)
            {
                this._scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoAsync(id, DateTime.Now, getCommentInfo, userGuid);
                var obj = AlarmListVM.DeviceAlarmList.SingleOrDefault(x => x.ID == id);
            }
            else//处理全部告警信息
            {
                this._scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoBatchAsync(UpdateCount, DateTime.Now, getCommentInfo, userGuid);
            }
        }

        /// <summary>
        /// 处理完成后刷新界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoCompleted(object sender, UpdateDeviceAlarmInfoCompletedEventArgs e)
        {
            AlarmListVM.GetData();
        }
        /// <summary>
        /// 批量修改，刷新页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoBatchCompleted(object sender, UpdateDeviceAlarmInfoBatchCompletedEventArgs e)
        {
            AlarmListVM.GetData();
        }

        #endregion
        /// <summary>
        /// 处理全部告警
        /// </summary>
        private bool DealAllFlag = true;
        private void btnDealAll_Click(object sender, RoutedEventArgs e)
        {
            if (RadGridView1.ItemsSource == null)
            {
                MessageBox.Show("没有需要更新的数据!");
                return;
            }
            else
            {
                DealAllFlag = true;
                RadWindow.Prompt("请输入备注：", new EventHandler<WindowClosedEventArgs>(OnClosed));
            }
        }
    }
}
