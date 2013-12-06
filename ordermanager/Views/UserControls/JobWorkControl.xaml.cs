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
            if (issuePopup.ShowDialog() == true)
            {
                ViewModel.IssueNewJob(issuePopup.JobOrder);
            }
        }

        private void tvSuppliers_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //if (tvSuppliers.SelectedItem is Company || tvSuppliers.SelectedItem is PurchaseOrder)
            //{
            //    tabControlJobWorks.Visibility = System.Windows.Visibility.Collapsed;
            //    ViewModel.DiscardChanges();
            //}
            //else if (tvSuppliers.SelectedItem is GRNReciept)
            //{
            //    GRNReciept recp = tvSuppliers.SelectedItem as GRNReciept;
            //    if (ViewModel.SelectedReceipt != recp)
            //        ViewModel.DiscardChanges();
            //    tabControlJobWorks.Visibility = System.Windows.Visibility.Visible;
            //    ViewModel.SelectedReceipt = recp;
            //}
        }

        #region [Issue To Next Job]
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
        #endregion

        #region [New Job Order For Failed Quantity]
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
        #endregion

        #region [Send For Special Approval]
        private void SendForKinttingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridKnittingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SendForSpecialApproval(jOrder);
            }
        }

        private void SendForDyeingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridDyeingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SendForSpecialApproval(jOrder);
            }
        }

        private void SendForPrintingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridPrintingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SendForSpecialApproval(jOrder);
            }
        }

        private void SendForCompactingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridCompactingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SendForSpecialApproval(jOrder);
            }
        }

        private void SendForWashingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridWashingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SendForSpecialApproval(jOrder);
            }
        }

        private void SendForOtherSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridOtherDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SendForSpecialApproval(jOrder);
            }
        }
        #endregion [Send For Special Approval]

        #region [Special Approval]
        private void KnittingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridKnittingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SpecialApproval(jOrder);
            }
        }

        private void DyeingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridDyeingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SpecialApproval(jOrder);
            }
        }

        private void PrintingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridPrintingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SpecialApproval(jOrder);
            }
        }

        private void CompactingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridCompactingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SpecialApproval(jOrder);
            }
        }

        private void WashingSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridWashingDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SpecialApproval(jOrder);
            }
        }

        private void OtherSpecialApproval_Click(object sender, RoutedEventArgs e)
        {
            JobOrder jOrder = gridOtherDetails.SelectedItem as JobOrder;
            if (jOrder != null)
            {
                ViewModel.SpecialApproval(jOrder);
            }
        }
        #endregion [Special Approval]

        private void PurchaseOrderSearchControl_OnTreeViewSelectionChanged(object selectedObject)
        {
            
            //if (tvSuppliers.SelectedItem is Company || tvSuppliers.SelectedItem is PurchaseOrder)
            //{
            //    tabControlJobWorks.Visibility = System.Windows.Visibility.Collapsed;
            //    ViewModel.DiscardChanges();
            //}
            //else if (tvSuppliers.SelectedItem is GRNReciept)
            //{
            //    GRNReciept recp = tvSuppliers.SelectedItem as GRNReciept;
            //    if (ViewModel.SelectedReceipt != recp)
            //        ViewModel.DiscardChanges();
            //    tabControlJobWorks.Visibility = System.Windows.Visibility.Visible;
            //    ViewModel.SelectedReceipt = recp;
            //}
            
            //if (selectedObject is Company || selectedObject is PurchaseOrder)
            //{
            //    tabControlJobWorks.Visibility = System.Windows.Visibility.Collapsed;
                
            //}
            //else if (selectedObject is PurchaseOrder)
            //{
            //    poGrnSummaryView.ViewModel = new PoGrnSummaryViewModel((PurchaseOrder)selectedObject);
            //    orderedItemGrnView.ViewModel = null;
            //    poGrnSummaryView.Visibility = System.Windows.Visibility.Visible;
            //    orderedItemGrnView.Visibility = System.Windows.Visibility.Collapsed;
            //}
            //else if (selectedObject is OrderedItem)
            //{
            //    orderedItemGrnView.ViewModel = new OrderedItemGrnViewModel((OrderedItem)selectedObject);
            //    poGrnSummaryView.ViewModel = null;
            //    poGrnSummaryView.Visibility = System.Windows.Visibility.Collapsed;
            //    orderedItemGrnView.Visibility = System.Windows.Visibility.Visible;
            //}
        }
    }
}
