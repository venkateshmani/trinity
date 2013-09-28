using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.DatabaseModel;

namespace ordermanager.ViewModel
{
    public class ProductMaterialsViewModel : INotifyPropertyChanged
    {
        private long m_OrderID = -1;
        private OrderProduct m_SelectedItem = null;
        private ObservableCollection<OrderProduct> m_Products = null;

        public ObservableCollection<OrderProduct> Products
        {
            get { return m_Products; }
            set
            {
                m_Products = value;
                NotifyPropertyChanged("Products");
            }
        }

        public OrderProduct SelectedItem
        {
            get { return m_SelectedItem; }
            set
            {
                if (value != m_SelectedItem)
                {
                    m_SelectedItem = value;
                    NotifyPropertyChanged("SelectedItem");
                }
            }
        }

        public bool SetOrderID(long orderID)
        {
            if (m_OrderID != orderID)
            {
                Order order = DBResources.Instance.Context.Orders.Find(orderID);
                if (order != null)
                {
                    Products = new ObservableCollection<OrderProduct>(order.OrderProducts);
                }
                m_OrderID = orderID;
            }
            return true;
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
