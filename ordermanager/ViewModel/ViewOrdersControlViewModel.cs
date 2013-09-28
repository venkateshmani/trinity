using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public class ViewOrdersControlViewModel
    {
        ObservableCollection<Order> m_Orders;
        public ViewOrdersControlViewModel()
        {
            m_Orders = new ObservableCollection<Order>(DBResources.Instance.Context.Orders.ToList());
        }

       public ObservableCollection<Order> Orders
        {
            get { return m_Orders; }
            set { m_Orders = value; }
        }
    }
}
