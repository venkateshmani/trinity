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
            gridDetails.DataContextChanged += DataGrid_DataContextChanged;
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
            CustomerDetailsControl details = new CustomerDetailsControl("Supplier");
            Company newCompany = new Company();
            newCompany.Name = companyName;
            details.DataContext = newCompany;
            if (details.ShowDialog() == true)
            {
                return DBResources.Instance.SaveNewCompany(newCompany, type);               
            }
            return null;
        }

        private bool EditSupplier(Company company)
        {
            CustomerDetailsControl details = new CustomerDetailsControl("Supplier");
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

        private void AddNewSubMaterial_Click(object sender, RoutedEventArgs e)
        {
            Button btnAddNewSubMaterial = sender as Button;
            if (btnAddNewSubMaterial != null)
            {
                Grid parentGrid = btnAddNewSubMaterial.Parent as Grid;
                if (parentGrid != null)
                {
                    ComboBox comboBox = GetControl(parentGrid, "subMaterialsComboBox") as ComboBox;
                    if (comboBox != null && m_ViewModel != null)
                    {
                        comboBox.SelectedItem = m_ViewModel.CreateNewSubMaterial(comboBox.Text);
                        btnAddNewSubMaterial.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
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
                        else if (!string.IsNullOrWhiteSpace(supplierComboBox.Text))
                        {
                            btnEditSupplier.Visibility = System.Windows.Visibility.Collapsed;
                            btnAddSupplier.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            btnAddSupplier.Visibility = System.Windows.Visibility.Collapsed;
                            btnEditSupplier.Visibility = System.Windows.Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void SubMaterialsComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox subMaterialsComboBox = sender as ComboBox;
            if (subMaterialsComboBox != null)
            {
                Grid parentGrid = subMaterialsComboBox.Parent as Grid;
                if (parentGrid != null)
                {
                    Button btnAddNewSubMaterial = GetControl(parentGrid, "btnAddNewSubMaterial") as Button;
                    if (btnAddNewSubMaterial != null)
                    {
                        if (subMaterialsComboBox.SelectedItem != null)
                            btnAddNewSubMaterial.Visibility = System.Windows.Visibility.Collapsed;
                        else if (!string.IsNullOrWhiteSpace(subMaterialsComboBox.Text))
                            btnAddNewSubMaterial.Visibility = System.Windows.Visibility.Visible;
                        else
                            btnAddNewSubMaterial.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
        }

        private void DataGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ObjectDataProvider sub = this.FindResource("GetAvailableSubMaterials") as ObjectDataProvider;
            if (sub != null && m_ViewModel != null && m_ViewModel.SelectedMaterial != null)
            {
                sub.MethodParameters.Clear();
                sub.MethodParameters.Add(m_ViewModel.SelectedMaterial.MaterialName.Name);
                sub.Refresh();
            }
            gridDetails.DataContextChanged -= DataGrid_DataContextChanged;
            object obj = this.DataContext;
            this.DataContext = null;
            this.DataContext = obj;
            gridDetails.DataContextChanged += DataGrid_DataContextChanged;

        }
    }
}
