using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.PurchaseOrderControl
{
    public class BillOfMaterialBrowserViewModel : ViewModelBase
    {
        public BillOfMaterialBrowserViewModel(Order order, ObservableCollection<ProductMaterialItem> purchasableMaterials)
        {
            this.SetOrder(order);
            this.PurchasableMaterials = purchasableMaterials;
        }

        private ObservableCollection<ProductMaterialItem> m_PurchasableMaterials = null;
        public ObservableCollection<ProductMaterialItem> PurchasableMaterials
        {
            get
            {
                return m_PurchasableMaterials;
            }
            private set
            {
                m_PurchasableMaterials = value;
            }
        }

    }
}
