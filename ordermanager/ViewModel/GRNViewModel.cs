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
    public class GRNViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<Company> Suppliers
        {
            get 
            {
                return DBResources.Instance.Suppliers;
            }
        }

        private PurchaseOrder m_SelectedPurchaseOrder = null;
        public PurchaseOrder SelectedPurchaseOrder
        {
            get
            {
                return m_SelectedPurchaseOrder;
            }
            set
            {
                m_SelectedPurchaseOrder = value;
                NotifyPropertyChanged("SelectedPurchaseOrder");
            }
        }

        public void Save(bool isSubmit)
        {
            if (!HasErrors)
            {
                if (isSubmit)
                {
                    if (SelectedPurchaseOrder.PurchaseOrderStatusID == 1)
                    {
                        SelectedPurchaseOrder.PurchaseOrderStatusID = 2;
                    }
                    else if (SelectedPurchaseOrder.PurchaseOrderStatusID == 2)
                    {
                        SelectedPurchaseOrder.PurchaseOrderStatusID = 3;
                    }
                }

                DBResources.Instance.Save();
            }
        }

        public bool HasErrors
        {
            get
            {
                SelectedPurchaseOrder.Validate();
                return SelectedPurchaseOrder.HasErrors;
            }
        }

        #region [INotifyPropertyChanged]
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion [INotifyPropertyChanged]
       
    }
}
