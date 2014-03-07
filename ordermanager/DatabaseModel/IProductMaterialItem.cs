using System;
namespace ordermanager.DatabaseModel
{
    public interface IPurchaseOrderItem
    {
        string Alias { get; set; }
        decimal CostWrapper { get; set; }
        Currency Currency { get; set; }
        decimal ItemCostWrapper { get; set; }
        decimal QuantityWrapper { get; set; }
        SubMaterial SubMaterial { get; set; }
        decimal TaxPerUnitWrapper { get; set; }
        UnitsOfMeasurement UnitsOfMeasurementWrapper { get; set; }
        PurchaseOrder PurchaseOrder { get; set; }
    }
}
