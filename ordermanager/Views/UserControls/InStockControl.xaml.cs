using ordermanager.ViewModel;
using ordermanager.ViewModel.InStock;
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

namespace ordermanager.Views.UserControls
{
    /// <summary>
    /// Interaction logic for InStockControl.xaml
    /// </summary>
    public partial class InStockControl : UserControl
    {
        public InStockControl()
        {
            InitializeComponent();
            this.Loaded += InStockControl_Loaded;
        }

        void InStockControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new InStockViewModel();
        }


        private InStockViewModel m_ViewModel = null;
        public InStockViewModel ViewModel
        {
            get
            {
                return m_ViewModel;
            }
            set
            {
                m_ViewModel = value;
                DataContext = value;
            }
        }

        private void stockListCatetogy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stock selectedStock = stockListCatetogy.SelectedItem as Stock;
            if (!selectedStock.IsLoaded)
            {
                selectedStock.Load();
            }

            stockGrid.ItemsSource = selectedStock.Items;
        }
    }
}
