using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
using System;
using System.Windows;
using System.Windows.Controls;
namespace ordermanager.Views.UserControls
{
    /// <summary>
    /// Interaction logic for JobWorkControl.xaml
    /// </summary>
    public partial class JobWorkControl : UserControl
    {
        JobWorkViewModel ViewModel;
        public JobWorkControl()
        {
            InitializeComponent();
            ViewModel = new JobWorkViewModel();
            this.DataContext = ViewModel;
        }

        private void CreateNewJobOrderForFailedQuantity(JobOrder parentOrder)
        {
            JobOrder newJob = new JobOrder();
            newJob.JobQuantity = parentOrder.QualityFailed.GetValueOrDefault(0);
            newJob.JobOrderType = parentOrder.JobOrderType;
            newJob.Supplier = parentOrder.Supplier;
            newJob.Instructions = parentOrder.Instructions;
            newJob.RequiredDate = parentOrder.RequiredDate;
            newJob.ChargesInINR = parentOrder.ChargesInINR;
            ViewModel.IssueNewJob(newJob);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
        }

        private void tvSuppliers_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvSuppliers.SelectedItem is Company || tvSuppliers.SelectedItem is PurchaseOrder)
            {
                tabControlJobWorks.Visibility = System.Windows.Visibility.Collapsed;
                ViewModel.DiscardChanges();
            }
            else if (tvSuppliers.SelectedItem is GRNReciept)
            {
                GRNReciept recp = tvSuppliers.SelectedItem as GRNReciept;
                if (ViewModel.SelectedReceipt != recp)
                    ViewModel.DiscardChanges();
                tabControlJobWorks.Visibility = System.Windows.Visibility.Visible;
                ViewModel.SelectedReceipt = recp;
            }
        }



        private void IssueToNextJobAfterKnitting_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridKnittingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                JobOrder newJob = new JobOrder();
                newJob.JobQuantity = jOrder.QualityPassed.GetValueOrDefault(0);               
                newJob.Supplier = jOrder.Supplier;   
                IssueToPopupBox issuePopup = new IssueToPopupBox(newJob,DBResources.Instance.AfterKnittingJobs);               
                issuePopup.ShowDialog();
                if (issuePopup.ShowDialog() == true)
                {                    
                    ViewModel.IssueNewJob(issuePopup.JobOrder);
                }
            }
        }


        private void IssueToNextJobAfterDyeing_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridDyeingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                JobOrder newJob = new JobOrder();
                newJob.JobQuantity = jOrder.QualityPassed.GetValueOrDefault(0);              
                newJob.GRNReciept = ViewModel.SelectedReceipt;
                IssueToPopupBox issuePopup = new IssueToPopupBox(newJob, DBResources.Instance.AfterDyeingJobs);
                issuePopup.ShowDialog();
                if (issuePopup.ShowDialog() == true)
                {
                    ViewModel.IssueNewJob(issuePopup.JobOrder);
                }
            }
        }

        private void IssueToNextJobAfterPrinting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IssueToNextJobAfterCompacting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IssueToNextJobAfterWashing_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewKnittingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridKnittingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }

        private void NewDyeingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridDyeingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }


        private void NewPrintingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridPrintingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }

        private void NewCompactingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridCompactingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }

        private void NewWashingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridWashingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }

        private void NewOtherJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridOtherDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }

        private void btnSendForSpecialApproval_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSpecialApproval_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
