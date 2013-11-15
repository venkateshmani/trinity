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
        }

        void Supplier_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PurchaseOrderDateWrapper")
            {
                Company comp = sender as Company;
                bool save = false;
                if (comp != null)
                {
                    foreach (OrderProduct product in Order.OrderProducts)
                    {
                        foreach (ProductMaterial material in product.ProductMaterials)
                        {
                            foreach (ProductMaterialItem materialItem in material.ProductMaterialItems)
                            {
                                if (materialItem.Company.CompanyID == comp.CompanyID && materialItem.PurchaseOrder.PurchaseOrderDate != comp.PurchaseOrderDateWrapper)
                                {
                                    materialItem.PurchaseOrder.PurchaseOrderDate = comp.PurchaseOrderDateWrapper;
                                    save = true;
                                }
                            }
                        }
                    }
                    if (save)
                        DBResources.Instance.Save();
                }
            }
        }

        public Order Order
        {
            get;
            set;
        }
    }
}
