using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class Shipment : EntityBase
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


        public decimal ShippedWrapper
        {
            get
            {
                return Shipped;
            }
            set
            {
                Shipped = value;
                OnPropertyChanged("ShippedWrapper");
            }
        }

        public decimal RejectedWrapper
        {
            get
            {
                return Rejected;
            }
            set
            {
                Rejected = value;
                OnPropertyChanged("RejectedWrapper");
            }
        }
    }
}
