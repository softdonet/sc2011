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
using Scada.Client.SL.ScadaDeviceService;
using Scada.Client.SL.CommClass;
using Scada.Model.Entity.SL;


namespace Scada.Client.SL.Modules.BaseInfo
{
    public partial class DeviceManage : UserControl
    {
        ScadaDeviceServiceSoapClient scadaDeviceServiceSoapClient = null;
        public DeviceManage()
        {
            InitializeComponent();
            scadaDeviceServiceSoapClient = ServiceManager.GetScadaDeviceService();
            scadaDeviceServiceSoapClient.AddCompleted += new EventHandler<AddCompletedEventArgs>(scadaDeviceServiceSoapClient_AddCompleted);
            scadaDeviceServiceSoapClient.AddDeviceInfoAsync("设备-pc");

            scadaDeviceServiceSoapClient.ListDeviceTreeViewCompleted += new EventHandler<ListDeviceTreeViewCompletedEventArgs>(scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted);
            scadaDeviceServiceSoapClient.ListDeviceTreeViewAsync();
        }

        void scadaDeviceServiceSoapClient_ListDeviceTreeViewCompleted(object sender, ListDeviceTreeViewCompletedEventArgs e)
        {
            this.treeViewList1.Source = BinaryObjTransfer.BinaryDeserialize<List<DeviceTreeNode>>(e.Result);
        }

        void scadaDeviceServiceSoapClient_AddCompleted(object sender, AddCompletedEventArgs e)
        {
            bool Flag = BinaryObjTransfer.BinaryDeserialize<Boolean>(e.Result.ToString());
        }
    }
}
