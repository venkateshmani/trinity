using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Views.ReportViewers
{
    public class ReportsMangerViewModel : INotifyPropertyChanged
    {
        public ReportsMangerViewModel()
        {
            ReportItems = new List<ReportItem>();
            ReportItems.Add(new ReportItem() { ReportName = "GRN", ReportView = new IndividualGrnReport() });
            ReportItems.Add(new ReportItem() { ReportName = "JO GRN", ReportView = new JoGRNReport() });
        }


        public List<ReportItem> ReportItems
        {
            get;
            set;
        }


        private IReportViewer m_CurrentReport = null;
        public IReportViewer CurrentReport
        {
            get
            {
                return m_CurrentReport;
            }
            set
            {
                m_CurrentReport = value;
                OnPropertyChanged("CurrentReport");
            }
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
