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

        string m_DeliveryDate = "Delivery Date: ";
        public string DeliveryDate
        {
            get { return m_DeliveryDate; }
            private set
            {
                m_DeliveryDate = value;
                NotifyPropertyChanged("DeliveryDate");
            }
        }

        public ProductMaterial SelectedMaterial
        {
            get { return m_SelectedMaterial; }
            set
            {
                if (value != m_SelectedMaterial)
                {
                    //Fix to avoid loosing values when data context changed
                    if (m_SelectedMaterial != null)
                    {
                        m_SelectedMaterial.DontLoosePropertyValue = true;
                    }

                    m_SelectedMaterial = value;
                    NotifyPropertyChanged("SelectedMaterial");

                    //Take back the fix which is made to avoid loosing values when data context changed

                    if (m_SelectedMaterial != null)
                    {
                        m_SelectedMaterial.DontLoosePropertyValue = false;
                    }
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

        public void AddNewCountry()
        {
            if (SelectedProduct != null)
                SelectedProduct.ProductBreakUpWrapper.ProductCountryWiseBreakUpWrapper.Add(new ProductCountryWiseBreakUp());
        }

        public bool SetOrder(Order order)
        {
            if (m_Order != order)
            {
                if (order != null)
                {
                    Products = new ObservableCollection<OrderProduct>(order.OrderProducts);
                    DeliveryDate = "Delivery Date: " + Convert.ToString(order.ExpectedDeliveryDate);
                }
                m_Order = order;
            }
            return true;
        }

        public bool Save(bool isSubmit,string userComment)
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
                foreach (ProductCountryWiseBreakUp breakupItem in dbProduct.ProductBreakUpWrapper.ProductCountryWiseBreakUpWrapper)
                {
                    if (breakupItem.ProductCountryWiseBreakUpID == 0)
                    {
                        dbProduct.ProductBreakUp.ProductCountryWiseBreakUps.Add(breakupItem);
                    }
                }
            }

            #region History

            History historyItem = new History();
            historyItem.Date = DateTime.Now;
            historyItem.UserName = DBResources.Instance.CurrentUser.UserName;
            historyItem.Comment = userComment;

            if (isSubmit)
            {
                historyItem.OrderChanges = "Submitted in Material Details Page. Order Stauts Changed to " + m_Order.OrderStatu.DisplayLabel.ToUpper();
            }
            else
            {
                historyItem.OrderChanges = "Saved Changes in Material Details";
            }

            m_Order.Histories.Add(historyItem);

            #endregion 

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
