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
using System.Windows.Shapes;
using ordermanager.ViewModel.Invoice;

namespace ordermanager.Views.UserControls.Invoice
{
    /// <summary>
    /// Interaction logic for CatronBoxBrowser.xaml
    /// </summary>
    public partial class CatronBoxBrowser 
    {
        public CatronBoxBrowser()
        {
            InitializeComponent();

        }

        private CartonBoxBrowserViewModel m_ViewModel = null;
        public CartonBoxBrowserViewModel ViewModel
        {
            get { return m_ViewModel; }
            set
            {
                m_ViewModel = value;
                this.DataContext = value;
            }
        }

        private void CartonBoxList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
