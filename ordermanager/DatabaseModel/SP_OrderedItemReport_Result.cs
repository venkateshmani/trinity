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
    
    public partial class SP_OrderedItemReport_Result
    {
        public string Po_No { get; set; }
        public Nullable<System.DateTime> Po_Date { get; set; }
        public string Supplier { get; set; }
        public string Item_Name { get; set; }
        public string Alias { get; set; }
        public decimal Item_Quantity { get; set; }
        public Nullable<decimal> Item_Value { get; set; }
        public string Currency { get; set; }
        public long Order_ID { get; set; }
        public string Customer_Name { get; set; }
        public Nullable<decimal> Currency_Value_in_INR { get; set; }
    }
}
