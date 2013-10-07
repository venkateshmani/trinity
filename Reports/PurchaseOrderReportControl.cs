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
    public partial class PurchaseOrderReportControl : UserControl
    {
        public PurchaseOrderReportControl()
        {
            InitializeComponent();
        }

        public void Generate(long? orderId, int? supplierId)
        {
            this.SP_PurchaseOrderTableAdapter.Fill(this.OrderManagerDBDataSet.SP_PurchaseOrder, orderId, supplierId);
            this.reportViewer1.RefreshReport();
        }

        public void CreatePDF(string fileName)
        {
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
    }
}
