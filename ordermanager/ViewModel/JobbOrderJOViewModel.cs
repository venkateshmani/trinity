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
    public class JobbOrderJOViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Company> Suppliers
        {
            get
            {
                return DBResources.Instance.Suppliers;
            }
        }

        private Company m_SelectedSupplier = null;
        public Company SelectedSupplier
        {
            get
            {
                return m_SelectedSupplier;
            }
            set
            {
                m_SelectedSupplier = value;

                //Select the Job Orders
                var activeJobOrders = from jobOrder in value.JobOrders
                                      where jobOrder.IsIssued == false
                                      select jobOrder;

                JobOrders = new ObservableCollection<JobOrder>(activeJobOrders);
                OnPropertyChanged("SelectedSupplier");
            }
        }

        private ObservableCollection<JobOrder> m_JobOrders = null;
        public ObservableCollection<JobOrder> JobOrders
        {
            get
            {
                return m_JobOrders;
            }
            set
            {
                m_JobOrders = value;
                OnPropertyChanged("JobOrders");
            }
        }

        public bool IssueNewJob(JobOrder jobOrder)
        {
            if (jobOrder != null)
            {
                jobOrder.GRNReciept.JobOrders.Add(jobOrder);
                Save();
                return true;
            }
            return false;
        }

        public bool Save()
        {
            return DBResources.Instance.Save();
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
    
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
