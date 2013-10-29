using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class ProductCountryWiseBreakUp : EntityBase
    {

        public decimal NumberOfPiecesWrapper
        {
            get
            {
                return NumberOfPieces;
            }
            set
            {
                NumberOfPieces = value;
                ValidateNumberOfPiecesWrapper();
                OnPropertyChanged("NumberOfPiecesWrapper");
            }
        }

        public Country CountryWrapper
        {
            get
            {
                return Country;
            }
            set
            {
                Country = value;
                OnPropertyChanged("CountryWrapper");
            }
        }

        public ProductSize ProductSizeWrapper
        {
            get
            {
                return ProductSize;
            }
            set
            {
                ProductSize = value;
                OnPropertyChanged("ProductSizeWrapper");
            }
        }

        #region Validation


        public void ValidateNumberOfPiecesWrapper()
        {
            if (NumberOfPiecesWrapper == 0)
            {
                AddError("NumberOfPiecesWrapper", "Can't be Zero", false);
            }
            else
            {
                RemoveError("NumberOfPiecesWrapper", "Can't be Zero");
            }
        }

        public void ValidateCountryWrapper()
        {
            if (CountryWrapper == null)
            {
                AddError("CountryWrapper", "Select Country", false);
            }
            else
            {
                RemoveError("CountryWrapper", "Select Country");
            }
        }

        public void ValidateProductSizeWrapper()
        {
            if (ProductSizeWrapper == null)
            {
                AddError("ProductSizeWrapper", "Select Size", false);
            }
            else
            {
                RemoveError("ProductSizeWrapper", "Select Size");
            }
        }
       
        public bool Validate()
        {
            ValidateNumberOfPiecesWrapper();
            ValidateCountryWrapper();
            ValidateProductSizeWrapper();

            return !HasErrors;
        }

        #endregion Validate
    }
}
