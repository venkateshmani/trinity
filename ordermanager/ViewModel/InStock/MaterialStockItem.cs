using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.InStock
{
    public class MaterialStockItem
    {
        private SubMaterial StockDBItem = null;
        public MaterialStockItem(SubMaterial stockDBItem)
        {
            StockDBItem = stockDBItem;
        }

        public string MaterialName
        {
            get
            {
                return StockDBItem.Name;
            }
        }

        public decimal Stock
        {
            get
            {
                if (StockDBItem.InStock == null)
                {
                    return 0;
                }

                return StockDBItem.InStock.Value;
            }
        }
    }
}
