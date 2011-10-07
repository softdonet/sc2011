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


using Visifire.Gauges;

namespace SCADA.UI.SampleData
{
    public partial class Thermometer : UserControl
    {

        public Thermometer()
        {
            InitializeComponent();

        }

        private void CreateGauge()
        {
            // Create a gauge
            Gauge gauge = new Gauge();
       
            gauge.Width = 300;
            gauge.Height = 300;
                        

            // Create a Needle Indicator
            NeedleIndicator indicator = new NeedleIndicator();
            indicator.Value = 87;

            // Add indicator to Indicators collection of gauge
            gauge.Indicators.Add(indicator);

            // Add gauge to the LayoutRoot for display
            LayoutRoot.Children.Add(gauge);
        }

    }
}
