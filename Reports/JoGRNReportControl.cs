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
    public partial class JoGRNReportControl : UserControl
    {
        public JoGRNReportControl()
        {
            InitializeComponent();
        }

        public void CreateReportAsPDF(int? supplierID, long? jobOrderId, string fileName)
        {
            try
            {
                this.SP_JoGRNTableAdapter.Fill(this.joGRNReportDataSet.SP_JoGRN, supplierID, jobOrderId);

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                reportViewer1.LocalReport.Refresh();
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
