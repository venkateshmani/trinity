using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class ProductMaterialItem : EntityBase
    {
        #region [Wrappers]
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
                CalculateItemCost();
                OnPropertyChanged("CurrencyValueInINR");
            }
        }

        public virtual Company SupplierWrapper
        {
            get
            {
                return Company;
            }
            set
            {
                if (Company != value)
                {
                    Company = value;                   
                }               
            }
        }

        public virtual decimal CostWrapper
        {
            get
            {
                return Cost;
            }
            set
            {
                if (Cost != value)
                {
                    Cost = value;
                    CalculateItemCost();
                }
                ValidateCost();
            }
        }

        public virtual decimal QuantityWrapper
        {
            get
            {
                return Quantity;
            }
            set
            {
                if (Quantity != value)
                {
                    Quantity = value;
                    CalculateItemCost();
                }
                ValidateQuantity();
            }
        }

        decimal m_ItemCostWrapper;
        public virtual decimal ItemCostWrapper
        {
            get { return m_ItemCostWrapper; }
            set
            {
                if (m_ItemCostWrapper != value)
                {
                    m_ItemCostWrapper = value;
                    OnPropertyChanged("ItemCostWrapper");
                }
            }
        }

        public virtual SubMaterial SubMaterialWrapper
        {
            get
            {
                return this.SubMaterial;
            }
            set
            {
                if (!DontLoosePropertyValue)
                {
                    this.SubMaterial = value;
                    OnPropertyChanged("SubMaterialWrapper");
                }
            }
        }

        #endregion [Wrappers]

        #region [Helpers]

        #region Currency Management

        public decimal CurrencyValueInINR
        {
            get
            {
                if (Currency != null && Currency.DefaultValueInINR != null)
                    return Currency.DefaultValueInINR.Value;

                if (CurrencyConversion != null)
                    return CurrencyConversion.ValueInINRForSubMaterialsWrapper;

                return 0;
            }
            set
            {
                CurrencyConversion.ValueInINRForSubMaterialsWrapper = value;
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
                var currencyConversion = ProductMaterial.OrderProduct.Order.OrderCurrencyConversions.Where(c => c.Currency.CurrencyID == Currency.CurrencyID)
                                                                       .Select(c => c);

                OrderCurrencyConversion newConversion = null;
                if (currencyConversion == null || currencyConversion.Count() == 0)
                {
                    newConversion = new OrderCurrencyConversion();
                    newConversion.Currency = Currency;

                    ProductMaterial.OrderProduct.Order.OrderCurrencyConversions.Add(newConversion);
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
            if (e.PropertyName == "ValueInINRForSubMaterialsWrapper")
            {
                OnPropertyChanged("CurrencyValueInINR");
                CalculateItemCost();
            }
        }

        #endregion 

        public void CalculateItemCost()
        {
            ItemCostWrapper = Cost * Quantity * CurrencyValueInINR;
        }
        #endregion [Helpers]

        #region Data Validation

        public void ValidateCost()
        {
            if (Cost == 0)
            {
                AddError("CostWrapper", "Cost can't be Zero", false);
            }
            else
            {
                RemoveError("CostWrapper", "Cost can't be Zero");
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

        private void ValidateQuantity()
        {
            if (QuantityWrapper == 0)
            {
                AddError("QuantityWrapper", "Quantity can't be Zero", false);
            }
            else
            {
                RemoveError("QuantityWrapper", "Quantity can't be Zero");
            }
        }
        #endregion Data Validation
    }
}
