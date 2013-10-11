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
    /// Interaction logic for POProductDetails.xaml
    /// </summary>
    public partial class POProductDetails : UserControl
    {
        PurchaseOrderControlViewModel m_ViewModel;
        public POProductDetails()
        {
            InitializeComponent();
        }

        public PurchaseOrderControlViewModel ViewModel
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

        private void btnAddNewCountry_Click(object sender, RoutedEventArgs e)
        {
            if (m_ViewModel != null)
                m_ViewModel.AddNewCountry();
        }
    }
}
