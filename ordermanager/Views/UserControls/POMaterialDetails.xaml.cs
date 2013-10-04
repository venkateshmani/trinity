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
    /// Interaction logic for POMaterialDetails.xaml
    /// </summary>
    public partial class POMaterialDetails : UserControl
    {
        PurchaseOrderControlViewModel m_ViewModel;

        public POMaterialDetails()
        {
            InitializeComponent();
        }


        public PurchaseOrderControlViewModel ViewModel
        {
            get
            {
                return m_ViewModel;
            }
            set
            {
                m_ViewModel = value;
                this.DataContext = value;
            }
        }

        private Company AddNewSupplier(string type, string companyName)
        {
            CustomerDetailsControl details = new CustomerDetailsControl();
            Company newCompany = DBResources.Instance.CreateNewCompany(type);
            newCompany.Name = companyName;
            details.DataContext = newCompany;
            if (details.ShowDialog() == true)
            {
                DBResources.Instance.SaveNewCompany(newCompany);
                return newCompany;
            }
            return null;
        }

        private bool EditSupplier(Company company)
        {
            CustomerDetailsControl details = new CustomerDetailsControl();
            details.DataContext = company;
            if (details.ShowDialog() == true)
            {
                DBResources.Instance.Save();
                return true;
            }
            return false;
        }

        private object GetControl(Grid parent, string controlName)
        {
            if (parent != null)
            {
                return parent.FindName(controlName);
            }
            return null;
        }

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.AddNewProductMaterialItem();
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewSubMaterial(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewSupplier_Click(object sender, RoutedEventArgs e)
        {
            Button btnAddSupplier = sender as Button;
            if (btnAddSupplier != null && btnAddSupplier.Tag != null)
            {
                Company newSupplier = AddNewSupplier("Supplier", Convert.ToString(btnAddSupplier.Tag));
                if (newSupplier != null)
                {
                    Grid parentGrid = btnAddSupplier.Parent as Grid;
                    if (parentGrid != null)
                    {
                        ComboBox comboBox = GetControl(parentGrid, "supplierComboBox") as ComboBox;
                        if (comboBox != null)
                        {
                            comboBox.SelectedItem = newSupplier;
                            btnAddSupplier.Visibility = System.Windows.Visibility.Collapsed;
                            Button btnEditSupplier = GetControl(parentGrid, "btnEditSupplier") as Button;
                            if (btnEditSupplier != null)
                                btnEditSupplier.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                }
            }
        }



        private void EditSupplier_Click(object sender, RoutedEventArgs e)
        {
            Button btnEditSupplier = sender as Button;
            if (btnEditSupplier != null && btnEditSupplier.Tag != null)
            {
                Grid parentGrid = btnEditSupplier.Parent as Grid;
                if (parentGrid != null)
                {
                    ComboBox comboBox = GetControl(parentGrid, "supplierComboBox") as ComboBox;
                    if (comboBox != null && comboBox.SelectedItem != null)
                    {
                        Company supplier = comboBox.SelectedItem as Company;
                        if (supplier != null)
                            EditSupplier(supplier);

                    }
                }
            }
        }

        private void SupplierComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox supplierComboBox = sender as ComboBox;
            if (supplierComboBox != null)
            {
                Grid parentGrid = supplierComboBox.Parent as Grid;
                if (parentGrid != null)
                {
                    Button btnAddSupplier = GetControl(parentGrid, "btnAddSupplier") as Button;
                    Button btnEditSupplier = GetControl(parentGrid, "btnEditSupplier") as Button;
                    if (btnAddSupplier != null && btnEditSupplier != null)
                    {
                        btnAddSupplier.Tag = supplierComboBox.Text;
                        btnEditSupplier.Tag = supplierComboBox.Text;
                        if (supplierComboBox.SelectedItem != null)
                        {
                            btnAddSupplier.Visibility = System.Windows.Visibility.Collapsed;
                            btnEditSupplier.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            btnEditSupplier.Visibility = System.Windows.Visibility.Collapsed;
                            btnAddSupplier.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                }
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
