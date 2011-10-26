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
using System.Windows.Navigation;

namespace SCADA.UI.Modules.Alarm
{
    public partial class ConfirmPage : Page
    {
        public ConfirmPage()
        {
            InitializeComponent();
            txtConform.Text = "请输入您的的确认信息...";
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
