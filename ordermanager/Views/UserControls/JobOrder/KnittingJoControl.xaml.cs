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

namespace ordermanager.Views.UserControls.JobOrderControls
{
    /// <summary>
    /// Interaction logic for KnittingJoControl.xaml
    /// </summary>
    public partial class KnittingJoControl : UserControl, IJobOrderControl
    {
        public KnittingJoControl()
        {
            InitializeComponent();
        }

        public bool Generate()
        {
            throw new NotImplementedException();
        }

        public bool Submit()
        {
            throw new NotImplementedException();
        }

        public bool Approve()
        {
            throw new NotImplementedException();
        }

        public bool Reject()
        {
            throw new NotImplementedException();
        }

        public bool ShowPDF()
        {
            throw new NotImplementedException();
        }


        public bool Discard()
        {
            throw new NotImplementedException();
        }

        #region Supplier Management

        private void addNewSupplierBtn_Click_1(object sender, RoutedEventArgs e)
        {
            var newSupplier = NewCompany("Supplier", supplierComboBox.Text);
            if (newSupplier != null)
            {
                supplierComboBox.SelectedItem = newSupplier;
            }
        }

        private void editExistingSupplier_Click_1(object sender, RoutedEventArgs e)
        {
            EditCompany(supplierComboBox.SelectedItem as Company);
        }

        private void supplierComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (supplierComboBox.SelectedItem == null)
            {
                addNewSupplierBtn.Visibility = System.Windows.Visibility.Visible;
                editExistingSupplier.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                addNewSupplierBtn.Visibility = System.Windows.Visibility.Collapsed;
                editExistingSupplier.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void EditCompany(Company company)
        {
            CustomerDetailsControl details = new CustomerDetailsControl(company.CompanyType.Type);
            details.DataContext = company;

            if (details.ShowDialog() == true)
            {
                DBResources.Instance.Save();
            }
        }

        private Company NewCompany(string type, string companyName)
        {
            CustomerDetailsControl details = new CustomerDetailsControl(type);
            Company newCompany = new Company();
            newCompany.Name = companyName;
            details.DataContext = newCompany;
            if (details.ShowDialog() == true)
            {
                return DBResources.Instance.SaveNewCompany(newCompany, type);
            }
            return null;
        }

        #endregion 
    

      
    }
}
