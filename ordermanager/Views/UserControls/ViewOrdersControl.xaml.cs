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
            this.Loaded += ViewOrdersControl_Loaded;
            ShowAllOrders = true;
        }

        public bool ShowAllOrders
        {
            get;
            set;
        }

        void ViewOrdersControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(ViewModel == null)
                ViewModel = new ViewOrdersControlViewModel(ShowAllOrders);
        }

        public void Reload()
        {
            ViewModel = new ViewOrdersControlViewModel(ShowAllOrders);
        }

        CollectionViewSource ordersCollectionViewSource = null;
        public ViewOrdersControlViewModel ViewModel
        {
            get
            {
                return m_OrderViewModel;
            }
            set
            {
                this.DataContext = value;
                m_OrderViewModel = value;
                ordersCollectionViewSource = this.FindResource("orderViewSource") as CollectionViewSource;
                ordersCollectionViewSource.Source = value.Orders;
                if (value.Orders.Count == 0)
                    noTasksInfoLabel.Visibility = System.Windows.Visibility.Visible;
                else
                    noTasksInfoLabel.Visibility = System.Windows.Visibility.Hidden;

                ordersCollectionViewSource.SortDescriptions.Add(new System.ComponentModel.SortDescription("OrderDate", System.ComponentModel.ListSortDirection.Descending));
            }
        }

        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        
        private void clearFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            companyNameFilter.SelectedItem = null;
            fromDateFilter.SelectedDate = null;
            toDateFilter.SelectedDate = null;
            searchText.Text = string.Empty;
            orderIDtxtBox.Text = string.Empty;
        }
        
        private void CollectionViewSource_Filter(object sender, FilterEventArgs e)
        {
            Order order = e.Item as Order;


            //Order ID Filter
            int orderID = 0;
            if(int.TryParse(orderIDtxtBox.Text, out orderID))
            {
                if (order.OrderID != orderID)
                {
                    e.Accepted = false;
                    return;
                }
            }

            //Company Name Search
            if (companyNameFilter.SelectedItem != null)
            {
                Company companyToFilter = companyNameFilter.SelectedItem as Company;
                if (order.Company.Name != companyToFilter.Name && order.Company1.Name != companyToFilter.Name)
                {
                    e.Accepted = false;
                    return;
                }
            }

            //Date Time Filter
            if (fromDateFilter.SelectedDate != null)
            {
                if (order.OrderDate < fromDateFilter.SelectedDate)
                {
                    e.Accepted = false;
                    return;
                }
            }

            if (toDateFilter.SelectedDate != null)
            {
                if (order.OrderDate > toDateFilter.SelectedDate)
                {
                    e.Accepted = false;
                    return;
                }
            }

            e.Accepted = true;
        }


        #region Filter Triggers

            private void orderID_TextChanged_1(object sender, TextChangedEventArgs e)
            {
                ordersCollectionViewSource.View.Refresh();
            }

            private void companyNameFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                ordersCollectionViewSource.View.Refresh();
            }

            private void fromDateFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                ordersCollectionViewSource.View.Refresh();
            }

            private void toDateFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
            {
                ordersCollectionViewSource.View.Refresh();
            }

            private void searchText_TextInput(object sender, TextCompositionEventArgs e)
            {
                ordersCollectionViewSource.View.Refresh();
            }

        #endregion 

        private void lvOrders_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (OnOrderClick != null && lvOrders.SelectedItem != null)
            {
                OnOrderClick(lvOrders.SelectedItem);
                DBResources.Instance.LastUpdated = null; //Reset
            }
        }

       
    }
}
