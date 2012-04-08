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

namespace Scada.Client.SL.Controls
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
            Application.Current.Host.Content.FullScreenChanged += new EventHandler(Content_FullScreenChanged);
        }

        void Content_FullScreenChanged(object sender, EventArgs e)
        {
            if (Application.Current.Host.Content.IsFullScreen)
            {
                btnFull.Content = "退出全屏";
                ToolTipService.SetToolTip(btnFull, "点击退出全屏");
            }
            else
            {
                btnFull.Content = "全屏模式";
                ToolTipService.SetToolTip(btnFull, "点击放大到全屏");
            }
        }

        private void btnFull_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Host.Content.IsFullScreen)
            {
                Application.Current.Host.Content.IsFullScreen = false;
                btnFull.Content = "全屏模式";
                ToolTipService.SetToolTip(btnFull, "点击放大到全屏");
            }
            else
            {
                Application.Current.Host.Content.IsFullScreen = true;
                btnFull.Content = "退出全屏";
                ToolTipService.SetToolTip(btnFull, "点击退出全屏");
            }
        }
    }
}
