using System;

namespace Scada.DAL.Ado
{

    /// <summary>
    /// 数据库默认连接字符串
    /// </summary>
    public class DefaultDBConnection
    {

        public DefaultDBConnection()
        {

        }

        public static string Default_DataBasic_Connection =
                "Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.1)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl))) ;Persist Security Info=True;User ID=ABIS_APP;Password=ABIS_APP;pooling=false";

    }
}
