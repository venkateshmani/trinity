using ordermanager.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public class InvoiceCartonBoxSummary
    {
        public string StyleID
        { get; set; }
        public string Size
        { get; set; }
        public string Color
        { get; set; }
        public decimal Quantity
        { get; set; }
        public decimal CostPerUnit
        { get; set; }
        public decimal TotalCost
        {
            get { return Quantity * CostPerUnit; }          
        }
    }
}
