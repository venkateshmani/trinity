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
                    foreach (OrderProduct product in Order.OrderProducts)
                    {
                        foreach (ProductMaterial material in product.ProductMaterials)
                        {
                            foreach (ProductMaterialItem materialItem in material.ProductMaterialItems)
                            {
                                if (!m_Suppliers.Contains(materialItem.Company))
                                {
                                    m_Suppliers.Add(materialItem.Company);
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

        public Order Order
        {
            get;
            set;
        }
    }
}
