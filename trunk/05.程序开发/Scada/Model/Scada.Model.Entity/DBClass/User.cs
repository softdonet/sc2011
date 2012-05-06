using System;
using System.Collections.Generic;


namespace Scada.Model.Entity
{
    /// <summary>
    ///”√ªß±Ì
    /// </summary>
    public partial class User
    {
        public Guid UserID { get; set; }
        public string LoginID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public char Status { get; set; }
    }
}

