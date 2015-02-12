using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class CompactingJoItem : EntityBase
    {
        #region Wrapper

        public string ColorWrapper
        {
            get
            {
                return Color;
            }
            set
            {
                Color = value;
                ValidateColor();
            }
        }

        public string QualityDescriptionWrapper
        {
            get
            {
                return QualityDescription;
            }
            set
            {
                QualityDescription = value;
                ValidateQualityDescription();
            }
        }

        public string ReqGSMWrapper
        {
            get
            {
                return ReqGSM;
            }
            set
            {
                ReqGSM = value;
                ValidateReqGSM();
            }
        }

        public string ReqWidthWrapper
        {
            get
            {
                return ReqWidth;
            }
            set
            {
                ReqWidth = value;
                ValidateReqWidth();
            }
        }

        public decimal NetQtyWrapper
        {
            get
            {
                return NetQty;
            }
            set
            {
                NetQty = value;
                ValidateNetQuantity();
                CalculateTotalAmount();
            }
        }

        public decimal RatePerKgWrapper
        {
            get
            {
                return RatePerKg;
            }
            set
            {
                RatePerKg = value;
                ValidateRatePerKg();
                CalculateTotalAmount();
            }
        }

        public decimal TotalAmountWrapper
        {
            get
            {
                return TotalAmount;
            }
            set
            {
                TotalAmount = value;
                OnPropertyChanged("TotalAmountWrapper");
            }
        }

        #endregion 

        private void CalculateTotalAmount()
        {
            TotalAmountWrapper = NetQty * RatePerKg;
        }

        #region Validation

        public bool Validate()
        {
            ValidateColor();
            ValidateNetQuantity();
            ValidateQualityDescription();
            ValidateRatePerKg();
            ValidateReqGSM();
            ValidateReqWidth();

            return this.HasErrors;
        }


        private void ValidateColor()
        {
            if (string.IsNullOrEmpty(Color))
            {
                AddError("ColorWrapper", "Required", false);
            }
            else
            {
                RemoveError("ColorWrapper", "Required");
            }
        }

        private void ValidateQualityDescription()
        {
            if (string.IsNullOrEmpty(QualityDescription))
            {
                AddError("QualityDescriptionWrapper", "Required", false);
            }
            else
            {
                RemoveError("QualityDescriptionWrapper", "Required");
            }
        }

        public void ValidateReqGSM()
        {
            if (string.IsNullOrEmpty(ReqGSM))
            {
                AddError("ReqGSMWrapper", "Required", false);
            }
            else
            {
                RemoveError("ReqGSMWrapper", "Required");
            }
        }

        public void ValidateReqWidth()
        {
            if (string.IsNullOrEmpty(ReqWidth))
            {
                AddError("ReqWidthWrapper", "Required", false);
            }
            else
            {
                RemoveError("ReqWidthWrapper", "Required");
            }
        }

        public void ValidateNetQuantity()
        {
            if (NetQty == 0)
            {
                AddError("NetQtyWrapper", "Required", false);
            }
            else
            {
                RemoveError("NetQtyWrapper", "Required");
            }
        }

        public void ValidateRatePerKg()
        {
            if (RatePerKg == 0)
            {
                AddError("RatePerKgWrapper", "Required", false);
            }
            else
            {
                RemoveError("RatePerKgWrapper", "Required");
            }
        }

        #endregion

        public override string ToString()
        {
            return "-" + this.CompactingJo.JoNoWrapper + "  Color:" + Color + "  Quality:" + QualityDescription + "  Req-GSM:" + ReqGSM + "  ReqWidth:" + ReqWidth;
        }
    }
}
