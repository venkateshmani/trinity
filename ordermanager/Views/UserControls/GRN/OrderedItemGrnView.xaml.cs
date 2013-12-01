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
    /// Interaction logic for OrderedItemGrnView.xaml
    /// </summary>
    public partial class OrderedItemGrnView : UserControl
    {
        public OrderedItemGrnView()
        {
            InitializeComponent();
        }

        private OrderedItemGrnViewModel m_ViewModel = null;
        public OrderedItemGrnViewModel ViewModel
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

        private void SupplierComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddNewSupplier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditSupplier_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
