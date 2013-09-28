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
    /// Interaction logic for PurchaseOrderControl.xaml
    /// </summary>
    public partial class PurchaseOrderControl : UserControl
    {              
        public PurchaseOrderControl()
        {
            InitializeComponent();  
        }

        private void productsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OrderProduct product = productsList.SelectedItem as OrderProduct;
            if (product != null && this.DataContext != null)
                (DataContext as PurchaseOrderControlViewModel).SelectedItem = product;
        }    
    } 
}


