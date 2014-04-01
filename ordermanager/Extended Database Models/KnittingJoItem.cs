using ordermanager.Extended_Database_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class KnittingJoItem : EntityBase
    {

        #region Wrappers

        public decimal TotalAmountWrapper
        {
            get
            {
                return TotalAmount;
            }
            set
            {
                TotalAmount = value;
                OnPropertyChanged("TotalAmountWrapper");
            }
        }

        public string NameOfProductWrapper 
        {
            get
            {
                return NameOfProduct;
            }
            set
            {
                NameOfProduct = value;
                OnPropertyChanged("NameOfProductWrapper");
            }
        }

        public decimal QuantityWrapper
        {
            get
            {
                return Quantity;
            }
            set
            {
                Quantity = value;
                CalculateTotalValueWrapper();
                OnPropertyChanged("QuantityWrapper");
            }
        }

        public decimal RateWrapper
        {
            get
            {
                return Rate;
            }
            set
            {
                Rate = value;
                CalculateTotalValueWrapper();
                OnPropertyChanged("RateWrapper");
            }
        }
        
        public string RemarksWrapper
        {
            get
            {
                return Remarks;
            }
            set
            {
                Remarks = value;
                OnPropertyChanged("RemarksWrapper");
            }
        }

        #endregion 

        private void CalculateTotalValueWrapper()
        {
            TotalAmountWrapper = Quantity * Rate;
        }


        #region Validate

        public bool Validate()
        {
            ValidateNameOfProduct();
            ValidateQuantity();
            ValidateRate();

            return HasErrors;
        }

        private void ValidateNameOfProduct()
        {
            if (string.IsNullOrEmpty(NameOfProductWrapper))
            {
                AddError("NameOfProductWrapper", "Required", false);
            }
            else
            {
                RemoveError("NameOfProduct", "Required");
            }
        }

        private void ValidateQuantity()
        {
            if (QuantityWrapper == 0)
            {
                AddError("QuantityWrapper", "Required", false);
            }
            else
            {
                RemoveError("QuantityWrapper", "Required");
            }
        }

        private void ValidateRate()
        {
            if (RateWrapper == 0)
            {
                AddError("RateWrapper", "Required", false);
            }
            else
            {
                RemoveError("RateWrapper", "Required");
            }
        }

        #endregion
    }
}
