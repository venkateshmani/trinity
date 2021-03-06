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
    
    public partial class UnitsOfMeasurement
    {
        public UnitsOfMeasurement()
        {
            this.MaterialStocks = new HashSet<MaterialStock>();
            this.OrderProducts = new HashSet<OrderProduct>();
            this.ProductMaterialItems = new HashSet<ProductMaterialItem>();
            this.ProductMaterials = new HashSet<ProductMaterial>();
            this.SubMaterials = new HashSet<SubMaterial>();
        }
    
        public short UOMID { get; set; }
        public string Units { get; set; }
        public decimal Multiplier { get; set; }
        public Nullable<decimal> QuantityMultiplier { get; set; }
    
        public virtual ICollection<MaterialStock> MaterialStocks { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<ProductMaterialItem> ProductMaterialItems { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<SubMaterial> SubMaterials { get; set; }
    }
}
