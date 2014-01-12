using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.Information
{
    public class MaterialsFromStockSummary
    {
        public MaterialsFromStockSummary(SubMaterial material)
        {
            this.Material = material;
            Quantity = 0;
        }

        public decimal Quantity
        {
            get;
            set;
        }

        public SubMaterial Material
        {
            get;
            set;
        }
    }
}
