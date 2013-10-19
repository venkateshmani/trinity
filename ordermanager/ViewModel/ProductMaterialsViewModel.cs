﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.DatabaseModel;
using System.Data.Entity;

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

        public bool Save(bool isSubmit, string userComment)
        {
            Order.HasUserClickedSaveOrSubmit = true;

            if (!HasError)
            {
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

                return DBResources.Instance.UpdateOrderProducts();
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
