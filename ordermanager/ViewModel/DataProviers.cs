using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.DatabaseModel;
using System.ComponentModel;

namespace ordermanager.ViewModel
{
    public class DataProvider : INotifyPropertyChanged
    {
        public DataProvider()
        {
            DBResources.Instance.PropertyChanged += Instance_PropertyChanged;
        }

        ~DataProvider()
        {
            DBResources.Instance.PropertyChanged -= Instance_PropertyChanged;
        }


        void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }

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

        public ObservableCollection<UserRole> UserRoles
        {
            get
            {
                return DBResources.Instance.UserRoles;
            }
        }

        public ObservableCollection<ShipmentMode> ShipmentModes
        {
            get
            {
                return DBResources.Instance.ShipmentModes;
            }
        }

        public ObservableCollection<ProductName> AvailableProducts
        {
            get
            {
                return DBResources.Instance.AvailableProducts;
            }
        }

        public ObservableCollection<MaterialName> AvailableMaterials
        {
            get
            {
                return DBResources.Instance.AvailableMaterials;
            }
        }

        private Dictionary<string, ObservableCollection<SubMaterial>> AvailableSubMaterials
        {
            get
            {
                return DBResources.Instance.AvailableSubMaterials;
            }
        }

        public ObservableCollection<SubMaterial> GetAvailableSubMaterials(string materialName)
        {
            if (AvailableSubMaterials.ContainsKey(materialName))
                return AvailableSubMaterials[materialName];
            else
                return null;
        }

        public ObservableCollection<Company> Companies
        {
            get
            {
                return DBResources.Instance.Companies;
            }
        }

        public ObservableCollection<Company> Suppliers
        {
            get
            {
                return DBResources.Instance.Suppliers;
            }
        }

        public ObservableCollection<Country> Countries
        {
            get
            {
                return DBResources.Instance.Countries;
            }
        }

        public ObservableCollection<ProductSize> ProductSizes
        {
            get
            {
                return DBResources.Instance.ProductSizes;
            }
        }

        public User LoggedInUser
        {
            get
            {
                return DBResources.Instance.CurrentUser;
            }
        }

        public ObservableCollection<ProductExtraCostType> AvailableExtraCostTypes
        {
            get { return DBResources.Instance.AvailableExtraCostTypes; }
        }

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
