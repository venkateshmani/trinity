using ordermanager.DatabaseModel;
using ordermanager.Interfaces_And_Enums;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
using ordermanager.Views.UserControls.JobOrderControls;
using System;
using System.Collections.Generic;
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

namespace ordermanager.Views.UserControls
{
    /// <summary>
    /// Interaction logic for GRNExpressView.xaml
    /// </summary>
    public partial class GRNExpressView : UserControl
    {
        public GRNExpressView()
        {
            InitializeComponent();
            this.Loaded += GRNExpressView_Loaded;
        }

        void GRNExpressView_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.ViewModel != null)
                this.ViewModel.ResetData();
        }

        public GRNExpressViewModel ViewModel
        {
            get
            {
                return this.DataContext as GRNExpressViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        public void SetOrder(Order order)
        {
            this.ViewModel = new GRNExpressViewModel(order);
        }

        private void materialList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedItem = materialList.SelectedItem as GRNExpressItem;
        }

        private void btnIssue_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.ContextMenu.IsOpen = true;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            switch (item.Header.ToString())
            {
                case "Knitting":
                case "Dyeing":
                case "Compacting":
                    CreateJoWindow newJobOrder = new CreateJoWindow();
                    newJobOrder.Order = ViewModel.Order;
                    newJobOrder.Quantity = ViewModel.SelectedItem.SelectedQuantity;
                    newJobOrder.GRNRefNo = ViewModel.SelectedItem.GRNreceiptNumbers;
                    newJobOrder.Initialize(item.Header.ToString());
                    if (newJobOrder.ShowDialog() == true)
                    {
                        byte receiptStatus = 1;
                        string headerString = item.Header.ToString();
                        if (headerString == "Knitting")
                            receiptStatus = (byte)RecieptStatus.IssuedToKnittting;
                        else if (headerString == "Dyeing")
                            receiptStatus = (byte)RecieptStatus.IssuedToDyeing;
                        else if (headerString == "Compacting")
                            receiptStatus = (byte)RecieptStatus.IssuedToCompacting;
                        foreach (var receipt in ViewModel.SelectedItem.GRNReceipts)
                            receipt.ReceiptStatusID = receiptStatus;

                        DBResources.Instance.Save();
                        ViewModel.ResetData();
                    }
                    break;
                case "Printing":
                case "Washing":
                case "Other":
                case "Stock":
                        
                        IssueToPopupBox issuePopupBox = new IssueToPopupBox(DBResources.Instance.AllJobsTypes, item.Header.ToString());
                        issuePopupBox.JobQuantity = issuePopupBox.Receipt.QualityPassedQuantityWrapper;

                        if (issuePopupBox.ShowDialog() == true)
                        {
                            if (issuePopupBox.JobOrder.JobOrderType.Type.ToLower() == "stock")
                            {
                                OrderedItem firstOrderedItem = ViewModel.SelectedItem.GRNReceipts.First().OrderedItem;
                                SubMaterial subMaterial = firstOrderedItem.SubMaterial;
                                MaterialStock stock = new MaterialStock();
                                stock.Order = ViewModel.Order;
                                stock.SubMaterial = subMaterial;
                                stock.InStockDateTime = DBResources.Instance.GetServerTime();
                                stock.StockQuantity = ViewModel.SelectedItem.SelectedQuantity;
                                stock.UnitsOfMeasurement = firstOrderedItem.ProductMaterialItem.UnitsOfMeasurementWrapper;
                                subMaterial.MaterialStocks.Add(stock);
                                foreach (var receipt in ViewModel.SelectedItem.GRNReceipts)
                                    receipt.ReceiptStatusID = (byte)RecieptStatus.IssuedToStock;
                            }
                            else
                            {
                                foreach(var receipt in ViewModel.SelectedItem.GRNReceipts)
                                    receipt.JobOrders.Add(issuePopupBox.JobOrder);
                            }

                            DBResources.Instance.Save();
                            ViewModel.ResetData();
                            foreach (var receipt in ViewModel.SelectedItem.GRNReceipts)
                                receipt.RefreshUIEnablers();
                        }
                    break;
            }

            foreach (var receipt in ViewModel.SelectedItem.GRNReceipts)
                receipt.RefreshUIEnablers();

            
        }
    }
}
