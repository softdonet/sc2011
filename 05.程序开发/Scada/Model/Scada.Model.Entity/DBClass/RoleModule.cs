using System;
using System.Collections.Generic;
namespace Scada.Model.Entity
{
    /// <summary>
    /// ��ɫģ��
    /// </summary>
    public class RoleModule
    {
        public Guid ID { get; set; }
        public Guid RoleID { get; set; }
        public Guid ModuleID { get; set; }
    }
}

