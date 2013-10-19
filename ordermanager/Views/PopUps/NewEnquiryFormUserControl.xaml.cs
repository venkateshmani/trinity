using MahApps.Metro;
using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private Accent currentAccent = ThemeManager.DefaultAccents.First(x => x.Name == "Blue");

        public NewEnquiryFormUserControl()
        {
            InitializeComponent();

        }

        private bool m_IsNewEnquiry = true;
        public bool IsNewEnquiry
        {
            get
            {
                return m_IsNewEnquiry;
            }
            set
            {
                m_IsNewEnquiry = value;
                rootLayout.IsEnabled = value;
                NewEnquiryViewModel = null;
                if (!value)
                {
                    // positiveDecisionBtn.Visibility = System.Windows.Visibility.Collapsed;
                    // negativeDecisionBtn.Visibility = System.Windows.Visibility.Collapsed;
                    addNewItemBtn.Visibility = System.Windows.Visibility.Collapsed;
                    addNewCustomerBtn.Visibility = System.Windows.Visibility.Collapsed;
                    addNewAgentBtn.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    NewEnquiryViewModel = new NewEnquiryViewModel();
                }
            }
        }


        public void SetOrder(Order order)
        {
            this.NewEnquiryViewModel = new NewEnquiryViewModel(order);
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
                if (value != null)
                {
                    SetButtonsVisibility(System.Windows.Visibility.Visible);
                    if (value.Order.OrderStatusID == 0)
                    {
                        SetButtonText(positiveDecisionBtn, "Create");
                        SetButtonText(negativeDecisionBtn, "Discard");
                    }
                    else
                    {
                        OrderStatusEnum status = Helper.GetOrderStatusEnumFromString(value.Order.OrderStatu.StatusLabel);
                        if (status == OrderStatusEnum.MaterialsJobCompleted)
                        {
                            SetButtonText(positiveDecisionBtn, "Approve");
                            SetButtonText(negativeDecisionBtn, "Reject");
                        }
                        else if (status == OrderStatusEnum.EnquiryApproved)
                        {
                            SetButtonText(positiveDecisionBtn, "Confirm");
                            SetButtonText(negativeDecisionBtn, "Cancel");
                        }
                        else
                        {
                            SetButtonsVisibility(System.Windows.Visibility.Collapsed);
                        }
                    }
                }
            }
        }

        private void SetButtonsVisibility(System.Windows.Visibility visibility)
        {
            positiveDecisionBtn.Visibility = visibility;
            negativeDecisionBtn.Visibility = visibility;
        }

        private void SetButtonText(Button btn, string text)
        {
            btn.Content = text;
            btn.ToolTip = text + " Enquiry";
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
                    agentInfo.Visibility = System.Windows.Visibility.Collapsed;
                    addNewAgentBtn.Visibility = System.Windows.Visibility.Collapsed;
                    editExistingAgentBtn.Visibility = System.Windows.Visibility.Collapsed;
                    commisionInfo.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    agentInfo.Visibility = System.Windows.Visibility.Visible;
                    addNewAgentBtn.Visibility = System.Windows.Visibility.Collapsed;
                    editExistingAgentBtn.Visibility = System.Windows.Visibility.Visible;
                    commisionInfo.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void editExistingCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            EditCompany(NewEnquiryViewModel.Order.Company);
        }

        private void editExistingAgentBtn_Click(object sender, RoutedEventArgs e)
        {
            EditCompany(NewEnquiryViewModel.Order.Agent);
        }

        private void EditCompany(Company company)
        {
            CustomerDetailsControl details = new CustomerDetailsControl();
            details.DataContext = company;

            if (details.ShowDialog() == true)
            {
                DBResources.Instance.Save();
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
                if (NewEnquiryViewModel.HasErrors)
                {
                    statusText.Visibility = System.Windows.Visibility.Visible;
                    return;
                }
                Order newOrder = NewEnquiryViewModel.CreateNewOrder();
                if (newOrder != null)
                {
                    statusText.Text = string.Format("Enquiry Successfull Created. ID : {0}", newOrder.OrderID.ToString());
                    statusText.Visibility = System.Windows.Visibility.Visible;
                    statusText.Foreground = new SolidColorBrush(Color.FromArgb(255, 17, 158, 218));
                    SetButtonsVisibility(System.Windows.Visibility.Collapsed);
                }
            }
            else if (positiveDecisionBtn.Content.ToString() == "Approve")
            {
                try
                {
                    if (NewEnquiryViewModel.UpdateOrderStatus("approved the Order",string.Empty, OrderStatusEnum.EnquiryApproved))
                    {
                        SetButtonText(positiveDecisionBtn, "Confirm");
                        SetButtonText(negativeDecisionBtn, "Cancel");
                        MessageBox.Show("Enquiry approved successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show("Enquiry approval failed!!!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enquiry approval failed!!!" + Environment.NewLine + ex.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (positiveDecisionBtn.Content.ToString() == "Confirm")
            {
                try
                {
                    if (NewEnquiryViewModel.UpdateOrderStatus( "confirmed the Order", string.Empty,OrderStatusEnum.OrderConfirmed))
                    {
                        SetButtonsVisibility(System.Windows.Visibility.Collapsed);
                        MessageBox.Show("Enquiry confirmed successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show("Enquiry confirming failed!!!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enquiry confirming failed!!!" + Environment.NewLine + ex.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                SetButtonsVisibility(System.Windows.Visibility.Collapsed);
            }
        }

        private void negativeDecisionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (positiveDecisionBtn.Content.ToString() == "Discard")
            {

            }
            else if (positiveDecisionBtn.Content.ToString() == "Reject")
            {
                try
                {
                    if (NewEnquiryViewModel.UpdateOrderStatus("rejected the Enquiry", string.Empty, OrderStatusEnum.EnquiryRejected))
                    {
                        SetButtonsVisibility(System.Windows.Visibility.Collapsed);
                        MessageBox.Show("Enquiry rejected successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show("Enquiry rejection failed!!!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enquiry rejection failed!!!" + Environment.NewLine + ex.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (positiveDecisionBtn.Content.ToString() == "Cancel")
            {
                try
                {
                    if (NewEnquiryViewModel.UpdateOrderStatus("cancelled the Enquiry", string.Empty, OrderStatusEnum.EnquiryCancelled))
                    {
                        SetButtonsVisibility(System.Windows.Visibility.Collapsed);
                        MessageBox.Show("Enquiry cancelled successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                        MessageBox.Show("Enquiry cancellation failed!!!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Enquiry cancellation failed!!!" + Environment.NewLine + ex.Message, "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                SetButtonsVisibility(System.Windows.Visibility.Collapsed);
            }
        }

        private void addNewCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            var newCustomer = NewCompany("Customer", customerComboBox.Text);
            if (newCustomer != null)
            {
                NewEnquiryViewModel.Order.Customer = newCustomer;
            }
        }

        private void addNewAgentBtn_Click(object sender, RoutedEventArgs e)
        {
            var newAgent = NewCompany("Agent", agentComboxBox.Text);
            if (newAgent != null)
            {
                NewEnquiryViewModel.Order.Agent = newAgent;
            }
        }

        private Company NewCompany(string type, string companyName)
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

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private Boolean IsTextAllowed(String text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        // Use the DataObject.Pasting Handler  
        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsTextAllowed(text)) e.CancelCommand();
            }
            else e.CancelCommand();
        }

        private void comboBox_TextChanged(object sender, TextChangedEventArgs e)
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

        private void orderDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateNumberOfDays();
        }

        private void expectedDeliveryDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateNumberOfDays();
        }

        private void UpdateNumberOfDays()
        {
            if (expectedDeliveryDate.SelectedDate != null && orderDate.SelectedDate != null)
            {
                TimeSpan tSpan = expectedDeliveryDate.SelectedDate.Value.Subtract(orderDate.SelectedDate.Value);
                numberOfDays.Text = tSpan.Days.ToString();
            }
        }
    }
}
