using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.DatabaseModel;

namespace ordermanager.ViewModel
{
    public class DataProvider
    {
        public ObservableCollection<UnitsOfMeasurement> UOMs
        {
            get
            {
                return DBResources.Instance.UOMs;
            }
        }

        public ObservableCollection<Currency> Currencies
        {
            get
            {
                return DBResources.Instance.Currencies;
            }
        }

        public ObservableCollection<ProductName> AvailableProducts
        {
            get
            {
                return DBResources.Instance.AvailableProducts;
            }
        }
    }

}
