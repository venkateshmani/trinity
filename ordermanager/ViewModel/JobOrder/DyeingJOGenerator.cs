using ordermanager.DatabaseModel;
using Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.JobOrderControls
{
    public class DyeingJOGenerator
    {
        private Reports.DyeingJoControl dyeingJoControl = null;
        public DyeingJOGenerator(DyeingJO jo, JoGenericParameters parameters)
        {
            dyeingJoControl = new Reports.DyeingJoControl();
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
             

        private DyeingJO m_JO = null;
        public DyeingJO JO
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

                dyeingJoControl.SetParameters(Parameters);
                dyeingJoControl.CreateReportAsPDF(JO.DyeingJOId, filePath);
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
