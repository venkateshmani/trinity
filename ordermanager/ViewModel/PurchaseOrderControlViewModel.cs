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
        private OrderProduct m_SelectedItem;
        private ObservableCollection<OrderProduct> m_Products;
        private Dictionary<string, ObservableCollection<ProductMaterial>> m_MaterialItems;

        public ObservableCollection<OrderProduct> Products
        {
            get { return m_Products; }
            set
            {
                m_Products = value;
                NotifyPropertyChanged("Products");
            }
        }

        public OrderProduct SelectedItem
        {
            get { return m_SelectedItem; }
            set
            {
                if (value != m_SelectedItem)
                {
                    m_SelectedItem = value;
                    NotifyPropertyChanged("SelectedItem");
                }
            }
        }

        public Dictionary<string, ObservableCollection<ProductMaterial>> MaterialItems
        {
            get { return GroupMaterialByName(); }
        }

        private Dictionary<string, ObservableCollection<ProductMaterial>> GroupMaterialByName()
        {
            Dictionary<string, ObservableCollection<ProductMaterial>> items = new Dictionary<string, ObservableCollection<ProductMaterial>>();
            foreach (MaterialName material in DBResources.Instance.AvailableMaterials)
            {
                ObservableCollection<ProductMaterial> queryItems = new ObservableCollection<ProductMaterial>((from item in SelectedItem.ProductMaterials where item.MaterialName.Name == material.Name select item));
                items.Add(material.Name, queryItems);
            }
            m_MaterialItems = items;
            return m_MaterialItems;
        }

        public bool SetOrder(Order order)
        {
            if (m_Order != order)
            {
                if (order != null)
                {
                    Products = new ObservableCollection<OrderProduct>(order.OrderProducts);
                    m_MaterialItems = new Dictionary<string, ObservableCollection<ProductMaterial>>();
                }
                m_Order = order;
            }
            return true;
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
