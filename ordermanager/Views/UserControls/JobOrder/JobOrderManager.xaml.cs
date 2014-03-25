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
          
        }

        private void positiveBtn_Click_1(object sender, RoutedEventArgs e)
        {
       
        }

        #endregion 

        private void SupplierList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
