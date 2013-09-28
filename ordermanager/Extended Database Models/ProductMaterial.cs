using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class ProductMaterial : EntityBase, INotifyPropertyChanged
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

        #region Helpers

            private void CalculateConsumptionCost()
            {
                decimal currencyValueInINR = 0m;

                var currencyValueInINRDbObj = OrderProduct.Order.OrderCurrencyConversions.Where(c => c.Currency.CurrencyID == Currency.CurrencyID)
                                                                              .Select(c => c.ValueInINR);

                if (currencyValueInINRDbObj != null && currencyValueInINRDbObj.Count() != 0)
                {
                    currencyValueInINR = currencyValueInINRDbObj.First();
                }

                ConsumptionCost = Cost * Consumption * currencyValueInINR;
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
