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
    
    public partial class Shipment
    {
        public long ShipmentID { get; set; }
        public decimal Shipped { get; set; }
        public decimal Rejected { get; set; }
        public System.DateTime Date { get; set; }
        public long ProductBreakUpSummaryID { get; set; }
    
        public virtual ProductBreakUpSummary ProductBreakUpSummary { get; set; }
    }
}