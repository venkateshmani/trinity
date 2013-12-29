namespace Reports
{
    partial class BudgetReportControl
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.OrderManagerDBDataSet = new Reports.OrderManagerDBDataSet();
            this.SP_MaterialDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SP_MaterialDetailsTableAdapter = new Reports.OrderManagerDBDataSetTableAdapters.SP_MaterialDetailsTableAdapter();
            this.OrderManagerDBOtherCostDataSet = new Reports.OrderManagerDBOtherCostDataSet();
            this.SP_MaterialOtherCostDetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SP_MaterialOtherCostDetailsTableAdapter = new Reports.OrderManagerDBOtherCostDataSetTableAdapters.SP_MaterialOtherCostDetailsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_MaterialDetailsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBOtherCostDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_MaterialOtherCostDetailsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "MaterialsForBudgetDataSet";
            reportDataSource1.Value = this.SP_MaterialDetailsBindingSource;
            reportDataSource2.Name = "MterialOtherCostForBudgetDataSet";
            reportDataSource2.Value = this.SP_MaterialOtherCostDetailsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Reports.BudgetReportSheet.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(501, 477);
            this.reportViewer1.TabIndex = 0;
            // 
            // OrderManagerDBDataSet
            // 
            this.OrderManagerDBDataSet.DataSetName = "OrderManagerDBDataSet";
            this.OrderManagerDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SP_MaterialDetailsBindingSource
            // 
            this.SP_MaterialDetailsBindingSource.DataMember = "SP_MaterialDetails";
            this.SP_MaterialDetailsBindingSource.DataSource = this.OrderManagerDBDataSet;
            // 
            // SP_MaterialDetailsTableAdapter
            // 
            this.SP_MaterialDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // OrderManagerDBOtherCostDataSet
            // 
            this.OrderManagerDBOtherCostDataSet.DataSetName = "OrderManagerDBOtherCostDataSet";
            this.OrderManagerDBOtherCostDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SP_MaterialOtherCostDetailsBindingSource
            // 
            this.SP_MaterialOtherCostDetailsBindingSource.DataMember = "SP_MaterialOtherCostDetails";
            this.SP_MaterialOtherCostDetailsBindingSource.DataSource = this.OrderManagerDBOtherCostDataSet;
            // 
            // SP_MaterialOtherCostDetailsTableAdapter
            // 
            this.SP_MaterialOtherCostDetailsTableAdapter.ClearBeforeFill = true;
            // 
            // BudgetReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportViewer1);
            this.Name = "BudgetReportControl";
            this.Size = new System.Drawing.Size(501, 477);
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_MaterialDetailsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBOtherCostDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_MaterialOtherCostDetailsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SP_MaterialDetailsBindingSource;
        private OrderManagerDBDataSet OrderManagerDBDataSet;
        private System.Windows.Forms.BindingSource SP_MaterialOtherCostDetailsBindingSource;
        private OrderManagerDBOtherCostDataSet OrderManagerDBOtherCostDataSet;
        private OrderManagerDBDataSetTableAdapters.SP_MaterialDetailsTableAdapter SP_MaterialDetailsTableAdapter;
        private OrderManagerDBOtherCostDataSetTableAdapters.SP_MaterialOtherCostDetailsTableAdapter SP_MaterialOtherCostDetailsTableAdapter;
    }
}
