using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.ViewModel.InStock;

namespace ordermanager.ViewModel
{
    public class InStockViewModel
    {
        public InStockViewModel()
        {
            Stocks = new List<Stock>();

            Stocks.Add(new ProductInStock { Name = "Product Stock" });
            Stocks.Add(new MaterialInStock { Name = "Material Stock" });
        }

        public List<Stock> Stocks
        {
            get;
            set;
        }
    }
}
