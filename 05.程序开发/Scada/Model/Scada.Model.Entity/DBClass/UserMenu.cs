using System;



namespace Scada.Model.Entity
{
    public class UserMenu
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MenuId { get; set; }
        public Int32 Level { get; set; }
        public String Remark { get; set; }

    }

}
