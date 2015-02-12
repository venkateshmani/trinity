using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.DatabaseModel;
using Reports;

namespace ordermanager.ViewModel.JobOrderControls
{
    public class CompactingJoGenerator
    {
        private Reports.CompactingJoControl compactingJoControl = null;
        public CompactingJoGenerator(CompactingJo jo, JoGenericParameters parameters)
        {
            compactingJoControl = new Reports.CompactingJoControl();
            JO = jo;
            Parameters = parameters;
        }


        private JoGenericParameters m_parameters = null;
        public JoGenericParameters Parameters
        {
            get
            {
                return m_parameters;
            }
            set
            {
                m_parameters = value;
            }
        }
             

        private CompactingJo m_JO = null;
        public CompactingJo JO
        {
            get
            {
                return m_JO;
            }
            private set
            {
                m_JO = value;
            }
        }

        public string GenerateJobOrder()
        { 
            string fileName = string.Empty;

            try
            {
                fileName = System.IO.Path.Combine(
                                             System.IO.Path.GetTempPath(), "OM_JO_" + JO.Order.OrderID.ToString() + "_" + JO.JoNo.Replace("-", "_") +".pdf");


                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                if (!GenerateJobOrder(fileName))
                {
                    fileName = string.Empty;
                }
            }
            catch (Exception ex)
            {
                
            }

            return fileName;
        }

        private bool GenerateJobOrder(string filePath)
        {
            try
            {

                compactingJoControl.SetParameters(Parameters);
                compactingJoControl.CreateReportAsPDF(JO.CompactingJoId, filePath);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }

            return true;

        }
    }
}

