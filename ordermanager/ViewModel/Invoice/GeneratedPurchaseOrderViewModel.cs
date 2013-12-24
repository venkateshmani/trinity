using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.Invoice
{
    public class GeneratedPurchaseOrderViewModel : ViewModelBase
    {
        public GeneratedPurchaseOrderViewModel()
        {
            
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
    }
}
