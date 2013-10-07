namespace Reports
{
    partial class PurchaseOrderReportControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SP_PurchaseOrderTableAdapter = new Reports.OrderManagerDBDataSetTableAdapters.SP_PurchaseOrderTableAdapter();
            this.SP_PurchaseOrderBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.OrderManagerDBDataSet = new Reports.OrderManagerDBDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.SP_PurchaseOrderBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "PODataSet";
            reportDataSource1.Value = this.SP_PurchaseOrderBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Reports.PurchaseReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(463, 412);
            this.reportViewer1.TabIndex = 1;
            // 
            // SP_PurchaseOrderTableAdapter
            // 
            this.SP_PurchaseOrderTableAdapter.ClearBeforeFill = true;
            // 
            // SP_PurchaseOrderBindingSource
            // 
            this.SP_PurchaseOrderBindingSource.DataMember = "SP_PurchaseOrder";
            this.SP_PurchaseOrderBindingSource.DataSource = this.OrderManagerDBDataSet;
            // 
            // OrderManagerDBDataSet
            // 
            this.OrderManagerDBDataSet.DataSetName = "OrderManagerDBDataSet";
            this.OrderManagerDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // PurchaseOrderReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportViewer1);
            this.Name = "PurchaseOrderReportControl";
            this.Size = new System.Drawing.Size(463, 412);
            ((System.ComponentModel.ISupportInitialize)(this.SP_PurchaseOrderBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private OrderManagerDBDataSetTableAdapters.SP_PurchaseOrderTableAdapter SP_PurchaseOrderTableAdapter;
        private System.Windows.Forms.BindingSource SP_PurchaseOrderBindingSource;
        private OrderManagerDBDataSet OrderManagerDBDataSet;
    }
}
