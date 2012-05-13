using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scada.Model.Entity
{
   public partial class User
    {
       public string StatusName { get; set; }
       public string UserInputOldPassword { get; set; }
       public string OldPassword { get; set; }
       public string NewPassword { get; set; }
       public string SurePassword { get; set; }
    }
}
