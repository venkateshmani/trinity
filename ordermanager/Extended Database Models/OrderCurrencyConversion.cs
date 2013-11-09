using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class OrderCurrencyConversion : EntityBase
    {
        public decimal ValueInINRWrapper
        {
            get
            {
                if (ValueInINR != null)
                    return ValueInINR.Value;
                return 0;
            }
            set
            {
                ValueInINR = value;
                ValidateValueInINR();
                OnPropertyChanged("ValueInINRWrapper");
            }
        }

        public decimal ValueInINRForMaterialsWrapper
        {
            get
            {
                if (ValueInINRForMaterials != null)
                    return ValueInINRForMaterials.Value;

                return 0;
            }
            set
            {
                ValueInINRForMaterials = value;
                OnPropertyChanged("ValueInINRForMaterialsWrapper");
            }
        }

        public decimal ValueInINRForSubMaterialsWrapper
        {
            get
            {
                if (ValueInINRForSubMaterials != null)
                    return ValueInINRForSubMaterials.Value;

                return 0;
            }
            set
            {
                ValueInINRForSubMaterials = value;
                OnPropertyChanged("ValueInINRForSubMaterialsWrapper");
            }
        }

        public Currency CurrencyWrapper
        {
            get
            {
                return Currency;
            }
            set
            {
                Currency = value;

                if (Currency.DefaultValueInINR != null)
                {
                    ValueInINR = Currency.DefaultValueInINR.Value;
                    ValueInINRForMaterialsWrapper = Currency.DefaultValueInINR.Value;
                    ValueInINRForSubMaterialsWrapper = Currency.DefaultValueInINR.Value;
                }
            }
        }

        #region Validation

        //Add the property validation methods in to this method to ensure validation on create button click
        public void Validate()
        {
            ValidateValueInINR();
        }

        private void ValidateValueInINR()
        {
            decimal defaultValueInINR = 0;

            if (Currency.DefaultValueInINR != null)
            {
                defaultValueInINR = Currency.DefaultValueInINR.Value;
            }

            if (ValueInINRWrapper == 0 && defaultValueInINR == 0)
            {
                AddError("ValueInINRWrapper", "Value in INR can't be Zero", false);
            }
            else
            {
                RemoveError("ValueInINRWrapper", "Value in INR can't be Zero");
            }
        }

        #endregion

    }
}
