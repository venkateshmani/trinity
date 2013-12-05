using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public class JobWorkViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Company> Suppliers
        {
            get { return DBResources.Instance.Suppliers; }
        }

        GRNReciept m_SelectedReceipt;
        public GRNReciept SelectedReceipt
        {
            get { return m_SelectedReceipt; }
            set
            {
                m_SelectedReceipt = value;
                OnPropertyChanged("SelectedReceipt");
            }
        }

        public bool IssueNewJob(JobOrder jobOrder)
        {
            if (jobOrder != null)
            {
                SelectedReceipt.JobOrders.Add(jobOrder);
                Save();
                // jobOrder.JobOrderReceiptsWrapper.Add(new JobOrderReceipt());               
                return true;
            }
            return false;
        }

        public bool SendForSpecialApproval(JobOrder jOrder)
        {
            jOrder.IsWaitingForApproval = true;
            return DBResources.Instance.Save();
        }

        public bool SpecialApproval(JobOrder jOrder)
        {
            jOrder.IsWaitingForApproval = false;
            jOrder.HasApproved = true;
            return DBResources.Instance.Save();
        }

        public bool CreateNewJobOrderForFailedQuantity(JobOrder parentOrder)
        {
            JobOrder newJob = new JobOrder();
            newJob.JobQuantity = parentOrder.QualityFailed.GetValueOrDefault(0);
            newJob.JobOrderType = parentOrder.JobOrderType;
            newJob.Supplier = parentOrder.Supplier;
            newJob.Instructions = parentOrder.Instructions;
            newJob.RequiredDate = parentOrder.RequiredDate;
            newJob.ChargesInINR = parentOrder.ChargesInINR;
            return IssueNewJob(newJob);
        }


        public bool Save()
        {
            return DBResources.Instance.Save();
        }

        public void DiscardChanges()
        {
            DBResources.Instance.DiscardChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
