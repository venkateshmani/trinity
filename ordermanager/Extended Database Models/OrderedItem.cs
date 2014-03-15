using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class OrderedItem : EntityBase, ICloneable, IPurchaseOrderItem
    {

        public string MaterialName
        {
            get
            {
                return this.ProductMaterialItem.SubMaterial.Name;
            }
        }


        public string UnitsOfMeasurement
        {
            get
            {
                return this.ProductMaterialItem.UnitsOfMeasurement.Units;
            }
        }


        public decimal TotalInvoicedQuantity
        {
            get
            {
                decimal quantity = 0;
                foreach (GRNReciept receipt in this.GRNReciepts)
                {
                    quantity += receipt.RecievedInHandWrapper;
                }

                return quantity;
            }
        }

        public decimal TotalRecievedInHandQuantity
        {
            get
            {
                decimal quantity = 0;
                foreach (GRNReciept receipt in this.GRNReciepts)
                {
                    quantity += receipt.RecievedInHandWrapper;
                }
                return quantity;
            }
        }

        public decimal ExcessQuantityRecieved
        {
            get
            {
                decimal quantity = TotalRecievedInHandQuantity - OrderedQuantity;

                if (quantity > 0)
                {
                    return quantity;
                }

                return 0;
            }
        }

        public decimal PendingQuantityToRecieve
        {
            get
            {
                decimal quantity = OrderedQuantity - TotalRecievedInHandQuantity;

                if (quantity > 0)
                {
                    return quantity;
                }

                return 0;
            }
        }

        public object Clone()
        {
            OrderedItem clonedItem = new OrderedItem();
            clonedItem.ProductMaterialItem = this.ProductMaterialItem;

            return clonedItem;
        }

        private List<JobOrder> m_AllJobOrders = null;

        private void LoadAllJobOrders()
        {
            m_AllJobOrders = new List<JobOrder>();
            if (GRNReciepts.Count > 0)
            {
                foreach (GRNReciept recpt in GRNReciepts)
                {
                    if (recpt.JobOrders.Count > 0)
                    {
                        m_AllJobOrders.AddRange(recpt.JobOrders);
                    }
                }
            }
        }


        private ObservableCollection<JobOrder> m_KnittingItemsWrapper = null;
        private ObservableCollection<JobOrder> m_DyeingItemsWrapper = null;
        private ObservableCollection<JobOrder> m_PrintingItemsWrapper = null;
        private ObservableCollection<JobOrder> m_CompactingItemsWrapper = null;
        private ObservableCollection<JobOrder> m_WashingItemsWrapper = null;
        private ObservableCollection<JobOrder> m_OtherItemsWrapper = null;

        public ObservableCollection<JobOrder> KnittingItemsWrapper
        {
            get
            {
                if (m_KnittingItemsWrapper == null)
                {
                    m_KnittingItemsWrapper = GetJobOrdersByType("Knitting");
                }
                return m_KnittingItemsWrapper;
            }
            private set
            {
                m_KnittingItemsWrapper = value;
            }
        }

        public ObservableCollection<JobOrder> DyeingItemsWrapper
        {
            get
            {
                if (m_DyeingItemsWrapper == null)
                {
                    m_DyeingItemsWrapper = GetJobOrdersByType("Dyeing");
                }
                return m_DyeingItemsWrapper;
            }
            private set
            {
                m_DyeingItemsWrapper = value;
            }
        }

        public ObservableCollection<JobOrder> PrintingItemsWrapper
        {
            get
            {
                if (m_PrintingItemsWrapper == null)
                {
                    m_PrintingItemsWrapper = GetJobOrdersByType("Printing");
                }
                return m_PrintingItemsWrapper;
            }
            private set
            {
                m_PrintingItemsWrapper = value;
            }
        }

        public ObservableCollection<JobOrder> CompactingItemsWrapper
        {
            get
            {
                if (m_CompactingItemsWrapper == null)
                {
                    m_CompactingItemsWrapper = GetJobOrdersByType("Compacting");
                }
                return m_CompactingItemsWrapper;
            }
            private set
            {
                m_CompactingItemsWrapper = value;
            }
        }

        public ObservableCollection<JobOrder> WashingItemsWrapper
        {
            get
            {
                if (m_WashingItemsWrapper == null)
                {
                    m_WashingItemsWrapper = GetJobOrdersByType("Washing");
                }
                return m_WashingItemsWrapper;
            }
            private set
            {
                m_WashingItemsWrapper = value;
            }
        }

        public ObservableCollection<JobOrder> OtherItemsWrapper
        {
            get
            {
                if (m_OtherItemsWrapper == null)
                {
                    m_OtherItemsWrapper = GetJobOrdersByType("Other");
                }
                return m_OtherItemsWrapper;
            }
            private set
            {
                m_OtherItemsWrapper = value;
            }
        }

        private ObservableCollection<JobOrder> GetJobOrdersByType(string type)
        {
            if (m_AllJobOrders == null)
                LoadAllJobOrders();           
            return new ObservableCollection<JobOrder>(m_AllJobOrders.Where(jo => jo.JobOrderType.Type == type).Select(jo => jo).ToList());
        }

        public string Alias
        {
            get
            {
                return this.ProductMaterialItem.Alias;
            }
            set
            {
                this.ProductMaterialItem.Alias = value;
            }
        }

        public decimal CostWrapper
        {
            get
            {
                if (this.CostPerUnit == null)
                     CostPerUnit = 0;

                return CostPerUnit.Value;
            }
            set
            {
                this.CostPerUnit = value;
                CalcluateCost();
            }
        }

        public Currency Currency
        {
            get
            {
                return this.ProductMaterialItem.Currency;
            }
            set
            {
                this.ProductMaterialItem.Currency = value; 
            }
        }

        public decimal ItemCostWrapper
        {
            get
            {
                if (this.TotalCost == null)
                    this.TotalCost = 0;

                return this.TotalCost.Value;
            }
            set
            {
                this.TotalCost = value;
           }
        }

        public decimal ItemCostInItemCurrency
        {
            get
            {
                if (this.TotalCost == null)
                    this.TotalCost = 0;

                return this.TotalCost.Value;
            }
            set
            {
                this.TotalCost = value;
            }
        }

        public decimal QuantityWrapper
        {
            get
            {
                return this.OrderedQuantity;
            }
            set
            {
                this.OrderedQuantity = value;
                CalcluateCost();
            }
        }

        public SubMaterial SubMaterial
        {
            get
            {
                return this.ProductMaterialItem.SubMaterial;
            }
            set
            {
                this.ProductMaterialItem.SubMaterial = value;
            }
        }

        public decimal TaxPerUnitWrapper
        {
            get
            {
                if (TaxInINRPerUnit == null)
                    TaxInINRPerUnit = 0;
                return TaxInINRPerUnit.Value;
            }
            set
            {
                TaxInINRPerUnit = value;
                CalcluateCost();
            }
        }

        public UnitsOfMeasurement UnitsOfMeasurementWrapper
        {
            get
            {
                return this.ProductMaterialItem.UnitsOfMeasurement;
            }
            set
            {
                this.ProductMaterialItem.UnitsOfMeasurement = value;
            }
        }

        private void CalcluateCost()
        {
            ItemCostWrapper = (CostWrapper + TaxPerUnitWrapper) * QuantityWrapper * UnitsOfMeasurementWrapper.Multiplier;
        }
    }
}
