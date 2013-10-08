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
                    //HistoryItems = new ObservableCollection<History>(order.Histories);
                    CreateDummyData();
                }
                m_Order = order;
            }
            return true;
        }

        private void CreateDummyData()
        {
            ObservableCollection<History> m_Items = new ObservableCollection<History>();
            m_Items.Add(new History() { Comment = "I wouldn't use a WebView in an ItemsControl at all, but you could try putting your entire template in a UserControlm then you handle the event inside of the UserControl and don't need to care about sender, since you can just name the Grid and access a named Grid.", OrderChanges = "UserControl then you handle the event inside of the UserControl and don't need to care about sender", UserName = "Bolla", Date = DateTime.Now });
            m_Items.Add(new History() { Comment = "You could try putting your entire template in a then you handle the event inside of the UserControl and don't need to care about sender, since you can just name the Grid and access a named Grid.", OrderChanges = "", UserName = "Venky", Date = DateTime.Now.AddHours(-10) });
            m_Items.Add(new History() { Comment = "A WebView in an ItemsControl at all, but you could try putting your entire template in a UserControlm then you handle the event inside of the UserControl and don't need to care about sender.", OrderChanges = "Event inside of the UserControl and don't need to care about sender", UserName = "Nani", Date = DateTime.Now.AddHours(-20) });
            m_Items.Add(new History() { Comment = "ItemsControl at all, but you could try putting your entire template in a UserControlm then you handle the event inside of the UserControl and don't need to care about sender, since you can just name.", OrderChanges = "You handle the event inside of the UserControl and don't need to care about anything", UserName = "Raja", Date = DateTime.Now.AddHours(-26) });
            m_Items.Add(new History() { Comment = "ItemsControl at all, but you could try putting your entire template in a UserControlm then you handle the event inside of the UserControl and don't need to care about sender, since you can just name.", UserName = "Raja", Date = DateTime.Now.AddHours(-26) });
            m_Items.Add(new History() { Comment = "I wouldn't use a WebView in an ItemsControl at all, but you could try putting your entire template in a UserControlm then you handle the event inside of the UserControl and don't need to care about sender, since you can just name the Grid and access a named Grid.", OrderChanges = "UserControl then you handle the event inside of the UserControl and don't need to care about sender", UserName = "Bolla", Date = DateTime.Now });
            m_Items.Add(new History() { Comment = "You could try putting your entire template in a then you handle the event inside of the UserControl and don't need to care about sender, since you can just name the Grid and access a named Grid.", OrderChanges = "", UserName = "Venky", Date = DateTime.Now.AddHours(-10) });
            m_Items.Add(new History() { Comment = "A WebView in an ItemsControl at all, but you could try putting your entire template in a UserControlm then you handle the event inside of the UserControl and don't need to care about sender.", OrderChanges = "Event inside of the UserControl and don't need to care about sender", UserName = "Nani", Date = DateTime.Now.AddHours(-20) });
            m_Items.Add(new History() { Comment = "ItemsControl at all, but you could try putting your entire template in a UserControlm then you handle the event inside of the UserControl and don't need to care about sender, since you can just name.", OrderChanges = "You handle the event inside of the UserControl and don't need to care about anything", UserName = "Raja", Date = DateTime.Now.AddHours(-26) });
            m_Items.Add(new History() { Comment = "ItemsControl at all, but you could try putting your entire template in a UserControlm then you handle the event inside of the UserControl and don't need to care about sender, since you can just name.", UserName = "Raja", Date = DateTime.Now.AddHours(-26) });
            HistoryItems = m_Items;
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
