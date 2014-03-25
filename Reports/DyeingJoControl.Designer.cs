namespace Reports
{
    partial class DyeingJoControl
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
            this.OrderManagerDBDyeingJoDataSet = new Reports.OrderManagerDBDyeingJoDataSet();
            this.SP_DyeingJODetailsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SP_DyeingJODetailsTableAdapter = new Reports.OrderManagerDBDyeingJoDataSetTableAdapters.SP_DyeingJODetailsTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBDyeingJoDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_DyeingJODetailsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DyeingJoItems";
            reportDataSource1.Value = this.SP_DyeingJODetailsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Reports.DyeingJobOrder.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(419, 417);
            this.reportViewer1.TabIndex = 0;
            // 
            // OrderManagerDBDyeingJoDataSet
            // 
            this.OrderManagerDBDyeingJoDataSet.DataSetName = "OrderManagerDBDyeingJoDataSet";
            this.OrderManagerDBDyeingJoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // SP_DyeingJODetailsBindingSource
            // 
            this.SP_DyeingJODetailsBindingSource.DataMember = "SP_DyeingJODetails";
            this.SP_DyeingJODetailsBindingSource.DataSource = this.OrderManagerDBDyeingJoDataSet;
            // 
            // SP_DyeingJODetailsTableAdapter
            // 
            this.SP_DyeingJODetailsTableAdapter.ClearBeforeFill = true;
            // 
            // DyeingJoControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.reportViewer1);
            this.Name = "DyeingJoControl";
            this.Size = new System.Drawing.Size(419, 417);
            ((System.ComponentModel.ISupportInitialize)(this.OrderManagerDBDyeingJoDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_DyeingJODetailsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource SP_DyeingJODetailsBindingSource;
        private OrderManagerDBDyeingJoDataSet OrderManagerDBDyeingJoDataSet;
        private OrderManagerDBDyeingJoDataSetTableAdapters.SP_DyeingJODetailsTableAdapter SP_DyeingJODetailsTableAdapter;
    }
}
