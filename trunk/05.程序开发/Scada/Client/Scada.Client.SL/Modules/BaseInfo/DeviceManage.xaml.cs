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

            //----------模拟数据------------------------------
            List<DeviceTreeNode> lst = new List<DeviceTreeNode>();
            DeviceTreeNode P1 = new DeviceTreeNode();
            P1.NodeValue = "区域1";

            DeviceTreeNode G1 = new DeviceTreeNode();
            G1.NodeValue = "管理分区1";
            P1.AddNodeKey(G1);

            DeviceTreeNode D1 = new DeviceTreeNode();
            D1.NodeValue = "P001";
            G1.AddNodeKey(D1);

            DeviceTreeNode D2 = new DeviceTreeNode();
            D2.NodeValue = "P001";
            G1.AddNodeKey(D2);

            DeviceTreeNode P2 = new DeviceTreeNode();
            P2.NodeValue = "区域2";

            DeviceTreeNode G2 = new DeviceTreeNode();
            G2.NodeValue = "管理分区2";
            P2.AddNodeKey(G2);

            DeviceTreeNode D3 = new DeviceTreeNode();
            D3.NodeValue = "P003";
            G2.AddNodeKey(D3);

            DeviceTreeNode D4 = new DeviceTreeNode();
            D4.NodeValue = "P004";
            G2.AddNodeKey(D4);

            lst.Add(P1);
            lst.Add(P2);
            //---------------------------------------------------------------------------

            //此处返回数据有问题
            MessageBox.Show(e.Result);
            // BinaryObjTransfer.BinaryDeserialize<List<DeviceTreeNode>>(e.Result);
            this.treeViewList1.Source = lst;
        }

        void scadaDeviceServiceSoapClient_AddCompleted(object sender, AddCompletedEventArgs e)
        {
            bool Flag = BinaryObjTransfer.BinaryDeserialize<Boolean>(e.Result.ToString());
        }
    }
}
