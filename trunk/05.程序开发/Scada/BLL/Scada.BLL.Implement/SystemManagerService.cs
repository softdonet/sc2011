using System;
using System.Text;
using System.Collections.Generic;
using Scada.BLL.Contract;
using Scada.Utility.Common.Transfer;
using Scada.Model.Entity;
using Scada.BLL.Implement.WeatherWebService;
using Scada.Utility.Common.Helper;




namespace Scada.BLL.Implement
{

    public class SystemManagerService : ISystemManagerService
    {

        #region 测试应用

        public int AddDD(int x, int y)
        {
            return x + y;
        }

        public string ListStudents()
        {
            List<Student> studens = new List<Student>();
            studens.Add(new Student { Name = "张三", Age = 17 });
            studens.Add(new Student { Name = "李四", Age = 19 });
            return BinaryObjTransfer.JsonSerializer<List<Student>>(studens);
        }

        #endregion


        #region 登录信息

        public Int32 GetLoginResultType(String username, String userpwd)
        {
            return 1;

        }

        #endregion

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
    }
}
