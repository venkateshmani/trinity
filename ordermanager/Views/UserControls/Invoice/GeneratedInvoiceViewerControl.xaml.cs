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
using ordermanager.DatabaseModel;

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
            ordermanager.DatabaseModel.Invoice selectedInvoice = invoiceList.SelectedItem as ordermanager.DatabaseModel.Invoice;
            if (selectedInvoice != null && ViewModel.Order != null)
            {

                string tempFilePathForPdf = System.IO.Path.Combine(
                                             System.IO.Path.GetTempPath(), "OM_Invoice-" + selectedInvoice.InvoiceNumber.Replace("/", "_") + ".pdf");
                string lastOpenedPdfFile = string.Empty;

                if (GenerateInvoice(selectedInvoice, tempFilePathForPdf))
                {
                    webBrowser.Source = new Uri(tempFilePathForPdf);
                }

                if (System.IO.File.Exists(lastOpenedPdfFile))
                {
                    System.IO.File.Delete(lastOpenedPdfFile);
                }

                lastOpenedPdfFile = tempFilePathForPdf;
            }                
        }

        private bool GenerateInvoice(ordermanager.DatabaseModel.Invoice invoice, string filePath)
        {
            try
            {
                Reports.InvoiceReportParameters parameters = new Reports.InvoiceReportParameters();
                invoiceUserControl.SetParameters(parameters);
                invoiceUserControl.CreateReportAsPDF(invoice.InvoiceID, filePath);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }

            return true;

        }

        private string GetSupplierInformation(Company supplier)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(supplier.Name + ",");
            if (!string.IsNullOrEmpty(supplier.Address1))
                sb.AppendLine(supplier.Address1 + ",");

            if (!string.IsNullOrEmpty(supplier.Address2))
                sb.AppendLine(supplier.Address2 + ",");

            sb.AppendLine(supplier.City + "," + supplier.State + ",");
            sb.AppendLine(supplier.Country + ".");

            return sb.ToString();
        }

        private void btnGenerateInvoices_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
