using System;
using System.Collections.Generic;


namespace Scada.Model.Entity
{
    /// <summary>
    ///”√ªß±Ì
    /// </summary>
    public class User
    {
        public Guid UserID { get; set; }
        public string LoginID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

