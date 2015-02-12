using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.ViewModel.Execution;
using ordermanager.ViewModel.Invoice;
using ordermanager.Views.PopUps;
using ordermanager.Views.UserControls.Execution;
using ordermanager.Views.UserControls.JobOrderControls;
using ordermanager.Views.UserControls.PurchaseOrderControls;
using ordermanager.Views.UserControls.Stock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
    /// Interaction logic for OrderWorkBench.xaml
    /// </summary>
    public partial class OrderWorkBench : UserControl
    {
        public event OnGoBackDelegate OnGoBack = null;
        ProductMaterialsViewModel m_MaterialsViewModel;
        PurchaseOrderControlViewModel m_PurchaseOrderViewModel;
        ChangeHistoryViewModel m_ChangeHistorViewModel;
        CreateNewInvoiceViewModel m_CreateInvoiceViewModel = null;
        GeneratedPurchaseOrderViewModel m_GeneratedInvoiceViewModel = null;

        //Lazzy initialization controls
        NewEnquiryFormUserControl viewEnquiry = null;
        ProductMaterialsControl materialsControl = null;
        PurchaseOrderMasterControl poControl = null;
        PurchaseOrderControl purchaseOrderControl = null;
        JobOrderJOView joCtrl = null;
        JobOrderManager joManagerCtrl = null;
        CreateNewInvoiceControl createNewInvoiceControl = null;
        GeneratedInvoiceViewerControl generatedInvoiceControl = null;
        ChangeHistoryControl changeHistoryControl = null;
        MaterialStockPerOrderControl stockPerOrderControl = null;

        OverAllCompleteReportControl ocReportControl = null;
        CuttingControl cuttingControl = null;
        ProductionControl productionControl = null;
        QualityControl qualityControl = null;
        PackagingControl packagingControl = null;
        ShippingControl shippingControl = null;

        Order m_Order = null;
        public OrderWorkBench()
        {
            InitializeComponent();
            
         
      

         

            this.Loaded += OrderWorkBench_Loaded;
            tabControl.SelectedIndex = 2;
            tabControl.SelectionChanged += tabControl_SelectionChanged;
            tabExecutionDetailsControl.SelectionChanged += tabExecutionDetailsControl_SelectionChanged;
        }

        string lastSelectedExecutionDetailsTab = string.Empty;
        void tabExecutionDetailsControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                UpdateExecutionView();
        }

        public Order Order
        {
            get
            {
                return m_Order;
            }
            set
            {
                orderIDTxtBlk.Text = value.OrderID.ToString();
                customerNameTxtBlk.Text = value.Customer.Name;
                styleNames.Text = string.Empty;
                foreach (var orderedProduct in value.OrderProducts)
                {
                    if (styleNames.Text != string.Empty)
                        styleNames.Text += ", ";
                    styleNames.Text += orderedProduct.ProductName.StyleID;
                }

                OrderStatusEnum status = (OrderStatusEnum)Enum.Parse(typeof(OrderStatusEnum), value.OrderStatu.StatusLabel);

                if (status < OrderStatusEnum.OrderConfirmed)
                {
                    tabMaterialDetails.Visibility = System.Windows.Visibility.Collapsed;
                    tabPurchaseOrder.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    tabMaterialDetails.Visibility = System.Windows.Visibility.Visible;
                    tabPurchaseOrder.Visibility = System.Windows.Visibility.Visible;
                }

                //Navigate to appropriate page
                if (status == OrderStatusEnum.EnquiryCreated ||
                    status == OrderStatusEnum.MaterialsAdded ||
                    status == OrderStatusEnum.EnquiryRejected)
                {
                    tabControl.SelectedItem = tabMaterials;
                }
                else if (status == OrderStatusEnum.MaterialsCostAdded
                    || status == OrderStatusEnum.EnquiryApproved
                    || status == OrderStatusEnum.EnquiryCancelled)
                {
                    tabControl.SelectedItem = tabViewEnquiry;
                }
                else if (status == OrderStatusEnum.OrderConfirmed)
                    tabControl.SelectedItem = tabMaterialDetails;
                else if (status == OrderStatusEnum.SubMaterialsJobCompleted)
                    tabControl.SelectedItem = tabPurchaseOrder;
                else
                    tabControl.SelectedItem = tabViewEnquiry;
                SetOrder(value);
            }
        }

        public void UpdateView()
        {
            if (m_Order != null)
            {
                string tabHeader = Convert.ToString(((System.Windows.Controls.HeaderedContentControl)(tabControl.SelectedItem)).Header);
                switch (tabHeader.Trim())
                {
                    case "View Enquiry":
                        {
                            

                            if (tabViewEnquiry.Visibility == System.Windows.Visibility.Visible)
                            {
                                if (viewEnquiry == null)
                                {
                                    viewEnquiry = new NewEnquiryFormUserControl
                                    {
                                        VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                                        IsNewEnquiry = false
                                    };
                                    viewEnquiryHolder.Child = viewEnquiry;
                                }
                                viewEnquiry.SetOrder(m_Order);
                            }
                            break;
                        }
                    case "Budget":
                        {
                            if (materialsControl == null)
                            {
                                materialsControl = new ProductMaterialsControl
                                {
                                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                                };
                                m_MaterialsViewModel = new ProductMaterialsViewModel();
                                materialsControl.DataContext = m_MaterialsViewModel;
                                materialsControlHolder.Child = materialsControl;
                            }
                            m_MaterialsViewModel.SetOrder(m_Order);
                            break;
                        }
                    case "BOM":
                        {
                            if (purchaseOrderControl == null)
                            {
                                purchaseOrderControl = new PurchaseOrderControl();
                                purchaseOrderControl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                                purchaseOrderControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                                m_PurchaseOrderViewModel = new PurchaseOrderControlViewModel();
                                purchaseOrderControl.DataContext = m_PurchaseOrderViewModel;
                                bomHolder.Child = purchaseOrderControl;
                            }
                            m_PurchaseOrderViewModel.SetOrder(m_Order);
                            break;
                        }
                    case "Purchase Order":
                        {
                            if (poControl == null)
                            {
                                poControl = new PurchaseOrderMasterControl();
                                poControl.VerticalAlignment = VerticalAlignment.Stretch;
                                poControl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                                purchaseOrderControlHolder.Child = poControl;
                            }
                            if (poControl.Order != m_Order)
                                poControl.SetOrder(m_Order);

                            break;
                        }
                    case "JO":

                        if (joManagerCtrl == null)
                        {
                            joManagerCtrl = new JobOrderManager();
                            joManagerCtrl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            joManagerCtrl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            joManagerCtrlHolder.Content = joManagerCtrl;
                        }

                        joManagerCtrl.SetOrder(m_Order);

                        if (joCtrl == null)
                        {
                            joCtrl = new JobOrderJOView();
                            joCtrl.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                            joCtrl.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                            joCtrlHolder.Content = joCtrl;
                        }

                        joCtrl.SetOrder(m_Order);
                        break;
                    case "History":
                        {
                            if (changeHistoryControl == null)
                            {
                                changeHistoryControl = new ChangeHistoryControl
                                {
                                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                                };

                                changeHistoryHolder.Child = changeHistoryControl;

                                m_ChangeHistorViewModel = new ChangeHistoryViewModel();
                                changeHistoryControl.DataContext = m_ChangeHistorViewModel;
                            }
                            m_ChangeHistorViewModel.SetOrder(m_Order);
                            break;
                        }
                    case "Invoice":
                        {
                            if (createNewInvoiceControl == null)
                            {
                                createNewInvoiceControl = new CreateNewInvoiceControl
                                {
                                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                                };

                                tabNewInvoice.Content = createNewInvoiceControl;
                                m_CreateInvoiceViewModel = new CreateNewInvoiceViewModel();
                                createNewInvoiceControl.ViewModel = m_CreateInvoiceViewModel;
                            }

                            m_CreateInvoiceViewModel.SetOrder(m_Order);
                            if (generatedInvoiceControl == null)
                            {
                                generatedInvoiceControl = new GeneratedInvoiceViewerControl
                                {
                                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                                };
                                tabGeneratedInvoice.Content = generatedInvoiceControl;
                                m_GeneratedInvoiceViewModel = new GeneratedPurchaseOrderViewModel();
                                generatedInvoiceControl.ViewModel = m_GeneratedInvoiceViewModel;
                            }
                            m_GeneratedInvoiceViewModel.SetOrder(m_Order);
                            break;
                        }
                    case "Execution":
                        UpdateExecutionView();
                        break;
                    case "Stock":
                        {
                            if (stockPerOrderControl == null)
                            {
                                stockPerOrderControl = new MaterialStockPerOrderControl
                                {
                                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                                };
                                stockerOrderHolder.Child = stockPerOrderControl;
                            }
                            stockPerOrderControl.Order = m_Order;
                            break;
                        }
                }
            }
        }

        //public void UpdateInformationView()
        //{
        //    if (tabInformationDetails.SelectedItem != null)
        //    {
        //        string tabHeader = Convert.ToString(((System.Windows.Controls.HeaderedContentControl)(tabInformationDetails.SelectedItem)).Header);
        //        switch (tabHeader.Trim())
        //        {
        //            case "Materials From Stock":
        //                tabMaterialFromStock.SetOrder(m_Order);
        //                break;
        //        }
        //    }
        //}

        public void UpdateExecutionView()
        {
            if (tabExecutionDetailsControl.SelectedItem != null)
            {
                string tabHeader = Convert.ToString(((System.Windows.Controls.HeaderedContentControl)(tabExecutionDetailsControl.SelectedItem)).Header);
                switch (tabHeader.Trim())
                {
                    case "● OC Report":
                        {
                            if (ocReportControl == null)
                            {
                                ocReportControl = new OverAllCompleteReportControl
                                {
                                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                                };
                                tabOverAllCompletion.Content = ocReportControl;
                            }
                            ocReportControl.SetOrder(m_Order);
                            break;
                        }
                    case "● Cutting":
                        {
                            if (cuttingControl == null)
                            {
                                cuttingControl = new CuttingControl
                                {
                                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                                };
                                tabCutting.Content = cuttingControl;
                            }
                            if (cuttingControl.ViewModel == null || cuttingControl.ViewModel.Order != m_Order)
                                cuttingControl.ViewModel = new CuttingViewModel() { Order = m_Order };
                            break;
                        }
                    case "● Production":
                        {
                            if (productionControl == null)
                            {
                                productionControl = new ProductionControl
                                {
                                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                                };
                                tabProduction.Content = productionControl;
                            }
                            if (productionControl.ViewModel == null || productionControl.ViewModel.Order != m_Order)
                                productionControl.ViewModel = new ProductionViewModel() { Order = m_Order };
                            break;
                        }
                    case "● Quality":
                        {
                            if (qualityControl == null)
                            {
                                qualityControl = new QualityControl
                                {
                                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                                };
                                tabQuality.Content = qualityControl;
                            }
                            if (qualityControl.ViewModel == null || qualityControl.ViewModel.Order != m_Order)
                                qualityControl.ViewModel = new QualityViewModel() { Order = m_Order };
                            break;
                        }
                    case "● Packaging":
                        {
                            if (packagingControl == null)
                            {
                                packagingControl = new PackagingControl
                                {
                                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                                };
                                tabPackaging.Content = packagingControl;
                            }
                            if (packagingControl.ViewModel == null || packagingControl.ViewModel.Order != m_Order)
                                packagingControl.ViewModel = new PackagingViewModel() { Order = m_Order };
                            break;
                        }
                    case "● Shipment":
                        {
                            if (shippingControl == null)
                            {
                                shippingControl = new ShippingControl
                                {
                                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch
                                };
                                tabShipment.Content = shippingControl;

                            }
                            if (shippingControl.ViewModel == null || shippingControl.ViewModel.Order != m_Order)
                                shippingControl.ViewModel = new ShipmentViewModel() { Order = m_Order };
                            break;
                        }
                }
            }
        }


        private void SetOrder(Order order)
        {
            if (m_Order != order)
            {
                m_Order = order;
                UpdateView();
            }
        }

        void OrderWorkBench_Loaded(object sender, RoutedEventArgs e)
        {
            if (DBResources.Instance.CurrentUser.UserRole.CanCreateNewEnquiry)
            {
                tabViewEnquiry.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                tabViewEnquiry.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void GoBack_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            
            if (OnGoBack != null)
            {
                OnGoBack();
            }
          
        }

        void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource != null && e.OriginalSource.GetType() == typeof(TabControl))
            {
                UpdateView();
            }
            e.Handled = true;
        }

        private void Button_PreviewMouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
          
            if (OnGoBack != null)
            {
                DBResources.Instance.ReloadChangedEntities();
                //DBResources.Instance.DiscardChanges();
                OnGoBack();
            }
          
        }


        public class MaterialDetails
        {
            public string MaterialName { get; set; }
            public string CostPerUnit { get; set; }
            public string UOM { get; set; }
            public string Consumption { get; set; }
            public string ConsumptionCost { get; set; }
        }

        public class ProductDetails
        {
            public string ProductName { get; set; }
            public string Quantity { get; set; }
            public int ItemIndexNumber { get; set; }
        }

        private void joCreateCtrl_OnMoveToJOList_1()
        {
            joTabControl.SelectedIndex = 1;
        }       
    }   
}
