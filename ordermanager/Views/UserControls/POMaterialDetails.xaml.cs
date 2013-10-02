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
    /// Interaction logic for POMaterialDetails.xaml
    /// </summary>
    public partial class POMaterialDetails : UserControl
    {
        PurchaseOrderControlViewModel m_ViewModel;

        public POMaterialDetails()
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

        private void AddNewItem_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.AddNewProductMaterialItem();
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewSubMaterial(object sender, RoutedEventArgs e)
        {

        }
    }
}
