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

        public bool Save()
        {
            ////bool 
            //foreach (Company sup in Suppliers)
            //{
            //    foreach (PurchaseOrder po in sup.PurchaseOrders)
            //    {
            //        foreach (GRNReciept recpt in po.GRNReciepts)
            //        { 
            //            foreach(JobOrder jo in recpt.JobOrders)
            //            {
            //                //foreach (JobOrderReceipt joRecpt in jo.JobOrderReceiptsWrapper)
            //                //{
            //                //    if (joRecpt.JobOrderReceiptID == 0)
            //                //    { 
            //                //        JobOrderReceipt jor= new JobOrderReceipt();
            //                //        jor.ReceiptDate = joRecpt.ReceiptDate;
            //                //        jor.ReceivedQuantity = joRecpt.ReceivedQuantity;
            //                //        jor.Comments = joRecpt.Comments;
            //                //        jo.JobOrderReceipts.Add(jor);
            //                //    }
            //                //}
            //            }
            //        }
            //    }
            //}
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
