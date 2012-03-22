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
using Scada.Model.Entity.Common;
using Scada.Client.VM.Modules.BaseInfo;



namespace Scada.Client.SL.Modules.BaseInfo
{


    /// <summary>
    /// 设备管理
    /// </summary>
    public partial class DeviceManage : UserControl
    {

        #region 变量声明

        private ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;
        private DeviceManageViewModel deviceManageViewModel;

        //默认修改
        private Boolean IsAddUpdateType = false;
        private DeviceTreeNode _userSelTreeNode = null;
        private DeviceInfo _userSelDeviceInfo = null;

        #endregion

        #region 构造函数

        public DeviceManage()
        {
            InitializeComponent();

            SetButtonState(false);

            //加载设备树
            deviceManageViewModel = new DeviceManageViewModel();
            this.DataContext = deviceManageViewModel;
            deviceManageViewModel.PropertyChanged += 
                new System.ComponentModel.PropertyChangedEventHandler(deviceManageViewModel_PropertyChanged);
  
            //加载维护人员信息
            this.scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            scadaDeviceServiceSoapClient.ListMaintenancePeopleCompleted += 
                new EventHandler<ListMaintenancePeopleCompletedEventArgs>(scadaDeviceServiceSoapClient_ListMaintenancePeopleCompleted);
            scadaDeviceServiceSoapClient.ListMaintenancePeopleAsync();
            
            //加载液晶屏显示
            LoadDisplayType();
            //加载当前选择模式
            LoadCurrentModel();

        }

        void deviceManageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName=="DeviceTreeNodeList")
            {
                this.treeViewList1.Source = deviceManageViewModel.DeviceTreeNodeList;
            }
        }

        #endregion

        #region 初始化基本信息
        private void LoadDisplayType()
        {
            List<KeyValue> keyValueList=new List<KeyValue>();
            keyValueList.Clear();
            keyValueList.Add(new KeyValue(){ Key=1, Value="完整显示"});
            keyValueList.Add(new KeyValue(){ Key=2, Value="基本显示"});
            keyValueList.Add(new KeyValue(){ Key=3, Value="天气预报"});
            keyValueList.Add(new KeyValue(){ Key=4, Value="不显示"});

            cmbDisplayType.ItemsSource = keyValueList;
        }

        private void LoadCurrentModel()
        {
            List<KeyValue> keyValueList = new List<KeyValue>();
            keyValueList.Clear();
            keyValueList.Add(new KeyValue() { Key=1, Value="实时模式" });
            keyValueList.Add(new KeyValue() { Key=2, Value="整点模式" });
            keyValueList.Add(new KeyValue() { Key=3, Value="优化模式" });
            cmbCurrentModel.ItemsSource = keyValueList;
        }
        #endregion

        #region 加载树型结构


        void scadaDeviceServiceSoapClient_ListMaintenancePeopleCompleted(object sender, ListMaintenancePeopleCompletedEventArgs e)
        {

            List<MaintenancePeople> maintenancePeopleList = BinaryObjTransfer.BinaryDeserialize<List<MaintenancePeople>>(e.Result);
            cmbMaintenancePeople.ItemsSource = maintenancePeopleList;
        }

        private void treeViewList1_OnTreeSelectItemClick(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender == null) { return; }
            DeviceTreeNode node = e.NewValue as DeviceTreeNode;
            //node.NodeParent = e.OldValue as DeviceTreeNode;
            this._userSelTreeNode = node;

            this.btnExport.IsEnabled = true;

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
                //scadaDeviceServiceSoapClient.ViewDeviceInfoAsync(node.NodeKey.ToString().ToUpper());
            }

            DeviceManageViewModel deviceManageViewModel = new DeviceManageViewModel(node.NodeKey.ToString().ToUpper());
            this.DataContext = deviceManageViewModel;

        }

        //设置按钮的状态
        private void SetButtonState(bool flag)
        {
            this.butAdd.IsEnabled = flag;
            this.butDel.IsEnabled = flag;
            this.butSave.IsEnabled = flag;
        }

        private void scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted(object sender, ViewDeviceInfoCompletedEventArgs e)
        {
            //string result = e.Result;
            //if (string.IsNullOrEmpty(result)) { return; }
            //_userSelDeviceInfo = BinaryObjTransfer.BinaryDeserialize<DeviceInfo>(result);
            //if (_userSelDeviceInfo == null) { return; }

            ////设备编号
            //this.txtDeviceNo.Text = _userSelDeviceInfo.DeviceNo;
            ////设备MAC
            //this.txtDeviceMac.Text = _userSelDeviceInfo.DeviceMAC;
            //this.txtSIM.Text = _userSelDeviceInfo.SIMNo;

            //this.txtHardType.Text = _userSelDeviceInfo.HardType;
            //this.dpProductDate.Text = _userSelDeviceInfo.ProductDate.ToString();
            ////管理分区
            //this.txtManageArea.Text = _userSelTreeNode.NodeValue;
            ////this.txtManageArea.Text = _userSelTreeNode.NodeParent.NodeValue; ;
            //this.cmbMaintenancePeople.SelectedValue = _userSelDeviceInfo.MaintenancePeopleID;

            //this.txtInstallPlace.Text = _userSelDeviceInfo.InstallPlace;
            //this.txtComment.Text = _userSelDeviceInfo.Comment;

            //this.txtLongitude.Text = _userSelDeviceInfo.Longitude.ToString();
            //this.txtLatitude.Text = _userSelDeviceInfo.Latitude.ToString();
            //this.txtHigh.Text = _userSelDeviceInfo.High.ToString();

            ////this.txtConnType.Text = "";
            ////if (_userSelDeviceInfo.ConnectType != null)
            ////    this.txtConnType.Text = _userSelDeviceInfo.ConnectType.ToString();


            //this.txtComment.Text = _userSelDeviceInfo.Comment;

            //this.txtWindage.Text = _userSelDeviceInfo.Windage.ToString();

            //this.txtHardwareVersion.Text = _userSelDeviceInfo.HardwareVersion;
            //this.txtSoftVersion.Text = _userSelDeviceInfo.SoftWareVersion;
           
            ////是否启用紧急按钮
            //this.chkUrgencyBtnEnable.IsChecked = _userSelDeviceInfo.UrgencyBtnEnable;
            //// 主温度报警
            //this.chkHighTemp1Alarm.IsChecked = _userSelDeviceInfo.Temperature1AlarmValid;
            //if (_userSelDeviceInfo.Temperature1HighAlarm!=null)
            //{
            //    this.txtHighTemp1Alarm.Text = _userSelDeviceInfo.Temperature1HighAlarm.ToString();
            //}
            //if (_userSelDeviceInfo.Temperature1LowAlarm!=null)
            //{
            //    this.txtLowTemp1Alarm.Text = _userSelDeviceInfo.Temperature1LowAlarm.ToString();

            //}
            ////从温度报警
            //this.chkHighTemp2Alarm.IsChecked = _userSelDeviceInfo.Temperature2AlarmValid;
            //if (_userSelDeviceInfo.Temperature2HighAlarm!=null)
            //{
            //    this.txtHighTemp2Alarm.Text = _userSelDeviceInfo.Temperature2HighAlarm.ToString();
            //}
            //if ( _userSelDeviceInfo.Temperature2LowAlarm!=null)
            //{
            //    this.txtLowTemp2Alarm.Text = _userSelDeviceInfo.Temperature2LowAlarm.ToString();
            //}

            ////湿度报警
            //this.chkHumidityAlarm.IsChecked = _userSelDeviceInfo.HumidityAlarmValid;
            //if (_userSelDeviceInfo.HumidityHighAlarm!=null)
            //{
            //    this.txtHumidityHighAlarm.Text = _userSelDeviceInfo.HumidityHighAlarm.ToString();
            //}
            //if (_userSelDeviceInfo.HumidityLowAlarm!=null)
            //{
            //    this.txtHumidityLowAlarm.Text = _userSelDeviceInfo.HumidityLowAlarm.ToString();
            //}

            ////信号报警
            //this.chkSignalAlarm.IsChecked = _userSelDeviceInfo.SignalAlarmValid;
            //if (_userSelDeviceInfo.SignalHighAlarm!=null)
            //{
            //    this.txtSignalHighAlarm.Text = _userSelDeviceInfo.SignalHighAlarm.ToString();
            //}
            //if (_userSelDeviceInfo.SignalLowAlarm!=null)
            //{
            //    this.txtSignalLowAlarm.Text = _userSelDeviceInfo.SignalLowAlarm.ToString();
            //}

            ////电量报警
            //this.chkElectricityAlarm.IsChecked = _userSelDeviceInfo.ElectricityAlarmValid;
            //if (_userSelDeviceInfo.ElectricityHighAlarm!=null)
            //{
            //    this.txtElectricityHighAlarm.Text = _userSelDeviceInfo.ElectricityHighAlarm.ToString();
            //}
            //if (_userSelDeviceInfo.ElectricityLowAlarm!=null)
            //{
            //    this.txtElectricityLowAlarm.Text = _userSelDeviceInfo.ElectricityLowAlarm.ToString();
            //}
            ////实时模式参数
            //if (_userSelDeviceInfo.RealTimeParam!=null)
            //{
            //    this.txtRealTimeParam.Text = _userSelDeviceInfo.RealTimeParam.ToString();
            //}
            ////整点模式参数1,2
            //if (_userSelDeviceInfo.FullTimeParam1!=null)
            //{
            //    this.txtFullTimeParam1.Text = _userSelDeviceInfo.FullTimeParam1.ToString();
            //}
            //if (_userSelDeviceInfo.FullTimeParam2!=null)
            //{
            //    this.txtFullTimeParam2.Text = _userSelDeviceInfo.FullTimeParam2.ToString();
            //}

            ////逢变则报模式
            //if (_userSelDeviceInfo.OptimizeParam1!=null)
            //{
            //    this.txtOptimizeParam1.Text = _userSelDeviceInfo.OptimizeParam1.ToString();
            //}
            //if (_userSelDeviceInfo.OptimizeParam2!=null)
            //{
            //    this.txtOptimizeParam2.Text = _userSelDeviceInfo.OptimizeParam2.ToString();
            //}
            //if (_userSelDeviceInfo.OptimizeParam3!=null)
            //{
            //    this.txtOptimizeParam3.Text = _userSelDeviceInfo.OptimizeParam3.ToString();
            //}

        }

        #endregion

        #region 增加设备

        private void LoadAddDeviceInfo(DeviceInfo deviceInfo)
        {
            scadaDeviceServiceSoapClient.AddDeviceInfoCompleted
                             += new EventHandler<AddDeviceInfoCompletedEventArgs>
                                            (scadaDeviceServiceSoapClient_AddDeviceInfoCompleted);

            string strSerializer = BinaryObjTransfer.BinarySerialize(deviceInfo);
            scadaDeviceServiceSoapClient.AddDeviceInfoAsync(strSerializer);
        }

        private void scadaDeviceServiceSoapClient_AddDeviceInfoCompleted(object sender, AddDeviceInfoCompletedEventArgs e)
        {
            bool Flag = BinaryObjTransfer.BinaryDeserialize<Boolean>(e.Result.ToString());
        }

        #endregion

        #region 修改设备

        private void LoadUpdateDeviceInfo(DeviceInfo deviceInfo)
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

            #region 设备属性

            
            deviceInfo.ID = Guid.NewGuid();
            deviceInfo.DeviceNo = txtDeviceNo.Text.Trim();
            deviceInfo.DeviceMAC = txtDeviceMac.Text.Trim();
            deviceInfo.SIMNo = txtSIM.Text.Trim();
            deviceInfo.HardType = txtHardType.Text.Trim();

            if (!string.IsNullOrEmpty( dpProductDate.Text))
            {
                deviceInfo.ProductDate = DateTime.Parse(dpProductDate.Text);
            }
            //获取管理分区的编号
            deviceInfo.ManageAreaID = new Guid("F5888F32-D7AB-485F-9340-4C65C6851F48");// _userSelTreeNode.NodeKey; ;
            deviceInfo.InstallPlace = txtInstallPlace.Text.Trim();
            
            //经度 维度 高度
            if (!string.IsNullOrEmpty(txtLongitude.Text.Trim()))
            {
                deviceInfo.Longitude = Convert.ToDecimal(txtLongitude.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtLatitude.Text.Trim()))
            {
                deviceInfo.Latitude = Convert.ToDecimal(txtLatitude.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtHigh.Text.Trim()))
            {
                deviceInfo.High = Convert.ToDecimal(txtHigh.Text.Trim());
            }

            deviceInfo.Comment = txtComment.Text.Trim();
            if (!string.IsNullOrEmpty(txtWindage.Text.Trim()))
            {
                deviceInfo.Windage = Convert.ToInt32(txtWindage.Text.Trim());
            }
            if (cmbMaintenancePeople.SelectedIndex != -1)
            {
                deviceInfo.MaintenancePeopleID = Guid.Parse(cmbMaintenancePeople.SelectedValue.ToString());
            }
            else
            {
                MessageBox.Show("请选择维护人员!");
                return;
            }
            deviceInfo.HardwareVersion = txtHardwareVersion.Text.Trim();
            deviceInfo.SoftWareVersion = txtSoftVersion.Text.Trim();

            
            chkUrgencyBtnEnable.IsThreeState=true;
            deviceInfo.UrgencyBtnEnable = chkUrgencyBtnEnable.IsChecked;

            //主温度
            deviceInfo.Temperature1AlarmValid = chkHighTemp1Alarm.IsChecked;
            if (!string.IsNullOrEmpty(txtHighTemp1Alarm.Text.Trim()))
            {
                deviceInfo.HumidityHighAlarm=Decimal.Parse(txtHighTemp1Alarm.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtLowTemp1Alarm.Text.Trim()))
            {
                deviceInfo.Temperature1LowAlarm = Decimal.Parse(txtLowTemp1Alarm.Text.Trim());
            }
            
            //从温度
            deviceInfo.Temperature2AlarmValid = chkHighTemp2Alarm.IsChecked;
            if (!string.IsNullOrEmpty(txtHighTemp2Alarm.Text.Trim()))
            {
                deviceInfo.Temperature1HighAlarm = Decimal.Parse(txtHighTemp2Alarm.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtLowTemp2Alarm.Text.Trim()))
            {
                deviceInfo.Temperature2LowAlarm = Decimal.Parse(txtLowTemp2Alarm.Text.Trim());
            }

            //湿度
            deviceInfo.HumidityAlarmValid = chkHumidityAlarm.IsChecked;
            if (!string.IsNullOrEmpty(txtHumidityHighAlarm.Text.Trim()))
            {
                decimal value = Decimal.Parse(txtHumidityHighAlarm.Text.Trim());
                if (value >= 1)
                {
                    MessageBox.Show("请输入大于0小于1的数!");
                    return;
                }
                else
                {
                    deviceInfo.HumidityHighAlarm = value;
                }
            }
            if (!string.IsNullOrEmpty(txtHumidityLowAlarm.Text.Trim()))
            {
                decimal value = Decimal.Parse(txtHumidityLowAlarm.Text.Trim());
                if (value >= 1)
                {
                    MessageBox.Show("请输入大于0小于1的数!");
                    return;
                }
                else
                {
                    deviceInfo.HumidityLowAlarm = value;
                }
            }
            //信号

            deviceInfo.SignalAlarmValid = chkSignalAlarm.IsChecked;
            if (!string.IsNullOrEmpty(txtSignalHighAlarm.Text.Trim()))
            {
                deviceInfo.SignalHighAlarm=Int32.Parse(txtSignalHighAlarm.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtSignalLowAlarm.Text.Trim()))
            {
                deviceInfo.SignalLowAlarm=Int32.Parse(txtSignalLowAlarm.Text.Trim());
            }

            //电量
            deviceInfo.ElectricityAlarmValid = chkElectricityAlarm.IsChecked;
            if (!string.IsNullOrEmpty(txtElectricityHighAlarm.Text.Trim()))
            {
                deviceInfo.ElectricityHighAlarm = Int32.Parse(txtElectricityHighAlarm.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtElectricityLowAlarm.Text.Trim()))
            {
                deviceInfo.ElectricityLowAlarm = Int32.Parse(txtElectricityLowAlarm.Text.Trim());
            }
            //液晶屏显示类型
            if (cmbDisplayType.SelectedValue!=DBNull.Value)
            {
                deviceInfo.LCDScreenDisplayType = Convert.ToInt32(cmbDisplayType.SelectedValue);
            }
            //当前模式：0 实时模式， 1 整点模式 2. 优化模式
            if (cmbCurrentModel.SelectedIndex!=-1)
            {
                deviceInfo.CurrentModel = Int32.Parse(cmbCurrentModel.SelectedValue.ToString());

            }
            else
            {
                MessageBox.Show("请选择模式!");
                return;
            }

            //实时模式参数
            if (!string.IsNullOrEmpty(txtRealTimeParam.Text.Trim()))
            {
                deviceInfo.RealTimeParam = Int32.Parse(txtRealTimeParam.Text.Trim());
            }

            //整点模式参数1,2
            if (!string.IsNullOrEmpty(txtFullTimeParam1.Text.Trim()))
            {
                deviceInfo.FullTimeParam1 = Int32.Parse(txtFullTimeParam1.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtFullTimeParam2.Text.Trim()))
            {
                deviceInfo.FullTimeParam2 = Int32.Parse(txtFullTimeParam2.Text.Trim());
            }
            //逢变则报模式参数 1,2,3
            if (!string.IsNullOrEmpty(txtOptimizeParam1.Text.Trim()))
            {
                deviceInfo.OptimizeParam1 = Int32.Parse(txtOptimizeParam1.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtOptimizeParam2.Text.Trim()))
            {
                deviceInfo.OptimizeParam2 = Int32.Parse(txtOptimizeParam2.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtOptimizeParam3.Text.Trim()))
            {
                deviceInfo.OptimizeParam3 = Int32.Parse(txtOptimizeParam3.Text.Trim());
            }

            #endregion

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

            if (!string.IsNullOrEmpty(txtLongitude.Text))
            {
                deviceInfo.Longitude = decimal.Parse(this.txtLongitude.Text);
            }
            if (string.IsNullOrEmpty(txtHigh.Text))
            {
                deviceInfo.High = decimal.Parse(this.txtHigh.Text);
            }

        }

        #endregion


        #region 私有方法



        #endregion




    }
}
