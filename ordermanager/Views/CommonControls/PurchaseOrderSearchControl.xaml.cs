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
                if (value != null)
                {
                    ViewModel = new PurchaseOrderSearchControlViewModel(value);
                }
                else
                {
                    ViewModel = null;
                }
            }
        }

        private void searchText_TextChanged_1(object sender, TextChangedEventArgs e)
        {
           // ApplyFilter();
        }


        //public void ApplyFilter()
        //{
        //    string findText = searchText.Text;
        //    foreach (object supplier in tvSuppliers.Items)
        //    {
        //        TreeViewItem supplierTreeItem = tvSuppliers.ItemContainerGenerator.ContainerFromItem(supplier) as TreeViewItem;

        //        foreach (object purchaseOrder in supplierTreeItem.Items)
        //        {
        //            TreeViewItem purchaseOrderTreeItem = supplierTreeItem.ItemContainerGenerator.ContainerFromItem(purchaseOrder) as TreeViewItem;

        //            foreach (object material in purchaseOrderTreeItem.Items)
        //            {
        //                TreeViewItem materialTreeItem = tvSuppliers.ItemContainerGenerator.ContainerFromItem(material) as TreeViewItem;
                        
        //            }
        //        }
        //    }

        //    foreach (Company supplier in ViewModel.Suppliers)
        //    {
        //        bool supplierNameMatchesSearchCriteria = false;
        //        if(supplier.Name.Contains(searchText.Text, StringComparison.OrdinalIgnoreCase))
        //          supplierNameMatchesSearchCriteria = true;

        //        foreach (PurchaseOrder purchaseOrder in supplier.PurchaseOrders)
        //        {
        //            bool purchaseOrderNoMatchesSearchCriteria = false;
        //            if (supplierNameMatchesSearchCriteria || purchaseOrder.PurchaseOrderNumber.Contains(searchText.Text, StringComparison.OrdinalIgnoreCase))
        //                purchaseOrderNoMatchesSearchCriteria = true;

        //            int numberOfMaterialNameMatchesCriteria = 0;
        //            foreach (OrderedItem orderedItem in purchaseOrder.OrderedItems)
        //            {
        //                if (orderedItem.ProductMaterialItem.SubMaterial.Name.Contains(searchText.Text, StringComparison.OrdinalIgnoreCase) || purchaseOrderNoMatchesSearchCriteria)
        //                {
        //                    orderedItem.Show();
        //                    numberOfMaterialNameMatchesCriteria += 1;
        //                }
        //                else
        //                {
        //                    orderedItem.Hide();
        //                }
        //            }

        //            if (numberOfMaterialNameMatchesCriteria != 0)
        //            {
        //                purchaseOrder.Show();
        //            }
        //            else
        //            {
        //                purchaseOrder.Hide();
        //            }
        //        }
        //    }
        //}

        private void tvPurchaseOrders_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (OnTreeViewSelectionChanged != null)
            {
                DBResources.Instance.DiscardChanges();
                OnTreeViewSelectionChanged(tvPurchaseOrders.SelectedItem);
            }
        }
 
    }
}
