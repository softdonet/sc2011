﻿using System;
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

namespace Scada.Client.SL.Modules.Query
{
    public partial class AlarmQuery : UserControl
    {
        private AlarmQuery()
        {
            InitializeComponent();
        }
        private static AlarmQuery instance;
        public static AlarmQuery GetInstance()
        {
            if (instance==null)
            {
                instance = new AlarmQuery();
            }
            return instance;
        }
    }
}
