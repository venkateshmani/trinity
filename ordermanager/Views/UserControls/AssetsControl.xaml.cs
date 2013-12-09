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
    /// Interaction logic for AssetsControl.xaml
    /// </summary>
    public partial class AssetsControl : UserControl
    {
        public AssetsControl()
        {
            InitializeComponent();
            this.Loaded += AssetsControl_Loaded;
        }

        void AssetsControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new AssetViewModel();
        }

        private AssetViewModel m_ViewModel = null;
        public AssetViewModel ViewModel
        {
            get
            {
                return m_ViewModel;
            }
            set
            {
                m_ViewModel = value;
                this.DataContext = value;
            }
        }

        private void AddNewAssetName_Click(object sender, RoutedEventArgs e)
        {
            AddNewAsset();
        }

        private void AddNewAsset()
        {
            if(!string.IsNullOrEmpty(assetName.Text))
            {
                assetName.SelectedItem = m_ViewModel.AddNewAssetName(assetName.Text);
                addNewAsset.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void customerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (assetName.SelectedItem == null || string.IsNullOrWhiteSpace(assetName.Text))
            {
                addNewAsset.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                addNewAsset.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void AssetNameComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (assetName.SelectedItem == null || string.IsNullOrWhiteSpace(assetName.Text))
            {
                addNewAsset.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                addNewAsset.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void AddNewAsset_Click(object sender, RoutedEventArgs e)
        {
            Asset asset = new Asset();
            if (assetName.SelectedItem == null)
                AddNewAsset();

            asset.AssetNameID = (assetName.SelectedItem as AssetName).AssetNameID;           
            asset.Quantity = Convert.ToDecimal(quantity.Text);
            asset.InvoiceNumber = invoiceNumber.Text;
            asset.InvoiceDate = Convert.ToDateTime(invoiceDate.Text);
            asset.ValueInINR = Convert.ToDecimal(valueInINR.Text);
            m_ViewModel.AddNewAsset(asset);

            assetName.SelectedItem = null;
            quantity.Text = string.Empty;
            invoiceNumber.Text = string.Empty;
            invoiceDate.SelectedDate = DateTime.Now;
            valueInINR.Text = string.Empty;

        }
    }
}
