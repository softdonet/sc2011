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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace Scada.Client.SL.Modules.UsersEvent
{
    public partial class UserEvent : UserControl
    {
        public UserEvent()
        {
            InitializeComponent();
        }

        private void RadGridView1_RowLoaded(object sender, Telerik.Windows.Controls.GridView.RowLoadedEventArgs e)
        {
            //TextBlock state = (e.Row.Cells[RadGridView1.Columns.Count - 2].Content as FrameworkElement) as TextBlock;
            //HyperlinkButton hlUrl = (e.Row.Cells[RadGridView1.Columns.Count - 1].Content as FrameworkElement).FindName("hlUrl") as HyperlinkButton;
            //if (state.Text.Trim() == "未确认")
            //{
            //    e.Row.Background = new SolidColorBrush(Colors.Red);
            //    e.Row.Cells[RadGridView1.Columns.Count - 1].Background = new SolidColorBrush(Colors.White);
            //    hlUrl.IsEnabled = true;
            //    AddAlert(e.Row);
            //}
        }
        Dictionary<GridViewRowItem, GridViewRowItem> dicDr = new Dictionary<GridViewRowItem, GridViewRowItem>();
        private void AddAlert(GridViewRowItem dr)
        {
            if (!dicDr.ContainsKey(dr))
            {
                dicDr.Add(dr, dr);
            }
        }



        private void hlUrl_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Prompt("请输入备注：", new EventHandler<WindowClosedEventArgs>(OnClosed));
        }

        private void OnClosed(object sender, WindowClosedEventArgs e)
        {
            //RadWindow.Alert(String.Format("DialogResult: {0}, PromptResult: {1}", e.DialogResult, e.PromptResult));
        }
    }
}
