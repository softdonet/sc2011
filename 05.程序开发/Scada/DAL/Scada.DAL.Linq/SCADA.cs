using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.DAL.Linq
{
    public partial class SCADADataContext
    {
        public SCADADataContext() :
            base(global::System.Configuration.ConfigurationManager.ConnectionStrings["Center"].ConnectionString, mappingSource)
        {
            OnCreated();
        }
    }
}
