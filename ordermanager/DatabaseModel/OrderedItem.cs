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
    
    public partial class OrderedItem
    {
        public OrderedItem()
        {
            this.GRNReciepts = new HashSet<GRNReciept>();
        }
    
        public long OrderedItemID { get; set; }
        public long ProductMaterialItemID { get; set; }
        public long PurchaseOrderID { get; set; }
        public decimal OrderedQuantity { get; set; }
        public Nullable<decimal> ExcessiveQuantity { get; set; }
        public Nullable<decimal> PendingQuantity { get; set; }
        public Nullable<decimal> TaxInINRPerUnit { get; set; }
        public Nullable<decimal> CostPerUnit { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
    
        public virtual ICollection<GRNReciept> GRNReciepts { get; set; }
        public virtual ProductMaterialItem ProductMaterialItem { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}
