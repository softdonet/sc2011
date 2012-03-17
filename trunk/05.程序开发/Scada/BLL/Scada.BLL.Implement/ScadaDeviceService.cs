using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;


using Scada.DAL.Ado;
using Scada.BLL.Contract;
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
                           AA.UpdateTime,AA.Temperature1,AA.Electricity,AA.Signal
                            from DeviceRealTime AA 
                            Inner Join DeviceInfo BB On AA.DeviceID=BB.ID 
                            Where 1=1 ");

            DataTable ds = null;

            //区域
            if (DeviceType == 0)
            {

            }
            //管理分区
            else if (DeviceType == 1)
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
            //组
            else if (DeviceType == 2)
                sSql.Append(" And BB.ManageAreaID ='" + DeviceID + "'");
            //设备
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
                        Temperature1 = Convert.ToDecimal(dr["Temperature1"]),
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
            Int32 rowNum = 0;
            Boolean result = false;
            SqlParameter para = null;

            StringBuilder sSql = new StringBuilder();
            List<SqlParameter> sSqlWhere = new List<SqlParameter>();
            try
            {
                DeviceInfo deviceValue = BinaryObjTransfer.JsonDeserialize<DeviceInfo>(deviceInfo);
                sSql.Append(@" Insert Into DeviceInfo(ID,DeviceNo,HardType,ProductDate,DeviceMAC,
                                SIMNo,ManageAreaID,InstallPlace,Longitude,Dimensionality,
                                High,Comment,ConnectPoint) Values (@ID,@DeviceNo,@HardType,@ProductDate,@DeviceMAC,
                                @SIMNo,@ManageAreaID,@InstallPlace,@Longitude,@Dimensionality,
                                @High,@Comment,@ConnectPoint)");
                para = new SqlParameter();
                para.DbType = DbType.Guid;
                para.ParameterName = "@ID";
                para.Value = deviceValue.ID;
                sSqlWhere.Add(para);

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
                para.Value = deviceValue.ProductDate;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.String;
                para.ParameterName = "@DeviceMAC";
                para.Value = deviceValue.DeviceMAC;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.String;
                para.ParameterName = "@SIMNo";
                para.Value = deviceValue.SIMNo;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.Guid;
                para.ParameterName = "@ManageAreaID";
                para.Value = deviceValue.ManageAreaID;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.String;
                para.ParameterName = "@InstallPlace";
                para.Value = deviceValue.InstallPlace;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.Decimal;
                para.ParameterName = "@Longitude";
                para.Value = deviceValue.Longitude;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.Decimal;
                para.ParameterName = "@Dimensionality";
                //para.Value = deviceValue.Dimensionality;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.Decimal;
                para.ParameterName = "@High";
                para.Value = deviceValue.High;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.String;
                para.ParameterName = "@Comment";
                para.Value = deviceValue.Comment;
                sSqlWhere.Add(para);

                para = new SqlParameter();
                para.DbType = DbType.String;
                para.ParameterName = "@ConnectPoint";
                //para.Value = deviceValue.ConnectPoint;
                sSqlWhere.Add(para);

                rowNum = SqlHelper.ExecuteNonQuery(CommandType.Text, sSql.ToString(), sSqlWhere.ToArray());
                //设备维护人员
                //UpdateDevicePeople(deviceValue.ID, deviceValue.DeviceMainValue);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
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

                //维护人列表
                //UpdateDevicePeople(deviceValue.ID, deviceValue.DeviceMainValue);

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public Boolean DeleteDeviceInfo(Guid deviceGuid)
        {
            Boolean result = false;
            try
            {
                //1)删除维护人列表
                string sSql = @" Delete MaintenancePeople 
                                Where DeviceID ='" + deviceGuid.ToString().ToUpper() + "'";
                SqlHelper.ExecuteNonQuery(sSql);

                //2)删除设备信息
                sSql = @" Delete DeviceInfo Where ID ='" + deviceGuid.ToString().ToUpper() + "'";
                SqlHelper.ExecuteNonQuery(sSql);

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }

            return result;

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

                //标识
                deviceInfo.ID = new Guid(item["id"].ToString());
                //设备MAC
                deviceInfo.DeviceMAC = item["devicemac"].ToString();
                //SIM卡号
                deviceInfo.SIMNo = item["simno"].ToString();
                //管理分区
                deviceInfo.ManageAreaID = new Guid(item["manageareaid"].ToString());
                //安装位置
                deviceInfo.InstallPlace = item["installplace"].ToString();
                //备注
                deviceInfo.Comment = item["comment"].ToString();
                //接入点
                //deviceInfo.ConnectPoint = item["connectpoint"].ToString();
                //经度
                decimal? decValue = null;
                if (item["longitude"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["longitude"]);
                deviceInfo.Longitude = decValue;
                //纬度
                decValue = null;
                if (item["latitude"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["latitude"]);
                deviceInfo.Latitude = decValue;
                //高度
                decValue = null;
                if (item["high"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["high"]);
                deviceInfo.High = decValue;

                /*
                //连接方式
                Int32? intValue = null;
                if (item["connectType"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["connectType"]);
                deviceInfo.ConnectType = intValue;
             
                //主DNS
                deviceInfo.MainDNS = item["maindns"].ToString();
                //从DNS
                deviceInfo.SecondDNS = item["seconddns"].ToString();
                //中心IP
                deviceInfo.CenterIP = item["centerip"].ToString();
                //域名
                deviceInfo.Domain = item["domain"].ToString();
                //端口
                intValue = null;
                if (item["port"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["port"]);
                deviceInfo.port = intValue;
                //采集频率
                intValue = null;
                if (item["collectfreq"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["collectfreq"]);
                deviceInfo.CollectFreq = intValue;
                //上报间隔
                intValue = null;
                if (item["reportinterval"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["reportinterval"]);
                deviceInfo.ReportInterval = intValue;
                */

                //偏差
                Int32? intValue = null;
                if (item["windage"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["windage"]);
                deviceInfo.Windage = intValue;

                /*
                 
                //报警上限
                decValue = null;
                if (item["alarmtop"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["alarmtop"]);
                deviceInfo.AlarmTop = decValue;
                //报警下限
                decValue = null;
                if (item["alarmdown"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["alarmdown"]);
                deviceInfo.AlarmDown = decValue;
                //硬件版本
                deviceInfo.Version = item["version"].ToString();

              
              deviceInfo.DeviceNo = item["deviceno"].ToString();
              deviceInfo.HardType = item["hardtype"].ToString();
              deviceInfo.ProductDate = Convert.ToDateTime(item["productdate"]);
              deviceInfo.UserName = item["username"].ToString();
              deviceInfo.Password = item["password"].ToString();
              deviceInfo.Coordinate = item["coordinate"].ToString();
              intValue = null;
              if (item["timetype"] != DBNull.Value)
                  intValue = Convert.ToInt32(item["timetype"]);
              deviceInfo.TimeType = intValue;
              */

                ////////////////////////辅助信息////////////////////////////
                //液晶屏显示类型
                //deviceInfo.LCDScreenDisplayType = item["lcdscreendisplaytype"].ToString();

                //实时模式参数
                //整点采集频率
                //整点发送频率
                //快速发送频率
                //速率
                //发送间隔

                ////////////////////////报警设置////////////////////////////
                //过程值1报警设置有效
                intValue = null;
                if (item["process1enable"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["process1enable"]);
                //deviceInfo.Process1Enable = intValue;
                //过程值1高高报警值
                decValue = null;
                if (item["process1highervalue"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["process1highervalue"]);
                //deviceInfo.Process1HigherValue = decValue;
                //过程值1高报警值
                intValue = null;
                if (item["process1highvalue"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["process1highvalue"]);
                //deviceInfo.Process1HighValue = intValue;
                //过程值1低报警值
                decValue = null;
                if (item["process1lowervalue"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["process1lowervalue"]);
                //deviceInfo.Process1LowerValue = decValue;
                //过程值1低低报警值
                decValue = null;
                if (item["process1lowvalue"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["process1lowvalue"]);
                //deviceInfo.Process1LowValue = decValue;
                //过程值1速率报警值
                decValue = null;
                if (item["process1ratevalue"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["process1ratevalue"]);
                //deviceInfo.Process1RateValue = decValue;

                //列出设备的维护人列表
                //deviceInfo.DeviceMainValue = ListDeviceMaintenancePeople(deviceInfo.ID);


            }
            result = BinaryObjTransfer.JsonSerializer<DeviceInfo>(deviceInfo);
            return result;
        }

        public string ListMaintenancePeople()
        {
            string result = string.Empty;
            List<MaintenancePeople> mainPeople = new List<MaintenancePeople>();
            mainPeople = ListMaintenancePeopleInfo();
            result = BinaryObjTransfer.JsonSerializer<List<MaintenancePeople>>(mainPeople);
            return result;


        }

        private List<MaintenancePeople> ListMaintenancePeopleInfo()
        {
            List<MaintenancePeople> result = new List<MaintenancePeople>();
            string sSql = @" Select ID,MainNo,MainName,City,Native,
                                    Telephone,MobileBack,Email from MaintenancePeople";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            if (ds == null || ds.Rows.Count == 0) { return result; }
            MaintenancePeople peoples = null;
            foreach (DataRow item in ds.Rows)
            {
                peoples = new MaintenancePeople();
                peoples.ID = new Guid(item["ID"].ToString());
                //peoples.MainNo = item["MainNo"].ToString();
                //peoples.MainName = item["MainName"].ToString();
                //peoples.City = item["City"].ToString();
                //peoples.Native = item["Native"].ToString();
                peoples.Telephone = item["Telephone"].ToString();
                //peoples.MobileBack = item["MobileBack"].ToString();
                peoples.Email = item["Email"].ToString();
                result.Add(peoples);
            }
            return result;
        }

        private List<MaintenancePeople> ListDeviceMaintenancePeople(Guid deviceID)
        {
            List<MaintenancePeople> result = new List<MaintenancePeople>();
            String sSql = @" Select AA.ID,BB.ID,BB.MainNo,BB.MainName,BB.City,
                             BB.Native,BB.Telephone,BB.MobileBack,BB.Email,AA.DeviceID
                            From MaintenancePeople AA
                            Inner JOIN MaintenancePeople BB On AA.MaintenanceID=BB.ID
                            Where AA.DeviceID ='" + deviceID.ToString().ToUpper() + "'";

            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            if (ds == null || ds.Rows.Count == 0) { return result; }
            MaintenancePeople people = null;
            foreach (DataRow item in ds.Rows)
            {
                people = new MaintenancePeople();
                people.ID = new Guid(item["ID"].ToString());
                // people.DeviceID = new Guid(item["DeviceID"].ToString());

                MaintenancePeople peoples = new MaintenancePeople();
                peoples.ID = new Guid(item["ID"].ToString());
                //peoples.MainNo = item["MainNo"].ToString();
                peoples.Name = item["Name"].ToString();
                peoples.Address = item["Address"].ToString();
                peoples.Telephone = item["Telephone"].ToString();
                peoples.Mobile = item["Mobile"].ToString();
                peoples.QQ = item["QQ"].ToString();
                peoples.MSN = item["MSN"].ToString();
                peoples.Email = item["Email"].ToString();

                //people.MaintenancePeopleInfo = peoples;
                result.Add(people);
            }
            return result;
        }

        public string ListDeviceTreeView()
        {
            List<DeviceTreeNode> treeList = new List<DeviceTreeNode>();
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
                treeList.Add(area);
            }
            return BinaryObjTransfer.JsonSerializer<List<DeviceTreeNode>>(treeList);
        }

        private List<DeviceTreeNode> getTreeNodeChild(Guid? nodeKey, String prefix)
        {
            List<DeviceTreeNode> result = new List<DeviceTreeNode>();
            string sSql = "select id,name,Level from DeviceTree";
            if (nodeKey != null)
            {
                sSql = sSql + " where ParentID ='" + nodeKey.ToString().ToUpper() + "'";
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
            string sSql = @"select id,DeviceNo,Longitude,Latitude,InstallPlace from DeviceInfo 
                                Where ManageAreaID ='" + nodeKey.ToString().ToUpper() + "'";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            foreach (DataRow item in ds.Rows)
            {
                result.Add(new DeviceTreeNode
                {
                    NodeType = 3,
                    NodeValue = prefix + item["DeviceNo"].ToString(),
                    NodeKey = new Guid(item["id"].ToString()),
                    Longitude = float.Parse(item["Longitude"].ToString()),
                    Latitude = float.Parse(item["Latitude"].ToString()),
                    InstallPlace = item["InstallPlace"].ToString(),
                });
            }
            return result;
        }

        private Boolean UpdateDevicePeople(Guid deviceId, List<MaintenancePeople> devicePeples)
        {
            Boolean result = false;
            string sSql = string.Empty;
            //1)Delete
            sSql = @" Delete MaintenancePeople 
                                Where DeviceID ='" + deviceId.ToString().ToUpper();

            //2)Insert
            SqlParameter para = null;
            List<SqlParameter> sSqlWhere = new List<SqlParameter>();
            try
            {
                foreach (MaintenancePeople item in devicePeples)
                {
                    sSql = @" Insert Into MaintenancePeople(ID,MaintenanceID,DeviceID) Values (@ID,@MaintenanceID,@DeviceID)";
                    para = new SqlParameter();
                    para.DbType = DbType.Guid;
                    para.ParameterName = "@ID";
                    para.Value = item.ID;
                    sSqlWhere.Add(para);

                    //para = new SqlParameter();
                    //para.DbType = DbType.Guid;
                    //para.ParameterName = "@MaintenanceID";
                    //para.Value = item.MaintenancePeopleInfo.ID;
                    //sSqlWhere.Add(para);

                    //para = new SqlParameter();
                    //para.DbType = DbType.Guid;
                    //para.ParameterName = "@DeviceID";
                    //para.Value = item.DeviceID;
                    //sSqlWhere.Add(para);

                    SqlHelper.ExecuteNonQuery(CommandType.Text, sSql.ToString(), sSqlWhere.ToArray());
                    sSqlWhere.Clear();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;

        }

        #endregion

        #region 设备告警

        //        public string GetListDeviceAlarmInfo()
        //        {

        //            List<DeviceAlarm> result = new List<DeviceAlarm>();
        //            string sSql = @" Select Top 100 ID,DeviceID,DeviceNo,EventType,
        //                              EventLevel,StartTime,EndTime,ConfirmTime,
        //                              DealStatus,DealPeople,Comment from DeviceAlarm";
        //            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
        //            DeviceAlarm alarm = null;
        //            foreach (DataRow item in ds.Rows)
        //            {

        //                alarm = new DeviceAlarm();
        //                alarm.ID = new Guid(item["ID"].ToString());
        //                alarm.DeviceID = new Guid(item["DeviceID"].ToString());
        //                alarm.DeviceNo = item["DeviceNo"].ToString();
        //                int? intType = null;
        //                if (item["EventType"] != DBNull.Value)
        //                    intType = Convert.ToInt32(item["EventType"]);
        //                alarm.EventType = intType;

        //                intType = null;
        //                if (item["EventType"] != DBNull.Value)
        //                    intType = Convert.ToInt32(item["EventLevel"]);
        //                alarm.EventLevel = intType;

        //                DateTime? dtValue = null;
        //                if (item["StartTime"] != DBNull.Value)
        //                    dtValue = Convert.ToDateTime(item["StartTime"]);
        //                alarm.StartTime = dtValue;

        //                dtValue = null;
        //                if (item["EndTime"] != DBNull.Value)
        //                    dtValue = Convert.ToDateTime(item["EndTime"]);
        //                alarm.EndTime = dtValue;

        //                dtValue = null;
        //                if (item["ConfirmTime"] != DBNull.Value)
        //                    dtValue = Convert.ToDateTime(item["ConfirmTime"]);
        //                alarm.ConfirmTime = dtValue;

        //                alarm.DealStatus = item["DealStatus"].ToString();
        //                alarm.DealPeople = item["DealPeople"].ToString();
        //                alarm.Comment = item["Comment"].ToString();
        //                result.Add(alarm);

        //            }

        //            return BinaryObjTransfer.JsonSerializer<List<DeviceAlarm>>(result);

        //        }


        public Boolean UpdateDeviceAlarmInfo(Guid AlarmId, DateTime ConfirmTime, String Comment, Guid DealPeopleId)
        {
            string sSql = @" Update DeviceAlarm 
                            Set ConfirmTime='" + ConfirmTime.ToString("yyyy-MM-dd hh:mm:ss") + @"',
                                   DealStatus='已确认', DealPeopleID='" + DealPeopleId + "',Comment='" + Comment + @"'
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

        /*
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
        */

        public string GetUserEventDetailInfo(Guid EventKey)
        {
            string result = string.Empty;
            List<UserEventDealDetail> dealDetails = new List<UserEventDealDetail>();
            string sSql = @" Select ID,EventID,Operator,StepNo,StepName,Memo,DealTime
                                From UserEventDealDetail 
                                Where EventID='" + EventKey.ToString().ToUpper() + "' Order by StepNo";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            if (ds.Rows.Count == 0) { return result; }
            UserEventDealDetail detail;
            foreach (DataRow item in ds.Rows)
            {
                detail = new UserEventDealDetail();
                detail.ID = new Guid(item["ID"].ToString());
                detail.EventID = new Guid(item["EventID"].ToString());
                detail.Operator = new Guid(item["Operator"].ToString());
                Int32 intValue = 0;
                if (item["StepNo"] != DBNull.Value)
                    intValue = Convert.ToInt32(item["StepNo"]);
                detail.StepNo = intValue;
                detail.StepName = item["StepName"].ToString();
                detail.Memo = item["Memo"].ToString();

                DateTime? dtValue = null;
                if (item["DealTime"] != DBNull.Value)
                    dtValue = Convert.ToDateTime(item["DealTime"]);
                detail.DealTime = dtValue;
                dealDetails.Add(detail);

            }
            result = BinaryObjTransfer.JsonSerializer<List<UserEventDealDetail>>(dealDetails);
            return result;
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
                stepInfo.ID = new Guid(item["ID"].ToString());
                stepInfo.StepName = item["StepName"].ToString();
                stepInfos.Add(stepInfo);
            }
            return BinaryObjTransfer.JsonSerializer<List<StepInfo>>(stepInfos);
        }

        public Boolean UpdateEventState(Guid EventID, int state)
        {
            Boolean result = false;
            String sSql = @" Update UserEvent Set State=" + state +
                                " Where ID='" + EventID.ToString().ToUpper() + "'";
            try
            {
                SqlHelper.ExecuteNonQuery(sSql);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        public Boolean ProcessingStepNo(String UserEventDealDetail)
        {

            Boolean result = false;
            if (String.IsNullOrEmpty(UserEventDealDetail)) { return result; }
            UserEventDealDetail eDealDetail = BinaryObjTransfer.JsonDeserialize<UserEventDealDetail>(UserEventDealDetail);
            if (eDealDetail == null) { return result; }
            String sSql = @" Insert Into UserEventDealDetail(ID,EventID,Operator,StepNo,
                                            StepName,Memo,DealTime) 
                            Values (@ID,@EventID,@Operator,@StepNo,@StepName,@Memo,@DealTime)";
            SqlParameter para = null;
            List<SqlParameter> sSqlWhere = new List<SqlParameter>();

            para = new SqlParameter();
            para.DbType = DbType.Guid;
            para.ParameterName = "@ID";
            para.Value = eDealDetail.ID;
            sSqlWhere.Add(para);

            para = new SqlParameter();
            para.DbType = DbType.Guid;
            para.ParameterName = "@EventID";
            para.Value = eDealDetail.EventID;
            sSqlWhere.Add(para);

            para = new SqlParameter();
            para.DbType = DbType.Guid;
            para.ParameterName = "@Operator";
            para.Value = eDealDetail.Operator;
            sSqlWhere.Add(para);

            para = new SqlParameter();
            para.DbType = DbType.Int32;
            para.ParameterName = "@StepNo";
            para.Value = eDealDetail.StepNo;
            sSqlWhere.Add(para);

            para = new SqlParameter();
            para.DbType = DbType.String;
            para.ParameterName = "@StepName";
            para.Value = eDealDetail.StepName;
            sSqlWhere.Add(para);

            para = new SqlParameter();
            para.DbType = DbType.String;
            para.ParameterName = "@Memo";
            para.Value = eDealDetail.Memo;
            sSqlWhere.Add(para);

            para = new SqlParameter();
            para.DbType = DbType.DateTime;
            para.ParameterName = "@DealTime";
            para.Value = eDealDetail.DealTime;
            sSqlWhere.Add(para);

            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, sSql, sSqlWhere.ToArray());
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        #endregion

        #region 图表分析

        //1获取树型设备(combox)
        public string GetDeviceTreeList()
        {
            List<DeviceTreeNode> treeList = new List<DeviceTreeNode>();

            List<DeviceTreeNode> firstNode = this.getTreeNodeChild(null, string.Empty);
            foreach (DeviceTreeNode area in firstNode)
            {
                treeList.Add(new DeviceTreeNode { NodeKey = area.NodeKey, NodeValue = area.NodeValue, NodeType = area.NodeType });
                List<DeviceTreeNode> secondNode = this.getTreeNodeChild(area.NodeKey, "-");
                foreach (DeviceTreeNode manage in secondNode)
                {
                    treeList.Add(new DeviceTreeNode { NodeKey = manage.NodeKey, NodeValue = manage.NodeValue, NodeType = manage.NodeType });
                    List<DeviceTreeNode> thirdNode = getTreeNodeChild(manage.NodeKey, "--");
                    foreach (DeviceTreeNode fourNode in thirdNode)
                    {
                        treeList.Add(new DeviceTreeNode { NodeKey = fourNode.NodeKey, NodeValue = fourNode.NodeValue, NodeType = fourNode.NodeType });
                        List<DeviceTreeNode> fiveNode = getTreeNodeDevice(fourNode.NodeKey, "---");
                        foreach (DeviceTreeNode item in fiveNode)
                        {
                            treeList.Add(new DeviceTreeNode { NodeKey = item.NodeKey, NodeValue = item.NodeValue, NodeType = item.NodeType });
                        }
                    }
                }
            }
            return BinaryObjTransfer.JsonSerializer<List<DeviceTreeNode>>(treeList);
        }

        //2同设备不同时间段获取温度值
        public string GetSameDeviceTemperatureDiffDate(Int32 DeviceType, Guid DeviceID, Int32 DateSelMode, string DateList)
        {
            Dictionary<DateTime, List<ChartSource>> source = new Dictionary<DateTime, List<ChartSource>>();
            DateTime start, end;
            List<DateTime> dates = BinaryObjTransfer.JsonDeserialize<List<DateTime>>(DateList);
            foreach (DateTime item in dates)
            {
                string groupType = string.Empty;
                start = item;
                string where = GetDevicEntityKey(DeviceType, DeviceID);
                end = DateDiffTime(item, DateSelMode, ref groupType);
                source.Add(item, GetDeviceDateTemperature(start, end, groupType, where));
            }
            return BinaryObjTransfer.JsonSerializer<Dictionary<DateTime, List<ChartSource>>>(source);
        }

        private DateTime DateDiffTime(DateTime start, Int32 dateSelMode, ref string groupType)
        {
            DateTime result = DateTime.MinValue;
            if (dateSelMode == 0)
            {
                result = start.AddDays(1);
                groupType = @" case when convert(varchar(16),AAA.UpdateTime,120) like '%[0-4]' 
		                        then convert(varchar(15),AAA.UpdateTime,120)+'0'
		                        else convert(varchar(15),AAA.UpdateTime,120)+'5' end ";
            }
            else if (dateSelMode == 1)
            {
                result = start.AddMonths(1);
                groupType = @" convert(varchar(13), AAA.UpdateTime,120) +':00:00' ";
            }
            else
            {
                result = start.AddYears(1);
                groupType = @" convert(varchar(7), AAA.UpdateTime,120) +'-01:00:00' ";
            }
            return result;
        }

        private string GetDevicEntityKey(Int32 DeviceType, Guid DeviceID)
        {
            string sSql = string.Empty;
            string result = string.Empty;


            if (DeviceType == 0)
            {
                return result;
            }
            else if (DeviceType == 1)
            {
                sSql = @" Select Distinct CCC.ID
                                from DeviceTree AAA 
                                Inner Join DeviceTree BBB On AAA.ID=BBB.ParentID
                                Inner Join DeviceInfo CCC On BBB.ID=CCC.ManageAreaID
                                Where AAA.ID = '" + DeviceID.ToString().ToUpper() + "'";
            }
            else if (DeviceType == 2)
                sSql = " Select Id from DeviceInfo Where ManageAreaID = '" + DeviceID.ToString().ToUpper() + "'";
            else if (DeviceType == 3)
            {
                result = " And AAA.DeviceID ='" + DeviceID.ToString().ToUpper() + "'";
                return result;
            }
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            if (ds == null || ds.Rows.Count == 0) { return result; }
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in ds.Rows)
            {
                sb.Append(",'" + dr["ID"] + "'");
            }
            result = "And AAA.DeviceID In ( " + sb.ToString().Substring(1) + " )";
            return result;
        }

        private List<ChartSource> GetDeviceDateTemperature(DateTime start, DateTime end, string groupType, string whereType)
        {
            List<ChartSource> result = new List<ChartSource>();
            string sSql = @" Select " + groupType + @" As DeviceDate ,Round(Avg(AAA.Temperature),2) As DeviceTemperature
                                from DeviceRealTime AAA  
                                Where 1=1 And AAA.Temperature >0 
                                And AAA.UpdateTime >='" + start.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                                And AAA.UpdateTime <'" + end.ToString("yyyy-MM-dd HH:mm:ss") + "' Group by " + groupType + " Order by DeviceDate";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            if (ds == null || ds.Rows.Count == 0) { return result; }
            foreach (DataRow dr in ds.Rows)
            {
                result.Add(
                    new ChartSource
                    {
                        DeviceDate = Convert.ToDateTime(dr["DeviceDate"]),
                        DeviceTemperature = Convert.ToDouble(dr["DeviceTemperature"])
                    });
            }
            return result;
        }

        //3)同时间不同设备获取温度值
        public string GetSameDateTemperatureDiffDevice(Int32 dataSelMode, DateTime starDate, DateTime endDate, String deviceInfo)
        {
            Dictionary<DeviceTree, List<ChartSource>> source = new Dictionary<DeviceTree, List<ChartSource>>();
            string groupType = string.Empty;
            DateDiffTime(starDate, dataSelMode, ref groupType);
            List<DeviceTree> deviceTrees = BinaryObjTransfer.JsonDeserialize<List<DeviceTree>>(deviceInfo);
            foreach (DeviceTree devTree in deviceTrees)
            {
                string where = GetDevicEntityKey((int)devTree.Level, devTree.ID);
                source.Add(devTree, GetDeviceDateTemperature(starDate, endDate, groupType, where));
            }
            return BinaryObjTransfer.JsonSerializer<Dictionary<DeviceTree, List<ChartSource>>>(source);

        }

        #endregion


    }

}
