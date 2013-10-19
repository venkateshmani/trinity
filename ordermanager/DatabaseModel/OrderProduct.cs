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
    
    public partial class OrderProduct
    {
        public OrderProduct()
        {
            this.ProductExtraCosts = new HashSet<ProductExtraCost>();
            this.ProductMaterials = new HashSet<ProductMaterial>();
        }
    
        public long ProductID { get; set; }
        public long OrderID { get; set; }
        public long ProductNameID { get; set; }
        public decimal ExpectedQuantity { get; set; }
        public short UOMID { get; set; }
        public short CurrencyID { get; set; }
        public decimal CustomerTargetPrice { get; set; }
        public decimal OrderValue { get; set; }
        public Nullable<long> ProductBreakUpID { get; set; }
        public Nullable<decimal> TotalConsumptionCost { get; set; }
    
        public virtual Currency Currency { get; set; }
        public virtual Order Order { get; set; }
        public virtual ProductBreakUp ProductBreakUp { get; set; }
        public virtual ProductName ProductName { get; set; }
        public virtual UnitsOfMeasurement UnitsOfMeasurement { get; set; }
        public virtual ICollection<ProductExtraCost> ProductExtraCosts { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
    }
}
