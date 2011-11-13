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
    public partial class DaysGraphNew : UserControl
    {
        public DaysGraphNew()
        {
            InitializeComponent();
        }
        public string Title
        {
            get { return this.chart1.Titles[0].Text ; }
            set { this.chart1.Titles[0].Text = value; }
        }
    }
}
