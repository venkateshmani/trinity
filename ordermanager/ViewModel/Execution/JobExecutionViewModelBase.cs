using ordermanager.DatabaseModel;
using ordermanager.Interfaces_And_Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.Execution
{
    public class JobExecutionViewModelBase : IJobExecutionViewModel, INotifyPropertyChanged
    {
        private DatabaseModel.Order m_Order = null;
        public DatabaseModel.Order Order
        {
            get
            {
                return m_Order;
            }
            set
            {
                m_Order = value;
                if (m_Order != null)
                {
                    Products = new ObservableCollection<OrderProduct>(Order.OrderProducts);
                }
                OnPropertyChanged("Order");
            }
        }

        private ObservableCollection<OrderProduct> m_Products = null;
        public ObservableCollection<OrderProduct> Products
        {
            get
            {
                return m_Products;
            }
            set
            {
                m_Products = value;
                OnPropertyChanged("Products");
            }
        }


        private DatabaseModel.OrderProduct m_SelectedProduct = null;
        public DatabaseModel.OrderProduct SelectedProduct
        {
            get
            {
                return m_SelectedProduct;
            }
            set
            {
                m_SelectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        private string m_SelectedDate = string.Empty;
        public string SelectedDate
        {
            get
            {
                return m_SelectedDate;
            }
            set
            {
                m_SelectedDate = value;

                if (!string.IsNullOrEmpty(value) && value == DateTime.Now.ToShortDateString())
                    IsReadOnly = false;
                else
                    IsReadOnly = true;

                OnPropertyChanged("SelectedDate");
            }
        }

        bool m_IsReadOnly = false;
        public bool IsReadOnly
        {
            get
            {
                return m_IsReadOnly;
            }
            set
            {
                m_IsReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }


        public virtual bool Save(string userComment, string executionType)
        {
            Order.LastModifiedDate = DateTime.Now;
            History historyItem = new History();
            historyItem.Date = DateTime.Now;
            historyItem.UserName = DBResources.Instance.CurrentUser.UserName;
            historyItem.Comment = userComment;
            historyItem.Order = Order;
            historyItem.OrderChanges = "has changed in " + executionType;
            Order.Histories.Add(historyItem);
            return DBResources.Instance.Save();
        }


        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        public virtual void AddNewRecord(DateTime date) { }
    }
}
