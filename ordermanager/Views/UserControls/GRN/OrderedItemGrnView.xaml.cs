using ordermanager.DatabaseModel;
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
    /// Interaction logic for OrderedItemGrnView.xaml
    /// </summary>
    public partial class OrderedItemGrnView : UserControl
    {
        public OrderedItemGrnView()
        {
            InitializeComponent();
        }

        private OrderedItemGrnViewModel m_ViewModel = null;
        public OrderedItemGrnViewModel ViewModel
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

        private void SupplierComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddNewSupplier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditSupplier_Click(object sender, RoutedEventArgs e)
        {

        }

        private void saveChanges_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                ViewModel.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void issueBtn_Click_1(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag != null && btn.Tag is GRNReciept)
            {
                GRNReciept receipt = btn.Tag as GRNReciept;
                IssueToPopupBox issuePopupBox = new IssueToPopupBox();
                issuePopupBox.Receipt = receipt;
                issuePopupBox.JobQuantity = issuePopupBox.Receipt.QualityPassedQuantityWrapper;
               
                if (issuePopupBox.ShowDialog() == true)
                {
                    if (issuePopupBox.JobOrder.JobOrderType.Type.ToLower() == "stock")
                    {

                    }
                    else
                    {
                        receipt.JobOrders.Add(issuePopupBox.JobOrder);
                    }
                }
                DBResources.Instance.Save();
            }
        }


        private JobOrderType GetJobOrderType(object sender)
        {
            Button issueToBtn = sender as Button;
            if (issueToBtn != null)
            {
                StackPanel parentContainer = issueToBtn.Parent as StackPanel;

                if (parentContainer != null)
                {
                    ComboBox comboBox = parentContainer.FindName("issueToComboBox") as ComboBox;
                    if (comboBox != null)
                    {
                        return comboBox.SelectedItem as JobOrderType;
                    }
                }
            }

            return null;
        }
    }
}
