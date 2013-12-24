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
using ordermanager.ViewModel.Invoice;

namespace ordermanager.Views.UserControls
{
    /// <summary>
    /// Interaction logic for GeneratedInvoiceViewerControl.xaml
    /// </summary>
    public partial class GeneratedInvoiceViewerControl : UserControl
    {
        public GeneratedInvoiceViewerControl()
        {
            InitializeComponent();
        }

        private GeneratedPurchaseOrderViewModel m_ViewModel = null;
        public GeneratedPurchaseOrderViewModel ViewModel
        {
            get { return m_ViewModel; }
            set
            {
                m_ViewModel = value;
                this.DataContext = value;
            }
        }

        private void invoiceList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
                
        }

        private void btnGenerateInvoices_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
