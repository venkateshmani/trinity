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
    
    public partial class MaterialInStockHistory
    {
        public long MaterialInStockHistoryID { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public decimal Quantity { get; set; }
        public long MaterialStockID { get; set; }
        public decimal TotalMaterialStockQuantity { get; set; }
    
        public virtual MaterialStock MaterialStock { get; set; }
    }
}