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
    
    public partial class OrderReportCardType
    {
        public OrderReportCardType()
        {
            this.OrderReportCards = new HashSet<OrderReportCard>();
        }
    
        public byte OrderReportCardTypeID { get; set; }
        public string Type { get; set; }
    
        public virtual ICollection<OrderReportCard> OrderReportCards { get; set; }
    }
}
