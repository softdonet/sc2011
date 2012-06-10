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
                TreeViewItem tv = treeViewList1.myTree.GetSelectedContainer();//.SelectedItem as TreeViewItem;
                if (tv != null)
                {
                    tv.IsExpanded = true;
                }
               // treeViewList1.myTree.SelectedItem = _userSelTreeNode;
                
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
