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
    public partial class CompareByDevice : UserControl
    {
        private static CompareByDevice instance;
        public static CompareByDevice GetInstance()
        {
            if (instance==null)
            {
                instance = new CompareByDevice();
            }
            return instance;
        }

        public CompareByDevice()
        {
            InitializeComponent();
        }
    }
}
