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


using Scada.Model.Entity.SL;
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

        #endregion

        #region 构造函数

        public DeviceManage()
        {

            InitializeComponent();
            this.scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();

            //加载树型
            this.LoadTreeViewInfo();

            //增加设备
            this.LoadAddDeviceInfo();


            //修改设备


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
            scadaDeviceServiceSoapClient.ViewDeviceInfoAsync(node.NodeKey.ToString().ToUpper());
        }

        private void scadaDeviceServiceSoapClient_ViewDeviceInfoCompleted(object sender, ViewDeviceInfoCompletedEventArgs e)
        {
            string result = e.Result;
        }

        #endregion




        #region 增加设备

        private void LoadAddDeviceInfo()
        {
            scadaDeviceServiceSoapClient.AddCompleted += new EventHandler<AddCompletedEventArgs>(scadaDeviceServiceSoapClient_AddCompleted);
            scadaDeviceServiceSoapClient.AddDeviceInfoAsync("设备-pc");
        }

        private void scadaDeviceServiceSoapClient_AddCompleted(object sender, AddCompletedEventArgs e)
        {
            bool Flag = BinaryObjTransfer.BinaryDeserialize<Boolean>(e.Result.ToString());
        }

        #endregion

        #region 修改设备
        #endregion

        #region 删除设备
        #endregion

        #region 事件处理



        #endregion

        #region 私有方法



        #endregion




    }
}
