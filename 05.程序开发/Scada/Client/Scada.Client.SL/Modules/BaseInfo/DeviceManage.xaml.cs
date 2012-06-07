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
using System.ComponentModel.DataAnnotations;
using Scada.Utility.Common.SL;
using Scada.Model.Entity.Enums;

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
            
        }

        void deviceManageViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DeviceTreeNodeList")
            {
                this.treeViewList1.Source = deviceManageViewModel.DeviceTreeNodeList;
            }
        }

        #endregion


        #region 加载树型结构

        private void treeViewList1_OnTreeSelectItemClick(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (sender == null) { return; }
            DeviceTreeNode node = e.NewValue as DeviceTreeNode;
            if (node != null)
            {
                switch (node.NodeType)
                {
                    case 0:
                    case 1:
                        break;
                    // break;
                    case 2:
                    case 3:
                        deviceManageViewModel.CurrentNodeGuid = node.NodeKey;
                        deviceManageViewModel.CurrentNodeLevel = node.NodeType;
                        break;
                    default:
                        break;
                }
              
                //node.NodeParent = e.OldValue as DeviceTreeNode;
                this._userSelTreeNode = node;

                //this.btnExport.IsEnabled = true;

                if (node.NodeType == 0 || node.NodeType == 1)
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

                //DeviceManageViewModel deviceManageViewModel = new DeviceManageViewModel(node.NodeKey.ToString().ToUpper());
                deviceManageViewModel.ViewDeviceInfoById(new Guid(node.NodeKey.ToString()));
                this.DataContext = deviceManageViewModel;
            }

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

        //验证输入
        private void LayoutRoot_BindingValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added) 
                (e.OriginalSource as Control).Background = new SolidColorBrush(Colors.Yellow);
            if (e.Action == ValidationErrorEventAction.Removed)
                (e.OriginalSource as Control).Background = new SolidColorBrush(Colors.White);
        }

        private void cmbCurrentModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cmbCurrentModel.SelectedIndex)
            {
                case 0://实时模式
                    txtRealTimeParam.IsReadOnly = false;
                    cmbFullTimeParam1.IsEnabled = false;
                    cmbFullTimeParam2.IsEnabled = false;
                    cmbOptimizeParam1.IsEnabled = false;
                    cmbOptimizeParam2.IsEnabled = false;
                    txtOptimizeParam3.IsReadOnly = true;

                    deviceManageViewModel.SelectedFullTimeParam1Item = null;
                    //deviceManageViewModel.SelectedFullTimeParam2Item = null;
                    //deviceManageViewModel.SelectedOptimize1Item = null;
                    //deviceManageViewModel.SelectedOptimize2Item = null;
                    //deviceManageViewModel.DeviceInfoList.OptimizeParam3 = null;
                    break;
                case 1://整点模式
                    txtRealTimeParam.IsReadOnly = true;
                    cmbFullTimeParam1.IsEnabled = true;
                    cmbFullTimeParam2.IsEnabled = true;
                    cmbOptimizeParam1.IsEnabled = false;
                    cmbOptimizeParam2.IsEnabled = false;
                    txtOptimizeParam3.IsReadOnly = true;

                    //deviceManageViewModel.DeviceInfoList.RealTimeParam = null;
                    //deviceManageViewModel.SelectedOptimize1Item = null;
                    //deviceManageViewModel.SelectedOptimize2Item = null;
                    break;
                case 2://逢变则报模式
                    txtRealTimeParam.IsReadOnly = true;
                    cmbFullTimeParam1.IsEnabled = false;
                    cmbFullTimeParam2.IsEnabled = false;
                    cmbOptimizeParam1.IsEnabled = true;
                    cmbOptimizeParam2.IsEnabled = true;
                    txtOptimizeParam3.IsReadOnly = false;

                    //deviceManageViewModel.DeviceInfoList.RealTimeParam = null;
                    //deviceManageViewModel.SelectedFullTimeParam1Item = null;
                    //deviceManageViewModel.SelectedFullTimeParam2Item = null;
                    //cmbFullTimeParam1.SelectedItem = null;
                    //cmbFullTimeParam2.SelectedItem = null;
                   
                    break;
                default:
                    txtRealTimeParam.IsReadOnly = true;
                    cmbFullTimeParam1.IsEnabled = false;
                    cmbFullTimeParam2.IsEnabled = false;
                    cmbOptimizeParam1.IsEnabled = false;
                    cmbOptimizeParam2.IsEnabled = false;
                    txtOptimizeParam3.IsReadOnly = true;
                    break;
            }
        }


        #region 私有方法



        #endregion




    }
}
