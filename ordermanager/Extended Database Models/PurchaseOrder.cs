﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class PurchaseOrder : EntityBase
    {

        //public void Validate()
        //{
        //    foreach (var item in OrderedItems)
        //    {
        //        item.ValidateDataForGRN();
        //    }
        //}


        public override bool HasErrors
        {
            get
            {
                bool errors = false;

                if (base.HasErrors)
                    errors = true;

                foreach (var item in OrderedItems)
                {
                    if (item.HasErrors)
                        errors = true;
                }

                return errors;
            }
        }

        public Nullable<System.DateTime> PurchaseOrderDateWrapper
        {
            get { return PurchaseOrderDate; }
            set
            {
                if (PurchaseOrderDate != value)
                {
                    PurchaseOrderDate = value;
                    OnPropertyChanged("PurchaseOrderDateWrapper");
                }
            }
        }

        public void DetermineStatus()
        {
            //Check for received goods count


            //Check for passed quantity issued to stock



            //Check for pruchase order for failed quantity

            
        }


    }
}
