using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports
{
    public class InvoiceReportParameters
    {
        public InvoiceReportParameters()
        {

        }

        public string InvoiceTitle { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public string OurOrderNumber { get; set; }
        public string YourOrderNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string CarriageBy { get; set; }
        public string PlaceOfReceiptByPrecarrier { get; set; }
        public string PortOfLoading { get; set; }
        public string PortOfDischarge { get; set; }
        public string ConsigneeAddress { get; set; }
    }
}
