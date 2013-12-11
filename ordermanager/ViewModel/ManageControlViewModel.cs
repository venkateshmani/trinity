using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public class ManageControlViewModel : INotifyPropertyChanged
    {
        Order m_Order;
        public Order Order
        {
            get { return m_Order; }
            set
            {
                if (value != m_Order)
                {
                    m_Order = value;
                    NotifyPropertyChanged("Order");
                }
            }
        }

        #region [INotifyPropertyChanged]
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion [INotifyPropertyChanged]
    }
}
