using System;
using System.Linq;
using System.Collections.Generic;


using System.Net;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;


using Visifire.Charts;
using Scada.Model.Entity;

using Scada.Utility.Common.SL;

namespace Scada.Client.UC
{

    /// <summary>
    /// 设备信息--历史平均温度统计图
    /// </summary>
    public partial class HistoryAvgValue : UserControl
    {

        #region 变量声明

        private List<ChartSource> _chartSource;


        SystemGlobalParameter _sysGlobalPar = null;
        public SystemGlobalParameter SysGlobalPar
        {
            get { return _sysGlobalPar; }
            set { _sysGlobalPar = value; }
        }

        #endregion

        #region 构造函数

        public HistoryAvgValue()
        {

            InitializeComponent();

        }

        public HistoryAvgValue(List<ChartSource> source)
        {
            this._chartSource = source;
            InitializeComponent();
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

            //2)Add Chart Source
            Visifire.Charts.DataSeries dataSeries = new Visifire.Charts.DataSeries();
            dataSeries.RenderAs = Visifire.Charts.RenderAs.Column;
            dataSeries.ShowInLegend = false;
            dataSeries.SelectionEnabled = true;
            dataSeries.MarkerEnabled = false;
            dataSeries.MarkerScale = 0;
            dataSeries.LabelEnabled = false;
            dataSeries.Color = new SolidColorBrush(Colors.Red);

            DataPoint datapoint;
            for (int i = 0; i < _chartSource.Count; i++)
            {

                if (i == 0)
                {
                    datapoint = new DataPoint();
                    datapoint.XValue = 0;
                    datapoint.YValue = 0;
                    dataSeries.DataPoints.Add(datapoint);
                }

                ChartSource item = _chartSource[i];
                datapoint = new DataPoint();
                datapoint.XValue = i + 1;
                datapoint.YValue = Math.Round(item.DeviceTemperature, 2);
                datapoint.ToolTipText = string.Format("{0},{1}", datapoint.XValue, datapoint.YValue);
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
