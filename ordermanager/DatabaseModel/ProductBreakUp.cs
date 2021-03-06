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
    
    public partial class ProductBreakUp
    {
        public ProductBreakUp()
        {
            this.OrderProducts = new HashSet<OrderProduct>();
            this.ProductCountryWiseBreakUps = new HashSet<ProductCountryWiseBreakUp>();
        }
    
        public long ProductBreakUpID { get; set; }
        public double Tolerance { get; set; }
        public byte ShipmentModeID { get; set; }
        public string PriceTerms { get; set; }
        public string PaymentTerms { get; set; }
        public string DocumentsRequired { get; set; }
    
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ShipmentMode ShipmentMode { get; set; }
        public virtual ICollection<ProductCountryWiseBreakUp> ProductCountryWiseBreakUps { get; set; }
    }
}
