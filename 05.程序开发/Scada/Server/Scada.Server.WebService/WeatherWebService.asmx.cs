using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WS = Scada.Server.WebService.TheThirdWeatherWebServiceReference;

namespace Scada.Server.WebService
{
    /// <summary>
    /// 天气预报服务
    /// yanghk at 2011-12-4
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WeatherWebService : System.Web.Services.WebService
    {
        /// <summary>
        /// 获取天气预报
        /// </summary>
        /// <param name="cityName">城市名称</param>
        /// <returns></returns>
        [WebMethod]
        public string[] GetWeather(string cityName)
        {   
            var service = new WS.WeatherWebServiceSoapClient();
            return service.getWeatherbyCityName(cityName);
        }
    }
}
