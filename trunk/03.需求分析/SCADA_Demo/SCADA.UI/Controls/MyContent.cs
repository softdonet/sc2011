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
    public class MyContent : ContentControl
    {
        public event EventHandler CloseBtn;
        public event EventHandler Savebtn;

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MyContent), new PropertyMetadata("标题"));


        public Visibility ShowSaveBtn
        {
            get { return (Visibility)GetValue(ShowSaveBtnProperty); }
            set { SetValue(ShowSaveBtnProperty, value); }
        }

        public static readonly DependencyProperty ShowSaveBtnProperty =
            DependencyProperty.Register("ShowSaveBtn", typeof(Visibility), typeof(MyContent), new PropertyMetadata(Visibility.Collapsed));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Button closebtn = this.GetTemplateChild("closebtn") as Button;
            if (closebtn != null)
            {
                closebtn.Click += new RoutedEventHandler(OnMyWinClosed);
            }

            Button savebtn = this.GetTemplateChild("savebtn") as Button;
            if (savebtn != null)
            {
                savebtn.Click += new RoutedEventHandler(OnMyWinSave);
            }
        }
              
        public MyContent()
        {
            this.DefaultStyleKey = typeof(MyContent);
        }

        protected void OnMyWinClosed(object sender, RoutedEventArgs e)
        {
            if (CloseBtn != null)
            {
                CloseBtn(this, new EventArgs());
            }
        }

        protected void OnMyWinSave(object sender, RoutedEventArgs e)
        {
            if (Savebtn != null)
            {
                Savebtn(this, new EventArgs());
            }
        }
    }
}
