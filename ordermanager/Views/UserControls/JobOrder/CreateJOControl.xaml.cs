using ordermanager.DatabaseModel;
using ordermanager.Interfaces_And_Enums;
using ordermanager.ViewModel;
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
        public event OnCloseDialogDelegate OnCloseDialog = null;
        IJobOrderControl selectedJobOrderControl = null;
        public event MoveToJOListDelegate OnMoveToJOList = null;
        public CreateJOCtrl()
        {
            InitializeComponent();
            jobOrderType.SelectedIndex = -1;
        }


        public void InitializeForCompacting()
        {
            jobOrderType.SelectedIndex = 3;
            InitializeControls();
        }

        public void InitializeForDyeing()
        {
            jobOrderType.SelectedIndex = 0;
            InitializeControls();
        }

        public void InitializeForKnitting()
        {
            jobOrderType.SelectedIndex = 1;
            InitializeControls();
        }


        private bool m_JobOrderIssued = false;
        public bool JobOrderIssued
        {
            get
            {
                return m_JobOrderIssued;
            }
            set
            {
                m_JobOrderIssued = value;
            }
        }

        private decimal m_Quantity = 0;
        public decimal Quantity
        {
            get
            {
                return m_Quantity;
            }
            set
            {
                m_Quantity = value;
            }
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

        private PurchaseOrder m_PurchaseOrder = null;
        public PurchaseOrder PurchaseOrder
        {
            get
            {
                return m_PurchaseOrder;
            }
            set
            {
                m_PurchaseOrder = value;
            }
        }

        private string m_GRNRefNo = string.Empty;
        public string GRNRefNo
        {
            get
            {
                return m_GRNRefNo;
            }
            set
            {
                m_GRNRefNo = value;
            }
        }

        private GRNReciept m_GRNReciept = null;
        public GRNReciept GRNReciept
        {
            get
            {
                return m_GRNReciept;
            }
            set
            {
                m_GRNReciept = value;
            }
        }

        private JobOrder m_ParentJobOrder = null;
        public JobOrder ParentJobOrder
        {
            get
            {
                return m_ParentJobOrder;
            }
            set
            {
                m_ParentJobOrder = value;
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
            dyeingJOControl.CreateNewJo(Order, Quantity, PurchaseOrder, GRNRefNo, GRNReciept, JobOrderIssued, ParentJobOrder);
        }

        public void CreateNewKnittingJo()
        {
            knittingJOControl.CreateNewJo(Order, Quantity, GRNReciept, JobOrderIssued, ParentJobOrder);
        }

        public void CreateNewCompactingJo()
        {
            compactingJoControl.CreateNewJo(Order, Quantity, PurchaseOrder, GRNRefNo, GRNReciept, JobOrderIssued, ParentJobOrder); 
        }

        public void OpenExistingJo(object jo)
        {
            if (jo is DyeingJO)
            {
                dyeingJOControl.OpenExistingJo(jo as DyeingJO);
                dyeingJOControl.Visibility = System.Windows.Visibility.Visible;
                knittingJOControl.Visibility = System.Windows.Visibility.Hidden;
                compactingJoControl.Visibility = Visibility.Hidden;
                actionButtonsContainer.Visibility = System.Windows.Visibility.Visible;
                selectedJobOrderControl = dyeingJOControl;
                ViewModel.CurrentViewActionButtons = dyeingJOControl.ViewModel as IActionButtons;
            }
            else if (jo is KnittingJO)
            {
                knittingJOControl.OpenExistingJo(jo as KnittingJO);
                dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                knittingJOControl.Visibility = System.Windows.Visibility.Visible;
                compactingJoControl.Visibility = Visibility.Hidden;
                actionButtonsContainer.Visibility = System.Windows.Visibility.Visible;
                selectedJobOrderControl = knittingJOControl;
                ViewModel.CurrentViewActionButtons = knittingJOControl.ViewModel as IActionButtons;
            }
            else if (jo is CompactingJo)
            {
                compactingJoControl.OpenExistingJo(jo as CompactingJo);
                dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                knittingJOControl.Visibility = System.Windows.Visibility.Hidden;
                compactingJoControl.Visibility = Visibility.Visible;
                actionButtonsContainer.Visibility = System.Windows.Visibility.Visible;
                selectedJobOrderControl = compactingJoControl;
                ViewModel.CurrentViewActionButtons = compactingJoControl.ViewModel as IActionButtons;
            }

            joTypeSelection.Visibility = System.Windows.Visibility.Collapsed;
            ViewModel.RefreshUIButtons();
        }

        private void jobOrderType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                InitializeControls();
            }
        }

        private void InitializeControls()
        {
            ComboBoxItem selectedItem = jobOrderType.SelectedItem as ComboBoxItem;
            if (selectedItem != null && selectedItem.Content != null)
            {
                if (selectedItem.Content.ToString() == "Dyeing")
                {
                    CreateNewDyeingJo();
                    dyeingJOControl.Visibility = System.Windows.Visibility.Visible;
                    knittingJOControl.Visibility = System.Windows.Visibility.Hidden;
                    compactingJoControl.Visibility = Visibility.Hidden;
                    actionButtonsContainer.Visibility = System.Windows.Visibility.Visible;
                    selectedJobOrderControl = dyeingJOControl;
                }
                else if (selectedItem.Content.ToString() == "Knitting")
                {
                    CreateNewKnittingJo();
                    dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                    knittingJOControl.Visibility = System.Windows.Visibility.Visible;
                    compactingJoControl.Visibility = Visibility.Hidden;
                    actionButtonsContainer.Visibility = System.Windows.Visibility.Visible;
                    selectedJobOrderControl = knittingJOControl;
                }
                else if (selectedItem.Content.ToString() == "Compacting")
                {
                    CreateNewCompactingJo();
                    dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                    knittingJOControl.Visibility = System.Windows.Visibility.Hidden;
                    compactingJoControl.Visibility = Visibility.Visible;
                    actionButtonsContainer.Visibility = System.Windows.Visibility.Visible;
                    selectedJobOrderControl = compactingJoControl;
                }
                else
                {
                    dyeingJOControl.Visibility = System.Windows.Visibility.Hidden;
                    knittingJOControl.Visibility = System.Windows.Visibility.Hidden;

                    selectedJobOrderControl = null;
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
                        }
                        else if (selectedJobOrderControl is KnittingJoControl)
                        {
                            ((KnittingJoControl)selectedJobOrderControl).ViewModel = null;
                        }
                        else if (selectedJobOrderControl is CompactingJoControl)
                        {
                            ((CompactingJoControl) selectedJobOrderControl).ViewModel = null;
                        }

                        actionButtonsContainer.Visibility = System.Windows.Visibility.Collapsed;
                        selectedJobOrderControl = null;
                      
                        OnCloseDialog(true);
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

        private void CompactingJoControl_OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (compactingJoControl.Visibility == System.Windows.Visibility.Visible)
            {
                var viewModel = new CreateJOViewModel
                {
                    CurrentViewActionButtons = compactingJoControl.ViewModel
                };

                this.ViewModel = viewModel;
            }
        }
    }
}
