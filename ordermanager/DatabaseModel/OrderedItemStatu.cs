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
    
    public partial class OrderedItemStatu
    {
        public OrderedItemStatu()
        {
            this.OrderedItems = new HashSet<OrderedItem>();
        }
    
        public byte OrderedItemStatusID { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<OrderedItem> OrderedItems { get; set; }
    }
}
