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
    
    public partial class MaterialsFromStock
    {
        public long MaterialsFromStockID { get; set; }
        public decimal Quantity { get; set; }
        public System.DateTime IssuedDate { get; set; }
        public long FromOrderID { get; set; }
        public long ToOrderID { get; set; }
        public long MaterialStockID { get; set; }
    
        public virtual MaterialStock MaterialStock { get; set; }
        public virtual Order Order { get; set; }
        public virtual Order Order1 { get; set; }
    }
}
