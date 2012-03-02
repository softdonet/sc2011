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
    public partial class StatisticsAnalyse : UserControl
    {
        #region 单例
        private static UserControl instance;
        public static UserControl GetInstance()
        {
            if (instance==null)
            {
                instance = new UserControl();
            }
            return instance;
        }
        #endregion
        public StatisticsAnalyse()
        {
            InitializeComponent();
        }
    }
}
