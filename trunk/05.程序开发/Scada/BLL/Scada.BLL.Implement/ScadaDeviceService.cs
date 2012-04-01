using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;


using Scada.DAL.Ado;
using Scada.BLL.Contract;
using Scada.Model.Entity;
using Scada.Utility.Common.Transfer;
using Scada.Utility.Common.Extensions;
using System.Linq;


namespace Scada.BLL.Implement
{

    public class ScadaDeviceService : IScadaDeviceService
    {
        ScadaDeviceServiceLinq scadaDeviceServiceLinq;
        private Scada.DAL.Linq.SCADADataContext sCADADataContext = null;


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
                sSql.Append(" And AA.UpdateTime >='" + Convert.ToDateTime(StartDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            if (EndDate != null)
                sSql.Append(" And AA.UpdateTime <'" + Convert.ToDateTime(EndDate).ToString("yyyy-MM-dd HH:mm:ss") + "'");
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

        //设备当天温度
        public string GetDeviceOnlyDay(String DeviceID)
        {
            List<ChartSource> result = new List<ChartSource>();
            //DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            DateTime start = new DateTime(2012, 3, 24, 0, 0, 0);
            DateTime end = start.AddDays(1);

            StringBuilder sSql = new StringBuilder();
            sSql.Append(@" Select t.UpdateTime,t.Temperature1 from DeviceRealTime t
                                Where t.DeviceID='" + DeviceID.ToUpper() + "'");
            sSql.Append(@" And t.UpdateTime >='" + Convert.ToDateTime(start).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            sSql.Append(@" And t.UpdateTime <'" + Convert.ToDateTime(end).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            sSql.Append(@" Order by t.UpdateTime");

            DataTable dt = SqlHelper.ExecuteDataTable(sSql.ToString());
            if (dt == null || dt.Rows.Count == 0) { return String.Empty; }
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(
                    new ChartSource
                    {
                        DeviceDate = Convert.ToDateTime(dr["UpdateTime"]),
                        DeviceTemperature = Convert.ToDouble(dr["Temperature1"])
                    }
                );
            }
            return BinaryObjTransfer.JsonSerializer<List<ChartSource>>(result);
        }

        //设备历史温度
        public string GetDeviceOnlyMonth(String DeviceID)
        {
            List<ChartSource> result = new List<ChartSource>();
            DateTime start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0).AddDays(1);

            StringBuilder sSql = new StringBuilder();
            sSql.Append(@" Select Convert(varchar(10), t.UpdateTime,120) +' 00:00:00' As UpdateDate,
                                Avg(t.Temperature1) As Temperature from DeviceRealTime t
                                Where t.DeviceID='" + DeviceID.ToUpper() + "'");
            sSql.Append(@" And t.UpdateTime >='" + Convert.ToDateTime(start).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            sSql.Append(@" And t.UpdateTime <'" + Convert.ToDateTime(end).ToString("yyyy-MM-dd HH:mm:ss") + "'");
            sSql.Append(@" Group by Convert(varchar(10), t.UpdateTime,120) +' 00:00:00'");
            sSql.Append(@" Order by UpdateDate");

            DataTable dt = SqlHelper.ExecuteDataTable(sSql.ToString());
            if (dt == null || dt.Rows.Count == 0) { return String.Empty; }
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(
                    new ChartSource
                    {
                        DeviceDate = Convert.ToDateTime(dr["UpdateDate"]),
                        DeviceTemperature = Convert.ToDouble(dr["Temperature"])
                    }
                );
            }
            return BinaryObjTransfer.JsonSerializer<List<ChartSource>>(result);
        }

        #endregion

        #region 设备管理

        public Boolean AddDeviceInfo(string deviceInfo)
        {
            scadaDeviceServiceLinq = new ScadaDeviceServiceLinq();
            try
            {
                scadaDeviceServiceLinq.AddDeviceInfo(deviceInfo);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


            #region Old

            Int32 rowNum = 0;
            Boolean result = false;
            //  SqlParameter para = null;

            StringBuilder sSql = new StringBuilder();
            List<SqlParameter> sSqlWhere = new List<SqlParameter>();
            try
            {
                DeviceInfo deviceValue = BinaryObjTransfer.JsonDeserialize<DeviceInfo>(deviceInfo);
                sSql.Append(@" Insert Into DeviceInfo
                               (ID, DeviceNo, DeviceSN, HardType, ProductDate, DeviceMAC, SIMNo, 
                               ManageAreaID, MaintenancePeopleID, InstallPlace, Longitude, Latitude, High, Comment, Windage,
                               HardwareVersion, SoftWareVersion, LCDScreenDisplayType, UrgencyBtnEnable, InforBtnEnable,
                               Temperature1AlarmValid, Temperature1HighAlarm, Temperature1LowAlarm,
                               Temperature2AlarmValid, Temperature2HighAlarm, Temperature2LowAlarm, 
                               HumidityAlarmValid, HumidityHighAlarm, HumidityLowAlarm,
                               SignalAlarmValid, SignalHighAlarm, SignalLowAlarm, 
                               ElectricityAlarmValid, ElectricityHighAlarm, ElectricityLowAlarm, 
                               CurrentModel, RealTimeParam, FullTimeParam1, FullTimeParam2, 
                               OptimizeParam1, OptimizeParam2, OptimizeParam3) 
                        Values (@ID,@DeviceNo,@DeviceSN,@HardType,@ProductDate,@DeviceMAC,@SIMNo,
                               @ManageAreaID,@MaintenancePeopleID,@InstallPlace,@Longitude,@Latitude,@High,@Comment,@Windage,
                               @HardwareVersion,@SoftWareVersion,@LCDScreenDisplayType,@UrgencyBtnEnable,@InforBtnEnable,
                               @Temperature1AlarmValid,@Temperature1HighAlarm,@Temperature1LowAlarm,
                               @Temperature2AlarmValid,@Temperature2HighAlarm,@Temperature2LowAlarm,
                               @HumidityAlarmValid,@HumidityHighAlarm,@HumidityLowAlarm,
                               @SignalAlarmValid,@SignalHighAlarm,@SignalLowAlarm,
                               @ElectricityAlarmValid,@ElectricityHighAlarm,@ElectricityLowAlarm,
                               @CurrentModel,@RealTimeParam,@FullTimeParam1,@FullTimeParam2,
                               @OptimizeParam1,@OptimizeParam2,@OptimizeParam3)");
            #endregion

                #region 对应的参数

                sSqlWhere.Add(new SqlParameter() { ParameterName = "@ID", DbType = DbType.Guid, Value = deviceValue.ID });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@DeviceNo", DbType = DbType.String, Value = deviceValue.DeviceNo });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@DeviceSN", DbType = DbType.String, Value = deviceValue.DeviceSN });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@HardType", DbType = DbType.String, Value = deviceValue.HardType });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@ProductDate", DbType = DbType.Date, Value = deviceValue.ProductDate });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@DeviceMAC", DbType = DbType.String, Value = deviceValue.DeviceMAC });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@SIMNo", DbType = DbType.String, Value = deviceValue.SIMNo });

                sSqlWhere.Add(new SqlParameter() { ParameterName = "@ManageAreaID", DbType = DbType.Guid, Value = deviceValue.ManageAreaID });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@MaintenancePeopleID", DbType = DbType.Guid, Value = deviceValue.MaintenancePeopleID });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@InstallPlace", DbType = DbType.String, Value = deviceValue.InstallPlace });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Longitude", DbType = DbType.Decimal, Size = 20, Value = deviceValue.Longitude });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Latitude", DbType = DbType.Decimal, Size = 20, Value = deviceValue.Latitude });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@High", DbType = DbType.Decimal, Size = 20, Value = deviceValue.High });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Comment", DbType = DbType.String, Value = deviceValue.Comment });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Windage", DbType = DbType.Int32, Value = deviceValue.Windage });

                sSqlWhere.Add(new SqlParameter() { ParameterName = "@HardwareVersion", DbType = DbType.String, Value = deviceValue.HardwareVersion });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@SoftWareVersion", DbType = DbType.String, Value = deviceValue.SoftWareVersion });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@LCDScreenDisplayType", DbType = DbType.Int32, Value = deviceValue.LCDScreenDisplayType });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@UrgencyBtnEnable", DbType = DbType.Boolean, Value = deviceValue.UrgencyBtnEnable });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@InforBtnEnable", DbType = DbType.Boolean, Value = deviceValue.InforBtnEnable });
                //主温度
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Temperature1AlarmValid", DbType = DbType.Boolean, Value = deviceValue.Temperature1AlarmValid });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Temperature1HighAlarm", DbType = DbType.Decimal, Size = 5, Value = deviceValue.Temperature1HighAlarm });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Temperature1LowAlarm", DbType = DbType.Decimal, Size = 5, Value = deviceValue.Temperature1LowAlarm });
                //从温度
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Temperature2AlarmValid", DbType = DbType.Boolean, Value = deviceValue.Temperature2AlarmValid });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Temperature2HighAlarm", DbType = DbType.Decimal, Size = 5, Value = deviceValue.Temperature2HighAlarm });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Temperature2LowAlarm", DbType = DbType.Decimal, Size = 5, Value = deviceValue.Temperature2LowAlarm });
                //湿度
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@HumidityAlarmValid", DbType = DbType.Boolean, Value = deviceValue.HumidityAlarmValid });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@HumidityHighAlarm", DbType = DbType.Decimal, Size = 2, Value = deviceValue.HumidityHighAlarm });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@HumidityLowAlarm", DbType = DbType.Decimal, Size = 2, Value = deviceValue.HumidityLowAlarm });
                //信号
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@SignalAlarmValid", DbType = DbType.Boolean, Value = deviceValue.SignalAlarmValid });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@SignalHighAlarm", DbType = DbType.Int32, Value = deviceValue.SignalHighAlarm });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@SignalLowAlarm", DbType = DbType.Int32, Value = deviceValue.SignalLowAlarm });
                //电量
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@ElectricityAlarmValid", DbType = DbType.Boolean, Value = deviceValue.ElectricityAlarmValid });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@ElectricityHighAlarm", DbType = DbType.Int32, Value = deviceValue.ElectricityHighAlarm });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@ElectricityLowAlarm", DbType = DbType.Int32, Value = deviceValue.ElectricityLowAlarm });

                sSqlWhere.Add(new SqlParameter() { ParameterName = "@CurrentModel", DbType = DbType.Int32, Value = deviceValue.CurrentModel });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@RealTimeParam", DbType = DbType.Int32, Value = deviceValue.RealTimeParam });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@FullTimeParam1", DbType = DbType.Int32, Value = deviceValue.FullTimeParam1 });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@FullTimeParam2", DbType = DbType.Int32, Value = deviceValue.FullTimeParam2 });

                sSqlWhere.Add(new SqlParameter() { ParameterName = "@OptimizeParam1", DbType = DbType.Int32, Value = deviceValue.OptimizeParam1 });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@OptimizeParam2", DbType = DbType.Int32, Value = deviceValue.OptimizeParam2 });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@OptimizeParam3", DbType = DbType.Int32, Value = deviceValue.OptimizeParam3 });

                #endregion

                #region Old
                //para = new SqlParameter();
                //para.DbType = DbType.Guid;
                //para.ParameterName = "@ID";
                //para.Value = deviceValue.ID;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.String;
                //para.ParameterName = "@DeviceNo";
                //para.Value = deviceValue.DeviceNo;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.String;
                //para.ParameterName = "@HardType";
                //para.Value = deviceValue.HardType;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.DateTime;
                //para.ParameterName = "@ProductDate";
                //para.Value = deviceValue.ProductDate;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.String;
                //para.ParameterName = "@DeviceMAC";
                //para.Value = deviceValue.DeviceMAC;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.String;
                //para.ParameterName = "@SIMNo";
                //para.Value = deviceValue.SIMNo;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.Guid;
                //para.ParameterName = "@ManageAreaID";
                //para.Value = deviceValue.ManageAreaID;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.String;
                //para.ParameterName = "@InstallPlace";
                //para.Value = deviceValue.InstallPlace;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.Decimal;
                //para.ParameterName = "@Longitude";
                //para.Value = deviceValue.Longitude;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.Decimal;
                //para.ParameterName = "@Dimensionality";
                ////para.Value = deviceValue.Dimensionality;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.Decimal;
                //para.ParameterName = "@High";
                //para.Value = deviceValue.High;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.String;
                //para.ParameterName = "@Comment";
                //para.Value = deviceValue.Comment;
                //sSqlWhere.Add(para);

                //para = new SqlParameter();
                //para.DbType = DbType.String;
                //para.ParameterName = "@ConnectPoint";
                ////para.Value = deviceValue.ConnectPoint;
                // sSqlWhere.Add(para);
                #endregion


                //rowNum = SqlHelper.ExecuteNonQuery(CommandType.Text, sSql.ToString(), sSqlWhere.ToArray());
                ////设备维护人员
                ////UpdateDevicePeople(deviceValue.ID, deviceValue.DeviceMainValue);
                //result = true;
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
            scadaDeviceServiceLinq = new ScadaDeviceServiceLinq();
            try
            {
                scadaDeviceServiceLinq.Update(deviceInfo);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            //--------------------------
            #region Old

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
                para.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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

            #endregion
        }

        public Boolean DeleteDeviceInfo(Guid deviceGuid)
        {
            scadaDeviceServiceLinq = new ScadaDeviceServiceLinq();
            try
            {
                scadaDeviceServiceLinq.Del(deviceGuid);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                Console.WriteLine(ex.Message);
            }

            #region Old

            //--------------------------------------------------
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
            #endregion


        }

        public string ViewDeviceInfo(Guid deviceGuid)
        {
            string result = string.Empty;
            scadaDeviceServiceLinq = new ScadaDeviceServiceLinq();
            try
            {
                result = scadaDeviceServiceLinq.Get(deviceGuid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;

            //-------------------------------
            #region Old

            //string result = string.Empty;
            string sSql = " select * from DeviceInfo where id ='" + deviceGuid + "'";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            if (ds == null || ds.Rows.Count == 0) { return result; }
            DeviceInfo deviceInfo = new DeviceInfo();
            foreach (DataRow item in ds.Rows)
            {

                //标识
                deviceInfo.ID = new Guid(item["id"].ToString());
                //设备编号
                deviceInfo.DeviceNo = item["DeviceNo"].ToString();
                //设备MAC
                deviceInfo.DeviceMAC = item["devicemac"].ToString();
                //SIM卡号
                deviceInfo.SIMNo = item["simno"].ToString();
                //硬件类型
                deviceInfo.HardType = item["HardType"].ToString();
                //生产日期
                if (item["ProductDate"] != DBNull.Value)
                {
                    deviceInfo.ProductDate = Convert.ToDateTime(item["ProductDate"].ToString());
                }

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

                deviceInfo.Comment = item["Comment"].ToString();
                //偏差
                Int32? intValue = null;
                if (item["windage"] != DBNull.Value)
                    decValue = Convert.ToDecimal(item["windage"]);
                deviceInfo.Windage = intValue;

                //硬件版本
                deviceInfo.HardwareVersion = item["HardwareVersion"].ToString();
                deviceInfo.SoftWareVersion = item["SoftWareVersion"].ToString();
                if (item["UrgencyBtnEnable"] is bool)
                {
                    deviceInfo.UrgencyBtnEnable = (bool)item["UrgencyBtnEnable"];

                }
                //是否启用紧急按钮
                if (item["UrgencyBtnEnable"] is bool)
                {
                    deviceInfo.UrgencyBtnEnable = Convert.ToBoolean(item["UrgencyBtnEnable"]);
                }

                ////////////////////////报警设置////////////////////////////
                //主温度报警设置有效
                if (item["Temperature1AlarmValid"] is Boolean)
                {
                    deviceInfo.Temperature1AlarmValid = (bool)item["Temperature1AlarmValid"];
                }
                if (item["Temperature1HighAlarm"] != DBNull.Value)
                {
                    deviceInfo.Temperature1HighAlarm = Convert.ToDecimal(item["Temperature1HighAlarm"]);
                }
                if (item["Temperature1LowAlarm"] is bool)
                {
                    deviceInfo.Temperature1LowAlarm = Convert.ToDecimal(item["Temperature1LowAlarm"]);

                }
                //从温度报警设置有效
                if (item["Temperature2AlarmValid"] is bool)
                {
                    deviceInfo.Temperature2AlarmValid = Convert.ToBoolean(item["Temperature2AlarmValid"]);
                }
                if (item["Temperature2HighAlarm"] != DBNull.Value)
                {
                    deviceInfo.Temperature2HighAlarm = Convert.ToDecimal(item["Temperature2HighAlarm"]);
                }
                if (item["Temperature2LowAlarm"] != DBNull.Value)
                {
                    deviceInfo.Temperature2LowAlarm = Convert.ToDecimal(item["Temperature2LowAlarm"]);
                }

                //湿度报警设置有效
                if (item["HumidityAlarmValid"] is bool)
                {
                    deviceInfo.HumidityAlarmValid = (bool)item["HumidityAlarmValid"];
                }
                if (item["HumidityHighAlarm"] != DBNull.Value)
                {
                    deviceInfo.HumidityHighAlarm = Convert.ToDecimal(item["HumidityHighAlarm"]);
                }
                if (item["HumidityLowAlarm"] != DBNull.Value)
                {
                    deviceInfo.HumidityLowAlarm = Convert.ToDecimal(item["HumidityLowAlarm"]);
                }
                //信号报警设置有效
                if (item["SignalAlarmValid"] is bool)
                {
                    deviceInfo.SignalAlarmValid = (bool)item["SignalAlarmValid"];
                }
                if (item["SignalHighAlarm"] != DBNull.Value)
                {
                    deviceInfo.SignalHighAlarm = Convert.ToInt32(item["SignalHighAlarm"]);
                }
                if (item["SignalLowAlarm"] is bool)
                {
                    deviceInfo.SignalLowAlarm = Convert.ToInt32(item["SignalLowAlarm"]);
                }

                //电量报警设置有效
                if (item["ElectricityAlarmValid"] is bool)
                {
                    deviceInfo.ElectricityAlarmValid = (bool)item["ElectricityAlarmValid"];
                }
                if (item["ElectricityHighAlarm"] != DBNull.Value)
                {
                    deviceInfo.ElectricityHighAlarm = Convert.ToInt32(item["ElectricityHighAlarm"]);
                }
                if (item["ElectricityLowAlarm"] != DBNull.Value)
                {
                    deviceInfo.ElectricityLowAlarm = Convert.ToInt32(item["ElectricityLowAlarm"]);
                }

                ////////////////////////辅助信息////////////////////////////
                //液晶屏显示类型
                if (item["LCDScreenDisplayType"] != DBNull.Value)
                {
                    deviceInfo.LCDScreenDisplayType = Convert.ToInt32(item["LCDScreenDisplayType"]);
                }

                //当前选择模式
                if (item["CurrentModel"] != DBNull.Value)
                {
                    deviceInfo.CurrentModel = Convert.ToInt32(item["CurrentModel"]);
                }

                //实时模式参数
                if (item["RealTimeParam"] != DBNull.Value)
                {
                    deviceInfo.RealTimeParam = Convert.ToInt32(item["RealTimeParam"]);
                }
                //整点模式参数1,2
                if (item["FullTimeParam1"] != DBNull.Value)
                {
                    deviceInfo.FullTimeParam1 = Convert.ToInt32(item["FullTimeParam1"]);
                }
                if (item["FullTimeParam2"] != DBNull.Value)
                {
                    deviceInfo.FullTimeParam2 = Convert.ToInt32(item["FullTimeParam2"]);
                }
                //逢变则报参数1,2,3
                if (item["OptimizeParam1"] != DBNull.Value)
                {
                    deviceInfo.OptimizeParam1 = Convert.ToInt32(item["OptimizeParam1"]);
                }
                if (item["OptimizeParam2"] != DBNull.Value)
                {
                    deviceInfo.OptimizeParam2 = Convert.ToInt32(item["OptimizeParam2"]);
                }
                if (item["OptimizeParam1"] != DBNull.Value)
                {
                    deviceInfo.OptimizeParam3 = Convert.ToInt32(item["OptimizeParam3"]);
                }
                //列出设备的维护人列表
                //deviceInfo.DeviceMainValue = ListDeviceMaintenancePeople(deviceInfo.ID);


            }
            result = BinaryObjTransfer.JsonSerializer<DeviceInfo>(deviceInfo);
            return result;

            #endregion

        }

        public string ListMaintenancePeople()
        {
            string result = string.Empty;
            List<MaintenancePeople> mainPeople = new List<MaintenancePeople>();
            mainPeople = ListMaintenancePeopleInfo();
            result = BinaryObjTransfer.JsonSerializer<List<MaintenancePeople>>(mainPeople);
            return result;


        }

        //获取维修人员列表
        private List<MaintenancePeople> ListMaintenancePeopleInfo()
        {
            List<MaintenancePeople> result = new List<MaintenancePeople>();
            string sSql = @" Select ID, Name, Address, Telephone, Mobile, QQ, MSN, Email, HeadImage
                             from MaintenancePeople";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            if (ds == null || ds.Rows.Count == 0) { return result; }
            MaintenancePeople peoples = null;
            foreach (DataRow item in ds.Rows)
            {
                peoples = new MaintenancePeople();
                peoples.ID = new Guid(item["ID"].ToString());
                peoples.Name = item["Name"].ToString();
                peoples.Address = item["Address"].ToString();
                peoples.Telephone = item["Telephone"].ToString();
                peoples.Mobile = item["Mobile"].ToString();
                peoples.QQ = item["QQ"].ToString();
                peoples.MSN = item["MSN"].ToString();
                peoples.Email = item["Email"].ToString();
                //byte[] getByte = item["HeadImage"];
                //peoples.HeadImage = Convert.ToByte(item["HeadImage"]);
                result.Add(peoples);
            }
            return result;
        }

        private List<MaintenancePeople> ListDeviceMaintenancePeople1(Guid deviceID)
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

        public string ListDeviceMaintenancePeople(Guid deviceID)
        {
            string result = string.Empty;
            List<MaintenancePeople> maintenancePeople = new List<MaintenancePeople>();
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
                maintenancePeople.Add(people);
            }
            return BinaryObjTransfer.JsonSerializer<List<MaintenancePeople>>(maintenancePeople);

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
                        //man.NodeParent = manage;
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
                            Set ConfirmTime='" + ConfirmTime.ToString("yyyy-MM-dd HH:mm:ss") + @"',
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

        #region 告警查询

        public string GetAlarmQueryInfo(Guid id, int DeviceType, DateTime startdDate, DateTime endDate)
        {
            string result = string.Empty;
            object obj = new Object();
            try
            {
                sCADADataContext = new Scada.DAL.Linq.SCADADataContext();

                if (DeviceType == 0)
                {

                }
                else if (DeviceType == 1)//管理分区
                {
                    var obj1 = from dt in sCADADataContext.DeviceTrees
                               from di in sCADADataContext.DeviceInfos
                               from da in sCADADataContext.DeviceAlarms
                               where dt.ParentID == id
                               where dt.ID == di.ManageAreaID
                               where da.DeviceID == di.ID
                               where da.StartTime > startdDate
                               where da.StartTime < endDate
                               select da; // select new {da.DeviceID, };


                    if (obj1 != null)
                    {
                        obj = obj1;
                        //Scada.Model.Entity.DeviceAlarm deviceAlarm = (obj1.ConvertTo<Scada.Model.Entity.DeviceAlarm>());
                        //result = BinaryObjTransfer.JsonSerializer<Scada.Model.Entity.DeviceAlarm>(deviceAlarm);
                    }
                }
                else if (DeviceType == 2)//组
                {
                    var obj2 = from di in sCADADataContext.DeviceInfos
                               from da in sCADADataContext.DeviceAlarms
                               where di.ManageAreaID == id
                               where da.DeviceID == di.ID
                               where da.StartTime > startdDate
                               where da.StartTime < endDate
                               select da;
                    if (obj2 != null)
                    {
                        obj = obj2;
                    }
                }
                else // (DeviceType==3)
                {
                    //var obj3 = sCADADataContext.DeviceAlarms.Select(e => e.DeviceID == id && e.StartTime > startdDate && e.StartTime < endDate);
                    var obj3 = from di in sCADADataContext.DeviceAlarms
                               where di.DeviceID == id
                               where di.StartTime >= startdDate
                               where di.StartTime < endDate
                               select di;

                    if (obj3 != null)
                    {
                        obj = obj3;
                    }
                }
                if (obj != null)
                {
                    Scada.Model.Entity.DeviceAlarm deviceAlarm = obj.ConvertTo<Scada.Model.Entity.DeviceAlarm>();
                    result = BinaryObjTransfer.JsonSerializer<Scada.Model.Entity.DeviceAlarm>(deviceAlarm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        #endregion

        #region 用户事件查询
        public string GetUserEventQueryInfo(Guid id, DateTime startDate, DateTime endDate)
        {
            string result = string.Empty;
            try
            {
                sCADADataContext = new Scada.DAL.Linq.SCADADataContext();
                var obj = sCADADataContext.UserEvents.Select(e => e.ID == id && e.RequestTime > startDate && e.RequestTime < endDate);
                if (obj != null)
                {
                    Scada.Model.Entity.UserEventModel userEventModel = obj.ConvertTo<Scada.Model.Entity.UserEventModel>();
                    result = BinaryObjTransfer.JsonSerializer<Scada.Model.Entity.UserEventModel>(userEventModel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                end = DateDiffTime(ref start, DateSelMode, ref groupType);
                source.Add(item, GetDeviceDateTemperature(start, end, groupType, where));
            }
            return BinaryObjTransfer.JsonSerializer<Dictionary<DateTime, List<ChartSource>>>(source);
        }

        private DateTime DateDiffTime(ref DateTime start, Int32 dateSelMode, ref string groupType)
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
                start = new DateTime(start.Year, start.Month, 1, 0, 0, 0);
                result = start.AddMonths(1);
                groupType = @" convert(varchar(10), AAA.UpdateTime,120) +' 00:00:00' ";
            }
            else
            {
                start = new DateTime(start.Year, 1, 1, 0, 0, 0);
                result = start.AddYears(1);
                groupType = @" convert(varchar(7), AAA.UpdateTime,120) +'-01 00:00:00' ";
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
            string sSql = @" Select " + groupType + @" As DeviceDate ,Round(Avg(AAA.Temperature1),2) As DeviceTemperature
                                from DeviceRealTime AAA  
                                Where 1=1 And AAA.Temperature1 >0 " + whereType + @"
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
            DateDiffTime(ref starDate, dataSelMode, ref groupType);
            List<DeviceTree> deviceTrees = BinaryObjTransfer.JsonDeserialize<List<DeviceTree>>(deviceInfo);
            foreach (DeviceTree devTree in deviceTrees)
            {
                string where = GetDevicEntityKey((int)devTree.Level, devTree.ID);
                source.Add(devTree, GetDeviceDateTemperature(starDate, endDate, groupType, where));
            }
            return BinaryObjTransfer.JsonSerializer<Dictionary<DeviceTree, List<ChartSource>>>(source);

        }

        #endregion

        #region 维护人员

        public string GetMaintenancePeople()
        {
            List<MaintenancePeople> mainPeople = new List<MaintenancePeople>();
            string sSql = " Select * from MaintenancePeople ";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            MaintenancePeople people = null;
            foreach (DataRow item in ds.Rows)
            {
                people = new MaintenancePeople();
                people.ID = new Guid(item["ID"].ToString());
                people.Name = item["Name"].ToString();
                people.Address = item["Address"].ToString();
                people.Telephone = item["Telephone"].ToString();
                people.Mobile = item["Mobile"].ToString();
                people.QQ = item["QQ"].ToString();
                people.MSN = item["MSN"].ToString();
                people.Email = item["Email"].ToString();
                if (item["HeadImage"] != DBNull.Value)
                    people.HeadImage = (byte[])item["HeadImage"];
                mainPeople.Add(people);
            }
            return BinaryObjTransfer.JsonSerializer<List<MaintenancePeople>>(mainPeople);
        }
        /// <summary>
        /// 传人维护人员ID，获取维护人员详细信息
        /// </summary>
        /// <param name="MainPeopleId"></param>
        /// <returns></returns>
        public string MPeopleById(Guid MainPeopleId)
        {
            List<MaintenancePeople> mainPeople = new List<MaintenancePeople>();
            string sSql = " Select * from MaintenancePeople where ID = " + "'" + MainPeopleId.ToString() + "'";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            MaintenancePeople people = null;
            foreach (DataRow item in ds.Rows)
            {
                people = new MaintenancePeople();
                people.ID = new Guid(item["ID"].ToString());
                people.Name = item["Name"].ToString();
                people.Address = item["Address"].ToString();
                people.Telephone = item["Telephone"].ToString();
                people.Mobile = item["Mobile"].ToString();
                people.QQ = item["QQ"].ToString();
                people.MSN = item["MSN"].ToString();
                people.Email = item["Email"].ToString();
                if (item["HeadImage"] != DBNull.Value)
                    people.HeadImage = (byte[])item["HeadImage"];
                mainPeople.Add(people);
            }
            return BinaryObjTransfer.JsonSerializer<List<MaintenancePeople>>(mainPeople);
        }

        public Boolean AddMaintenancePeople(string people)
        {
            Boolean result = false;
            string sSql = @" Insert Into MaintenancePeople(ID,Name,Address,Telephone,
                                    Mobile,QQ,MSN,Email,HeadImage) Values (@ID,@Name,@Address,@Telephone,
                                    @Mobile,@QQ,@MSN,@Email,@HeadImage)";
            List<SqlParameter> sSqlWhere = new List<SqlParameter>();
            try
            {
                MaintenancePeople mainPeople = BinaryObjTransfer.JsonDeserialize<MaintenancePeople>(people);
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@ID", DbType = DbType.Guid, Value = mainPeople.ID });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Name", DbType = DbType.String, Value = mainPeople.Name });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Address", DbType = DbType.String, Value = mainPeople.Address });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Telephone", DbType = DbType.String, Value = mainPeople.Telephone });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Mobile", DbType = DbType.String, Value = mainPeople.Mobile });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@QQ", DbType = DbType.String, Value = mainPeople.QQ });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@MSN", DbType = DbType.String, Value = mainPeople.MSN });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Email", DbType = DbType.String, Value = mainPeople.Email });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@HeadImage", DbType = DbType.Binary, Value = mainPeople.HeadImage });
                int rowNum = SqlHelper.ExecuteNonQuery(CommandType.Text, sSql, sSqlWhere.ToArray());
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        public Boolean UpdateMaintenancePeople(string people)
        {
            Boolean result = false;
            string sSql = @" Update MaintenancePeople Set Name=@Name,Address=@Address,Telephone=@Telephone,
                                    Mobile=@Mobile,QQ=@QQ,MSN=@MSN,Email=@Email,HeadImage=@HeadImage Where ID=@ID";
            List<SqlParameter> sSqlWhere = new List<SqlParameter>();
            try
            {
                MaintenancePeople mainPeople = BinaryObjTransfer.JsonDeserialize<MaintenancePeople>(people);
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Name", DbType = DbType.String, Value = mainPeople.Name });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Address", DbType = DbType.String, Value = mainPeople.Address });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Telephone", DbType = DbType.String, Value = mainPeople.Telephone });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Mobile", DbType = DbType.String, Value = mainPeople.Mobile });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@QQ", DbType = DbType.String, Value = mainPeople.QQ });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@MSN", DbType = DbType.String, Value = mainPeople.MSN });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@Email", DbType = DbType.String, Value = mainPeople.Email });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@HeadImage", DbType = DbType.Binary, Value = mainPeople.HeadImage });
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@ID", DbType = DbType.Guid, Value = mainPeople.ID });
                int rowNum = SqlHelper.ExecuteNonQuery(CommandType.Text, sSql, sSqlWhere.ToArray());
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        public Boolean DeleteMaintenancePeople(Guid peopleKey)
        {
            Boolean result = false;
            string sSql = @" Delete MaintenancePeople Where ID=@ID";
            List<SqlParameter> sSqlWhere = new List<SqlParameter>();
            try
            {
                sSqlWhere.Add(new SqlParameter() { ParameterName = "@ID", DbType = DbType.Guid, Value = peopleKey });
                int rowNum = SqlHelper.ExecuteNonQuery(CommandType.Text, sSql, sSqlWhere.ToArray());
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        private byte[] StringToByte(string strValue)
        {
            return System.Text.Encoding.Default.GetBytes(strValue);
        }

        private string ByteToString(byte[] bytes)
        {
            return System.Text.Encoding.Default.GetString(bytes);
        }

        #endregion

    }

}
