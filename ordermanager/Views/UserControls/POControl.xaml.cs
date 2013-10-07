using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
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
    /// Interaction logic for POControl.xaml
    /// </summary>
    public partial class POControl : UserControl
    {
        public POControl()
        {
            InitializeComponent();
        }

        private void supplierList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Company supplier = supplierList.SelectedItem as Company;
            if (supplier != null)
            {
                purchaseOrderReportControl.Generate(Order.OrderID, supplier.CompanyID);
                purchaseOrderReportControl.CreatePDF("D:\\test.pdf");
            }
        }

        POControlViewModel m_ViewModel = null;
        public POControlViewModel ViewModel
        {
            get
            {
                return m_ViewModel;
            }
            set
            {
                m_ViewModel = value;
                DataContext = value;
            }
        }

        private Order m_Order = null;
        public Order Order
        {
            get
            {
                return m_Order;
            }
            set
            {
                m_Order = value;
            }
        }

        public void SetOrder(Order order)
        {
            Order = order;
            ViewModel = new POControlViewModel(order);
        }
    }
}
