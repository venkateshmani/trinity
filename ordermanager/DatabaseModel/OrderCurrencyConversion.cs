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
    
    public partial class OrderCurrencyConversion
    {
        public long OrderCurrencyConversionID { get; set; }
        public long OrderID { get; set; }
        public short CurrencyID { get; set; }
        public decimal ValueInINR { get; set; }
    
        public virtual Currency Currency { get; set; }
        public virtual Order Order { get; set; }
    }
}