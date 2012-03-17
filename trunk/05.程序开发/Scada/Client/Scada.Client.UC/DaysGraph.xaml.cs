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
            DateTime sameDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            this.charTemperature.AxesX[0].AxisMinimum = sameDay;
            this.charTemperature.AxesX[0].AxisMaximum = sameDay.AddDays(1);
        }

        #endregion


        #region 开放方法

        public void SetDeviceTemperature()
        {

            //1)Clear Chart Source
            this.charTemperature.Series.Clear();

            //2)Add Chart Source
            Visifire.Charts.DataSeries dataSeries = new Visifire.Charts.DataSeries();
            dataSeries.RenderAs = Visifire.Charts.RenderAs.Spline;
            dataSeries.ShowInLegend = false;
            dataSeries.SelectionEnabled = true;
            dataSeries.MarkerEnabled = false;
            dataSeries.MarkerScale = 0;
            dataSeries.LabelEnabled = false;
            dataSeries.XValueType = ChartValueTypes.DateTime;
            dataSeries.XValueFormatString = "yyyy-MM-dd HH:mm:ss";
            DataPoint datapoint;
            foreach (ChartSource item in _chartSource)
            {
                datapoint = new DataPoint();
                datapoint.XValue = item.DeviceDate.ToString("yyyy-MM-dd HH:mm:ss");
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
