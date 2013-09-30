using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ProductMaterials.xaml
    /// </summary>
    public partial class ProductMaterialsControl : UserControl
    {
        ProductMaterialsViewModel m_ViewModel;

        public ProductMaterialsControl()
        {
            InitializeComponent();
        }

        private void productsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderProduct product = productsList.SelectedItem as OrderProduct;
            if (product != null && m_ViewModel != null)
                m_ViewModel.SelectedProduct = product;
        }

        private void AddNewMaterial(object sender, RoutedEventArgs e)
        {
            Button addBtn = sender as Button;
            if (addBtn != null)
            {
                Grid parentGrid = addBtn.Parent as Grid;
                if (parentGrid != null)
                {
                    ComboBox comboBox = parentGrid.FindName("materialsComboBox") as ComboBox;
                    if (comboBox != null && m_ViewModel != null)
                    {
                        comboBox.SelectedItem = m_ViewModel.CreateNewMaterial(comboBox.Text);
                        addBtn.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            m_ViewModel = DataContext as ProductMaterialsViewModel;           
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            if (m_ViewModel != null)
            {
                m_ViewModel.AddNewMaterialItem();             
            }
        }      

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {            
            if (m_ViewModel != null)
            {               
                //m_ViewModel.ProductMaterialsList.Remove(materialsGrid.CurrentItem as ProductMaterial);             
            }           
        }      

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (m_ViewModel != null)
            {
                m_ViewModel.Save(false);
            } 
        }

        private void productsList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            productColumn.Width = productsList.ActualWidth;
        }
    }
}
