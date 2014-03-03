using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

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

        #region T&C Sub Properties

        private string m_PriceTerms = null;
        private string priceTermsIdentificationKey = "1.Price Terms:";
        [TermsAndConditionsAttribute()]
        public string PriceTerms
        {
            get 
            {
                if (m_PriceTerms == null)
                    SeperateTermsAndConditions();
                return m_PriceTerms; 
            }
            set 
            { 
                m_PriceTerms = value;
                CollateTermsAndConditions();
                ValidateTermsAndConditions();
                OnPropertyChanged("PriceTerms");
            }
        }

        private string m_Freigt = null;
        private string freigtIdentificationKey = "2.Freigt:";
        [TermsAndConditionsAttribute()]
        public string Freigt
        {
            get
            {
                if(m_Freigt == null)
                    SeperateTermsAndConditions();

                return m_Freigt; 
            }
            set 
            {
                m_Freigt = value;
                CollateTermsAndConditions();
                ValidateTermsAndConditions();
                OnPropertyChanged("Freigt");
            }
        }

        private string m_PaymentTerms = null;
        private string paymentIdenficationKey = "3.Payment Terms:";
        [TermsAndConditionsAttribute()]
        public string PaymentTerms
        {
            get 
            {
                if(m_PaymentTerms == null)
                    SeperateTermsAndConditions();

                return m_PaymentTerms; 
            }
            set 
            {
                m_PaymentTerms = value;
                CollateTermsAndConditions();
                ValidateTermsAndConditions();
                OnPropertyChanged("PaymentTerms");
            }
        }

        private string m_DeliveryDate = null;
        private string deliveryDateIdentificationKey = "4.Delivery Date:";
        [TermsAndConditionsAttribute()]
        public string DeliveryDate
        {
            get 
            {
                if(m_DeliveryDate == null)
                    SeperateTermsAndConditions();

                return m_DeliveryDate; 
            }
            set 
            {
                m_DeliveryDate = value;
                CollateTermsAndConditions();
                ValidateTermsAndConditions();
                OnPropertyChanged("DeliveryDate");
            }
        }

        private string m_QualitySpecifications = null;
        private string qualitySpecIdentificationKey = "5.Quality Specifications:";
        [TermsAndConditionsAttribute()]
        public string QualitySpecifications
        {
            get 
            {
                if(m_QualitySpecifications == null)
                    SeperateTermsAndConditions();

                return m_QualitySpecifications; 
            }
            set 
            {
                m_QualitySpecifications = value;
                CollateTermsAndConditions();
                ValidateTermsAndConditions();
                OnPropertyChanged("QualitySpecifications");
            }
        }

        private string m_QuantityAllowance = null;
        private string quantityAllowanceIdentificationKey = "6.Quantity Allowance:";
        [TermsAndConditionsAttribute()]
        public string QuantityAllowance
        {
            get 
            {
                if(m_QuantityAllowance == null)
                    SeperateTermsAndConditions();

                return m_QuantityAllowance; 
            }
            set 
            {
                m_QuantityAllowance = value;
                CollateTermsAndConditions();
                ValidateTermsAndConditions();
                OnPropertyChanged("QuantityAllowance");
            }
        }


        private void CollateTermsAndConditions()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(priceTermsIdentificationKey);
            sb.AppendLine(PriceTerms);
            sb.AppendLine();

            sb.AppendLine(freigtIdentificationKey);
            sb.AppendLine(Freigt);
            sb.AppendLine();

            sb.AppendLine(paymentIdenficationKey);
            sb.AppendLine(PaymentTerms);
            sb.AppendLine();

            sb.AppendLine(deliveryDateIdentificationKey);
            sb.AppendLine(DeliveryDate);
            sb.AppendLine();

            sb.AppendLine(qualitySpecIdentificationKey);
            sb.AppendLine(QualitySpecifications);
            sb.AppendLine();

            sb.AppendLine(quantityAllowanceIdentificationKey);
            sb.AppendLine(QuantityAllowance);
            sb.AppendLine();

            TermsAndConditions = sb.ToString();
        }

        private void SeperateTermsAndConditions()
        {
            if (TermsAndConditions != null)
            {
                string termsAndConditions = TermsAndConditions;

                m_QuantityAllowance = termsAndConditions.Substring(termsAndConditions.IndexOf(quantityAllowanceIdentificationKey));
                termsAndConditions = termsAndConditions.Replace(QuantityAllowance, "");
                m_QuantityAllowance = QuantityAllowance.Replace(quantityAllowanceIdentificationKey, "").Trim();

                m_QualitySpecifications = termsAndConditions.Substring(termsAndConditions.IndexOf(qualitySpecIdentificationKey));
                termsAndConditions = termsAndConditions.Replace(QualitySpecifications, "");
                m_QualitySpecifications = QualitySpecifications.Replace(qualitySpecIdentificationKey, "").Trim();

                m_DeliveryDate = termsAndConditions.Substring(termsAndConditions.IndexOf(deliveryDateIdentificationKey));
                termsAndConditions = termsAndConditions.Replace(DeliveryDate, "");
                m_DeliveryDate = DeliveryDate.Replace(deliveryDateIdentificationKey, "").Trim();

                m_PaymentTerms = termsAndConditions.Substring(termsAndConditions.IndexOf(paymentIdenficationKey));
                termsAndConditions = termsAndConditions.Replace(PaymentTerms, "");
                m_PaymentTerms = PaymentTerms.Replace(paymentIdenficationKey, "").Trim();

                m_Freigt = termsAndConditions.Substring(termsAndConditions.IndexOf(freigtIdentificationKey));
                termsAndConditions = termsAndConditions.Replace(Freigt, "");
                m_Freigt = Freigt.Replace(freigtIdentificationKey, "").Trim();

                m_PriceTerms = termsAndConditions.Substring(termsAndConditions.IndexOf(priceTermsIdentificationKey));
                termsAndConditions = termsAndConditions.Replace(PriceTerms, "");
                m_PriceTerms = PriceTerms.Replace(priceTermsIdentificationKey, "").Trim();
            }
        }

        private void ValidateTermsAndConditions()
        {
            PropertyInfo[] pInfos = this.GetType().GetProperties();
            foreach (PropertyInfo pInfo in pInfos)
            {
                bool hasAttribute = Attribute.IsDefined(pInfo, typeof(TermsAndConditionsAttribute));
                if (hasAttribute)
                {
                    object tAndC = pInfo.GetValue(this);
                    if (tAndC == null || !IsValidTermsAndConditions(tAndC as string))
                    {
                        AddTCError(pInfo.Name);
                    }
                    else
                    {
                        RemoveTCError(pInfo.Name);
                    }
                }
            }
        }

        private string termsAndConditionsErrorMessage = "Invalid Terms & Conditions";
        private void AddTCError(string termName)
        {
            AddError(termName, termsAndConditionsErrorMessage, false);
        }

        private void RemoveTCError(string termName)
        {
            RemoveError(termName, termsAndConditionsErrorMessage);
        }

        private bool IsValidTermsAndConditions(string termsAndConditions)
        {
            if (string.IsNullOrEmpty(termsAndConditions))
                return false;

            string lowerCaseString = termsAndConditions.ToLower().Trim();
            if(lowerCaseString == "na" || lowerCaseString == @"n/a" || lowerCaseString == @"n\a" || lowerCaseString == "not applicable"
                || lowerCaseString == "notapplicable")
            {
                return false;
            }

            return true;
        }

        #endregion 

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


        public string POStatus
        {
            get
            {
                string status = string.Empty;
                if (Approval != null)
                {
                    if (Approval.IsApproved == null)
                        status = "Waiting for Approval";
                    else if (Approval.IsApproved == true)
                        status = "Approved";
                    else if (Approval.IsApproved == false)
                        status = "Rejected";
                }

                return status;
            }
        }


        public void Validate()
        {
            ValidateSupplier();
            ValidatePurchaseOrderDateWrapper();
            ValidateTermsAndConditions();
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

    public class TermsAndConditionsAttribute : Attribute
    {
        
    }
}
