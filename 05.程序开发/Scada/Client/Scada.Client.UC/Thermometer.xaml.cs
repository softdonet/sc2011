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

namespace Scada.Client.UC
{
    public partial class Thermometer : UserControl
    {
        public Thermometer()
        {
            InitializeComponent();
        }

        public decimal? Temperature
        {
            set
            {
                if (value.HasValue)
                {
                    this.myTemp.Indicators[0].Value = (double)value;
                }
            }
        }
    }
}
