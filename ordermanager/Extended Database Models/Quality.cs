using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class Quality : EntityBase
    {
        public ProductSize SizeWrapper
        {
            get
            {
                return ProductBreakUpSummary.ProductSize;
            }
        }

        public Color ColorWrapper
        {
            get
            {
                return ProductBreakUpSummary.Color;
            }
        }

        public decimal NumberOfPiecesWrapper
        {
            get
            {
                return ProductBreakUpSummary.NumberOfPieces;
            }
        }

        public decimal PassedWrapper
        {
            get
            {
                return Passed;
            }
            set
            {
                Passed = value;
                FailedWrapper = NumberOfPiecesWrapper - value;
                OnPropertyChanged("PassedWrapper");
            }
        }

        public decimal FailedWrapper
        {
            get
            {
                return Failed;
            }
            set
            {
                Failed = value;
                OnPropertyChanged("FailedWrapper");
            }
        }

    }
}
