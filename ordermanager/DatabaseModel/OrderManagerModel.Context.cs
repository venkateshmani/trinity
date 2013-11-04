﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OrderManagerDBEntities : DbContext
    {
        public OrderManagerDBEntities()
            : base("name=OrderManagerDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CommissionValueType> CommissionValueTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<MaterialName> MaterialNames { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderCurrencyConversion> OrderCurrencyConversions { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderStatu> OrderStatus { get; set; }
        public DbSet<OrderThrough> OrderThroughs { get; set; }
        public DbSet<ProductBreakUp> ProductBreakUps { get; set; }
        public DbSet<ProductCountryWiseBreakUp> ProductCountryWiseBreakUps { get; set; }
        public DbSet<ProductExtraCost> ProductExtraCosts { get; set; }
        public DbSet<ProductExtraCostType> ProductExtraCostTypes { get; set; }
        public DbSet<ProductMaterialItem> ProductMaterialItems { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<ProductName> ProductNames { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderStatu> PurchaseOrderStatus { get; set; }
        public DbSet<ShipmentMode> ShipmentModes { get; set; }
        public DbSet<SubMaterial> SubMaterials { get; set; }
        public DbSet<UnitsOfMeasurement> UnitsOfMeasurements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
