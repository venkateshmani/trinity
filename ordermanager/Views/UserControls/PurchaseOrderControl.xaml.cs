using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void AddNewSubMaterial(object sender, RoutedEventArgs e)
        {
            //Button addBtn = sender as Button;
            //if (addBtn != null)
            //{
            //    Grid parentGrid = addBtn.Parent as Grid;
            //    if (parentGrid != null)
            //    {
            //        ComboBox comboBox = parentGrid.FindName("materialsComboBox") as ComboBox;
            //        if (comboBox != null && m_ViewModel != null)
            //        {
            //            comboBox.SelectedItem = m_ViewModel.CreateNewMaterial(comboBox.Text);
            //            addBtn.Visibility = System.Windows.Visibility.Collapsed;
            //        }
            //    }
            //}
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            m_ViewModel = DataContext as PurchaseOrderControlViewModel;
            poMaterialsDetails.ViewModel = m_ViewModel;
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
            m_ViewModel.Save(false);
        }
    }
}


