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

        public ViewOrdersControl()
        {
            InitializeComponent();
            List<OrderDetails> items = new List<OrderDetails>();
            items.Add(new OrderDetails() { CustomerName = "John Prakesh", CustomerLocation = "Hyderabad", OrderDate = new DateTime(2014, 09, 19, 11, 16, 00, 12, DateTimeKind.Local), OrderValue = "$10000", Status = "Pending", Group = "My Tasks" });
            items.Add(new OrderDetails() { CustomerName = "Levis India", CustomerLocation = "Delhi", OrderDate = DateTime.Now, OrderValue = "INR 120000", Status = "Production" });
            items.Add(new OrderDetails() { CustomerName = "Sammy", CustomerLocation = "Chennai", OrderDate = DateTime.Now, OrderValue = "$80000", Status = "Dispatched" });
            items.Add(new OrderDetails() { CustomerName = "Doe", CustomerLocation = "Banglore", OrderDate = DateTime.Now, OrderValue = "INR 123456", Status = "Approved", Group = "My Tasks" });
            items.Add(new OrderDetails() { CustomerName = "Robert", CustomerLocation = "Mumbai", OrderDate = DateTime.Now, OrderValue = "GBP 20000", Status = "Pending" });
            items.Add(new OrderDetails() { CustomerName = "David", CustomerLocation = "Hyderabad", OrderDate = DateTime.Now, OrderValue = "INR 170000", Status = "Pending" });
            lvOrders.ItemsSource = items;
        }
       



        private void orderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OnOrderClick != null)
            {
                OnOrderClick();
            }
        }
        public class OrderDetails
        {
            public OrderDetails()
            {
                OrderDate = DateTime.Now;
                Group = "Orders";
            }

            public string OrderID
            {
                get { return OrderDate.ToString("ddMMyyyyHHmmssFFF"); }
            }
            public string CustomerName { get; set; }

            public string CustomerLocation { get; set; }

            public string OrderValue { get; set; }

            public DateTime OrderDate { get; set; }

            public string Status { get; set; }

            public string Group { get; set; }
        }     

        private void lvOrders_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
