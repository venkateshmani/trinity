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
    
    public partial class JobOrder
    {
        public JobOrder()
        {
            this.JobOrderReceipts = new HashSet<JobOrderReceipt>();
        }
    
        public long JobOrderID { get; set; }
        public decimal JobQuantity { get; set; }
        public int SupplierID { get; set; }
        public decimal ChargesInINR { get; set; }
        public System.DateTime RequiredDate { get; set; }
        public string Instructions { get; set; }
        public byte JobOrderTypeID { get; set; }
        public Nullable<decimal> PendingQuantity { get; set; }
        public long GRNReciptID { get; set; }
        public Nullable<decimal> QualityPassed { get; set; }
        public Nullable<decimal> QualityFailed { get; set; }
        public string DCNumber { get; set; }
        public string Comments { get; set; }
        public bool IsIssued { get; set; }
        public bool IsWaitingForApproval { get; set; }
        public bool HasApproved { get; set; }
    
        public virtual Company Company { get; set; }
        public virtual GRNReciept GRNReciept { get; set; }
        public virtual JobOrderType JobOrderType { get; set; }
        public virtual ICollection<JobOrderReceipt> JobOrderReceipts { get; set; }
    }
}
