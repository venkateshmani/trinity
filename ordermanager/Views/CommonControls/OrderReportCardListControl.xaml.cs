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

namespace ordermanager.Views.CommonControls
{
    /// <summary>
    /// Interaction logic for OrderReportCardListControl.xaml
    /// </summary>
    public partial class OrderReportCardListControl : UserControl
    {
        public static readonly DependencyProperty OrderProperty =
                  DependencyProperty.Register("Order", typeof(Order),
                  typeof(OrderReportCardListControl), new FrameworkPropertyMetadata(null, OnOrderPropertyChanged));

        public Order Order
        {
            get { return (Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        private static void OnOrderPropertyChanged(DependencyObject source, 
        DependencyPropertyChangedEventArgs e)
        {
                OrderReportCardListControl control = source as OrderReportCardListControl;
                Order order = (Order)e.NewValue;
                control.reportCardHolder.Children.Clear();

                foreach (OrderReportCard reportCard in order.OrderReportCards)
                {
                    OrderReportCardItemControl item = new OrderReportCardItemControl();
                    item.DataContext = reportCard;
                    item.Margin = new Thickness(5);
                    control.reportCardHolder.Children.Add(item);
                }
            
        }

        public OrderReportCardListControl()
        {
            InitializeComponent();
        }
    }
}
