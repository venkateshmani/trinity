using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.InStock
{
    public class ProductStockItem
    {
        private ProductStock StockDBItem = null;
        public ProductStockItem(ProductStock stockDbItem)
        {
            stockDbItem = StockDBItem;
        }

        [DisplayName("ProductName")]
        public string ProductName
        {
            get
            {
                return StockDBItem.ProductName.Name;
            }
        }

        public string StyleID
        {
            get
            {
                return StockDBItem.ProductName.StyleID;
            }
        }

        public string ProductSize
        {
            get
            {
                return StockDBItem.ProductSize.Size;
            }
        }

        public string Color
        {
            get
            {
                return StockDBItem.Color.Name;
            }
        }

        public decimal CuttingStock
        {
            get
            {
                return StockDBItem.CutStock;
            }
        }

        public decimal ProductStock
        {
            get
            {
                return StockDBItem.ProductStock1;
            }
        }
    }
}
