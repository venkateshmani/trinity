using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class ProductCutting : EntityBase
    {
        public ProductSize SizeWrapper
        {
            get
            {
                return ProductBreakUpSummary.ProductSize;
            }
        }

        public Color ColorWrapper
        {
            get
            {
                return ProductBreakUpSummary.Color;
            }
        }

        public decimal NumberOfPiecesWrapper
        {
            get
            {
                return ProductBreakUpSummary.NumberOfPieces;
            }
        }

        public decimal CutQuantityWrapper
        {
            get
            {
                return CutQuantity;
            }
            set
            {
                if (CutQuantity != value)
                {
                    CutQuantity = value;
                    CalculatePendingQuantity();
                    OnPropertyChanged("CutQuantityWrapper");
                }
            }
        }

        public decimal PendingWrapper
        {
            get
            {
                return Pending;
            }
            set
            {
                Pending = value;
                OnPropertyChanged("PendingWrapper");
            }
        }

        private void CalculatePendingQuantity()
        {
            decimal totalCutQuantity = 0;
            foreach (ProductCutting cutting in ProductBreakUpSummary.ProductCuttings)
            {
                if (cutting.Date <= this.Date)
                {
                    totalCutQuantity += cutting.CutQuantity;
                }
            }

            decimal pending = NumberOfPiecesWrapper - totalCutQuantity;

            if (pending < 0)
            {
                decimal totalExcess = 0;

                foreach (ProductCutting cutting in ProductBreakUpSummary.ProductCuttings)
                {
                    if (cutting.Date < this.Date)
                    {
                        totalExcess += cutting.ExcessToStockWrapper.Value;
                    }
                }

                ExcessToStockWrapper = Math.Abs(pending) - totalExcess; 
                PendingWrapper = 0;
            }
            else
            {
                PendingWrapper = pending;
            }
        }

        public decimal? ExcessToStockWrapper
        {
            get
            {
                if (ExcessToStock == null)
                    return 0;

                return ExcessToStock;
            }
            set
            {
                ExcessToStock = value;
                OnPropertyChanged("ExcessToStockWrapper");
            }
        }

    }
}
