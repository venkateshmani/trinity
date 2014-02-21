﻿using System;
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
    public partial class PurchaseOrderReportControl : UserControl
    {
        public PurchaseOrderReportControl()
        {
            InitializeComponent();
        }

        public void SetParameters(string supplierInformation, string purchaseOrderNumber, string quoteNumber, string quoteDate, string termsAndConditions)
        {
            ReportParameter[] parameters = new ReportParameter[5];
            parameters[0] = new ReportParameter("SupplierInformation", supplierInformation);
            parameters[1] = new ReportParameter("PurchaseOrderNumber", purchaseOrderNumber);
            parameters[2] = new ReportParameter("QuoteNumber", quoteNumber);
            parameters[3] = new ReportParameter("QuoteDate", quoteDate);
            parameters[4] = new ReportParameter("TermsAndConditions", termsAndConditions);

            this.reportViewer1.LocalReport.SetParameters(parameters);
        }

        public void DisplayReport(long? purchaseOrderID)
        {
            this.SP_PurchaseOrderTableAdapter.Fill(this.OrderManagerDBDataSet.SP_PurchaseOrder, purchaseOrderID);
            this.reportViewer1.RefreshReport();
        }

        public void CreateReportAsPDF(long? purchaseOrderID, string fileName)
        {

            try
            {
                this.SP_PurchaseOrderTableAdapter.Fill(this.OrderManagerDBDataSet.SP_PurchaseOrder, purchaseOrderID);

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
                MessageBox.Show(ex.Message);
            }
        }
    }
}
