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

        public NewEnquiryViewModel(Order order)
        {
            Order = order;
        }

        private bool m_CanAddNewItem = true;
        public bool CanAddNewItem
        {
            get
            {
                return m_CanAddNewItem;
            }
            set
            {
                m_CanAddNewItem = value;
                OnPropertyChanged("CanAddNewItem");
            }
        }

        public bool CanUpdateEnquiry
        {
            get
            {
                if (Order.OrderStatusID == (short)OrderStatusEnum.EnquiryApproved &&
                    DBResources.Instance.CurrentUser.UserRole.CanConfirmOrder)
                    return true;
                else if (Order.OrderStatusID == 0 &&
                    DBResources.Instance.CurrentUser.UserRole.CanCreateNewEnquiry)
                    return true;

                return false;
            }
        }

        private bool m_IsReadOnly = false;
        public bool IsReadOnly
        {
            get
            {
                return m_IsReadOnly;
            }
            set
            {
                m_IsReadOnly = value;
                CanAddNewItem = !IsReadOnly;
                OnPropertyChanged("IsReadOnly");
            }
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

        
        public decimal TotalOrderValue
        {
            get
            {
                if(Order.TotalOrderValue != null)
                    return Order.TotalOrderValue.Value;

                return 0;
            }
            set
            {
                Order.TotalOrderValue = value;
                OnPropertyChanged("TotalOrderValue");
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
                    newProduct.PropertyChanged += Order_PropertyChanged;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var deletedItem in e.OldItems)
                {
                    OrderProduct deletedProduct = deletedItem as OrderProduct;
                    deletedProduct.Order = null;
                    deletedProduct.PropertyChanged -= Order_PropertyChanged;
                }
            }
        }

        void Order_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "OrderValueWrapper")
            {
                CalculateTotalOrderValue();
            }
        }

        private void CalculateTotalOrderValue()
        {
            decimal totalOrderValue = 0;
            foreach (OrderProduct product in OrderProducts)
            {
                totalOrderValue += product.OrderValue;
            }

            TotalOrderValue = totalOrderValue;
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

        public ProductName CreateNewProduct(string newProductName, string styleId)
        {
            return DBResources.CreateNewProduct(newProductName, styleId);
        }

        public Order CreateNewOrder(string userComment)
        {
            if (!HasErrors)
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
                CanAddNewItem = false;
                return DBResources.CreateNewOrder(Order, userComment);
            }

            return null;
        }

        public bool UpdateOrderStatus(string userActionVerb, string userComment,OrderStatusEnum newStatus)
        {
            if (newStatus != OrderStatusEnum.None && Order != null)
            {
                Order.OrderStatusID = (short)newStatus;
                Order.LastModifiedDate = DateTime.Now;

                #region History

                History historyItem = new History();
                historyItem.Date = DateTime.Now;
                historyItem.UserName = DBResources.Instance.CurrentUser.UserName;
                historyItem.Comment = userComment;
                historyItem.OrderChanges = "has " + userActionVerb;

                Order.Histories.Add(historyItem);

                #endregion 

                return DBResources.Save();
            }
            return false;
        }

        public DBResources DBResources
        {
            get
            {
                return DBResources.Instance;
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
                Order.Validate();
                if (Order.HasErrors)
                    hasErrors = true;

                foreach (OrderCurrencyConversion currencyConversion in OrderCurrencyConversions)
                {
                    currencyConversion.Validate();
                    if (currencyConversion.HasErrors)
                        hasErrors = true;
                }
                
                foreach (OrderProduct product in OrderProducts)
                {
                    product.Validate();
                    if (product.HasErrors)
                        hasErrors = true;
                }

                return hasErrors;
            }
        }
    }
}
