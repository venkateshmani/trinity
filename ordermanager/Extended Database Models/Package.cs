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
                CalculatePendingAndExcessStockQuantity();
                OnPropertyChanged("PackagedWrapper");
            }
        }

        private void CalculatePendingAndExcessStockQuantity()
        {
            decimal totalPackagedQuantity = 0;
            decimal? totalExcessQuantity = 0;
            foreach (Package package in ProductBreakUpSummary.Packages)
            {
                if (package.Date <= this.Date)
                {
                    totalPackagedQuantity += package.PackagedWrapper;
                    totalExcessQuantity += package.ExcessToStockWrapper;
                }
            }

            decimal pending = NumberOfPiecesWrapper - totalPackagedQuantity;

            if (pending < 0)
            {
                ExcessToStockWrapper = Math.Abs(pending) - totalExcessQuantity;
                PendingWrapper = 0;
            }
            else
            {
                PendingWrapper = pending;
                ExcessToStockWrapper = 0;
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
      
        public decimal? ExcessToStockWrapper
        {
            get
            {
                if (ExcessToStock == null)
                    return 0;
                return ExcessToStock.Value;
            }
            set
            {
                ExcessToStock = value;
                OnPropertyChanged("ExcessToStockWrapper");
            }
        }
    }
}
