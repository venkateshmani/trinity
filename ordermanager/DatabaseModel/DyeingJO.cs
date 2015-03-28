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
    
    public partial class DyeingJO
    {
        public DyeingJO()
        {
            this.DyeingJoItems = new HashSet<DyeingJoItem>();
        }
    
        public long DyeingJOId { get; set; }
        public int SupplierID { get; set; }
        public long OrderID { get; set; }
        public Nullable<long> PurchaseOrderID { get; set; }
        public System.DateTime JODate { get; set; }
        public string QuoteNo { get; set; }
        public System.DateTime QuoteDate { get; set; }
        public string GRNRefNo { get; set; }
        public string OrderRef { get; set; }
        public string Process { get; set; }
        public string TermsAndConditions { get; set; }
        public decimal TotalValue { get; set; }
        public long ApprovalID { get; set; }
        public string JoNo { get; set; }
        public Nullable<long> JobOrderID { get; set; }
        public Nullable<long> GRNRecieptID { get; set; }
    
        public virtual Approval Approval { get; set; }
        public virtual Company Company { get; set; }
        public virtual GRNReciept GRNReciept { get; set; }
        public virtual JobOrder JobOrder { get; set; }
        public virtual Order Order { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual ICollection<DyeingJoItem> DyeingJoItems { get; set; }
    }
}
