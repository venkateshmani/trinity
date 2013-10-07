//------------------------------------------------------------------
// <copyright company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reports
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'OrderManagerDBDataSet.SP_PurchaseOrder' table. You can move, or remove it, as needed.
            this.SP_PurchaseOrderTableAdapter.Fill(this.OrderManagerDBDataSet.SP_PurchaseOrder, 9, 24);
            this.reportViewer1.RefreshReport();

            purchaseOrderReportControl1.Generate(9, 24);
        }
    }
}