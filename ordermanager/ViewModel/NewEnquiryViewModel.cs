using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public class NewEnquiryViewModel
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
                }

                return m_OrderProducts;
            }
        }
        

        public void CreateNewOrder()
        {
            DBResources.CreateNewOrder(Order);
        }

        public DBResources DBResources
        {
            get
            {
                return DBResources.Instance;
            }
        }
    }
}
