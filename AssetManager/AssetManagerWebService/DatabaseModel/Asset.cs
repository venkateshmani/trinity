//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssetManagerWebService.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Asset
    {
        public long AssetID { get; set; }
        public long AssetNameID { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal ValueInINR { get; set; }
        public decimal Quantity { get; set; }
        public Nullable<int> BrandNameID { get; set; }
        public Nullable<int> SupplierID { get; set; }
    
        public virtual AssetName AssetName { get; set; }
        public virtual BrandName BrandName { get; set; }
        public virtual Company Company { get; set; }
    }
}
