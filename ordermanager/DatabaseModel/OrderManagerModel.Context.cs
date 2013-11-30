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
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
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
    
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetCategory> AssetCategories { get; set; }
        public DbSet<AssetName> AssetNames { get; set; }
        public DbSet<BrandName> BrandNames { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<CommissionValueType> CommissionValueTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyType> CompanyTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<GRNReciept> GRNReciepts { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<JobOrder> JobOrders { get; set; }
        public DbSet<JobOrderReceipt> JobOrderReceipts { get; set; }
        public DbSet<JobOrderType> JobOrderTypes { get; set; }
        public DbSet<MaterialName> MaterialNames { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderCurrencyConversion> OrderCurrencyConversions { get; set; }
        public DbSet<OrderedItem> OrderedItems { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderStatu> OrderStatus { get; set; }
        public DbSet<OrderThrough> OrderThroughs { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<ProductBreakUp> ProductBreakUps { get; set; }
        public DbSet<ProductBreakUpSummary> ProductBreakUpSummaries { get; set; }
        public DbSet<ProductCountryWiseBreakUp> ProductCountryWiseBreakUps { get; set; }
        public DbSet<ProductCutting> ProductCuttings { get; set; }
        public DbSet<ProductExtraCost> ProductExtraCosts { get; set; }
        public DbSet<ProductExtraCostType> ProductExtraCostTypes { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<ProductMaterialItem> ProductMaterialItems { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<ProductName> ProductNames { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderStatu> PurchaseOrderStatus { get; set; }
        public DbSet<Quality> Qualities { get; set; }
        public DbSet<ReceiptStatu> ReceiptStatus { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentMode> ShipmentModes { get; set; }
        public DbSet<SubMaterial> SubMaterials { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<UnitsOfMeasurement> UnitsOfMeasurements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    
        public virtual ObjectResult<Nullable<System.DateTime>> SP_GetServerTime()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<System.DateTime>>("SP_GetServerTime");
        }
    }
}
