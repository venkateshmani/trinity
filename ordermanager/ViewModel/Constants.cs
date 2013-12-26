using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel
{
    public static class Constants
    {
        public static string GetPurchaseOrderNumber(Company supplier, Order order)
        {
            string startYear = string.Empty;
            string endYear = string.Empty;
            int currentYear = int.Parse(DateTime.Now.ToString("yy"));
            string poUniqueNumber = order.OrderID.ToString() + "-" + supplier.CompanyID.ToString();
            string timeStamp = DateTime.Now.ToString("ddMMhhmmtt");

            if (DateTime.Now.Month >= 4)
            {
                startYear = currentYear.ToString();
                endYear = (currentYear + 1).ToString();
            }
            else
            {
                startYear = (currentYear - 1).ToString();
                endYear = currentYear.ToString();
            }


            return string.Format("TCIPL/{0}-{1}/MAC/{2}/{3}", startYear, endYear, timeStamp, poUniqueNumber);
        }

        public static string GetInvoiceNumber(Order order)
        {
            string startYear = string.Empty;
            string endYear = string.Empty;
            int currentYear = int.Parse(DateTime.Now.ToString("yy"));
            string invoiceUniqueNumber = order.OrderID.ToString() + "-" + (order.Invoices.Count + 1).ToString();
            string timeStamp = DateTime.Now.ToString("ddMMhhmmtt");

            if (DateTime.Now.Month >= 4)
            {
                startYear = currentYear.ToString();
                endYear = (currentYear + 1).ToString();
            }
            else
            {
                startYear = (currentYear - 1).ToString();
                endYear = currentYear.ToString();
            }


            return string.Format("INV/{0}-{1}/{2}/{3}", startYear, endYear, timeStamp, invoiceUniqueNumber);
        }
    }
}
