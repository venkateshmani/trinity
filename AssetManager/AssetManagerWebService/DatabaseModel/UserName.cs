//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AssetManagerWebService.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserName
    {
        public int UserNameID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int UserRoleID { get; set; }
    
        public virtual UserRole UserRole { get; set; }
    }
}
