using System;
using System.Text;




using Scada.BLL.Contract;




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

            StringBuilder sSql = new StringBuilder();
            sSql.Append(@" Select * from DeviceRealTime AA 
                            Inner Join DeviceInfo BB Where 1=1 ");
            if (DeviceType == 1)
            {
                string sql = string.Format(@" Select ID from DeviceTree 
                                                    Where ParentID = '{0}'", DeviceID.ToString());

            }
            else if (DeviceType == 2)
            {

            }
            else if (DeviceType == 3)
            {

            }


            return null;
        }


        #endregion

    }

}
