using System;
using System.Collections.Generic;

namespace Scada.Model.Entity
{
    /// <summary>
    ///ÓÃ»§½ÇÉ«
    /// </summary>
    public class UserRole
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
    }
}

