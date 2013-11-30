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

namespace ordermanager.Views.CommonControls
{
    /// <summary>
    /// Interaction logic for PurchaseOrderSearchControl.xaml
    /// </summary>
    public partial class PurchaseOrderSearchControl : UserControl
    {

        public event TreeViewSelectionChangedDelegate OnTreeViewSelectionChanged = null;

        #region Fields

        private ObservableCollection<Company> m_Suppliers = null;
        private ObservableCollection<Company> FilteredSuppliersView = null;

        #endregion

        #region Properties

        private PurchaseOrderSearchControlViewModel m_ViewModel = null;
        public PurchaseOrderSearchControlViewModel ViewModel
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

        #endregion 

        public PurchaseOrderSearchControl()
        {
            InitializeComponent();
            this.Loaded += PurchaseOrderSearchControl_Loaded;
        }

        void PurchaseOrderSearchControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new PurchaseOrderSearchControlViewModel();
            tvSuppliers.ItemsSource = ViewModel.Suppliers;
            ApplyFilter();
        }

        private void searchText_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        public void ApplyFilter()
        {
            foreach (Company supplier in ViewModel.Suppliers)
            {
                bool supplierNameMatchesSearchCriteria = false;
                if(supplier.Name.Contains(searchText.Text, StringComparison.OrdinalIgnoreCase))
                  supplierNameMatchesSearchCriteria = true;

                foreach (PurchaseOrder purchaseOrder in supplier.PurchaseOrders)
                {
                    bool purchaseOrderNoMatchesSearchCriteria = false;
                    if (supplierNameMatchesSearchCriteria || purchaseOrder.PurchaseOrderNumber.Contains(searchText.Text, StringComparison.OrdinalIgnoreCase))
                        purchaseOrderNoMatchesSearchCriteria = true;

                    int numberOfMaterialNameMatchesCriteria = 0;
                    foreach (OrderedItem orderedItem in purchaseOrder.OrderedItems)
                    {
                        if (orderedItem.ProductMaterialItem.SubMaterial.Name.Contains(searchText.Text, StringComparison.OrdinalIgnoreCase) || purchaseOrderNoMatchesSearchCriteria)
                        {
                            orderedItem.Show();
                            numberOfMaterialNameMatchesCriteria += 1;
                        }
                        else
                        {
                            orderedItem.Hide();
                        }
                    }

                    if (numberOfMaterialNameMatchesCriteria != 0)
                    {
                        purchaseOrder.Show();
                    }
                    else
                    {
                        purchaseOrder.Hide();
                    }
                }
            }
        }

        private void tvSuppliers_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (OnTreeViewSelectionChanged != null)
            {
                OnTreeViewSelectionChanged(tvSuppliers.SelectedItem);
            }
        }
 
    }
}
