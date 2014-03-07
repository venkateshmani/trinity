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

        public PurchaseOrderSearchControlViewModel(Order order)
        {
            Order = order;
        }

        private Order m_Order = null;
        public Order Order
        {
            get
            {
                return m_Order;
            }
            set
            {
                m_Order = value;
            }
        }

        private ObservableCollection<PurchaseOrder> m_PurchaseOrders = null;
        public ObservableCollection<PurchaseOrder> PurchaseOrders
        {
            get
            {
                if (m_PurchaseOrders == null)
                {
                    m_PurchaseOrders = new ObservableCollection<PurchaseOrder>();
                    foreach (var po in Order.PurchaseOrders)
                    {
                        if (po.Approval != null && po.Approval.IsApproved != null && po.Approval.IsApproved.Value)
                            m_PurchaseOrders.Add(po);
                    }
                }

                return m_PurchaseOrders;
            }
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
