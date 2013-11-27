using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.Utilities;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using ordermanager.Extended_Database_Models;

namespace ordermanager.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PurchaseOrderControl.xaml
    /// </summary>
    public partial class PurchaseOrderControl : UserControl
    {
        PurchaseOrderControlViewModel m_ViewModel;

        public PurchaseOrderControl()
        {
            InitializeComponent();
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (m_ViewModel != null)
                m_ViewModel.PropertyChanged -= m_ViewModel_PropertyChanged;
            m_ViewModel = DataContext as PurchaseOrderControlViewModel;
            poMaterialsDetails.ViewModel = m_ViewModel;
            poProductDetails.ViewModel = m_ViewModel;
            if (m_ViewModel != null)
            {
                m_ViewModel.PropertyChanged += m_ViewModel_PropertyChanged;
                SetControlState();
            }
        }

        private void SetControlState()
        {
            if (m_ViewModel != null && m_ViewModel.Order != null)
            {
                OrderStatusEnum status = Helper.GetOrderStatusEnumFromString(m_ViewModel.Order.OrderStatu.StatusLabel);
                if (DBResources.Instance.CurrentUser.UserRole.CanAddSubMaterials && status == OrderStatusEnum.OrderConfirmed)
                {
                    gridButtons.Visibility = System.Windows.Visibility.Visible;
                    poProductDetails.IsReadOnly = false;
                }
                else
                {
                    gridButtons.Visibility = System.Windows.Visibility.Collapsed;
                    poProductDetails.IsReadOnly = true;
                }
            }
        }

        void m_ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Products")
            {
                SetSelectedItem();
            }
            else if (e.PropertyName == "Order")
            {
                SetControlState();
            }
        }

        void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            SetSelectedItem();
        }

        private void SetSelectedItem()
        {
            ItemsControl control = tvProducts as ItemsControl;
            control.ItemContainerGenerator.StatusChanged -= ItemContainerGenerator_StatusChanged;
            if (control.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                if (tvProducts.Items.Count > 0)
                {
                    TreeViewItem item = control.ItemContainerGenerator.ContainerFromItem(tvProducts.Items[0]) as TreeViewItem;
                    if (item != null)
                        item.IsSelected = true;
                }
            }
            else
            {
                control.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
            }
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_ViewModel != null)
            {
                m_ViewModel.AddNewProductMaterialItem();
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tvProducts_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvProducts.SelectedItem != null)
            {
                if (tvProducts.SelectedItem is OrderProduct)
                {
                    m_ViewModel.SelectedProduct = tvProducts.SelectedItem as OrderProduct;
                    poProductDetails.Visibility = System.Windows.Visibility.Visible;
                    poMaterialsDetails.Visibility = System.Windows.Visibility.Hidden;
                }
                else if (tvProducts.SelectedItem is ProductMaterial)
                {
                    ProductMaterial material = tvProducts.SelectedItem as ProductMaterial;
                    if (m_ViewModel.SelectedProduct.ProductID != material.ProductID)
                    {
                        m_ViewModel.SelectedProduct = material.OrderProduct;
                    }
                    m_ViewModel.SelectedMaterial = material;
                    poProductDetails.Visibility = System.Windows.Visibility.Hidden;
                    poMaterialsDetails.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Save(false) == false)
            {
                string message = string.Format("Failed to Save !. Fill in the highlighted fields and Save");
                InformUser(message);
            }
            else
            {
                string message = string.Format("Successfully Saved !");
                InformUser(message);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            bool? saved = Save(true);
            if (saved == true)
            {
                string message = string.Format("Successfully Submitted !.");
                GenerateAndAssignPurchaseOrders();
                InformUser(message);
                SetControlState();
            }
            else if (saved == false)
            {
                string message = string.Format("Failed to Submit !. Fill in the highlighted fields and Submit");
                InformUser(message);
            }
        }


        public void GenerateAndAssignPurchaseOrders()
        {
            //Create or Get and Populate Purchase Orders
            Dictionary<Company, PurchaseOrder> purchaseOrders = new Dictionary<Company, PurchaseOrder>();
            foreach (var supplier in m_ViewModel.Order.Suppliers)
            {
                string purchaseOrderNumber = Constants.GetPurchaseOrderNumber(supplier, m_ViewModel.Order);
                PurchaseOrder po = DBResources.Instance.GetPurchaseOrder(purchaseOrderNumber);
                if (po == null)
                {
                    po = DBResources.Instance.CreateNewPurchaseOrder(m_ViewModel.Order, supplier, purchaseOrderNumber);
                }

                if (po != null && !purchaseOrders.ContainsKey(supplier))
                {
                    purchaseOrders.Add(supplier, po);
                }
            }

            //Assign PO's to Material Items
            foreach (var orderProduct in m_ViewModel.Products)
            {
                foreach (PurchaseOrderItems poItem in orderProduct.PurchaseOrderItems)
                {
                    PurchaseOrder po = purchaseOrders[poItem.Supplier];

                    foreach (OrderedItem itemToOrder in poItem.OrderedItems)
                    {
                        po.OrderedItems.Add(itemToOrder);
                    }
                }
            }

            DBResources.Instance.Save();
        }

        private bool? Save(bool isSubmit)
        {
            if (m_ViewModel != null)
            {
                if (m_ViewModel.Validate())
                {
                    if (isSubmit)
                    {
                        CommentBox commentBox = new CommentBox(Util.GetParentWindow(this));
                        if ((commentBox.ShowDialog() == true))
                        {
                            return m_ViewModel.Save(isSubmit, commentBox.Comment);
                        }
                        return false;
                    }
                    else
                        return m_ViewModel.Save(isSubmit, "");
                }
                return false;
            }
            return false;
        }

        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
        }

    }
}


