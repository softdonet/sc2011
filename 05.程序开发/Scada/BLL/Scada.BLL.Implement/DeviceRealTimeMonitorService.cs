using System;
using System.Xml;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;
using System.Threading;
using System.Collections.Generic;


using Scada.DAL.Ado;
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

        #region 变量声明

        private Timer timer = null;
        
        public event DataReceivedHandle ReaTimeDataReceived;
        public event DataReceivedHandle AlarmDataReceived;
        public event DataReceivedHandle CallDataReceived;

        #endregion


        #region 构造函数

        public DeviceRealTimeMonitorService()
        {
            timer = new Timer(new System.Threading.TimerCallback(CheckDBMessages), this, 2000, 2000);
        }

        #endregion


        #region 私有方法

        private void CheckDBMessages(object o)
        {

            //实行刷新
            this.SendReaTimedata(o);

            //告警数据
            this.SendAlarmData(o);

            //用户事件
            this.SendCallData(o);

        }

        //实行刷新
        private void SendReaTimedata(object o)
        {

            //0实行刷新时间
            DateTime realTime = new DateTime(2011, 11, 12, 13, 15, 0);

            //1列出实行数据
            List<DeviceRealTime> readTimeDatas = this.LoadDeviceRealTime(realTime);

            //2列出树型结构
            List<DeviceRealTimeTree> readlTimeTreeDatas = this.LoadDeviceRealTree();

            //3更新实体设备
            this.UpdateEntityDevice(readlTimeTreeDatas, readTimeDatas);

            //4更新虚拟设备
            this.UpdateVirtualDevice(readlTimeTreeDatas);

            //5序列化数据
            string reaTimedata = BinaryObjTransfer.JsonSerializer<List<DeviceRealTimeTree>>(readlTimeTreeDatas);

            //6回调数据
            if (ReaTimeDataReceived != null)
                this.ReaTimeDataReceived(reaTimedata);

        }

        private List<DeviceRealTime> LoadDeviceRealTime(DateTime realTime)
        {
            List<DeviceRealTime> result = new List<DeviceRealTime>();
            string sSql = @" Select AA.DeviceID,BB.InstallPlace,AA.UpdateTime,AA.Temperature,
                                AA.Electricity,AA.Signal,AA.Status
                            from DeviceRealTime AA 
                            Inner Join DeviceInfo BB On AA.DeviceID=BB.ID 
                            Where 1=1  And UpdateTime='" + realTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            DataTable dtInfo = SqlHelper.ExecuteDataTable(sSql.ToString());
            if (dtInfo.Rows.Count == 0) { return result; }
            foreach (DataRow dr in dtInfo.Rows)
            {
                result.Add(new DeviceRealTime
                {
                    DeviceID = new Guid(dr["DeviceID"].ToString()),
                    InstallPlace = dr["InstallPlace"].ToString(),
                    UpdateTime = Convert.ToDateTime(dr["UpdateTime"]),
                    Temperature = Convert.ToDecimal(dr["Temperature"]),
                    Electricity = Convert.ToInt32(dr["Electricity"]),
                    Signal = Convert.ToInt32(dr["Signal"]),
                    Status = Convert.ToInt32(dr["Status"])
                });
            }
            return result;
        }

        private List<DeviceRealTimeTree> LoadDeviceRealTree()
        {
            List<DeviceRealTimeTree> result = new List<DeviceRealTimeTree>();
            List<DeviceRealTimeTree> realTree = this.getTreeNodeChild(null);
            foreach (DeviceRealTimeTree area in realTree)
            {
                List<DeviceRealTimeTree> ManageTable = this.getTreeNodeChild(area.NodeKey);
                foreach (DeviceRealTimeTree manage in ManageTable)
                {
                    manage.NodeChild = getTreeNodeDevice(manage.NodeKey);
                    area.AddNodeKey(manage);
                }
                result.Add(area);
            }
            return result;
        }

        private List<DeviceRealTimeTree> getTreeNodeChild(Guid? nodeKey)
        {
            int nodeIndex;
            List<DeviceRealTimeTree> result = new List<DeviceRealTimeTree>();
            string sSql = "select id,name from DeviceTree";
            if (nodeKey != null)
            {
                nodeIndex = 2;
                sSql = sSql + " where ParentID ='" + nodeKey.ToString().ToUpper() + "'";
            }
            else
            {
                nodeIndex = 1;
                sSql = sSql + " Where ParentID Is Null";
            }
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            foreach (DataRow item in ds.Rows)
            {
                result.Add(new DeviceRealTimeTree
                {
                    NodeType = nodeIndex,
                    NodeValue = item["Name"].ToString(),
                    NodeKey = new Guid(item["id"].ToString())
                });
            }
            return result;
        }

        private List<DeviceRealTimeTree> getTreeNodeDevice(Guid nodeKey)
        {
            List<DeviceRealTimeTree> result = new List<DeviceRealTimeTree>();
            string sSql = "select id,DeviceNo from DeviceInfo Where ManageAreaID ='" + nodeKey.ToString().ToUpper() + "'";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            foreach (DataRow item in ds.Rows)
            {
                result.Add(new DeviceRealTimeTree
                {
                    NodeType = 3,
                    NodeValue = item["DeviceNo"].ToString(),
                    NodeKey = new Guid(item["id"].ToString())
                });
            }
            return result;
        }

        private void UpdateEntityDevice(List<DeviceRealTimeTree> readlTimeTreeDatas, List<DeviceRealTime> readTimeDatas)
        {
            //区域
            foreach (DeviceRealTimeTree realFir in readlTimeTreeDatas)
            {
                //管理分区
                foreach (DeviceRealTimeTree realSec in realFir.NodeChild)
                {
                    //实体设备
                    foreach (DeviceRealTimeTree realThi in realSec.NodeChild)
                    {
                        DeviceRealTime realTime = readTimeDatas.Find(x => x.DeviceID == realThi.NodeKey);
                        realThi.InstallPlace = realTime.InstallPlace;
                        realThi.Temperature = realTime.Temperature;
                        realThi.Electricity = realTime.Electricity;
                        realThi.Signal = realTime.Signal;
                        realThi.Status = realTime.Status;
                        realThi.UpdateTime = realTime.UpdateTime;

                        realFir.UpdateTime = realTime.UpdateTime;
                        realFir.UpdateTime = realTime.UpdateTime;

                    }
                }
            }
        }

        private void UpdateVirtualDevice(List<DeviceRealTimeTree> readlTimeTreeDatas)
        {

        }


        //告警数据
        private void SendAlarmData(object o)
        {
            string alarmData = DateTime.Now.ToString() + "告警数据";
            if (AlarmDataReceived != null)
                this.AlarmDataReceived(alarmData);
        }

        //用户事件
        private void SendCallData(object o)
        {
            string callData = DateTime.Now.ToString() + "用户事件";
            if (CallDataReceived != null)
                this.CallDataReceived(callData);
        }

        #endregion



        #region 模拟数据

        private List<DeviceRealTimeTree> AnologData(object o)
        {

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

            return lst;

        }

        #endregion




    }
}
