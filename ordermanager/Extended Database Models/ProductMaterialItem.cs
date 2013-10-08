using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
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
                if (Currency != value)
                {
                    Currency = value;
                    CalculateItemCost();
                }
                ValidateCurrency();
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
        public void CalculateItemCost()
        {
            decimal currencyValueInINR = 0m;
            if (Currency != null)
            {
                var currencyValueInINRDbObj = ProductMaterial.OrderProduct.Order.OrderCurrencyConversions.Where(c => c.Currency.CurrencyID == Currency.CurrencyID)
                                                                              .Select(c => c.ValueInINR);
                if (currencyValueInINRDbObj != null && currencyValueInINRDbObj.Count() != 0)
                {
                    currencyValueInINR = currencyValueInINRDbObj.First();
                }
            }
            ItemCostWrapper = Cost * Quantity * currencyValueInINR;
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
