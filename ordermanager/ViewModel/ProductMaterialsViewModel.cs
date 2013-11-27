using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.DatabaseModel;
using System.Data.Entity;
using System.Windows;

namespace ordermanager.ViewModel
{
    public class ProductMaterialsViewModel : INotifyPropertyChanged
    {

        private Order m_Order = null;
        private OrderProduct m_SelectedItem = null;
        private ObservableCollection<OrderProduct> m_Products = null;

        public ObservableCollection<OrderProduct> Products
        {
            get { return m_Products; }
            set
            {
                m_Products = value;
                NotifyPropertyChanged("Products");
                if (m_Products.Count > 0)
                    NotifyPropertyChanged("SelectedIndex");
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

        public int SelectedIndex
        {
            get { return 0; }
        }

        public Order Order
        {
            get
            {
                return m_Order;
            }
        }

        public bool SetOrder(Order order)
        {
            if (m_Order != order)
            {
                m_Products = new ObservableCollection<OrderProduct>(order.OrderProducts);
                m_Order = order;
                NotifyPropertyChanged("Products");
                if (m_Products.Count > 0)
                    NotifyPropertyChanged("SelectedIndex");
                NotifyPropertyChanged("Order");
                SetAccess();
            }
            return true;
        }

        public void Refresh()
        {
            SetAccess();
        }

        private void SetAccess()
        {
            ActionButtonsVisibility = Visibility.Collapsed;
            NewItemAddBtnVisibility = Visibility.Collapsed;
            AddNewExtraCostItemBtnVisibility = Visibility.Collapsed;
            CanEditMaterials = false;
            CanEditMaterialsCost = false;
            //CanEditConsumption = false;
            //CanEditExtraCost = false;
            if (m_Order != null)
            {
                UserRole cuRole = DBResources.Instance.CurrentUser.UserRole;
                OrderStatu coStatus = Order.OrderStatu;
                if (cuRole.CanAddMaterials || cuRole.CanAddConsumption)
                {
                    if (coStatus.OrderStatusID == (short)OrderStatusEnum.EnquiryCreated || Order.OrderStatu.OrderStatusID == (short)OrderStatusEnum.EnquiryRejected)
                    {
                        ActionButtonsVisibility = Visibility.Visible;
                        NewItemAddBtnVisibility = Visibility.Visible;
                        CanEditMaterials = true;
                        //CanEditExtraCost = true;
                    }
                }
                if (cuRole.CanAddMaterialsCost && coStatus.OrderStatusID == (short)OrderStatusEnum.MaterialsAdded)
                {
                    ActionButtonsVisibility = Visibility.Visible;
                    NewItemAddBtnVisibility = Visibility.Hidden;
                    AddNewExtraCostItemBtnVisibility = Visibility.Visible;
                    CanEditMaterialsCost = true;
                }
            }
        }

        public MaterialName CreateNewMaterial(string materialName)
        {
            return DBResources.Instance.CreateNewMaterial(materialName);
        }

        public ProductExtraCostType CreateNewExtraCostType(string extraCostName)
        {
            return DBResources.Instance.CreateNewExtraCostType(extraCostName);
        }

        public bool AddNewMaterialItem()
        {
            if (m_SelectedItem != null)
            {
                m_SelectedItem.ProductMaterialsWrapper.Add(new ProductMaterial());
                return true;
            }
            else
                return false;
        }

        public bool AddNewExtraCostItem()
        {
            if (m_SelectedItem != null)
            {
                m_SelectedItem.ProductExtraCostWrapper.Add(new ProductExtraCost());
                return true;
            }
            else
                return false;
        }

        private Visibility m_ActionButtonsVisibility = Visibility.Hidden;
        public Visibility ActionButtonsVisibility
        {
            get
            {
                return m_ActionButtonsVisibility;
            }
            set
            {
                if (m_ActionButtonsVisibility != value)
                {
                    m_ActionButtonsVisibility = value;
                    NotifyPropertyChanged("ActionButtonsVisibility");
                }
            }
        }

        private Visibility m_NewItemAddBtnVisibility = Visibility.Hidden;
        public Visibility NewItemAddBtnVisibility
        {
            get
            {
                return m_NewItemAddBtnVisibility;
            }
            set
            {
                if (m_NewItemAddBtnVisibility != value)
                {
                    m_NewItemAddBtnVisibility = value;
                    NotifyPropertyChanged("NewItemAddBtnVisibility");
                }
            }
        }

        private Visibility m_AddNewExtraCostItemBtnVisibility = Visibility.Hidden;
        public Visibility AddNewExtraCostItemBtnVisibility
        {
            get
            {
                return m_AddNewExtraCostItemBtnVisibility;
            }
            set
            {
                if (m_AddNewExtraCostItemBtnVisibility != value)
                {
                    m_AddNewExtraCostItemBtnVisibility = value;
                    NotifyPropertyChanged("AddNewExtraCostItemBtnVisibility");
                }
            }
        }

        private bool m_CanEditMaterials;
        public bool CanEditMaterials
        {
            get { return m_CanEditMaterials; }
            set
            {
                if (m_CanEditMaterials != value)
                {
                    m_CanEditMaterials = value;
                    NotifyPropertyChanged("CanEditMaterials");
                }
            }
        }

        private bool m_CanEditMaterialsCost;
        public bool CanEditMaterialsCost
        {
            get { return m_CanEditMaterialsCost; }
            set
            {
                if (m_CanEditMaterialsCost != value)
                {
                    m_CanEditMaterialsCost = value;
                    NotifyPropertyChanged("CanEditMaterialsCost");
                }
            }
        }

        //private bool m_CanEditConsumption;
        //public bool CanEditConsumption
        //{
        //    get { return m_CanEditConsumption; }
        //    set
        //    {
        //        if (m_CanEditConsumption != value)
        //        {
        //            m_CanEditConsumption = value;
        //            NotifyPropertyChanged("CanEditConsumption");
        //        }
        //    }
        //}

        //private bool m_CanEditExtraCost;
        //public bool CanEditExtraCost
        //{
        //    get { return m_CanEditExtraCost; }
        //    set
        //    {
        //        if (m_CanEditExtraCost != value)
        //        {
        //            m_CanEditExtraCost = value;
        //            NotifyPropertyChanged("CanEditExtraCost");
        //        }
        //    }
        //}

        public bool HasUserClickedSaveOrSubmit
        {
            get
            {
                return Order.HasUserClickedSaveOrSubmit;
            }
            set
            {
                Order.HasUserClickedSaveOrSubmit = value;
            }
        }

        public bool Save(bool isSubmit, string userComment)
        {
            //Just to make sure
            Order.HasUserClickedSaveOrSubmit = true;

            if (!HasError)
            {
                if (isSubmit)
                {
                    if (Order.OrderStatusID == (short)OrderStatusEnum.EnquiryCreated || Order.OrderStatusID == (short)OrderStatusEnum.EnquiryRejected)
                        Order.OrderStatusID = (short)OrderStatusEnum.MaterialsAdded;
                    else if (Order.OrderStatusID == (short)OrderStatusEnum.MaterialsAdded)
                        Order.OrderStatusID = (short)OrderStatusEnum.MaterialsCostAdded;
                    //else if (Order.OrderStatusID == (short)OrderStatusEnum.MaterialsCostAdded)
                    //    Order.OrderStatusID = (short)OrderStatusEnum.MaterialsJobCompleted;
                }

                foreach (OrderProduct dbProduct in Products)
                {
                    foreach (ProductMaterial material in dbProduct.ProductMaterialsWrapper)
                    {
                        if (material.MaterialID == 0)
                        {
                            dbProduct.ProductMaterials.Add(material);
                        }
                    }
                    foreach (ProductExtraCost extraCost in dbProduct.ProductExtraCostWrapper)
                    {
                        if (extraCost.ProductID == 0)
                        {
                            dbProduct.ProductExtraCosts.Add(extraCost);
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
                    historyItem.OrderChanges = "Submitted in Materials Page. Order Stautus changed to " + Order.OrderStatu.DisplayLabel.ToUpper();
                }
                else
                {
                    historyItem.OrderChanges = "Saved changes in Materials";
                }

                Order.Histories.Add(historyItem);

                #endregion

                Order.LastModifiedDate = DBResources.Instance.GetServerTime();
                if (DBResources.Instance.UpdateOrderProducts())
                {
                    if (isSubmit)
                        ActionButtonsVisibility = Visibility.Hidden;
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool HasError
        {
            get
            {
                bool hasError = false;
                foreach (OrderProduct product in Products)
                {
                    if (product.ValidateProductMaterials())
                        hasError = true;
                }
                return hasError;
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
