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
                ExcessToStockWrapper = Math.Abs(pending);
                PendingWrapper = 0;
            }
            else
            {
                PendingWrapper = pending;
            }
        }

        private decimal m_ExcessToStock = 0m;
        public decimal ExcessToStockWrapper
        {
            get
            {
                return m_ExcessToStock;
            }
            set
            {
                m_ExcessToStock = value;
                OnPropertyChanged("ExcessToStockWrapper");
            }
        }

    }
}
