using System;
using System.Text;
using System.Data;
using System.Collections.Generic;


using Scada.DAL.Ado;
using Scada.BLL.Contract;
using Scada.Model.Entity;
using System.Data.SqlClient;
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
            Int32 rowNum = 0;
            Boolean result = false;
            SqlParameter para = null;
            StringBuilder sSql = new StringBuilder();
            List<SqlParameter> sSqlWhere = new List<SqlParameter>();

            try
            {
                DeviceInfo deviceValue = BinaryObjTransfer.JsonDeserialize<DeviceInfo>(deviceInfo);

                sSql.Append(@" Update DeviceInfo 
                                    Set DeviceNo = @DeviceNo,HardType = @HardType,ProductDate = @ProductDate,
                                       DeviceMac = @DeviceMac,SimNo = @SimNo,InstallPlace = @InstallPlace");

                para = new SqlParameter();
                para.DbType = DbType.String;
                para.ParameterName = "@DeviceNo";
                para.Value = deviceValue.DeviceNo;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.String;
                para.ParameterName = "@HardType";
                para.Value = deviceValue.HardType;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.DateTime;
                para.ParameterName = "@ProductDate";
                para.Value = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.String;
                para.ParameterName = "@DeviceMac";
                para.Value = deviceValue.DeviceMAC;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.String;
                para.ParameterName = "@SimNo";
                para.Value = deviceValue.SIMNo;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.String;
                para.ParameterName = "@InstallPlace";
                para.Value = deviceValue.InstallPlace;
                sSqlWhere.Add(para);

                sSql.Append(@" Where id ='" + deviceValue.ID.ToString() + "'");
                rowNum = SqlHelper.ExecuteNonQuery(CommandType.Text, sSql.ToString(), sSqlWhere.ToArray());
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public Boolean DeleteDeviceInfo(string deviceGuid)
        {
            return false;
            /*删除一系列*/

        }

        public string ViewDeviceInfo(string deviceGuid)
        {
            string result = string.Empty;
            string sSql = " select * from DeviceInfo where id ='" + deviceGuid + "'";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            if (ds == null || ds.Rows.Count == 0) { return result; }
            DeviceInfo deviceInfo = new DeviceInfo();
            foreach (DataRow item in ds.Rows)
            {

                deviceInfo.ID = new Guid(item["id"].ToString());
                deviceInfo.DeviceNo = item["deviceno"].ToString();
                deviceInfo.HardType = item["hardtype"].ToString();
                deviceInfo.ProductDate = Convert.ToDateTime(item["productdate"]);
                deviceInfo.DeviceMAC = item["devicemac"].ToString();
                deviceInfo.SIMNo = item["simno"].ToString();
                deviceInfo.ManageAreaID = new Guid(item["manageareaid"].ToString());
                deviceInfo.InstallPlace = item["installplace"].ToString();


                decimal? decValue = null;

                /*
                if (item["longitude"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["longitude"]);
                deviceInfo.Longitude = decValue;

                decValue = null;
                if (item["dimensionality"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["dimensionality"]);
                deviceInfo.Dimensionality = decValue;
                */

                decValue = null;
                if (item["high"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["high"]);
                deviceInfo.High = decValue;

                deviceInfo.Comment = item["comment"].ToString();
                deviceInfo.ConnectPoint = item["connectpoint"].ToString();
                deviceInfo.UserName = item["username"].ToString();
                deviceInfo.Password = item["password"].ToString();
                deviceInfo.Coordinate = item["coordinate"].ToString();

                Int32? intValue = null;
                if (item["connectType"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["connectType"]);
                deviceInfo.ConnectType = intValue;

                deviceInfo.MainDNS = item["maindns"].ToString();
                deviceInfo.SecondDNS = item["seconddns"].ToString();
                deviceInfo.CenterIP = item["centerip"].ToString();
                deviceInfo.Domain = item["domain"].ToString();

                intValue = null;
                if (item["port"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["port"]);
                deviceInfo.port = intValue;

                intValue = null;
                if (item["collectfreq"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["collectfreq"]);
                deviceInfo.CollectFreq = intValue;

                intValue = null;
                if (item["reportinterval"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["reportinterval"]);
                deviceInfo.ReportInterval = intValue;

                decValue = null;
                if (item["alarmtop"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["alarmtop"]);
                deviceInfo.AlarmTop = decValue;

                decValue = null;
                if (item["alarmdown"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["alarmdown"]);
                deviceInfo.AlarmDown = decValue;

                decValue = null;
                if (item["windage"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["windage"]);
                deviceInfo.Windage = decValue;

                deviceInfo.Version = item["version"].ToString();

                intValue = null;
                if (item["timetype"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["timetype"]);
                deviceInfo.TimeType = intValue;

                deviceInfo.LCDScreenDisplayType = item["lcdscreendisplaytype"].ToString();

                intValue = null;
                if (item["useurgencybutton"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["useurgencybutton"]);
                deviceInfo.UseUrgencyButton = intValue;

                intValue = null;
                if (item["process1enable"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["process1enable"]);
                deviceInfo.Process1Enable = intValue;

                decValue = null;
                if (item["process1highervalue"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["process1highervalue"]);
                deviceInfo.Process1HigherValue = decValue;

                intValue = null;
                if (item["process1highvalue"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["process1highvalue"]);
                deviceInfo.Process1HighValue = intValue;

                decValue = null;
                if (item["process1lowervalue"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["process1lowervalue"]);
                deviceInfo.Process1LowerValue = decValue;

                decValue = null;
                if (item["process1lowvalue"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["process1lowvalue"]);
                deviceInfo.Process1LowValue = decValue;

            }
            result = BinaryObjTransfer.JsonSerializer<DeviceInfo>(deviceInfo);
            return result;
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
            return BinaryObjTransfer.JsonSerializer<List<DeviceTreeNode>>(treeList);
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
            string sSql = @"select id,DeviceNo from DeviceInfo 
                                Where ManageAreaID ='" + nodeKey.ToString().ToUpper() + "'";
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

        #region 设备告警

        public string GetListDeviceAlarmInfo()
        {

            List<DeviceAlarm> result = new List<DeviceAlarm>();
            string sSql = @" Select Top 100 ID,DeviceID,DeviceNo,EventType,
                              EventLevel,StartTime,EndTime,ConfirmTime,
                              DealStatus,DealPeople,Comment from DeviceAlarm";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            DeviceAlarm alarm = null;
            foreach (DataRow item in ds.Rows)
            {

                alarm = new DeviceAlarm();
                alarm.ID = new Guid(item["ID"].ToString());
                alarm.DeviceID = new Guid(item["DeviceID"].ToString());
                alarm.DeviceNo = item["DeviceNo"].ToString();
                int? intType = null;
                if (item["EventType"] != DBNull.Value)
                    intType = Convert.ToInt32(item["EventType"]);
                alarm.EventType = intType;

                intType = null;
                if (item["EventType"] != DBNull.Value)
                    intType = Convert.ToInt32(item["EventLevel"]);
                alarm.EventLevel = intType;

                DateTime? dtValue = null;
                if (item["StartTime"] != DBNull.Value)
                    dtValue = Convert.ToDateTime(item["StartTime"]);
                alarm.StartTime = dtValue;

                dtValue = null;
                if (item["EndTime"] != DBNull.Value)
                    dtValue = Convert.ToDateTime(item["EndTime"]);
                alarm.EndTime = dtValue;

                dtValue = null;
                if (item["ConfirmTime"] != DBNull.Value)
                    dtValue = Convert.ToDateTime(item["ConfirmTime"]);
                alarm.ConfirmTime = dtValue;

                alarm.DealStatus = item["DealStatus"].ToString();
                alarm.DealPeople = item["DealPeople"].ToString();
                alarm.Comment = item["Comment"].ToString();
                result.Add(alarm);

            }

            return BinaryObjTransfer.JsonSerializer<List<DeviceAlarm>>(result);

        }


        public Boolean UpdateDeviceAlarmInfo(Guid AlarmId, DateTime ConfirmTime, String Comment, String DealPeople)
        {
            string sSql = @" Update DeviceAlarm 
                            Set ConfirmTime='" + ConfirmTime.ToString("yyyy-MM-dd hh:mm:ss") + @"',
                                   DealStatus='已确认', DealPeople='" + DealPeople + "',Comment='" + Comment + @"'
                            Where ID ='" + AlarmId.ToString().ToUpper() + "'";
            Boolean result = false;
            try
            {
                Int32 intQuery = SqlHelper.ExecuteNonQuery(sSql);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        #endregion

        #region 用户事件

        public string GetListUserEventInfo()
        {

            List<UserEventTab> userEvents = new List<UserEventTab>();
            string sSql = @" Select ID,EventNo,DeviceID,DeviceNo,
                                State,Count,RequestTime from UserEvent";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            UserEventTab userEvent;
            foreach (DataRow item in ds.Rows)
            {
                userEvent = new UserEventTab();
                userEvent.ID = new Guid(item["ID"].ToString());
                userEvent.EventNo = item["EventNo"].ToString();
                userEvent.DeviceID = new Guid(item["DeviceID"].ToString());
                userEvent.DeviceNo = item["DeviceNo"].ToString();
                Int32? intValue = null;
                if (item["State"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["State"]);
                userEvent.State = intValue;

                intValue = null;
                if (item["Count"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["Count"]);
                userEvent.Count = intValue;

                DateTime? dTime = null;
                if (item["RequestTime"] != DBNull.Value)
                    dTime = Convert.ToDateTime(item["RequestTime"]);
                userEvent.RequestTime = dTime;

                userEvents.Add(userEvent);

            }

            return BinaryObjTransfer.JsonSerializer<List<UserEventTab>>(userEvents);

        }

        public string GetUserEventKeyInfo(Guid EventKey)
        {
            return string.Empty;
        }

        public Boolean UpdateUserEventInfo(string EventDetails)
        {
            return false;
        }

        public string GetListStepInfo()
        {
            List<StepInfo> stepInfos = new List<StepInfo>();
            string sSql = " select ID,StepName from StepInfo";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            StepInfo stepInfo = null;
            foreach (DataRow item in ds.Rows)
            {
                stepInfo = new StepInfo();
                stepInfo.StepID = new Guid(item["ID"].ToString());
                stepInfo.StepName = item["StepName"].ToString();
                stepInfos.Add(stepInfo);
            }
            return BinaryObjTransfer.JsonSerializer<List<StepInfo>>(stepInfos);
        }

        #endregion


    }

}
