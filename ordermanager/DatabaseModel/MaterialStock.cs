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
    
    public partial class MaterialStock
    {
        public MaterialStock()
        {
            this.MaterialInStockHistories = new HashSet<MaterialInStockHistory>();
            this.MaterialsFromStocks = new HashSet<MaterialsFromStock>();
        }
    
        public long MaterialStockID { get; set; }
        public long OrderID { get; set; }
        public long SubMaterialsNameID { get; set; }
        public decimal StockQuantity { get; set; }
        public short UOMID { get; set; }
        public decimal IssuedQuantity { get; set; }
        public System.DateTime InStockDateTime { get; set; }
    
        public virtual ICollection<MaterialInStockHistory> MaterialInStockHistories { get; set; }
        public virtual ICollection<MaterialsFromStock> MaterialsFromStocks { get; set; }
        public virtual Order Order { get; set; }
        public virtual SubMaterial SubMaterial { get; set; }
        public virtual UnitsOfMeasurement UnitsOfMeasurement { get; set; }
    }
}
