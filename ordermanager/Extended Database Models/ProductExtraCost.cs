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
                Validate();
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
                Validate();
                OnPropertyChanged("DescriptionWrapper");
            }
        }

        public bool Validate()
        {
            if (HasUserClickedSaveOrSubmit)
            {
                //if (Cost != 0 && string.IsNullOrEmpty(Description))
                //{
                //    AddError("DescriptionWrapper", "Enter the reason for the cost", false);
                //}
                //else
                //{
                //    RemoveError("DescriptionWrapper", "Enter the reason for the cost");
                //}
                if (ProductExtraCostType.ExtraCostTypeID == 0)
                    AddError("ProductExtraCostType", "Select", false);
                else
                    RemoveError("ProductExtraCostType");
                if (Cost == 0)
                    AddError("Cost", "Add cost", false);
                else
                    RemoveError("Cost");
            }

            return !HasErrors;
        }
    }
}
