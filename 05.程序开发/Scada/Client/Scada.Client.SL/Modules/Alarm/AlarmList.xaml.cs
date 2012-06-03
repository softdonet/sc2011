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
        }

        #endregion

        #region 事件处理

        private void RadGridView1_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {

            if (e.Row is GridViewHeaderRow)
                return;

            TextBlock state = (e.Row.Cells[RadGridView1.Columns.Count - 3].Content as FrameworkElement) as TextBlock;
            HyperlinkButton hlBtn = (e.Row.Cells[RadGridView1.Columns.Count - 2].Content as FrameworkElement).FindName("hlBtn") as HyperlinkButton;
            if (string.IsNullOrEmpty(state.Text.Trim()))//未处理的数据
            {
                //e.Row.Background = new SolidColorBrush(Colors.Red);
                //e.Row.Cells[RadGridView1.Columns.Count - 2].Background = new SolidColorBrush(Colors.White);
                e.Row.Background = new SolidColorBrush(Colors.Orange);
                e.Row.Cells[RadGridView1.Columns.Count - 2].Background = new SolidColorBrush(Colors.White);
                hlBtn.IsEnabled = true;
                AddAlert(e.Row);
            }

        }
        private Dictionary<GridViewRowItem, GridViewRowItem> dicDr = new Dictionary<GridViewRowItem, GridViewRowItem>();
        private void AddAlert(GridViewRowItem dr)
        {
            if (!dicDr.ContainsKey(dr))
            {
                dicDr.Add(dr, dr);
            }
        }
        private void RemoveAlert(GridViewRowItem dr)
        {
            dicDr.Remove(dr);
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

            foreach (GridViewRowItem dgr in dicDr.Keys)
            {
                dgr.Background = col;
            }
            flag = !flag;
            timer.Begin();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Duration = new TimeSpan(0, 0, 0, 0, 500);
            timer.Begin();
        }

        private Guid id;
        private void hlBtn_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton hlB = sender as HyperlinkButton;
            id = (hlB.DataContext as DeviceAlarmViewModel).DeviceAlarm.ID;
            RadWindow.Prompt("请输入备注：", new EventHandler<WindowClosedEventArgs>(OnClosed));
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            if (e.DialogResult != true) { return; }
            string getCommentInfo = e.PromptResult;
            Guid userGuid = App.CurUser.UserID;
            this._scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoAsync(id, DateTime.Now, getCommentInfo,userGuid);
            var obj = AlarmListVM.DeviceAlarmList.SingleOrDefault(x => x.DeviceAlarm.ID == id);
            if (obj != null)
            {
                obj.Comment = getCommentInfo;
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
        #endregion

    }
}
