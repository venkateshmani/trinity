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

namespace ordermanager.Views.UserControls.GRN
{
    /// <summary>
    /// Interaction logic for PoGrnSummaryView.xaml
    /// </summary>
    public partial class PoGrnSummaryView : UserControl
    {
        public PoGrnSummaryView()
        {
            InitializeComponent();
        }

        private PoGrnSummaryViewModel m_ViewModel = null;
        public PoGrnSummaryViewModel ViewModel
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

        private void addNewItemBtn_Click_1(object sender, RoutedEventArgs e)
        {
            AddItemsWindow addNewItemsWnd = new AddItemsWindow();
            addNewItemsWnd.AvailableItems = ViewModel.AvailableItemsInPoToCreateGRNReceipt;
            if (addNewItemsWnd.ShowDialog() == true)
            {
                ViewModel.AddItems(addNewItemsWnd.SelectedMaterials);
            }
        }

        private void addNewReceipt_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.AddReceipt();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
