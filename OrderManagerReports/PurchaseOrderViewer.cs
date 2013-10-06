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

namespace OrderManagerReports
{
    public partial class PurchaseOrderViewer : Form
    {
        public PurchaseOrderViewer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'OrderManagerDBDataSet.ProductMaterialItem' table. You can move, or remove it, as needed.
            this.ProductMaterialItemTableAdapter.Fill(this.OrderManagerDBDataSet.ProductMaterialItem);
            this.reportViewer1.RefreshReport();
        }
    }
}