using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ordermanager.Views.ReportViewers
{
    /// <summary>
    /// Interaction logic for ReportsManager.xaml
    /// </summary>
    public partial class ReportsManager : UserControl
    {
        public ReportsManager()
        {
            InitializeComponent();
            this.Loaded += ReportsManager_Loaded;
        }

        void ReportsManager_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel == null)
            {
                this.ViewModel = new ReportsMangerViewModel();
            }
        }

        public ReportsMangerViewModel ViewModel
        {
            get
            {
                return this.DataContext as ReportsMangerViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void reportsList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                var selectedItem = reportsList.SelectedItem as ReportItem;
                if (selectedItem != null)
                {
                    reportViewContainer.Child = selectedItem.ReportView;
                    this.ViewModel.CurrentReport = selectedItem.ReportView as IReportViewer;
                }
            }
        }

        private void pdfButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel.CurrentReport != null)
            {
                this.ViewModel.CurrentReport.GeneratePDF(string.Empty);
            }
        }
   }
}
