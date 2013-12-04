using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
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
                IssueToPopupBox issuePopup = new IssueToPopupBox(DBResources.Instance.AfterKnittingJobs);
                issuePopup.JobQuantity = jOrder.QualityPassedWrapper.GetValueOrDefault(0);
                issuePopup.ChargesInINR = jOrder.ChargesInINRWrapper;
                issuePopup.Supplier = jOrder.Supplier;
                issuePopup.ShowDialog();
                if (issuePopup.ShowDialog() == true)
                {
                    //JobOrder newJob = new JobOrder();
                    //newJob.JobQuantity = issuePopup.JobQuantity;
                    //newJob.JobOrderType = issuePopup.IssueTo;
                    //newJob.Supplier = issuePopup.Supplier;
                    //newJob.Instructions = issuePopup.Instructions;
                    //newJob.RequiredDate = issuePopup.RequiredDate;              
                    //newJob.ChargesInINR = issuePopup.ChargesInINR;
                    ViewModel.IssueNewJob(issuePopup.JobOrder);
                }
            }
        }

        private void NewKnittingJobOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
