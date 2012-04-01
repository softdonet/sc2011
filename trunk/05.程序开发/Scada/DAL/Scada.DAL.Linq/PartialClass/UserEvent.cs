using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.DAL.Linq
{
    public partial class UserEvent
    {
        public string Memo
        {
            get { 
            
                //return this.UserEventDealDetails
                return "报告维护人员==》到达==》";
              }
        }
    }
}
