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

namespace ordermanager.Views.PopUps
{
    /// <summary>
    /// Interaction logic for NewEnquiryFormUserControl.xaml
    /// </summary>
    public partial class NewEnquiryFormUserControl : UserControl
    {
        public NewEnquiryFormUserControl()
        {
            InitializeComponent();
            this.Loaded += NewEnquiryFormUserControl_Loaded;
        }

        
        void NewEnquiryFormUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            NewEnquiryViewModel = new NewEnquiryViewModel();
        }

        private void addNewCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerDetailsControl details = new CustomerDetailsControl();
            Company newCompany = DBResources.Instance.CreateNewCompany("Customer");
            newCompany.Name = customerComboBox.Text;

            details.DataContext = newCompany;
            if (details.ShowDialog() == true)
            {
                DBResources.Instance.SaveNewCompany(newCompany);
                NewEnquiryViewModel.Order.Company = newCompany;
            }
        }

        private NewEnquiryViewModel m_NewEnquiryViewModel = null;
        public NewEnquiryViewModel NewEnquiryViewModel
        {
            get
            {
                return m_NewEnquiryViewModel;
            }
            set
            {
                m_NewEnquiryViewModel = value;
                this.DataContext = value;
            }
        }

        private void currencyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                //Dirty
                NewEnquiryViewModel.RefreshCurrencyConversionTable();
        }

        private void addNewItemBtn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NewEnquiryViewModel.OrderProducts.Add(new OrderProduct());
        }

        private void customerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void agentComboxBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (agentComboxBox.SelectedItem == null)
            {
                addNewAgentBtn.Visibility = System.Windows.Visibility.Visible;
                editExistingAgentBtn.Visibility = System.Windows.Visibility.Collapsed;
                commisionInfo.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                if (((Company)agentComboxBox.SelectedItem).Name.ToLower() == "customer")
                {
                    addNewAgentBtn.Visibility = System.Windows.Visibility.Collapsed;
                    editExistingAgentBtn.Visibility = System.Windows.Visibility.Collapsed;
                    commisionInfo.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    addNewAgentBtn.Visibility = System.Windows.Visibility.Collapsed;
                    editExistingAgentBtn.Visibility = System.Windows.Visibility.Visible;
                    commisionInfo.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void editExistingCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerDetailsControl details = new CustomerDetailsControl();
            details.DataContext = NewEnquiryViewModel.Order.Company;

            if (details.ShowDialog() == true)
            {
                DBResources.Instance.Save();
            }
        }

        private void editExistingAgentBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerDetailsControl details = new CustomerDetailsControl();
            details.DataContext = NewEnquiryViewModel.Order.Agent;

            if (details.ShowDialog() == true)
            {
                DBResources.Instance.Save();
            }

        }

        private void productComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox productComboBox = sender as ComboBox;
            if (productComboBox != null)
            {
                Grid parentGrid = productComboBox.Parent as Grid;
                
                if (parentGrid != null)
                {
                    Button addbtn = parentGrid.FindName("addBtn") as Button;
                    if (addbtn != null)
                    {
                        if (productComboBox.SelectedItem != null)
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

     
        private void addNewProductBtn_Click(object sender, RoutedEventArgs e)
        {
            Button addBtn = sender as Button;
            if (addBtn != null)
            {
                Grid parentGrid = addBtn.Parent as Grid;

                if (parentGrid != null)
                {
                    ComboBox comboBox = parentGrid.FindName("comboBox") as ComboBox;
                    if (comboBox != null)
                    {
                        comboBox.SelectedItem = NewEnquiryViewModel.CreateNewProduct(comboBox.Text);
                        addBtn.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            }  
        }

        private void positiveDecisionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (positiveDecisionBtn.Content.ToString() == "Create")
            {
                NewEnquiryViewModel.CreateNewOrder();
            }
        }
    }
}
