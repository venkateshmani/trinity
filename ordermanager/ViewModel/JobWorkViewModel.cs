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
            get 
            {
                return DBResources.Instance.Suppliers;
            }
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

        public bool IssueToStock(decimal quantityToStock)
        {
            if (SelectedOrderedItem.ProductMaterialItem.SubMaterial.InStock == null)
                SelectedOrderedItem.ProductMaterialItem.SubMaterial.InStock = 0;
            SelectedOrderedItem.ProductMaterialItem.SubMaterial.InStock += quantityToStock;
            return Save();
        }

        public bool IssueNewJob(JobOrder jobOrder)
        {
            bool res = false;
            if (jobOrder != null)
            {
                if (jobOrder.JobOrderType.Type.ToLower() == "stock")
                {
                    res=IssueToStock(jobOrder.JobQuantity);                    
                }
                else
                {
                    jobOrder.GRNReciept.JobOrders.Add(jobOrder);
                    res = Save();
                }               
                if (res)
                    jobOrder.Refresh();
            }
            return res;
        }

        public bool SendForSpecialApproval(JobOrder jOrder)
        {
            bool res = false;
            jOrder.IsWaitingForApproval = true;
            res = Save();
            if (res)
                jOrder.Refresh();
            return res;
        }

        public bool SpecialApproval(JobOrder jOrder)
        {
            bool res = false;
            jOrder.IsWaitingForApproval = false;
            jOrder.HasApproved = true;
            res = Save();
            if (res)
                jOrder.Refresh();
            return res;
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

        public bool CanSave
        {
            get
            {
                return DBResources.Instance.CurrentUser.UserRole.CanModifyJobOrder;
            }
        }
    }
}
