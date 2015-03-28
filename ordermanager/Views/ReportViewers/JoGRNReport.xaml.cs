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
    /// Interaction logic for JoGRNReport.xaml
    /// </summary>
    public partial class JoGRNReport : UserControl, IReportViewer, INotifyPropertyChanged
    {
        JoGRNReportControl joGrnReportControl = null;
        public JoGRNReport()
        {
            InitializeComponent();
            this.Loaded += JoGRNReport_Loaded;
        }

        void JoGRNReport_Loaded(object sender, RoutedEventArgs e)
        {
            supplierList.ItemsSource = DBResources.Instance.Suppliers;
        }

        public bool GeneratePDF(string fileName)
        {
            try
            {
                if (FileIdentifier!= null)
                {
                    if (string.IsNullOrEmpty(fileName))
                    {

                        fileName = System.IO.Path.Combine(
                                                System.IO.Path.GetTempPath(), "JoGRN_" + FileIdentifier + ".pdf");
                    }

                    if (System.IO.File.Exists(fileName))
                    {
                        System.IO.File.Delete(fileName);
                    }

                    if (joGrnReportControl == null)
                        joGrnReportControl = new JoGRNReportControl();
               
                    joGrnReportControl.CreateReportAsPDF(SupplierID, JobOrderId, fileName);
                    System.Diagnostics.Process.Start(fileName);
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
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
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void supplier_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (bySupplierRadio.IsChecked.Value)
            {
                Company supplier = supplierList.SelectedItem as Company;
                ReportItems = DBResources.Instance.Context.SP_JoGRN(SupplierID, JobOrderId).ToList();
            }
        }

        private void getBtn_Click_1(object sender, RoutedEventArgs e)
        {
            if (byJoNumber.IsChecked.Value)
                ReportItems = DBResources.Instance.Context.SP_JoGRN(SupplierID, JobOrderId).ToList();
        }

        private long? ParseJobOrderNumber()
        {
            if (joNumberBox != null && joNumberBox.Text.Length > 1)
            {
                string joNumberString = joNumberBox.Text.Substring(1, joNumberBox.Text.Length - 1);
                long jobOrderId = 0;
                if (long.TryParse(joNumberString, out jobOrderId))
                {
                    return jobOrderId;
                }
                else
                {
                    MessageBox.Show("Invalid Job Order Number Format");
                }
            }

            return null;
        }


        public int? SupplierID
        {
            get
            {
                if (bySupplierRadio.IsChecked.Value)
                {
                    Company supplier = supplierList.SelectedItem as Company;
                    if (supplier != null)
                    {
                        return supplier.CompanyID;
                    }
                }

                return null;
            }
         
        }

        public long? JobOrderId
        {
            get
            {
                if (byJoNumber.IsChecked.Value)
                {
                   return ParseJobOrderNumber();
                }
                return null;
            }
            
        }

        public string FileIdentifier
        {
            get
            {
                string fId = null;

                if (SupplierID != null)
                    fId = SupplierID.ToString();
                else if (JobOrderId != null)
                    fId = "J" + JobOrderId.ToString();

                return fId;
                    
            }
        }


        private void bySupplierRadio_Checked_1(object sender, RoutedEventArgs e)
        {
            supplier_SelectionChanged_1(supplierList, null);
        }

        private void byJoNumber_Checked_1(object sender, RoutedEventArgs e)
        {
            getBtn_Click_1(getBtn, e);   
        }
    }
}
