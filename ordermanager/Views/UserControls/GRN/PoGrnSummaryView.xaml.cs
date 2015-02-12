using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
using Reports;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ordermanager.Views.UserControls.GRN
{
    /// <summary>
    /// Interaction logic for PoGrnSummaryView.xaml
    /// </summary>
    public partial class PoGrnSummaryView : UserControl
    {
        CollectionViewSource grnReportViewSource = null;
        public PoGrnSummaryView()
        {
            InitializeComponent();
            grnReportViewSource = this.FindResource("grnReportViewSource") as CollectionViewSource;
            grnReportViewSource.Filter += grnReportViewSource_Filter;
        }

        private PoGrnSummaryViewModel m_ViewModel = null;
        public PoGrnSummaryViewModel ViewModel
        {
            get
            {
                return m_ViewModel;
            }
            set
            {
                if (value != null)
                {
                    m_ViewModel = value;
                    ObservableCollection<GRNReciept> items = new ObservableCollection<GRNReciept>();
                    foreach (var orderedItem in value.PurchaseOrder.OrderedItems)
                    {
                        foreach (var grnReceipt in orderedItem.GRNReciepts)
                        {
                            items.Add(grnReceipt);
                        }
                    }
                    grnReportViewSource.Source = items;
                    grnReportGrid.ItemsSource = grnReportViewSource.View;
                    this.DataContext = value;
                }
            }
        }

        private void addNewItemBtn_Click_1(object sender, RoutedEventArgs e)
        {
            AddItemsWindow addNewItemsWnd = new AddItemsWindow();
            addNewItemsWnd.AvailableItems = ViewModel.AvailableItemsInPoToCreateGRNReceipt;
            if (addNewItemsWnd.ShowDialog() == true)
            {
                ViewModel.AddItems(addNewItemsWnd.SelectedMaterials);
            }
        }

        private void addNewReceipt_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.HasUserClickedSaveOrSubmit = true;
                if (!ViewModel.HasErrors)
                {
                    ViewModel.AddReceipt();
                    InformUser("Receipt Added Successfully");
                    poGrnTabControl.SelectedIndex = 1;
                    ViewModel.RefreshPurchaseOrder();
                    ViewModel = new PoGrnSummaryViewModel(ViewModel.PurchaseOrder);
                }
                else
                {
                    InformUser("Fill in the highlighted fields and try again");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                InformUser("Failed to add receipt");
            }
        }

        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
        }

        private void materialsReceiptGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
             grnReportViewSource.View.Refresh();
        }

        void grnReportViewSource_Filter(object sender, FilterEventArgs e)
        {
            bool isAccepted = false;
            if (grnList.SelectedItem != null)
            {
                long grnIndex = (long)grnList.SelectedItem;
                GRNReciept item = e.Item as GRNReciept;
                if (item.GRNIndex == grnIndex)
                    isAccepted = true;
            }

            e.Accepted = isAccepted;
        }

        public string GenerateReport()
        {
            string fileName = string.Empty;
            long grnIndex = 0;

            if (grnList.SelectedItem != null)
            {
                grnIndex = (long)grnList.SelectedItem;
            }
            else 
                return string.Empty;

            try
            {
                fileName = System.IO.Path.Combine(
                                             System.IO.Path.GetTempPath(), "GRN_" + grnIndex + ".pdf");


                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                if (!GenerateGrnReport(fileName, grnIndex))
                {
                    fileName = string.Empty;
                }
            }
            catch (Exception ex)
            {

            }

            return fileName;
        }

        GrnReportControl grnReportControl = null;
        private bool GenerateGrnReport(string filePath, long? grnIndex)
        {
            try
            {
                if (grnReportControl == null)
                    grnReportControl = new GrnReportControl();

                GRNReciept grnReceipt = grnReportGrid.Items[0] as GRNReciept;
                GrnReportParameters parameters = new GrnReportParameters();
                parameters.Supplier = Constants.GetSupplierInformation(ViewModel.PurchaseOrder.Supplier);
                parameters.GrnNo = grnIndex.Value.ToString();
                parameters.GrnDate = grnReceipt.RecievedDate.Value.ToString();
                parameters.StyleInfo = Constants.GetStyleInfo(ViewModel.PurchaseOrder.Order);

                grnReportControl.SetParameters(parameters);
                grnReportControl.CreateReportAsPDF(grnIndex, filePath);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }

            return true;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string generatedFileName = GenerateReport();
            if (!string.IsNullOrEmpty(generatedFileName))
            {
                System.Diagnostics.Process.Start(generatedFileName);
            }
        }
    }


}
