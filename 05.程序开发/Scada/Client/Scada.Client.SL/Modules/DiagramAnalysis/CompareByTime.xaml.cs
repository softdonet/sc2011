﻿using System;
using System.Linq;
using System.Collections.Generic;

using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;

using Scada.Model.Entity;
using Scada.Utility.Common.SL.Enums;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;

using Visifire.Charts;
using Telerik.Windows.Controls.Calendar;
using Scada.Utility.Common.SL;





namespace Scada.Client.SL.Modules.DiagramAnalysis
{


    /// <summary>
    /// 按日期对比(同设备不同时间段)
    /// </summary>
    public partial class CompareByTime : UserControl
    {


        #region 变量声明

        private List<Color> _colorArr;

        private DateTime _friDate, _secDate, _thiDate, _fouDate;

        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient;

        #endregion


        #region 静态构造

        private static CompareByTime instance;
        public static CompareByTime GetInstance()
        {
            if (instance == null)
            {
                instance = new CompareByTime();
            }
            return instance;
        }

        public static DateTime GetEndDateTime(ref DateTime starDate, DateSelMode dateSelMode)
        {
            DateTime result;
            if (dateSelMode == DateSelMode.天)
                result = starDate.AddDays(1);
            else if (dateSelMode == DateSelMode.月)
            {
                starDate = new DateTime(starDate.Year, starDate.Month, 1, 0, 0, 0);
                result = starDate.AddMonths(1);
            }
            else
                result = DateTime.Now;
            return result;
        }

        #endregion


          #region 构造函数

        public CompareByTime()
        {

            InitializeComponent();
            SetColorArea();

            //选择网元
            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            this._scadaDeviceServiceSoapClient.GetDeviceTreeListCompleted +=
                                            new EventHandler<GetDeviceTreeListCompletedEventArgs>
                                            (scadaDeviceServiceSoapClient_GetDeviceTreeListCompleted);
            this._scadaDeviceServiceSoapClient.GetDeviceTreeListAsync();

            //选择周期
            this.cmbSelDateMode.Items.Clear();
            object[] selMode = EnumHelper.GetValues(typeof(DateSelMode));
            foreach (var item in selMode)
            {
                this.cmbSelDateMode.Items.Add(item.ToString());
            }
            if (this.cmbSelDateMode.Items.Count > 0)
                this.cmbSelDateMode.SelectedIndex = 0;
            this.cmbSelDateMode.SelectionChanged += new SelectionChangedEventHandler(cmbSelDateMode_SelectionChanged);


            //选择日期
            this._friDate = DateTime.Now;
            this._secDate = DateTime.Now.AddDays(-1);
            this._thiDate = DateTime.Now.AddDays(-2);
            this._fouDate = DateTime.Now.AddDays(-3);

            this.dateFrist.SelectedValue = this._friDate;
            this.dateSecond.SelectedValue = this._secDate;
            this.dateThird.SelectedValue = this._thiDate;
            this.dateFour.SelectedValue = this._fouDate;

            //初期化图表
            this.charTemperature.Series.Clear();

       


        }

        #endregion
       


        #region 私有方法

        /// <summary>
        /// 设置颜色区域
        /// </summary>
        void SetColorArea()
        {
            //0))Clear Chart TrendLines
            this.charTemperature.TrendLines.Clear();

            TrendLine t1 = new TrendLine();
            t1.Orientation = Orientation.Horizontal;
            t1.StartValue = App.SysGlobalPar.ChartMinTemp;
            t1.EndValue = App.SysGlobalPar.ChartLowTemp;
            t1.LineColor = new SolidColorBrush("#9FEAF451".ToColor());

            TrendLine t2 = new TrendLine();
            t2.Orientation = Orientation.Horizontal;
            t2.StartValue = App.SysGlobalPar.ChartLowTemp;
            t2.EndValue = App.SysGlobalPar.ChartHighTemp;
            t2.LineColor = new SolidColorBrush("#9F9AD846".ToColor());

            TrendLine t3 = new TrendLine();
            t3.Orientation = Orientation.Horizontal;
            t3.StartValue = App.SysGlobalPar.ChartHighTemp;
            t3.EndValue = App.SysGlobalPar.ChartMaxTemp;
            t3.LineColor = new SolidColorBrush("#9FF86D5A".ToColor());

            this.charTemperature.TrendLines.Add(t1);
            this.charTemperature.TrendLines.Add(t2);
            this.charTemperature.TrendLines.Add(t3);
            this.charTemperature.AxesY[0].AxisMaximum = App.SysGlobalPar.ChartMaxTemp;
            this.charTemperature.AxesY[0].AxisMinimum = App.SysGlobalPar.ChartMinTemp;
        }

        private void scadaDeviceServiceSoapClient_GetDeviceTreeListCompleted(object sender, GetDeviceTreeListCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string msgInfo = e.Result;
                List<DeviceTreeNode> devicTreeList = BinaryObjTransfer.BinaryDeserialize<List<DeviceTreeNode>>(msgInfo);
                this.cmbSelDevice.ItemsSource = devicTreeList;
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        private void scadaDeviceServiceSoapClient_GetSameDeviceTemperatureDiffDateCompleted(object sender,
                                                    GetSameDeviceTemperatureDiffDateCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string msgInfo = e.Result;
                Dictionary<DateTime, List<ChartSource>> chartSource =
                    BinaryObjTransfer.BinaryDeserialize<Dictionary<DateTime, List<ChartSource>>>(msgInfo);
                if (chartSource.Count() == 0) { return; }

                //Clear ChartSource
                this.charTemperature.Series.Clear();

                //更改X轴间隔
                Int32 dateSelMode = this.cmbSelDateMode.SelectedIndex;
                this.charTemperature.AxesX[0].Title = SetChartAxesValue(dateSelMode);
                string formatDate = SetChartAxesXFormat(this.charTemperature.AxesX[0], dateSelMode);


                DateTime st = DateTime.Now;
                DateTime ed = DateTime.Now;
                if (dateSelMode == 0)
                {
                    st = Convert.ToDateTime("2001-01-01 00:00:00");
                    ed = Convert.ToDateTime("2001-01-02 00:00:00");
                }
                else if (dateSelMode == 1)
                {
                    st = Convert.ToDateTime("2001-01-01 00:00:00");
                    ed = Convert.ToDateTime("2001-02-01 00:00:00");
                }
                this.charTemperature.AxesX[0].AxisMinimum = st;
                this.charTemperature.AxesX[0].AxisMaximum = ed;

                int i = 0;
                foreach (var item in chartSource)
                {
                    this.AddService(String.Empty, this._colorArr[i], item.Value, formatDate, dateSelMode);
                    i++;
                }
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }


        public static String SetChartAxesValue(Int32 dateSelMode)
        {
            String result = String.Empty;
            if (dateSelMode == 0)
                result = "x轴按小时分布";
            else if (dateSelMode == 1)
                result = "x轴按天分布";
            else if (dateSelMode == 2)
                result = "x轴按月分布";
            return result;
        }

        private void AddService(string serviceName, Color color, List<ChartSource> source, string formatdate, Int32 dateSelMode)
        {

            Visifire.Charts.DataSeries dataSeries = new Visifire.Charts.DataSeries();
            dataSeries.RenderAs = Visifire.Charts.RenderAs.Line;
            dataSeries.ShowInLegend = false;
            dataSeries.SelectionEnabled = true;
            dataSeries.MarkerEnabled = false;
            dataSeries.LabelEnabled = false;
            dataSeries.XValueType = ChartValueTypes.DateTime;
            dataSeries.XValueFormatString = "yyyy-MM-dd HH:mm:ss";
            dataSeries.Color = new SolidColorBrush(color);
            dataSeries.LineThickness = 1.5;

            DataPoint datapoint;
            foreach (ChartSource item in source)
            {
                datapoint = new DataPoint();
                datapoint.XValue = item.DeviceDate.ToString(formatdate);
                datapoint.YValue = item.DeviceTemperature;
                datapoint.ToolTipText = string.Format("{0},{1}", item.DeviceDate, datapoint.YValue);
                dataSeries.DataPoints.Add(datapoint);
            }
            this.charTemperature.Series.Add(dataSeries);
        }

        public static string SetChartAxesXFormat(Axis axis, Int32 scale)
        {
            string result = string.Empty;
            if (scale == 0)
            {
                axis.ValueFormatString = "HH";
                axis.IntervalType = IntervalTypes.Hours;
                result = "2001-01-01 HH:mm:ss";
            }
            else if (scale == 1)
            {
                axis.ValueFormatString = "dd";
                axis.IntervalType = IntervalTypes.Days;
                result = "2001-01-dd HH:mm:ss";
            }
            return result;
        }



        #endregion


        #region 事件处理

        private void cmbSelDateMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int dateSelMode = this.cmbSelDateMode.SelectedIndex;
            this.dateFrist.DateSelectionMode =
                this.dateSecond.DateSelectionMode =
                this.dateThird.DateSelectionMode =
                    this.dateFour.DateSelectionMode = (DateSelectionMode)Enum.ToObject(typeof(DateSelectionMode), dateSelMode);
        }

        private void butViewCompare_Click(object sender, RoutedEventArgs e)
        {

            //Check Condition
            this._friDate = (DateTime)this.dateFrist.DisplayDate;
            this._secDate = (DateTime)this.dateSecond.DisplayDate;
            this._thiDate = (DateTime)this.dateThird.DisplayDate;
            this._fouDate = (DateTime)this.dateFour.DisplayDate;

            List<DateTime> checkDateFrom = new List<DateTime>();
            this._colorArr = new List<Color>();
            if ((Boolean)chkFrist.IsChecked)
            {
                checkDateFrom.Add(this._friDate);
                this._colorArr.Add(Colors.Red);
            }
            if ((Boolean)chkSecond.IsChecked)
            {
                checkDateFrom.Add(this._secDate);
                this._colorArr.Add(Color.FromArgb(255, 30, 144, 255));
            }
            if ((Boolean)chkThird.IsChecked)
            {
                checkDateFrom.Add(this._thiDate);
                this._colorArr.Add(Color.FromArgb(255, 154, 205, 50));
            }
            if ((Boolean)chkFour.IsChecked)
            {
                checkDateFrom.Add(this._fouDate);
                this._colorArr.Add(Color.FromArgb(255, 192, 192, 192));
            }

            if (checkDateFrom.Count == 0)
            {
                ScadaMessageBox.ShowWarnMessage("请选择要比较的日期！！！", "重要提示");
                return;
            }

            IEnumerable<DateTime> checkDateTo = checkDateFrom.Distinct();
            if (checkDateTo.Count() != checkDateFrom.Count())
            {
                ScadaMessageBox.ShowWarnMessage("有相同日期进行比较！！！", "重要提示");
                return;
            }

            //Check Device
            object device = this.cmbSelDevice.SelectedItem;
            if (device == null)
            {
                ScadaMessageBox.ShowWarnMessage("请选择要比较的设备信息！！！", "重要提示");
                return;
            }
            DeviceTreeNode deviceTreeNode = device as DeviceTreeNode;
            Int32 dateSelMode = this.cmbSelDateMode.SelectedIndex;
            string jsonDates = BinaryObjTransfer.BinarySerialize(checkDateFrom);
            this._scadaDeviceServiceSoapClient.GetSameDeviceTemperatureDiffDateCompleted +=
                                          new EventHandler<GetSameDeviceTemperatureDiffDateCompletedEventArgs>
                                          (scadaDeviceServiceSoapClient_GetSameDeviceTemperatureDiffDateCompleted);
            this._scadaDeviceServiceSoapClient.GetSameDeviceTemperatureDiffDateAsync(deviceTreeNode.NodeType, deviceTreeNode.NodeKey, dateSelMode, jsonDates);

        }

        #endregion

    }

    public enum DateSelMode
    {
        天 = 0,
        月 = 1
    }

}
