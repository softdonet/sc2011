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
using Scada.Utility.Common.SL;
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

        private DateTime _starDate, _endDate;

        private List<Color> _colorArr;

        private List<DeviceTreeNode> _selDeviceTreeNode;

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
            _selDeviceTreeNode = new List<DeviceTreeNode>();
            _selDeviceTreeNode.Clear();


        }

        #endregion


        #region 事件处理

        private void butViewCompare_Click(object sender, RoutedEventArgs e)
        {

            int dateSelMode = this.cmbSelDateMode.SelectedIndex;

            _starDate = this.dateFrist.DisplayDate;
            _endDate = CompareByTime.GetEndDateTime(_starDate, (DateSelMode)dateSelMode);


            //Check DeviceInfo
            this._selDeviceTreeNode.Clear();
            if ((Boolean)this.chkFrist.IsChecked && 
                            this.cmbDevFrist.SelectedItem != null)
            {



            }



        }

        #endregion


        #region 私有方法

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
