using ordermanager.DatabaseModel;
using ordermanager.ViewModel.Information;
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

namespace ordermanager.Views.UserControls.Information
{
    /// <summary>
    /// Interaction logic for FromStock.xaml
    /// </summary>
    public partial class MaterialsFromStock : UserControl
    {
        public MaterialsFromStock()
        {
            InitializeComponent();
        }

        private OrderInformationViewModel m_ViewModel = null;
        public OrderInformationViewModel ViewModel
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

        public void SetOrder(Order order)
        {
            if (ViewModel != null && ViewModel.Order == order)
                return;

            ViewModel = new OrderInformationViewModel(order);
        }
    }
}
