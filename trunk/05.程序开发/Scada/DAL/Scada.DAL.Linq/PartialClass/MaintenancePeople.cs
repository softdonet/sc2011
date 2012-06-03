using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scada.Utility.Common.Helper;


namespace Scada.DAL.Linq
{
    partial class MaintenancePeople
    {
        public string ImageUrl
        {
            get
            {
                return FileServerHelper.GetHeadeImageUrl(this.ImagePath);
            }
        }
    }
}
