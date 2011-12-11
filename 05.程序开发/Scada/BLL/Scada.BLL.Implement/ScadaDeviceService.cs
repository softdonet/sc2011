using System;
using System.Text;
using System.Data;
using System.Collections.Generic;


using Scada.DAL.Ado;
using Scada.BLL.Contract;
using Scada.Model.DB;
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
                sSql.Append(" And AA.ID='" + DeviceID.ToString().ToUpper() + "'");

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

    }

}
