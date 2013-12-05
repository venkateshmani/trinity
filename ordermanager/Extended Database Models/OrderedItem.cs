using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel    
{
    public partial class OrderedItem : EntityBase, ICloneable
    {

        public decimal TotalInvoicedQuantity
        {
            get
            {
                decimal quantity = 0;
                foreach (GRNReciept receipt in this.GRNReciepts)
                {
                    quantity += receipt.RecievedInHandWrapper;
                }

                return quantity;
            }
        }

        public decimal TotalRecievedInHandQuantity
        {
            get
            {
                decimal quantity = 0;
                foreach (GRNReciept receipt in this.GRNReciepts)
                {
                    quantity += receipt.RecievedInHandWrapper;
                }
                return quantity;
            }
        }

        public decimal ExcessQuantityRecieved
        {
            get
            {
                decimal quantity = TotalRecievedInHandQuantity - OrderedQuantity;

                if (quantity > 0)
                {
                    return quantity;
                }

                return 0;
            }
        }

        public decimal PendingQuantityToRecieve
        {
            get
            {
                decimal quantity = OrderedQuantity - TotalRecievedInHandQuantity;

                if (quantity > 0)
                {
                    return quantity;
                }

                return 0;
            }
        }

        public object Clone()
        {
            OrderedItem clonedItem = new OrderedItem();
            clonedItem.ProductMaterialItem = this.ProductMaterialItem;

            return clonedItem;
        }
    }
}
