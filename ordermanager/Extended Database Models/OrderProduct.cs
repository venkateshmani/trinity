using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class OrderProduct : EntityBase, INotifyPropertyChanged
    {

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
                }
            }

        #endregion 

        #region Helpers

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
                CalculateOrderValue();
            }

            private void CalculateOrderValue()
            {
                if (Currency != null && CurrencyConversion == null)
                {
                    foreach (OrderCurrencyConversion currencyConversion in Order.OrderCurrencyConversions)
                    {
                        if (currencyConversion.Currency.CurrencyID == Currency.CurrencyID)
                        {
                            CurrencyConversion = currencyConversion;
                        }
                    }
                }

                decimal currencyValueInINR = 0;

                if (CurrencyConversion != null)
                {
                    currencyValueInINR = CurrencyConversion.ValueInINR;
                }

                OrderValueWrapper = ExpectedQuantity * CustomerTargetPrice * currencyValueInINR;
            }

        #endregion 
        
        #region Data Validation

        public void Validate()
        {
            ValidateProduct();
            ValidateExpectedQuantity();
            ValidateUOM();
            ValidateCurrency();
            ValidateCustomerTargetPrice();
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
                AddError("ExpectedQuantityWrapper", "Quantity can't be Zero",false);
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
                AddError("UnitsOfMeasurementWrapper", "Select Units",false);
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
