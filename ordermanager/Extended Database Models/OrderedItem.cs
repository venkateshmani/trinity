using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ordermanager.DatabaseModel
{
    public partial class OrderedItem : EntityBase , ICloneable
    {

        #region  Data From Navigation Properties

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

        #endregion 

        #region UI only property

        const string purchaseOrderGeneratedStatus = "PO Generated";
        private string m_OrderedItemStatusText = string.Empty;

        public string OrderedItemStatusText
        {
            get
            {
                if (m_OrderedItemStatusText == string.Empty && SpawnedNewPurchaseOrderWrapper)
                {
                    m_OrderedItemStatusText = purchaseOrderGeneratedStatus;
                }

                return m_OrderedItemStatusText;
            }
            set
            {
                m_OrderedItemStatusText = value;
                OnPropertyChanged("OrderedItemStatusText");
            }
        }

        private Visibility? m_StatusTextVisibility = null;
        public Visibility StatusTextVisibility
        {
            get
            {
                if (SpawnedNewPurchaseOrderWrapper)
                    m_StatusTextVisibility = Visibility.Visible;
                else
                    m_StatusTextVisibility = Visibility.Collapsed;

                return m_StatusTextVisibility.Value;
            }
            set
            {
                m_StatusTextVisibility = value;
                OnPropertyChanged("StatusTextVisibility");
            }
        }

        private Visibility? m_SupplierVisibility = null;
        public Visibility SupplierVisibility
        {
            get
            {
                if (SpawnedNewPurchaseOrderWrapper)
                    m_SupplierVisibility = Visibility.Collapsed;
                else
                    m_SupplierVisibility = Visibility.Visible;

                return m_SupplierVisibility.Value;
            }
            set
            {
                m_SupplierVisibility = value;

                if (value == Visibility.Visible)
                    StatusTextVisibility = Visibility.Collapsed;
                else
                    StatusTextVisibility = Visibility.Visible;
                    
                OnPropertyChanged("SupplierVisibility");
            }
        }

        //public OrderedItemStatu OrderedItemStatuWrapper
        //{
        //    get
        //    {
        //        return this.OrderedItemStatu;
        //    }
        //    set
        //    {
        //        this.OrderedItemStatu = value;
        //        if (value.OrderedItemStatusID == 1)
        //            OrderedItemStatusText = string.Empty;
        //        else if (value.OrderedItemStatusID == 2)
        //            OrderedItemStatusText = "Completed";
        //        else if (value.OrderedItemStatusID == 3)
        //            OrderedItemStatusText = "PO. No. (" + SpawnedPurchaseOrder.PurchaseOrderNumber + ")";

        //        OnPropertyChanged("OrderedItemStatuWrapper");
        //        OnPropertyChanged("IsReadOnly");
        //    }
        //}

        public bool IsReadOnly
        {
            get
            {
                if (OrderedItemStatu.OrderedItemStatusID == 1)
                    return false;

                return true;
            }
        }

        public PurchaseOrder SpawnedPurchaseOrder
        {
            get
            {
                return PurchaseOrder;
            }
            set
            {
                PurchaseOrder = value;
            }
        }

        public bool SpawnedNewPurchaseOrderWrapper
        {
            get
            {
                if (SpawnedNewPurchaseOrder != null && SpawnedNewPurchaseOrder.Value == true)
                    return true;

                return false;
            }
            set
            {
                SpawnedNewPurchaseOrder = value;
                if (value)
                {
                    OrderedItemStatusText = purchaseOrderGeneratedStatus;
                    SupplierVisibility = Visibility.Collapsed;
                }

                OnPropertyChanged("SpawnedNewPurchaseOrderWrapper");
            }
        }

        private Company m_Supplier = null;
        public Company Supplier
        {
            get
            {
                if (m_Supplier == null && this.SpawnedPurchaseOrder != null && this.SpawnedPurchaseOrder.Company != null)
                    return this.SpawnedPurchaseOrder.Company;

                return m_Supplier;
            }
            set
            {
                m_Supplier = value;
            }
        }

        #endregion 

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




     

    
        public object Clone()
        {
            OrderedItem item = new OrderedItem();
            item.ProductMaterialItem = this.ProductMaterialItem;
            item.OrderedQuantity = this.QualityFailedQuantityWrapper;

            return item;
        }
    }
}
