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
    
    public partial class History
    {
        public long HistoryID { get; set; }
        public long OrderID { get; set; }
        public System.DateTime Date { get; set; }
        public string UserName { get; set; }
        public string OrderChanges { get; set; }
        public string Comment { get; set; }
    
        public virtual Order Order { get; set; }
    }
}
