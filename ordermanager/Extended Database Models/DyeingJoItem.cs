using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class DyeingJoItem : EntityBase
    {
        public decimal NetQtyWrapper
        {
            get
            {
                return NetQty;
            }
            set
            {
                NetQty = value;
                CalculateTotalAmount();
            }
        }

        public decimal RatePerKgWrapper
        {
            get
            {
                return RatePerKg;
            }
            set
            {
                RatePerKg = value;
                CalculateTotalAmount();
            }
        }

        public decimal TotalAmountWrapper
        {
            get
            {
                return TotalAmount;
            }
            set
            {
                TotalAmount = value;
                CalculateTotalAmount();
                OnPropertyChanged("TotalAmountWrapper");
            }
        }

        private void CalculateTotalAmount()
        {
            TotalAmount = NetQty * RatePerKg;
        }
    }
}
