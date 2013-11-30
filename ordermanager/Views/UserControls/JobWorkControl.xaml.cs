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
        public JobWorkControl()
        {
            InitializeComponent();
            List<Supplier> supp = new List<Supplier>();
            supp.Add(new Supplier());
            supp.Add(new Supplier());
            supp.Add(new Supplier());
            tvSuppliers.ItemsSource = supp;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }


    public class Supplier
    {
        static int count = 0;
        public Supplier()
        {
            count++;
            PurchaseOrders = new List<PurchaseOrderTemp>();
            Name = "Supplier" + count.ToString();
            PurchaseOrders.Add(new PurchaseOrderTemp());
            PurchaseOrders.Add(new PurchaseOrderTemp());
            PurchaseOrders.Add(new PurchaseOrderTemp());
        }
        public string Name
        { get; set; }
        public List<PurchaseOrderTemp> PurchaseOrders
        { get; set; }
    }

    public class PurchaseOrderTemp
    {
        static int count = 0;
        public PurchaseOrderTemp()
        {
            count++;
            Receipts = new List<Receipt>();
            Name = "PurchaseOrder" + count.ToString();
            Receipts.Add(new Receipt());
            Receipts.Add(new Receipt());
            Receipts.Add(new Receipt());
        }
        public string Name
        {
            get;
            set;
        }
        public List<Receipt> Receipts
        { get; set; }
    }

    public class Receipt
    {
        static int count = 0;
        public Receipt()
        {
            count++;
            Name = "Receipt" + count.ToString();
        }
        public string Name
        {
            get;
            set;
        }
    }
}
