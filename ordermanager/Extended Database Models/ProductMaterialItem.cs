using ordermanager.DatabaseModel;
using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class ProductMaterialItem : EntityBase
    {
        #region [Wrappers]
        public virtual UnitsOfMeasurement UnitsOfMeasurementWrapper
        {
            get
            {
                return UnitsOfMeasurement;
            }
            set
            {
                UnitsOfMeasurement = value;
                ValidateUOM();
            }
        }

        public virtual Currency CurrencyWrapper
        {
            get
            {
                return Currency;
            }
            set
            {
                Currency = value;
                ValidateCurrency();
                SelectOrAddCurrencyConversion();
                CalculateItemCost();
                OnPropertyChanged("CurrencyValueInINR");
            }
        }

        public virtual Company SupplierWrapper
        {
            get
            {
                return Company;
            }
            set
            {
                if (Company != value)
                {
                    Company = value;
                }
            }
        }

        public virtual decimal CostWrapper
        {
            get
            {
                return Cost;
            }
            set
            {
                if (Cost != value)
                {
                    Cost = value;
                    CalculateItemCost();
                }
                ValidateCost();
            }
        }

        public virtual decimal QuantityWrapper
        {
            get
            {
                return Quantity;
            }
            set
            {
                if (Quantity != value)
                {
                    Quantity = value;
                    CalculateItemCost();
                }
                ValidateQuantity();
            }
        }

        public decimal InvoicedQuantityWrapper
        {
            get
            {
                decimal value = 0;
                if (this.InvoicedQuantity != null && this.InvoicedQuantity.HasValue)
                    value = this.InvoicedQuantity.Value;

                return value;
            }
            set
            {
                this.InvoicedQuantity = value;
                ValidateInvoicedQuantity();
                CalculateExcessiveQuantity();
                OnPropertyChanged("InvoicedQuantityWrapper");
            }
        }


        public decimal RecievedInHandWrapper
        {
            get
            {
                decimal value = 0;
                if (this.RecievedInHand != null && this.RecievedInHand.HasValue)
                    value = this.RecievedInHand.Value;

                return value;
            }
            set
            {
                this.RecievedInHand = value;
                ValidateRecievedQuantity();
                CalculateExcessiveQuantity();
                OnPropertyChanged("RecievedInHandWrapper");
            }
        }

        public DateTime? InvoicedDateWrapper
        {
            get
            {
                return InvoiceDate;
            }
            set
            {
                InvoiceDate = value;
                ValidateInvoiceDate();
                OnPropertyChanged("InvoicedDateWrapper");
            }
        }


        public DateTime? RecievedDateWrapper
        {
            get
            {
                return RecievedDate;
            }
            set
            {
                RecievedDate = value;
                ValidateRecievedDate();
                OnPropertyChanged("RecievedDateWrapper");
            }
        }

        public decimal ExcessiveQuantityWrapper
        {
            get
            {
                decimal value = 0;
                if (this.ExcessiveQuantity != null && this.ExcessiveQuantity.HasValue)
                    value = this.ExcessiveQuantity.Value;

                return value;
            }
            set
            {
                this.ExcessiveQuantity = value;
                OnPropertyChanged("ExcessiveQuantityWrapper");
            }
        }

        private void CalculateExcessiveQuantity()
        {
            ExcessiveQuantityWrapper = RecievedInHandWrapper - InvoicedQuantityWrapper;
        }
        

        decimal m_ItemCostWrapper;
        public virtual decimal ItemCostWrapper
        {
            get { return m_ItemCostWrapper; }
            set
            {
                if (m_ItemCostWrapper != value)
                {
                    m_ItemCostWrapper = value;
                    OnPropertyChanged("ItemCostWrapper");
                }
            }
        }

        public virtual SubMaterial SubMaterialWrapper
        {
            get
            {
                return this.SubMaterial;
            }
            set
            {
                if (!DontLoosePropertyValue)
                {
                    this.SubMaterial = value;
                    OnPropertyChanged("SubMaterialWrapper");
                }
            }
        }


        public decimal QualityPassedQuantityWrapper
        {
            get
            {
                decimal value = 0;
                if (this.QualityPassedQuantity != null && this.QualityPassedQuantity.HasValue)
                    value = this.QualityPassedQuantity.Value;

                return value;
            }
            set
            {
                this.QualityPassedQuantity = value;
                ValidateQualityPassedQuantity();
                OnPropertyChanged("QualityPassedQuantityWrapper");
            }
        }

        public decimal QualityFailedQuantityWrapper
        {
            get
            {
                decimal value = 0;
                if (this.QualityFailedQuantity != null && this.QualityFailedQuantity.HasValue)
                    value = this.QualityFailedQuantity.Value;

                return value;
            }
            set
            {
                this.QualityFailedQuantity = value;
                OnPropertyChanged("QualityFailedQuantityWrapper");
            }
        }

        #endregion [Wrappers]

        
        #region [Helpers]

        public bool IsEditable
        {
            get
            {
                if (!DBResources.Instance.CurrentUser.UserRole.CanAddSubMaterials)
                    return false;

                if( this.ProductMaterial != null && this.ProductMaterial.OrderProduct != null &&
                    this.ProductMaterial.OrderProduct.Order != null &&
                    this.ProductMaterial.OrderProduct.Order.OrderStatusID != (short)OrderStatusEnum.OrderConfirmed)
                {
                    return false;
                }

                return true;
            }
        }

        private string m_PurchaseOrderNumber = string.Empty;
        public string PurchaseOrderNumber
        {
            get
            {
                if (m_PurchaseOrderNumber == string.Empty && this.ProductMaterial != null && this.ProductMaterial.OrderProduct != null && this.ProductMaterial.OrderProduct.Order != null)
                {
                    return Constants.GetPurchaseOrderNumber(this.SupplierWrapper, this.ProductMaterial.OrderProduct.Order);
                }

                return string.Empty;
            }
        }

        #region Currency Management

        public decimal CurrencyValueInINR
        {
            get
            {
                if (Currency != null && Currency.DefaultValueInINR != null)
                    return Currency.DefaultValueInINR.Value;

                if (CurrencyConversion == null)
                    SelectOrAddCurrencyConversion();

                if (CurrencyConversion != null)
                    return CurrencyConversion.ValueInINRForSubMaterialsWrapper;

                return 0;
            }
            set
            {
                if (Currency != null && Currency.DefaultValueInINR == null)
                {
                    CurrencyConversion.ValueInINRForSubMaterialsWrapper = value;
                    ValidateCurrencyValueInINR();
                }
            }
        }

        private OrderCurrencyConversion m_CurrencyConversion = null;
        public OrderCurrencyConversion CurrencyConversion
        {
            get
            {
                return m_CurrencyConversion;
            }
            set
            {
                if (m_CurrencyConversion != null)
                {
                    m_CurrencyConversion.PropertyChanged -= m_CurrencyConversion_PropertyChanged;
                }

                m_CurrencyConversion = value;
                m_CurrencyConversion.PropertyChanged += m_CurrencyConversion_PropertyChanged;
            }
        }

        private void SelectOrAddCurrencyConversion()
        {
            if (Currency != null)
            {
                var currencyConversion = ProductMaterial.OrderProduct.Order.OrderCurrencyConversions.Where(c => c.Currency.CurrencyID == Currency.CurrencyID)
                                                                       .Select(c => c);

                OrderCurrencyConversion newConversion = null;
                if (currencyConversion == null || currencyConversion.Count() == 0)
                {
                    newConversion = new OrderCurrencyConversion();
                    newConversion.Currency = Currency;

                    ProductMaterial.OrderProduct.Order.OrderCurrencyConversions.Add(newConversion);
                }
                else
                {
                    newConversion = currencyConversion.First();
                }

                CurrencyConversion = newConversion;
            }
        }

        void m_CurrencyConversion_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ValueInINRForSubMaterialsWrapper")
            {
                OnPropertyChanged("CurrencyValueInINR");
                CalculateItemCost();
            }
        }

        #endregion

        public void CalculateItemCost()
        {
            ItemCostWrapper = Cost * Quantity * CurrencyValueInINR;
        }
        #endregion [Helpers]

        #region Data Validation

        public bool Validate()
        {
            ValidateQuantity();
            ValidateCurrency();
            ValidateCost();
            ValidateCurrencyValueInINR();
            ValidateUOM();
            ValidateSupplier();
            return !HasErrors;
        }

        #region Validate For GRN

        public void ValidateDataForGRN()
        {
            ValidateInvoicedQuantity();
            ValidateRecievedQuantity();
            ValidateInvoiceDate();
            ValidateRecievedDate();
            ValidateQualityPassedQuantity();
        }

        public void ValidateQualityPassedQuantity()
        {
            //Update UI
            QualityFailedQuantityWrapper = RecievedInHandWrapper - QualityPassedQuantityWrapper;

            if (QualityPassedQuantity > RecievedInHand)
            {
                AddError("QualityPassedQuantityWrapper", "Passeed quantity can't be greater than received qunatity", false);
            }
            else
            {
                RemoveError("QualityPassedQuantityWrapper", "Passeed quantity can't be greater than received qunatity");
            }
        }

        public void ValidateInvoicedQuantity()
        {
            if (InvoicedQuantityWrapper == 0)
            {
                AddError("InvoicedQuantityWrapper", "Value can't be Zero", false);
            }
            else
            {
                RemoveError("InvoicedQuantityWrapper", "Value can't be Zero");
            }
        }

        public void ValidateRecievedQuantity()
        {
            if (RecievedInHandWrapper == 0)
            {
                AddError("RecievedInHandWrapper", "Value can't be Zero", false);
            }
            else
            {
                RemoveError("RecievedInHandWrapper", "Value can't be Zero");
            }
        }

        public void ValidateInvoiceDate()
        {
            if (InvoicedDateWrapper == null)
            {
                AddError("InvoicedDateWrapper", "Choose a date", false);
            }
            else
            {
                RemoveError("InvoicedDateWrapper", "Choose a date");
            }
        }

        public void ValidateRecievedDate()
        {
            if (RecievedDateWrapper == null)
            {
                AddError("RecievedDateWrapper", "Choose a date", false);
                return;
            }
            else
            {
                RemoveError("RecievedDateWrapper", "Choose a date");
            }

            if (InvoiceDate != null)
            {
                if (RecievedDate < InvoiceDate)
                {
                    AddError("RecievedDateWrapper", "Recieved data can't be before Invoice Date", false);
                }
                else
                {
                    RemoveError("RecievedDateWrapper", "Recieved data can't be before Invoice Date");
                }
            }
        }

        #endregion 

        public void ValidateCurrencyValueInINR()
        {
            RemoveError("CurrencyValueInINR");
            if (CurrencyValueInINR == 0)
            {
                AddError("CurrencyValueInINR", "Value in INR can't be Zero", false);
            }
        }

        public void ValidateCost()
        {

            RemoveError("CostWrapper");
            if (Cost == 0)
            {
                AddError("CostWrapper", "Cost can't be Zero", false);
            }
        }

        private void ValidateCurrency()
        {
            RemoveError("CurrencyWrapper");
            if (CurrencyWrapper == null)
            {
                AddError("CurrencyWrapper", "Select Currency", false);
            }
        }

        private void ValidateUOM()
        {
            RemoveError("UnitsOfMeasurementWrapper");
            if (UnitsOfMeasurementWrapper == null)
            {
                AddError("UnitsOfMeasurementWrapper", "Select Units", false);
            }
        }

        private void ValidateQuantity()
        {
            RemoveError("QuantityWrapper");
            if (QuantityWrapper == 0)
            {
                AddError("QuantityWrapper", "Quantity can't be Zero", false);
            }
        }

        private void ValidateSupplier()
        {
            RemoveError("SupplierWrapper");
            if (Company ==null)
            {
                AddError("SupplierWrapper", "Select supplier", false);
            }
        }

        public override bool HasErrors
        {
            get
            {
                return base.HasErrors;
            }
        }

        #endregion Data Validation
    }
}
