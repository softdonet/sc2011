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
using System.Windows.Media.Imaging;

namespace SCADA.UI.Modules.Alarm
{
    public partial class AlarmList : UserControl
    {



        public AlarmList()
        {
            InitializeComponent();
            MyContent.CloseBtn += new EventHandler(MyContent_CloseBtn);
            timer.Duration = new TimeSpan(0, 0, 0, 0, 2000); //200毫秒        
            timer.Begin();

        }

        Dictionary<DataGridRow, DataGridRow> dicDr = new Dictionary<DataGridRow, DataGridRow>();
        private void AddAlert(DataGridRow dr)
        {
            if (!dicDr.ContainsKey(dr))
            {
                dicDr.Add(dr, dr);
            }
        }
        private void RemoveAlert(DataGridRow dr)
        {
            dicDr.Remove(dr);
        }
        bool flag = false;
        DataGridRow dgr = null;
        void timer_Tick(object sender, EventArgs e)
        {
            //在这里处理定时器事件
             SolidColorBrush col = new SolidColorBrush(Colors.Yellow );
             //if (flag)
             //{
             //    col = new SolidColorBrush(Colors.Yellow);

             //}
             dgr.Background = col;
            //foreach (DataGridRow dgr in dicDr.Keys)
            //{
            //    dgr.Background = col;
            //}
           
            flag = !flag;
            timer.Begin();

        }
        void MyContent_CloseBtn(object sender, EventArgs e)
        {
            Storyboard2.Begin();
            ViewHost.Visibility = Visibility.Collapsed;
        }

        private void dgrdList_LoadingRow(object sender, DataGridRowEventArgs e)
        {

            Expression.Blend.SampleData.DataGridViewSampleDataSource.Item item =
                e.Row.DataContext as Expression.Blend.SampleData.DataGridViewSampleDataSource.Item;

            if (item.DealWith == "未确认")
            {
                //e.Row.Background = new SolidColorBrush(Colors.Red);
                //ImageBrush img = new ImageBrush();
                //img.ImageSource = new BitmapImage(new Uri("Image/gif009.gif", UriKind.Relative));
                //e.Row.Background = img;
                //dicDr.Add(e.Row, e.Row);
                dgr = e.Row;
            }
            #region bg

            //(Expression.Blend.SampleData.DataGridViewSampleDataSource.Item)
            //string aa = ((this.dgrdList.Columns[1].GetCellContent(e.Row)) as TextBlock).Text;
            //------------------------------
            //设置行背景色

            // SolidColorBrush r = new SolidColorBrush(Color.FromArgb(180, 180, 0, 0));

            // e.Row.Background = r;

            // //设置列颜色

            //// (this.dgrdList.Columns[1].GetCellContent(e.Row)).Foreground = r;
            // foreach (var item in dgrdList.Columns)
            // {

            // }
            #endregion
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
            if (columnValue == "未确认")
            {
                Storyboard1.Begin();
                //MyContent.Content = new ConfirmPage();
                MyContent.Title = "确认信息";
            }

        }

    }
}
