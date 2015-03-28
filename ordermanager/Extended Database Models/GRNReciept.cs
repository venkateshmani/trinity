using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ordermanager.DatabaseModel
{
    public partial class GRNReciept : EntityBase
    {

        public decimal QualityPassedQuantityWrapper
        {
            get
            {
                if(QualityPassedQuantity != null)
                    return QualityPassedQuantity.Value;

                return 0;
            }
            set
            {
                this.QualityPassedQuantity = value;
                QualityFailedQuantityWrapper = this.RecievedInHand.Value - this.QualityPassedQuantity.Value;
                OnPropertyChanged("QualityPassedQuantityWrapper");
                RefreshUIEnablers();
            }
        }

        public decimal QualityFailedQuantityWrapper
        {
            get
            {
                if (QualityFailedQuantity != null)
                    return QualityFailedQuantity.Value;

                return 0;
            }
            set
            {
                this.QualityFailedQuantity = value;
                OnPropertyChanged("QualityFailedQuantityWrapper");
            }
        }

        public string ReceiptNumber
        {
            get
            {
                if (this.GRNReciptID != 0)
                {
                    return "Receipt-" + this.GRNReciptID;
                }

                return string.Empty;
            }
        }

        public Visibility IssueButtonVisibility
        {
            get
            {
                if (this.JobOrders.Count > 0 || 
                    this.DyeingJOes.Count > 0 ||
                    this.KnittingJOes.Count > 0 ||
                    this.CompactingJoes.Count > 0  ||
                    this.QualityPassedQuantity == 0 || this.ReceiptStatu != null)
                {
                    return System.Windows.Visibility.Collapsed;
                }

                return System.Windows.Visibility.Visible;
            }
        }
            

        public bool CanEditQuantityPassed
        {
            get
            {
                if (this.JobOrders.Count > 0 ||
                    (this.QualityFailedQuantityWrapper > 0 && this.SpawnedNewPurchaseOrder != null && this.SpawnedNewPurchaseOrder.Value))
                    return false;
 
                return true;
            }
        }

        public bool CanGeneratePurchaseOrder
        {
            get
            {
                if (this.QualityFailedQuantityWrapper > 0 &&
                    this.SpawnedNewPurchaseOrder == null)
                    return true;

                return false;
            }
        }

        public string PassedQuantityStatus
        {
            get
            {
                if (this.JobOrders.Count > 0 ||
                    this.DyeingJOes.Count > 0 ||
                    this.KnittingJOes.Count > 0 ||
                    this.CompactingJoes.Count > 0 ||
                    this.QualityPassedQuantity == 0 || this.ReceiptStatu != null)
                {
                    if (this.ReceiptStatusID == 10)
                        return "Sent to store";

                    return "Issued";
                }

                return string.Empty;
            }
        }

        public string FailedQuantityStatus
        {
            get
            {
                if (this.PurchaseOrder != null)
                {
                    return "New PO: " + this.PurchaseOrder.PurchaseOrderNumber;
                }
                
                return string.Empty;
            }
        }

        public void RefreshUIEnablers()
        {
            OnPropertyChanged("CanEditQuantityPassed");
            OnPropertyChanged("CanGeneratePurchaseOrder");
            OnPropertyChanged("IssueButtonVisibility");
            OnPropertyChanged("FailedQuantityStatus");
            OnPropertyChanged("PassedQuantityStatus");
        }

        private decimal m_AlreadyRecievedQuantity = 0;
        public decimal AlreadyRecievedQuantity
        {
            get
            {
                return m_AlreadyRecievedQuantity;
            }
            set
            {
                m_AlreadyRecievedQuantity = value;
                OnPropertyChanged("AlreadyRecievedQuantity");
            }
        }


        public string GRNNumber
        {
            get { return "G" + this.OrderedItem.PurchaseOrder.OrderID + "-" + this.GRNReciptID; }
            set {}
        }


        public decimal InvoicedQuantityWrapper
        {
            get
            {
                if (this.InvoicedQuantity != null)
                    return this.InvoicedQuantity.Value;

                return 0;
            }
            set
            {
                this.InvoicedQuantity = value;
                ValidateInvoicedQuantity();
            }
        }

        public decimal RecievedInHandWrapper
        {
            get
            {
                if (this.RecievedInHand != null)
                    return this.RecievedInHand.Value;

                return 0;
            }
            set
            {
                this.RecievedInHand = value;
                PendingQuantity = OrderedItem.OrderedQuantity -(AlreadyRecievedQuantity + value);
                ValidateRecievedInHandQuantity();
            }
        }

        public decimal PendingQuantityWrapper
        {
            get
            {
                if (PendingQuantity == null)
                    PendingQuantity = 0;
                return PendingQuantity.Value;
            }
            set
            {
                PendingQuantity = value;
            }
        }

        #region UI Enablers

        public Visibility QualityPassedStatusVisibility
        {
            get
            {
                if (this.JobOrders.Count != 0)
                    return Visibility.Visible;

                return Visibility.Collapsed;
            }
        }

        public Visibility QualityFailedStatusVisiblity
        {
            get
            {
                if (this.QualityFailedQuantityWrapper != 0 && SpawnedNewPurchaseOrder != null && SpawnedNewPurchaseOrder.Value == true)
                {
                    return Visibility.Visible;
                }

                return Visibility.Collapsed;
            }
        }

         
        #endregion 


        #region Validation

        public void ValidateForNewReciept()
        {
            ValidateInvoicedQuantity();
            ValidateRecievedInHandQuantity();
        }

        public void ValidateInvoicedQuantity()
        {
            if (this.InvoicedQuantity == null || this.InvoicedQuantity.Value == 0)
            {
                AddError("InvoicedQuantityWrapper", "Enter the quantity mentioned in the Invoice", false);
            }
            else
            {
                RemoveError("InvoicedQuantityWrapper", "Enter the quantity mentioned in the Invoice");

                decimal totalInvoicedQuantity = AlreadyRecievedQuantity + InvoicedQuantityWrapper;
                decimal maxAllowedQuantity = totalInvoicedQuantity + 0.05M * totalInvoicedQuantity;

                if ( (OrderedItem.OrderedQuantity + 0.05m * OrderedItem.OrderedQuantity ) < totalInvoicedQuantity)
                {
                    AddError("InvoicedQuantityWrapper", "Exceeds 5% allowance", false);
                }
                else
                {
                    RemoveError("InvoicedQuantityWrapper", "Exceeds 5% allowance");
                }
            }
        }

        public void ValidateRecievedInHandQuantity()
        {
            if (this.RecievedInHand == null || this.RecievedInHand.Value == 0)
            {
                AddError("RecievedInHandWrapper", "Enter the quantity recieved in hand", false);
            }
            else
            {
                RemoveError("RecievedInHandWrapper", "Enter the quantity recieved in hand");
            }
        }

        #endregion 
    }
}
