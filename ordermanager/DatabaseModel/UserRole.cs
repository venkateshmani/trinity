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
    
    public partial class UserRole
    {
        public UserRole()
        {
            this.Users = new HashSet<User>();
        }
    
        public short UserRoleID { get; set; }
        public string RoleName { get; set; }
        public bool CanCreateUser { get; set; }
        public bool CanCreateNewEnquiry { get; set; }
        public bool CanAddMaterials { get; set; }
        public bool CanAddMaterialsCost { get; set; }
        public bool CanAddConsumptionCost { get; set; }
        public bool CanAddExtraCost { get; set; }
        public bool CanApproveEnquiry { get; set; }
        public bool CanConfirmOrder { get; set; }
        public bool CanAddSubMaterials { get; set; }
        public bool CanGeneratePurchaseOrder { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
    }
}
