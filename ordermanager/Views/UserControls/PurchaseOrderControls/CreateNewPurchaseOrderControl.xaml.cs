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
    /// <summary>
    /// Interaction logic for CreateNewPurchaseOrderControl.xaml
    /// </summary>
    public partial class CreateNewPurchaseOrderControl : UserControl
    {
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

            if (po.Approval.IsApproved == null)
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
            if(negativeBtn.Content.ToString() == "Discard")
                Reset();
            else if (negativeBtn.Content.ToString() == "Reject")
            {
                CommentBox cBox = new CommentBox();
                cBox.ShowDialog();

                ViewModel.PurchaseOrder.Approval.IsApproved = false;

                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine("Rejected By " + DBResources.Instance.CurrentUser.UserName + " at " + DBResources.Instance.GetServerTime().ToString());
                sb.AppendLine("Comments:");
                sb.AppendLine(cBox.Comment);
                ViewModel.PurchaseOrder.Approval.Comments += sb.ToString();
                DBResources.Instance.Save();

                SetUIAccesibility(PurchaseOrderState.Rejeted);
            }
        }

        private void positiveBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (positiveBtn.Content.ToString() == "Generate" || positiveBtn.Content.ToString() == "Submit")
            {
                ViewModel.PurchaseOrder.Validate();
                if (!ViewModel.PurchaseOrder.HasErrors)
                {
                    foreach (ProductMaterialItem item in ViewModel.SelectedItems)
                    {
                        item.PurchaseOrder = ViewModel.PurchaseOrder;
                    }

                    if (ViewModel.PurchaseOrder.Approval == null)
                    {
                        Order.PurchaseOrders.Add(ViewModel.PurchaseOrder);
                        Approval approval = new Approval();
                        approval.ApprovalEntityTypeID = 2;
                        approval.Order = ViewModel.Order;
                        approval.Comments = "Generated by " + DBResources.Instance.CurrentUser.UserName + " at " + DBResources.Instance.GetServerTime().ToString();
                        ViewModel.PurchaseOrder.Approval = approval;
                        approval.PurchaseOrders.Add(ViewModel.PurchaseOrder);
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine();
                        sb.AppendLine("Submitted By " + DBResources.Instance.CurrentUser.UserName + " at " + DBResources.Instance.GetServerTime().ToString());
                        ViewModel.PurchaseOrder.Approval.Comments += sb.ToString();
                        ViewModel.PurchaseOrder.Approval.IsApproved = null;
                    }

                    
                    DBResources.Instance.Save();
                    

                    if (positiveBtn.Content.ToString() == "Generate")
                    {
                        SetUIAccesibility(PurchaseOrderState.Generated);
                        InformUser("Purchase Order Successfully Created");
                        Reset();
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
                
                foreach (ProductMaterialItem item in ViewModel.SelectedItems)
                {
                    OrderedItem itemToOrder = new OrderedItem { ProductMaterialItem = item, OrderedQuantity = item.Quantity };
                    item.SupplierWrapper = ViewModel.PurchaseOrder.Company;
                    item.LastPOGeneratedOn = DBResources.Instance.GetServerTime();
                    ViewModel.PurchaseOrder.OrderedItems.Add(itemToOrder);
                    item.PurchaseOrder = ViewModel.PurchaseOrder;
                }


                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                sb.AppendLine("Approved By " + DBResources.Instance.CurrentUser.UserName + " at " + DBResources.Instance.GetServerTime().ToString());
                ViewModel.PurchaseOrder.Approval.Comments += sb.ToString();
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
                    System.Diagnostics.Process.Start(generatedFile);
                }
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
                    editExistingSupplier.IsEnabled = true;
                    ViewModel.IsReadOnly = false;
                    break;
                case PurchaseOrderState.Generated:
                case PurchaseOrderState.Submitted:
                    ViewModel.IsReadOnly = true;
                    positiveBtn.Content = "Approve";
                    negativeBtn.Content = "Reject";

                    btnChooseItems.Visibility = System.Windows.Visibility.Collapsed;
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

                    if (DBResources.Instance.CurrentUser.UserRole.CanGeneratePurchaseOrder)
                    {
                        positiveBtn.Visibility = System.Windows.Visibility.Visible;
                        negativeBtn.Visibility = System.Windows.Visibility.Visible;
                        btnChooseItems.Visibility = System.Windows.Visibility.Visible;
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
                    positiveBtn.Visibility = System.Windows.Visibility.Visible;
                    negativeBtn.Visibility = System.Windows.Visibility.Collapsed;
                    btnChooseItems.Visibility = System.Windows.Visibility.Collapsed;
                    ViewModel.IsReadOnly = true;
                    break;
            }
        }
    }


    public enum PurchaseOrderState
    {
        New, 
        Generated,
        Submitted,
        Rejeted,
        Approved,
    }
}
