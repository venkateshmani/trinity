using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public class PurchaseOrderSearchControlViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Company> m_Suppliers = null;

        public PurchaseOrderSearchControlViewModel()
        {
            Suppliers = DBResources.Instance.Suppliers;
        }

        public ObservableCollection<Company> Suppliers
        {
            get
            {
                return m_Suppliers;
            }
            set
            {
                m_Suppliers = value;
                OnPropertyChanged("Suppliers");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
