using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.Views.UserControls.Stock
{
    public class MaterialStockPerOrderViewModel : ViewModelBase
    {
        public MaterialStockPerOrderViewModel(Order order)
        {
            this.Order = order;
            SelectedOrder = order;
        }

        public ObservableCollection<MaterialStock> Materials
        {
            get
            {
                return new ObservableCollection<MaterialStock>(Order.MaterialStocks);
            }
        }

        public ObservableCollection<Order> Orders
        {
            get
            {
                return new ObservableCollection<Order>(DBResources.Instance.Context.Orders.ToList());
            }
        }

        private Order m_SelectedOrder = null;
        public Order SelectedOrder
        {
            get
            {
                return m_SelectedOrder;
            }
            set
            {
                m_SelectedOrder = value;
            }
        }

        private MaterialStock m_SelectedMaterialStock = null;
        public MaterialStock SelectedMaterialStock
        {
            get
            {
                return m_SelectedMaterialStock;
            }
            set
            {
                if (value != null)
                {
                    m_SelectedMaterialStock = value;
                }
            }
        }

        public ObservableCollection<MaterialsFromStock> StockIssuedFrom
        {
            get
            {
                ObservableCollection<MaterialsFromStock> from = new ObservableCollection<MaterialsFromStock>();
                foreach (var item in SelectedMaterialStock.MaterialsFromStocks)
                {
                    if (item.FromOrderID != this.Order.OrderID)
                        from.Add(item);
                }

                return from;
            }
        }

        public ObservableCollection<MaterialsFromStock> StockIssuedTo
        {
            get
            {
                ObservableCollection<MaterialsFromStock> to = new ObservableCollection<MaterialsFromStock>();
                foreach (var item in SelectedMaterialStock.MaterialsFromStocks)
                {
                    if (item.FromOrderID == this.Order.OrderID)
                        to.Add(item);
                }

                return to;
            }
        }


        public void RefreshProperties()
        {
            foreach (var prop in this.GetType().GetProperties())
            {
                OnPropertyChanged(prop.Name);
            }
        }

        private decimal m_IssueQuantity = 0;
        public decimal IssueQuantity
        {
            get
            {
                return m_IssueQuantity;
            }
            set
            {
                m_IssueQuantity = value;
                ValidateIssueQuantity();
                OnPropertyChanged("IssueQuantity");
            }
        }

        private void ValidateIssueQuantity()
        {
            if (IssueQuantity > SelectedMaterialStock.Balance)
            {
                AddError("IssueQuantity", "Issue quantity is more than the balance in the stock", false);
            }
            else
            {
                RemoveError("IssueQuantity", "Issue quantity is more than the balance in the stock");
            }
        }

        public bool Issue()
        {
            if (this.Order.OrderID == SelectedOrder.OrderID)
            {
                MaterialsFromStock newIssue = DBResources.Instance.Context.MaterialsFromStocks.Create();
                newIssue.Order = this.Order;
                newIssue.Order1 = this.SelectedOrder;
                newIssue.Quantity = IssueQuantity;
                newIssue.IssuedDate = DBResources.Instance.GetServerTime();
                newIssue.MaterialStock = SelectedMaterialStock;
                SelectedMaterialStock.IssuedQuantityWrapper += IssueQuantity;
                this.Order.MaterialsFromStocks.Add(newIssue);
            }
            else
            {
                MaterialStock stockInOtherOrder = null;
                foreach (MaterialStock stock in SelectedOrder.MaterialStocks)
                {
                    if (stock.SubMaterial == SelectedMaterialStock.SubMaterial)
                    {
                        stockInOtherOrder = stock;
                        break;
                    }
                }

                if (stockInOtherOrder == null)
                {
                    stockInOtherOrder = new MaterialStock();
                    stockInOtherOrder.Order = this.SelectedOrder;
                    stockInOtherOrder.SubMaterial = SelectedMaterialStock.SubMaterial;
                    stockInOtherOrder.InStockDateTime = DBResources.Instance.GetServerTime();
                    stockInOtherOrder.StockQuantity = 0;
                    stockInOtherOrder.UnitsOfMeasurement = SelectedMaterialStock.UnitsOfMeasurement;
                    stockInOtherOrder.SubMaterial.MaterialStocks.Add(stockInOtherOrder);
                    this.SelectedOrder.MaterialStocks.Add(stockInOtherOrder);
                }

                stockInOtherOrder.StockQuantity += IssueQuantity;

                //To Record
                MaterialsFromStock to = DBResources.Instance.Context.MaterialsFromStocks.Create();
                to.Order = this.Order;
                to.Order1 = this.SelectedOrder;
                to.Quantity = IssueQuantity;
                to.IssuedDate = DBResources.Instance.GetServerTime();
                to.MaterialStock = SelectedMaterialStock;
                SelectedMaterialStock.IssuedQuantityWrapper += IssueQuantity;
                this.Order.MaterialsFromStocks.Add(to);

                //From Record
                MaterialsFromStock from = DBResources.Instance.Context.MaterialsFromStocks.Create();
                from.Order = this.Order;
                from.Order1 = this.SelectedOrder;
                from.Quantity = IssueQuantity;
                from.IssuedDate = DBResources.Instance.GetServerTime();
                from.MaterialStock = stockInOtherOrder;
                stockInOtherOrder.MaterialsFromStocks.Add(from);



            }
            return DBResources.Instance.Save();
        }
    }
}
