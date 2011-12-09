using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;

namespace Scada.BLL.Implement
{

    public delegate void DataReceivedHandle(XElement data);
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
            //模拟要发送的数据
            XElement reaTimedata = new XElement("Root", new XElement("Device", DateTime.Now.ToString() + "实时数据到达！"));
            XElement alarmData = new XElement("Root", new XElement("Device", DateTime.Now.ToString() + "告警数据到达！"));
            XElement callData = new XElement("Root", new XElement("Device", DateTime.Now.ToString() + "呼叫数据到达！"));

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
