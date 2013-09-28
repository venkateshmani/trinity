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

namespace ordermanager.Views.UserControls
{
    /// <summary>
    /// Interaction logic for ViewOrdersControl.xaml
    /// </summary>
    public partial class ViewOrdersControl : UserControl
    {
        public event OnOrderClickDelegate OnOrderClick = null;
        private ViewOrdersControlViewModel m_OrderViewModel;
        public ViewOrdersControl()
        {
            InitializeComponent();
            m_OrderViewModel = new ViewOrdersControlViewModel();
            this.DataContext = m_OrderViewModel;
        }      

        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OnOrderClick != null && lvOrders.SelectedItem != null)
            {
                OnOrderClick(lvOrders.SelectedItem);
            }
        }
    }
}
