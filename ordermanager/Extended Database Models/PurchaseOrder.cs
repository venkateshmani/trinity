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
            get { return m_PriceTerms; }
            set 
            { 
                m_PriceTerms = value;
                CollateTermsAndCondtions();
                OnPropertyChanged("PriceTerms");
            }
        }

        private string m_Freigt = null;
        private string freigtIdentificationKey = "2.Freigt:";
        [TermsAndConditionsAttribute()]
        public string Freigt
        {
            get { return m_Freigt; }
            set 
            {
                m_Freigt = value;
                CollateTermsAndCondtions();
                OnPropertyChanged("Freigt");
            }
        }

        private string m_PaymentTerms = null;
        private string paymentIdenficationKey = "3.Payment Terms:";
        [TermsAndConditionsAttribute()]
        public string PaymentTerms
        {
            get { return m_PaymentTerms; }
            set 
            {
                m_PaymentTerms = value;
                CollateTermsAndCondtions();
                OnPropertyChanged("PaymentTerms");
            }
        }

        private string m_DeliveryDate = null;
        private string deliveryDateIdentificationKey = "4.Delivery Date:";
        [TermsAndConditionsAttribute()]
        public string DeliveryDate
        {
            get { return m_DeliveryDate; }
            set 
            {
                m_DeliveryDate = value;
                CollateTermsAndCondtions();
                OnPropertyChanged("DeliveryDate");
            }
        }

        private string m_QualitySpecifications = null;
        private string qualitySpecIdentificationKey = "5.Quality Specifications:";
        [TermsAndConditionsAttribute()]
        public string QualitySpecifications
        {
            get { return m_QualitySpecifications; }
            set 
            {
                m_QualitySpecifications = value;
                CollateTermsAndCondtions();
                OnPropertyChanged("QualitySpecifications");
            }
        }

        private string m_QuantityAllowance = null;
        private string quantityAllowanceIdentificationKey = "6.Quantity Allowance:";
        [TermsAndConditionsAttribute()]
        public string QuantityAllowance
        {
            get { return m_QuantityAllowance; }
            set 
            {
                m_QuantityAllowance = value;
                CollateTermsAndCondtions();
                OnPropertyChanged("QuantityAllowance");
            }
        }


        private void CollateTermsAndCondtions()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(priceTermsIdentificationKey);
            sb.AppendLine(PriceTerms);

            sb.AppendLine(freigtIdentificationKey);
            sb.AppendLine(Freigt);

            sb.AppendLine(paymentIdenficationKey);
            sb.AppendLine(PaymentTerms);

            sb.AppendLine(deliveryDateIdentificationKey);
            sb.AppendLine(DeliveryDate);

            sb.AppendLine(qualitySpecIdentificationKey);
            sb.AppendLine(QualitySpecifications);

            sb.AppendLine(quantityAllowanceIdentificationKey);
            sb.AppendLine(QuantityAllowance);

            TermsAndConditions = sb.ToString();
        }

        private void ValidateTermsAndConditions()
        {
            PropertyInfo[] pInfos = this.GetType().GetProperties();
            foreach (PropertyInfo pInfo in pInfos)
            {
                bool hasAttribute = Attribute.IsDefined(pInfo, typeof(TermsAndConditionsAttribute));
                if (hasAttribute)
                {
                    if (!IsValidTermsAndConditions(termsAndConditionsErrorMessage))
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
            string lowerCaseString = termsAndConditions.ToLower().Trim();

            if(string.IsNullOrEmpty(lowerCaseString) || lowerCaseString == "na" || lowerCaseString == "N/A" || lowerCaseString == "not applicable"
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
