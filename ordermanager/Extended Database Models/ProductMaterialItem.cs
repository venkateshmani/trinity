using ordermanager.DatabaseModel;
using ordermanager.Interfaces_And_Enums;
using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class ProductMaterialItem : EntityBase, IUIRefresher, IPurchaseOrderItem
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
                CalculateItemCost();
                ValidateUOM();
            }
        }

        public virtual decimal TaxPerUnitWrapper
        {
            get
            {
                return TaxInINR;
            }
            set
            {
                TaxInINR = value;
                CalculateItemCost();
                OnPropertyChanged("TaxPerUnitWrapper");
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
                OnPropertyChanged("CurrencyWrapper");
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

        decimal m_ItemCostWrapper = -1;
        public virtual decimal ItemCostWrapper
        {
            get 
            {
                if (m_ItemCostWrapper == -1)
                    CalculateItemCost();
                return m_ItemCostWrapper; 
            }
            set
            {
                if (m_ItemCostWrapper != value)
                {
                    m_ItemCostWrapper = value;
                    OnPropertyChanged("ItemCostWrapper");
                }
            }
        }

        decimal m_ItemCostInItemCurrency = -1;
        public decimal ItemCostInItemCurrency
        {
            get
            {
                if (m_ItemCostInItemCurrency == -1)
                    CalculateItemCost();
                return m_ItemCostInItemCurrency;
            }
            set
            {
                if (m_ItemCostInItemCurrency != value)
                {
                    m_ItemCostInItemCurrency = value;
                    OnPropertyChanged("ItemCostInItemCurrency");
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

        public string POStatus
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                if (this.PurchaseOrder == null)
                    sb.AppendLine("Not Generated");
                else if (this.PurchaseOrder.Approval.IsApproved == null)
                {
                    sb.AppendLine("Waiting for Approval");
                    sb.Append(this.PurchaseOrder.Supplier.Name);
                }
                else if (this.PurchaseOrder.Approval.IsApproved == true)
                {
                    sb.AppendLine("Generated");
                    sb.Append(this.PurchaseOrder.Supplier.Name);
                }
                else if (this.PurchaseOrder.Approval.IsApproved == false)
                {
                    sb.AppendLine("Rejected");
                    sb.Append(this.PurchaseOrder.Supplier.Name);
                }

                return sb.ToString();
            }
        }


        #endregion [Wrappers]
        
        #region [Helpers]

        private bool m_IsSelectedToGeneratePO = false;
        public bool IsSelectedToGeneratePO
        {
            get
            {
                return m_IsSelectedToGeneratePO;
            }
            set
            {
                m_IsSelectedToGeneratePO = value;
                OnPropertyChanged("IsSelectedToGeneratePO");
            }
        }

        public bool CanAddMaterials
        {
            get
            {
                if (!DBResources.Instance.CurrentUser.UserRole.CanAddSubMaterials)
                    return false;

                return IsEditable;
            }
        }

        public bool CanAddCosts
        {
            get
            {
                if (!DBResources.Instance.CurrentUser.UserRole.CanAddSubMaterialsCost)
                    return false;

                return IsEditable;
            }
        }

        /// <summary>
        /// Used to determine the edit when the purchase order is rejected
        /// </summary>
        public bool CanEditCostAndTax
        {
            get
            {
                if (this.PurchaseOrder == null)
                    return false;

                if (this.PurchaseOrder.Approval == null)
                    return false;

                if (this.PurchaseOrder.Approval.IsApproved == null)
                    return false;

                if (this.PurchaseOrder.Approval.IsApproved.Value == false)
                    return true;

                return false;
            }
        }

        public bool IsEditable
        {
            get
            {
                if (this.PurchaseOrder != null)
                    return false;

                if (this.ProductMaterial.Approval != null && (this.ProductMaterial.Approval.IsApproved == null || 
                                                              this.ProductMaterial.Approval.IsApproved.Value == true))
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

        public void RefreshUIProperties()
        {
            OnPropertyChanged("IsEditable");
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
                    newConversion.CurrencyWrapper = Currency;

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
            decimal uomMultipler = 1;
            if (UnitsOfMeasurement != null)
                uomMultipler = UnitsOfMeasurement.Multiplier;

            ItemCostWrapper = (Cost + TaxPerUnitWrapper) * Quantity * CurrencyValueInINR * uomMultipler;
            ItemCostInItemCurrency = (Cost + TaxPerUnitWrapper) * Quantity * uomMultipler;

            OnPropertyChanged("ActualInINR");
        }
        #endregion [Helpers]

        #region Data Validation

        public bool Validate()
        {
            if (DBResources.Instance.CurrentUser.UserRole.CanAddSubMaterials)
            {
                ValidateQuantity();
                ValidateUOM();
            }

            //if (DBResources.Instance.CurrentUser.UserRole.CanAddSubMaterialsCost)
            //{
            //    ValidateCurrency();
            //    ValidateCost();
            //    ValidateCurrencyValueInINR();
            //}
            
            return !HasErrors;
        }

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


        public decimal ActualInINR
        {
            get
            {
                 return this.ProductMaterial.TotalSubMaterialsPurchaseCostWrapper; 
            }
        }

        public decimal BudgetInINR
        {
            get
            {
                return this.ProductMaterial.ConsumptionCostWrapper * this.ProductMaterial.OrderProduct.ExpectedQuantity * this.ProductMaterial.OrderProduct.UnitsOfMeasurement.Multiplier;
            }
        }
    }
}
