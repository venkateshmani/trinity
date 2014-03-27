using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using ordermanager.ViewModel.Execution;
using ordermanager.ViewModel.Invoice;
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

        Order m_Order = null;
        public OrderWorkBench()
        {
            InitializeComponent();
            m_MaterialsViewModel = new ProductMaterialsViewModel();
            materialsControl.DataContext = m_MaterialsViewModel;
            m_PurchaseOrderViewModel = new PurchaseOrderControlViewModel();
            purchaseOrderControl.DataContext = m_PurchaseOrderViewModel;
            m_ChangeHistorViewModel = new ChangeHistoryViewModel();
            changeHistoryControl.DataContext = m_ChangeHistorViewModel;

            m_CreateInvoiceViewModel = new CreateNewInvoiceViewModel();
            createNewInvoiceControl.ViewModel = m_CreateInvoiceViewModel;

            m_GeneratedInvoiceViewModel = new GeneratedPurchaseOrderViewModel();
            generatedInvoiceControl.ViewModel = m_GeneratedInvoiceViewModel;

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
                SetOrder(value);
                this.DataContext = value;
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
            }
        }

        public void UpdateView()
        {
            string tabHeader = Convert.ToString(((System.Windows.Controls.HeaderedContentControl)(tabControl.SelectedItem)).Header);
            switch (tabHeader.Trim())
            {
                case "Budget":
                    m_MaterialsViewModel.SetOrder(m_Order);
                    break;  
                case "BOM":
                    m_PurchaseOrderViewModel.SetOrder(m_Order);
                    break;
                case "Purchase Order":
                    poControl.SetOrder(m_Order);
                    break;
                case "History":
                    m_ChangeHistorViewModel.SetOrder(m_Order);
                    break;
                case "Invoice":
                    m_CreateInvoiceViewModel.SetOrder(m_Order);
                    m_GeneratedInvoiceViewModel.SetOrder(m_Order);
                    break;
                case "Execution":
                    UpdateExecutionView();
                    break;
                case "Info":
                    UpdateInformationView();
                    break;

            }
        }

        public void UpdateInformationView()
        {
            if (tabInformationDetails.SelectedItem != null)
            {
                string tabHeader = Convert.ToString(((System.Windows.Controls.HeaderedContentControl)(tabInformationDetails.SelectedItem)).Header);
                switch (tabHeader.Trim())
                {
                    case "Materials From Stock":
                        tabMaterialFromStock.SetOrder(m_Order);
                        break;
                }
            }
        }

        public void UpdateExecutionView()
        {
            if (tabExecutionDetailsControl.SelectedItem != null)
            {
                string tabHeader = Convert.ToString(((System.Windows.Controls.HeaderedContentControl)(tabExecutionDetailsControl.SelectedItem)).Header);
                switch (tabHeader.Trim())
                {
                    case "● OC Report":
                        ocReportControl.SetOrder(m_Order);
                        break;
                    case "● Cutting":
                        if (cuttingControl.ViewModel == null || cuttingControl.ViewModel.Order != m_Order)
                            cuttingControl.ViewModel = new CuttingViewModel() { Order = m_Order };
                        break;
                    case "● Production":
                        if (productionControl.ViewModel == null || productionControl.ViewModel.Order != m_Order)
                            productionControl.ViewModel = new ProductionViewModel() { Order = m_Order };
                        break;
                    case "● Quality":
                        if (qualityControl.ViewModel == null || qualityControl.ViewModel.Order != m_Order)
                            qualityControl.ViewModel = new QualityViewModel() { Order = m_Order };
                        break;
                    case "● Packaging":
                        if (packagingControl.ViewModel == null || packagingControl.ViewModel.Order != m_Order)
                            packagingControl.ViewModel = new PackagingViewModel() { Order = m_Order };
                        break;
                    case "● Shipment":
                        if (shippingControl.ViewModel == null || shippingControl.ViewModel.Order != m_Order)
                            shippingControl.ViewModel = new ShipmentViewModel() { Order = m_Order };
                        break;
                }
            }
        }


        private void SetOrder(Order order)
        {
            if (m_Order != order)
            {
                m_Order = order;
                viewEnquiry.SetOrder(order);
                joCtrl.SetOrder(order);
                joCreateCtrl.SetOrder(order);
                UpdateView();
            }
        }

        void OrderWorkBench_Loaded(object sender, RoutedEventArgs e)
        {

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
    }   
}
