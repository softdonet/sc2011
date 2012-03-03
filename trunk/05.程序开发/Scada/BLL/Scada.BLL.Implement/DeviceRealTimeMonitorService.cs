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
            timer = new Timer(new System.Threading.TimerCallback(CheckDBMessages), this, 2000, 5000);
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

            //1列出实行数据
            List<DeviceRealTime> readTimeDatas = this.LoadDeviceRealTime();

            //2列出树型结构
            List<DeviceRealTimeTree> readlTimeTreeDatas = this.LoadDeviceRealTree();

            //3更新实体设备
            this.UpdateEntityDevice(readlTimeTreeDatas, readTimeDatas);

            //4序列化数据
            string reaTimedata = BinaryObjTransfer.JsonSerializer<List<DeviceRealTimeTree>>(readlTimeTreeDatas);

            //5回调数据
            if (ReaTimeDataReceived != null)
                this.ReaTimeDataReceived(reaTimedata);

        }

        private List<DeviceRealTime> LoadDeviceRealTime()
        {
            List<DeviceRealTime> result = new List<DeviceRealTime>();
            string sSql = @" Select AA.DeviceID,BB.InstallPlace,AA.UpdateTime,AA.Temperature,
                                AA.Electricity,AA.Signal,AA.Status
                            from (
            Select TT.* from DeviceRealTime TT Where TT.UpdateTime =
                (Select Max(UpdateTime) from DeviceRealTime 
                Where DeviceID = TT.DeviceID)) AA
                    Inner Join DeviceInfo BB On AA.DeviceID=BB.ID";

            DataTable dtInfo = SqlHelper.ExecuteDataTable(sSql);
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
                        if (realTime == null) { continue; }
                        realThi.InstallPlace = realTime.InstallPlace;
                        realThi.Temperature = realTime.Temperature;
                        realThi.Electricity = realTime.Electricity;
                        realThi.Signal = realTime.Signal;
                        realThi.Status = realTime.Status;
                        realThi.UpdateTime = realTime.UpdateTime;
                    }
                    this.UpdateVirtualDevice(realSec);
                }
                this.UpdateVirtualDevice(realFir);
            }
        }

        private void UpdateVirtualDevice(DeviceRealTimeTree treeDatas)
        {

            List<DeviceRealTimeTree> realTree = treeDatas.NodeChild;
            if (realTree == null) { return; }

            //时间
            treeDatas.UpdateTime = realTree.Max(p => p.UpdateTime);

            //温度
            decimal? avgTemperature = realTree.Average(p => p.Temperature);
            if (avgTemperature != null)
                avgTemperature = Math.Round((decimal)avgTemperature, 2);
            treeDatas.Temperature = avgTemperature;

            //电量
            treeDatas.Electricity = (int?)realTree.Average(p => p.Electricity);

            //信号强度
            treeDatas.Signal = (int?)realTree.Average(p => p.Signal);

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


    }
}
