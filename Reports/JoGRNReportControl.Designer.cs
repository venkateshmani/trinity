namespace Reports
{
    partial class JoGRNReportControl
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
            this.SP_JoGRNBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.joGRNReportDataSet = new Reports.joGRNReportDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SP_JoGRNTableAdapter = new Reports.joGRNReportDataSetTableAdapters.SP_JoGRNTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.SP_JoGRNBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.joGRNReportDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // SP_JoGRNBindingSource
            // 
            this.SP_JoGRNBindingSource.DataMember = "SP_JoGRN";
            this.SP_JoGRNBindingSource.DataSource = this.joGRNReportDataSet;
            // 
            // joGRNReportDataSet
            // 
            this.joGRNReportDataSet.DataSetName = "joGRNReportDataSet";
            this.joGRNReportDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "joReportDataSet";
            reportDataSource1.Value = this.SP_JoGRNBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Reports.joGrnReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(150, 150);
            this.reportViewer1.TabIndex = 0;
            // 
            // SP_JoGRNTableAdapter
            // 
            this.SP_JoGRNTableAdapter.ClearBeforeFill = true;
            // 
            // JoGRNReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportViewer1);
            this.Name = "JoGRNReportControl";
            ((System.ComponentModel.ISupportInitialize)(this.SP_JoGRNBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.joGRNReportDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SP_JoGRNBindingSource;
        private joGRNReportDataSet joGRNReportDataSet;
        private joGRNReportDataSetTableAdapters.SP_JoGRNTableAdapter SP_JoGRNTableAdapter;
    }
}
