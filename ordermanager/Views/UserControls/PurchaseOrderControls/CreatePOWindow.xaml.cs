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
using System.Windows.Shapes;

namespace ordermanager.Views.UserControls.PurchaseOrderControls
{
    /// <summary>
    /// Interaction logic for CreatePOWindow.xaml
    /// </summary>
    public partial class CreatePOWindow 
    {
        public CreatePOWindow()
        {
            InitializeComponent();
        }

        public void SetOrder(Order order, PurchaseOrder po)
        {
            poEditControl.SetOrder(order, po);
        }
    }
}
