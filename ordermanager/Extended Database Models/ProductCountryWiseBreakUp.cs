using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class ProductCountryWiseBreakUp : EntityBase
    {

        public decimal NumberOfPiecesWrapper
        {
            get
            {
                return NumberOfPieces;
            }
            set
            {
                NumberOfPieces = value;
                Validate();
                OnPropertyChanged("NumberOfPiecesWrapper");
            }
        }

        public decimal LSWrapper
        {
            get
            {
                return LS;
            }
            set
            {
                LS = value;
                Validate();
                OnPropertyChanged("LSWrapper");
            }
        }
        public decimal SWrapper
        {
            get
            {
                return S;
            }
            set
            {
                S = value;
                Validate();
                OnPropertyChanged("SWrapper");
            }
        }
        public decimal MWrapper
        {
            get
            {
                return M;
            }
            set
            {
                M = value;
                Validate();
                OnPropertyChanged("MWrapper");
            }
        }
        public decimal LWrapper
        {
            get
            {
                return L;
            }
            set
            {
                L = value;
                Validate();
                OnPropertyChanged("LWrapper");
            }
        }
        public decimal XLWrapper
        {
            get
            {
                return XL;
            }
            set
            {
                XL = value;
                Validate();
                OnPropertyChanged("XLWrapper");
            }
        }
        public decimal XXLWrapper
        {
            get
            {
                return XXL;
            }
            set
            {
                XXL = value;
                Validate();
                OnPropertyChanged("XXLWrapper");
            }
        }
        public decimal XXXLWrapper
        {
            get
            {
                return XXXL;
            }
            set
            {
                XXXL = value;
                Validate();
                OnPropertyChanged("XXXLWrapper");
            }
        }

        public bool Validate()
        {
            decimal totalQuantity = LS + S + M + L + XL + XXL + XXXL;
            RemoveError("NumberOfPiecesWrapper");
            if (totalQuantity == NumberOfPieces)
                return true;
            else
            {
                AddError("NumberOfPiecesWrapper", string.Format("Total count ({0}) not matching {1}the Number Of Pieces ({2}).", totalQuantity, Environment.NewLine, NumberOfPieces), false);
                return false;
            }
        }
    }
}
