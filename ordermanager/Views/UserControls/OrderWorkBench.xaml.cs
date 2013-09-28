using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
            long orderId = 0;
            this.DataContext = new ProductMaterialsViewModel(orderId);
            this.Loaded += OrderWorkBench_Loaded;
        }

        void OrderWorkBench_Loaded(object sender, RoutedEventArgs e)
        {
            //List<ProductDetails> productDetails = new List<ProductDetails>();
            //productDetails.Add(new ProductDetails { ItemIndexNumber = 1, ProductName = "T-Shirt", Quantity = "200 Pieces" });
            //productDetails.Add(new ProductDetails { ItemIndexNumber = 2, ProductName = "Trousers", Quantity = "100 Pieces" });
            //productDetails.Add(new ProductDetails { ItemIndexNumber = 3, ProductName = "Briefs", Quantity = "500 Pieces" });
            //productsList.ItemsSource = productDetails;

            //List<MaterialDetails> materialDetails = new List<MaterialDetails>();
            //materialDetails.Add(new MaterialDetails { MaterialName = "Materials 1", CostPerUnit = "$10", Consumption = "100", UOM = "1 KG", ConsumptionCost = "1000" });
            //materialsGrid.ItemsSource = materialDetails;
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


        public class MaterialDetails
        {
            public string MaterialName { get; set; }
            public string CostPerUnit { get; set; }
            public string UOM { get; set; }
            public string Consumption { get; set; }
            public string ConsumptionCost { get; set; }
        }

        public class ProductDetails
        {
            public string ProductName { get; set; }
            public string Quantity { get; set; }
            public int ItemIndexNumber { get; set; }
        }

        private void productsList_SizeChanged_1(object sender, SizeChangedEventArgs e)
        {
           // productColumn.Width = productsList.ActualWidth - 5;  //5 for Border
        }
    }

    public class MaterialsProvider
    {
        private ObservableCollection<string> dtMaterialsProvider = null;
        public MaterialsProvider()
        {
            dtMaterialsProvider = new ObservableCollection<string>();
            dtMaterialsProvider.Add("Materials 1");
            dtMaterialsProvider.Add("Materials 2");
            dtMaterialsProvider.Add("Materials 3");
            dtMaterialsProvider.Add("Materials 4");
            dtMaterialsProvider.Add("Materials 4");
            dtMaterialsProvider.Add("Materials 4");
            dtMaterialsProvider.Add("Materials 4");
            dtMaterialsProvider.Add("Materials 4");
        }

        public ObservableCollection<string> AvailableMaterials
        {
            get
            {
                return dtMaterialsProvider;
            }
        }
    }
}
