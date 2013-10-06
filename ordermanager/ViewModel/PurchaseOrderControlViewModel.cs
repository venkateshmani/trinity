using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.DatabaseModel;

namespace ordermanager.ViewModel
{
    public class PurchaseOrderControlViewModel : INotifyPropertyChanged
    {
        private Order m_Order = null;
        private OrderProduct m_SelectedProduct;
        private ProductMaterial m_SelectedMaterial;
        private ObservableCollection<OrderProduct> m_Products;

        public ObservableCollection<OrderProduct> Products
        {
            get { return m_Products; }
            set
            {
                m_Products = value;
                NotifyPropertyChanged("Products");
            }
        }

        public OrderProduct SelectedProduct
        {
            get { return m_SelectedProduct; }
            set
            {
                if (value != m_SelectedProduct)
                {                   
                    m_SelectedProduct = value;
                    SelectedMaterial = null;
                    NotifyPropertyChanged("SelectedProduct");
                   
                }
            }
        }              

        public ProductMaterial SelectedMaterial
        {
            get { return m_SelectedMaterial; }
            set
            {
                if (value != m_SelectedMaterial)
                {                   
                    m_SelectedMaterial = value;                  
                    NotifyPropertyChanged("SelectedMaterial");
                }
            }
        }       

        public bool AddNewProductMaterialItem()
        {
            if (m_SelectedMaterial != null)
            {
                m_SelectedMaterial.ProductMaterialItemsWrapper.Add(new ProductMaterialItem());
                return true;
            }
            else
                return false;
        }

        public bool SetOrder(Order order)
        {
            if (m_Order != order)
            {
                if (order != null)
                {
                    Products = new ObservableCollection<OrderProduct>(order.OrderProducts);
                }
                m_Order = order;
            }
            return true;
        }

        public bool Save(bool isSubmit)
        {
            foreach (OrderProduct dbProduct in Products)
            {
                foreach (ProductMaterial dbMaterial in dbProduct.ProductMaterials)
                {
                    foreach (ProductMaterialItem item in dbMaterial.ProductMaterialItemsWrapper)
                    {
                        if (item.ProductMaterialItemID == 0)
                        {
                            dbMaterial.ProductMaterialItems.Add(item);
                        }
                    }
                }
            }
            return DBResources.Instance.UpdateOrderProducts();
        }

        public SubMaterial CreateNewSubMaterial(string subMaterialName)
        {
            return DBResources.Instance.CreateNewSubMaterial(subMaterialName, SelectedMaterial);
        }
       
        #region [INotifyPropertyChanged]
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion [INotifyPropertyChanged]

    }
}
