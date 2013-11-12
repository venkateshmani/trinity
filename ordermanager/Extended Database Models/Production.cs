using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class Production : EntityBase
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

        public decimal CompletedQuantityWrapper
        {
            get
            {
                return CompletedQuantity;
            }
            set
            {
                CompletedQuantity = value;
                CalculatePendingQuantity();
                OnPropertyChanged("CompletedQuantityWrapper");
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
            decimal totalCompletedQuantity = 0;
            foreach (Production production in ProductBreakUpSummary.Productions)
            {
                if (production.Date <= this.Date)
                {
                    totalCompletedQuantity += production.CompletedQuantity;
                }
            }

            PendingWrapper = NumberOfPiecesWrapper - totalCompletedQuantity;
        }
    }
}
