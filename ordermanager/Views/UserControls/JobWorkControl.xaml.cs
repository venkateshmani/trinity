using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
namespace ordermanager.Views.UserControls
{
    /// <summary>
    /// Interaction logic for JobWorkControl.xaml
    /// </summary>
    public partial class JobWorkControl : UserControl
    {
        public JobWorkControl()
        {
            InitializeComponent();
            tabControlJobWorks.Visibility = System.Windows.Visibility.Collapsed;
            ViewModel = new JobWorkViewModel();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();
        }


        public JobWorkViewModel ViewModel
        {
            get
            {
                return this.DataContext as JobWorkViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void IssueNextJob(JobOrder jOrder, ObservableCollection<JobOrderType> jobTypes)
        {
            JobOrder newJob = new JobOrder();
            newJob.JobQuantity = jOrder.QualityPassed.GetValueOrDefault(0);
            newJob.GRNReciept = jOrder.GRNReciept;
            IssueToPopupBox issuePopup = new IssueToPopupBox(newJob, jobTypes, string.Empty);
            if (issuePopup.ShowDialog() == true)
            {
                jOrder.IsIssued = true;
                if (issuePopup.JobOrder.JobOrderType.Type.ToLower() == "stock")
                {
                    if (ViewModel.IssueToStock(issuePopup.JobOrder.JobQuantity))
                        jOrder.Refresh();
                }
                else
                {
                    if (ViewModel.IssueNewJob(issuePopup.JobOrder))
                        jOrder.Refresh();
                }
            }
        }

        private void CreateNewJobOrderForFailedQuantity(JobOrder jOrder)
        {
            if (jOrder != null && jOrder.ValidateIssueAndReceiptDetails())
            {
                JobOrder newJob = new JobOrder();
                newJob.GRNReciept = jOrder.GRNReciept;
                newJob.JobQuantity = jOrder.QualityFailed.GetValueOrDefault(0);
                newJob.JobOrderType = jOrder.JobOrderType;
                newJob.Supplier = jOrder.Supplier;
                newJob.Instructions = jOrder.Instructions;
                newJob.RequiredDate = jOrder.RequiredDate;
                newJob.ChargesInINR = jOrder.ChargesInINR;

                IssueToPopupBox issuePopup = new IssueToPopupBox(newJob);
                if (issuePopup.ShowDialog() == true)
                {
                    jOrder.FailedQuantityIssued = true;
                    if (ViewModel.IssueNewJob(issuePopup.JobOrder))
                        jOrder.Refresh();
                }
            }
        }   

        #region [Issue To Next Job]
        private void IssueToNextJob(JobOrder jOrder, ObservableCollection<JobOrderType> orderTypes)
        {
            if (jOrder != null && jOrder.ValidateIssueAndReceiptDetails())
            {
                IssueNextJob(jOrder, orderTypes);
            }
        }
        private void IssueToNextJobAfterKnitting_Click(object sender, RoutedEventArgs e)
        {
            IssueToNextJob(gridKnittingDetails.SelectedItem as JobOrder, DBResources.Instance.AfterKnittingJobs);
        }

        private void IssueToNextJobAfterDyeing_Click(object sender, RoutedEventArgs e)
        {
            IssueToNextJob(gridKnittingDetails.SelectedItem as JobOrder, DBResources.Instance.AfterDyeingJobs);
        }

        private void IssueToNextJobAfterPrinting_Click(object sender, RoutedEventArgs e)
        {
            IssueToNextJob(gridPrintingDetails.SelectedItem as JobOrder, DBResources.Instance.AfterPrintingJobs);
        }

        private void IssueToNextJobAfterCompacting_Click(object sender, RoutedEventArgs e)
        {
            IssueToNextJob(gridCompactingDetails.SelectedItem as JobOrder, DBResources.Instance.AfterCompactingJobs);
        }

        private void IssueToNextJobAfterWashing_Click(object sender, RoutedEventArgs e)
        {
            IssueToNextJob(gridWashingDetails.SelectedItem as JobOrder, DBResources.Instance.AfterWashingJobs);
        }

        private void IssueToNextJobAfterOther_Click(object sender, RoutedEventArgs e)
        {
            IssueToNextJob(gridOtherDetails.SelectedItem as JobOrder, DBResources.Instance.AfterOtherJobs);
        }
        #endregion

        #region [New Job Order For Failed Quantity]

        private void NewKnittingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateNewJobOrderForFailedQuantity(gridKnittingDetails.SelectedItem as JobOrder);
        }

        private void NewDyeingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateNewJobOrderForFailedQuantity(gridDyeingDetails.SelectedItem as JobOrder);
        }

        private void NewPrintingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateNewJobOrderForFailedQuantity(gridPrintingDetails.SelectedItem as JobOrder);
        }

        private void NewCompactingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateNewJobOrderForFailedQuantity(gridCompactingDetails.SelectedItem as JobOrder);
        }

        private void NewWashingJobOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateNewJobOrderForFailedQuantity(gridWashingDetails.SelectedItem as JobOrder);
        }

        private void NewOtherJobOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateNewJobOrderForFailedQuantity(gridOtherDetails.SelectedItem as JobOrder);
        }
        #endregion

        #region [Send For Special Approval]
        private void SendForSpecialApproval(JobOrder jOrder)
        {
            if (jOrder != null && jOrder.ValidateIssueAndReceiptDetails())
            {
                ViewModel.SendForSpecialApproval(jOrder);
            }
        }

        private void SendForKinttingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SendForSpecialApproval(gridKnittingDetails.SelectedItem as JobOrder);
        }

        private void SendForDyeingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SendForSpecialApproval(gridDyeingDetails.SelectedItem as JobOrder);
        }

        private void SendForPrintingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SendForSpecialApproval(gridPrintingDetails.SelectedItem as JobOrder);
        }

        private void SendForCompactingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SendForSpecialApproval(gridCompactingDetails.SelectedItem as JobOrder);
        }

        private void SendForWashingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SendForSpecialApproval(gridWashingDetails.SelectedItem as JobOrder);
        }

        private void SendForOtherSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SendForSpecialApproval(gridOtherDetails.SelectedItem as JobOrder);
        }
        #endregion [Send For Special Approval]

        #region [Special Approval]
        private void SpecialApproval(JobOrder jOrder)
        {
            if (jOrder != null && jOrder.ValidateIssueAndReceiptDetails())
            {
                ViewModel.SpecialApproval(jOrder);
            }
        }

        private void KnittingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SpecialApproval(gridKnittingDetails.SelectedItem as JobOrder);
        }

        private void DyeingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SpecialApproval(gridDyeingDetails.SelectedItem as JobOrder);
        }

        private void PrintingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SpecialApproval(gridPrintingDetails.SelectedItem as JobOrder);
        }

        private void CompactingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SpecialApproval(gridCompactingDetails.SelectedItem as JobOrder);
        }

        private void WashingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SpecialApproval(gridWashingDetails.SelectedItem as JobOrder);
        }

        private void OtherSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            SpecialApproval(gridOtherDetails.SelectedItem as JobOrder);
        }
        #endregion [Special Approval]

        private void PurchaseOrderSearchControl_OnTreeViewSelectionChanged(object selectedObject)
        {
            if (selectedObject is Company || selectedObject is PurchaseOrder)
            {
                tabControlJobWorks.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (selectedObject is OrderedItem)
            {
                OrderedItem item = selectedObject as OrderedItem;

                ViewModel.DiscardChanges();
                tabControlJobWorks.Visibility = System.Windows.Visibility.Visible;
                ViewModel.SelectedOrderedItem = item;

            }
        }

       
    }
}
