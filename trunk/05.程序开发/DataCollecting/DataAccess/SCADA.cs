using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    /// <summary>
    /// 数据库连接串读取
    /// </summary>
    public partial class SCADADataContext
    {
        public SCADADataContext() :
            base(global::System.Configuration.ConfigurationManager.ConnectionStrings["Center"].ConnectionString, mappingSource)
        {
            OnCreated();
        }
    }
}
