using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
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

namespace ordermanager.Views.UserControls
{
    /// <summary>
    /// Interaction logic for GRNControl.xaml
    /// </summary>
    public partial class GRNControl : UserControl
    {
       
        public GRNControl()
        {
            InitializeComponent();
          
        }

        private void PurchaseOrderSearchControl_OnTreeViewSelectionChanged_1(object selectedObject)
        {
            if (selectedObject is Company)
            {
                poGrnSummaryView.Visibility = System.Windows.Visibility.Collapsed;
                orderedItemGrnView.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (selectedObject is PurchaseOrder)
            {
                poGrnSummaryView.ViewModel = new PoGrnSummaryViewModel((PurchaseOrder)selectedObject);
                orderedItemGrnView.ViewModel = null;
                poGrnSummaryView.Visibility = System.Windows.Visibility.Visible;
                orderedItemGrnView.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (selectedObject is OrderedItem)
            {
                orderedItemGrnView.ViewModel = new OrderedItemGrnViewModel((OrderedItem)selectedObject);
                poGrnSummaryView.ViewModel = null;
                poGrnSummaryView.Visibility = System.Windows.Visibility.Collapsed;
                orderedItemGrnView.Visibility = System.Windows.Visibility.Visible;
            }
        }


    }
}

