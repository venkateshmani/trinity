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
using System.Reflection;

namespace Reports
{
    public partial class KnittingJoControl : UserControl
    {
        public KnittingJoControl()
        {
            InitializeComponent();
        }

        public void SetParameters(KnittingJoParameters parameters)
        {
            this.reportViewer1.LocalReport.SetParameters(CreateReportParameters(parameters));
        }

        private ReportParameter[] CreateReportParameters(KnittingJoParameters parameters)
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

        public void CreateReportAsPDF(long? knittingID, string fileName)
        {
            try
            {
                this.SP_KnittingJODetailsTableAdapter.Fill(this.OrderManagerDBKnittingJoDataSet.SP_KnittingJODetails, knittingID);

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
