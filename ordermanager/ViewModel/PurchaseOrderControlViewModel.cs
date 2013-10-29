using ordermanager.DatabaseModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

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


        private bool m_CanAddNewBreakUpItem = true;
        public bool CanAddNewBreakUpItem
        {
            get
            {
                return m_CanAddNewBreakUpItem;
            }
            set
            {
                m_CanAddNewBreakUpItem = value;
                NotifyPropertyChanged("CanAddNewBreakUpItem");
            }
        }


        private bool m_IsReadOnly = false;
        public bool IsReadOnly
        {
            get
            {
                return m_IsReadOnly;
            }
            set
            {
                m_IsReadOnly = value;
                CanAddNewBreakUpItem = !IsReadOnly;
                NotifyPropertyChanged("IsReadOnly");
            }
        }


        public Order Order
        {
            get { return m_Order; }
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

        public void AddNewBreakUp()
        {
            if (SelectedProduct != null)
                SelectedProduct.ProductBreakUpWrapper.ProductCountryWiseBreakUpWrapper.Add(new ProductCountryWiseBreakUp());
        }

        public Country AddNewCountry(string newCountryName)
        {
            return DBResources.Instance.CreateNewCountry(newCountryName);
        }

        public ProductSize AddNewProductSize(string newProductSize)
        {
            return DBResources.Instance.CreateNewProductSize(newProductSize);
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
                NotifyPropertyChanged("Order");
            }

            if (DBResources.Instance.CurrentUser.UserRole.CanAddSubMaterials &&
                     Order.OrderStatu.OrderStatusID == (short)OrderStatusEnum.OrderConfirmed)
                AddDeleteButtonVisibility = Visibility.Visible;
            else
                AddDeleteButtonVisibility = Visibility.Hidden;

            return true;
        }

        public bool Save(bool isSubmit, string userComment)
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

            if (isSubmit)
            {
                m_Order.OrderStatusID = (short)OrderStatusEnum.SubMaterialsJobCompleted;
                AddDeleteButtonVisibility = Visibility.Hidden;
                foreach (OrderProduct dbProduct in Products)
                {
                    foreach (ProductMaterial dbMaterial in dbProduct.ProductMaterials)
                    {
                        foreach (ProductMaterialItem item in dbMaterial.ProductMaterialItemsWrapper)
                        {
                            item.OnPropertyChanged("IsEditable");
                        }
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
                historyItem.OrderChanges = "Submitted in Material Details Page. Order Status Changed to " + m_Order.OrderStatu.DisplayLabel.ToUpper();
            }
            else
            {
                historyItem.OrderChanges = "Saved Changes in Material Details";
            }

            m_Order.Histories.Add(historyItem);

            #endregion

            return DBResources.Instance.UpdateOrderProducts();
        }

        public bool Validate()
        {
            bool hasErrors = false;
            foreach (OrderProduct dbProduct in Products)
            {

                if (dbProduct.ProductBreakUp != null)
                {
                    dbProduct.ProductBreakUp.RemoveError("ShipmentModeWrapper");
                    if (dbProduct.ProductBreakUp.ShipmentModeWrapper ==null)
                    {                       
                        dbProduct.ProductBreakUp.AddError("ShipmentModeWrapper", "Select Shipment Mode", false);
                        hasErrors = true;
                    }
                }
                foreach (ProductMaterial dbMaterial in dbProduct.ProductMaterials)
                {
                    foreach (ProductMaterialItem item in dbMaterial.ProductMaterialItemsWrapper)
                    {
                        if (!item.Validate())
                            hasErrors = true;
                    }
                }
                foreach (ProductCountryWiseBreakUp breakupItem in dbProduct.ProductBreakUpWrapper.ProductCountryWiseBreakUpWrapper)
                {
                    if (!breakupItem.Validate())
                        hasErrors = true;
                }
            }
            return !hasErrors;
        }

        private Visibility m_AddDeleteButtonVisibility = Visibility.Visible;
        public Visibility AddDeleteButtonVisibility
        {
            get
            {
                return m_AddDeleteButtonVisibility;
            }
            set
            {
                m_AddDeleteButtonVisibility = value;
                NotifyPropertyChanged("AddDeleteButtonVisibility");
            }
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
