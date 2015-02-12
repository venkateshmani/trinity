namespace Reports
{
    partial class GrnReportControl
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
            this.dataset = new Reports.dataset();
            this.SP_GRNReportBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SP_GRNReportTableAdapter = new Reports.datasetTableAdapters.SP_GRNReportTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_GRNReportBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "Datasource";
            reportDataSource1.Value = this.SP_GRNReportBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Reports.GrnReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(437, 307);
            this.reportViewer1.TabIndex = 0;
            // 
            // dataset
            // 
            this.dataset.DataSetName = "dataset";
            this.dataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SP_GRNReportBindingSource
            // 
            this.SP_GRNReportBindingSource.DataMember = "SP_GRNReport";
            this.SP_GRNReportBindingSource.DataSource = this.dataset;
            // 
            // SP_GRNReportTableAdapter
            // 
            this.SP_GRNReportTableAdapter.ClearBeforeFill = true;
            // 
            // GrnReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportViewer1);
            this.Name = "GrnReportControl";
            this.Size = new System.Drawing.Size(437, 307);
            ((System.ComponentModel.ISupportInitialize)(this.dataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_GRNReportBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SP_GRNReportBindingSource;
        private dataset dataset;
        private datasetTableAdapters.SP_GRNReportTableAdapter SP_GRNReportTableAdapter;

    }
}
