﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Visifire.Gauges;

namespace Scada.Client.UC
{
    public class MyGauge : Gauge
    {
        protected override void LoadWm(GaugeTypes type)
        {
            //base.LoadWm(type);
            CreateWmElement(GaugeTypes.Circular, string.Empty, string.Empty);
        }
    }
}
