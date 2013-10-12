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
                OnPropertyChanged("MaterialNameWrapper");
            }
        }

        public virtual decimal CostPerUnitWrapper
        {
            get
            {
                return Cost;
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
                return Consumption;
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
                return ConsumptionCost;
            }
            set
            {
                ConsumptionCost = value;
                OnPropertyChanged("ConsumptionCostWrapper");
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
                m_TotalSubMaterialsPurchaseCostWrapper = value;
                OnPropertyChanged("TotalSubMaterialsPurchaseCostWrapper");
                RemoveError("TotalSubMaterialsPurchaseCostWrapper");
                OrderProduct.RemoveError(MaterialName.Name);
                if (TotalSubMaterialsPurchaseCostWrapper > ConsumptionCostWrapper)
                {
                    AddError("TotalSubMaterialsPurchaseCostWrapper", "Total purchase cost can't be more than consumption cost", false);
                    this.OrderProduct.AddError(MaterialName.Name, MaterialName.Name + " has some errors", false);
                }                
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
        #endregion

        #region Data Validation

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

        public void ValidateConsumtpion()
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

        public void ValidateCostPerUnit()
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

        public void ValidateMaterialName()
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

        public void ValidateUOM()
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

        public void ValidateCurrency()
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

        #region Helpers

        #region Currency Management

            public decimal CurrencyValueInINR
            {
                get
                {
                    if (Currency != null && Currency.DefaultValueInINR != null)
                        return Currency.DefaultValueInINR.Value;

                    if (CurrencyConversion == null)
                        SelectOrAddCurrencyConversion();

                    if(CurrencyConversion != null)
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
                        newConversion.Currency = Currency;

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
            ConsumptionCostWrapper = Cost * Consumption * CurrencyValueInINR;
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
