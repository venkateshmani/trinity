using MahApps.Metro.Controls;
using ordermanager.DatabaseModel;
using ordermanager.Extended_Database_Models;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
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
    /// Interaction logic for GRNControl.xaml
    /// </summary>
    public partial class GRNControl : UserControl
    {
        public GRNControl()
        {
            InitializeComponent();
            this.Loaded += GRNControl_Loaded;
        }

        GRNViewModel m_GRNViewModel = null;
        public GRNViewModel ViewModel
        {
            get
            {
                return m_GRNViewModel;
            }
            set
            {
                m_GRNViewModel = value;
                this.DataContext = value;
            }
        }
        void GRNControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel = new GRNViewModel();
            this.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsMaterialsReceiptReadOnly" || e.PropertyName == "IsQualityReadOnly")
                grnDetailsControl_SelectionChanged_1(grnDetailsControl, null);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.HasErrors)
            {
                InformUser("Fix the Errors highlighted in red borders and try again !!");
            }
            else
            {
                ViewModel.Save(false);
                InformUser("Sucessfully Saved !!");
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.HasErrors)
            {
                InformUser("Fix the Errors highlighted in red borders and try again !!");
            }
            else
            {
                ViewModel.Save(true);
                InformUser("Submitted Successfully !!");
            }
        }

        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
        }


        private void tvProducts_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvProducts.SelectedItem != null)
            {
                DBResources.Instance.DiscardChanges();
                if (tvProducts.SelectedItem is Company)
                {
                    supplierSelectedInfo.Visibility = System.Windows.Visibility.Visible;
                    grnDetailsControl.Visibility = System.Windows.Visibility.Hidden;
                }
                else if (tvProducts.SelectedItem is PurchaseOrder)
                {
                    PurchaseOrder po = tvProducts.SelectedItem as PurchaseOrder;
                    ViewModel.SelectedPurchaseOrder = po;
                    materialsReciptGrid.ItemsSource = po.OrderedItems;
                    qualityGrid.ItemsSource = po.OrderedItems;
                    grnDetailsControl.Visibility = System.Windows.Visibility.Visible;
                    supplierSelectedInfo.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        private void grnDetailsControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            gridButtons.Visibility = System.Windows.Visibility.Collapsed;

            if (ViewModel.SelectedPurchaseOrder != null)
            {
                string tabHeader = Convert.ToString(((System.Windows.Controls.HeaderedContentControl)(grnDetailsControl.SelectedItem)).Header).Trim();
                switch (tabHeader)
                {
                    case "Materials Receipt":
                        if (ViewModel.SelectedPurchaseOrder.PurchaseOrderStatusID == 1)
                            gridButtons.Visibility = System.Windows.Visibility.Visible;
                      break;
                    case "Quality":
                        if (ViewModel.SelectedPurchaseOrder.PurchaseOrderStatusID == 2)
                            gridButtons.Visibility = System.Windows.Visibility.Visible;
                      break;
                }
            }
        }

    }
}
