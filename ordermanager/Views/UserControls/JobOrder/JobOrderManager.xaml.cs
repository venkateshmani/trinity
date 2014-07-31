using ordermanager.DatabaseModel;
using ordermanager.Extended_Database_Models;
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
    /// Interaction logic for JobOrderManager.xaml
    /// </summary>
    public partial class JobOrderManager : UserControl
    {
        public JobOrderManager()
        {
            InitializeComponent();
            this.Loaded += JobOrderManager_Loaded;
            this.IsVisibleChanged += JobOrderManager_IsVisibleChanged;
        }

        void JobOrderManager_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == System.Windows.Visibility.Visible)
            {
                if (ViewModel != null)
                {
                    ViewModel = new JobOrderManagerViewModel(Order);
                }
            }
        }

        void JobOrderManager_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        public JobOrderManagerViewModel ViewModel
        {
            get
            {
                return this.DataContext as JobOrderManagerViewModel;
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
            this.Order = order;
            ViewModel = new JobOrderManagerViewModel(order);
        }
            

        #region Action Events

        private void commentsBtn_Click_1(object sender, RoutedEventArgs e)
        {
            CommentBox cBox = new CommentBox();
            cBox.UpdateBtnText = "Close";
            cBox.Title = "Comments";
            cBox.IsReadOnly = true;
            cBox.Comment = ViewModel.SelectedJobOrderInfo.Approval.Comments;
            cBox.ShowDialog();
        }

        private void negativeBtn_Click_1(object sender, RoutedEventArgs e)
        {
          
        }

        private void positiveBtn_Click_1(object sender, RoutedEventArgs e)
        {
       
        }

        #endregion 

        private void SupplierList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IJobOrderInfo jobOrderInfo = supplierList.SelectedItem as IJobOrderInfo;
            if (jobOrderInfo != null)
            {
                ViewModel.SelectedJobOrderInfo = jobOrderInfo;

                if (jobOrderInfo.Type == "Dyeing")
                {
                    joControl.OpenExistingJo(jobOrderInfo as DyeingJO);   
                }
                else if (jobOrderInfo.Type == "Knitting")
                {
                    joControl.OpenExistingJo(jobOrderInfo as KnittingJO);
                }
            }
        }
    }
}
