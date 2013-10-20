using ordermanager.DatabaseModel;
using ordermanager.Utilities;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
            if (m_ViewModel!=null && m_ViewModel.Order != null)
            {
                gridButtons.Visibility = System.Windows.Visibility.Visible;
                poMaterialsDetails.IsEnabled = true;
                poProductDetails.IsEnabled = true;  
                OrderStatusEnum status = Helper.GetOrderStatusEnumFromString(m_ViewModel.Order.OrderStatu.StatusLabel);
                if (status >= OrderStatusEnum.SubMaterialsJobCompleted)
                {
                    gridButtons.Visibility = System.Windows.Visibility.Collapsed;
                    poMaterialsDetails.IsEnabled = false;
                    poProductDetails.IsEnabled = false;                  
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
                MessageBox.Show("Cannot save the materials details. Please make sure all the mandatory details are filled", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            bool? saved = Save(true);
            if (saved == true)
            {
                SetControlState();
            }
            else if (saved == false)
            {
                MessageBox.Show("Cannot submit the materials details. Please make sure all the mandatory details are filled", "Submit Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool? Save(bool isSubmit)
        {
            if (m_ViewModel != null)
            {
                if (m_ViewModel.Validate())
                {
                    CommentBox commentBox = new CommentBox(Util.GetParentWindow(this));
                    if (commentBox.ShowDialog() == true)
                    {
                        return m_ViewModel.Save(isSubmit, commentBox.Comment);
                    }
                    return null;
                }
                return false;
            }
            return false;
        }
    }
}


