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
    
    public partial class SP_MaterialDetails_Result
    {
        public long MaterialID { get; set; }
        public string MaterialName { get; set; }
        public Nullable<decimal> Consumption { get; set; }
        public string Units { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public string Currency { get; set; }
        public Nullable<decimal> CurrencyValueInINR { get; set; }
        public Nullable<decimal> OtherCostInINR { get; set; }
        public string OtherCostName { get; set; }
        public Nullable<decimal> ConsumptionCost { get; set; }
    }
}