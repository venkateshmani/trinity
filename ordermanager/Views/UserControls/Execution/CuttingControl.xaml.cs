using ordermanager.DatabaseModel;
using ordermanager.Interfaces_And_Enums;
using ordermanager.Utilities;
using ordermanager.ViewModel.Execution;
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

namespace ordermanager.Views.UserControls.Execution
{
    /// <summary>
    /// Interaction logic for CuttingControl.xaml
    /// </summary>
    public partial class CuttingControl : UserControl, IJobExecutionView
    {
        public CuttingControl()
        {
            InitializeComponent();
            this.Loaded += CuttingControl_Loaded;
        }

        #region View Model Initialization

        void CuttingControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ViewModel = new CuttingViewModel();
        }

        public void SetOrder(Order order)
        {
            ViewModel.Order = order;
        }

        private CuttingViewModel m_ViewModel = null;
        public CuttingViewModel ViewModel
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

        #endregion

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            CommentBox commentBox = new CommentBox(Util.GetParentWindow(this));
            if ((commentBox.ShowDialog() == true))
            {
                m_ViewModel.Save(commentBox.Comment);
            }
        }

        private void TreeViewItemSelected(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item != null)
            {
                TreeViewItem parent = ItemsControl.ItemsControlFromItemContainer(item) as TreeViewItem;
                if (parent != null && parent.Header is OrderProduct)
                {
                    ViewModel.SelectedDate = tvProducts.SelectedItem.ToString();
                    ViewModel.SelectedProduct = parent.Header as OrderProduct;
                    executionDetails.ItemsSource = ViewModel.SelectedProduct.GetCuttings(ViewModel.SelectedDate);
                    executionDetails.Visibility = System.Windows.Visibility.Visible;  
                }
                else
                {
                    m_ViewModel.SelectedProduct = tvProducts.SelectedItem as OrderProduct;
                    executionDetails.Visibility = System.Windows.Visibility.Hidden;   
                }
            }
        }
    }
}
