using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.ViewModel.JobOrderControls;
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

namespace ordermanager.Views.UserControls.JobOrderControls
{
    /// <summary>
    /// Interaction logic for DyeingJOControl.xaml
    /// </summary>
    public partial class DyeingJOControl : UserControl, IJobOrderControl
    {
        
        public DyeingJOControl()
        {
            InitializeComponent();
        }

        private DyeingJoViewModel m_ViewModel = null;
        public DyeingJoViewModel ViewModel
        {
            get
            {
                return this.DataContext as DyeingJoViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        public void CreateNewJo(Order order, decimal quantity, PurchaseOrder po, string grnRefNo, GRNReciept receipt, bool jobOrderIssued, JobOrder parentJo)
        {
            ViewModel = new DyeingJoViewModel(order, quantity, po, grnRefNo, receipt, jobOrderIssued,parentJo);
            budgetVsActualControl.Initialize(receipt.OrderedItem, parentJo);
        }

        public void OpenExistingJo(DyeingJO jo)
        {
            ViewModel = new DyeingJoViewModel(jo);
            budgetVsActualControl.Initialize(jo.JobOrder.GRNReciept.OrderedItem, jo.JobOrder);
        }

        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
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
                    
                    ViewModel.Order.DyeingJOes.Add(ViewModel.JO);

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

        public bool ShowPDF()
        {
            try
            {
                DyeingJOGenerator pdfGenerator = new DyeingJOGenerator(ViewModel.JO, ViewModel.GetReportParameters());
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
                if (gridDetails.SelectedItem != null && gridDetails.SelectedItem is DyeingJoItem)
                {
                    ViewModel.Delete(gridDetails.SelectedItem as DyeingJoItem);
                }
            }

        #endregion 
    

           
    }
}
