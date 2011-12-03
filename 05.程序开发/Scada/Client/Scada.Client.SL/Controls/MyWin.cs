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

namespace SCADA.UI.Controls
{
    public class MyWin : ContentControl
    {

        public event EventHandler CloseBtn;

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MyWin), new PropertyMetadata("标题"));

        public Visibility ShowCloseBtn
        {
            get { return (Visibility)GetValue(ShowCloseBtnProperty); }
            set { SetValue(ShowCloseBtnProperty, value); }
        }

        public static readonly DependencyProperty ShowCloseBtnProperty =
            DependencyProperty.Register("ShowCloseBtn", typeof(Visibility), typeof(MyWin), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ShowSaveBtn
        {
            get { return (Visibility)GetValue(ShowSaveBtnProperty); }
            set { SetValue(ShowSaveBtnProperty, value); }
        }

        public static readonly DependencyProperty ShowSaveBtnProperty =
            DependencyProperty.Register("ShowSaveBtn", typeof(Visibility), typeof(MyWin), new PropertyMetadata(Visibility.Collapsed));

        public Visibility ShowArrow
        {
            get { return (Visibility)GetValue(ShowArrowProperty); }
            set { SetValue(ShowArrowProperty, value); }
        }

        public static readonly DependencyProperty ShowArrowProperty =
            DependencyProperty.Register("ShowArrow", typeof(Visibility), typeof(MyWin), new PropertyMetadata(Visibility.Visible));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Button closebtn = this.GetTemplateChild("closebtn") as Button;
            if (closebtn != null)
            {
                closebtn.Click += new RoutedEventHandler(OnMyWinClosed);
            }
        }

        public MyWin()
        {
            this.DefaultStyleKey = typeof(MyWin);
        }

        protected void OnMyWinClosed(object sender, RoutedEventArgs e)
        {
            if (CloseBtn != null)
            {
                CloseBtn(this,new EventArgs());
            }
        }

       


    }
}
