using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.InStock
{
    public class ProductInStock : Stock
    {
        public override void Load()
        {
            foreach (ProductStock stockDBItem in DBResources.Instance.Context.ProductStocks)
            {
                ProductStockItem inStockItem = new ProductStockItem(stockDBItem);
                this.Items.Add(inStockItem);
            }

            base.Load();
        }
    }
}
