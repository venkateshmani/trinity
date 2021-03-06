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
    
    public partial class ProductBreakUpSummary
    {
        public ProductBreakUpSummary()
        {
            this.Packages = new HashSet<Package>();
            this.ProductCuttings = new HashSet<ProductCutting>();
            this.Productions = new HashSet<Production>();
            this.Qualities = new HashSet<Quality>();
            this.Shipments = new HashSet<Shipment>();
        }
    
        public long ProductBreakUpSummaryID { get; set; }
        public long ProductSizeID { get; set; }
        public int ColorID { get; set; }
        public decimal NumberOfPieces { get; set; }
        public Nullable<long> ProductID { get; set; }
    
        public virtual Color Color { get; set; }
        public virtual OrderProduct OrderProduct { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
        public virtual ProductSize ProductSize { get; set; }
        public virtual ICollection<ProductCutting> ProductCuttings { get; set; }
        public virtual ICollection<Production> Productions { get; set; }
        public virtual ICollection<Quality> Qualities { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
