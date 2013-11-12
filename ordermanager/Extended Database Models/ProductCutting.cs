using ordermanager.ViewModel;
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

                decimal oldExcessToStock =0;
                if(ExcessToStock != null)
                    oldExcessToStock = ExcessToStock.Value;

                ExcessToStock = value;

                decimal newExcessToStock = 0;
                if (ExcessToStock != null)
                    newExcessToStock = value.Value;

                
                var stock  =  (from productStock in DBResources.Instance.Context.ProductStocks
                             where productStock.ProductNameID == this.ProductBreakUpSummary.OrderProduct.ProductNameID &&
                             productStock.ProductSizeID == this.ProductBreakUpSummary.ProductSizeID &&
                             productStock.ColorID == this.ProductBreakUpSummary.ColorID
                             select productStock).FirstOrDefault();

                if (stock == null)
                {
                    ProductStock pStock = DBResources.Instance.Context.ProductStocks.Add(new ProductStock());
                    pStock.ProductName = this.ProductBreakUpSummary.OrderProduct.ProductName;
                    pStock.ProductSize = this.ProductBreakUpSummary.ProductSize;
                    pStock.Color = this.ProductBreakUpSummary.Color;
                    pStock.CutStock = newExcessToStock;
                }
                else
                {
                    stock.CutStock -= oldExcessToStock;
                    stock.CutStock += newExcessToStock;
                }
                     
                OnPropertyChanged("ExcessToStockWrapper");
            }
        }

    }
}
