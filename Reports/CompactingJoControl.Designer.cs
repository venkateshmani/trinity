namespace Reports
{
    partial class CompactingJoControl
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
            this.CompactingJoDataSet = new Reports.CompactingJoDataSet();
            this.SP_CompactingJoDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SP_CompactingJoDetailsTableAdapter = new Reports.CompactingJoDataSetTableAdapters.SP_CompactingJoDetailsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.CompactingJoDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_CompactingJoDetailsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "CompactingJoItems";
            reportDataSource1.Value = this.SP_CompactingJoDetailsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Reports.CompactingJobOrder.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(150, 150);
            this.reportViewer1.TabIndex = 0;
            // 
            // CompactingJoDataSet
            // 
            this.CompactingJoDataSet.DataSetName = "CompactingJoDataSet";
            this.CompactingJoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SP_CompactingJoDetailsBindingSource
            // 
            this.SP_CompactingJoDetailsBindingSource.DataMember = "SP_CompactingJoDetails";
            this.SP_CompactingJoDetailsBindingSource.DataSource = this.CompactingJoDataSet;
            // 
            // SP_CompactingJoDetailsTableAdapter
            // 
            this.SP_CompactingJoDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // CompactingJoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportViewer1);
            this.Name = "CompactingJoControl";
            ((System.ComponentModel.ISupportInitialize)(this.CompactingJoDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_CompactingJoDetailsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SP_CompactingJoDetailsBindingSource;
        private CompactingJoDataSet CompactingJoDataSet;
        private CompactingJoDataSetTableAdapters.SP_CompactingJoDetailsTableAdapter SP_CompactingJoDetailsTableAdapter;
    }
}
