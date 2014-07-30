using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.InStock
{
    public class MaterialStockItem : EntityBase
    {
        private SubMaterial StockDBItem = null;
        private MaterialsFromStock FromStock = null;
        public MaterialStockItem(SubMaterial stockDBItem)
        {
            StockDBItem = stockDBItem;
            CanIssue = true;
            QuantityToIssue = null;
            FromStock = new MaterialsFromStock();
        }

        public string MaterialName
        {
            get
            {
                return StockDBItem.Name;
            }
        }

        public decimal Stock
        {
            get
            {
                if (StockDBItem.InStock == null || StockDBItem.InStock.Value == 0)
                {
                    CanIssue = false;
                    OnPropertyChanged("CanIssue");
                    return 0;
                }
                return StockDBItem.InStock.Value;
            }
        }

        public decimal? QuantityToIssue
        { get; set; }

        public string CustomerName
        {
            get;
            private set;
        }
        private long? m_OrderNumber;
        public long? OrderNumber
        {
            get { return m_OrderNumber; }
            set
            {
                if (m_OrderNumber != value)
                {
                    m_OrderNumber = value;
                    if (m_OrderNumber != null)
                    {
                        RemoveError("OrderNumber");
                        Order ord = DBResources.Instance.Context.Orders.Find(m_OrderNumber.Value);
                        if (ord == null)
                        {
                            AddError("OrderNumber", "Order number not found", false);
                            CustomerName = null;
                        }
                        else
                            CustomerName = ord.Customer.Name;
                        OnPropertyChanged("CustomerName");
                    }
                }
            }
        }

        public bool CanIssue
        {
            get;
            private set;
        }

        public bool Validate()
        {
            RemoveError("OrderNumber");
            if (OrderNumber == null)
            {
                AddError("OrderNumber", "Enter order number", false);
            }
            else if (OrderNumber.Value <= 0)
            {
                AddError("OrderNumber", "Order number should be greater than zero.", false);
            }
            else
            {
                Order ord = DBResources.Instance.Context.Orders.Find(OrderNumber.Value);
                if (ord == null)
                    AddError("OrderNumber", "Order number not found", false);
            }

            RemoveError("QuantityToIssue");
            if (QuantityToIssue == null)
                AddError("QuantityToIssue", "Enter issue quantity.", false);
            else if (QuantityToIssue.Value <= 0)
                AddError("QuantityToIssue", "Quantity should be greater than zero.", false);
            else if (QuantityToIssue.Value > Stock)
                AddError("QuantityToIssue", "Issue quantity cannot be more than the quantity in stock.", false);
            return !HasErrors;
        }

        public bool Issue()
        {
            try
            {
               // FromStock.SubMaterialsNameID = StockDBItem.SubMaterialsNameID;
                FromStock.Quantity = QuantityToIssue.Value;
                FromStock.IssuedDate = DBResources.Instance.GetServerTime();
                //FromStock.OrderID = OrderNumber.Value;
                StockDBItem.InStock = Stock - FromStock.Quantity;
                DBResources.Instance.Context.MaterialsFromStocks.Add(FromStock);
                DBResources.Instance.Context.SaveChanges();
                FromStock = new MaterialsFromStock();
                QuantityToIssue = null;
                OrderNumber = null;
                OnPropertyChanged("Stock");
                OnPropertyChanged("QuantityToIssue");
                OnPropertyChanged("OrderNumber");
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
