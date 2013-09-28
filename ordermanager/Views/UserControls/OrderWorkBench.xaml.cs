﻿using ordermanager.DatabaseModel;
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
        ProductMaterialsViewModel m_MaterialsViewModel;
        PurchaseOrderControlViewModel m_PurchaseOrderViewModel;
        Order m_Order = null;
        public OrderWorkBench()
        {
            InitializeComponent();
            m_MaterialsViewModel = new ProductMaterialsViewModel();
            materialsControl.DataContext = m_MaterialsViewModel;
            m_PurchaseOrderViewModel = new PurchaseOrderControlViewModel();
            purchaseOrderControl.DataContext = m_PurchaseOrderViewModel;
            this.Loaded += OrderWorkBench_Loaded;
            tabControl.SelectedIndex = 2;
            tabControl.SelectionChanged += tabControl_SelectionChanged;
        }

        public Order Order
        {
            get { return m_Order; }
            set { SetOrder(value); }
        }
      

        public void UpdateView()
        {
            string tabHeader = Convert.ToString(((System.Windows.Controls.HeaderedContentControl)(tabControl.SelectedItem)).Header);
            switch (tabHeader.Trim())
            {
                case "Materials":
                    m_MaterialsViewModel.SetOrder(m_Order);
                    break;
                case "Purchase Order":
                    m_PurchaseOrderViewModel.SetOrder(m_Order);
                    break;
            }
        }

        private void SetOrder(Order order)
        {
            if (m_Order != order)
            {
                m_Order = order;
                UpdateView();
            }
        }

        void OrderWorkBench_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void GoBack_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (OnGoBack != null)
            {
                OnGoBack();
            }
        }

        void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateView();
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
