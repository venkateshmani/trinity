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
    
    public partial class ProductCountryWiseBreakUp
    {
        public long ProductCountryWiseBreakUpID { get; set; }
        public long ProductBreakUpID { get; set; }
        public int CountryID { get; set; }
        public long ProductSizeID { get; set; }
        public decimal NumberOfPieces { get; set; }
        public int ColorID { get; set; }
    
        public virtual Color Color { get; set; }
        public virtual Country Country { get; set; }
        public virtual ProductBreakUp ProductBreakUp { get; set; }
        public virtual ProductSize ProductSize { get; set; }
    }
}
