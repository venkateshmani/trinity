using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class PurchaseOrder : EntityBase
    {

        public void Validate()
        {
            foreach (var item in ProductMaterialItems)
            {
                item.ValidateDataForGRN();
            }
        }
             

        public override bool HasErrors
        {
            get
            {
                bool errors = false;

                if (base.HasErrors)
                    errors = true;

                foreach (var item in ProductMaterialItems)
                {
                    if (item.HasErrors)
                        errors = true;
                }

                return errors;
            }
        }
    }
}
