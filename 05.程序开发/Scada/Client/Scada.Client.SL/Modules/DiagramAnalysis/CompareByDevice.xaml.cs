using System;
using System.Linq;
using System.Collections.Generic;


using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Animation;


using Scada.Model.Entity;
using Scada.Utility.Common.SL.Enums;
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;


using Visifire.Charts;
using Telerik.Windows.Controls.Calendar;






namespace Scada.Client.SL.Modules.DiagramAnalysis
{



    /// <summary>
    /// 按设备对比(同时间段不同设备)
    /// </summary>
    public partial class CompareByDevice : UserControl
    {

        #region 变量声明

        private Int32 _dateSelectMode;

        private DateTime _starDate, _endDate;

        private List<Color> _colorArr;

        private List<DeviceTree> _selDeviceTreeNode;

        private ScadaDeviceServiceSoapClient _scadaDeviceServiceSoapClient;

        #endregion


        #region 静态构造

        private static CompareByDevice instance;
        public static CompareByDevice GetInstance()
        {
            if (instance == null)
            {
                instance = new CompareByDevice();
            }
            return instance;
        }

        #endregion


        #region 构造函数

        public CompareByDevice()
        {

            InitializeComponent();


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
            this.dateFrist.SelectedValue = DateTime.Now;

            //对比设备
            this._scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            this._scadaDeviceServiceSoapClient.GetDeviceTreeListCompleted +=
                                            new EventHandler<GetDeviceTreeListCompletedEventArgs>
                                            (scadaDeviceServiceSoapClient_GetDeviceTreeListCompleted);
            this._scadaDeviceServiceSoapClient.GetDeviceTreeListAsync();

            //选择设备
            _selDeviceTreeNode = new List<DeviceTree>();
            _selDeviceTreeNode.Clear();

            //初期化图表
            this._colorArr = new List<Color>();
            this.charTemperature.Series.Clear();

            this._scadaDeviceServiceSoapClient.GetSameDateTemperatureDiffDeviceCompleted +=
                                       new EventHandler<GetSameDateTemperatureDiffDeviceCompletedEventArgs>
                                       (scadaDeviceServiceSoapClient_GetSameDateTemperatureDiffDeviceCompleted);

        }

        #endregion


        #region 事件处理

        private void butViewCompare_Click(object sender, RoutedEventArgs e)
        {

            //Check DeviceInfo
            this._colorArr.Clear();
            this._selDeviceTreeNode.Clear();

            //选择基础信息
            this.QueryDeviceTemperature();

            if (this._selDeviceTreeNode.Count == 0)
            {
                ScadaMessageBox.ShowWarnMessage("请选择要比较的设备！！！", "重要提示");
                return;
            }
            IEnumerable<DeviceTree> checkDeviceTo = _selDeviceTreeNode.Distinct();
            if (checkDeviceTo.Count() != _selDeviceTreeNode.Count())
            {
                ScadaMessageBox.ShowWarnMessage("有相同设备进行比较！！！", "重要提示");
                return;
            }

            //+-<>
            DateSelMode dateSelMode = (DateSelMode)_dateSelectMode;
            DeviceComaprePageTurn.getInstance().AddPrepaid(this._starDate, this._endDate, dateSelMode);

            this.LoadChartSource();

        }

        private void LoadChartSource()
        {
            string jsonDevices = BinaryObjTransfer.BinarySerialize(_selDeviceTreeNode);
            this._scadaDeviceServiceSoapClient.GetSameDateTemperatureDiffDeviceAsync(this._dateSelectMode, this._starDate, this._endDate, jsonDevices);
        }

        private void scadaDeviceServiceSoapClient_GetSameDateTemperatureDiffDeviceCompleted(object sender,
                                                    GetSameDateTemperatureDiffDeviceCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string msgInfo = e.Result;
                Dictionary<DeviceTree, List<ChartSource>> chartSource =
                    BinaryObjTransfer.BinaryDeserialize<Dictionary<DeviceTree, List<ChartSource>>>(msgInfo);
                if (chartSource.Count() == 0) { return; }

                //Clear ChartSource
                this.charTemperature.Series.Clear();

                //更改X轴间隔
                Int32 dateSelMode = this.cmbSelDateMode.SelectedIndex;
                this.charTemperature.AxesX[0].Title = String.Format("起止时间:  {0}---{1}", this._starDate, this._endDate);
                CompareByTime.SetChartAxesXFormat(this.charTemperature.AxesX[0], dateSelMode);

                this.charTemperature.AxesX[0].AxisMinimum = this._starDate;
                this.charTemperature.AxesX[0].AxisMaximum = this._endDate;

                int i = 0;
                foreach (var item in chartSource)
                {
                    this.AddService(String.Empty, this._colorArr[i], item.Value, dateSelMode);
                    i++;
                }
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        private void AddService(string serviceName, Color color, List<ChartSource> source, Int32 dateSelMode)
        {

            Visifire.Charts.DataSeries dataSeries = new Visifire.Charts.DataSeries();
            dataSeries.RenderAs = Visifire.Charts.RenderAs.Line ;
            dataSeries.ShowInLegend = false;
            dataSeries.SelectionEnabled = true;
            dataSeries.MarkerEnabled = false;
            dataSeries.LabelEnabled = false;
            dataSeries.XValueType = ChartValueTypes.DateTime;
            dataSeries.Color = new SolidColorBrush(color);

            DataPoint datapoint;
            foreach (ChartSource item in source)
            {
                datapoint = new DataPoint();
                datapoint.XValue = item.DeviceDate;
                datapoint.YValue = item.DeviceTemperature;
                datapoint.ToolTipText = string.Format("{0},{1}", datapoint.XValue, datapoint.YValue);
                dataSeries.DataPoints.Add(datapoint);
            }
            this.charTemperature.Series.Add(dataSeries);

        }


        //上一周期
        private void butFast_Click(object sender, RoutedEventArgs e)
        {

            DeviceComaprePageTurn.getInstance().Forth();
            this._starDate = DeviceComaprePageTurn.getInstance().StarDate;
            this._endDate = DeviceComaprePageTurn.getInstance().EndDate;
            this.LoadChartSource();

        }

        //增加刻度
        private void butUNext_Click(object sender, RoutedEventArgs e)
        {
            DeviceComaprePageTurn.getInstance().Add();
            this._starDate = DeviceComaprePageTurn.getInstance().StarDate;
            this._endDate = DeviceComaprePageTurn.getInstance().EndDate;
            this.LoadChartSource();
        }

        //减少刻度
        private void butDNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DeviceComaprePageTurn.getInstance().Remove();
            }
            catch (Exception ex)
            {
                ScadaMessageBox.ShowWarnMessage(ex.Message, "重要提示");
                return;
            }
            this._starDate = DeviceComaprePageTurn.getInstance().StarDate;
            this._endDate = DeviceComaprePageTurn.getInstance().EndDate;
            this.LoadChartSource();
        }

        //下一周期
        private void butLast_Click(object sender, RoutedEventArgs e)
        {
            DeviceComaprePageTurn.getInstance().Back();
            this._starDate = DeviceComaprePageTurn.getInstance().StarDate;
            this._endDate = DeviceComaprePageTurn.getInstance().EndDate;
            this.LoadChartSource();
        }

        #endregion


        #region 私有方法

        private void QueryDeviceTemperature()
        {

            //选择日期模式
            _dateSelectMode = this.cmbSelDateMode.SelectedIndex;
            DateSelMode dateSelMode = (DateSelMode)_dateSelectMode;
            _starDate = this.dateFrist.DisplayDate;
            _endDate = CompareByTime.GetEndDateTime(ref _starDate, dateSelMode);

            //选择设备信息
            this.AddDeviceTreeNode(this.chkFrist, this.cmbDevFrist);
            this.AddDeviceTreeNode(this.chkSecond, this.cmbDevSecond);
            this.AddDeviceTreeNode(this.chkThird, this.cmbDevThird);
            this.AddDeviceTreeNode(this.chkFour, this.cmbDevFour);

        }

        private void AddDeviceTreeNode(CheckBox checkBox, ComboBox cmbBox)
        {

            if ((Boolean)checkBox.IsChecked && cmbBox.SelectedItem != null)
            {
                DeviceTreeNode treeNode = cmbBox.SelectedItem as DeviceTreeNode;
                this._selDeviceTreeNode.Add(
                                        new DeviceTree
                                        {
                                            ID = treeNode.NodeKey,
                                            Level = treeNode.NodeType,
                                            Name = treeNode.NodeValue
                                        });

                if (checkBox.Name == "chkFrist")
                    this._colorArr.Add(Colors.Red);
                else if (checkBox.Name == "chkSecond")
                    this._colorArr.Add(Color.FromArgb(255, 30, 144, 255));
                else if (checkBox.Name == "chkThird")
                    this._colorArr.Add(Color.FromArgb(255, 154, 205, 50));
                else if (checkBox.Name == "chkFour")
                    this._colorArr.Add(Color.FromArgb(255, 192, 192, 192));
            }
        }

        private void cmbSelDateMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int dateSelMode = this.cmbSelDateMode.SelectedIndex;
            this.dateFrist.DateSelectionMode =
                this.dateFrist.DateSelectionMode =
                this.dateFrist.DateSelectionMode =
                    this.dateFrist.DateSelectionMode = (DateSelectionMode)Enum.ToObject(typeof(DateSelectionMode), dateSelMode);
        }

        private void scadaDeviceServiceSoapClient_GetDeviceTreeListCompleted(object sender, GetDeviceTreeListCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                string msgInfo = e.Result;
                List<DeviceTreeNode> devicTreeList = BinaryObjTransfer.BinaryDeserialize<List<DeviceTreeNode>>(msgInfo);
                if (devicTreeList.Count() == 0) { return; }
                this.cmbDevFrist.ItemsSource = devicTreeList;
                this.cmbDevSecond.ItemsSource = devicTreeList;
                this.cmbDevThird.ItemsSource = devicTreeList;
                this.cmbDevFour.ItemsSource = devicTreeList;
            }
            else
                ScadaMessageBox.ShowWarnMessage("获取数据失败！", "警告信息");
        }

        #endregion


    }
}
