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
    /// Interaction logic for ProductionControl.xaml
    /// </summary>
    public partial class ProductionControl : UserControl, IJobExecutionView
    {
        public ProductionControl()
        {
            InitializeComponent();
            this.Loaded += ProductionControl_Loaded;
        }

        #region View Model Initialization

            void ProductionControl_Loaded(object sender, RoutedEventArgs e)
            {
                ViewModel = new ProductionViewModel();
            }

            public void SetOrder(Order order)
            {
                ViewModel.Order = order;
            }


            private ProductionViewModel m_ViewModel = null;
            public ProductionViewModel ViewModel
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

        private void ExecutionTreeViewControl_SelectionChanged(DatabaseModel.OrderProduct product, DateTime date)
        {

        }
    }
}
