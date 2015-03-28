using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ordermanager.Views.ReportViewers
{
    public class ReportItem : INotifyPropertyChanged
    {
        public ReportItem()
        {

        }

        private string m_ReportName = string.Empty;
        public string ReportName
        {
            get
            {
                return m_ReportName;
            }
            set
            {
                m_ReportName = value;
                OnPropertyChanged("ReportName");
            }
        }

        private UserControl m_ReportView = null;
        public UserControl ReportView
        {
            get
            {
                return m_ReportView;
            }
            set
            {
                m_ReportView = value;
                OnPropertyChanged("ReportView");
            }
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
