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
            PurchaseItems = new ObservableCollection<ProductMaterialItem>();
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


        private ObservableCollection<ProductMaterialItem> m_PurchaseItems = null;
        public ObservableCollection<ProductMaterialItem> PurchaseItems
        {
            get
            {
                return m_PurchaseItems;
            }
            set
            {
                m_PurchaseItems = value;
            }
        }

        public void SetPurchaseOrder(PurchaseOrder po)
        {
            foreach (var ProductMaterialItem in PurchaseItems)
            {
                ProductMaterialItem.PurchaseOrder = po;
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
                foreach (var item in PurchaseItems)
                {
                    if (!hasErrors)
                        hasErrors = item.HasErrors;
                }

                return hasErrors;
            }
        }
             
    }
}
