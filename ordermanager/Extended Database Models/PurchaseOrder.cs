using ordermanager.ViewModel;
using System;
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

        public string PurchaseOrderNumberWrapper
        {
            get
            {
                return PurchaseOrderNumber;
            }
            set
            {
                PurchaseOrderNumber = value;
                OnPropertyChanged("PurchaseOrderNumberWrapper");
            }
        }

        public Company Supplier
        {
            get
            {
                return Company;
            }
            set
            {
                Company = value;
                ValidateSupplier();
                OnPropertyChanged("Supplier");
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
                    ValidatePurchaseOrderDateWrapper();
                    OnPropertyChanged("PurchaseOrderDateWrapper");
                }
            }
        }


        public void Validate()
        {
            ValidateSupplier();
            ValidatePurchaseOrderDateWrapper();
        }


        private void ValidateSupplier()
        {
            if (Supplier == null)
            {
                AddError("Supplier", "Please select the supplier", false);
            }
            else
            {
                RemoveError("Supplier", "Please select the supplier");
            }
        }

        private void ValidatePurchaseOrderDateWrapper()
        {
            if (PurchaseOrderDateWrapper == null)
            {
                AddError("PurchaseOrderDateWrapper", "Please select a date", false);
                return;
            }
            else
            {
                RemoveError("PurchaseOrderDateWrapper", "Please select a date");
                return;
            }

            if (PurchaseOrderDateWrapper.Value < DBResources.Instance.GetServerTime())
            {
                AddError("PurchaseOrderDateWrapper", "Date can't be less than today's date", false);
            }
            else
            {
                RemoveError("PurchaseOrderDateWrapper", "Date can't be less than today's date");
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
