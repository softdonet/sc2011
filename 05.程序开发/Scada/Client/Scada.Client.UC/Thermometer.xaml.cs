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
using Scada.Model.Entity;

namespace Scada.Client.UC
{
    public partial class Thermometer : UserControl
    {
        SystemGlobalParameter _sysGlobalPar = null;
        public SystemGlobalParameter SysGlobalPar
        {
            get { return _sysGlobalPar; }
            set { _sysGlobalPar = value; }
        }

        public Thermometer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化区域
        /// </summary>
        public void Init()
        {
            this.myTemp.CircularScales[0].Minimum = SysGlobalPar.ChartMinTemp;
            this.myTemp.CircularScales[0].Maximum = SysGlobalPar.ChartMaxTemp;
            this.myTemp.Ranges[0].StartValue = SysGlobalPar.ChartMinTemp;
            this.myTemp.Ranges[0].EndValue = SysGlobalPar.ChartLowTemp;
            this.myTemp.Ranges[1].StartValue = SysGlobalPar.ChartLowTemp;
            this.myTemp.Ranges[1].EndValue = SysGlobalPar.ChartHighTemp;
            this.myTemp.Ranges[2].StartValue = SysGlobalPar.ChartHighTemp;
            this.myTemp.Ranges[2].EndValue = SysGlobalPar.ChartMaxTemp;
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
