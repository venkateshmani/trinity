using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
using System;
using System.Collections.Generic;
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
            this.Loaded += JobOrderJOView_Loaded;
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

        void JobOrderJOView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new JobbOrderJOViewModel();
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
            JobOrder jOrder = jobOrderDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                IssueNextJob(jOrder);
            }
        }

        private void IssueNextJob(JobOrder jOrder)
        {
            JobOrder newJob = new JobOrder();
            newJob.JobQuantity = jOrder.QualityPassed.GetValueOrDefault(0);
            newJob.GRNReciept = jOrder.GRNReciept;
            IssueToPopupBox issuePopup = new IssueToPopupBox(newJob, DBResources.Instance.AfterKnittingJobs);
            if (issuePopup.ShowDialog() == true)
            {
                ViewModel.IssueNewJob(issuePopup.JobOrder);
            }
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
            try
            {
                JobOrder parentOrder = jobOrderDetails.SelectedItem as JobOrder;
                JobOrder newJob = new JobOrder();
                newJob.JobQuantity = parentOrder.QualityFailed.GetValueOrDefault(0);
                newJob.JobOrderType = parentOrder.JobOrderType;
                newJob.Supplier = parentOrder.Supplier;
                newJob.Instructions = parentOrder.Instructions;
                newJob.RequiredDate = parentOrder.RequiredDate;
                newJob.ChargesInINR = parentOrder.ChargesInINR;
                ViewModel.IssueNewJob(newJob);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
