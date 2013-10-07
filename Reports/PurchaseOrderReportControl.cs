using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reports
{
    public partial class PurchaseOrderReportControl : UserControl
    {
        public PurchaseOrderReportControl()
        {
            InitializeComponent();
        }

        public void Generate(long? orderId, int? supplierId)
        {
            this.SP_PurchaseOrderTableAdapter.Fill(this.OrderManagerDBDataSet.SP_PurchaseOrder, orderId, supplierId);
            this.reportViewer1.RefreshReport();
        }
    }
}
