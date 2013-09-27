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
        private ObservableCollection<OrderProduct> m_Products;
        private OrderProduct m_SelectedProduct;

        public ProductMaterialsViewModel(long orderId)
        {
            OrderManagerDBEntities dbContext = new OrderManagerDBEntities();
            Order order = dbContext.Orders.Find(orderId);
            if (order != null)
            {
                m_Products = new ObservableCollection<OrderProduct>(order.OrderProducts);
            }
        }

        public ObservableCollection<OrderProduct> Products
        {
            get { return m_Products; }
            set
            {
                m_Products = value;
                NotifyPropertyChanged("Products");
            }
        }

        public OrderProduct SelectedProduct
        {
            get;
            set;
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
