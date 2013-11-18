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
    /// Interaction logic for AssetsControl.xaml
    /// </summary>
    public partial class AssetsControl : UserControl
    {
        public AssetsControl()
        {
            InitializeComponent();
            this.Loaded += AssetsControl_Loaded;
        }

        void AssetsControl_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel = new AssetViewModel();
        }

        private AssetViewModel m_ViewModel = null;
        public AssetViewModel ViewModel
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
    }
}
