using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class GRNReciept : EntityBase
    {
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
    }
}
