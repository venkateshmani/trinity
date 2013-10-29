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
                }
                return m_ProductExtraCostWrapper;
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
        /// Per unit value
        /// </summary>
        public decimal OurCostInProductCurrenyValue
        {
            get
            {
                decimal cost = 0;
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
                material.IsValidating = true;
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
                    newConversion.Currency = Currency;

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
            OrderValueWrapper = ExpectedQuantity * CustomerTargetPrice * CurrencyValueInINR;
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

            foreach (ProductExtraCost extraCost in ProductExtraCosts)
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
    }
}
