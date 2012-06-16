using System;
using System.Linq;
using System.Collections.Generic;



using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Animation;




using Scada.Model.Entity;
using Visifire.Charts;
using Scada.Utility.Common.SL;



namespace Scada.Client.UC
{

    /// <summary>
    /// 设备信息--本天温度曲线值
    /// </summary>
    public partial class DaysGraph : UserControl
    {

        #region 变量声明

        private List<ChartSource> _chartSource;

        #endregion


        #region 构造函数

        public DaysGraph()
        {

            InitializeComponent();

            this.Init();
        }

        SystemGlobalParameter _sysGlobalPar = null;
        public SystemGlobalParameter SysGlobalPar
        {
            get { return _sysGlobalPar; }
            set { _sysGlobalPar = value; }
        }

        public DaysGraph(List<ChartSource> source)
        {
            this._chartSource = source;
            InitializeComponent();
            this.Init();
        }

        #endregion


        #region 界面初期化

        private void Init()
        {

        }

        #endregion


        #region 开放方法

        public void SetDeviceTemperature()
        {
            //0))Clear Chart TrendLines
            this.charTemperature.TrendLines.Clear();

            TrendLine t1 = new TrendLine();
            t1.Orientation = Orientation.Horizontal;
            t1.StartValue = _sysGlobalPar.ChartMinTemp;
            t1.EndValue = _sysGlobalPar.ChartLowTemp;
            t1.LineColor = new SolidColorBrush("#9FEAF451".ToColor());

            TrendLine t2 = new TrendLine();
            t2.Orientation = Orientation.Horizontal;
            t2.StartValue = _sysGlobalPar.ChartLowTemp;
            t2.EndValue = _sysGlobalPar.ChartHighTemp;
            t2.LineColor = new SolidColorBrush("#9F9AD846".ToColor());
          
            TrendLine t3 = new TrendLine();
            t3.Orientation = Orientation.Horizontal;
            t3.StartValue = _sysGlobalPar.ChartHighTemp;
            t3.EndValue = _sysGlobalPar.ChartMaxTemp;
            t3.LineColor = new SolidColorBrush("#9FF86D5A".ToColor());

            this.charTemperature.TrendLines.Add(t1);
            this.charTemperature.TrendLines.Add(t2);
            this.charTemperature.TrendLines.Add(t3);
            this.charTemperature.AxesY[0].AxisMaximum = _sysGlobalPar.ChartMaxTemp;
            this.charTemperature.AxesY[0].AxisMinimum = _sysGlobalPar.ChartMinTemp;

            //1)Clear Chart Source
            this.charTemperature.Series.Clear();

            //2)Setting AxesX Min Max
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime end = start.AddDays(1);
            this.charTemperature.AxesX[0].AxisMinimum = start;
            this.charTemperature.AxesX[0].AxisMaximum = end;

            //3)Add Chart Source
            Visifire.Charts.DataSeries dataSeries = new Visifire.Charts.DataSeries();
            dataSeries.RenderAs = Visifire.Charts.RenderAs.Line;
            dataSeries.ShowInLegend = false;
            dataSeries.SelectionEnabled = true;
            dataSeries.MarkerEnabled = false;
            dataSeries.MarkerScale = 0;
            dataSeries.LabelEnabled = false;
            dataSeries.XValueType = ChartValueTypes.DateTime;
            dataSeries.XValueFormatString = "yyyy-MM-dd HH:mm:ss";
            dataSeries.LineThickness = 1.5;
            DataPoint datapoint;
            foreach (ChartSource item in _chartSource)
            {
                datapoint = new DataPoint();
                datapoint.XValue = item.DeviceDate;
                if (item.DeviceTemperature > 0)
                {
                    datapoint.YValue = Math.Round(item.DeviceTemperature, 2);
                    datapoint.ToolTipText = string.Format("{0},{1}", datapoint.XValue, datapoint.YValue);
                }
                dataSeries.DataPoints.Add(datapoint);
            }
            this.charTemperature.Series.Add(dataSeries);
        }

        public void SetDeviceTemperature(List<ChartSource> source)
        {

            if (source == null || source.Count == 0)
            {
                this.charTemperature.Series.Clear();
                return;
            }

            this._chartSource = source;
            this.SetDeviceTemperature();
        }

        #endregion


    }

}
