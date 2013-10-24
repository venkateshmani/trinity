﻿using System;
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
                if (order != null)
                {
                    Products = new ObservableCollection<OrderProduct>(order.OrderProducts);
                }
                m_Order = order;


                UserRole cuRole = DBResources.Instance.CurrentUser.UserRole;
                OrderStatu coStatus = Order.OrderStatu;

                if ((cuRole.CanAddMaterials && coStatus.OrderStatusID == (short)OrderStatusEnum.EnquiryCreated) ||
                     (cuRole.CanAddMaterialsCost && coStatus.OrderStatusID == (short)OrderStatusEnum.MaterialsAdded) ||
                     (cuRole.CanAddConsumption && coStatus.OrderStatusID == (short)OrderStatusEnum.MaterialsCostAdded))
                {
                    ActionButtonsVisibility = Visibility.Visible;
                }
                else
                {
                    ActionButtonsVisibility = Visibility.Hidden;
                }

              
                if ((Order.OrderStatu.OrderStatusID == (short)OrderStatusEnum.EnquiryCreated ||
                    Order.OrderStatu.OrderStatusID == (short)OrderStatusEnum.EnquiryRejected) &&
                    DBResources.Instance.CurrentUser.UserRole.CanAddMaterials == true)
                {
                    NewItemAddBtnVisibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    NewItemAddBtnVisibility = System.Windows.Visibility.Hidden;
                }
            }
            return true;
        }

        public MaterialName CreateNewMaterial(string materialName)
        {
            return DBResources.Instance.CreateNewMaterial(materialName);
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

        private Visibility m_ActionButtonsVisibility = Visibility.Visible;
        public Visibility ActionButtonsVisibility
        {
            get
            {
                return m_ActionButtonsVisibility;
            }
            set
            {
                m_ActionButtonsVisibility = value;
                NotifyPropertyChanged("ActionButtonsVisibility");
            }
        }

        private Visibility m_NewItemAddBtnVisibiliti = Visibility.Visible;
        public Visibility NewItemAddBtnVisibility
        {
            get
            {
                return m_NewItemAddBtnVisibiliti;
            }
            set
            {
                m_NewItemAddBtnVisibiliti = value;
                NotifyPropertyChanged("NewItemAddBtnVisibility");
            }
        }

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
                    if (Order.OrderStatusID == (short)OrderStatusEnum.EnquiryCreated)
                        Order.OrderStatusID = (short)OrderStatusEnum.MaterialsAdded;
                    else if (Order.OrderStatusID == (short)OrderStatusEnum.MaterialsAdded)
                        Order.OrderStatusID = (short)OrderStatusEnum.MaterialsCostAdded;
                    else if (Order.OrderStatusID == (short)OrderStatusEnum.MaterialsCostAdded)
                        Order.OrderStatusID = (short)OrderStatusEnum.MaterialsJobCompleted;
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
                }

                #region History

                    History historyItem = new History();
                    historyItem.Date = DateTime.Now;
                    historyItem.UserName = DBResources.Instance.CurrentUser.UserName;
                    historyItem.Comment = userComment;

                    if (isSubmit)
                    {
                        historyItem.OrderChanges = "Submitted in Materials Page. Order Stauts Changed to " + Order.OrderStatu.DisplayLabel.ToUpper();
                    }
                    else
                    {
                        historyItem.OrderChanges = "Saved Changes in Materials";
                    }

                    Order.Histories.Add(historyItem);
                    
                #endregion 

                Order.LastModifiedDate = DateTime.Now;
                if (DBResources.Instance.UpdateOrderProducts())
                {
                    if(isSubmit)
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
