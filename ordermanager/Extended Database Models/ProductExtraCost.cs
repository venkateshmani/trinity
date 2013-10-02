using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class ProductExtraCost :EntityBase
    {
        public decimal CostWrapper
        {
            get
            {
                return Cost;
            }
            set
            {
                Cost = value;
                OnPropertyChanged("CostWrapper");
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
                OnPropertyChanged("CurrencyWrapper");
            }
        }

        public string DescriptionWrapper
        {
            get
            {
                return Description;
            }
            set
            {
                Description = value;
                OnPropertyChanged("Description");
            }
        }

        public bool Validate()
        {
            if (Cost != 0 && string.IsNullOrEmpty(Description))
            {
                AddError("DescriptionWrapper", "Enter the reason for the cost", false);
            }
            else
            {
                RemoveError("DescriptionWrapper", "Enter the reason for the cost");
            }

            return HasErrors;
        }
    }
}
