using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.BLL.Contract
{
    public interface ISystemManagerService
    {

        int AddDD(int x, int y);

        //登录
        Int32 GetLoginResultType(String username, String userpwd);

        /// <summary>
        /// 获取天气预报
        /// </summary>
        /// <param name="cityName">城市名称</param>
        /// <returns></returns>
        string GetWeather(string cityName);
    }
}
