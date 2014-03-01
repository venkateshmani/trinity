using ordermanager.Extended_Database_Models;
using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class OrderProduct : EntityBase
    {

        #region fields

        private ProductExtraCost m_OtherCost = null;

        #endregion

        #region Property Wrappers

        public virtual Currency CurrencyWrapper
        {
            get
            {
                return Currency;
            }
            set
            {
                Currency = value;
                ValidateCurrency();
                SelectOrAddCurrencyConversion();
                CalculateOrderValue();

                OnPropertyChanged("CurrencyValueInINR");
            }
        }

        private ObservableCollection<ProductExtraCost> m_ProductExtraCostWrapper = null;

        public ObservableCollection<ProductExtraCost> ProductExtraCostWrapper
        {
            get
            {
                if (m_ProductExtraCostWrapper == null)
                {
                    m_ProductExtraCostWrapper = new ObservableCollection<ProductExtraCost>(this.ProductExtraCosts);
                    foreach (var extraCost in m_ProductExtraCostWrapper)
                    {
                        extraCost.PropertyChanged += ExtraCost_PropertyChanged;
                    }
                    m_ProductExtraCostWrapper.CollectionChanged += m_ProductExtraCostWrapper_CollectionChanged;
                }
                return m_ProductExtraCostWrapper;
            }
        }

        void m_ProductExtraCostWrapper_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    ProductExtraCost newMaterialItem = newItem as ProductExtraCost;
                    newMaterialItem.PropertyChanged += ExtraCost_PropertyChanged;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var deletedItem in e.OldItems)
                {
                    ProductExtraCost deletedMaterial = deletedItem as ProductExtraCost;                   
                    deletedMaterial.OrderProduct = null;
                    deletedMaterial.PropertyChanged -= ExtraCost_PropertyChanged;
                }
            }
            OnPropertyChanged("ProductExtraCostWrapper");
        }

        void ExtraCost_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CostWrapper")
            {
                CalculateTotalMaterialsCost();
            }
        }


        //public virtual ProductExtraCost OtherCost
        //{
        //    get
        //    {
        //        if (m_OtherCost == null)
        //        {
        //            foreach (var extraCost in ProductExtraCosts)
        //            {
        //                if (extraCost.ExtraCostTypeID == 1)
        //                {
        //                    m_OtherCost = extraCost;
        //                    m_OtherCost.PropertyChanged += m_OtherCost_PropertyChanged;
        //                    break;
        //                }
        //            }
        //        }

        //        if (m_OtherCost == null)
        //        {
        //            m_OtherCost = new ProductExtraCost();
        //            m_OtherCost.PropertyChanged += m_OtherCost_PropertyChanged;
        //            m_OtherCost.ExtraCostTypeID = 1;
        //            Currency indianCurrency = DBResources.Instance.Context.Currencies.Find(1);

        //            if (indianCurrency != null)
        //                m_OtherCost.Currency = indianCurrency;

        //            this.ProductExtraCosts.Add(m_OtherCost);
        //        }

        //        return m_OtherCost;
        //    }
        //}

        void m_OtherCost_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CostWrapper")
            {
                CalculateTotalMaterialsCost();
            }
        }

        public virtual ProductName ProductNameWrapper
        {
            get
            {
                return ProductName;
            }
            set
            {
                ProductName = value;
                ValidateProduct();
                OnPropertyChanged("ProductNameWrapper");
            }
        }

        public virtual UnitsOfMeasurement UnitsOfMeasurementWrapper
        {
            get
            {
                return UnitsOfMeasurement;
            }
            set
            {
                UnitsOfMeasurement = value;
                CalculateOrderValue();
                ValidateUOM();
            }
        }

        public ProductBreakUp ProductBreakUpWrapper
        {
            get
            {
                if (ProductBreakUp == null)
                    ProductBreakUp = new ProductBreakUp();
                return ProductBreakUp;
            }
            set
            {
                if (ProductBreakUp != value)
                    ProductBreakUp = value;
            }
        }

        public decimal PerUnitOrderValue
        {
            get
            {
                if (ExpectedQuantity != 0)
                    return OrderValueWrapper / ExpectedQuantity;

                return 0;
            }
            set
            {
                OnPropertyChanged("PerUnitOrderValue");
            }
        }

        public decimal OrderValueWrapper
        {
            get
            {
                return OrderValue;
            }
            set
            {
                OrderValue = value;
                OnPropertyChanged("OrderValueWrapper");
                OnPropertyChanged("PerUnitOrderValue");
            }
        }

        public decimal CustomerTargetPriceWrapper
        {
            get
            {
                return CustomerTargetPrice;
            }
            set
            {
                CustomerTargetPrice = value;
                CalculateOrderValue();
                ValidateCustomerTargetPrice();
                RefreshConsumptionCostProperties();
            }
        }

        public decimal ExpectedQuantityWrapper
        {
            get
            {
                return ExpectedQuantity;
            }
            set
            {
                ExpectedQuantity = value;
                CalculateOrderValue();
                ValidateExpectedQuantity();
                RefreshConsumptionCostProperties();
            }
        }


        /// <summary>
        /// Total Our Cost
        /// </summary>
        public decimal TotalOurCostInProductCurrenyValue
        {
            get
            {
                decimal cost = 0;

                if (CurrencyConversion == null)
                    SelectOrAddCurrencyConversion();

                if (CurrencyConversion.ValueInINR != null)
                {
                    cost = PerUnitTotalProductMaterialsCost / CurrencyConversion.ValueInINR.Value;
                }

                cost = cost * ExpectedQuantityWrapper * UnitsOfMeasurement.Multiplier;

                return cost;
            }
        }

        public decimal TotalProfitOrLossAmount
        {
            get
            {
                return OrderValue - TotalOurCostInProductCurrenyValue;
            }
        }

        /// <summary>
        /// Per unit value
        /// </summary>
        public decimal OurCostInProductCurrenyValue
        {
            get
            {
                decimal cost = 0;

                if (CurrencyConversion == null)
                    SelectOrAddCurrencyConversion();

                if (CurrencyConversion.ValueInINR != null)
                {
                    cost = PerUnitTotalProductMaterialsCost / CurrencyConversion.ValueInINR.Value;
                }

                return cost;
            }
        }


        public bool IsOurCostHigherThanQuoted
        {
            get
            {
                return OurCostInProductCurrenyValue > CustomerTargetPrice;
            }
        }

        public decimal ProfitOrLossAmount
        {
            get
            {
                return CustomerTargetPrice - OurCostInProductCurrenyValue;
            }
        }

        private void RefreshConsumptionCostProperties()
        {
            OnPropertyChanged("OurCostInProductCurrenyValue");
            OnPropertyChanged("IsOurCostHigherThanQuoted");
            OnPropertyChanged("ProfitOrLossAmount");
            OnPropertyChanged("TotalOurCostInProductCurrenyValue");
            OnPropertyChanged("TotalProfitOrLossAmount");
        }

        private decimal m_PerUnitTotalProductMaterialsCost = 0;
        public decimal PerUnitTotalProductMaterialsCost
        {
            get
            {
                if (m_PerUnitTotalProductMaterialsCost == 0 && ExpectedQuantity != 0)
                {
                    m_PerUnitTotalProductMaterialsCost = TotalProductMaterialsCostWrapper / ExpectedQuantity;
                }

                return m_PerUnitTotalProductMaterialsCost;
            }
            set
            {
                m_PerUnitTotalProductMaterialsCost = value;
                ValidatePerUnitTotalProductMaterialsCost();
                OnPropertyChanged("PerUnitTotalProductMaterialsCost");
            }
        }

        public decimal TotalProductMaterialsCostWrapper
        {
            get
            {
                if (m_TotalProductMaterialsCostWrapper == -1.0m)
                    CalculateTotalMaterialsCost();
                return m_TotalProductMaterialsCostWrapper;
            }
            set
            {
                if (m_TotalProductMaterialsCostWrapper != value)
                {
                    m_TotalProductMaterialsCostWrapper = value;
                    OnPropertyChanged("TotalProductMaterialsCostWrapper");
                    if (ExpectedQuantity != 0)
                        PerUnitTotalProductMaterialsCost = TotalProductMaterialsCostWrapper / ExpectedQuantity;
                }
            }
        }

        public decimal TotalProductPurchaseCostWrapper
        {
            get
            {
                if (m_TotalProductPurchaseCostWrapper == -1.0m)
                    CalculateTotalPurchaseCost();
                return m_TotalProductPurchaseCostWrapper;
            }
            set
            {
                if (m_TotalProductPurchaseCostWrapper != value)
                {
                    m_TotalProductPurchaseCostWrapper = value;
                    OnPropertyChanged("TotalProductPurchaseCostWrapper");
                }
            }
        }

        private bool m_HasErrorsInProductMaterials;
        public bool HasErrorsInProductMaterials
        {
            get
            {
                return m_HasErrorsInProductMaterials;
            }
            set
            {
                m_HasErrorsInProductMaterials = value;
                OnPropertyChanged("HasErrorsInProductMaterials");
            }
        }

        public override bool HasUserClickedSaveOrSubmit
        {
            get
            {
                return base.HasUserClickedSaveOrSubmit;
            }
            set
            {
                foreach (ProductExtraCost extraCost in ProductExtraCosts)
                {
                    extraCost.HasUserClickedSaveOrSubmit = value;
                }

                foreach (ProductMaterial material in ProductMaterials)
                {
                    material.HasUserClickedSaveOrSubmit = true;
                }

                base.HasUserClickedSaveOrSubmit = value;
            }
        }

        public bool CanAddMaterialsWrapper
        {
            get { return DBResources.Instance.CurrentUser.UserRole.CanAddMaterials; }
        }
        public bool CanAddConsumptionWrapper
        {
            get { return DBResources.Instance.CurrentUser.UserRole.CanAddConsumption; }
        }
        public bool CanAddMaterialsCostWrapper
        {
            get { return DBResources.Instance.CurrentUser.UserRole.CanAddMaterialsCost; }
        }

        public bool ValidateProductMaterials()
        {

            bool hasError = false;
            if (HasUserClickedSaveOrSubmit)
            {
                if (ProductMaterialsWrapper.Count == 0)
                {
                    hasError = true;
                }
            }

            foreach (var material in ProductMaterialsWrapper)
            {
                material.ValidateMaterialName();
                material.ValidateUOM();
                material.ValidateConsumtpion();
                if (Helper.GetOrderStatusEnumFromString(this.Order.OrderStatu.StatusLabel) == OrderStatusEnum.MaterialsAdded)
                {
                    material.ValidateCostPerUnit();
                    material.ValidateCurrency();
                    material.ValidateCurrencyValueInINR();
                    material.ValidateOtherCost();
                    if (ValidateExtraCosts())
                        hasError = true;
                }
                if (material.HasErrors)
                    hasError = true;
                material.IsValidating = false;
            }

            if (ValidatePerUnitTotalProductMaterialsCost())
                hasError = true;

            HasErrorsInProductMaterials = hasError;
            return hasError;
        }

        public bool ValidateExtraCosts()
        {
            bool hasError = false;
            if (DBResources.Instance.CurrentUser.UserRole.CanAddMaterialsCost)
            {
                foreach (ProductExtraCost eCost in ProductExtraCostWrapper)
                {
                    if (!eCost.Validate())
                        hasError = true;
                }
            }
            return hasError;
        }

        #endregion

        #region Helpers

        public decimal CurrencyValueInINR
        {
            get
            {
                if (Currency != null && Currency.DefaultValueInINR != null)
                    return Currency.DefaultValueInINR.Value;

                if (CurrencyConversion == null)
                    SelectOrAddCurrencyConversion();

                if (CurrencyConversion != null)
                    return CurrencyConversion.ValueInINRWrapper;

                return 0;
            }
            set
            {
                if (Currency != null && Currency.DefaultValueInINR == null)
                {
                    CurrencyConversion.ValueInINRWrapper = value;
                    ValidateCurrencyValueInINR();
                }

                OnPropertyChanged("CurrencyValueInINR");
            }
        }

        private OrderCurrencyConversion m_CurrencyConversion = null;
        public OrderCurrencyConversion CurrencyConversion
        {
            get
            {
                return m_CurrencyConversion;
            }
            set
            {
                if (m_CurrencyConversion != null)
                {
                    m_CurrencyConversion.PropertyChanged -= m_CurrencyConversion_PropertyChanged;
                }

                m_CurrencyConversion = value;
                m_CurrencyConversion.PropertyChanged += m_CurrencyConversion_PropertyChanged;
            }
        }

        private void SelectOrAddCurrencyConversion()
        {
            if (Currency != null)
            {
                var currencyConversion = Order.OrderCurrencyConversions.Where(c => c.Currency.CurrencyID == Currency.CurrencyID)
                                                                       .Select(c => c);

                OrderCurrencyConversion newConversion = null;
                if (currencyConversion == null || currencyConversion.Count() == 0)
                {
                    newConversion = new OrderCurrencyConversion();
                    newConversion.CurrencyWrapper = Currency;

                    Order.OrderCurrencyConversions.Add(newConversion);
                }
                else
                {
                    newConversion = currencyConversion.First();
                }

                CurrencyConversion = newConversion;
            }
        }

        void m_CurrencyConversion_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ValueInINRWrapper")
            {
                OnPropertyChanged("CurrencyValueInINR");
                CalculateOrderValue();
            }
        }

        private void CalculateOrderValue()
        {
            decimal uomMultipler = 1;
            if (UnitsOfMeasurement != null)
                uomMultipler = UnitsOfMeasurement.Multiplier;

            OrderValueWrapper = ExpectedQuantity * CustomerTargetPrice * CurrencyValueInINR * uomMultipler;
        }


        public string StyleID
        {
            get
            {
                if (this.ProductName != null)
                    return this.ProductName.StyleID;

                return string.Empty;
            }
        }

        private Dictionary<string, PurchaseOrderItems> m_PurchaseOrderAndTheirItems = null;
        public List<PurchaseOrderItems> PurchaseOrderItems
        {
            get
            {
                if (m_PurchaseOrderAndTheirItems == null)
                {
                    m_PurchaseOrderAndTheirItems = new Dictionary<string, PurchaseOrderItems>();
                    foreach (ProductMaterial material in ProductMaterials)
                    {
                        foreach (ProductMaterialItem item in material.ProductMaterialItems)
                        {
                            PurchaseOrderItems poItems = null;
                            if(m_PurchaseOrderAndTheirItems.ContainsKey(item.PurchaseOrderNumber))
                            {
                                poItems = m_PurchaseOrderAndTheirItems[item.PurchaseOrderNumber];
                            }
                            else
                            {
                                poItems = new PurchaseOrderItems { Supplier = item.SupplierWrapper, PurchaseOrderNumber = item.PurchaseOrderNumber };
                                m_PurchaseOrderAndTheirItems.Add(item.PurchaseOrderNumber, poItems);
                            }

                            OrderedItem itemToOrder = new OrderedItem { ProductMaterialItem = item, OrderedQuantity = item.Quantity };
                            poItems.OrderedItems.Add(itemToOrder);
                        }
                    }
                }

                return m_PurchaseOrderAndTheirItems.Values.ToList();
            }
        }

        #endregion

        ObservableCollection<ProductMaterial> m_ProductMaterialsWrapper;
        public ObservableCollection<ProductMaterial> ProductMaterialsWrapper
        {
            get
            {
                if (m_ProductMaterialsWrapper == null)
                {
                    m_ProductMaterialsWrapper = new ObservableCollection<ProductMaterial>(this.ProductMaterials);
                    foreach (var material in m_ProductMaterialsWrapper)
                    {
                        material.PropertyChanged += MaterialItem_PropertyChanged;
                    }
                    m_ProductMaterialsWrapper.CollectionChanged += m_ProductMaterialsWrapper_CollectionChanged;
                }
                return m_ProductMaterialsWrapper;
            }
        }

        void m_ProductMaterialsWrapper_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    ProductMaterial newMaterialItem = newItem as ProductMaterial;
                    newMaterialItem.OrderProduct = this;
                    newMaterialItem.PropertyChanged += MaterialItem_PropertyChanged;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var deletedItem in e.OldItems)
                {
                    ProductMaterial deletedMaterial = deletedItem as ProductMaterial;
                    deletedMaterial.OrderProduct = null;
                    deletedMaterial.PropertyChanged -= MaterialItem_PropertyChanged;
                }
            }
            OnPropertyChanged("ProductMaterialsWrapper");
        }

        void MaterialItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ConsumptionCostWrapper")
            {
                CalculateTotalMaterialsCost();
            }
            else if (e.PropertyName == "TotalSubMaterialsPurchaseCostWrapper")
            {
                CalculateTotalPurchaseCost();
            }
            if (e.PropertyName == "HasErrors")
            {
                ValidateProductMaterials();
            }
        }

        decimal m_TotalProductMaterialsCostWrapper = -1.0m;
        private void CalculateTotalMaterialsCost()
        {
            decimal cost = 0;
            foreach (ProductMaterial material in ProductMaterialsWrapper)
            {
                if (material.OrderProduct != null)
                    cost += material.ConsumptionCostWrapper;
            }

            foreach (ProductExtraCost extraCost in ProductExtraCostWrapper)
            {
                cost += extraCost.Cost;
            }

            TotalProductMaterialsCostWrapper = cost * ExpectedQuantity;
        }


        decimal m_TotalProductPurchaseCostWrapper = -1.0m;
        private void CalculateTotalPurchaseCost()
        {
            decimal cost = 0;
            foreach (ProductMaterial material in ProductMaterialsWrapper)
            {
                if (material.OrderProduct != null)
                    cost += material.TotalSubMaterialsPurchaseCostWrapper;
            }
            TotalProductPurchaseCostWrapper = cost;
        }

        #region Data Validation

        public void Validate()
        {
            ValidateProduct();
            ValidateExpectedQuantity();
            ValidateUOM();
            ValidateCurrency();
            ValidateCurrencyValueInINR();
            ValidateCustomerTargetPrice();
            ValidatePerUnitTotalProductMaterialsCost();
        }


        public void ValidateCurrencyValueInINR()
        {
            if (CurrencyValueInINR == 0)
            {
                AddError("CurrencyValueInINR", "Value in INR can't be Zero", false);
            }
            else
            {
                RemoveError("CurrencyValueInINR", "Value in INR can't be Zero");
            }
        }

        private bool ValidatePerUnitTotalProductMaterialsCost()
        {
            //if (DBResources.Instance.CurrentUser.UserRole.CanAddConsumption)
            //{
            //    if (PerUnitTotalProductMaterialsCost > PerUnitOrderValue)
            //    {
            //        AddError("PerUnitTotalProductMaterialsCost", "Consumption cost is higher than order value", false);
            //        return true;
            //    }
            //    else
            //    {
            //        RemoveError("PerUnitTotalProductMaterialsCost", "Consumption cost is higher than order value");
            //        return false;
            //    }
            //}

            return false;
        }

        private void ValidateCustomerTargetPrice()
        {
            if (CustomerTargetPriceWrapper == 0)
            {
                AddError("CustomerTargetPriceWrapper", "Customer can't expect product at free of cost", false);
            }
            else
            {
                RemoveError("CustomerTargetPriceWrapper", "Customer can't expect product at free of cost");
            }
        }

        private void ValidateExpectedQuantity()
        {
            if (ExpectedQuantityWrapper == 0)
            {
                AddError("ExpectedQuantityWrapper", "Quantity can't be Zero", false);
            }
            else
            {
                RemoveError("ExpectedQuantityWrapper", "Quantity can't be Zero");
            }
        }

        private void ValidateProduct()
        {
            if (ProductNameWrapper == null)
            {
                AddError("ProductNameWrapper", "Select Product", false);
            }
            else
            {
                RemoveError("ProductNameWrapper", "Select Product");
            }
        }

        private void ValidateUOM()
        {
            if (UnitsOfMeasurementWrapper == null)
            {
                AddError("UnitsOfMeasurementWrapper", "Select Units", false);
            }
            else
            {
                RemoveError("UnitsOfMeasurementWrapper", "Select Units");
            }
        }


        private void ValidateCurrency()
        {
            if (CurrencyWrapper == null)
            {
                AddError("CurrencyWrapper", "Select Currency", false);
            }
            else
            {
                RemoveError("CurrencyWrapper", "Select Currency");
            }
        }

        #endregion

        #region Execution Dates


        public void RefreshExecutionTabProperties()
        {
            //Ugly code: But this is the only option for the time and money given
            ProductCuttings = null;
            var pcd = ProductCuttingDates;

            ProductProductions = null;
            var ppd = ProductProductionDates;

            ProductQualityChecks = null;
            var pqcd = ProductQualityCheckDates;

            ProductPackagings = null;
            var ppkd = ProductPackagingDates;

            ProductShipments = null;
            var ppsd = ProductShipmentDates;
        }

        #region Cutting Management

            
            private Dictionary<string, List<ProductCutting>> ProductCuttings = null;
            public List<string> ProductCuttingDates
            {
                get
                {
                    if (ProductCuttings == null)
                    {
                        ProductCuttings = new Dictionary<string, List<ProductCutting>>();
                        foreach (ProductBreakUpSummary summary in ProductBreakUpSummaries)
                        {
                            foreach (ProductCutting productCutting in summary.ProductCuttings)
                            {
                                List<ProductCutting> cuttings = null;
                                if(ProductCuttings.ContainsKey(productCutting.Date.ToShortDateString()))
                                {
                                    cuttings = ProductCuttings[productCutting.Date.ToShortDateString()];
                                }
                                else
                                {
                                    cuttings = new List<ProductCutting>();
                                    ProductCuttings.Add(productCutting.Date.ToShortDateString(), cuttings);
                                }

                                if (cuttings != null)
                                    cuttings.Add(productCutting);
                            }
                        }
                    }

                    AddNewCuttingDateRecord(DateTime.Now.ToShortDateString());

                    return ProductCuttings.Keys.Reverse().ToList();
                }
            }

            public List<ProductCutting> GetCuttings(string date)
            {
                if (ProductCuttings.ContainsKey(date))
                {
                    return ProductCuttings[date];
                }

                return null;
            }

            public void AddNewCuttingDateRecord(string date)
            {
                if (!ProductCuttings.ContainsKey(date))
                {
                    List<ProductCutting> cuttings = new List<ProductCutting>();
                    ProductCuttings.Add(date, cuttings);
                    foreach (ProductBreakUpSummary summary in ProductBreakUpSummaries)
                    {
                        ProductCutting newCutting = new ProductCutting() { Date = DateTime.Parse(date) };
                        newCutting.ProductBreakUpSummary = summary;
                        summary.ProductCuttings.Add(newCutting);
                        cuttings.Add(newCutting);
                    }
                }
            }

            public void DiscardTodaysCuttingRecord()
            {
                string todayDate = DateTime.Now.ToShortDateString();

                if (!ProductCuttings.ContainsKey(todayDate))
                {
                    List<ProductCutting> cuttings = ProductCuttings[todayDate];
                    foreach (ProductCutting cutting in cuttings)
                    {
                        if (cutting.CuttingID == 0)
                        {
                            cutting.ProductBreakUpSummary.ProductCuttings.Remove(cutting);
                            cutting.ProductBreakUpSummary = null;
                        }
                    }

                    ProductCuttings.Remove(todayDate);
                }
            }

        #endregion 

        #region Production Management

            private Dictionary<string, List<Production>> ProductProductions = null;
            public List<string> ProductProductionDates
            {
                get
                {
                    if (ProductProductions == null)
                    {
                        ProductProductions = new Dictionary<string, List<Production>>();
                        foreach (ProductBreakUpSummary summary in ProductBreakUpSummaries)
                        {
                            foreach (Production productProduction in summary.Productions)
                            {
                                List<Production> productions = null;
                                if (ProductProductions.ContainsKey(productProduction.Date.ToShortDateString()))
                                {
                                    productions = ProductProductions[productProduction.Date.ToShortDateString()];
                                }
                                else
                                {
                                    productions = new List<Production>();
                                    ProductProductions.Add(productProduction.Date.ToShortDateString(), productions);
                                }

                                if (productions != null)
                                    productions.Add(productProduction);
                            }
                        }
                    }

                    AddNewProductionDateRecord(DateTime.Now.ToShortDateString());
                    return ProductProductions.Keys.Reverse().ToList();
                }
            }

            public List<Production> GetProductions(string date)
            {
                if (ProductProductions.ContainsKey(date))
                {
                    return ProductProductions[date];
                }

                return null;
            }

            public void AddNewProductionDateRecord(string date)
            {
                if (!ProductProductions.ContainsKey(date))
                {
                    List<Production> productions = new List<Production>();
                    ProductProductions.Add(date, productions);
                    foreach (ProductBreakUpSummary summary in ProductBreakUpSummaries)
                    {
                        Production newProduction = new Production() { Date = DateTime.Parse(date) };
                        newProduction.ProductBreakUpSummary = summary;
                        summary.Productions.Add(newProduction);
                        productions.Add(newProduction);
                    }
                }
            }

            #endregion 

        #region Quality Management

            private Dictionary<string, List<Quality>> ProductQualityChecks = null;
            public List<string> ProductQualityCheckDates
            {
                get
                {
                    if (ProductQualityChecks == null)
                    {
                        ProductQualityChecks = new Dictionary<string, List<Quality>>();
                        foreach (ProductBreakUpSummary summary in ProductBreakUpSummaries)
                        {
                            foreach (Quality productQualityCheck in summary.Qualities)
                            {
                                List<Quality> qualityChecks = null;
                                if (ProductQualityChecks.ContainsKey(productQualityCheck.Date.ToShortDateString()))
                                {
                                    qualityChecks = ProductQualityChecks[productQualityCheck.Date.ToShortDateString()];
                                }
                                else
                                {
                                    qualityChecks = new List<Quality>();
                                    ProductQualityChecks.Add(productQualityCheck.Date.ToShortDateString(), qualityChecks);
                                }

                                if (qualityChecks != null)
                                    qualityChecks.Add(productQualityCheck);
                            }
                        }
                    }
                    AddNewQualityCheckDateRecord(DateTime.Now.ToShortDateString());
                    return ProductQualityChecks.Keys.Reverse().ToList();
                }
            }

            public List<Quality> GetQualityChecks(string date)
            {
                if (ProductQualityChecks.ContainsKey(date))
                {
                    return ProductQualityChecks[date];
                }

                return null;
            }

            public void AddNewQualityCheckDateRecord(string date)
            {
                if (!ProductQualityChecks.ContainsKey(date))
                {
                    List<Quality> qualities = new List<Quality>();
                    ProductQualityChecks.Add(date, qualities);
                    foreach (ProductBreakUpSummary summary in ProductBreakUpSummaries)
                    {
                        Quality newQualityCheck = new Quality() { Date = DateTime.Parse(date) };
                        newQualityCheck.ProductBreakUpSummary = summary;
                        summary.Qualities.Add(newQualityCheck);
                        qualities.Add(newQualityCheck);
                    }
                }
            }
         
        #endregion 

        #region Packaging Management

            private Dictionary<string, List<Package>> ProductPackagings = null;
            public List<string> ProductPackagingDates
            {
                get
                {
                    if (ProductPackagings == null)
                    {
                        ProductPackagings = new Dictionary<string, List<Package>>();
                        foreach (ProductBreakUpSummary summary in ProductBreakUpSummaries)
                        {
                            foreach (Package productPackage in summary.Packages)
                            {
                                List<Package> packages = null;
                                if (ProductPackagings.ContainsKey(productPackage.Date.ToShortDateString()))
                                {
                                    packages = ProductPackagings[productPackage.Date.ToShortDateString()];
                                }
                                else
                                {
                                    packages = new List<Package>();
                                    ProductPackagings.Add(productPackage.Date.ToShortDateString(), packages);
                                }

                                if (packages != null)
                                    packages.Add(productPackage);
                            }
                        }
                    }
                    AddNewPackageDateRecord(DateTime.Now.ToShortDateString());
                    return ProductPackagings.Keys.Reverse().ToList();
                }
            }

            public List<Package> GetPackagings(string date)
            {
                if (ProductPackagings.ContainsKey(date))
                {
                    return ProductPackagings[date];
                }

                return null;
            }

            public void AddNewPackageDateRecord(string date)
            {
                if (!ProductPackagings.ContainsKey(date))
                {
                    List<Package> packages = new List<Package>();
                    ProductPackagings.Add(date, packages);
                    foreach (ProductBreakUpSummary summary in ProductBreakUpSummaries)
                    {
                        Package newPackage = new Package() { Date = DateTime.Parse(date) };
                        newPackage.ProductBreakUpSummary = summary;
                        summary.Packages.Add(newPackage);
                        packages.Add(newPackage);
                    }
                }
            }

        #endregion 

        #region Shipping Management

            private Dictionary<string, List<Shipment>> ProductShipments = null;
            public List<string> ProductShipmentDates
            {
                get
                {
                    if (ProductShipments == null)
                    {
                        ProductShipments = new Dictionary<string, List<Shipment>>();
                        foreach (ProductBreakUpSummary summary in ProductBreakUpSummaries)
                        {
                            foreach (Shipment productShipment in summary.Shipments)
                            {
                                List<Shipment> shipments = null;
                                if (ProductShipments.ContainsKey(productShipment.Date.ToShortDateString()))
                                {
                                    shipments = ProductShipments[productShipment.Date.ToShortDateString()];
                                }
                                else
                                {
                                    shipments = new List<Shipment>();
                                    ProductShipments.Add(productShipment.Date.ToShortDateString(), shipments);
                                }

                                if (shipments != null)
                                    shipments.Add(productShipment);
                            }
                        }
                    }
                    AddNewShipmentDateRecord(DateTime.Now.ToShortDateString());
                    return ProductShipments.Keys.Reverse().ToList();
                }
            }

            public List<Shipment> GetShipments(string date)
            {
                if (ProductShipments.ContainsKey(date))
                {
                    return ProductShipments[date];
                }

                return null;
            }

            public void AddNewShipmentDateRecord(string date)
            {
                if (!ProductShipments.ContainsKey(date))
                {
                    List<Shipment> shipments = new List<Shipment>();
                    ProductShipments.Add(date, shipments);
                    foreach (ProductBreakUpSummary summary in ProductBreakUpSummaries)
                    {
                        Shipment newShipment = new Shipment() { Date = DateTime.Parse(date) };
                        newShipment.ProductBreakUpSummary = summary;
                        summary.Shipments.Add(newShipment);
                        shipments.Add(newShipment);
                    }
                }
            }
         

        #endregion 

       

        #endregion

        #region OC Report

            public decimal NumberOfCutPieces
            {
                get
                {
                    decimal totalNumberOfCutPieces = 0;
                    foreach (ProductBreakUpSummary summay in ProductBreakUpSummaries)
                    {
                        foreach (ProductCutting cutting in summay.ProductCuttings)
                        {
                            totalNumberOfCutPieces += cutting.CutQuantity;
                        }
                    }

                    return totalNumberOfCutPieces;
                }
            }

            public decimal NumberOfProducedPieces
            {
                get
                {
                    decimal totalNumberOfProducedPieces = 0;
                    foreach (ProductBreakUpSummary summay in ProductBreakUpSummaries)
                    {
                        foreach (Production production in summay.Productions)
                        {
                            totalNumberOfProducedPieces += production.CompletedQuantity;
                        }
                    }

                    return totalNumberOfProducedPieces;
                }
            }

            public decimal NumberOfQualityPassed
            {
                get
                {
                    decimal totalNumberOfQualityPassed = 0;
                    foreach (ProductBreakUpSummary summay in ProductBreakUpSummaries)
                    {
                        foreach (Quality quality in summay.Qualities)
                        {
                            totalNumberOfQualityPassed += quality.Passed;
                        }
                    }

                    return totalNumberOfQualityPassed;
                }
            }

            public decimal NumberOfShippedPieces
            {
                get
                {
                    decimal totalNumberOfShippedPieces = 0;
                    foreach (ProductBreakUpSummary summay in ProductBreakUpSummaries)
                    {
                        foreach (Shipment shipment in summay.Shipments)
                        {
                            totalNumberOfShippedPieces += shipment.Shipped;
                        }
                    }

                    return totalNumberOfShippedPieces;
                }
            }


        #endregion 
    }
}
