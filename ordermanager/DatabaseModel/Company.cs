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
    
    public partial class Company
    {
        public Company()
        {
            this.Assets = new HashSet<Asset>();
            this.CompactingJoes = new HashSet<CompactingJo>();
            this.DyeingJOes = new HashSet<DyeingJO>();
            this.Invoices = new HashSet<Invoice>();
            this.JobOrders = new HashSet<JobOrder>();
            this.KnittingJOes = new HashSet<KnittingJO>();
            this.Orders = new HashSet<Order>();
            this.Orders1 = new HashSet<Order>();
            this.ProductMaterialItems = new HashSet<ProductMaterialItem>();
            this.PurchaseOrders = new HashSet<PurchaseOrder>();
        }
    
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string EmaiID { get; set; }
        public short CompanyTypeID { get; set; }
        public string TIN { get; set; }
        public string CST { get; set; }
    
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<CompactingJo> CompactingJoes { get; set; }
        public virtual CompanyType CompanyType { get; set; }
        public virtual ICollection<DyeingJO> DyeingJOes { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<JobOrder> JobOrders { get; set; }
        public virtual ICollection<KnittingJO> KnittingJOes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Order> Orders1 { get; set; }
        public virtual ICollection<ProductMaterialItem> ProductMaterialItems { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
