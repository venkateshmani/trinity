using ordermanager.Extended_Database_Models;
using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class KnittingJO : EntityBase, IJobOrderInfo
    {
        private ObservableCollection<KnittingJoItem> m_Items = null;
        public ObservableCollection<KnittingJoItem> Items
        {
            get
            {
                if (m_Items == null)
                {
                    m_Items = new ObservableCollection<KnittingJoItem>(this.KnittingJoItems);
                    foreach (var item in m_Items)
                    {
                        item.PropertyChanged += item_PropertyChanged;
                    }
                }

                return m_Items;
            }
        }

        public void Add()
        {
            KnittingJoItem item = new KnittingJoItem();
            item.KnittingJO = this;
            item.PropertyChanged += item_PropertyChanged;
            Items.Add(item);

            this.KnittingJoItems.Add(item);

            CalcualteTotalAmount();
        }

        public void Remove(KnittingJoItem item)
        {
            item.PropertyChanged -= item_PropertyChanged;
            Items.Remove(item);
            //item.KnittingJO = null;

            if (KnittingJoItems.Contains(item))
            {
                KnittingJoItems.Remove(item);
            }
            //item.KnittingJO = null;
            DBResources.Instance.MarkObjectForDelete(item);
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
                OnPropertyChanged("Supplier");
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
                OnPropertyChanged("QuoteDateWrapper");
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
                OnPropertyChanged("QuoteNoWrapper");
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
                OnPropertyChanged("TermsAndConditionsWrapper");
            }
        }

        public string QualitySpecificationsWrapper
        {
            get
            {
                return QualitySpecifications;
            }
            set
            {
                QualitySpecifications = value;
                ValidateQualitySpecifications();
                OnPropertyChanged("QualitySpecificationsWrapper");
            }
        }


        #endregion

        #region Validation

        public bool Validate()
        {
            ValidateSupplier();
            ValidateQuoteDate();
            ValidateQuoteNo();
            ValidateTermsAndConditions();
            ValidateQualitySpecifications();

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
            if (Supplier == null)
            {
                AddError("Supplier", "Required", false);
            }
            else
            {
                RemoveError("Supplier", "Required");
            }
        }


        private void ValidateQuoteDate()
        {
            if (QuoteDateWrapper == null)
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
            if (string.IsNullOrEmpty(QuoteNo))
            {
                AddError("QuoteNoWrapper", "Required", false);
            }
            else
            {
                RemoveError("QuoteNoWrapper", "Required");
            }
        }

        private void ValidateTermsAndConditions()
        {
            if (string.IsNullOrEmpty(TermsAndConditionsWrapper))
            {
                AddError("TermsAndConditionsWrapper", "Required", false);
            }
            else
            {
                RemoveError("TermsAndConditionsWrapper", "Required");
            }
        }

        private void ValidateQualitySpecifications()
        {
            if (string.IsNullOrEmpty(QualitySpecificationsWrapper))
            {
                AddError("QuailitySpecificationsWrapper", "Required", false);
            }
            else
            {
                RemoveError("QualitySpecificationsWrapper", "Required");
            }
        }

        #endregion



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
                return "Knitting";
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
