using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for PurchaseOrderControl.xaml
    /// </summary>
    public partial class PurchaseOrderControl : UserControl
    {
        // private ObservableCollection<KeyValueCollection> m_Items = new ObservableCollection<KeyValueCollection>();
        Dictionary<string, List<PurchaseOrder>> m_Items = new Dictionary<string, List<PurchaseOrder>>();
         List<string> m_Currency = null;
        public PurchaseOrderControl()
        {
            List<PurchaseOrder> items = new List<PurchaseOrder>();
            items.Add(new PurchaseOrder() { Category = "Yarn", ItemName = "Yarn1", Quantity = 100.0, Cost = 12.75, Alias = "Hyderabad" });
            items.Add(new PurchaseOrder() { Category = "Fabric", ItemName = "Fabric1", Quantity = 1564, Cost = 2.25, Alias = "Delhi" });
            items.Add(new PurchaseOrder() { Category = "Accessories", ItemName = "Accessories1", Quantity = 7890, Cost = 35.50, Alias = "Chennai" });
            items.Add(new PurchaseOrder() { Category = "Packing Materials", ItemName = "Material1", Quantity = 50.5, Cost = 48.0, Alias = "Banglore" });
            items.Add(new PurchaseOrder() { Category = "Yarn", ItemName = "Yarn2", Quantity = 298, Cost = 24.5, Alias = "Hyderabad" });
            items.Add(new PurchaseOrder() { Category = "Fabric", ItemName = "Fabric2", Quantity = 56, Cost = 25.0, Alias = "Delhi" });
            items.Add(new PurchaseOrder() { Category = "Accessories", ItemName = "Accessorie2", Quantity = 80, Cost = 5.75, Alias = "Chennai" });
            items.Add(new PurchaseOrder() { Category = "Packing Materials", ItemName = "Material2", Quantity = 28, Cost = 4.0, Alias = "Banglore" });

            List<string> CategoryGroup = new List<string>(4);
            CategoryGroup.Add("Yarn");
            CategoryGroup.Add("Fabric");
            CategoryGroup.Add("Accessories");
            CategoryGroup.Add("Packing Materials");            
      
            m_Currency = new List<string>(3);
            m_Currency.Add("INR");
            m_Currency.Add("USD");
            m_Currency.Add("GBP");

            foreach (string grp in CategoryGroup)
            {
                List<PurchaseOrder> queryItems = (from item in items where item.Category == grp select item).ToList<PurchaseOrder>();
                // m_Items.Add(new KeyValueCollection(){Key=grp, Value=queryItems});
                m_Items.Add(grp, queryItems);               
            }
            InitializeComponent();
            this.DataContext = this;
        }

        // public ObservableCollection<KeyValueCollection> PurchaseOrderItems
        public Dictionary<string, List<PurchaseOrder>> PurchaseOrderItems
        {
            get { return m_Items; }
            set { m_Items = value; }
        }

        public List<string> CurrencyValue
        {
            get { return m_Currency; }
        }

        public class KeyValueCollection
        {
            public string Key;
            public List<PurchaseOrder> Value;
        }
    }
    public class PurchaseOrder
    {
        List<string> m_Currency = null;
        public PurchaseOrder()
        {
            Currency = "INR";
        }

        public string Category
        {
            get;
            set;
        }

        public string ItemName
        {
            get;
            set;
        }

        public string Currency
        {
            get;
            set;
        }

        public string Alias
        {
            get;
            set;
        }

        public double Cost
        {
            get;
            set;
        }

        public double Quantity
        {
            get;
            set;
        }

    }


}


