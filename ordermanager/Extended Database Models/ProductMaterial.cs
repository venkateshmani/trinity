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
                OnPropertyChanged("CurrencyWrapper");
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
        #endregion

        #region Data Validation

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

        private void CalculateConsumptionCost()
        {
            decimal currencyValueInINR = 0m;

            if (Currency != null)
            {
                var currencyValueInINRDbObj = OrderProduct.Order.OrderCurrencyConversions.Where(c => c.Currency.CurrencyID == Currency.CurrencyID)
                                                                              .Select(c => c.ValueInINR);

                if (currencyValueInINRDbObj != null && currencyValueInINRDbObj.Count() != 0)
                {
                    currencyValueInINR = currencyValueInINRDbObj.First();
                }

            }
            ConsumptionCostWrapper = Cost * Consumption * currencyValueInINR;
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
        #endregion
    }
}
