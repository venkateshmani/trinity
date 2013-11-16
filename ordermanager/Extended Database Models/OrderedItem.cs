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

        public OrderedItemStatu OrderedItemStatuWrapper
        {
            get
            {
                return this.OrderedItemStatu;
            }
            set
            {
                this.OrderedItemStatu = value;
                if (value.OrderedItemStatusID == 1)
                    OrderedItemStatusText = string.Empty;
                else if (value.OrderedItemStatusID == 2)
                    OrderedItemStatusText = "Completed";
                else if (value.OrderedItemStatusID == 3)
                    OrderedItemStatusText = "PO. No. (" + SpawnedPurchaseOrder.PurchaseOrderNumber + ")";

                OnPropertyChanged("OrderedItemStatuWrapper");
                OnPropertyChanged("IsReadOnly");
            }
        }

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
                return PurchaseOrder1;
            }
            set
            {
                PurchaseOrder1 = value;
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


        private void CalculateExcessiveQuantity()
        {
            ExcessiveQuantityWrapper = RecievedInHandWrapper - InvoicedQuantityWrapper;
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

    
        public object Clone()
        {
            OrderedItem item = new OrderedItem();
            item.ProductMaterialItem = this.ProductMaterialItem;
            item.OrderedQuantity = this.QualityFailedQuantityWrapper;

            return item;
        }
    }
}
