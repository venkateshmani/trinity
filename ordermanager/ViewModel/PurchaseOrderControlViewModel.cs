using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Linq;

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

        public DBResources DBResources
        {
            get
            {
                return DBResources.Instance;
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

        public bool DeleteProductMateialItem(ProductMaterialItem item)
        {
            if (m_SelectedMaterial != null)
            {
                m_SelectedMaterial.ProductMaterialItemsWrapper.Remove(item);
                return true;
            }

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

        public Color AddNewColor(string newColorName)
        {
            return DBResources.Instance.CreateNewColor(newColorName);
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
            Dictionary<long, Dictionary<int, decimal>> summaryDict = new Dictionary<long, Dictionary<int, decimal>>(1);

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
                    
                    if (!summaryDict.ContainsKey(breakupItem.ProductSize.ProductSizeID))
                    {
                        Dictionary<int, decimal> tmp = new Dictionary<int, decimal>(1);
                        summaryDict.Add(breakupItem.ProductSize.ProductSizeID, tmp);
                    }

                    if (!summaryDict[breakupItem.ProductSize.ProductSizeID].ContainsKey(breakupItem.Color.ColorID))
                    {
                        summaryDict[breakupItem.ProductSize.ProductSizeID].Add(breakupItem.Color.ColorID, breakupItem.NumberOfPiecesWrapper);
                    }
                    else
                    {
                        summaryDict[breakupItem.ProductSize.ProductSizeID][breakupItem.Color.ColorID] += breakupItem.NumberOfPiecesWrapper;
                    }
                }

                for (int i = 0; i < dbProduct.ProductBreakUpSummaries.Count; )
                {
                    dbProduct.ProductBreakUpSummaries.Remove(dbProduct.ProductBreakUpSummaries.ElementAt(i));
                }
                    
                foreach (KeyValuePair<long, Dictionary<int, decimal>> kvp in summaryDict)
                {
                    foreach (KeyValuePair<int, decimal> kvp2 in kvp.Value)
                    {
                        var productBreakUpSummaryInDB = (from record in dbProduct.ProductBreakUpSummaries
                                                            where record.ProductSizeID == kvp.Key && record.ColorID == kvp2.Value
                                                            select record).SingleOrDefault();

                        if (productBreakUpSummaryInDB == null)
                        {
                            ProductBreakUpSummary sumyItem = new ProductBreakUpSummary();
                            sumyItem.ProductID = dbProduct.ProductID;
                            sumyItem.ProductSizeID = kvp.Key;
                            sumyItem.ColorID = kvp2.Key;
                            sumyItem.NumberOfPieces = kvp2.Value;
                            dbProduct.ProductBreakUpSummaries.Add(sumyItem);
                        }
                        else
                        {
                            productBreakUpSummaryInDB.NumberOfPieces = kvp2.Value;
                        }
                    }
                }

                //Ugly code: But this is the only option for the time and money given
                dbProduct.RefreshExecutionTabProperties();
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
                    if (dbProduct.ProductBreakUp.ShipmentModeWrapper == null)
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
                decimal totalQuantity = 0;
                foreach (ProductCountryWiseBreakUp breakupItem in dbProduct.ProductBreakUpWrapper.ProductCountryWiseBreakUpWrapper)
                {
                    totalQuantity += breakupItem.NumberOfPieces;
                    if (!breakupItem.Validate())
                        hasErrors = true;
                }
                decimal per = Convert.ToDecimal(dbProduct.ProductBreakUpWrapper.Tolerance) / 100;
                dbProduct.RemoveError("ExpectedQuantity");
                dbProduct.ProductBreakUpWrapper.RemoveError("ToleranceWrapper");
                decimal quantityMultipler = dbProduct.UnitsOfMeasurement.QuantityMultiplier.Value;
                if (totalQuantity > (1.0M + per) * (dbProduct.ExpectedQuantity * quantityMultipler) || totalQuantity < (1.0M - per) * dbProduct.ExpectedQuantity * quantityMultipler)
                {
                    dbProduct.AddError("ExpectedQuantity", "ExpectedQuantity", false);
                    dbProduct.ProductBreakUpWrapper.AddError("ToleranceWrapper", string.Format("The total quantity {0} is not in the allowed tolerance limits of {1} and {2}.", totalQuantity, (1.0M - per) * dbProduct.ExpectedQuantity,(1.0M + per) * dbProduct.ExpectedQuantity), false);
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
