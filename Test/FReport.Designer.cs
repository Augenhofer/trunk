namespace Feuchte_Rapport
{
    partial class FReport
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.PCI_MaintenanceBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labAbDatum = new System.Windows.Forms.Label();
            this.labBisDatum = new System.Windows.Forms.Label();
            this.dtTiPickerAb = new System.Windows.Forms.DateTimePicker();
            this.dtTiPickerBis = new System.Windows.Forms.DateTimePicker();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.labFilter = new System.Windows.Forms.Label();
            this.txtBoxFilter = new System.Windows.Forms.TextBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.radBtnRapport = new System.Windows.Forms.RadioButton();
            this.radBtnHumidity = new System.Windows.Forms.RadioButton();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.LaPassDataSet = new Feuchte_Rapport.LaPassDataSet();
            this.HumidityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PCI_MaintenanceBindingSource)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LaPassDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HumidityBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // PCI_MaintenanceBindingSource
            // 
            this.PCI_MaintenanceBindingSource.DataMember = "PCI_Maintenance";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.reportViewer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1067, 792);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tableLayoutPanel2.ColumnCount = 14;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Controls.Add(this.labAbDatum, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.labBisDatum, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtTiPickerAb, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtTiPickerBis, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.pictureBox1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnClose, 13, 0);
            this.tableLayoutPanel2.Controls.Add(this.labFilter, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtBoxFilter, 9, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnFilter, 11, 0);
            this.tableLayoutPanel2.Controls.Add(this.radBtnRapport, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.radBtnHumidity, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1061, 73);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // labAbDatum
            // 
            this.labAbDatum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labAbDatum.AutoSize = true;
            this.labAbDatum.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labAbDatum.Location = new System.Drawing.Point(267, 13);
            this.labAbDatum.Name = "labAbDatum";
            this.labAbDatum.Size = new System.Drawing.Size(73, 46);
            this.labAbDatum.TabIndex = 0;
            this.labAbDatum.Text = "ab Datum:";
            this.labAbDatum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labBisDatum
            // 
            this.labBisDatum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labBisDatum.AutoSize = true;
            this.labBisDatum.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.labBisDatum.Location = new System.Drawing.Point(452, 13);
            this.labBisDatum.Name = "labBisDatum";
            this.labBisDatum.Size = new System.Drawing.Size(73, 46);
            this.labBisDatum.TabIndex = 1;
            this.labBisDatum.Text = "bis Datum:";
            this.labBisDatum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtTiPickerAb
            // 
            this.dtTiPickerAb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtTiPickerAb.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.dtTiPickerAb.Location = new System.Drawing.Point(346, 21);
            this.dtTiPickerAb.Name = "dtTiPickerAb";
            this.dtTiPickerAb.Size = new System.Drawing.Size(100, 31);
            this.dtTiPickerAb.TabIndex = 3;
            this.dtTiPickerAb.Value = new System.DateTime(2016, 10, 12, 0, 0, 0, 0);
            this.dtTiPickerAb.CloseUp += new System.EventHandler(this.dtTiPicker_CloseUp);
            // 
            // dtTiPickerBis
            // 
            this.dtTiPickerBis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtTiPickerBis.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTiPickerBis.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.dtTiPickerBis.Location = new System.Drawing.Point(531, 21);
            this.dtTiPickerBis.Name = "dtTiPickerBis";
            this.dtTiPickerBis.Size = new System.Drawing.Size(100, 31);
            this.dtTiPickerBis.TabIndex = 4;
            this.dtTiPickerBis.CloseUp += new System.EventHandler(this.dtTiPicker_CloseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Feuchte_Rapport.Properties.Resources.filter;
            this.pictureBox1.Location = new System.Drawing.Point(29, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 67);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Calibri", 14.25F);
            this.btnClose.Image = global::Feuchte_Rapport.Properties.Resources.CloseForm;
            this.btnClose.Location = new System.Drawing.Point(954, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 43);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Schließen";
            this.btnClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // labFilter
            // 
            this.labFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labFilter.AutoSize = true;
            this.labFilter.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labFilter.Location = new System.Drawing.Point(637, 13);
            this.labFilter.Name = "labFilter";
            this.labFilter.Size = new System.Drawing.Size(47, 46);
            this.labFilter.TabIndex = 7;
            this.labFilter.Text = "Filter:";
            this.labFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBoxFilter
            // 
            this.txtBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxFilter.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxFilter.Location = new System.Drawing.Point(690, 21);
            this.txtBoxFilter.Name = "txtBoxFilter";
            this.txtBoxFilter.Size = new System.Drawing.Size(100, 31);
            this.txtBoxFilter.TabIndex = 8;
            // 
            // btnFilter
            // 
            this.btnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFilter.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilter.Image = global::Feuchte_Rapport.Properties.Resources.filter;
            this.btnFilter.Location = new System.Drawing.Point(827, 15);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(68, 43);
            this.btnFilter.TabIndex = 9;
            this.btnFilter.Text = "Los";
            this.btnFilter.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // radBtnRapport
            // 
            this.radBtnRapport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radBtnRapport.AutoSize = true;
            this.radBtnRapport.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radBtnRapport.Location = new System.Drawing.Point(82, 23);
            this.radBtnRapport.Name = "radBtnRapport";
            this.radBtnRapport.Size = new System.Drawing.Size(100, 27);
            this.radBtnRapport.TabIndex = 10;
            this.radBtnRapport.TabStop = true;
            this.radBtnRapport.Text = "Rapport_Buch";
            this.radBtnRapport.UseVisualStyleBackColor = true;
            this.radBtnRapport.CheckedChanged += new System.EventHandler(this.radBtnRapport_CheckedChanged);
            // 
            // radBtnHumidity
            // 
            this.radBtnHumidity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radBtnHumidity.AutoSize = true;
            this.radBtnHumidity.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radBtnHumidity.Location = new System.Drawing.Point(188, 23);
            this.radBtnHumidity.Name = "radBtnHumidity";
            this.radBtnHumidity.Size = new System.Drawing.Size(73, 27);
            this.radBtnHumidity.TabIndex = 11;
            this.radBtnHumidity.TabStop = true;
            this.radBtnHumidity.Text = "Feuchte";
            this.radBtnHumidity.UseVisualStyleBackColor = true;
            this.radBtnHumidity.CheckedChanged += new System.EventHandler(this.radBtnHumidity_CheckedChanged);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "LaPassDataSet";
            reportDataSource2.Value = this.HumidityBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Feuchte_Rapport.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 82);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(1061, 707);
            this.reportViewer1.TabIndex = 4;
            // 
            // LaPassDataSet
            // 
            this.LaPassDataSet.DataSetName = "LaPassDataSet";
            this.LaPassDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // HumidityBindingSource
            // 
            this.HumidityBindingSource.DataMember = "Humidity";
            this.HumidityBindingSource.DataSource = this.LaPassDataSet;
            // 
            // FReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 792);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Berichte";
            this.Load += new System.EventHandler(this.FReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PCI_MaintenanceBindingSource)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LaPassDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HumidityBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource PCI_MaintenanceBindingSource;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label labAbDatum;
        private System.Windows.Forms.Label labBisDatum;
        private System.Windows.Forms.DateTimePicker dtTiPickerAb;
        private System.Windows.Forms.DateTimePicker dtTiPickerBis;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private LaPassDataSet LaPassDataSet;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Label labFilter;
        private System.Windows.Forms.TextBox txtBoxFilter;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.RadioButton radBtnRapport;
        private System.Windows.Forms.RadioButton radBtnHumidity;
        private System.Windows.Forms.BindingSource HumidityBindingSource;
    }
}