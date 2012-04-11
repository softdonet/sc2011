using System;





namespace Scada.Model.Entity
{

    public class MenuTree
    {
        public Guid MenuId { get; set; }
        public String MeunName { get; set; }
        public Guid? ParentID { get; set; }
        public String Remark { get; set; }
    }


}
