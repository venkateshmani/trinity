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
    public class ProductMaterialsViewModel : INotifyPropertyChanged
    {
        private Order m_Order = null;
        private OrderProduct m_SelectedItem = null;
        private ObservableCollection<OrderProduct> m_Products = null;
        ObservableCollection<ProductMaterial> m_SelectedProductMaterials;

        public ObservableCollection<OrderProduct> Products
        {
            get { return m_Products; }
            set
            {
                m_Products = value;
                NotifyPropertyChanged("Products");
            }
        }

        public ObservableCollection<ProductMaterial> ProductMaterialsList
        {
            get
            {
                if (m_SelectedItem!=null && m_SelectedProductMaterials == null)
                {
                    m_SelectedProductMaterials = new ObservableCollection<ProductMaterial>(m_SelectedItem.ProductMaterials);
                    m_SelectedProductMaterials.CollectionChanged += m_SelectedProductMaterials_CollectionChanged;
                }
                return m_SelectedProductMaterials;
            }
        }

        public OrderProduct SelectedProduct
        {
            get { return m_SelectedItem; }
            set
            {
                if (value != m_SelectedItem)
                {
                    m_SelectedItem = value;
                    NotifyPropertyChanged("SelectedProduct");
                }
            }
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

        public MaterialName CreateNewMaterial(string materialName)
        {
            return DBResources.Instance.CreateNewMaterial(materialName);
        }
        
        void m_SelectedProductMaterials_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (m_SelectedItem != null)
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    foreach (var newItem in e.NewItems)
                    {
                        ProductMaterial newMaterialItem = newItem as ProductMaterial;
                        newMaterialItem.OrderProduct = m_SelectedItem;
                    }
                }
                else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    foreach (var deletedItem in e.OldItems)
                    {
                        ProductMaterial deletedMaterial = deletedItem as ProductMaterial;
                        deletedMaterial.OrderProduct = null;
                    }
                }
                NotifyPropertyChanged("ProductMaterialsList");
            }
            
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
