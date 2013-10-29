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
using ordermanager.Utilities;
using MahApps.Metro.Controls;
using ordermanager.Interfaces_And_Enums;


namespace ordermanager.Views.PopUps
{
    /// <summary>
    /// Interaction logic for NewEnquiryFormUserControl.xaml
    /// </summary>
    public partial class NewEnquiryFormUserControl : System.Windows.Controls.UserControl
    {
        private Accent currentAccent = ThemeManager.DefaultAccents.First(x => x.Name == "Blue");
        public event OnNavigateToDelegate OnNavigateTo = null;
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
                NewEnquiryViewModel = null;
                //rootLayout.IsEnabled = true;
                if (!value)
                {
                    //addNewItemBtn.Visibility = System.Windows.Visibility.Collapsed;
                    addNewCustomerBtn.Visibility = System.Windows.Visibility.Collapsed;
                    addNewAgentBtn.Visibility = System.Windows.Visibility.Collapsed;

                    ourPriceColumn.Visibility = System.Windows.Visibility.Visible;
                    profitLoss.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    NewEnquiryViewModel = new NewEnquiryViewModel();
                    ourPriceColumn.Visibility = System.Windows.Visibility.Hidden;
                    profitLoss.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        public void SetOrder(Order order)
        {
            this.NewEnquiryViewModel = new NewEnquiryViewModel(order);
            if (!IsNewEnquiry)
            {
                this.NewEnquiryViewModel.IsReadOnly = true;
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
                        if (status == OrderStatusEnum.MaterialsCostAdded)
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

        private void SetButtonText(System.Windows.Controls.Button btn, string text)
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
            EditCompany(NewEnquiryViewModel.Order.Company);
        }

        private void editExistingAgentBtn_Click(object sender, RoutedEventArgs e)
        {
            EditCompany(NewEnquiryViewModel.Order.Agent);
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
                        AddEditProductPopupBox addNewProductPopUp = new AddEditProductPopupBox(Util.GetParentWindow(this));
                        addNewProductPopUp.ProductName = comboBox.Text;
                        if (addNewProductPopUp.ShowDialog() == true)
                        {
                            comboBox.SelectedItem = NewEnquiryViewModel.CreateNewProduct(addNewProductPopUp.ProductName, addNewProductPopUp.StyleId);
                            addBtn.Visibility = System.Windows.Visibility.Collapsed;

                            Button editButton = parentGrid.FindName("editBtn") as Button;
                            if (editButton != null)
                            {
                                editButton.Visibility = System.Windows.Visibility.Visible;
                            }
                        }
                    }
                }
            }
        }

        private void positiveDecisionBtn_Click(object sender, RoutedEventArgs e)
        {
            CommentBox commentBox = new CommentBox(Util.GetParentWindow(this));
            if (commentBox.ShowDialog() == true)
            {
                string userComment = commentBox.Comment;

                if (positiveDecisionBtn.Content.ToString() == "Create")
                {
                    if (NewEnquiryViewModel.HasErrors)
                    {
                        string message = string.Format("Failed to Create !. Fill in the highlighted fields and Click Create again");
                        InformUser(message);
                        return;
                    }
                    Order newOrder = NewEnquiryViewModel.CreateNewOrder(userComment);
                    if (newOrder != null)
                    {
                        SetButtonsVisibility(System.Windows.Visibility.Collapsed);
                        string message = string.Format("Enquiry Successfully Created. ID : {0}", newOrder.OrderID.ToString());
                        InformUser(message);
                        Navigate(OrderManagerTab.AllOrders);
                    }
                }
                else if (positiveDecisionBtn.Content.ToString() == "Approve")
                {
                    try
                    {
                        if (NewEnquiryViewModel.UpdateOrderStatus("approved the Order", userComment, OrderStatusEnum.EnquiryApproved))
                        {
                            SetButtonText(positiveDecisionBtn, "Confirm");
                            SetButtonText(negativeDecisionBtn, "Cancel");
                            string message = "Enquiry approved successfully";
                            InformUser(message);
                            NewEnquiryViewModel.OnPropertyChanged("CanUpdateEnquiry"); //Call to rebind in UI
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
                        if (NewEnquiryViewModel.UpdateOrderStatus("confirmed the Order", userComment, OrderStatusEnum.OrderConfirmed))
                        {
                            SetButtonsVisibility(System.Windows.Visibility.Collapsed);
                            string message = "Enquiry confirmed successfully";
                            InformUser(message);
                            NewEnquiryViewModel.OnPropertyChanged("CanUpdateEnquiry"); //Call to rebind in UI
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
        }

        private void negativeDecisionBtn_Click(object sender, RoutedEventArgs e)
        {

            if (negativeDecisionBtn.Content.ToString() == "Discard")
            {
                PopupBox actionConfirmer = new PopupBox(Util.GetParentWindow(this));
                actionConfirmer.Message = "Are you sure to discard the new enquiry ?";
                actionConfirmer.PopupButton = PopupButton.YesNo;
                bool? result = actionConfirmer.ShowDialog();
                if (result != null && result.Value == true)
                {
                    Navigate(OrderManagerTab.MyTasks);
                }
                return;
            }

            CommentBox commentBox = new CommentBox(Util.GetParentWindow(this));
            if (commentBox.ShowDialog() == true)
            {
                string userComment = commentBox.Comment;
                if (positiveDecisionBtn.Content.ToString() == "Reject")
                {
                    try
                    {
                        if (NewEnquiryViewModel.UpdateOrderStatus("rejected the Enquiry", userComment, OrderStatusEnum.EnquiryRejected))
                        {
                            SetButtonsVisibility(System.Windows.Visibility.Collapsed);
                            InformUser("Enquiry rejected");
                        }
                        else
                            InformUser("Unable to reject enquiry");
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
                        if (NewEnquiryViewModel.UpdateOrderStatus("cancelled the Enquiry", userComment, OrderStatusEnum.EnquiryCancelled))
                        {
                            SetButtonsVisibility(System.Windows.Visibility.Collapsed);
                            InformUser("Enquiry cancelled");
                        }
                        else
                            InformUser("Unable to cancel the enquiry");
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

        }

        private void addNewCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            var newCustomer = NewCompany("Customer", customerComboBox.Text);
            if (newCustomer != null)
            {
                NewEnquiryViewModel.Order.Customer = newCustomer;
            }
        }

        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
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

                            Button editBtn = parentGrid.FindName("editBtn") as Button;
                            if (editBtn != null)
                            {
                                editBtn.Visibility = System.Windows.Visibility.Visible;
                            }
                        }
                        else
                        {
                            addbtn.Visibility = System.Windows.Visibility.Visible;

                            Button editBtn = parentGrid.FindName("editBtn") as Button;
                            if (editBtn != null)
                            {
                                editBtn.Visibility = System.Windows.Visibility.Collapsed;
                            }
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

        private void Navigate(Interfaces_And_Enums.OrderManagerTab tab)
        {
            if (OnNavigateTo != null)
            {
                NewEnquiryViewModel = new NewEnquiryViewModel();
                OnNavigateTo(tab);
            }
        }

        private void internalDeliveryDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (internalDeliveryDate.SelectedDate != null && orderDate.SelectedDate != null)
            {
                TimeSpan tSpan = internalDeliveryDate.SelectedDate.Value.Subtract(orderDate.SelectedDate.Value);
                internalDeliveryDateSpan.Text = tSpan.Days.ToString();
            }
        }

        private void editStyleID_Click(object sender, RoutedEventArgs e)
        {
            Button editBtn = sender as Button;
            if (editBtn != null)
            {
                Grid parentGrid = editBtn.Parent as Grid;

                if (parentGrid != null)
                {
                    ComboBox comboBox = parentGrid.FindName("comboBox") as ComboBox;
                    if (comboBox != null && comboBox.SelectedItem != null)
                    {
                        ProductName productName = comboBox.SelectedItem as ProductName;
                        if(productColumn !=null)
                        {
                            AddEditProductPopupBox addNewProductPopUp = new AddEditProductPopupBox(Util.GetParentWindow(this));
                            addNewProductPopUp.AllowToEditOnlyStyleID = true;
                            addNewProductPopUp.ProductName = productName.Name;
                            addNewProductPopUp.StyleId = productName.StyleID;
                            if (addNewProductPopUp.ShowDialog() == true)
                            {
                                productName.StyleID = addNewProductPopUp.StyleId;
                            }
                        }
                    }
                }
            }
        }
    }
}
