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
using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.ViewModel.JobOrderControls;
using ordermanager.Views.PopUps;

namespace ordermanager.Views.UserControls.JobOrderControls
{
    /// <summary>
    /// Interaction logic for CompactingJo.xaml
    /// </summary>
    public partial class CompactingJoControl : UserControl, IJobOrderControl
    {
        public CompactingJoControl()
        {
            InitializeComponent();
        }


        
        public CompactingJoViewModel ViewModel
        {
            get { return this.DataContext as CompactingJoViewModel; }
            set { this.DataContext = value; }
        }


        public void CreateNewJo(Order order, decimal quantity, PurchaseOrder po, string grnRefNo, GRNReciept receipt, bool jobOrderIssued, JobOrder parentJo)
        {
            ViewModel = new CompactingJoViewModel(order, quantity, po, grnRefNo, receipt, jobOrderIssued, parentJo);
        }

        public void OpenExistingJo(CompactingJo jo)
        {
            ViewModel = new CompactingJoViewModel(jo);
        }

        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
        }

        #region IJobOrderControl Implementation

        public bool Submit()
        {
            try
            {
                StringBuilder comment = new StringBuilder();
                comment.AppendLine("Submitted on " + DBResources.Instance.GetServerTime() + " by " + DBResources.Instance.CurrentUser.UserName);
                comment.Append(ViewModel.JO.Approval.Comments);

                ViewModel.JO.Approval.Comments = comment.ToString();
                ViewModel.JO.Approval.IsApproved = null;

                DBResources.Instance.Save();

                ViewModel.JO.RefreshInfoJobOrderInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public bool Approve()
        {
            try
            {
                StringBuilder comment = new StringBuilder();
                comment.AppendLine("Approved on " + DBResources.Instance.GetServerTime() + " by " + DBResources.Instance.CurrentUser.UserName);
                comment.Append(ViewModel.JO.Approval.Comments);

                ViewModel.JO.Approval.Comments = comment.ToString();
                ViewModel.JO.Approval.IsApproved = true;

                
                foreach (var item in ViewModel.JO.CompactingJoItems)
                {
                    JobOrder jo = new JobOrder();
                    jo.Company = ViewModel.JO.Company;
                    jo.GRNReciept = ViewModel.JO.GRNReciept;
                    jo.ChargesInINR = item.NetQtyWrapper * item.RatePerKgWrapper;
                    jo.RequiredDateWrapper = DateTime.Now;
                    jo.JobOrderTypeID = 4;
                    jo.JobOrder2 = ViewModel.JO.JobOrder;
                    jo.Instructions = ViewModel.JO.TermsAndConditionsWrapper;
                    jo.JobQuantity = item.NetQtyWrapper;
                    jo.CompactingJoItems.Add(item);
                    item.JobOrder = jo;
                }

                DBResources.Instance.Save();

                ViewModel.JO.RefreshInfoJobOrderInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public bool Reject()
        {
            try
            {

                CommentBox cBox = new CommentBox();
                if (cBox.ShowDialog() == true)
                {
                    StringBuilder comment = new StringBuilder();
                    comment.AppendLine("Rejected on " + DBResources.Instance.GetServerTime() + " by " + DBResources.Instance.CurrentUser.UserName);
                    comment.AppendLine(cBox.Comment);
                    comment.Append(ViewModel.JO.Approval.Comments);

                    ViewModel.JO.Approval.Comments = comment.ToString();
                    ViewModel.JO.Approval.IsApproved = false;

                    DBResources.Instance.Save();

                    ViewModel.JO.RefreshInfoJobOrderInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }


        public bool Generate()
        {
            try
            {
                if (ViewModel.JO.Validate() == false)
                {
                    Approval approval = new Approval();
                    approval.Order = ViewModel.JO.Order;
                    approval.IsApproved = null;
                    approval.ApprovalEntityTypeID = 3;

                    StringBuilder comments = new StringBuilder();
                    comments.Append("Generated on " + DBResources.Instance.GetServerTime() + " by " + DBResources.Instance.CurrentUser.UserName);

                    approval.Comments = comments.ToString();
                    ViewModel.JO.Approval = approval;

                    ViewModel.Order.CompactingJoes.Add(ViewModel.JO);

                    DBResources.Instance.Save();

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Job Order Successfully Created");
                    sb.AppendLine("Submitted For Approval !.");
                    InformUser(sb.ToString());

                    ViewModel.JO.RefreshInfoJobOrderInfo();
                }
                else
                {
                    InformUser("Fix the Highlighted Errors and try again");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        public bool ShowPDF()
        {
            try
            {
                CompactingJoGenerator pdfGenerator = new CompactingJoGenerator(ViewModel.JO, ViewModel.GetReportParameters());
                string generatedFile = pdfGenerator.GenerateJobOrder();
                if (string.IsNullOrEmpty(generatedFile))
                {
                    InformUser("Failed to Generate !");
                }
                else
                {
                    System.Diagnostics.Process.Start(generatedFile);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }


        public bool Discard()
        {
            throw new NotImplementedException();
        }


        #endregion

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


        #region Items Management

        private void btnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            ViewModel.Add();
        }

        private void btnRemove_Click_1(object sender, RoutedEventArgs e)
        {
            if (gridDetails.SelectedItem != null && gridDetails.SelectedItem is CompactingJoItem)
            {
                ViewModel.Delete(gridDetails.SelectedItem as CompactingJoItem);
            }
        }

        #endregion 
    
    }
}
