using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
using ordermanager.Views.UserControls.JobOrderControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for JobOrderJOView.xaml
    /// </summary>
    public partial class JobOrderJOView : UserControl
    {
        BackgroundWorker jobOrderSearcher = null;
        ProgressUpdateWindow progressUpdateWindow = null;
        public JobOrderJOView()
        {
            InitializeComponent();
            jobOrderSearcher = new BackgroundWorker();
            jobOrderSearcher.WorkerReportsProgress = true;
            jobOrderSearcher.WorkerSupportsCancellation = true;
            jobOrderSearcher.DoWork += jobOrderSearcher_DoWork;
            jobOrderSearcher.RunWorkerCompleted += jobOrderSearcher_RunWorkerCompleted;
        }

        void jobOrderSearcher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            JobOrder matchedJobOrder = e.Result as JobOrder;
            if (matchedJobOrder != null)
            {
                supplierList.SelectedItem = matchedJobOrder.Supplier;
                jobOrderDetails.SelectedItem = matchedJobOrder;
            }
            else
            {
                InformUser("No match found");
            }

            progressUpdateWindow.Close();
        }


        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
        }

        void jobOrderSearcher_DoWork(object sender, DoWorkEventArgs e)
        {
            string searchStringArgument = e.Argument as string;
            string searchText = searchStringArgument.ToLower();
            foreach (Company supplier in ViewModel.Suppliers)
            {
                foreach (JobOrder jobOrder in supplier.JobOrders)
                {
                    if (!jobOrder.IsIssued && jobOrder.JobOrderNumber.ToLower() == searchText)
                    {
                        e.Result = jobOrder;
                        break;
                    }
                }
            }
        }

        public void SetOrder(Order order)
        {
            ViewModel = new JobbOrderJOViewModel(order);
        }

        private JobbOrderJOViewModel m_ViewModel = null;
        public JobbOrderJOViewModel ViewModel
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

        private void SupplierList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedSupplier = supplierList.SelectedItem as Company;
        }

        private void btnIsue_Click(object sender, RoutedEventArgs e)
        {
            Button btnIssue = sender as Button;
            JobOrder jOrder = jobOrderDetails.SelectedItem as JobOrder;

            var nextTypes = GetNextJobOrderTypes(jOrder);

            ContextMenu cMenu = new ContextMenu();
            foreach (var type in nextTypes)
            {
                MenuItem mItem = new MenuItem();
                mItem.Header = type.Type;
                mItem.Click += mItem_Click;
                cMenu.Items.Add(mItem);
            }

            btnIssue.ContextMenu = cMenu;
            btnIssue.ContextMenu.IsOpen = true;
        }

        private ObservableCollection<JobOrderType> GetNextJobOrderTypes(JobOrder jOrder)
        {
            ObservableCollection<JobOrderType> nextTypes;
            switch (jOrder.JobOrderTypeID)
            {
                case 1:
                    nextTypes = DBResources.Instance.AfterKnittingJobs;
                    break;
                case 2:
                    nextTypes = DBResources.Instance.AfterDyeingJobs;
                    break;
                case 3:
                    nextTypes = DBResources.Instance.AfterPrintingJobs;
                    break;
                case 4:
                    nextTypes = DBResources.Instance.AfterCompactingJobs;
                    break;
                case 5:
                    nextTypes = DBResources.Instance.AfterWashingJobs;
                    break;
                case 6:
                    nextTypes = DBResources.Instance.AfterOtherJobs;
                    break;
                default:
                    return null;
            }

            return nextTypes;
        }

       

        private void btnSendForApproval_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JobOrder jOrder = jobOrderDetails.SelectedItem as JobOrder;
                if (jOrder != null)
                {
                    ViewModel.SendForSpecialApproval(jOrder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JobOrder jOrder = jobOrderDetails.SelectedItem as JobOrder;
                if (jOrder != null)
                {
                    ViewModel.SpecialApproval(jOrder);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReissue_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = jobOrderDetails.SelectedItem as JobOrder;
            if (jOrder != null && jOrder.ValidateIssueAndReceiptDetails())
            {
                Button btnIssue = sender as Button;
                var nextTypes = GetNextJobOrderTypes(jOrder);

                ContextMenu cMenu = new ContextMenu();
                foreach (var type in nextTypes)
                {
                    MenuItem mItem = new MenuItem();
                    mItem.Header = type.Type;
                    mItem.Click += Reissue_mItem_Click;
                    cMenu.Items.Add(mItem);
                }

                btnIssue.ContextMenu = cMenu;
                btnIssue.ContextMenu.IsOpen = true;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBResources.Instance.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            PerformSearch();
        }

        public void PerformSearch()
        {
            jobOrderSearcher.RunWorkerAsync(searchBox.Text);
            progressUpdateWindow = new ProgressUpdateWindow();
            progressUpdateWindow.Width = 650;
            progressUpdateWindow.UpdateString = "Performing search in the database. It may take a while, please wait !";
            progressUpdateWindow.ShowDialog();
        }

        private void searchBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PerformSearch();
            }
        }

        #region ReIssue

        void Reissue_mItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            JobOrder jOrder = jobOrderDetails.SelectedItem as JobOrder;

            switch (item.Header.ToString())
            {
                case "Knitting":
                    CreateJoWindow knittWindow = new CreateJoWindow();
                    knittWindow.Order = jOrder.GRNReciept.OrderedItem.PurchaseOrder.Order;
                    knittWindow.PurchaseOrder = jOrder.GRNReciept.OrderedItem.PurchaseOrder;
                    knittWindow.Quantity = jOrder.QualityFailed.GetValueOrDefault(0);
                    knittWindow.GRNRefNo = jOrder.GRNReciept.GRNReciptID.ToString();
                    knittWindow.GRNReciept = jOrder.GRNReciept;
                    knittWindow.ParentJobOrder = jOrder;
                    jOrder.IsIssued = true;
                    knittWindow.InitializeForKnitting();
                    knittWindow.ShowDialog();
                    break;
                case "Dyeing":
                    CreateJoWindow dyeWindow = new CreateJoWindow();
                    dyeWindow.Order = jOrder.GRNReciept.OrderedItem.PurchaseOrder.Order;
                    dyeWindow.PurchaseOrder = jOrder.GRNReciept.OrderedItem.PurchaseOrder;
                    dyeWindow.ParentJobOrder = jOrder;
                    dyeWindow.Quantity = jOrder.QualityFailed.GetValueOrDefault(0);
                    dyeWindow.GRNRefNo = jOrder.GRNReciept.GRNReciptID.ToString();
                    dyeWindow.GRNReciept = jOrder.GRNReciept;
                    jOrder.IsIssued = true;
                    dyeWindow.InitializeForDyeing();
                    dyeWindow.ShowDialog();
                    break;
                case "Printing":
                case "Compacting":
                case "Washing":
                case "Other":
                case "Stock":

                     JobOrder newJob = new JobOrder();
                     newJob.GRNReciept = jOrder.GRNReciept;
                     newJob.JobQuantity = jOrder.QualityFailed.GetValueOrDefault(0);
                     newJob.JobOrderType = jOrder.JobOrderType;
                     newJob.Supplier = jOrder.Supplier;
                     newJob.Instructions = jOrder.Instructions;
                     newJob.RequiredDate = jOrder.RequiredDate;
                     newJob.ChargesInINR = jOrder.ChargesInINR;
                     newJob.JobOrder2 = jOrder;
                    IssueToPopupBox issuePopup = new IssueToPopupBox(newJob, GetNextJobOrderTypes(jOrder), item.Header.ToString());
                    if (issuePopup.ShowDialog() == true)
                    {
                        jOrder.FailedQuantityIssued = true;
                        if (ViewModel.IssueNewJob(issuePopup.JobOrder))
                            jOrder.Refresh();
                    }
                    break;
            }

            jOrder.Refresh();
            jOrder.RefreshAllProperties();
        }

        #endregion 

        #region Issue

        void mItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            JobOrder jOrder = jobOrderDetails.SelectedItem as JobOrder;

            switch (item.Header.ToString())
            {
                case "Knitting":
                    CreateJoWindow knittWindow = new CreateJoWindow();
                    knittWindow.Order = jOrder.GRNReciept.OrderedItem.PurchaseOrder.Order;
                    knittWindow.PurchaseOrder = jOrder.GRNReciept.OrderedItem.PurchaseOrder;
                    knittWindow.Quantity = jOrder.QualityPassed.Value;
                    knittWindow.GRNRefNo = jOrder.GRNReciept.GRNReciptID.ToString();
                    knittWindow.GRNReciept = jOrder.GRNReciept;
                    knittWindow.ParentJobOrder = jOrder;
                    jOrder.IsIssued = true;
                    knittWindow.InitializeForKnitting();
                    knittWindow.ShowDialog();
                    break;
                case "Dyeing":
                    CreateJoWindow dyeWindow = new CreateJoWindow();
                    dyeWindow.Order = jOrder.GRNReciept.OrderedItem.PurchaseOrder.Order;
                    dyeWindow.PurchaseOrder = jOrder.GRNReciept.OrderedItem.PurchaseOrder;
                    dyeWindow.ParentJobOrder = jOrder;
                    dyeWindow.Quantity = jOrder.QualityPassed.Value;
                    dyeWindow.GRNRefNo = jOrder.GRNReciept.GRNReciptID.ToString();
                    dyeWindow.GRNReciept = jOrder.GRNReciept;
                    jOrder.IsIssued = true;
                    dyeWindow.InitializeForDyeing();
                    dyeWindow.ShowDialog();
                    break;
                case "Printing":
                case "Compacting":
                case "Washing":
                case "Other":
                case "Stock":
                    JobOrder newJob = new JobOrder();
                    newJob.JobQuantity = jOrder.QualityPassed.GetValueOrDefault(0);
                    newJob.GRNReciept = jOrder.GRNReciept;
                    newJob.JobOrder2 = jOrder;
                    IssueToPopupBox issuePopup = new IssueToPopupBox(newJob, GetNextJobOrderTypes(jOrder), item.Header.ToString());
                    issuePopup.MaterialName = jOrder.RecievedAsWrapper;
                    if (issuePopup.ShowDialog() == true)
                    {
                        jOrder.IsIssued = true;
                        if (ViewModel.IssueNewJob(issuePopup.JobOrder))
                            jOrder.Refresh();
                    }
                    break;
            }

            jOrder.Refresh();
            jOrder.RefreshAllProperties();
        }

        #endregion 
    }
}
