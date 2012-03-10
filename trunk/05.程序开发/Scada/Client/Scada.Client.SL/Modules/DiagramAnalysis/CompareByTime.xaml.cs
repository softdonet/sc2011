using System;
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
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;

using Visifire.Charts;
using Telerik.Windows.Controls.Calendar;





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

        #endregion


        #region 构造函数

        public CompareByTime()
        {

            InitializeComponent();


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

                int i = 0;
                foreach (var item in chartSource)
                {
                    this.AddService(String.Empty, this._colorArr[i], item.Value);
                    i++;
                }
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        private void AddService(string serviceName, Color color, List<ChartSource> source)
        {

            Visifire.Charts.DataSeries dataSeries = new Visifire.Charts.DataSeries();
            //dataSeries.Name = serviceName;
            dataSeries.RenderAs = Visifire.Charts.RenderAs.Spline;
            dataSeries.ShowInLegend = false;
            dataSeries.SelectionEnabled = true;
            dataSeries.MarkerEnabled = false;
            dataSeries.LabelEnabled = false;
            dataSeries.Color = new SolidColorBrush(color);

            DataPoint datapoint;
            foreach (ChartSource item in source)
            {
                datapoint = new DataPoint();
                datapoint.AxisXLabel = item.DeviceDate.ToString("HH:mm");
                datapoint.YValue = item.DeviceTemperature;
                dataSeries.DataPoints.Add(datapoint);
            }
            this.charTemperature.Series.Add(dataSeries);

        }


        #endregion

        #region 事件处理

        private void cmbSelDateMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int dateSelMode = this.cmbSelDateMode.SelectedIndex;
            this.dateFrist.DateSelectionMode =
                this.dateFrist.DateSelectionMode =
                this.dateFrist.DateSelectionMode =
                    this.dateFrist.DateSelectionMode = (DateSelectionMode)Enum.ToObject(typeof(DateSelectionMode), dateSelMode);
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
                this._colorArr.Add(Color.FromArgb(255, 199, 54, 59));
            }
            if ((Boolean)chkSecond.IsChecked)
            {
                checkDateFrom.Add(this._secDate);
                this._colorArr.Add(Color.FromArgb(255, 22, 86, 147));
            }
            if ((Boolean)chkThird.IsChecked)
            {
                checkDateFrom.Add(this._thiDate);
                this._colorArr.Add(Color.FromArgb(255, 157, 245, 47));
            }
            if ((Boolean)chkFour.IsChecked)
            {
                checkDateFrom.Add(this._fouDate);
                this._colorArr.Add(Color.FromArgb(255, 116, 116, 116));
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
        月 = 1,
        年 = 2,
    }

}
