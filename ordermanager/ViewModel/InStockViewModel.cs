using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.ViewModel.InStock;
using System.Collections.ObjectModel;
using ordermanager.DatabaseModel;
using System.ComponentModel;

namespace ordermanager.ViewModel
{
    public class InStockViewModel : EntityBase
    {
        public InStockViewModel()
        {
            //Stocks = new List<Stock>();

            //Stocks.Add(new ProductInStock { Name = "Product Stock" });
            //Stocks.Add(new MaterialInStock { Name = "Material Stock" });
            Load();
        }
       
        public ObservableCollection<ProductStockItem> ProductStockItems
        {
            get;
            private set;
        }
       
        public ObservableCollection<MaterialStockItem> MaterialStockItems
        {
            get;
            private set;
        }

        private void Load()
        {
            ProductStockItems = new ObservableCollection<ProductStockItem>();
            foreach (ProductStock stockDBItem in DBResources.Instance.Context.ProductStocks)
            {
                ProductStockItems.Add(new ProductStockItem(stockDBItem));               
            }
            OnPropertyChanged("ProductStockItems");

            MaterialStockItems = new ObservableCollection<MaterialStockItem>();
            foreach (SubMaterial stockDBItem in DBResources.Instance.Context.SubMaterials)
            {
                MaterialStockItems.Add(new MaterialStockItem(stockDBItem));                
            }
            OnPropertyChanged("MaterialStockItems");
        }



        //public List<Stock> Stocks
        //{
        //    get;
        //    set;
        //}
    }
}
