using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class Company : EntityBase
    {
        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                AddError("Name", "Enter name", false);
            else
                RemoveError("Name");
            if (string.IsNullOrWhiteSpace(City))
                AddError("City", "Enter City name", false);
            else
                RemoveError("City");

            if (string.IsNullOrWhiteSpace(City))
                AddError("Country", "Enter Country name", false);
            else
                RemoveError("Country");

            if (string.IsNullOrWhiteSpace(Telephone) && string.IsNullOrWhiteSpace(Mobile))
                AddError("Telephone", "Enter Work Telephone or Mobile details", false);
            else
                RemoveError("Telephone");

            if (CompanyType.Type == "Supplier")
            {
                if (string.IsNullOrWhiteSpace(TIN))
                    AddError("TIN", "Enter TIN number", false);
                else
                    RemoveError("TIN");

                if (string.IsNullOrWhiteSpace(CST))
                    AddError("CST", "Enter CST number", false);
                else
                    RemoveError("CST");
            }
            return !HasErrors;
        }
    }
}
