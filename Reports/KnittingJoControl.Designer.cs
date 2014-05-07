namespace Reports
{
    partial class KnittingJoControl
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
            this.OrderManagerDBKnittingJoDataSet = new Reports.OrderManagerDBKnittingJoDataSet();
            this.SP_KnittingJODetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SP_KnittingJODetailsTableAdapter = new Reports.OrderManagerDBKnittingJoDataSetTableAdapters.SP_KnittingJODetailsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBKnittingJoDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_KnittingJODetailsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "KnittingJoItems";
            reportDataSource1.Value = this.SP_KnittingJODetailsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Reports.KnittingJobOrder.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(392, 433);
            this.reportViewer1.TabIndex = 0;
            // 
            // OrderManagerDBKnittingJoDataSet
            // 
            this.OrderManagerDBKnittingJoDataSet.DataSetName = "OrderManagerDBKnittingJoDataSet";
            this.OrderManagerDBKnittingJoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SP_KnittingJODetailsBindingSource
            // 
            this.SP_KnittingJODetailsBindingSource.DataMember = "SP_KnittingJODetails";
            this.SP_KnittingJODetailsBindingSource.DataSource = this.OrderManagerDBKnittingJoDataSet;
            // 
            // SP_KnittingJODetailsTableAdapter
            // 
            this.SP_KnittingJODetailsTableAdapter.ClearBeforeFill = true;
            // 
            // KnittingJoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportViewer1);
            this.Name = "KnittingJoControl";
            this.Size = new System.Drawing.Size(392, 433);
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBKnittingJoDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_KnittingJODetailsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SP_KnittingJODetailsBindingSource;
        private OrderManagerDBKnittingJoDataSet OrderManagerDBKnittingJoDataSet;
        private OrderManagerDBKnittingJoDataSetTableAdapters.SP_KnittingJODetailsTableAdapter SP_KnittingJODetailsTableAdapter;

    }
}
