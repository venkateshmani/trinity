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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ordermanager.Views.UserControls.Stock
{
    /// <summary>
    /// Interaction logic for MaterialStockPerOrderControl.xaml
    /// </summary>
    public partial class MaterialStockPerOrderControl : UserControl
    {
        public MaterialStockPerOrderControl()
        {
            InitializeComponent();
        }

        public MaterialStockPerOrderViewModel ViewModel
        {
            get
            {
                return this.DataContext as MaterialStockPerOrderViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        public Order Order
        {
            get
            {
                if (this.ViewModel != null)
                    return this.ViewModel.Order;

                return null;
            }
            set
            {
                ViewModel = new MaterialStockPerOrderViewModel(value);
            }
        }

        private void materialsList_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (materialsList.SelectedItem != null)
            {
                ViewModel.SelectedMaterialStock = materialsList.SelectedItem as MaterialStock;
                materialRecievedHistory.ItemsSource = ViewModel.StockIssuedFrom;
                materialIssuedHistory.ItemsSource = ViewModel.StockIssuedTo;
            }
        }

        private void issueButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.HasErrors)
            {
                if (ViewModel.Issue())
                {
                    ViewModel.IssueQuantity = 0;
                    materialRecievedHistory.ItemsSource = ViewModel.StockIssuedFrom;
                    materialIssuedHistory.ItemsSource = ViewModel.StockIssuedTo;
                }
            }
        }
    }
}
