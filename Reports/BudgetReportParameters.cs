using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports
{
    public class BudgetReportParameters
    {
        public string Buyer {get;set;}
        public string OrderID {get;set;}
        public string OrderDate {get;set;}
        public string ProductName {get;set;}
        public string ExpectedQuantity {get;set;}
        public string PerUnitBuyerTargetPrice {get;set;}
        public string TotalValue {get;set;}
        public string OrderedProductCurrency {get;set;}
        public string CurrencyValueInINR {get;set;}
        public string StyleID {get;set;}
        public string DateOfGeneration {get;set;}
        public string OurPrice {get;set;}
        public string ProfitOrLoss {get;set;}
        public string NumberOfItems { get; set; }

    }
}
