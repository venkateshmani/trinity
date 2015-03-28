using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using Reports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for IndividualGrnReport.xaml
    /// </summary>
    public partial class IndividualGrnReport : UserControl, IReportViewer, INotifyPropertyChanged
    {
        GrnReportControl grnReportControl = null;
        public IndividualGrnReport()
        {
            InitializeComponent();
        }

        long firstGrnIndex = 0;
        string grnString = string.Empty;
        List<long> grnIndexes = new List<long>();
        private void getBtn_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] inputs = grnNumberBox.Text.Split(',');
                grnString = string.Empty;
                grnIndexes.Clear();
                
                foreach(string input in inputs)
                {
                    if(input.Contains("to"))
                    {
                        string[] strRanges = input.Replace("to", "").Split(' ');
                        if(strRanges.Length == 3)
                        {
                            strRanges[0] = strRanges[0].Trim();
                            strRanges[2] = strRanges[2].Trim();
                            long start = 0;
                            long end = 0;
                            if(long.TryParse(strRanges[0], out start) && long.TryParse(strRanges[2], out end))
                            {
                                if(start > end)
                                {
                                    //Swap Values
                                    long temp = start;
                                    start = end;
                                    end = start;
                                }

                                for(long i = start ; i<=end;i++)
                                {
                                    if (!grnIndexes.Contains(i))
                                        grnIndexes.Add(i);
                                }
                            }
                        }
                    }
                    else
                    {
                        long grnIndex = 0;
                        //Individual GRN
                        if(long.TryParse(input.Trim(), out grnIndex))
                        {
                            if (!grnIndexes.Contains(grnIndex))
                                grnIndexes.Add(grnIndex);
                        }
                        else
                        {
                            string invoiceNumber = input.ToLower().Replace("i", "").Trim();
                            //Look for Invoice Number
                            foreach(GRNReciept receipt in DBResources.Instance.Context.GRNReciepts)
                            {
                                if (receipt.InvoiceNumber == invoiceNumber)
                                {
                                    if (!grnIndexes.Contains(receipt.GRNIndex.Value))
                                        grnIndexes.Add(receipt.GRNIndex.Value);
                                }
                            }
                        }
                    }
                }

                foreach (long grnIndex in grnIndexes)
                {
                    if(grnString != string.Empty)
                    {
                        grnString += ",";
                    }

                    grnString += grnIndex.ToString();
                }
                ReportItems = null;
                if(grnString != string.Empty)
                {
                  if(long.TryParse(grnString.Split(',')[0], out firstGrnIndex))
                  {
                     ReportItems = DBResources.Instance.Context.SP_GRNReport(grnString).ToList();
                  }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool GeneratePDF(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = System.IO.Path.Combine(
                                            System.IO.Path.GetTempPath(), "GRN_" + grnNumberBox.Text + ".pdf");
                }

                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                if (grnReportControl == null)
                    grnReportControl = new GrnReportControl();

                var firstGrnReceipt = (from grnReceipt in DBResources.Instance.Context.GRNReciepts
                                       where grnReceipt.GRNIndex == firstGrnIndex
                                       select grnReceipt).FirstOrDefault();

                if (firstGrnReceipt != null)
                {
                    GRNReciept grnReceipt = firstGrnReceipt as GRNReciept;
                    GrnReportParameters parameters = new GrnReportParameters();

                    if (grnIndexes.Count == 1)
                    {
                        parameters.Supplier = Constants.GetSupplierInformation(grnReceipt.OrderedItem.PurchaseOrder.Supplier);
                        parameters.GrnDate = grnReceipt.RecievedDate.Value.ToString();
                    }
                    else
                    {
                        parameters.Supplier = "Multiple Suppliers";
                        parameters.GrnDate = "Multiple Dates";
                    }
                    parameters.GrnNo = grnNumberBox.Text;
                    
                    parameters.StyleInfo = Constants.GetStyleInfo(grnReceipt.OrderedItem.PurchaseOrder.Order);

                    grnReportControl.SetParameters(parameters);
                    grnReportControl.CreateReportAsPDF(grnString, fileName);
                    System.Diagnostics.Process.Start(fileName);
                    return true;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }

            return false;
        }

        ICollection m_ReportItems = null;
        public System.Collections.ICollection ReportItems
        {
            get
            {
                return m_ReportItems;
            }
            set
            {
                m_ReportItems = value;
                OnPropertyChanged("ReportItems");
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
