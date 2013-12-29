namespace Reports
{
    partial class InvoiceUserControl
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
            this.OrderManagerDBDataSetForInvoiceGeneration = new Reports.OrderManagerDBDataSetForInvoiceGeneration();
            this.SP_InvoiceDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SP_InvoiceDetailsTableAdapter = new Reports.OrderManagerDBDataSetForInvoiceGenerationTableAdapters.SP_InvoiceDetailsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBDataSetForInvoiceGeneration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_InvoiceDetailsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "InvoiceDataSet";
            reportDataSource1.Value = this.SP_InvoiceDetailsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Reports.Invoice.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(496, 477);
            this.reportViewer1.TabIndex = 0;
            // 
            // OrderManagerDBDataSetForInvoiceGeneration
            // 
            this.OrderManagerDBDataSetForInvoiceGeneration.DataSetName = "OrderManagerDBDataSetForInvoiceGeneration";
            this.OrderManagerDBDataSetForInvoiceGeneration.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SP_InvoiceDetailsBindingSource
            // 
            this.SP_InvoiceDetailsBindingSource.DataMember = "SP_InvoiceDetails";
            this.SP_InvoiceDetailsBindingSource.DataSource = this.OrderManagerDBDataSetForInvoiceGeneration;
            // 
            // SP_InvoiceDetailsTableAdapter
            // 
            this.SP_InvoiceDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // InvoiceUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportViewer1);
            this.Name = "InvoiceUserControl";
            this.Size = new System.Drawing.Size(496, 477);
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBDataSetForInvoiceGeneration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_InvoiceDetailsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SP_InvoiceDetailsBindingSource;
        private OrderManagerDBDataSetForInvoiceGeneration OrderManagerDBDataSetForInvoiceGeneration;
        private OrderManagerDBDataSetForInvoiceGenerationTableAdapters.SP_InvoiceDetailsTableAdapter SP_InvoiceDetailsTableAdapter;
    }
}
