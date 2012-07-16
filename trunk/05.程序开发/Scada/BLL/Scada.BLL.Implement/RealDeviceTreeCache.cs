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

        public string ReLoadDeviceTreeCache()
        {
            this._jsonTreeList = String.Empty;
            this._treeList.Clear();
            this.GetDeviceTreeList();
            return _jsonTreeList;
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
            //_treeList = new List<DeviceTreeNode>();
            _treeList.Clear();
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
            List<DeviceTreeNode> result = new List<DeviceTreeNode>();
            string sSql = @" Select AA.ID,AA.DeviceNo,Isnull(AA.Longitude,0) As Longitude, IsNull(AA.Latitude,0) As Latitude ,
                                AA.InstallPlace,AA.Comment,IsNull(AA.High,0) As High ,
                                BB.Name,BB.Mobile
                                from DeviceInfo AA 
                                Left JOIN  MaintenancePeople BB On AA.MaintenancePeopleID=BB.ID
                                WHere AA.ManageAreaID ='" + nodeKey.ToString().ToUpper() + "' Order by AA.DeviceNo";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            foreach (DataRow item in ds.Rows)
            {
                result.Add(new DeviceTreeNode
                {
                    NodeType = 3,
                    NodeValue = prefix + item["DeviceNo"].ToString(),
                    NodeKey = new Guid(item["ID"].ToString()),
                    Longitude = float.Parse(item["Longitude"].ToString()),
                    Latitude = float.Parse(item["Latitude"].ToString()),
                    InstallPlace = item["InstallPlace"].ToString(),
                    Comment = item["Comment"].ToString(),
                    High = float.Parse(item["High"].ToString()),
                    MaintenanceName = item["Name"].ToString(),
                    Mobile = item["Mobile"].ToString()
                });
            }
            return result;
        }

        #endregion


    }

}
