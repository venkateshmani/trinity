using ordermanager.DatabaseModel;
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

namespace ordermanager.Views.UserControls.JobOrderControls
{
    /// <summary>
    /// Interaction logic for CreateJOCtrl.xaml
    /// </summary>
    public partial class CreateJOCtrl : UserControl
    {

        IJobOrderControl selectedJobOrderControl = null;

        public CreateJOCtrl()
        {
            InitializeComponent();
            jobOrderType.SelectedIndex = -1;
        }

        private Order m_Order = null;
        public Order Order
        {
            get
            {
                return m_Order;
            }
            set
            {
                m_Order = value;
            }
        }

        public void SetOrder(Order order)
        {
            Order = order;
            joTypeSelection.Visibility = System.Windows.Visibility.Visible;
        }

        public void CreateNewDyeingJo()
        {
            dyeingJOControl.CreateNewJo(Order);
        }

        public void CreateNewKnittingJo()
        {

        }

        public void OpenExistingJo(object jo)
        {
            if (jo is DyeingJO)
            {
                dyeingJOControl.OpenExistingJo(jo as DyeingJO);
                dyeingJOControl.Visibility = System.Windows.Visibility.Visible;
                knittingJOControl.Visibility = System.Windows.Visibility.Hidden;
                selectedJobOrderControl = dyeingJOControl;
            }
            else if (jo is KnittingJO)
            {
                dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                knittingJOControl.Visibility = System.Windows.Visibility.Visible;
                selectedJobOrderControl = knittingJOControl;
            }

            joTypeSelection.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void jobOrderType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                ComboBoxItem selectedItem = jobOrderType.SelectedItem as ComboBoxItem;
                if (selectedItem != null && selectedItem.Content != null)
                {
                    if (selectedItem.Content.ToString() == "Dyeing")
                    {
                        dyeingJOControl.Visibility = System.Windows.Visibility.Visible;
                        knittingJOControl.Visibility = System.Windows.Visibility.Hidden;
                        CreateNewDyeingJo();
                        selectedJobOrderControl = dyeingJOControl;
                    }
                    else if (selectedItem.Content.ToString() == "Knitting")
                    {
                        dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                        knittingJOControl.Visibility = System.Windows.Visibility.Visible;
                        CreateNewKnittingJo();
                        selectedJobOrderControl = knittingJOControl;
                    }
                    else
                    {
                        dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                        knittingJOControl.Visibility = System.Windows.Visibility.Hidden;

                        selectedJobOrderControl = null;
                    }
                }
            }
        }

        #region Action Events

        private void commentsBtn_Click_1(object sender, RoutedEventArgs e)
        {
            CommentBox cBox = new CommentBox();
            cBox.UpdateBtnText = "Close";
            cBox.Title = "Comments";
            cBox.IsReadOnly = true;
            //cBox.Comment = ViewModel.PurchaseOrder.Approval.Comments;
            cBox.ShowDialog();
        }

        private void negativeBtn_Click_1(object sender, RoutedEventArgs e)
        {
            string buttonContent = positiveBtn.Content.ToString();
            switch (buttonContent)
            {
                case "Reject":
                    selectedJobOrderControl.Reject();
                    break;
                case "Discard":
                    selectedJobOrderControl.Discard();
                    break;
            }
        }

        private void positiveBtn_Click_1(object sender, RoutedEventArgs e)
        {
            string buttonContent = positiveBtn.Content.ToString();

            switch (buttonContent)
            {
                case "Generate":
                    selectedJobOrderControl.Generate();
                    break;
                case "Submit":
                    selectedJobOrderControl.Submit();
                    break;
                case "Approve":
                    selectedJobOrderControl.Approve();
                    break;
                case "PDF":
                    selectedJobOrderControl.ShowPDF();
                    break;
            }
        }

        #endregion 
    }
}
