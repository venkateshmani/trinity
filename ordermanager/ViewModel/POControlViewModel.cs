using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ordermanager.DatabaseModel;

namespace ordermanager.ViewModel
{
    public class POControlViewModel
    {
        public POControlViewModel(Order order)
        {
            this.Order = order;
            foreach (PurchaseOrder po in order.PurchaseOrders)
            {
                po.PropertyChanged += po_PropertyChanged;
            }
        }

        void po_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PurchaseOrderDateWrapper")
            {
                DBResources.Instance.Save();
            }
        }

        public Order Order
        {
            get;
            set;
        }
    }
}
