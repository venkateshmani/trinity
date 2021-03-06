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
    
    public partial class CartonBox
    {
        public CartonBox()
        {
            this.Packages = new HashSet<Package>();
        }
    
        public long CartonBoxID { get; set; }
        public string Number { get; set; }
        public long OrderID { get; set; }
        public bool InvoiceGenerated { get; set; }
        public Nullable<long> InvoiceID { get; set; }
    
        public virtual Invoice Invoice { get; set; }
        public virtual Order Order { get; set; }
        public virtual ICollection<Package> Packages { get; set; }
    }
}
