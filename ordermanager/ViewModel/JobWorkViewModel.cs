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

        OrderedItem m_SelectedOrderedItem;
        public OrderedItem SelectedOrderedItem
        {
            get { return m_SelectedOrderedItem; }
            set
            {
                m_SelectedOrderedItem = value;
                OnPropertyChanged("SelectedOrderedItem");
            }
        }

        public bool IssueNewJob(JobOrder jobOrder)
        {
            if (jobOrder != null)
            {
                jobOrder.GRNReciept.JobOrders.Add(jobOrder);
                return Save();
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

        //public bool CreateNewJobOrderForFailedQuantity(JobOrder parentOrder)
        //{
        //    JobOrder newJob = new JobOrder();
        //    newJob.JobQuantity = parentOrder.QualityFailed.GetValueOrDefault(0);
        //    newJob.JobOrderType = parentOrder.JobOrderType;
        //    newJob.Supplier = parentOrder.Supplier;
        //    newJob.Instructions = parentOrder.Instructions;
        //    newJob.RequiredDate = parentOrder.RequiredDate;
        //    newJob.ChargesInINR = parentOrder.ChargesInINR;
        //    return IssueNewJob(newJob);
        //}


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

        public bool CanSave
        {
            get
            {
                return DBResources.Instance.CurrentUser.UserRole.CanModifyJobOrder;
            }
        }
    }
}
