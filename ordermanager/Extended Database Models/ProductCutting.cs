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
                CutQuantity = value;
                OnPropertyChanged("CutQuantityWrapper");
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
