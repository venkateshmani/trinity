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
        public ViewOrdersControlViewModel(bool showAllOrders)
        {
            if (showAllOrders)
                Orders = new ObservableCollection<Order>(DBResources.Instance.Context.Orders.ToList());
            else
            {
                UserRole currentUserRole = DBResources.Instance.CurrentUser.UserRole;
                var orderStatusesOwnedByCurrentUser = from OrderStatus in DBResources.Instance.Context.OrderStatus
                                                      where OrderStatus.UserRole.UserRoleID == currentUserRole.UserRoleID
                                                      select OrderStatus.OrderStatusID;

                m_Orders = new ObservableCollection<Order>();
                foreach (Order order in DBResources.Instance.Context.Orders)
                {
                    if(orderStatusesOwnedByCurrentUser.Contains(order.OrderStatu.OrderStatusID))
                        m_Orders.Add(order);
                }
                
            }

        }

       public ObservableCollection<Order> Orders
        {
            get { return m_Orders; }
            set { m_Orders = value; }
        }
    }
}
