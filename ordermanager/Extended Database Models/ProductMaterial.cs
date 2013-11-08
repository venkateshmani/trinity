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
    public partial class ProductMaterial : EntityBase
    {
        #region Wrapper Properties

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
                CalculateConsumptionCost();

                OnPropertyChanged("CurrencyWrapper");
                OnPropertyChanged("CurrencyValueInINR");
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
                CalculateConsumptionCost();
                ValidateUOM();
                OnPropertyChanged("UnitsOfMeasurementWrapper");
            }
        }

        public virtual MaterialName MaterialNameWrapper
        {
            get
            {
                return MaterialName;
            }
            set
            {
                MaterialName = value;
                ValidateMaterialName();
                OnPropertyChanged("MaterialNameWrapper");
            }
        }

        public virtual decimal CostPerUnitWrapper
        {
            get
            {
                if (Cost != null)
                    return Cost.Value;

                return 0;
            }
            set
            {
                Cost = value;
                ValidateCostPerUnit();
                CalculateConsumptionCost();
                OnPropertyChanged("CostPerUnitWrapper");
            }
        }

        public virtual decimal ConsumptionWrapper
        {
            get
            {
                if (Consumption != null)
                    return Consumption.Value;

                return 0;
            }
            set
            {
                Consumption = value;
                ValidateConsumtpion();
                CalculateConsumptionCost();
                OnPropertyChanged("ConsumptionWrapper");
            }
        }

        public virtual decimal ConsumptionCostWrapper
        {
            get
            {
                if (ConsumptionCost != null)
                    return ConsumptionCost.Value;

                return 0;
            }
            set
            {
                ConsumptionCost = value;
                OnPropertyChanged("ConsumptionCostWrapper");
            }
        }

        public virtual decimal TotalConsumptionCost
        {
            get
            {
                if (this.OrderProduct != null && ConsumptionCost != null)
                    return ConsumptionCost.Value * this.OrderProduct.ExpectedQuantity;

                return 0;
            }
        }

        public decimal OtherCostInINRWrapper
        {
            get
            {
                if (OtherCostInINR != null)
                    return OtherCostInINR.Value;
                return 0;
            }
            set
            {
                if (OtherCostInINR != value)
                {
                    OtherCostInINR = value;
                    CalculateConsumptionCost();
                    OnPropertyChanged("OtherCostInINRWrapper");
                }
            }
        }

        public string OtherCostNameWrapper
        {
            get
            {
                return OtherCostName;
            }
            set
            {
                if (OtherCostName != value)
                {
                    OtherCostName = value;
                    OnPropertyChanged("OtherCostNameWrapper");
                }
            }
        }

        public decimal TotalSubMaterialsPurchaseCostWrapper
        {
            get
            {
                if (m_TotalSubMaterialsPurchaseCostWrapper == -1.0m)
                    CalculateTotalSubMaterialsPurchaseCost();
                return m_TotalSubMaterialsPurchaseCostWrapper;
            }
            set
            {
                if (m_TotalSubMaterialsPurchaseCostWrapper != value)
                {
                    m_TotalSubMaterialsPurchaseCostWrapper = value;
                    OnPropertyChanged("TotalSubMaterialsPurchaseCostWrapper");
                    RemoveError("TotalSubMaterialsPurchaseCostWrapper");
                    OrderProduct.RemoveError(MaterialName.Name);
                    if (TotalSubMaterialsPurchaseCostWrapper > (ConsumptionCostWrapper * ProductQuantity))
                    {
                        AddError("TotalSubMaterialsPurchaseCostWrapper", "Total purchase cost can't be more than consumption cost", false);
                        OrderProduct.AddError(MaterialName.Name, MaterialName.Name + " has some errors", false);
                    }
                }
            }
        }
        #endregion

        #region Data Validation

        public void ValidateCurrencyValueInINR()
        {
            if (DBResources.Instance.CurrentUser.UserRole.CanAddMaterials)
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
        }

        public void ValidateConsumtpion()
        {
            if (DBResources.Instance.CurrentUser.UserRole.CanAddConsumption)
            {
                if (Consumption == 0)
                {
                    AddError("ConsumptionWrapper", "Consumption can't be Zero", false);
                }
                else
                {
                    RemoveError("ConsumptionWrapper", "Consumption can't be Zero");
                }
            }
        }

        public void ValidateCostPerUnit()
        {
            if (DBResources.Instance.CurrentUser.UserRole.CanAddMaterials)
            {
                if (Cost == 0)
                {
                    AddError("CostPerUnitWrapper", "Cost Per Unit can't be Zero", false);
                }
                else
                {
                    RemoveError("CostPerUnitWrapper", "Cost Per Unit can't be Zero");
                }
            }
        }

        public void ValidateMaterialName()
        {
            if (HasUserClickedSaveOrSubmit && DBResources.Instance.CurrentUser.UserRole.CanAddMaterials)
            {
                if (MaterialName == null)
                {
                    AddError("MaterialNameWrapper", "Select Material", false);
                }
                else
                {
                    RemoveError("MaterialNameWrapper", "Select Material");
                }
            }
        }

        public void ValidateUOM()
        {
            if (DBResources.Instance.CurrentUser.UserRole.CanAddConsumption)
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
        }

        public void ValidateCurrency()
        {
            if (DBResources.Instance.CurrentUser.UserRole.CanAddMaterials)
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
        }

        public void ValidateOtherCost()
        {
            RemoveError("OtherCostInINRWrapper");
            RemoveError("OtherCostNameWrapper");
            if (OtherCostInINRWrapper > 0)
            {
                if (string.IsNullOrWhiteSpace(OtherCostNameWrapper))
                    AddError("OtherCostNameWrapper", "Enter other cost name", false);
            }
            if (!string.IsNullOrWhiteSpace(OtherCostNameWrapper))
            {
                if (OtherCostInINRWrapper <= 0)
                    AddError("OtherCostInINRWrapper", "Enter other cost value", false);
            }
        }


        #endregion

        #region Helpers

        private decimal ProductQuantity
        {
            get
            {
                if (this.OrderProduct != null)
                    return this.OrderProduct.ExpectedQuantity;

                return 0;
            }
        }
        
        #region Currency Management

        public decimal CurrencyValueInINR
        {
            get
            {
                if (Currency != null && Currency.DefaultValueInINR != null)
                    return Currency.DefaultValueInINR.Value;

                if (CurrencyConversion == null)
                    SelectOrAddCurrencyConversion();

                if (CurrencyConversion != null)
                    return CurrencyConversion.ValueInINRForMaterialsWrapper;

                return 0;
            }
            set
            {
                if (Currency != null && Currency.DefaultValueInINR == null)
                {
                    CurrencyConversion.ValueInINRForMaterialsWrapper = value;
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
                var currencyConversion = OrderProduct.Order.OrderCurrencyConversions.Where(c => c.Currency.CurrencyID == Currency.CurrencyID)
                                                                       .Select(c => c);

                OrderCurrencyConversion newConversion = null;
                if (currencyConversion == null || currencyConversion.Count() == 0)
                {
                    newConversion = new OrderCurrencyConversion();
                    newConversion.CurrencyWrapper = Currency;

                    OrderProduct.Order.OrderCurrencyConversions.Add(newConversion);
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
            if (e.PropertyName == "ValueInINRForMaterialsWrapper")
            {
                OnPropertyChanged("CurrencyValueInINR");
                CalculateConsumptionCost();
            }
        }

        #endregion

        private void CalculateConsumptionCost()
        {
            decimal uomMultipler = 1;
            if (UnitsOfMeasurement != null)
                uomMultipler = UnitsOfMeasurement.Multiplier;

            ConsumptionCostWrapper = CostPerUnitWrapper * ConsumptionWrapper * CurrencyValueInINR * uomMultipler + OtherCostInINRWrapper;
        }

        decimal m_TotalSubMaterialsPurchaseCostWrapper = -1.0m;
        private void CalculateTotalSubMaterialsPurchaseCost()
        {
            decimal cost = 0;
            foreach (ProductMaterialItem item in ProductMaterialItemsWrapper)
            {
                if (item.ProductMaterial != null)
                    cost += item.ItemCostWrapper;
            }
            TotalSubMaterialsPurchaseCostWrapper = cost;
        }

        ObservableCollection<ProductMaterialItem> m_SubMaterialsWrapper;
        public ObservableCollection<ProductMaterialItem> ProductMaterialItemsWrapper
        {
            get
            {
                if (m_SubMaterialsWrapper == null)
                {
                    m_SubMaterialsWrapper = new ObservableCollection<ProductMaterialItem>(this.ProductMaterialItems);
                    foreach (var subMaterial in m_SubMaterialsWrapper)
                    {
                        subMaterial.PropertyChanged += ProductMaterialItem_PropertyChanged;
                        subMaterial.CalculateItemCost();
                    }
                    m_SubMaterialsWrapper.CollectionChanged += SubMaterialsWrapper_CollectionChanged;
                    CalculateTotalSubMaterialsPurchaseCost();
                }
                return m_SubMaterialsWrapper;
            }
        }

        void SubMaterialsWrapper_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    ProductMaterialItem newMaterialItem = newItem as ProductMaterialItem;
                    newMaterialItem.ProductMaterial = this;
                    newMaterialItem.PropertyChanged += ProductMaterialItem_PropertyChanged;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var deletedItem in e.OldItems)
                {
                    ProductMaterialItem deletedMaterial = deletedItem as ProductMaterialItem;
                    deletedMaterial.ProductMaterial = null;
                    deletedMaterial.PropertyChanged -= ProductMaterialItem_PropertyChanged;
                }
            }
            OnPropertyChanged("ProductMaterialsWrapper");
        }

        private void ProductMaterialItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ItemCostWrapper")
            {
                CalculateTotalSubMaterialsPurchaseCost();
            }
        }

        public override bool DontLoosePropertyValue
        {
            get
            {
                return base.DontLoosePropertyValue;
            }
            set
            {
                base.DontLoosePropertyValue = value;
                foreach (ProductMaterialItem item in this.ProductMaterialItemsWrapper)
                {
                    item.DontLoosePropertyValue = value;
                }
            }
        }
        #endregion
    }
}
