using ordermanager.DatabaseModel;
using ordermanager.Interfaces_And_Enums;
using ordermanager.ViewModel.Execution;
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
    /// Interaction logic for PackagingControl.xaml
    /// </summary>
    public partial class PackagingControl : UserControl, IJobExecutionView
    {
        public PackagingControl()
        {
            InitializeComponent();
            this.Loaded += PackagingControl_Loaded;
        }

        #region View Model Initialization

            void PackagingControl_Loaded(object sender, RoutedEventArgs e)
            {
                ViewModel = new PackagingViewModel();
            }

            public void SetOrder(Order order)
            {
                ViewModel.Order = order;
            }


            private PackagingViewModel m_ViewModel = null;
            public PackagingViewModel ViewModel
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

        }

        private void TreeViewItemSelected(object sender, RoutedEventArgs e)
        {

        }
    }
}
