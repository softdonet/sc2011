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

namespace SCADA.UI.Modules.Alarm
{
    public partial class AlarmList : UserControl
    {
        public AlarmList()
        {
            InitializeComponent();
          //  MyContent.CloseBtn += new EventHandler(MyContent_CloseBtn);

        }

        void MyContent_CloseBtn(object sender, EventArgs e)
        {
            Storyboard2.Begin();
          //  ViewHost.Visibility = Visibility.Collapsed;
        }

        private void dgrdList_LoadingRow(object sender, DataGridRowEventArgs e)
        {
 
            //------------------------------
            //设置行背景色

           // SolidColorBrush r = new SolidColorBrush(Color.FromArgb(180, 180, 0, 0));

           // e.Row.Background = r;

           // //设置列颜色

           //// (this.dgrdList.Columns[1].GetCellContent(e.Row)).Foreground = r;
           // foreach (var item in dgrdList.Columns)
           // {
                
           // }
//-----------------------------------------------------
            e.Row.MouseLeftButtonUp -= new System.Windows.Input.MouseButtonEventHandler(Row_MouseLeftButtonUp);
            e.Row.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(Row_MouseLeftButtonUp);
        }

        void Row_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //foreach (object ovj in dgrdList.ItemsSource)
            //{
            //    string aa = ((TextBox)dgrdList.Columns[1].GetCellContent(ovj)).Text;

            //}
            //string sColumnValue = ((TextBlock)this.dgrdList.Columns[1].GetCellContent(this.dgrdList.SelectedItem)).Text.Trim();

            string columnValue = ((Expression.Blend.SampleData.DataGridViewSampleDataSource.Item)(this.dgrdList.SelectedItem)).DealWith;
            if (columnValue == "确认")
            {
                Storyboard1.Begin();
             //   MyContent.Content = new ConfirmPage();
              //  MyContent.Title = "确认信息";
            }

        }

    }
}
