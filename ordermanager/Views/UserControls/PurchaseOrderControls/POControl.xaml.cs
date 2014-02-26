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
using System.ComponentModel;
using System.Windows.Forms;
using ordermanager.Views.PopUps;

namespace ordermanager.Views.UserControls
{
    /// <summary>
    /// Interaction logic for POControl.xaml
    /// </summary>
    public partial class POControl : System.Windows.Controls.UserControl
    {
        private BackgroundWorker m_POGenerator = null;
        private Reports.PurchaseOrderReportControl purchaseOrderReportControl = null;
        public POControl()
        {
            InitializeComponent();
            purchaseOrderReportControl = new Reports.PurchaseOrderReportControl();
            this.IsVisibleChanged += POControl_IsVisibleChanged;
            m_POGenerator = new BackgroundWorker();
            m_POGenerator.WorkerReportsProgress = true;
            m_POGenerator.ProgressChanged += m_POGenerator_ProgressChanged;
            m_POGenerator.DoWork += m_POGenerator_DoWork;
            m_POGenerator.WorkerSupportsCancellation = true;
            m_POGenerator.RunWorkerCompleted += m_POGenerator_RunWorkerCompleted;
         //   webBrowser.Navigate("about:blank");
        }

        void POControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible)
            {
                if (supplierList.Items.Count != 0)
                {
                    //supplierList.SelectedIndex = 0;
                }
            }
            else
            {
                //webBrowser.Navigate("about:blank");
            }
        }

        void m_POGenerator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressWindow.Close();   
        }

        void m_POGenerator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        void m_POGenerator_DoWork(object sender, DoWorkEventArgs e)
        {
            string folderPath = e.Argument.ToString();

            foreach (PurchaseOrder po in Order.PurchaseOrders)
            {
                if (po.Company.PurchaseOrderDateWrapper == null)
                    po.Company.PurchaseOrderDateWrapper = DateTime.Now;
                string filePath = System.IO.Path.Combine(
                                             folderPath, "PurchaseOrder-" + po.PurchaseOrderNumber.Replace("/", "_") + ".pdf");
                GeneratePurchaseOrder(po, filePath);
            }
        }

        private void SupplierList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            poEditControl.Visibility = System.Windows.Visibility.Visible;
            PurchaseOrder poDetails = supplierList.SelectedItem as PurchaseOrder;
            if (poDetails != null && Order != null)
            {
                poEditControl.SetOrder(poDetails.Order, poDetails);
                
                //if (poDetails.PurchaseOrderDateWrapper == null)
                //    poDetails.PurchaseOrderDateWrapper = DateTime.Now;

                //string tempFilePathForPdf = System.IO.Path.Combine(
                //                             System.IO.Path.GetTempPath(), "OM_PurchaseOrder-" + poDetails.PurchaseOrderNumber.Replace("/", "_") + ".pdf");
                //string lastOpenedPdfFile = string.Empty;

                //if (GeneratePurchaseOrder(poDetails, tempFilePathForPdf))
                //{
                //    webBrowser.Source = new Uri(tempFilePathForPdf);
                //}

                //if (System.IO.File.Exists(lastOpenedPdfFile))
                //{
                //    System.IO.File.Delete(lastOpenedPdfFile);
                //}

                //lastOpenedPdfFile = tempFilePathForPdf;
            }
        }

        private bool GeneratePurchaseOrder(PurchaseOrder po, string filePath)
        {
            try
            {
                string supplierInformation = GetSupplierInformation(po.Company);
                string purchaseOrderNumber = GetPurchaseOrderNumber(po.Company);
                string quoteNumber = GetQuoteNumber();
                string quoteDate = GetQuoteDate(po.PurchaseOrderDate);

                purchaseOrderReportControl.SetParameters(supplierInformation, purchaseOrderNumber, quoteNumber, quoteDate,
                                                        po.PriceTerms,
                                                        po.Freigt,
                                                        po.PaymentTerms,
                                                        po.DeliveryDate,
                                                        po.QualitySpecifications,
                                                        po.QuantityAllowance);
                purchaseOrderReportControl.CreateReportAsPDF(po.PurchaseOrderID, filePath);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }

            return true;

        }

        private string GetQuoteDate(DateTime? date)
        {
            if (date == null)
                return string.Empty;
            
            return date.Value.ToString("MM/dd/yyyy");
        }

        private string GetQuoteNumber()
        {
            return string.Format("HMI-{0}R", Order.OrderID.ToString());
        }

        private string GetPurchaseOrderNumber(Company supplier)
        {
            return Constants.GetPurchaseOrderNumber(supplier, Order);
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

        ProgressUpdateWindow progressWindow = null;
        private void btnGeneratePOs_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fldDiag = new FolderBrowserDialog();
            if (fldDiag.ShowDialog() == DialogResult.OK)
            {
                progressWindow = new ProgressUpdateWindow();
                progressWindow.UpdateString = "Generate Purchase Orders";
                progressWindow.Show();
                string folderPath = fldDiag.SelectedPath;
                if (!m_POGenerator.IsBusy)
                {
                    m_POGenerator.RunWorkerAsync(folderPath);
                }
            }
            
        }        
    }
}
