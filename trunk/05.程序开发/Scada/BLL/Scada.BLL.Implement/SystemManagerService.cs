using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using Scada.BLL.Contract;
using Scada.Utility.Common.Transfer;
using Scada.Model.Entity;
using Scada.BLL.Implement.WeatherWebService;
using Scada.Utility.Common.Helper;
using Scada.DAL.Ado;
using System.Data.SqlClient;




namespace Scada.BLL.Implement
{

    public class SystemManagerService : ISystemManagerService
    {

        #region 测试应用

        public int AddDD(int x, int y)
        {
            return x + y;
        }

        #endregion


        #region 登录信息

        public Int32 GetLoginResultType(String username, String userpwd)
        {
            return 1;

        }

        #endregion


        #region 天气预报

        /// <summary>
        /// 获取天气预报
        /// </summary>
        /// <param name="cityName">城市名称</param>
        /// <returns></returns>
        public string GetWeather(string cityName)
        {
            var service = new WeatherWebServiceSoapClient();
            string[] ws = service.getWeatherbyCityName(cityName);
            return BinaryObjTransfer.JsonSerializer<Weather>(WeatherHelper.GetWeather(ws));
        }

        #endregion


        #region 系统参数

        public string GetSystemParameterManage()
        {
            List<SystemParameterManage> result = new List<SystemParameterManage>();

            String sSql = @" Select * from SystemParameterManage";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            if (ds == null || ds.Rows.Count == 0) { return string.Empty; }
            SystemParameterManage sysParMange;
            foreach (DataRow item in ds.Rows)
            {
                sysParMange = new SystemParameterManage();
                sysParMange.ParameterID = new Guid(item["id"].ToString());
                sysParMange.ParameterKey = item["Parameter"].ToString();
                sysParMange.ParameterValue = Convert.ToSingle(item["Value"]);
                result.Add(sysParMange);
            }
            return BinaryObjTransfer.JsonSerializer<List<SystemParameterManage>>(result);
        }

        public string GetSystemGlobalParameter()
        {
            SystemGlobalParameter result = new SystemGlobalParameter();
            String sSql = " Select * from SystemGlobalParamete";
            DataTable ds = SqlHelper.ExecuteDataTable(sSql);
            if (ds == null || ds.Rows.Count == 0) { return string.Empty; }
            foreach (DataRow item in ds.Rows)
            {
                result.ConnectType = Convert.ToInt32(item["ConnectType"]);
                result.ConnectName = item["ConnectName"].ToString();
                result.MainDNS = item["MainDNS"].ToString();
                result.SecondDNS = item["SecondDNS"].ToString();
                result.Domain = item["Domain"].ToString();
                result.Port = Convert.ToInt32(item["Port"]);
            }
            return BinaryObjTransfer.JsonSerializer<SystemGlobalParameter>(result);
        }

        public Boolean UpdateSystemParameterManage(string systemParameterManage)
        {
            Boolean result = false;
            StringBuilder sb = new StringBuilder();
            List<SystemParameterManage> sysParMan =
                BinaryObjTransfer.JsonDeserialize<List<SystemParameterManage>>(systemParameterManage);
            if (sysParMan == null || sysParMan.Count == 0) { return result; }
            foreach (SystemParameterManage item in sysParMan)
            {
                String sSql = String.Format(@" Update SystemParameterManage 
                                                    Set Value ={0} 
                                                    Where ID='{1}' ;",
                                                    item.ParameterValue,
                                                    item.ParameterID.ToString().ToUpper());
                sb.Append(sSql);
            }

            try
            {
                int rowNum = SqlHelper.ExecuteNonQuery(sb.ToString());
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.Message);
            }
            return result;

        }

        public Boolean UpdateSystemGlobalParameter(string systemGlobalParameter)
        {
            Boolean result = false;
            SystemGlobalParameter sysGloPar = BinaryObjTransfer.JsonDeserialize<SystemGlobalParameter>(systemGlobalParameter);
            if (sysGloPar == null) { return result; }
            string sSql = @" Update SystemGlobalParamete Set ConnectType =@ConnectType,
                                        ConnectName=@ConnectName,MainDNS=@MainDNS,
                                        SecondDNS=@SecondDNS,Domain=@Domain,Port=@Port";
            List<SqlParameter> sSqlWhere = new List<SqlParameter>();

            sSqlWhere.Add(new SqlParameter { ParameterName = "@ConnectType", DbType = DbType.Int32, Value = sysGloPar.ConnectType });
            sSqlWhere.Add(new SqlParameter { ParameterName = "@ConnectName", DbType = DbType.String, Value = sysGloPar.ConnectName });
            sSqlWhere.Add(new SqlParameter { ParameterName = "@MainDNS", DbType = DbType.String, Value = sysGloPar.MainDNS });
            sSqlWhere.Add(new SqlParameter { ParameterName = "@SecondDNS", DbType = DbType.String, Value = sysGloPar.SecondDNS });
            sSqlWhere.Add(new SqlParameter { ParameterName = "@Domain", DbType = DbType.String, Value = sysGloPar.Domain });
            sSqlWhere.Add(new SqlParameter { ParameterName = "@Port", DbType = DbType.Int32, Value = sysGloPar.Port });

            try
            {
                int rowNum = SqlHelper.ExecuteNonQuery(CommandType.Text, sSql.ToString(), sSqlWhere.ToArray());
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

    }
}
