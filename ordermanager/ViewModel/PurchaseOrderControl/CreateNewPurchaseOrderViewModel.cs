using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.PurchaseOrderControl
{
    public class CreateNewPurchaseOrderViewModel
    {
        public CreateNewPurchaseOrderViewModel(Order order)
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

        private PurchaseOrder m_PurchaseOrder = null;
        private PurchaseOrder PurchaseOrder
        {
            get
            {
                if (m_PurchaseOrder == null)
                {
                    m_PurchaseOrder = new PurchaseOrder();
                }
                return m_PurchaseOrder;
            }
        }

    }
}
