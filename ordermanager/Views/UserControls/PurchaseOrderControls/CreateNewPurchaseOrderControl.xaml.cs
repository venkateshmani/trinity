using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.ViewModel.PurchaseOrderControl;
using ordermanager.Views.PopUps;
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

namespace ordermanager.Views.UserControls.PurchaseOrderControls
{
    public delegate void POStatusChanged(PurchaseOrderState state);
    /// <summary>
    /// Interaction logic for CreateNewPurchaseOrderControl.xaml
    /// </summary>
    public partial class CreateNewPurchaseOrderControl : UserControl
    {
        public event POStatusChanged PurchaseOrderStatusChanged = null;
        public CreateNewPurchaseOrderControl()
        {
            InitializeComponent();
        }

        private CreateNewPurchaseOrderViewModel m_ViewModel = null;
        public CreateNewPurchaseOrderViewModel ViewModel
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

        private Order m_Order = null;
        public Order Order
        {
            get
            {
                return m_Order;
            }
            set
            {
                m_Order = value;
            }
        }

        public void SetOrder(Order order)
        {
            if (ViewModel == null || ViewModel.Order != order)
            {
                this.Order = order;
                ViewModel = new CreateNewPurchaseOrderViewModel(order);
            }
            SetUIAccesibility(PurchaseOrderState.New);
        }

        public void SetOrder(Order order, PurchaseOrder po)
        {
            this.Order = order;
            ViewModel = new CreateNewPurchaseOrderViewModel(order, po);

            if (po.Approval == null)
            {
                btnChooseItems.Visibility = System.Windows.Visibility.Collapsed;
                poCurrencySelection.IsEnabled = false;
            }
            else if (po.Approval.IsApproved == null)
            {
                SetUIAccesibility(PurchaseOrderState.Generated);                
            }
            else if (po.Approval.IsApproved.Value == true)
            {
                SetUIAccesibility(PurchaseOrderState.Approved);
            }
            else
            {
                SetUIAccesibility(PurchaseOrderState.Rejeted);
            }
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

        #region Purchase Order Generate & Discard Management

            private void InformUser(string message)
            {
                PopupBox informer = new PopupBox();
                informer.Message = message;
                informer.PopupButton = PopupButton.OK;
                informer.ShowDialog();
            }

            private void btnDiscard_Click(object sender, RoutedEventArgs e)
            {
              
            }

            public void Reset()
            {
                ViewModel.ResetUserSelection();
                ViewModel = null;
                ViewModel = new CreateNewPurchaseOrderViewModel(Order);
            }

        #endregion 

        private void btnChooseItems_Click_1(object sender, RoutedEventArgs e)
        {
            BillOfMaterialBrowser bomBrowser = new BillOfMaterialBrowser(ViewModel.Order, ViewModel.PurchasableMaterials);
            bomBrowser.ShowDialog();
        }

        private void negativeBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (negativeBtn.Content.ToString() == "Discard")
            {
                Reset();
                if (PurchaseOrderStatusChanged != null)
                {
                    PurchaseOrderStatusChanged(PurchaseOrderState.Discarded);
                }
            }
            else if (negativeBtn.Content.ToString() == "Reject")
            {
                CommentBox cBox = new CommentBox();
                cBox.ShowDialog();

                ViewModel.PurchaseOrder.Approval.IsApproved = false;

                StringBuilder sb = new StringBuilder();

                sb.AppendLine("Rejected By " + DBResources.Instance.CurrentUser.UserName + " at " + DBResources.Instance.GetServerTime().ToString());
                sb.AppendLine(cBox.Comment);
                sb.AppendLine();
                ViewModel.PurchaseOrder.Approval.Comments = sb.ToString() + ViewModel.PurchaseOrder.Approval.Comments;
                DBResources.Instance.Save();

                SetUIAccesibility(PurchaseOrderState.Rejeted);
            }

            ViewModel.PurchaseOrder.RefreshUIProperties();
        }

        private void positiveBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (positiveBtn.Content.ToString() == "Generate" || positiveBtn.Content.ToString() == "Submit")
            {
                ViewModel.PurchaseOrder.Validate();
                if (!ViewModel.PurchaseOrder.HasErrors)
                {
                    if (positiveBtn.Content.ToString() == "Generate")
                    {
                        ViewModel.PurchaseOrder = DBResources.Instance.Context.PurchaseOrders.Add(ViewModel.PurchaseOrder);
                    }

                    foreach (IPurchaseOrderItem item in ViewModel.SelectedItems)
                    {
                        item.PurchaseOrder = ViewModel.PurchaseOrder;
                    }

                    if (ViewModel.PurchaseOrder.Approval == null)
                    {
                        Order.PurchaseOrders.Add(ViewModel.PurchaseOrder);
                        Approval approval = new Approval();
                        approval.ApprovalEntityTypeID = 2;
                        approval.Order = ViewModel.Order;
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Generated by " + DBResources.Instance.CurrentUser.UserName + " at " + DBResources.Instance.GetServerTime().ToString());
                        sb.AppendLine();
                        approval.Comments = sb.ToString();
                        ViewModel.PurchaseOrder.Approval = approval;
                        approval.PurchaseOrders.Add(ViewModel.PurchaseOrder);
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Submitted By " + DBResources.Instance.CurrentUser.UserName + " at " + DBResources.Instance.GetServerTime().ToString());
                        sb.AppendLine();
                        ViewModel.PurchaseOrder.Approval.Comments = sb.ToString() + ViewModel.PurchaseOrder.Approval.Comments;
                        ViewModel.PurchaseOrder.Approval.IsApproved = null;
                    }

                    DBResources.Instance.Save();
                    

                    if (positiveBtn.Content.ToString() == "Generate")
                    {
                        SetUIAccesibility(PurchaseOrderState.Generated);
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Purchase Order Successfully Created");
                        sb.AppendLine("Submitted For Approval !.");
                        InformUser(sb.ToString());
                        Reset();
                        if (PurchaseOrderStatusChanged != null)
                        {
                            PurchaseOrderStatusChanged(PurchaseOrderState.Generated);
                        }
                    }
                    else
                    {
                        SetUIAccesibility(PurchaseOrderState.Submitted);
                        InformUser("Purchase Order Successfully Submitted");
                    }
                }
                else
                {
                    InformUser("Fix the Highlighted Errors and try again");
                }
            }
            else if(positiveBtn.Content.ToString() == "Approve")
            {
                
                foreach (IPurchaseOrderItem item in ViewModel.SelectedItems)
                {
                    if (item is ProductMaterialItem)
                    {
                        ProductMaterialItem pmItem = item as ProductMaterialItem;
                        OrderedItem itemToOrder = new OrderedItem { ProductMaterialItem = pmItem, OrderedQuantity = pmItem.Quantity };
                        itemToOrder.CostWrapper = item.CostWrapper;
                        itemToOrder.TaxPerUnitWrapper = item.TaxPerUnitWrapper;
                        pmItem.SupplierWrapper = ViewModel.PurchaseOrder.Company;
                        pmItem.LastPOGeneratedOn = DBResources.Instance.GetServerTime();
                        ViewModel.PurchaseOrder.OrderedItems.Add(itemToOrder);
                        item.PurchaseOrder = ViewModel.PurchaseOrder;
                    }
                }


                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Approved By " + DBResources.Instance.CurrentUser.UserName + " at " + DBResources.Instance.GetServerTime().ToString());
                sb.AppendLine();
                ViewModel.PurchaseOrder.Approval.Comments = sb.ToString() + ViewModel.PurchaseOrder.Approval.Comments;
                ViewModel.PurchaseOrder.Approval.IsApproved = true;

                DBResources.Instance.Save();

                SetUIAccesibility(PurchaseOrderState.Approved);
            }
            else if (positiveBtn.Content.ToString() == "PDF")
            {
                PurchaseOrderPDFGenerator pdfGenerator = new PurchaseOrderPDFGenerator(ViewModel.PurchaseOrder);
                string generatedFile = pdfGenerator.GeneratePurchaseOrder();
                if (string.IsNullOrEmpty(generatedFile))
                {
                    InformUser("Failed to Generate !");
                }
                else
                {
                    //Initiate PDF Viewer
                    System.Diagnostics.Process.Start(generatedFile);

                    //Initiate New Outlook Message
                    try
                    {
                        InitiateNewOutlookMessage(generatedFile);
                    }
                    catch
                    {

                    }
                }
            }

            ViewModel.PurchaseOrder.RefreshUIProperties();
        }

        private void InitiateNewOutlookMessage(string generatedFile)
        {
            Microsoft.Win32.RegistryKey key =
            Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\microsoft\\windows\\currentversion\\app paths\\OUTLOOK.EXE");
            string path = (string)key.GetValue("Path");
            if (path != null)
            {
                string processStartInfo = string.Format(@"outlook.exe /c ipm.note /a {0}", generatedFile);
                System.Diagnostics.Process.Start(processStartInfo);
            }
        }

        private void SetUIAccesibility(PurchaseOrderState state)
        {
            switch (state)
            {
                case PurchaseOrderState.New:
                    positiveBtn.Content = "Generate";
                    negativeBtn.Content = "Discard";
                    positiveBtn.Visibility = System.Windows.Visibility.Visible;
                    negativeBtn.Visibility = System.Windows.Visibility.Visible;
                    btnChooseItems.Visibility = System.Windows.Visibility.Visible;
                    commentsBtn.Visibility = System.Windows.Visibility.Hidden;
                    editExistingSupplier.IsEnabled = true;
                    poCurrencySelection.IsEnabled = true;
                    ViewModel.IsReadOnly = false;
                    break;
                case PurchaseOrderState.Generated:
                case PurchaseOrderState.Submitted:
                    ViewModel.IsReadOnly = true;
                    positiveBtn.Content = "Approve";
                    negativeBtn.Content = "Reject";
                    poCurrencySelection.IsEnabled = false;
                    btnChooseItems.Visibility = System.Windows.Visibility.Collapsed;
                    commentsBtn.Visibility = System.Windows.Visibility.Visible;
                    if (DBResources.Instance.CurrentUser.UserRole.CanApprovePurchaseOrder)
                    {
                        positiveBtn.Visibility = System.Windows.Visibility.Visible;
                        negativeBtn.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        positiveBtn.Visibility = System.Windows.Visibility.Collapsed;
                        negativeBtn.Visibility = System.Windows.Visibility.Collapsed;
                    }
                    break;
                case PurchaseOrderState.Rejeted:
                    positiveBtn.Content = "Submit";
                    negativeBtn.Content = "Delete";
                    poCurrencySelection.IsEnabled = true;
                    commentsBtn.Visibility = System.Windows.Visibility.Visible;
                    if (DBResources.Instance.CurrentUser.UserRole.CanGeneratePurchaseOrder)
                    {
                        positiveBtn.Visibility = System.Windows.Visibility.Visible;
                        negativeBtn.Visibility = System.Windows.Visibility.Visible;

                        if (ViewModel.PurchaseOrder.OrderedItems.Count == 0)
                        {
                            btnChooseItems.Visibility = System.Windows.Visibility.Visible;
                        }
                        else
                        {
                            btnChooseItems.Visibility = System.Windows.Visibility.Collapsed;
                            poCurrencySelection.IsEnabled = false;
                        }
                        ViewModel.IsReadOnly = false;
                    }
                    else
                    {
                        positiveBtn.Visibility = System.Windows.Visibility.Collapsed;
                        negativeBtn.Visibility = System.Windows.Visibility.Collapsed;
                        btnChooseItems.Visibility = System.Windows.Visibility.Collapsed;
                        ViewModel.IsReadOnly = true;
                    }
                    break;
                case PurchaseOrderState.Approved:
                    positiveBtn.Content = "PDF";
                    negativeBtn.Content = "";
                    poCurrencySelection.IsEnabled = false;
                    positiveBtn.Visibility = System.Windows.Visibility.Visible;
                    negativeBtn.Visibility = System.Windows.Visibility.Collapsed;
                    btnChooseItems.Visibility = System.Windows.Visibility.Collapsed;
                    ViewModel.IsReadOnly = true;
                    break;
            }
        }

        private void commentsBtn_Click_1(object sender, RoutedEventArgs e)
        {
            CommentBox cBox = new CommentBox();
            cBox.UpdateBtnText = "Close";
            cBox.Title = "Comments";
            cBox.IsReadOnly = true;
            cBox.Comment = ViewModel.PurchaseOrder.Approval.Comments;
            cBox.ShowDialog();
        }

        private void poCurrencySelection_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }


    public enum PurchaseOrderState
    {
        New, 
        Generated,
        Submitted,
        Rejeted,
        Approved,
        Discarded,
    }
}
