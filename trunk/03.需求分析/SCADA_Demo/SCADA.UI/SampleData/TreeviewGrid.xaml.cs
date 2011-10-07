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

namespace SCADA.UI.SampleData
{
    public partial class TreeviewGrid : UserControl
    {
        private List<DataSource> Products;
        public TreeviewGrid()
        {
            InitializeComponent();

            this.LoadDataDaource();
        }

        private void LoadDataDaource()
        {
            this.Products = new List<DataSource>();
            for (int i = 0; i < 10; i++)
            {
                this.Products.Add(
                    new DataSource
                    {
                        ProductID = i,
                        ProductName = "name" + i,
                        UnitPrice = 0.5 + i,
                        UnitsInStock = i
                    });
            }

            this.RadGridView1.ItemsSource = Products;

        }
    }

    public class DataSource
    {
        //区域名称 管理分区 片区名称 设备名称 安装地点 温度 压力 状态 百分比 信号强度
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public Int32 UnitsInStock { get; set; }
    }

}
