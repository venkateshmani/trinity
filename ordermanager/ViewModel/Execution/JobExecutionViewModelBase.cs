using ordermanager.Interfaces_And_Enums;
using System;
using System.Collections.Generic;
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
                    Products = Order.OrderProducts.ToList();
                }
                OnPropertyChanged("Order");
            }
        }

        private List<DatabaseModel.OrderProduct> m_Products = null;
        public List<DatabaseModel.OrderProduct> Products
        {
            get
            {
                return m_Products;
            }
            set
            {
                Products = value;
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

        private DateTime m_SelectedDate = DateTime.Today;
        public DateTime SelectedDate
        {
            get
            {
                return m_SelectedDate;
            }
            set
            {
                m_SelectedDate = value;
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
                IsReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }

      
        public virtual bool Save(string userComment)
        {
            return true;
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
       
    }
}
