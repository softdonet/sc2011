using System;
using System.Collections.Generic;

namespace Scada.Model.Entity
{
    /// <summary>
    ///�û���ɫ
    /// </summary>
    public class UserRole
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
    }
}

