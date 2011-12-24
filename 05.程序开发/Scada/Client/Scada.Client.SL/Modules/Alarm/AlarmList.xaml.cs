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
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Controls;






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

        public AlarmList()
        {

            InitializeComponent();

            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            this._scadaDeviceServiceSoapClient.GetListDeviceAlarmInfoCompleted +=
                    new EventHandler<GetListDeviceAlarmInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            this._scadaDeviceServiceSoapClient.GetListDeviceAlarmInfoAsync();

            this._scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoCompleted +=
                new EventHandler<UpdateDeviceAlarmInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoCompleted);

        }

        private void scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted(object sender, GetListDeviceAlarmInfoCompletedEventArgs e)
        {
            List<DeviceAlarm> deviceAlam = BinaryObjTransfer.BinaryDeserialize<List<DeviceAlarm>>(e.Result);
            this.RadGridView1.ItemsSource = deviceAlam;
        }

        #endregion


        #region 事件处理

        private void RadGridView1_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            TextBlock state = (e.Row.Cells[RadGridView1.Columns.Count - 2].Content as FrameworkElement) as TextBlock;
            HyperlinkButton hlBtn = (e.Row.Cells[RadGridView1.Columns.Count - 1].Content as FrameworkElement).FindName("hlBtn") as HyperlinkButton;
            if (state.Text.Trim() == "未确认")
            {
                e.Row.Background = new SolidColorBrush(Colors.Red);
                e.Row.Cells[RadGridView1.Columns.Count - 1].Background = new SolidColorBrush(Colors.White);
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
            SolidColorBrush col = new SolidColorBrush(Color.FromArgb(255, 254, 63, 23));
            if (flag)
                col = new SolidColorBrush(Color.FromArgb(255, 254, 181, 24));
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

        private void hlBtn_Click(object sender, RoutedEventArgs e)
        {

            RadWindow.Prompt("请输入备注：", new EventHandler<WindowClosedEventArgs>(OnClosed));
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            if (e.DialogResult != true) { return; }
            string getCommentInfo = e.PromptResult;
            this._scadaDeviceServiceSoapClient.UpdateDeviceAlarmInfoAsync(Guid.NewGuid(), DateTime.Now, getCommentInfo, "Admin");
        }

        private void scadaDeviceServiceSoapClient_UpdateDeviceAlarmInfoCompleted(object sender, UpdateDeviceAlarmInfoCompletedEventArgs e)
        {
            Boolean result = e.Result;
        }


        #endregion



    }
}
