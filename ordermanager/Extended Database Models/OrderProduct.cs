using ordermanager.Extended_Database_Models;
using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class OrderProduct : EntityBase
    {

        #region fields

        private ProductExtraCost m_OtherCost = null;

        #endregion

        #region Property Wrappers

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
                CalculateOrderValue();

            }
        }

        public virtual ProductExtraCost OtherCost
        {
            get
            {
                if(m_OtherCost == null)
                {
                    foreach (var extraCost in ProductExtraCosts)
                    {
                        if (extraCost.ExtraCostID == 1)
                        {
                            m_OtherCost = extraCost;
                            m_OtherCost.PropertyChanged +=m_OtherCost_PropertyChanged;
                            break;
                        }
                    }
                }

                if (m_OtherCost == null)
                {
                    m_OtherCost = new ProductExtraCost();
                    m_OtherCost.PropertyChanged += m_OtherCost_PropertyChanged;
                    m_OtherCost.ExtraCostID = 1;

                    OrderManagerDBEntities context = new OrderManagerDBEntities();
                    Currency indianCurrency = context.Currencies.Find(1);

                    if (indianCurrency != null)
                        m_OtherCost.Currency = indianCurrency;

                    this.ProductExtraCosts.Add(m_OtherCost);
                }

                return m_OtherCost;
            }
        }

        void m_OtherCost_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CostWrapper")
            {
                CalculateTotalMaterialsCost();
            }
        }

        public virtual ProductName ProductNameWrapper
        {
            get
            {
                return ProductName;
            }
            set
            {
                ProductName = value;
                ValidateProduct();
            }
        }

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

        public decimal OrderValueWrapper
        {
            get
            {
                return OrderValue;
            }
            set
            {
                OrderValue = value;
                OnPropertyChanged("OrderValueWrapper");
            }
        }

        public decimal CustomerTargetPriceWrapper
        {
            get
            {
                return CustomerTargetPrice;
            }
            set
            {
                CustomerTargetPrice = value;
                CalculateOrderValue();
                ValidateCustomerTargetPrice();
            }
        }

        public decimal ExpectedQuantityWrapper
        {
            get
            {
                return ExpectedQuantity;
            }
            set
            {
                ExpectedQuantity = value;
                CalculateOrderValue();
                ValidateExpectedQuantity();
            }
        }

        public decimal TotalProductMaterialsCostWrapper
        {
            get
            {
                if (m_TotalProductMaterialsCostWrapper == -1.0m)
                    CalculateTotalMaterialsCost();
                return m_TotalProductMaterialsCostWrapper;
            }
            set
            {
                m_TotalProductMaterialsCostWrapper = value;
                OnPropertyChanged("TotalProductMaterialsCostWrapper");
            }
        }

        private bool m_HasErrorsInProductMaterials;
        public bool HasErrorsInProductMaterials
        {
            get
            {
                return m_HasErrorsInProductMaterials;
            }
            set
            {
                m_HasErrorsInProductMaterials = value;
                OnPropertyChanged("HasErrorsInProductMaterials");
            }
        }

        public bool ValidateProductMaterials()
        {
            bool hasError = false;
            if (HasUserClickedSaveOrSubmit)
            {
                if (ProductMaterialsWrapper.Count == 0)
                {
                    hasError = true;
                }
            }

            foreach (var material in ProductMaterialsWrapper)
            {
                material.ValidateMaterialName();
                material.ValidateCostPerUnit();
                material.ValidateConsumtpion();
                material.ValidateCurrency();
                material.ValidateUOM();

                if (material.HasErrors)
                    hasError = true;
            }

            if(ValidateExtraCosts())
                hasError = true;

            HasErrorsInProductMaterials = hasError;
            return hasError;
        }

        public bool ValidateExtraCosts()
        {
            bool hasError = false;
            foreach (var extraCost in ProductExtraCosts)
            {
                if (extraCost.Validate())
                    hasError = true;
            }

            return hasError;
        }

        #endregion

        #region Helpers

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
                var currencyConversion = Order.OrderCurrencyConversions.Where(c => c.Currency.CurrencyID == Currency.CurrencyID)
                                                                       .Select(c => c);

                OrderCurrencyConversion newConversion = null;
                if (currencyConversion == null || currencyConversion.Count() == 0)
                {
                    newConversion = new OrderCurrencyConversion();
                    newConversion.Currency = Currency;

                    Order.OrderCurrencyConversions.Add(newConversion);
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
            CalculateOrderValue();
        }

        private void CalculateOrderValue()
        {
            if (Currency != null && CurrencyConversion == null)
            {
                foreach (OrderCurrencyConversion currencyConversion in Order.OrderCurrencyConversions)
                {
                    if (currencyConversion.Currency.CurrencyID == Currency.CurrencyID)
                    {
                        CurrencyConversion = currencyConversion;
                    }
                }
            }

            decimal currencyValueInINR = 0;

            if (CurrencyConversion != null)
            {
                currencyValueInINR = CurrencyConversion.ValueInINR;
            }

            OrderValueWrapper = ExpectedQuantity * CustomerTargetPrice * currencyValueInINR;
        }

        #endregion

        Dictionary<string, PurchaseOrder> m_MaterialItems;
        public Dictionary<string, PurchaseOrder> MaterialItemsWrapper
        {
            get { return GroupMaterialByName(); }
        }

        private Dictionary<string, PurchaseOrder> GroupMaterialByName()
        {
            Dictionary<string, PurchaseOrder> items = new Dictionary<string, PurchaseOrder>();
            foreach (MaterialName material in DBResources.Instance.AvailableMaterials)
            {
                ObservableCollection<ProductMaterial> queryItems = new ObservableCollection<ProductMaterial>((from item in ProductMaterials where item.MaterialName.Name == material.Name select item));
                List<ProductMaterialItem> proItems = new List<ProductMaterialItem>();
                ObservableCollection<SubMaterial> sub = new ObservableCollection<SubMaterial>();

                for (int i = 1; i <= 3; i++)
                {
                    sub.Add(new SubMaterial() { Name = material.Name + "-SubItem" + i.ToString() });
                }
                foreach (ProductMaterial proMaterial in queryItems)
                {
                    proItems.AddRange(proMaterial.ProductMaterialItems);
                    proItems.Add(new ProductMaterialItem() { Cost = 100.0m, Quantity = 10.0m });
                }
                items.Add(material.Name, new PurchaseOrder(material, sub, new ObservableCollection<ProductMaterialItem>(proItems)));
            }
            m_MaterialItems = items;
            return m_MaterialItems;
        }

        ObservableCollection<ProductMaterial> m_ProductMaterialsWrapper;
        public ObservableCollection<ProductMaterial> ProductMaterialsWrapper
        {
            get
            {
                if (m_ProductMaterialsWrapper == null)
                {
                    m_ProductMaterialsWrapper = new ObservableCollection<ProductMaterial>(this.ProductMaterials);
                    foreach (var material in m_ProductMaterialsWrapper)
                    {
                        material.PropertyChanged += MaterialItem_PropertyChanged;
                    }
                    m_ProductMaterialsWrapper.CollectionChanged += m_ProductMaterialsWrapper_CollectionChanged;
                }
                return m_ProductMaterialsWrapper;
            }
        }        

        void m_ProductMaterialsWrapper_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    ProductMaterial newMaterialItem = newItem as ProductMaterial;
                    newMaterialItem.OrderProduct = this;
                    newMaterialItem.PropertyChanged += MaterialItem_PropertyChanged;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (var deletedItem in e.OldItems)
                {
                    ProductMaterial deletedMaterial = deletedItem as ProductMaterial;
                    deletedMaterial.OrderProduct = null;
                    deletedMaterial.PropertyChanged -= MaterialItem_PropertyChanged;
                }
            }
            OnPropertyChanged("ProductMaterialsWrapper");
        }

        void MaterialItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ConsumptionCostWrapper")
            {
                CalculateTotalMaterialsCost();
            }

            if (e.PropertyName == "HasErrors")
            {
                ValidateProductMaterials();
            }
        }

        decimal m_TotalProductMaterialsCostWrapper = -1.0m;
        private void CalculateTotalMaterialsCost()
        {
            decimal cost = 0;
            foreach (ProductMaterial material in ProductMaterialsWrapper)
            {
                if (material.OrderProduct != null)
                    cost += material.ConsumptionCostWrapper;
            }

            foreach (ProductExtraCost extraCost in ProductExtraCosts)
            {
                cost += extraCost.Cost;
            }

            TotalProductMaterialsCostWrapper = cost;
        }

        #region Data Validation

        public void Validate()
        {
            ValidateProduct();
            ValidateExpectedQuantity();
            ValidateUOM();
            ValidateCurrency();
            ValidateCustomerTargetPrice();
        }

        private void ValidateCustomerTargetPrice()
        {
            if (CustomerTargetPriceWrapper == 0)
            {
                AddError("CustomerTargetPriceWrapper", "Customer can't expect product at free of cost", false);
            }
            else
            {
                RemoveError("CustomerTargetPriceWrapper", "Customer can't expect product at free of cost");
            }
        }

        private void ValidateExpectedQuantity()
        {
            if (ExpectedQuantityWrapper == 0)
            {
                AddError("ExpectedQuantityWrapper", "Quantity can't be Zero", false);
            }
            else
            {
                RemoveError("ExpectedQuantityWrapper", "Quantity can't be Zero");
            }
        }

        private void ValidateProduct()
        {
            if (ProductNameWrapper == null)
            {
                AddError("ProductNameWrapper", "Select Product", false);
            }
            else
            {
                RemoveError("ProductNameWrapper", "Select Product");
            }
        }

        private void ValidateUOM()
        {
            if (UnitsOfMeasurementWrapper == null)
            {
                AddError("UnitsOfMeasurementWrapper", "Select Units", false);
            }
            else
            {
                RemoveError("UnitsOfMeasurementWrapper", "Select Units");
            }
        }


        private void ValidateCurrency()
        {
            if (CurrencyWrapper == null)
            {
                AddError("CurrencyWrapper", "Select Currency", false);
            }
            else
            {
                RemoveError("CurrencyWrapper", "Select Currency");
            }
        }

        #endregion
    }
}
