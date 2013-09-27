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
    public class NewEnquiryViewModel : INotifyPropertyChanged
    {

        public NewEnquiryViewModel()
        {
            
        }

        private Order m_Order = null;
        public Order Order
        {
            get
            {
                if (m_Order == null)
                {
                    m_Order = DBResources.CreateNewOrder();
                }
                return m_Order;
            }
            set
            {
                m_Order = value;
            }
        }


        private ObservableCollection<OrderProduct> m_OrderProducts = null;
        public ObservableCollection<OrderProduct> OrderProducts
        {
            get
            {
                if (m_OrderProducts == null)
                {
                    m_OrderProducts = new ObservableCollection<OrderProduct>(Order.OrderProducts);
                    m_OrderProducts.CollectionChanged += m_OrderProducts_CollectionChanged;
                }

                return m_OrderProducts;
            }
        }

        void m_OrderProducts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    OrderProduct newProduct = newItem as OrderProduct;
                    newProduct.Order = Order;
                }
            }
        }

        private ObservableCollection<OrderCurrencyConversion> m_OrderCurrencyConversions = null;
        public ObservableCollection<OrderCurrencyConversion> OrderCurrencyConversions
        {
            get
            {
                if (m_OrderCurrencyConversions == null)
                {
                    m_OrderCurrencyConversions = new ObservableCollection<OrderCurrencyConversion>(Order.OrderCurrencyConversions);
                }

                return m_OrderCurrencyConversions;
            }
            private set
            {
                m_OrderCurrencyConversions = value;
                OnPropertyChanged("OrderCurrencyConversions");
            }
        }
        

        public void RefreshCurrencyConversionTable()
        {
            //Handle Deletion of a Currency
            for (int i = 0 ; i < OrderCurrencyConversions.Count;)
            {
                OrderCurrencyConversion currencyConversion = OrderCurrencyConversions[i];

                var products = OrderProducts.Where(p => p.Currency != null && p.Currency.CurrencyID == currencyConversion.Currency.CurrencyID)
                                                                           .Select(p => p);

                if (products == null || products.Count() == 0)
                {
                    OrderCurrencyConversions.Remove(currencyConversion);
                    Order.OrderCurrencyConversions.Remove(currencyConversion);
                    continue;
                }

                i = i + 1;
            }

            OrderCurrencyConversions = new ObservableCollection<OrderCurrencyConversion>(Order.OrderCurrencyConversions);
        }

        public ProductName CreateNewProduct(string newProductName)
        {
            return DBResources.CreateNewProduct(newProductName);
        }

        public Order CreateNewOrder()
        {
            foreach (OrderProduct product in OrderProducts)
            {
                if (product.ProductID == 0)
                {
                    Order.OrderProducts.Add(product);
                }
            }

            Order.OrderStatusID = 1;
            Order.LastModifiedDate = DateTime.Now;
            return DBResources.CreateNewOrder(Order);
        }

        public DBResources DBResources
        {
            get
            {
                return DBResources.Instance;
            }
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
