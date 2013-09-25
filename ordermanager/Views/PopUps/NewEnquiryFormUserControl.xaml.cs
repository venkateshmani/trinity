using ordermanager.DatabaseModel;
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

        Order newEnquiry = null;
        void NewEnquiryFormUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            newEnquiry = new Order();
            this.DataContext = newEnquiry;
        }

        private void addNewCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomerDetailsControl details = new CustomerDetailsControl();
            details.ShowDialog();
        }
    }
}
