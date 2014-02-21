﻿using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.PurchaseOrderControl
{
    public class PurchaseOrderPDFGenerator
    {
        private Reports.PurchaseOrderReportControl purchaseOrderReportControl = null;
        public PurchaseOrderPDFGenerator(PurchaseOrder purchaseOrder)
        {
            purchaseOrderReportControl = new Reports.PurchaseOrderReportControl();
            PurchaseOrder = purchaseOrder;
        }

        private PurchaseOrder m_PurchaseOrder = null;
        public PurchaseOrder PurchaseOrder
        {
            get
            {
                return m_PurchaseOrder;
            }
            private set
            {
                m_PurchaseOrder = value;
            }
        }


        public Order Order
        {
            get
            {
                return PurchaseOrder.Order;
            }
        }

        public string GeneratePurchaseOrder()
        { 
            string fileName = string.Empty;

            try
            {
                fileName = System.IO.Path.Combine(
                                             System.IO.Path.GetTempPath(), "OM_PurchaseOrder-" + PurchaseOrder.PurchaseOrderNumber.Replace("/", "_") + ".pdf");


                if (!System.IO.File.Exists(fileName))
                {
                    if (!GeneratePurchaseOrder(fileName))
                    {
                        fileName = string.Empty;
                    }
                }
            }
            catch
            {

            }

            return fileName;
        }

        private bool GeneratePurchaseOrder(string filePath)
        {
            try
            {
                string supplierInformation = GetSupplierInformation(PurchaseOrder.Company);
                string purchaseOrderNumber = GetPurchaseOrderNumber(PurchaseOrder.Company);
                string quoteNumber = GetQuoteNumber();
                string quoteDate = GetQuoteDate(PurchaseOrder.PurchaseOrderDate);

                purchaseOrderReportControl.SetParameters(supplierInformation, purchaseOrderNumber, quoteNumber, quoteDate, PurchaseOrder.TermsAndConditions);
                purchaseOrderReportControl.CreateReportAsPDF(PurchaseOrder.PurchaseOrderID, filePath);
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }

            return true;

        }

        private string GetQuoteDate(DateTime? date)
        {
            if (date == null)
                return string.Empty;

            return date.Value.ToString("MM/dd/yyyy");
        }

        private string GetQuoteNumber()
        {
            return string.Format("HMI-{0}R", Order.OrderID.ToString());
        }

        private string GetPurchaseOrderNumber(Company supplier)
        {
            return Constants.GetPurchaseOrderNumber(supplier, Order);
        }

        private string GetSupplierInformation(Company supplier)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(supplier.Name + ",");
            if (!string.IsNullOrEmpty(supplier.Address1))
                sb.AppendLine(supplier.Address1 + ",");

            if (!string.IsNullOrEmpty(supplier.Address2))
                sb.AppendLine(supplier.Address2 + ",");

            sb.AppendLine(supplier.City + "," + supplier.State + ",");
            sb.AppendLine(supplier.Country + ".");

            return sb.ToString();

        }
    }
}
