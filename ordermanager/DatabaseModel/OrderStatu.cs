//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ordermanager.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderStatu
    {
        public OrderStatu()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public short OrderStatusID { get; set; }
        public string StatusLabel { get; set; }
        public string DisplayLabel { get; set; }
        public Nullable<short> NextUserRoleID { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
