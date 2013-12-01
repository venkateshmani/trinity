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
    public class OrderedItemGrnViewModel : INotifyPropertyChanged
    {

        public OrderedItemGrnViewModel(OrderedItem orderedItem)
        {
            this.OrderedItem = orderedItem;
        }

        private OrderedItem m_OrderedItem = null;
        public OrderedItem OrderedItem
        {
            get
            {
                return m_OrderedItem;
            }
            set
            {
                m_OrderedItem = value;
                OnPropertyChanged("OrderedItem");
            }
        }

        public ObservableCollection<GRNReciept> Receipts
        {
            get
            {
                return new ObservableCollection<GRNReciept>(OrderedItem.GRNReciepts);
            }
        }

        public void Save()
        {
            DBResources.Instance.Save();
        }

        public void OnPropertyChanged(string proeprtyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(proeprtyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
