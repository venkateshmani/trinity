using MahApps.Metro.Controls;
using ordermanager.ViewModel;
using ordermanager.Views.PopUps;
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

namespace ordermanager.Views.UserControls.GRN
{
    /// <summary>
    /// Interaction logic for PoGrnSummaryView.xaml
    /// </summary>
    public partial class PoGrnSummaryView : UserControl
    {
        public PoGrnSummaryView()
        {
            InitializeComponent();
        }

        private PoGrnSummaryViewModel m_ViewModel = null;
        public PoGrnSummaryViewModel ViewModel
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

        private void addNewItemBtn_Click_1(object sender, RoutedEventArgs e)
        {
            AddItemsWindow addNewItemsWnd = new AddItemsWindow();
            addNewItemsWnd.AvailableItems = ViewModel.AvailableItemsInPoToCreateGRNReceipt;
            if (addNewItemsWnd.ShowDialog() == true)
            {
                ViewModel.AddItems(addNewItemsWnd.SelectedMaterials);
            }
        }

        private void addNewReceipt_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.HasUserClickedSaveOrSubmit = true;
                if (!ViewModel.HasErrors)
                {
                    ViewModel.AddReceipt();
                    InformUser("Receipt Added Successfully");
                }
                else
                {
                    InformUser("Fill in the highlighted fields and try again");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                InformUser("Failed to add receipt");
            }
        }

        private void InformUser(string message)
        {
            PopupBox informer = new PopupBox();
            informer.Message = message;
            informer.PopupButton = PopupButton.OK;
            informer.ShowDialog();
        }
    }
}
