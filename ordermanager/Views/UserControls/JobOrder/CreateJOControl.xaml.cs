using ordermanager.DatabaseModel;
using ordermanager.ViewModel.JobOrderControls;
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
        public event MoveToJOListDelegate OnMoveToJOList = null;
        public CreateJOCtrl()
        {
            InitializeComponent();
            jobOrderType.SelectedIndex = -1;
        }


        public CreateJOViewModel ViewModel
        {
            get
            {
                return this.DataContext as CreateJOViewModel;
            }
            set
            {
                this.DataContext = value;
            }
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
            knittingJOControl.CreateNewJo(Order);
        }

        public void OpenExistingJo(object jo)
        {
            if (jo is DyeingJO)
            {
                dyeingJOControl.OpenExistingJo(jo as DyeingJO);
                dyeingJOControl.Visibility = System.Windows.Visibility.Visible;
                knittingJOControl.Visibility = System.Windows.Visibility.Hidden;
                actionButtonsContainer.Visibility = System.Windows.Visibility.Visible;
                selectedJobOrderControl = dyeingJOControl;
                ViewModel.CurrentViewActionButtons = dyeingJOControl.ViewModel as IActionButtons;
            }
            else if (jo is KnittingJO)
            {
                knittingJOControl.OpenExistingJo(jo as KnittingJO);
                dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                knittingJOControl.Visibility = System.Windows.Visibility.Visible;
                actionButtonsContainer.Visibility = System.Windows.Visibility.Visible;
                selectedJobOrderControl = knittingJOControl;
                ViewModel.CurrentViewActionButtons = knittingJOControl.ViewModel as IActionButtons;
            }

            joTypeSelection.Visibility = System.Windows.Visibility.Collapsed;
            ViewModel.RefreshUIButtons();
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
                        CreateNewDyeingJo();
                        dyeingJOControl.Visibility = System.Windows.Visibility.Visible;
                        knittingJOControl.Visibility = System.Windows.Visibility.Hidden;
                        actionButtonsContainer.Visibility = System.Windows.Visibility.Visible;
                        selectedJobOrderControl = dyeingJOControl;
                    }
                    else if (selectedItem.Content.ToString() == "Knitting")
                    {
                        CreateNewKnittingJo();
                        dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                        knittingJOControl.Visibility = System.Windows.Visibility.Visible;
                        actionButtonsContainer.Visibility = System.Windows.Visibility.Visible;
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

            if (ViewModel != null)
            {
                ViewModel.RefreshUIButtons();
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

            string buttonContent = negativeBtn.Content.ToString();
            switch (buttonContent)
            {
                case "Reject":
                    selectedJobOrderControl.Reject();
                    break;
                case "Discard":
                    selectedJobOrderControl.Discard();
                    break;
            }

            ViewModel.RefreshUIButtons();
        }

        private void positiveBtn_Click_1(object sender, RoutedEventArgs e)
        {
            string buttonContent = positiveBtn.Content.ToString();

            switch (buttonContent)
            {
                case "Generate":
                    if (selectedJobOrderControl.Generate())
                    {
                        if (selectedJobOrderControl is DyeingJOControl)
                        {
                            ((DyeingJOControl)selectedJobOrderControl).ViewModel = null;
                            dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                        }
                        else if(selectedJobOrderControl is KnittingJoControl)
                        {
                            //TODO
                            knittingJOControl.Visibility = System.Windows.Visibility.Hidden;
                        }

                        actionButtonsContainer.Visibility = System.Windows.Visibility.Collapsed;

                        jobOrderType.SelectedItem = null;
                        jobOrderType.SelectedIndex = -1;
                        selectedJobOrderControl = null;

                        if (OnMoveToJOList != null)
                        {
                            OnMoveToJOList();
                        }
                    }
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

            ViewModel.RefreshUIButtons();
        }

        #endregion 

        private void dyeingJOControl_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (dyeingJOControl.Visibility == System.Windows.Visibility.Visible)
            {
                CreateJOViewModel viewModel = new CreateJOViewModel();
                viewModel.CurrentViewActionButtons = dyeingJOControl.ViewModel as IActionButtons;

                this.ViewModel = viewModel;
            }
        }

        private void knittingJOControl_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (knittingJOControl.Visibility == System.Windows.Visibility.Visible)
            {
                CreateJOViewModel viewModel = new CreateJOViewModel();
                viewModel.CurrentViewActionButtons = knittingJOControl.ViewModel as IActionButtons;

                this.ViewModel = viewModel;
            }
        }
    }
}
