using System;
using System.Web.Services;



using Scada.Server.Implement;


namespace Scada.Server.Web
{


    /// <summary>
    /// 1)WebService
    /// </summary>
    [WebService(Namespace = "http://www.aideinfo.com.cn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public int TestAdd(int x, int y)
        {
            ScadaService scada = new ScadaService();
            return scada.Add(x, y);
        }
    }
}
