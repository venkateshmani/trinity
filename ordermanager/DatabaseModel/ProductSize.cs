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
    
    public partial class ProductSize
    {
        public ProductSize()
        {
            this.ProductBreakUpSummaries = new HashSet<ProductBreakUpSummary>();
            this.ProductCountryWiseBreakUps = new HashSet<ProductCountryWiseBreakUp>();
            this.ProductStocks = new HashSet<ProductStock>();
        }
    
        public long ProductSizeID { get; set; }
        public string Size { get; set; }
    
        public virtual ICollection<ProductBreakUpSummary> ProductBreakUpSummaries { get; set; }
        public virtual ICollection<ProductCountryWiseBreakUp> ProductCountryWiseBreakUps { get; set; }
        public virtual ICollection<ProductStock> ProductStocks { get; set; }
    }
}
