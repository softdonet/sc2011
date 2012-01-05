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
using Scada.Client.SL.CommClass;
using Scada.Client.SL.ScadaDeviceService;



namespace Scada.Client.SL.Modules.BaseInfo
{
    public partial class DeviceManage : UserControl
    {

        #region 变量声明

        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;

        //默认修改
        private Boolean IsAddUpdateType = false;
        private DeviceTreeNode _userSelTreeNode = null;
        private DeviceInfo _userSelDeviceInfo = null;

        #endregion

        #region 构造函数

        public DeviceManage()
        {

            InitializeComponent();
            this.scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            //加载树型
            this.LoadTreeViewInfo();

            //增加设备
            //this.LoadAddDeviceInfo();

            //修改设备
            this.LoadUpdateDeviceInfo();

            //删除设备




        }

        #endregion

        #region 加载树型结构

        private void LoadTreeViewInfo()
        {

            //刷新树结构
            scadaDeviceServiceSoapClient.ListDeviceTreeViewCompleted +=
                new EventHandler<ListDeviceTreeViewCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();

            //列设备信息
            scadaDeviceServiceSoapClient.ViewDeviceInfoCompleted +=
                new EventHandler<ViewDeviceInfoCompletedEventArgs>(scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted);

        }

        private void scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted(object sender, ListDeviceTreeViewCompletedEventArgs e)
        {
            this.treeViewList1.Source = BinaryObjTransfer.BinaryDeserialize<List<DeviceTreeNode>>(e.Result);
        }

        private void treeViewList1_OnTreeSelectItemClick(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender == null) { return; }
            DeviceTreeNode node = e.NewValue as DeviceTreeNode;
            this._userSelTreeNode = node;

            this.btnExport.IsEnabled = true;
            if (node.NodeType != null)
            {


                if (node.NodeType == 1)
                {
                    this.butAdd.IsEnabled = false;
                    this.butDel.IsEnabled = false;
                    this.butSave.IsEnabled = false;
                }
                else if (node.NodeType == 2)
                {
                    this.butAdd.IsEnabled = true;
                    this.butDel.IsEnabled = false;
                    this.butSave.IsEnabled = true;
                }
                else if (node.NodeType == 3)
                {
                    this.butAdd.IsEnabled = true;
                    this.butDel.IsEnabled = true;
                    this.butSave.IsEnabled = true;
                    scadaDeviceServiceSoapClient.ViewDeviceInfoAsync(node.NodeKey.ToString().ToUpper());
                }
            }
        }

        private void scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted(object sender, ViewDeviceInfoCompletedEventArgs e)
        {
            string result = e.Result;
            if (string.IsNullOrEmpty(result)) { return; }
            _userSelDeviceInfo = BinaryObjTransfer.BinaryDeserialize<DeviceInfo>(result);
            if (_userSelDeviceInfo == null) { return; }

            //设备MAC
            this.txtDeviceMac.Text = _userSelDeviceInfo.DeviceMAC;
            this.txtSIM.Text = _userSelDeviceInfo.SIMNo;
            this.txtManageArea.Text = _userSelTreeNode.NodeValue;
            this.txtInstallPlace.Text = _userSelDeviceInfo.InstallPlace;
            this.txtComment.Text = _userSelDeviceInfo.Comment;

            this.txtConnPoint.Text = _userSelDeviceInfo.ConnectPoint;
            this.txtLongitude.Text = _userSelDeviceInfo.Longitude.ToString();
            this.txtDimensionality.Text = _userSelDeviceInfo.Dimensionality.ToString();
            this.txtHigh.Text = _userSelDeviceInfo.High.ToString();

            this.txtConnType.Text = "";
            if (_userSelDeviceInfo.ConnectType != null)
                this.txtConnType.Text = _userSelDeviceInfo.ConnectType.ToString();

            this.txtMainDNS.Text = _userSelDeviceInfo.MainDNS;
            this.txtSecondDNS.Text = _userSelDeviceInfo.SecondDNS;
            this.txtCenterIp.Text = _userSelDeviceInfo.CenterIP;
            this.txtDomain.Text = _userSelDeviceInfo.Domain;

            this.txtPort.Text = "";
            if (_userSelDeviceInfo.port != null)
                this.txtPort.Text = _userSelDeviceInfo.port.ToString();

            //定时表类型
            /*
            Int32? intType = _userSelDeviceInfo.TimeType;
            if (intType != null)
            {
                if (intType == 1)
                    this.radioButton1.IsChecked = true;
                else if (intType == 2)
                    this.radioButton2.IsChecked = true;
                else if (intType == 3)
                    this.radioButton3.IsChecked = true;
                else if (intType == 4)
                    this.radioButton4.IsChecked = true;
                else
                    this.radioButton5.IsChecked = true;
            }
            */

            this.txtVersion.Text = _userSelDeviceInfo.Version;

            //硬件配置
            Int32? intType = _userSelDeviceInfo.CollectFreq;
            if (intType != null)
                this.txtCollectFreq.Text = intType.ToString();

            intType = _userSelDeviceInfo.ReportInterval;
            if (intType != null)
                this.txtReportInterval.Text = intType.ToString();

            decimal? decValue = _userSelDeviceInfo.AlarmTop;
            if (decValue != null)
                this.txtAlarnTop.Text = decValue.ToString();

            decValue = _userSelDeviceInfo.AlarmDown;
            if (decValue != null)
                this.txtAlarnDown.Text = decValue.ToString();

            decValue = _userSelDeviceInfo.Windage;
            if (decValue != null)
                this.txtWindage.Text = decValue.ToString();

        }

        #endregion

        #region 增加设备

        private void LoadAddDeviceInfo(DeviceInfo deviceInfo)
        {
            scadaDeviceServiceSoapClient.AddDeviceInfoCompleted
                             += new EventHandler<AddDeviceInfoCompletedEventArgs>
                                            (scadaDeviceServiceSoapClient_AddDeviceInfoCompleted);

            scadaDeviceServiceSoapClient.AddDeviceInfoAsync(BinaryObjTransfer.BinarySerialize(deviceInfo));
        }

        private void scadaDeviceServiceSoapClient_AddDeviceInfoCompleted(object sender, AddDeviceInfoCompletedEventArgs e)
        {
            bool Flag = BinaryObjTransfer.BinaryDeserialize<Boolean>(e.Result.ToString());
        }

        #endregion



        #region 修改设备

        private void LoadUpdateDeviceInfo()
        {
            scadaDeviceServiceSoapClient.UpdateDeviceInfoCompleted
                            += new EventHandler<UpdateDeviceInfoCompletedEventArgs>
                                        (scadaDeviceServiceSoapClient_UpdateDeviceInfoCompleted);
        }

        private void scadaDeviceServiceSoapClient_UpdateDeviceInfoCompleted(object sender, UpdateDeviceInfoCompletedEventArgs e)
        {
            string result = e.Result.ToString();
        }



        #endregion

        #region 删除设备
        #endregion

        #region 事件处理

        private void butAdd_Click(object sender, RoutedEventArgs e)
        {
            DeviceInfo deviceInfo = new DeviceInfo();
            deviceInfo.ID = Guid.NewGuid();
            deviceInfo.DeviceMAC = txtDeviceMac.Text.Trim();
            deviceInfo.SIMNo = txtSIM.Text.Trim();
            deviceInfo.InstallPlace = txtInstallPlace.Text.Trim();
            deviceInfo.Comment = txtComment.Text.Trim();
            deviceInfo.ConnectPoint = txtConnPoint.Text.Trim();
            if (!string.IsNullOrEmpty(txtLongitude.Text.Trim()))
            {
                deviceInfo.Longitude = Convert.ToDecimal(txtLongitude.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtDimensionality.Text.Trim()))
            {
                deviceInfo.Dimensionality = Convert.ToDecimal(txtDimensionality.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtHigh.Text.Trim()))
            {
                deviceInfo.High = Convert.ToDecimal(txtHigh.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtConnType.Text.Trim()))
            {
                deviceInfo.ConnectType = Convert.ToInt32(txtConnType.Text.Trim());
            }
            deviceInfo.MainDNS = txtMainDNS.Text.Trim();
            deviceInfo.SecondDNS = txtSecondDNS.Text.Trim();
            deviceInfo.CenterIP = txtCenterIp.Text.Trim();
            deviceInfo.Domain = txtDomain.Text.Trim();
            if (!string.IsNullOrEmpty(txtPort.Text.Trim()))
            {
                deviceInfo.port = Convert.ToInt32(txtPort.Text.Trim());
            }
            if (!string.IsNullOrEmpty((txtCollectFreq.Text.Trim())))
            {
                deviceInfo.CollectFreq = Convert.ToInt32(txtCollectFreq.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtReportInterval.Text.Trim()))
            {
                deviceInfo.ReportInterval = Convert.ToInt32(txtReportInterval.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtWindage.Text.Trim()))
            {
                deviceInfo.Windage=Convert.ToInt32(txtWindage.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtAlarnTop.Text.Trim()))
            {
                deviceInfo.AlarmTop = Convert.ToDecimal(txtAlarnTop.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtAlarnDown.Text.Trim()))
            {
                deviceInfo.AlarmDown = Convert.ToDecimal(txtAlarnDown.Text.Trim());
            }
            deviceInfo.Version = txtVersion.Text.Trim();
            deviceInfo.LCDScreenDisplayType = (cmbDisplayType.SelectedValue as DeviceInfo).LCDScreenDisplayType;
            if (chkInstancyBtn.IsChecked == true)
            {
                deviceInfo.UseUrgencyButton = 0;
            }
            else
            {
                deviceInfo.UseUrgencyButton = 1;
            }
            if (chkProcess1.IsChecked==true)
            {
                deviceInfo.Process1Enable = 0;
            }
            else
            {
                deviceInfo.Process1Enable = 1;
            }
            if (!string.IsNullOrEmpty(txtProcess1HigherAlarm.Text.Trim()))
            {

                deviceInfo.Process1HigherValue = Convert.ToDecimal(txtProcess1HigherAlarm.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtProcess1HighAlarm.Text.Trim()))
            {

                deviceInfo.Process1HigherValue = Convert.ToDecimal(txtProcess1HighAlarm.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtProcess1lowAlarm.Text.Trim()))
            {

                deviceInfo.Process1HigherValue = Convert.ToDecimal(txtProcess1lowAlarm.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtProcess1lowerAlarm.Text.Trim()))
            {

                deviceInfo.Process1HigherValue = Convert.ToDecimal(txtProcess1lowerAlarm.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtProcess1RateAlarm.Text.Trim()))
            {

                deviceInfo.Process1HigherValue = Convert.ToDecimal(txtProcess1RateAlarm.Text.Trim());
            }
            
            this.IsAddUpdateType = true;
            LoadAddDeviceInfo(deviceInfo);


        }

        private void butDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void butSave_Click(object sender, RoutedEventArgs e)
        {
            if (_userSelDeviceInfo != null)
            {
                //_userSelDeviceInfo = new DeviceInfo();
                if (IsAddUpdateType)
                    _userSelDeviceInfo.ID = Guid.NewGuid();

                this.AddDeviceProperty(_userSelDeviceInfo);

                string deviceInfo = string.Empty;
                if (IsAddUpdateType)
                {
                    deviceInfo = BinaryObjTransfer.BinarySerialize<DeviceInfo>(_userSelDeviceInfo);
                    scadaDeviceServiceSoapClient.AddDeviceInfoAsync(deviceInfo);
                }
                else
                {
                    deviceInfo = BinaryObjTransfer.BinarySerialize<DeviceInfo>(_userSelDeviceInfo);
                    scadaDeviceServiceSoapClient.UpdateDeviceInfoAsync(deviceInfo);
                }
            }
        }

        private void AddDeviceProperty(DeviceInfo deviceInfo)
        {

            deviceInfo.DeviceMAC = this.txtDeviceMac.Text;
            deviceInfo.SIMNo = this.txtSIM.Text;

            deviceInfo.InstallPlace = this.txtInstallPlace.Text;
            deviceInfo.Comment = this.txtComment.Text;

            deviceInfo.ConnectPoint = this.txtConnPoint.Text;
            if (!string.IsNullOrEmpty(txtLongitude.Text))
            {
                deviceInfo.Longitude = decimal.Parse(this.txtLongitude.Text);
            }
            if (!string.IsNullOrEmpty(txtDimensionality.Text))
            {
                deviceInfo.Dimensionality = decimal.Parse(this.txtDimensionality.Text);
            }
            if (string.IsNullOrEmpty(txtHigh.Text))
            {
                deviceInfo.High = decimal.Parse(this.txtHigh.Text);
            }
            deviceInfo.ConnectType = 1;

            deviceInfo.MainDNS = this.txtMainDNS.Text;
            deviceInfo.SecondDNS = this.txtSecondDNS.Text;
            deviceInfo.CenterIP = this.txtCenterIp.Text;
            deviceInfo.Domain = this.txtDomain.Text;

            string strValue = this.txtPort.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.port = Convert.ToInt32(strValue);

            //定时表类型
            Int32? intType = null;
            /*
            if ((Boolean)this.radioButton1.IsChecked)
                intType = 1;
            else if ((Boolean)this.radioButton2.IsChecked)
                intType = 2;
            else if ((Boolean)this.radioButton3.IsChecked)
                intType = 3;
            else if ((Boolean)this.radioButton4.IsChecked)
                intType = 4;
            else if ((Boolean)this.radioButton5.IsChecked)
                intType = 5;
            */
            deviceInfo.TimeType = intType;
            deviceInfo.Version = this.txtVersion.Text;

            //硬件配置
            strValue = this.txtCollectFreq.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.CollectFreq = Convert.ToInt32(strValue);

            strValue = this.txtReportInterval.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.ReportInterval = Convert.ToInt32(strValue);

            strValue = this.txtAlarnTop.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.AlarmTop = Convert.ToDecimal(strValue);

            strValue = this.txtAlarnDown.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.AlarmDown = Convert.ToDecimal(strValue);

            strValue = this.txtWindage.Text;
            if (!string.IsNullOrEmpty(strValue))
                deviceInfo.Windage = Convert.ToDecimal(strValue);


        }

        #endregion

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        #region 私有方法



        #endregion




    }
}
