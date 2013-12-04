using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel    
{
    public partial class OrderedItem : EntityBase, ICloneable
    {

        public object Clone()
        {
            OrderedItem clonedItem = new OrderedItem();
            clonedItem.ProductMaterialItem = this.ProductMaterialItem;

            return clonedItem;
        }
    }
}
