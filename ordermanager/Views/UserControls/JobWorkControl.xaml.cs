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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
        }

        private void IssueNextJob(JobOrder jOrder)
        {
            JobOrder newJob = new JobOrder();
            newJob.JobQuantity = jOrder.QualityPassed.GetValueOrDefault(0);
            newJob.GRNReciept = ViewModel.SelectedReceipt;
            IssueToPopupBox issuePopup = new IssueToPopupBox(newJob, DBResources.Instance.AfterKnittingJobs);
            issuePopup.ShowDialog();
            if (issuePopup.ShowDialog() == true)
            {
                ViewModel.IssueNewJob(issuePopup.JobOrder);
            }
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
                IssueNextJob(jOrder);
            }
        }

        private void IssueToNextJobAfterDyeing_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridDyeingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                IssueNextJob(jOrder);
            }
        }

        private void IssueToNextJobAfterPrinting_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridPrintingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                IssueNextJob(jOrder);
            }
        }

        private void IssueToNextJobAfterCompacting_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridCompactingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                IssueNextJob(jOrder);
            }
        }

        private void IssueToNextJobAfterWashing_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridWashingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                IssueNextJob(jOrder);
            }
        }


        private void NewKnittingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridKnittingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }

        private void NewDyeingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridDyeingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
               ViewModel.CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }


        private void NewPrintingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridPrintingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }

        private void NewCompactingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridCompactingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }

        private void NewWashingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridWashingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }

        private void NewOtherJobOrder_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridOtherDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.CreateNewJobOrderForFailedQuantity(jOrder);
            }
        }

        private void btnSendForSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridKnittingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SendForSpecialApproval(jOrder);
            }
        }

        private void btnSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridKnittingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SpecialApproval(jOrder);
            }
        }
    }
}
