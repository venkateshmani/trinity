using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for OrderWorkBench.xaml
    /// </summary>
    public partial class OrderWorkBench : UserControl
    {
        public event OnGoBackDelegate OnGoBack = null;
        public OrderWorkBench()
        {
            InitializeComponent();
            this.Loaded += OrderWorkBench_Loaded;
        }

        void OrderWorkBench_Loaded(object sender, RoutedEventArgs e)
        {
            List<ProductDetails> productDetails = new List<ProductDetails>();
            productDetails.Add(new ProductDetails { ProductName = "T-Shirt", OrderValue = "100$" });
            productDetails.Add(new ProductDetails { ProductName = "Trousers", OrderValue = "200$" });
            productDetails.Add(new ProductDetails { ProductName = "Briefs", OrderValue = "250$" });
            productsList.ItemsSource = productDetails;
        }

        private void GoBack_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (OnGoBack != null)
            {
                OnGoBack();
            }
        }

        private void TabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void Button_PreviewMouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (OnGoBack != null)
                OnGoBack();
        }

        public class ProductDetails
        {
            public string ProductName {get;set;}
            public string OrderValue { get; set; }
        }

        private void productsList_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
            productColumn.Width = productsList.ActualWidth - 5;  //5 for Border
        }
    }
}
