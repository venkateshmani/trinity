using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class MaterialStock : EntityBase
    {
        public decimal Balance
        {
            get
            {
                return StockQuantity - IssuedQuantity;
            }
        }

        public string BalanceString
        {
            get
            {
                return Balance + " " + UnitsOfMeasurement.Units;
            }
        }

        public decimal IssuedQuantityWrapper
        {
            get
            {
                return IssuedQuantity;
            }
            set
            {
                IssuedQuantity = value;
                OnPropertyChanged("IssuedQuantityWrapper");
                OnPropertyChanged("BalanceString");
            }
        }

        public void AddInStockHistory(decimal quantity)
        {
            MaterialInStockHistory history = new MaterialInStockHistory();
            history.Quantity = quantity;
            history.TimeStamp = DBResources.Instance.GetServerTime();
            history.TotalMaterialStockQuantity = Balance;
            this.MaterialInStockHistories.Add(history);
        }
    }
}
