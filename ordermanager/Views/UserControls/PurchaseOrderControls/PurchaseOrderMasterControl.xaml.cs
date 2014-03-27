using ordermanager.DatabaseModel;
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

namespace ordermanager.Views.UserControls.PurchaseOrderControls
{
    /// <summary>
    /// Interaction logic for PurchaseOrderMasterControl.xaml
    /// </summary>
    public partial class PurchaseOrderMasterControl : UserControl
    {
        public PurchaseOrderMasterControl()
        {
            InitializeComponent();
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
            poViewer.SetOrder(Order);
            newPoCreator.SetOrder(Order);
            grnControl.Order = order;
        }
    }
}
