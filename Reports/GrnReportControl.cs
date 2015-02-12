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
using System.Reflection;

namespace Reports
{
    public partial class GrnReportControl : UserControl
    {
        
        public GrnReportControl()
        {
            InitializeComponent();
        }


        public void SetParameters(GrnReportParameters parameters)
        {
            this.reportViewer1.LocalReport.SetParameters(CreateReportParameters(parameters));
        }

        private ReportParameter[] CreateReportParameters(GrnReportParameters parameters)
        {
            List<ReportParameter> reportParameters = new List<ReportParameter>();
            PropertyInfo[] propertyInfos = parameters.GetType().GetProperties();
            foreach (PropertyInfo pInfo in propertyInfos)
            {
                ReportParameter parameter = new ReportParameter(pInfo.Name, (string)pInfo.GetValue(parameters));
                reportParameters.Add(parameter);
            }

            return reportParameters.ToArray();
        }

        public void CreateReportAsPDF(long? GrnIndex, string fileName)
        {

            try
            {
                this.SP_GRNReportTableAdapter.Fill(this.dataset.SP_GRNReport, GrnIndex);
                
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
