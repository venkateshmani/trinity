using ordermanager.DatabaseModel;
using ordermanager.ViewModel.PurchaseOrderControl;
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
using System.Windows.Shapes;

namespace ordermanager.Views.UserControls.PurchaseOrderControls
{
    /// <summary>
    /// Interaction logic for BillOfMaterialBrowser.xaml
    /// </summary>
    public partial class BillOfMaterialBrowser 
    {
        public BillOfMaterialBrowser()
        {
            InitializeComponent();
        }

        public BillOfMaterialBrowser(Order order, ObservableCollection<ProductMaterialItem> purchasableMaterials)
            : this()
        {
            ViewModel = new BillOfMaterialBrowserViewModel(order, purchasableMaterials);
        }

        private BillOfMaterialBrowserViewModel m_ViewModel = null;
        public BillOfMaterialBrowserViewModel ViewModel
        {
            get
            {
                return m_ViewModel;
            }
            set
            {
                m_ViewModel = value;
                this.DataContext = value;
            }
        }

        private void positiveDecisionBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
