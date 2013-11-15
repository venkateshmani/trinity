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
        public long OrderedItemID { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public Nullable<System.DateTime> RecievedDate { get; set; }
        public Nullable<decimal> InvoicedQuantity { get; set; }
        public Nullable<decimal> RecievedInHand { get; set; }
        public Nullable<decimal> ExcessiveQuantity { get; set; }
        public Nullable<decimal> OtherChargesInINR { get; set; }
        public Nullable<decimal> QualityPassedQuantity { get; set; }
        public Nullable<decimal> QualityFailedQuantity { get; set; }
        public long ProductMaterialItemID { get; set; }
        public Nullable<decimal> InvoiceValue { get; set; }
        public long PurchaseOrderID { get; set; }
        public decimal OrderedQuantity { get; set; }
        public Nullable<bool> SpawnedNewPurchaseOrder { get; set; }
    
        public virtual ProductMaterialItem ProductMaterialItem { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}