using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using Scada.Model.Entity;
using Scada.Utility.Common.Transfer;

namespace Scada.BLL.Implement
{

    public delegate void DataReceivedHandle(string data);
    /// <summary>
    /// 数据库实时数据监控
    /// yanhk at 2011-12-9
    /// </summary>
    /// <param name="data"></param>
    public class DeviceRealTimeMonitorService
    {
        private Timer timer = null;
        public event DataReceivedHandle ReaTimeDataReceived;
        public event DataReceivedHandle AlarmDataReceived;
        public event DataReceivedHandle CallDataReceived;

        public DeviceRealTimeMonitorService()
        {
            timer = new Timer(new System.Threading.TimerCallback(CheckDBMessages), this, 2000, 2000);
        }

        /// <summary>
        /// 监控数据库，必要的时候发送数据
        /// </summary>
        /// <param name="o"></param>
        private void CheckDBMessages(object o)
        {
            #region  模拟实时数据

            Random rd = new Random();
            List<DeviceRealTimeTree> lst = new List<DeviceRealTimeTree>();
            DeviceRealTimeTree P1 = new DeviceRealTimeTree();
            P1.NodeValue = "区域1";
            P1.Electricity = rd.Next(1, 5);
            P1.Signal = rd.Next(1, 5);
            P1.Status = rd.Next(1, 4);
            P1.Temperature = decimal.Parse(rd.Next(15, 30) + "." + rd.Next(0, 99));
            P1.InstallPlace = "北京天安门";
            P1.NodeType = 1;
            P1.UpdateTime = DateTime.Now;

            DeviceRealTimeTree G1 = new DeviceRealTimeTree();
            G1.NodeValue = "管理分区1";
            G1.Electricity = rd.Next(1, 5);
            G1.Signal = rd.Next(1, 5);
            G1.Status = rd.Next(1, 4);
            G1.Temperature = decimal.Parse(rd.Next(15, 30) + "." + rd.Next(0, 99));
            G1.InstallPlace = "北京天安门";
            G1.NodeType = 2;
            G1.UpdateTime = DateTime.Now;

            P1.AddNodeKey(G1);

            DeviceRealTimeTree D1 = new DeviceRealTimeTree();
            D1.NodeValue = "P001";
            D1.Electricity = rd.Next(1, 5);
            D1.Signal = rd.Next(1, 5);
            D1.Status = rd.Next(1, 4);
            D1.Temperature = decimal.Parse(rd.Next(15, 30) + "." + rd.Next(0, 99));
            D1.InstallPlace = "北京天安门";
            D1.NodeType = 3;
            D1.UpdateTime = DateTime.Now;
            G1.AddNodeKey(D1);

            DeviceRealTimeTree D2 = new DeviceRealTimeTree();
            D2.NodeValue = "P002";
            D2.Electricity = rd.Next(1, 5);
            D2.Signal = rd.Next(1, 5);
            D2.Status = rd.Next(1, 4);
            D2.Temperature = decimal.Parse(rd.Next(15, 30) + "." + rd.Next(0, 99));
            D2.InstallPlace = "北京天安门";
            D2.NodeType = 3;
            D2.UpdateTime = DateTime.Now;
            G1.AddNodeKey(D2);

            DeviceRealTimeTree P2 = new DeviceRealTimeTree();
            P2.NodeValue = "区域2";
            P2.Electricity = rd.Next(1, 5);
            P2.Signal = rd.Next(1, 5);
            P2.Status = rd.Next(1, 4);
            P2.Temperature = decimal.Parse(rd.Next(15, 30) + "." + rd.Next(0, 99));
            P2.InstallPlace = "北京天安门";
            P2.NodeType = 1;
            P2.UpdateTime = DateTime.Now;

            DeviceRealTimeTree G2 = new DeviceRealTimeTree();
            G2.NodeValue = "管理分区2";
            G2.Electricity = rd.Next(1, 5);
            G2.Signal = rd.Next(1, 5);
            G2.Status = rd.Next(1, 4);
            G2.Temperature = decimal.Parse(rd.Next(15, 30) + "." + rd.Next(0, 99));
            G2.InstallPlace = "北京天安门";
            G2.NodeType = 2;
            G2.UpdateTime = DateTime.Now;
            P2.AddNodeKey(G2);

            DeviceRealTimeTree D3 = new DeviceRealTimeTree();
            D3.NodeValue = "P003";
            D3.Electricity = rd.Next(1, 5);
            D3.Signal = rd.Next(1, 5);
            D3.Status = rd.Next(1, 4);
            D3.Temperature = decimal.Parse(rd.Next(15, 30) + "." + rd.Next(0, 99));
            D3.InstallPlace = "北京天安门";
            D3.NodeType = 3;
            D3.UpdateTime = DateTime.Now;
            G2.AddNodeKey(D3);

            DeviceRealTimeTree D4 = new DeviceRealTimeTree();
            D4.NodeValue = "P004";
            D4.Electricity = rd.Next(1, 5);
            D4.Signal = rd.Next(1, 5);
            D4.Status = rd.Next(1, 4);
            D4.Temperature = decimal.Parse(rd.Next(15, 30) + "." + rd.Next(0, 99));
            D4.InstallPlace = "北京天安门";
            D4.NodeType = 3;
            D4.UpdateTime = DateTime.Now;
            G2.AddNodeKey(D4);

            lst.Add(P1);
            lst.Add(P2);

            #endregion  

            string reaTimedata = BinaryObjTransfer.JsonSerializer<List<DeviceRealTimeTree>>(lst);
            string alarmData =  DateTime.Now.ToString() + "告警数据";
            string callData =  DateTime.Now.ToString() + "用户事件";
            if (ReaTimeDataReceived != null)
            {
                this.ReaTimeDataReceived(reaTimedata);
            }

            if (AlarmDataReceived != null)
            {
                this.AlarmDataReceived(alarmData);
            }

            if (CallDataReceived != null)
            {
                this.CallDataReceived(callData);
            }
        }
    }
}
