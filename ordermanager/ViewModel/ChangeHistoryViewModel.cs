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
    public class ChangeHistoryViewModel : INotifyPropertyChanged
    {
        private Order m_Order = null;
        private ObservableCollection<History> m_HistoryItems;

        public ObservableCollection<History> HistoryItems
        {
            get { return m_HistoryItems; }
            set
            {
                m_HistoryItems = value;
                NotifyPropertyChanged("HistoryItems");
            }
        }

        public bool SetOrder(Order order)
        {
            if (m_Order != order)
            {
                if (order != null)
                {
                    HistoryItems = new ObservableCollection<History>(order.Histories.Reverse());
                    //CreateDummyData();
                }
                m_Order = order;
            }
            return true;
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
