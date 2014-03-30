
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.Extended_Database_Models;

namespace ordermanager.DatabaseModel
{
    public partial class DyeingJO  : EntityBase, IJobOrderInfo
    {
        private ObservableCollection<DyeingJoItem> m_Items = null;
        public ObservableCollection<DyeingJoItem> Items
        {
            get
            {
                if (m_Items == null)
                {
                     m_Items = new ObservableCollection<DyeingJoItem>(this.DyeingJoItems);
                     foreach (var item in m_Items)
                     {
                         item.PropertyChanged += item_PropertyChanged; 
                     }
                }

                return m_Items;
            }
        }

        public string PurchaseOrderNumber
        {
            get
            {
                if (PurchaseOrder != null)
                    return PurchaseOrder.PurchaseOrderNumber;

                return string.Empty;
            }
            set
            {
                if (this.Order != null)
                {
                    foreach (var po in this.Order.PurchaseOrders)
                    {
                        if (po.PurchaseOrderNumber == value)
                            PurchaseOrder = po;
                    }
                }
            }
        }

        public void Add()
        {
            DyeingJoItem item = new DyeingJoItem();
            item.DyeingJO = this;
            item.PropertyChanged +=item_PropertyChanged;
            Items.Add(item);

            this.DyeingJoItems.Add(item);
            
            CalcualteTotalAmount();
        }

        public void Remove(DyeingJoItem item)
        {
            item.PropertyChanged -= item_PropertyChanged;
            Items.Remove(item);
            item.DyeingJO = null;
            if(DyeingJoItems.Contains(item))
            {
                DyeingJoItems.Remove(item);
            }
            item.DyeingJO = null;
            CalcualteTotalAmount();
        }

        void item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TotalAmountWrapper")
            {
                CalcualteTotalAmount();
            }
        }

        public decimal TotalValueWrapper
        {
            get
            {
                return TotalValue;
            }
            set
            {
                TotalValue = value;
                OnPropertyChanged("TotalValueWrapper");
            }
        }

        private void CalcualteTotalAmount()
        {
            TotalValueWrapper = 0;
            foreach (var item in Items)
            {
                TotalValueWrapper += item.TotalAmountWrapper;
            }
        }


        #region Wrappers

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
            }
        }

        public DateTime QuoteDateWrapper
        {
            get
            {
                return QuoteDate;
            }
            set
            {
                QuoteDate = value;
                ValidateQuoteDate();
            }
        }

        public string QuoteNoWrapper
        {
            get
            {
                return QuoteNo;
            }
            set
            {
                QuoteNo = value;
                ValidateQuoteNo();
            }
        }

        public string GRNRefNoWrapper
        {
            get
            {
                return GRNRefNo;
            }
            set
            {
                GRNRefNo = value;
                ValidateGRNRefNo();
            }
        }

        public string OrderRefWrapper
        {
            get
            {
                return OrderRef;
            }
            set
            {
                OrderRef = value;
                ValidateOrderRefNo();
            }
        }

        public string ProcessWrapper
        {
            get
            {
                return Process;
            }
            set
            {
                Process = value;
                ValidateProcess();
            }

        }

        public string TermsAndConditionsWrapper
        {
            get
            {
                return TermsAndConditions;
            }
            set
            {
                TermsAndConditions = value;
                ValidateTermsAndConditions();
            }
        }

        #endregion 

        public bool Validate()
        {
            ValidateSupplier();
            ValidatePurchaseOrder();
            ValidateQuoteDate();
            ValidateQuoteNo();
            ValidateGRNRefNo();
            ValidateOrderRefNo();
            ValidateProcess();
            ValidateTermsAndConditions();


            bool hasError = this.HasErrors;

            //Validate Child Items
            foreach (var item in this.Items)
            {
                bool result = item.Validate();
                if (result)
                    hasError = true;
            }

            return hasError;

        }

        
        private void ValidateSupplier()
        {
            if (Company == null)
            {
                AddError("Supplier", "Required", false);
            }
            else
            {
                RemoveError("Supplier", "Required");
            }
        }

        private void ValidatePurchaseOrder()
        {
            if (PurchaseOrder == null)
            {
                AddError("PurchaseORderNumber", "Required", false);
            }
            else
            {
                RemoveError("PurchaseOrderNumber", "Required");
            }
        }

        private void ValidateQuoteDate()
        {
            if (QuoteDate == null)
            {
                AddError("QuoteDateWrapper", "Required", false);
            }
            else
            {
                RemoveError("QuoteDateWrapper", "Required");
            }
        }

        private void ValidateQuoteNo()
        {
            if(string.IsNullOrEmpty(QuoteNo))
            {
                AddError("QuoteNoWrapper", "Required",false);
            }
            else
            {
                RemoveError("QuoteNoWrapper", "Required");
            }
        }

        private void ValidateGRNRefNo()
        {
            if (string.IsNullOrEmpty(GRNRefNo))
            {
                AddError("GRNRefNoWrapper", "Required", false);
            }
            else
            {
                RemoveError("GRNRefNoWrapper", "Required");
            }
        }

        private void ValidateOrderRefNo()
        {
            if (string.IsNullOrEmpty(OrderRef))
            {
                AddError("OrderRefWrapper", "Required", false);
            }
            else
            {
                RemoveError("OrderRefWrapper", "Required");
            }
        }

        private void ValidateProcess()
        {
            if (string.IsNullOrEmpty(Process))
            {
                AddError("ProcessWrapper", "Required", false);
            }
            else
            {
                RemoveError("ProcessWrapper", "Required");
            }
        }

        private void ValidateTermsAndConditions()
        {
            if (string.IsNullOrEmpty(TermsAndConditions))
            {
                AddError("TermsAndConditionsWrapper", "Required", false);
            }
            else
            {
                RemoveError("TermsAndConditionsWrapper", "Required");
            }
        }


        #region IJobOrder Info

        public string SupplierName
        {
            get
            {
                return Supplier.Name;
            }
        }

        public string Status
        {
            get
            {
                string status = string.Empty;

                if (Approval == null)
                    status = string.Empty;
                else if (Approval.IsApproved == null)
                    status = "Waiting For Approval";
                else if (Approval.IsApproved.Value == false)
                    status = "Rejected";
                else if (Approval.IsApproved.Value == true)
                    status = "Approved";

                return status;
            }
            
        }

        public string Type
        {
            get
            {
                return "Dyeing";
            }
           
        }

        public string JobOrderNumber
        {
            get { return JoNo; }
        }

        public void RefreshInfoJobOrderInfo()
        {
            foreach (var pInfo in typeof(IJobOrderInfo).GetProperties())
            {
                OnPropertyChanged(pInfo.Name);
            }
        }

        #endregion

    }
}
