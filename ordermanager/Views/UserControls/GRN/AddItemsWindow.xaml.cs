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
using System.Windows.Shapes;

namespace ordermanager.Views.UserControls.GRN
{
    /// <summary>
    /// Interaction logic for AddItemsWindow.xaml
    /// </summary>
    public partial class AddItemsWindow 
    {
        public AddItemsWindow()
        {
            InitializeComponent();
        }

        private List<OrderedItem> m_AvailableItems = null;
        List<CheckedListItem<OrderedItem>> checkedListBoxItems = null;
        public List<OrderedItem> AvailableItems
        {
            get
            {
                return m_AvailableItems;
            }
            set
            {
                m_AvailableItems = value;
                checkedListBoxItems = new List<CheckedListItem<OrderedItem>>();
                foreach (OrderedItem item in m_AvailableItems)
                {
                    decimal orderedQuantity = item.OrderedQuantity;
                    decimal recievedQuantitySoFar = 0;
                    foreach (var receipts in item.GRNReciepts)
                    {
                        recievedQuantitySoFar += receipts.InvoicedQuantityWrapper;
                    }

                    if (recievedQuantitySoFar < orderedQuantity)
                    {
                        checkedListBoxItems.Add(new CheckedListItem<OrderedItem>(item));
                    }
                }

                itemsListBox.ItemsSource = checkedListBoxItems;
            }
        }

        public List<OrderedItem> SelectedMaterials
        {
            get
            {
                List<OrderedItem> selectedItems = new List<OrderedItem>();
                foreach (var item in checkedListBoxItems)
                {
                    if (item.IsChecked)
                    {
                        selectedItems.Add(item.Item);
                    }
                }

                return selectedItems;
            }
        }

        private void addNewItemBtn_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
