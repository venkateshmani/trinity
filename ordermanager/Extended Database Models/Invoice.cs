using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class Invoice : EntityBase
    {

        public string InvoiceNumberWrapper
        {
            get
            {
                return InvoiceNumber;
            }
            set
            {
                InvoiceNumber = value;
                OnPropertyChanged("InvoiceNumberWrapper");
            }
        }

        public System.DateTime InvoiceDateWrapper
        {
            get
            {
                return InvoiceDate;
            }
            set
            {
                InvoiceDate = value;
                OnPropertyChanged("InvoiceDateWrapper");
            }
        }

        public string ExporterRefNumberWrapper
        {
            get
            {
                return ExporterRefNumber;
            }
            set
            {
                ExporterRefNumber = value;
                OnPropertyChanged("ExporterRefNumberWrapper");
            }
        }

        public string BuyerRefNumberWrapper
        {
            get
            {
                return BuyerRefNumber;
            }
            set
            {
                BuyerRefNumber = value;
                OnPropertyChanged("BuyerRefNumberWrapper");
            }
        }

        public string PlaceOfReceiptByPrecarrierWrapper
        {
            get
            {
                return PlaceOfReceiptByPrecarrier;
            }
            set
            {
                PlaceOfReceiptByPrecarrier = value;
                OnPropertyChanged("PlaceOfReceiptByPrecarrierWrapper");
            }
        }

        public string LoadingPlaceWrapper
        {
            get
            {
                return LoadingPlace;
            }
            set
            {
                LoadingPlace = value;
                OnPropertyChanged("LoadingPlaceWrapper");
            }
        }

        public string DischargePlaceWrapper
        {
            get
            {
                return DischargePlace;
            }
            set
            {
                DischargePlace = value;
                OnPropertyChanged("DischargePlace");
            }
        }

        public bool IsProformaWrapper
        {
            get
            {
                return IsProforma;
            }
            set
            {
                IsProforma = value;
                OnPropertyChanged("IsProformaWrapper");
            }
        }

        public Company Consignee
        {
            get
            {
                return Company;
            }
            set
            {
                Company = value;
                OnPropertyChanged("Consignee");
            }
        }

        public Country Origin
        {
            get
            {
                return Country;
            }
            set
            {
                Country = value;
                OnPropertyChanged("Origin");
            }
        }

        public Country Destination
        {
            get
            {
                return Country1;
            }
            set
            {
                Country1 = value;
                OnPropertyChanged("Destination");
            }
        }

        public ShipmentMode ShipmentModeWrapper
        {
            get
            {
                return ShipmentMode;
            }
            set
            {
                ShipmentMode = value;
                OnPropertyChanged("ShipmentModeWrapper");
            }
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(InvoiceNumberWrapper))
                AddError("InvoiceNumberWrapper", "Enter Invoice Number", false);
            else
                RemoveError("InvoiceNumberWrapper");
            if (string.IsNullOrWhiteSpace(ExporterRefNumberWrapper))
                AddError("ExporterRefNumberWrapper", "Enter Exporter Reference Number", false);
            else
                RemoveError("ExporterRefNumberWrapper");
            if (string.IsNullOrWhiteSpace(BuyerRefNumberWrapper))
                AddError("BuyerRefNumberWrapper", "Enter Buyer Reference Number", false);
            else
                RemoveError("BuyerRefNumberWrapper");
            if (string.IsNullOrWhiteSpace(PlaceOfReceiptByPrecarrierWrapper))
                AddError("PlaceOfReceiptByPrecarrierWrapper", "Enter port of receipt by precarrier", false);
            else
                RemoveError("PlaceOfReceiptByPrecarrierWrapper");
            if (string.IsNullOrWhiteSpace(LoadingPlaceWrapper))
                AddError("LoadingPlaceWrapper", "Enter port of loading", false);
            else
                RemoveError("LoadingPlaceWrapper");
            if (string.IsNullOrWhiteSpace(DischargePlaceWrapper))
                AddError("DischargePlaceWrapper", "Enter port of discharge", false);
            else
                RemoveError("DischargePlaceWrapper");

            if (Consignee == null)
                AddError("Consignee", "Select consignee", false);
            else
                RemoveError("Consignee");
            if (Origin == null)
                AddError("Origin", "Select origin", false);
            else
                RemoveError("Origin");
            if (Destination == null)
                AddError("Destination", "Select destination", false);
            else
                RemoveError("Destination");

            if (ShipmentModeWrapper == null)
                AddError("ShipmentModeWrapper", "Select carriage by", false);
            else
                RemoveError("ShipmentModeWrapper");
            if (InvoiceDateWrapper == null)
                AddError("InvoiceDateWrapper", "Select invoice date", false);
            else
                RemoveError("InvoiceDateWrapper");
            
            return !HasErrors;
        }
    }
}
