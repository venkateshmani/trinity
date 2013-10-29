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

namespace ordermanager.Views
{
    /// <summary>
    /// Interaction logic for CustomerDetailsControl.xaml
    /// </summary>
    public partial class CustomerDetailsControl : Window
    {
        CompanyType m_CompanyType;
        public CustomerDetailsControl()
        {
            InitializeComponent();
        }

        public CustomerDetailsControl(string type)
        {
            InitializeComponent();
            m_CompanyType = DBResources.Instance.CompanyTypes.Where(c => c.Type == type).Select(c => c).FirstOrDefault();
            txtBoxCST.Visibility = System.Windows.Visibility.Collapsed;
            txtBoxTIN.Visibility = System.Windows.Visibility.Collapsed;
            this.Height = 475;
            if (type == "Supplier")
            {
                lblHeader.Content = "Supplier Details";
                txtBoxCST.Visibility = System.Windows.Visibility.Visible;
                txtBoxTIN.Visibility = System.Windows.Visibility.Visible;
                this.Height = 525;
            }
            else if (type == "Agent")
            {
                lblHeader.Content = "Agent Details";
            }
            else
            {
                lblHeader.Content = "Customer Details";
            }
        }

        private void btnSave_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Company comp = this.DataContext as Company;
            comp.CompanyType = m_CompanyType;
            if (comp.Validate())
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void btnReset_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
