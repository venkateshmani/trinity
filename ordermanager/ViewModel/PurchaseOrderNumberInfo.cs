using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public class PurchaseOrderNumberInfo
    {
        public string PurchaseOrderNumber
        {
            get;
            set;
        }

        public Company Supplier
        {
            get;
            set;
        }

        public PurchaseOrderNumberInfo(Company supplier, Order order)
        {
            Supplier = supplier;
            PurchaseOrderNumber = Constants.GetPurchaseOrderNumber(supplier, order);
        }
    }
}
