using ordermanager.DatabaseModel;
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
    /// Interaction logic for JobWorkControl.xaml
    /// </summary>
    public partial class JobWorkControl : UserControl
    {
        JobWorkViewModel ViewModel;
        public JobWorkControl()
        {
            InitializeComponent();
            ViewModel = new JobWorkViewModel();
            this.DataContext =ViewModel;
            //List<Supplier> supp = new List<Supplier>();
            //supp.Add(new Supplier());
            //supp.Add(new Supplier());
            //supp.Add(new Supplier());
            //tvSuppliers.ItemsSource = supp;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tvSuppliers_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (tvSuppliers.SelectedItem is Company || tvSuppliers.SelectedItem is PurchaseOrder)
            {
                tabControlJobWorks.Visibility = System.Windows.Visibility.Collapsed;
            }

            else if (tvSuppliers.SelectedItem is GRNReciept)
            {
                tabControlJobWorks.Visibility = System.Windows.Visibility.Visible;
                ViewModel.SelectedReceipt = tvSuppliers.SelectedItem as GRNReciept;
            }
        }       

        private void btnNewReceipt_Click(object sender, RoutedEventArgs e)
        {
           //Button btn =  sender as Button;
           //Grid gd = btn.Parent as Grid;
           //if (gd != null)
           //{
               JobOrder jOrder = gridKnittingDetails.SelectedItem as JobOrder;
               if (jOrder != null)
                   ViewModel.AddJobReceipt(jOrder);
           //}
        }
    }


    //public class Supplier
    //{
    //    static int count = 0;
    //    public Supplier()
    //    {
    //        count++;
    //        PurchaseOrders = new List<PurchaseOrderTemp>();
    //        Name = "Supplier" + count.ToString();
    //        PurchaseOrders.Add(new PurchaseOrderTemp());
    //        PurchaseOrders.Add(new PurchaseOrderTemp());
    //        PurchaseOrders.Add(new PurchaseOrderTemp());
    //    }
    //    public string Name
    //    { get; set; }
    //    public List<PurchaseOrderTemp> PurchaseOrders
    //    { get; set; }
    //}

    //public class PurchaseOrderTemp
    //{
    //    static int count = 0;
    //    public PurchaseOrderTemp()
    //    {
    //        count++;
    //        Receipts = new List<Receipt>();
    //        Name = "PurchaseOrder" + count.ToString();
    //        Receipts.Add(new Receipt());
    //        Receipts.Add(new Receipt());
    //        Receipts.Add(new Receipt());
    //    }
    //    public string Name
    //    {
    //        get;
    //        set;
    //    }
    //    public List<Receipt> Receipts
    //    { get; set; }
    //}

    //public class Receipt
    //{
    //    static int count = 0;
    //    public Receipt()
    //    {
    //        count++;
    //        Name = "Receipt" + count.ToString();
    //    }
    //    public string Name
    //    {
    //        get;
    //        set;
    //    }
    //}
}
