using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Reports
{
    public partial class InvoiceUserControl : UserControl
    {
        public InvoiceUserControl()
        {
            InitializeComponent();
        }

        public void SetParameters(InvoiceReportParameters parameters)
        {
            ReportParameter[] reportParameters = new ReportParameter[12];
            reportParameters[0] = new ReportParameter("InvoiceTitle", parameters.InvoiceTitle);
            reportParameters[1] = new ReportParameter("InvoiceNumber", parameters.InvoiceNumber);
            reportParameters[2] = new ReportParameter("InvoiceDate", parameters.InvoiceDate);
            reportParameters[3] = new ReportParameter("OurOrderNumber", parameters.OurOrderNumber);
            reportParameters[4] = new ReportParameter("YourOrderNumber", parameters.YourOrderNumber);
            reportParameters[5] = new ReportParameter("Origin", parameters.Origin);
            reportParameters[6] = new ReportParameter("Destination", parameters.Destination);
            reportParameters[7] = new ReportParameter("CarriageBy", parameters.CarriageBy);
            reportParameters[8] = new ReportParameter("PlaceOfReceiptByPrecarrier", parameters.PlaceOfReceiptByPrecarrier);
            reportParameters[9] = new ReportParameter("PortOfLoading", parameters.PortOfLoading);
            reportParameters[10] = new ReportParameter("PortOfDischarge", parameters.PortOfDischarge);
            reportParameters[11] = new ReportParameter("ConsigneeAddress", parameters.ConsigneeAddress);

            this.reportViewer1.LocalReport.SetParameters(reportParameters);
        }

        public void CreateReportAsPDF(long? invoiceID, string fileName)
        {
            
            try
            {
                this.SP_InvoiceDetailsTableAdapter.Fill(this.OrderManagerDBDataSetForInvoice.SP_InvoiceDetails, invoiceID);

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;


                // Setup the report viewer object and get the array of bytes
                byte[] bytes = reportViewer1.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);

                using (System.IO.FileStream sw = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
                {
                    sw.Write(bytes, 0, bytes.Length);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
