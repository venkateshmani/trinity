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
    }
}
