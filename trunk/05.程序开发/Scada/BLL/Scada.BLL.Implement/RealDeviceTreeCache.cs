using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;


using Scada.DAL.Ado;
using Scada.Model.Entity;
using Scada.Utility.Common.Transfer;


namespace Scada.BLL.Implement
{



    public class RealDeviceTreeCache
    {

        #region 变量声明

        private string _jsonTreeList = string.Empty;

        private List<DeviceTreeNode> _treeList = null;

        private Scada.DAL.Linq.SCADADataContext sCADADataContext = null;

        #endregion


        #region 静态构造

        private static RealDeviceTreeCache _realDeviceTreeCache;

        public static RealDeviceTreeCache getInstance()
        {
            if (_realDeviceTreeCache == null)
            {
                _realDeviceTreeCache = new RealDeviceTreeCache();
            }
            return _realDeviceTreeCache;
        }

        #endregion


        #region 构造函数

        public RealDeviceTreeCache()
        {
            sCADADataContext = new DAL.Linq.SCADADataContext();
            this._treeList = new List<DeviceTreeNode>();
        }

        #endregion


        #region 开放方法

        /// <summary>
        /// 开放方法
        /// </summary>
        public void LoadDeviceTreeCache()
        {
            if (string.IsNullOrEmpty(_jsonTreeList))
                this.GetDeviceTreeList();
        }

        public string GetDeviceTreeCache()
        {
            if (string.IsNullOrEmpty(_jsonTreeList))
                this.GetDeviceTreeList();
            return _jsonTreeList;
        }

        //TODO 更改设备刷新树形结构

        #endregion


        #region 私有方法

        private void GetDeviceTreeList()
        {
            List<DeviceTreeNode> areaTable = this.getTreeNodeChild(null, string.Empty);
            foreach (DeviceTreeNode area in areaTable)
            {
                List<DeviceTreeNode> ManageTable = this.getTreeNodeChild(area.NodeKey, string.Empty);
                foreach (DeviceTreeNode manage in ManageTable)
                {
                    List<DeviceTreeNode> ManTable = this.getTreeNodeChild(manage.NodeKey, string.Empty);
                    for (int i = 0; i < ManTable.Count; i++)
                    {
                        DeviceTreeNode man = ManTable[i];
                        man.NodeChild = getTreeNodeDevice(man.NodeKey, string.Empty);
                        manage.AddNodeKey(man);
                    }
                    area.AddNodeKey(manage);
                }
                _treeList.Add(area);
            }
            _jsonTreeList = BinaryObjTransfer.JsonSerializer<List<DeviceTreeNode>>(_treeList);

        }

        private List<DeviceTreeNode> getTreeNodeChild(Guid? nodeKey, String prefix)
        {
            List<DeviceTreeNode> result = new List<DeviceTreeNode>();
            string sSql = " Select id,name,Level from DeviceTree";
            if (nodeKey != null)
            {
                sSql = sSql + " Where ParentID ='" + nodeKey.ToString().ToUpper() + "' Order by Sort ";
            }
            else
            {
                sSql = sSql + " Where ParentID Is Null";
            }
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            foreach (DataRow item in ds.Rows)
            {
                result.Add(new DeviceTreeNode
                {
                    NodeType = Convert.ToInt32(item["Level"]),
                    NodeValue = prefix + item["Name"].ToString(),
                    NodeKey = new Guid(item["id"].ToString()),

                });
            }
            return result;
        }

        private List<DeviceTreeNode> getTreeNodeDevice(Guid nodeKey, String prefix)
        {
            var devList = sCADADataContext.DeviceInfos.Where(e => e.ManageAreaID == nodeKey).OrderBy(e => e.DeviceNo);
            List<DeviceTreeNode> result = devList.Select(e => new DeviceTreeNode()
            {
                NodeType = 3,
                NodeValue = prefix + e.DeviceNo,
                NodeKey = e.ID,
                Longitude = float.Parse(e.Longitude.HasValue ? e.Longitude.Value.ToString() : "0"),
                Latitude = float.Parse(e.Latitude.HasValue ? e.Latitude.Value.ToString() : "0"),
                InstallPlace = e.InstallPlace,
                Comment = e.Comment,
                High = float.Parse(e.High.HasValue ? e.High.Value.ToString() : "0"),
                MaintenanceName = e.MaintenancePeople == null ? string.Empty : e.MaintenancePeople.Name,
                Mobile = e.MaintenancePeople == null ? string.Empty : e.MaintenancePeople.Mobile

            }).ToList();
            return result;
        }

        #endregion


    }

}
