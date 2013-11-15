using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Extended_Database_Models
{
    public class PurchaseOrderItems : INotifyPropertyChanged
    {

        public PurchaseOrderItems()
        {
            OrderedItems = new ObservableCollection<OrderedItem>();
            PurchaseOrderNumber = string.Empty;
            Supplier = null;
        }
    
        public string PurchaseOrderNumber
        {
            get;
            set;
        }

        public Company Supplier
        {
            get;
            set;
        }


        private ObservableCollection<OrderedItem> m_OrderedItem = null;
        public ObservableCollection<OrderedItem> OrderedItems
        {
            get
            {
                return m_OrderedItem;
            }
            set
            {
                m_OrderedItem = value;
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

        public bool HasErrors
        {
            get
            {
                bool hasErrors = false;
                foreach (var item in OrderedItems)
                {
                    if (!hasErrors)
                        hasErrors = item.HasErrors;
                }

                return hasErrors;
            }
        }
             
    }
}
