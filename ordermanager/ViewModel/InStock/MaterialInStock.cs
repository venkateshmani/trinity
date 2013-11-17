using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.InStock
{
    class MaterialInStock : Stock
    {
        public override void Load()
        {
            foreach (SubMaterial stockDBItem in DBResources.Instance.Context.SubMaterials)
            {
                MaterialStockItem inStockItem = new MaterialStockItem(stockDBItem);
                this.Items.Add(inStockItem);
            }

            base.Load();
        }      
    }
}
