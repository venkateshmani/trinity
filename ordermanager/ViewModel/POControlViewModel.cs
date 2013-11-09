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

        private ObservableCollection<Company> m_Suppliers = null;
        public ObservableCollection<Company> Suppliers
        {
            get
            {
                if (m_Suppliers == null && Order != null)
                {
                    m_Suppliers = new ObservableCollection<Company>();
                    Company supplier;
                    foreach (OrderProduct product in Order.OrderProducts)
                    {
                        foreach (ProductMaterial material in product.ProductMaterials)
                        {
                            foreach (ProductMaterialItem materialItem in material.ProductMaterialItems)
                            {
                                if (!m_Suppliers.Contains(materialItem.Company))
                                {
                                    supplier = materialItem.Company;
                                    supplier.PurchaseOrderDateWrapper = materialItem.PurchaseOrder.PurchaseOrderDate;
                                    supplier.PropertyChanged -= Supplier_PropertyChanged;
                                    supplier.PropertyChanged += Supplier_PropertyChanged;
                                    m_Suppliers.Add(supplier);
                                }
                            }
                        }
                    }
                }
                return m_Suppliers;
            }
            set
            {
                m_Suppliers = value;
            }
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
