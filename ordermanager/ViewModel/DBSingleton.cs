using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.Utilities;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Data.SqlClient;

namespace ordermanager.ViewModel
{
    public class DBResources : INotifyPropertyChanged, IDisposable
    {

        #region  single ton
        private static DBResources _DBSingleton = null;

        public static DBResources Instance
        {
            get
            {
                if (_DBSingleton == null)
                    _DBSingleton = new DBResources();

                return _DBSingleton;
            }
        }

        public OrderManagerDBEntities Context
        {
            get { return dbContext; }
        }
        #endregion

        #region
        OrderManagerDBEntities dbContext = null;
        #endregion

        private DBResources()
        {
            dbContext = new OrderManagerDBEntities();
        }

        public void ReInstanceDbContext()
        {
            _DBSingleton = null;
            _DBSingleton = new DBResources();
        }

        public string LastUpdatedStatusString
        {
            get
            {
                string statusString = string.Empty;
                if (LastUpdated != null)
                {
                    statusString = "Last saved at " + LastUpdated.Value.ToLongTimeString();
                }

                return statusString;
            }
        }


        private DateTime? m_LastUpdated = null;
        public DateTime? LastUpdated
        {
            get
            {
                return m_LastUpdated;
            }
            set
            {
                m_LastUpdated = value;
                OnPropertyChanged("LastUpdated");
                OnPropertyChanged("LastUpdatedStatusString");
            }
        }
        

        #region Supporting Collections

        #region Companies

        public ObservableCollection<Company> m_Companies = null;
        public ObservableCollection<Company> Companies
        {
            get
            {
                if (m_Companies == null)
                {
                    m_Companies = new ObservableCollection<Company>(dbContext.Companies.ToList());
                }

                return m_Companies;
            }
            private set
            {
                m_Companies = value;
                OnPropertyChanged("Companies");
            }
        }

        private ObservableCollection<Company> m_Customers = null;
        public ObservableCollection<Company> Customers
        {
            get
            {
                if (m_Customers == null)
                {
                    m_Customers = UpdateCompaniesCollection("Customer");
                }

                return m_Customers;
            }
            private set
            {
                m_Customers = value;
                OnPropertyChanged("Customers");
            }
        }

        private ObservableCollection<Company> m_Agents = null;
        public ObservableCollection<Company> Agents
        {
            get
            {
                if (m_Agents == null)
                {
                    m_Agents = UpdateCompaniesCollection("Agent");
                }
                return m_Agents;
            }
            private set
            {
                m_Agents = value;
                OnPropertyChanged("Agents");
            }
        }

        private ObservableCollection<Company> m_Suppliers = null;
        public ObservableCollection<Company> Suppliers
        {
            get
            {
                if (m_Suppliers == null)
                {
                    m_Suppliers = UpdateCompaniesCollection("Supplier");
                }
                return m_Suppliers;
            }
            private set
            {
                m_Suppliers = value;
                OnPropertyChanged("Suppliers");
            }
        }

        private ObservableCollection<Company> UpdateCompaniesCollection(string type)
        {
            return new ObservableCollection<Company>(dbContext.Companies.Where(c => c.CompanyType.Type == type)
                                                               .Select(c => c)
                                                               .ToList());
        }


        public PurchaseOrder CreateNewPurchaseOrder(Order order, Company supplier, string purchseOrderNumber)
        {
            PurchaseOrder newPO = new PurchaseOrder();
            newPO.PurchaseOrderNumber = purchseOrderNumber;
            newPO.SupplierID = supplier.CompanyID;
            newPO.OrderID = order.OrderID;
            newPO.PurchaseOrderStatusID = 1;

            OrderManagerDBEntities newManager = new OrderManagerDBEntities();
            newManager.PurchaseOrders.Add(newPO);
            newManager.SaveChanges();
            newManager.Dispose();

            newPO = GetPurchaseOrder(purchseOrderNumber);

            return newPO;
        }

        public PurchaseOrder GetPurchaseOrder(string purchaseOrderNumber)
        {
            return dbContext.PurchaseOrders.Where(c => c.PurchaseOrderNumber == purchaseOrderNumber)
                                    .Select(c => c)
                                    .FirstOrDefault();
        }

        //Create a new Company of type passed in the arguments
        public Company CreateNewCompany(string type)
        {
            Company newCompany = new Company();


            var companyType = dbContext.CompanyTypes.Where(c => c.Type == type)
                                                      .Select(c => c)
                                                      .FirstOrDefault();

            newCompany.CompanyTypeID = companyType.CompanyTypeID;
            //newCompany.CompanyType = companyType;

            return newCompany;
        }

        public Company SaveNewCompany(Company company, string type)
        {
            OrderManagerDBEntities localContext = new OrderManagerDBEntities();
            try
            {
                var companyType = localContext.CompanyTypes.Where(c => c.Type == type).Select(c => c).FirstOrDefault();
                company.CompanyTypeID = companyType.CompanyTypeID;
                company.CompanyType = companyType;
                localContext.Companies.Add(company);
                localContext.SaveChanges();
                localContext.Dispose();
            }
            catch { throw; }
            Companies = new ObservableCollection<Company>(dbContext.Companies.ToList());
            Company newCompany = null;
            if (Companies != null)
            {
                newCompany = Companies.Where(c => (c.CompanyType.Type == company.CompanyType.Type && c.Name == company.Name)).Select(c => c).FirstOrDefault();
            }
            else return null;

            switch (company.CompanyType.Type)
            {
                case "Customer":
                    Customers = UpdateCompaniesCollection("Customer");
                    break;
                case "Agent":
                    Agents = UpdateCompaniesCollection("Agent");
                    break;
                case "Supplier":
                    Suppliers = UpdateCompaniesCollection("Supplier");
                    break;
            }
            return newCompany;
        }

        private ObservableCollection<CompanyType> m_CompanyTypes = null;
        public ObservableCollection<CompanyType> CompanyTypes
        {
            get
            {
                if (m_CompanyTypes == null)
                {
                    m_CompanyTypes = new ObservableCollection<CompanyType>(dbContext.CompanyTypes);
                }
                return m_CompanyTypes;
            }
            private set
            {
                m_CompanyTypes = value;
            }
        }
        #endregion

        #region Product, Materials and SubMaterials

        private ObservableCollection<ProductName> m_AvailableProducts = null;
        public ObservableCollection<ProductName> AvailableProducts
        {
            get
            {
                if (m_AvailableProducts == null)
                {
                    m_AvailableProducts = new ObservableCollection<ProductName>(dbContext.ProductNames.ToList());
                }
                return m_AvailableProducts;
            }
            private set
            {
                m_AvailableProducts = value;
                OnPropertyChanged("AvailableProducts");
            }
        }

        //Create a new product
        public ProductName CreateNewProduct(string newProductName, string styleID)
        {
            ProductName newProduct = new ProductName();
            newProduct.Name = newProductName;

            newProduct.StyleID = styleID;
            OrderManagerDBEntities newManager = new OrderManagerDBEntities();
            newManager.ProductNames.Add(newProduct);
            newManager.SaveChanges();
            newManager.Dispose();

            AvailableProducts = new ObservableCollection<ProductName>(dbContext.ProductNames.ToList());

            newProduct = AvailableProducts.Where(a => a.Name == newProductName)
                                          .Select(a => a).First();

            return newProduct;
        }

        private ObservableCollection<MaterialName> m_AvailableMaterials = null;
        public ObservableCollection<MaterialName> AvailableMaterials
        {
            get
            {
                if (m_AvailableMaterials == null)
                {
                    m_AvailableMaterials = new ObservableCollection<MaterialName>(dbContext.MaterialNames.ToList());
                }
                return m_AvailableMaterials;
            }
            private set
            {
                m_AvailableMaterials = value;
                OnPropertyChanged("AvailableMaterials");
            }
        }

        Dictionary<string, ObservableCollection<SubMaterial>> m_AvailableSubMaterials;
        public Dictionary<string, ObservableCollection<SubMaterial>> AvailableSubMaterials
        {
            get
            {
                if (m_AvailableSubMaterials == null)
                {
                    PopulateAvailableSubMaterials();
                }
                return m_AvailableSubMaterials;
            }
        }

        private ObservableCollection<ProductExtraCostType> m_AvailableExtraCostTypes = null;
        public ObservableCollection<ProductExtraCostType> AvailableExtraCostTypes
        {
            get
            {
                if (m_AvailableExtraCostTypes == null)
                {
                    m_AvailableExtraCostTypes = new ObservableCollection<ProductExtraCostType>(dbContext.ProductExtraCostTypes.ToList());
                }
                return m_AvailableExtraCostTypes;
            }
            private set
            {
                m_AvailableExtraCostTypes = value;
                OnPropertyChanged("AvailableExtraCostTypes");
            }
        }

        private void PopulateAvailableSubMaterials()
        {
            m_AvailableSubMaterials = new Dictionary<string, ObservableCollection<SubMaterial>>(1);
            List<SubMaterial> subMaterials = dbContext.SubMaterials.ToList();
            foreach (MaterialName mName in AvailableMaterials)
            {
                ObservableCollection<SubMaterial> queryItems = new ObservableCollection<SubMaterial>((from item in subMaterials where item.MaterialName.Name == mName.Name select item));
                if (!m_AvailableSubMaterials.ContainsKey(mName.Name))
                    m_AvailableSubMaterials.Add(mName.Name, queryItems);
            }

            OnPropertyChanged("AvailableSubMaterials");
        }

        #region Inline Items Creation

        //Create a new product
        public MaterialName CreateNewMaterial(string newMaterialName)
        {
            //Check whether the material is already existing
            MaterialName newMaterial = AvailableMaterials.Where(a => a.Name == newMaterialName)
                       .Select(a => a).FirstOrDefault();

            if (newMaterial == null)
            {
                OrderManagerDBEntities newManager = new OrderManagerDBEntities();
                newMaterial = new MaterialName();
                newMaterial.Name = newMaterialName;
                newManager.MaterialNames.Add(newMaterial);
                newManager.SaveChanges();
                newManager.Dispose();

                AvailableMaterials = new ObservableCollection<MaterialName>(dbContext.MaterialNames.ToList()); //Refresh

                newMaterial = AvailableMaterials.Where(a => a.Name == newMaterialName)
                              .Select(a => a).First();
            }
            return newMaterial;
        }

        public ProductExtraCostType CreateNewExtraCostType(string extraCostName)
        {
            ProductExtraCostType newCostType = AvailableExtraCostTypes.Where(a => a.TypeName == extraCostName)
                       .Select(a => a).FirstOrDefault();

            if (newCostType == null)
            {
                OrderManagerDBEntities newManager = new OrderManagerDBEntities();
                newCostType = new ProductExtraCostType();
                newCostType.TypeName = extraCostName;
                newManager.ProductExtraCostTypes.Add(newCostType);
                newManager.SaveChanges();
                newManager.Dispose();
                AvailableExtraCostTypes = new ObservableCollection<ProductExtraCostType>(dbContext.ProductExtraCostTypes.ToList()); //Refresh
                newCostType = AvailableExtraCostTypes.Where(a => a.TypeName == extraCostName)
                              .Select(a => a).First();
            }
            return newCostType;
        }

        public SubMaterial CreateNewSubMaterial(string subMaterialName, ProductMaterial material)
        {
            SubMaterial newSubMaterial = AvailableSubMaterials[material.MaterialName.Name].Where(a => a.Name == subMaterialName)
                                                                                            .Select(a => a).FirstOrDefault();

            if (newSubMaterial == null)
            {
                OrderManagerDBEntities newManager = new OrderManagerDBEntities();
                newSubMaterial = new SubMaterial();
                newSubMaterial.Name = subMaterialName;
                newSubMaterial.MaterialNameID = material.MaterialNameID;
                newManager.SubMaterials.Add(newSubMaterial);
                newManager.SaveChanges();
                newManager.Dispose();

                if (AvailableSubMaterials.ContainsKey(material.MaterialName.Name))
                {
                    PopulateAvailableSubMaterials();
                    newSubMaterial = AvailableSubMaterials[material.MaterialName.Name].Where(a => a.Name == subMaterialName)
                                                                                        .Select(a => a).First();
                }
            }

            return newSubMaterial;
        }

        #endregion

        #endregion

        #region No Add support tables

        private ObservableCollection<OrderThrough> m_OrderThroughs = null;
        public ObservableCollection<OrderThrough> OrderThroughs
        {
            get
            {
                if (m_OrderThroughs == null)
                {
                    m_OrderThroughs = new ObservableCollection<OrderThrough>(dbContext.OrderThroughs.ToList());
                }

                return m_OrderThroughs;
            }
        }

        private ObservableCollection<CommissionValueType> m_CommissionValueTypes = null;
        public ObservableCollection<CommissionValueType> CommissionValueTypes
        {
            get
            {
                if (m_CommissionValueTypes == null)
                {
                    m_CommissionValueTypes = new ObservableCollection<CommissionValueType>(dbContext.CommissionValueTypes.ToList());
                }
                return m_CommissionValueTypes;
            }
        }

        private ObservableCollection<Currency> m_Currencies = null;
        public ObservableCollection<Currency> Currencies
        {
            get
            {
                if (m_Currencies == null)
                {
                    m_Currencies = new ObservableCollection<Currency>(dbContext.Currencies.ToList());
                }

                return m_Currencies;
            }
        }

        private ObservableCollection<UnitsOfMeasurement> m_UOMs = null;
        public ObservableCollection<UnitsOfMeasurement> UOMs
        {
            get
            {
                if (m_UOMs == null)
                {
                    m_UOMs = new ObservableCollection<UnitsOfMeasurement>(dbContext.UnitsOfMeasurements.ToList());
                }

                return m_UOMs;
            }
        }

        public ObservableCollection<ShipmentMode> m_ShipmentModes = null;
        public ObservableCollection<ShipmentMode> ShipmentModes
        {
            get
            {
                if (m_ShipmentModes == null)
                {
                    m_ShipmentModes = new ObservableCollection<ShipmentMode>(dbContext.ShipmentModes.ToList());
                }

                return m_ShipmentModes;
            }
            private set
            {
                m_ShipmentModes = value;
            }
        }
        #endregion

        #region Orders

        public Order CreateNewOrder()
        {
            Order newOrder = dbContext.Orders.Create();

            //Prepare Order Report Card
            foreach (OrderReportCardType type in dbContext.OrderReportCardTypes)
            {
                OrderReportCard reportCard = new OrderReportCard();
                reportCard.OrderReportCardType = type;
                reportCard.Order = newOrder;
                newOrder.OrderReportCards.Add(reportCard);
            }


            //Prepare Job Order Tolerances
            foreach (JobOrderToleranceType type in dbContext.JobOrderToleranceTypes)
            {
                JobOrderTolerance toleranceEntry = new JobOrderTolerance();
                toleranceEntry.Order = newOrder;
                toleranceEntry.JobOrderToleranceType = type;
                toleranceEntry.ToleranceValueInPercentage = type.DefaultToleranceInPercentage;
                newOrder.JobOrderTolerances.Add(toleranceEntry);
            }

            newOrder.OrderDate = DateTime.Now;
            return newOrder;
        }

        public Order CreateNewOrder(Order newOrder, string userComment)
        {
            Order newSavedOrder = dbContext.Orders.Add(newOrder);
            History historyItem = new History();
            historyItem.Comment = userComment;
            historyItem.UserName = CurrentUser.UserName;
            historyItem.OrderChanges = "Created New Enquiry";
            historyItem.Date = DateTime.Now;
            newSavedOrder.Histories.Add(historyItem);

            Save();
            return newSavedOrder;
        }

        #endregion

        #region Asset Management

        public ObservableCollection<AssetName> m_AssetNames = null;
        public ObservableCollection<AssetName> AvailableAssetNames
        {
            get
            {
                if (m_AssetNames == null)
                {
                    m_AssetNames = new ObservableCollection<AssetName>(dbContext.AssetNames.ToList());
                }

                return m_AssetNames;
            }
            private set
            {
                m_AssetNames = value;
                OnPropertyChanged("AvailableAssetNames");
            }
        }


        public AssetName CreateNewAssetName(string assetName)
        {
            AssetName newAssetName = new AssetName();
            newAssetName.Name = assetName;

            OrderManagerDBEntities newManager = new OrderManagerDBEntities();
            newManager.AssetNames.Add(newAssetName);
            newManager.SaveChanges();
            newManager.Dispose();

            AvailableAssetNames = new ObservableCollection<AssetName>(dbContext.AssetNames.ToList());

            newAssetName = AvailableAssetNames.Where(a => a.Name == assetName)
                                          .Select(a => a).First();

            return newAssetName;
        }

        #endregion

        #region Color Management


        public ObservableCollection<Color> m_Colors = null;
        public ObservableCollection<Color> Colors
        {
            get
            {
                if (m_Colors == null)
                {
                    m_Colors = new ObservableCollection<Color>(dbContext.Colors.ToList());
                }

                return m_Colors;
            }
            private set
            {
                m_Colors = value;
                OnPropertyChanged("Colors");
            }
        }

        public Color CreateNewColor(string colorName)
        {
            Color newColor = new Color();
            newColor.Name = colorName;

            OrderManagerDBEntities newManager = new OrderManagerDBEntities();
            newManager.Colors.Add(newColor);
            newManager.SaveChanges();
            newManager.Dispose();

            Colors = new ObservableCollection<Color>(dbContext.Colors.ToList());

            newColor = Colors.Where(a => a.Name == colorName)
                             .Select(a => a).First();

            return newColor;
        }

        #endregion

        #region [JobOrderType]
        public ObservableCollection<JobOrderType> m_AfterKnittingJobs = null;
        public ObservableCollection<JobOrderType> AfterKnittingJobs
        {
            get
            {
                if (m_AfterKnittingJobs == null)
                    SetAvailableNextJobOrderTypes();
                return m_AfterKnittingJobs;
            }
        }
        public ObservableCollection<JobOrderType> m_AfterDyeingJobs = null;
        public ObservableCollection<JobOrderType> AfterDyeingJobs
        {
            get
            {
                if (m_AfterDyeingJobs == null)
                    SetAvailableNextJobOrderTypes();
                return m_AfterDyeingJobs;
            }
        }
        public ObservableCollection<JobOrderType> m_AfterPrintingJobs = null;
        public ObservableCollection<JobOrderType> AfterPrintingJobs
        {
            get
            {
                if (m_AfterPrintingJobs == null)
                    SetAvailableNextJobOrderTypes();
                return m_AfterPrintingJobs;
            }
        }
        public ObservableCollection<JobOrderType> m_AfterCompactingJobs = null;
        public ObservableCollection<JobOrderType> AfterCompactingJobs
        {
            get
            {
                if (m_AfterCompactingJobs == null)
                    SetAvailableNextJobOrderTypes();
                return m_AfterCompactingJobs;
            }
        }
        public ObservableCollection<JobOrderType> m_AfterWashingJobs = null;
        public ObservableCollection<JobOrderType> AfterWashingJobs
        {
            get
            {
                if (m_AfterWashingJobs == null)
                    SetAvailableNextJobOrderTypes();
                return m_AfterWashingJobs;
            }
        }
        public ObservableCollection<JobOrderType> m_AfterOtherJobs = null;
        public ObservableCollection<JobOrderType> AfterOtherJobs
        {
            get
            {
                if (m_AfterOtherJobs == null)
                    SetAvailableNextJobOrderTypes();
                return m_AfterOtherJobs;
            }
        }

        ObservableCollection<JobOrderType> m_AllJobsTypes = null;
        public ObservableCollection<JobOrderType> AllJobsTypes
        {
            get
            {
                if (m_AllJobsTypes == null)
                    SetAvailableNextJobOrderTypes();
                return m_AllJobsTypes;
            }
        }

        private void SetAvailableNextJobOrderTypes()
        {
            m_AllJobsTypes = new ObservableCollection<JobOrderType>(dbContext.JobOrderTypes.ToList());
            m_AfterKnittingJobs = new ObservableCollection<JobOrderType>(m_AllJobsTypes.Where(c => c.JobOrderTypeID > 1).Select(c => c).ToList());
            m_AfterDyeingJobs = new ObservableCollection<JobOrderType>(m_AllJobsTypes.Where(c => c.JobOrderTypeID > 2).Select(c => c).ToList());
            m_AfterPrintingJobs = new ObservableCollection<JobOrderType>(m_AllJobsTypes.Where(c => c.JobOrderTypeID > 3).Select(c => c).ToList());
            m_AfterCompactingJobs = new ObservableCollection<JobOrderType>(m_AllJobsTypes.Where(c => c.JobOrderTypeID > 4).Select(c => c).ToList());
            m_AfterWashingJobs = new ObservableCollection<JobOrderType>(m_AllJobsTypes.Where(c => c.JobOrderTypeID > 5).Select(c => c).ToList());
            m_AfterOtherJobs = new ObservableCollection<JobOrderType>(m_AllJobsTypes.Where(c => c.JobOrderTypeID > 6).Select(c => c).ToList());
        }
        #endregion [JobOrderType]

        public bool UpdateOrderProducts()
        {
            Save();
            return true;
        }

        #endregion


        public void AttachTheMissingNavigationProperties()
        {
            foreach (DbEntityEntry entry in Context.ChangeTracker.Entries())
            {
                OrderCurrencyConversion curConv = entry.Entity as OrderCurrencyConversion;
                if (curConv != null && curConv.Order == null)
                {
                    curConv.Order = dbContext.Orders.Find(curConv.OrderID);
                }
            }
        }

        public void ReloadChangedEntities()
        {
            foreach (DbEntityEntry entry in Context.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                    entry.State = EntityState.Detached;
                else
                    entry.Reload();
            }
        }

        public void DiscardChanges()
        {
            // Undo the changes of the all entries. 
            foreach (DbEntityEntry entry in Context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    // Under the covers, changing the state of an entity from  
                    // Modified to Unchanged first sets the values of all  
                    // properties to the original values that were read from  
                    // the database when it was queried, and then marks the  
                    // entity as Unchanged. This will also reject changes to  
                    // FK relationships since the original value of the FK  
                    // will be restored. 
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    // If the EntityState is the Deleted, reload the date from the database.   
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default: break;
                }
            }
        }

        

        public bool Save()
        {
            try
            {
                AttachTheMissingNavigationProperties();
                dbContext.SaveChanges();
                LastUpdated = GetServerTime(true);
                return true;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                        DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"D:\errors.txt", outputLines);
                throw;
            }
        }

        

        public void Dispose()
        {
            dbContext.Dispose();
        }


        private DateTime? cachedDateTime = null;

        public DateTime GetServerTime()
        {
            return GetServerTime(false);
        }

        public DateTime GetServerTime(bool refetch)
        {
            if (cachedDateTime == null || refetch)
            {
                List<DateTime?> serverTime = DBResources.Instance.Context.SP_GetServerTime().ToList<DateTime?>();
                cachedDateTime = serverTime[0].GetValueOrDefault(DateTime.Now);
            }

            return cachedDateTime.Value;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region User Management


        private ObservableCollection<UserRole> m_UserRole = null;
        public ObservableCollection<UserRole> UserRoles
        {
            get
            {
                if (m_UserRole == null)
                {
                    m_UserRole = new ObservableCollection<UserRole>(dbContext.UserRoles.ToList());
                }

                return m_UserRole;
            }
        }

        public void AddNewUser(User NewUser)
        {
            dbContext.Users.Add(NewUser);
            Save();
        }


        public User CurrentUser
        {
            get;
            private set;
        }

        
        public LoginResult AuthenticateUser(string userName, string password)
        {
            User user = dbContext.Users.Where(u => u.UserName == userName).Select(u => u)
                                                        .FirstOrDefault();
            if (user != null)
            {
                string passwordValueInDatabase = user.Password;
                if (user.Password == password || passwordValueInDatabase.Decrypt() == password)  //Later condition is just only for development
                {
                    CurrentUser = user;
                    return new LoginResult() { Authenticated = true, NeedPasswordReset = (passwordValueInDatabase == user.UserName) };
                }
                else
                    return new LoginResult() { Authenticated = false, Message = "Authentication failed." };

            }

            return new LoginResult() { Authenticated = false, Message = "User not found" };
        }

        public bool ChangePassword(string newPassword)
        {
            string encryptedPassword = newPassword.Encrypt();
            CurrentUser.Password = encryptedPassword;
            Save();
            return true;
        }

        #endregion

        #region Country Management

        private ObservableCollection<Country> m_Countries = null;
        public ObservableCollection<Country> Countries
        {
            get
            {
                if (m_Countries == null)
                {
                    m_Countries = new ObservableCollection<Country>(dbContext.Countries.ToList());
                }
                return m_Countries;
            }
            set
            {
                m_Countries = value;
                OnPropertyChanged("Countries");
            }
        }


        public Country CreateNewCountry(string countryName)
        {
            Country country = new Country();
            country.Name = countryName;

            OrderManagerDBEntities newManager = new OrderManagerDBEntities();
            newManager.Countries.Add(country);
            newManager.SaveChanges();
            newManager.Dispose();

            Countries = new ObservableCollection<Country>(dbContext.Countries.ToList());

            country = Countries.Where(a => a.Name == countryName)
                                          .Select(a => a).First();

            return country;
        }

        #endregion

        #region Product Size Management

        private ObservableCollection<ProductSize> m_ProductSizes = null;
        public ObservableCollection<ProductSize> ProductSizes
        {
            get
            {
                if (m_ProductSizes == null)
                {
                    m_ProductSizes = new ObservableCollection<ProductSize>(dbContext.ProductSizes.ToList());
                }

                return m_ProductSizes;
            }
            set
            {
                m_ProductSizes = value;
                OnPropertyChanged("ProductSizes");
            }
        }

        public ProductSize CreateNewProductSize(string newSize)
        {
            ProductSize newProductSize = new ProductSize();
            newProductSize.Size = newSize;

            OrderManagerDBEntities newManager = new OrderManagerDBEntities();
            newManager.ProductSizes.Add(newProductSize);
            newManager.SaveChanges();
            newManager.Dispose();

            ProductSizes = new ObservableCollection<ProductSize>(dbContext.ProductSizes.ToList());

            newProductSize = ProductSizes.Where(a => a.Size == newSize)
                                          .Select(a => a).First();

            return newProductSize;
        }

        #endregion
    }

    public class LoginResult
    {
        public bool Authenticated
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
        public bool NeedPasswordReset
        {
            get;
            set;
        }

    }
}
