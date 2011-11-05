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
using Telerik.Windows.Controls.GridView;
using SCADA.UI.Modules.Device;
using Telerik.Windows.Controls;

namespace SCADA.UI.Modules.Alarm
{
    public partial class AlertListNew : UserControl
    {
        public AlertListNew()
        {
            InitializeComponent();
            MyContent.CloseBtn += new EventHandler(MyContent_CloseBtn);
        }
        void MyContent_CloseBtn(object sender, EventArgs e)
        {
            Storyboard2.Begin();
            ViewHost.Visibility = Visibility.Collapsed;
        }
        Dictionary<GridViewRowItem, GridViewRowItem> dicDr = new Dictionary<GridViewRowItem, GridViewRowItem>();
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
        bool flag = false;

        void timer_Tick(object sender, EventArgs e)
        {
            //在这里处理定时器事件
            SolidColorBrush col = new SolidColorBrush(Color.FromArgb(255, 254, 63, 23));
            if (flag)
            {
                col = new SolidColorBrush(Color.FromArgb(255, 254, 181, 24));
            }

            foreach (GridViewRowItem dgr in dicDr.Keys)
            {
                dgr.Background = col;
            }

            flag = !flag;
            timer.Begin();

        }
        private void RadGridView1_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            TextBlock state = (e.Row.Cells[RadGridView1.Columns.Count - 2].Content as FrameworkElement) as TextBlock;
            HyperlinkButton hlUrl = (e.Row.Cells[RadGridView1.Columns.Count - 1].Content as FrameworkElement).FindName("hlUrl") as HyperlinkButton;
            if (state.Text.Trim() == "未确认")
            {
                e.Row.Background = new SolidColorBrush(Colors.Red);
                e.Row.Cells[RadGridView1.Columns.Count - 1].Background = new SolidColorBrush(Colors.White);
                hlUrl.IsEnabled = true;
                AddAlert(e.Row);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Duration = new TimeSpan(0, 0, 0, 0, 500); //200毫秒        
            timer.Begin();
        }

        private void hlUrl_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Prompt("请输入备注：", new EventHandler<WindowClosedEventArgs>(OnClosed));
        }
        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            //RadWindow.Alert(String.Format("DialogResult: {0}, PromptResult: {1}", e.DialogResult, e.PromptResult));
        }
        private void hlLook_Click(object sender, RoutedEventArgs e)
        {
            Storyboard1.Begin();
            MyContent.Content = new DetailsPage();
            MyContent.Title = "设备详细信息";
        }
    }
}
