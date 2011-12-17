using System;
using System.Text;
using System.Data;
using System.Collections.Generic;

using Scada.BLL.Contract;

using Scada.DAL.Ado;
using Scada.Model.DB;
using Scada.Model.Entity;
using Scada.Utility.Common.Transfer;



namespace Scada.BLL.Implement
{

    public class ScadaDeviceService : IScadaDeviceService
    {

        #region 测试流程

        public int Add(int x, int y)
        {
            return x + y;
        }

        #endregion

        #region 设备查询


        /// <summary>
        /// 设备查询
        /// </summary>
        /// <param name="DeviceID"></param>
        /// <param name="DeviceType">1区域,2管理分区,3设备实体</param>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public string GetListDeviceInfo(Guid DeviceID, Int32 DeviceType,
                                    DateTime? StartDate, DateTime? EndDate)
        {

            string result = string.Empty;
            List<DeviceRealTime> deviceRealTimes = new List<DeviceRealTime>();
            StringBuilder sSql = new StringBuilder();
            sSql.Append(@" Select AA.ID,BB.DeviceNo,BB.InstallPlace,
                           AA.UpdateTime,Temperature,Electricity,Signal
                            from DeviceRealTime AA 
                            Inner Join DeviceInfo BB On AA.DeviceID=BB.ID 
                            Where 1=1 ");

            DataTable ds = null;
            if (DeviceType == 1)
            {
                string sql = string.Format(@" Select ID from DeviceTree 
                                                    Where ParentID = '{0}'", DeviceID.ToString());
                ds = SqlHelper.ExecuteDataTable(sql);
                if (ds == null || ds.Rows.Count == 0) { return result; }
                string sqlWhere = string.Empty;
                foreach (var item in ds.Rows)
                {
                    sqlWhere = sqlWhere + string.Format("'{0}',", item);
                }
                sqlWhere = sqlWhere.Substring(0, sqlWhere.Length - 1);
                sSql.Append(" And BB.ManageAreaID In (" + sqlWhere + ")");
            }
            else if (DeviceType == 2)
                sSql.Append(" And BB.ManageAreaID ='" + DeviceID + "'");
            else if (DeviceType == 3)
                sSql.Append(" And AA.DeviceID='" + DeviceID.ToString().ToUpper() + "'");

            if (StartDate != null)
                sSql.Append(" And AA.UpdateTime >='" + Convert.ToDateTime(StartDate).ToString("yyyy-MM-dd hh:mm:ss") + "'");
            if (EndDate != null)
                sSql.Append(" And AA.UpdateTime <'" + Convert.ToDateTime(EndDate).ToString("yyyy-MM-dd hh:mm:ss") + "'");
            try
            {
                ds = SqlHelper.ExecuteDataTable(sSql.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (ds == null || ds.Rows.Count == 0) { return result; }
            foreach (DataRow dr in ds.Rows)
            {

                //add by zgj 未判断 DBNull
                deviceRealTimes.Add(
                    new DeviceRealTime
                    {
                        ID = new Guid(dr["ID"].ToString()),
                        DeviceNo = dr["DeviceNo"].ToString(),
                        InstallPlace = dr["InstallPlace"].ToString(),
                        UpdateTime = Convert.ToDateTime(dr["UpdateTime"]),
                        Temperature = Convert.ToDecimal(dr["Temperature"]),
                        Electricity = Convert.ToInt32(dr["Electricity"]),
                        Signal = Convert.ToInt32(dr["Signal"])
                    }
                );
            }
            result = BinaryObjTransfer.JsonSerializer<List<DeviceRealTime>>(deviceRealTimes);
            return result;
        }


        #endregion

        #region 设备管理

        public Boolean AddDeviceInfo(string deviceInfo)
        {
            return false;
        }

        public Boolean UpdateDeviceInfo(string deviceInfo)
        {
            return false;
        }

        public Boolean DeleteDeviceInfo(string deviceGuid)
        {
            return false;
        }

        public string ViewDeviceInfo(string deviceGuid)
        {
            return string.Empty;
        }

        public string ListMaintenancePeople()
        {
            return string.Empty;
        }

        public string ListDeviceTreeView()
        {
            List<DeviceTreeNode> treeList = new List<DeviceTreeNode>();
            List<DeviceTreeNode> areaTable = this.getTreeNodeChild(null);
            foreach (DeviceTreeNode area in areaTable)
            {
                List<DeviceTreeNode> ManageTable = this.getTreeNodeChild(area.NodeKey);
                foreach (DeviceTreeNode manage in ManageTable)
                {
                    manage.NodeChild = getTreeNodeDevice(manage.NodeKey);
                    area.AddNodeKey(manage);
                }
                treeList.Add(area);
            }

            #region 模拟树数据
          
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
         
            #endregion

            return BinaryObjTransfer.JsonSerializer<List<DeviceTreeNode>>(lst);
        }

        private List<DeviceTreeNode> getTreeNodeChild(Guid? nodeKey)
        {
            int nodeIndex;
            List<DeviceTreeNode> result = new List<DeviceTreeNode>();
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
                result.Add(new DeviceTreeNode
                {
                    NodeType = nodeIndex,
                    NodeValue = item["Name"].ToString(),
                    NodeKey = new Guid(item["id"].ToString())
                });
            }
            return result;
        }

        private List<DeviceTreeNode> getTreeNodeDevice(Guid nodeKey)
        {
            List<DeviceTreeNode> result = new List<DeviceTreeNode>();
            string sSql = "select id,DeviceNo from DeviceInfo Where ManageAreaID ='" + nodeKey.ToString().ToUpper() + "'";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            foreach (DataRow item in ds.Rows)
            {
                result.Add(new DeviceTreeNode
                {
                    NodeType = 3,
                    NodeValue = item["DeviceNo"].ToString(),
                    NodeKey = new Guid(item["id"].ToString())
                });
            }
            return result;
        }

        #endregion

    }

}
