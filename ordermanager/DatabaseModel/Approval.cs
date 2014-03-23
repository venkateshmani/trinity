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
    
    public partial class Approval
    {
        public Approval()
        {
            this.DyeingJOes = new HashSet<DyeingJO>();
            this.KnittingJOes = new HashSet<KnittingJO>();
            this.OrderProducts = new HashSet<OrderProduct>();
            this.ProductMaterials = new HashSet<ProductMaterial>();
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
        }
    
        public long ApprovalID { get; set; }
        public byte ApprovalEntityTypeID { get; set; }
        public long OrderID { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public string Comments { get; set; }
    
        public virtual ApprovalEntityType ApprovalEntityType { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<DyeingJO> DyeingJOes { get; set; }
        public virtual ICollection<KnittingJO> KnittingJOes { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
