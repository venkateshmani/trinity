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

namespace ordermanager.Views.PopUps
{
    /// <summary>
    /// Interaction logic for NewEnquiryFormUserControl.xaml
    /// </summary>
    public partial class NewEnquiryFormUserControl : UserControl
    {
        public NewEnquiryFormUserControl()
        {
            InitializeComponent();
            this.Loaded += NewEnquiryFormUserControl_Loaded;
        }

        
        void NewEnquiryFormUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            NewEnquiryViewModel = new NewEnquiryViewModel();
        }

        private void addNewCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerDetailsControl details = new CustomerDetailsControl();
            details.ShowDialog();
        }

        private NewEnquiryViewModel m_NewEnquiryViewModel = null;
        public NewEnquiryViewModel NewEnquiryViewModel
        {
            get
            {
                return m_NewEnquiryViewModel;
            }
            set
            {
                m_NewEnquiryViewModel = value;
                this.DataContext = value;
            }
        }

        private void currencyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void addNewItemBtn_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            NewEnquiryViewModel.OrderProducts.Add(new OrderProduct());
        }
    }
}
