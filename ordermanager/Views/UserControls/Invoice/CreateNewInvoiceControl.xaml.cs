using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.Views.UserControls.Invoice;
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
    /// Interaction logic for InvoiceControl.xaml
    /// </summary>
    public partial class CreateNewInvoiceControl : UserControl
    {
        public CreateNewInvoiceControl()
        {
            InitializeComponent();
        }

       
        private void addNewCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            var newCustomer = NewCompany("Customer", customerComboBox.Text);
            if (newCustomer != null)
            {
                customerComboBox.SelectedItem = newCustomer;
            }
        }

        private void editExistingCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            EditCompany(customerComboBox.SelectedItem as Company);
        }

        private void customerComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (customerComboBox.SelectedItem == null)
            {
                addNewCustomerBtn.Visibility = System.Windows.Visibility.Visible;
                editExistingCustomerBtn.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                addNewCustomerBtn.Visibility = System.Windows.Visibility.Collapsed;
                editExistingCustomerBtn.Visibility = System.Windows.Visibility.Visible;
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

        private void btnAddNewCountry_Click(object sender, RoutedEventArgs e)
        {
            Button addBtn = sender as Button;
            if (addBtn != null)
            {
                StackPanel parentContainer = addBtn.Parent as StackPanel;

                if (parentContainer != null)
                {
                    ComboBox comboBox = null;
                    foreach (var child in parentContainer.Children)
                    {
                        if (child is ComboBox)
                        {
                            comboBox = child as ComboBox;
                            break;
                        }
                    }

                    if (comboBox != null && !string.IsNullOrEmpty((comboBox.Text)))
                    {
                        comboBox.SelectedItem = DBResources.Instance.CreateNewCountry(comboBox.Text);
                        addBtn.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }
        }

        private void countryComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComboBox countryComboBox = sender as ComboBox;
            if (countryComboBox != null)
            {
                StackPanel parentContainer = countryComboBox.Parent as StackPanel;

                if (parentContainer != null)
                {
                    Button addbtn = null;
                    foreach (var child in parentContainer.Children)
                    {
                        if (child is Button)
                        {
                            addbtn = child as Button;
                            break;
                        }
                    }
                    
                    if (addbtn != null)
                    {
                        if (countryComboBox.SelectedItem != null || string.IsNullOrEmpty(countryComboBox.Text))
                        {
                            addbtn.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        else
                        {
                            addbtn.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                }
            }          
        }

        private void SelectedCartonBoxBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CatronBoxBrowser browser = new CatronBoxBrowser();
            browser.ShowDialog();
        }
    }
}
