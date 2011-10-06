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
using System.Windows.Browser;

namespace SCADA.UI.Controls
{
    public partial class Login : UserControl
    {
       public delegate void myKeyDownHandle();
       public event myKeyDownHandle myKeyDowmEvent;

        public Login()
        {
            InitializeComponent();
            txtTile.Text = "管网SCADA系统";
            this.Loaded += new RoutedEventHandler(Login_Loaded);
            
        }

        void Login_Loaded(object sender, RoutedEventArgs e)
        {
            HtmlPage.Plugin.Focus();
            txbName.Focus();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (myKeyDowmEvent != null)
            {
                this.myKeyDowmEvent();
            }
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (myKeyDowmEvent != null)
                {
                    this.myKeyDowmEvent();
                }
            }
        }
    }
}
