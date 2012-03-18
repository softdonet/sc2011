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



namespace Scada.Client.UC
{

    /// <summary>
    /// 设备信息--历史平均温度统计图
    /// </summary>
    public partial class HistoryAvgValue : UserControl
    {

        #region 变量声明

        private List<ChartSource> _chartSource;

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
                ChartSource item = _chartSource[i];
                datapoint = new DataPoint();
                //datapoint.XValue = item.DeviceDate.ToString("yyyy-MM-dd HH:mm:ss");
                datapoint.XValue = i;
                datapoint.YValue = item.DeviceTemperature;
                datapoint.ToolTipText = string.Format("{0},{1}", datapoint.XValue, datapoint.YValue);
                dataSeries.DataPoints.Add(datapoint);
            }
            this.charTemperature.Series.Add(dataSeries);
        }

        public void SetDeviceTemperature(List<ChartSource> source)
        {
            this._chartSource = source;
            this.SetDeviceTemperature();
        }

        #endregion

    }
}
