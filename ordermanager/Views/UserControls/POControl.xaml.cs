using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
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
using System.IO;

namespace ordermanager.Views.UserControls
{
    /// <summary>
    /// Interaction logic for POControl.xaml
    /// </summary>
    public partial class POControl : UserControl
    {
        public POControl()
        {
            InitializeComponent();
        }

        private void supplierList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Company supplier = supplierList.SelectedItem as Company;
            if (supplier != null)
            {
                string tempFilePathForPdf = System.IO.Path.Combine(
                                             System.IO.Path.GetTempPath(), "OM_PurchaseOrder" + Order.OrderID.ToString() + "_" + supplier.CompanyID.ToString() + ".pdf");
                string lastOpenedPdfFile = string.Empty;

                string supplierInformation = GetSupplierInformation(supplier);
                string purchaseOrderNumber = GetPurchaseOrderNumber(supplier);
                string quoteNumber = GetQuoteNumber();
                string quoteDate = GetQuoteDate();
                purchaseOrderReportControl.SetParameters(supplierInformation, purchaseOrderNumber, quoteNumber, quoteDate);
                purchaseOrderReportControl.Generate(Order.OrderID, supplier.CompanyID);
                purchaseOrderReportControl.CreatePDF(tempFilePathForPdf);
                webBrowser.Source = new Uri(tempFilePathForPdf);

                if (System.IO.File.Exists(lastOpenedPdfFile))
                {
                    System.IO.File.Delete(lastOpenedPdfFile);
                }

                lastOpenedPdfFile = tempFilePathForPdf;
            }
        }

        private string GetQuoteDate()
        {
            return Order.OrderDate.ToString("MM/dd/yyyy");
        }

        private string GetQuoteNumber()
        {
            return string.Format("HMI-{0}R", Order.OrderID.ToString());
        }

        private string GetPurchaseOrderNumber(Company supplier)
        {
            string startYear = string.Empty;
            string endYear = string.Empty;
            int currentYear = int.Parse(DateTime.Now.ToString("yy"));
            string poUniqueNumber = Order.OrderID.ToString() + supplier.CompanyID.ToString();

            if(DateTime.Now.Month >= 4)
            {
                startYear = currentYear.ToString();
                endYear = (currentYear + 1).ToString();
            }
            else
            {
                startYear = (currentYear -1).ToString();
                endYear = currentYear.ToString();
            }


            return string.Format("TCIPL/{0}-{1}/MAC/{2}", startYear, endYear, poUniqueNumber);
        }

        private string GetSupplierInformation(Company supplier)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(supplier.Name + ",");
            if(!string.IsNullOrEmpty(supplier.Address1))
                sb.AppendLine(supplier.Address1 + ",");

            if(!string.IsNullOrEmpty(supplier.Address2))
                sb.AppendLine(supplier.Address2 + ",");

            sb.AppendLine(supplier.City + "," + supplier.State + ",");
            sb.AppendLine(supplier.Country + "."); 

            return sb.ToString();

        }

        POControlViewModel m_ViewModel = null;
        public POControlViewModel ViewModel
        {
            get
            {
                return m_ViewModel;
            }
            set
            {
                m_ViewModel = value;
                DataContext = value;
            }
        }

        private Order m_Order = null;
        public Order Order
        {
            get
            {
                return m_Order;
            }
            set
            {
                m_Order = value;
            }
        }

        public void SetOrder(Order order)
        {
            Order = order;
            ViewModel = new POControlViewModel(order);
        }
    }
}
