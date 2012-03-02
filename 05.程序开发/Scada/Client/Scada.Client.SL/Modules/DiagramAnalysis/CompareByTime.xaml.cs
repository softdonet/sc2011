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

namespace Scada.Client.SL.Modules.DiagramAnalysis
{
    public partial class CompareByTime : UserControl
    {
        private static CompareByTime instance;
        public static CompareByTime GetInstance()
        {
            if (instance==null)
            {
                instance = new CompareByTime();
            }
            return instance;
        }
        public CompareByTime()
        {
            InitializeComponent();
        }
    }
}
