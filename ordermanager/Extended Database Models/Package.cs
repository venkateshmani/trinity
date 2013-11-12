using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class Package : EntityBase
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

        public decimal PackagedWrapper
        {
            get
            {
                return Packed;
            }
            set
            {
                Packed = value;
                CalculatePendingQuantity();
                OnPropertyChanged("PackagedWrapper");
            }
        }

        private void CalculatePendingQuantity()
        {
            decimal totalPackagedQuantity = 0;
            foreach (Package package in ProductBreakUpSummary.Packages)
            {
                if (package.Date <= this.Date)
                {
                    totalPackagedQuantity += package.PackagedWrapper;
                }
            }

            decimal pending = NumberOfPiecesWrapper - totalPackagedQuantity;

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
