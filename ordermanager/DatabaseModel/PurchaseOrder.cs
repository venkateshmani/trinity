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
    
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            this.ProductMaterialItems = new HashSet<ProductMaterialItem>();
        }
    
        public long PurchaseOrderID { get; set; }
        public int SupplierID { get; set; }
        public Nullable<System.DateTime> PurchaseOrderDate { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public Nullable<byte> PurchaseOrderStatusID { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual ICollection<ProductMaterialItem> ProductMaterialItems { get; set; }
        public virtual PurchaseOrderStatu PurchaseOrderStatu { get; set; }
    }
}
