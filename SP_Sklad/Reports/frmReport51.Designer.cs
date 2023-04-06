namespace SP_Sklad.ViewsForm
{
    partial class frmReport51
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReport51));
            DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.SideBySideBarSeriesView sideBySideBarSeriesView1 = new DevExpress.XtraCharts.SideBySideBarSeriesView();
            DevExpress.XtraCharts.LineSeriesView lineSeriesView1 = new DevExpress.XtraCharts.LineSeriesView();
            DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.PerYearPanel = new System.Windows.Forms.Panel();
            this.YearEdit3 = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.GrpComboBox = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.checkedComboBoxEdit1 = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.checkedComboBoxEdit2 = new DevExpress.XtraEditors.CheckedComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.REP_51BS = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).BeginInit();
            this.PerYearPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YearEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrpComboBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.REP_51BS)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 520);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1008, 52);
            this.BottomPanel.TabIndex = 18;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(12, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(149, 30);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Попередній перегляд";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("OkButton.ImageOptions.Image")));
            this.OkButton.Location = new System.Drawing.Point(911, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(85, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Закрити";
            // 
            // chartControl1
            // 
            this.chartControl1.BackImage.Stretch = true;
            this.chartControl1.DataSource = this.REP_51BS;
            xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
            xyDiagram1.AxisY.Interlaced = true;
            xyDiagram1.AxisY.MinorCount = 4;
            xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
            this.chartControl1.Diagram = xyDiagram1;
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
            this.chartControl1.Legend.MarkerMode = DevExpress.XtraCharts.LegendMarkerMode.CheckBox;
            this.chartControl1.Location = new System.Drawing.Point(0, 133);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.OptionsPrint.SizeMode = DevExpress.XtraCharts.Printing.PrintSizeMode.Stretch;
            series1.ArgumentDataMember = "Name";
            series1.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series1.LegendTextPattern = "Січень";
            series1.Name = "JanuaryLine";
            series1.ValueDataMembersSerializable = "TotalAmountOut1";
            series2.ArgumentDataMember = "Name";
            series2.LegendTextPattern = "Лютий";
            series2.Name = "FebruaryLine";
            series2.ValueDataMembersSerializable = "TotalAmountOut2";
            series3.ArgumentDataMember = "Name";
            series3.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            series3.LegendTextPattern = "Березень";
            series3.Name = "MarchLine";
            series3.ValueDataMembersSerializable = "TotalAmountOut3";
            sideBySideBarSeriesView1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(129)))), ((int)(((byte)(189)))));
            series3.View = sideBySideBarSeriesView1;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2,
        series3};
            this.chartControl1.SeriesTemplate.View = lineSeriesView1;
            this.chartControl1.Size = new System.Drawing.Size(1008, 387);
            this.chartControl1.TabIndex = 19;
            chartTitle1.Font = new System.Drawing.Font("Tahoma", 15F);
            chartTitle1.TextColor = System.Drawing.Color.DarkSlateGray;
            this.chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
            // 
            // PerYearPanel
            // 
            this.PerYearPanel.Controls.Add(this.checkedComboBoxEdit2);
            this.PerYearPanel.Controls.Add(this.labelControl2);
            this.PerYearPanel.Controls.Add(this.checkedComboBoxEdit1);
            this.PerYearPanel.Controls.Add(this.labelControl1);
            this.PerYearPanel.Controls.Add(this.simpleButton2);
            this.PerYearPanel.Controls.Add(this.GrpComboBox);
            this.PerYearPanel.Controls.Add(this.labelControl11);
            this.PerYearPanel.Controls.Add(this.YearEdit3);
            this.PerYearPanel.Controls.Add(this.labelControl9);
            this.PerYearPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PerYearPanel.Location = new System.Drawing.Point(0, 0);
            this.PerYearPanel.Name = "PerYearPanel";
            this.PerYearPanel.Size = new System.Drawing.Size(1008, 133);
            this.PerYearPanel.TabIndex = 38;
            // 
            // YearEdit3
            // 
            this.YearEdit3.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.YearEdit3.Location = new System.Drawing.Point(12, 36);
            this.YearEdit3.Name = "YearEdit3";
            this.YearEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.YearEdit3.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.YearEdit3.Properties.IsFloatValue = false;
            this.YearEdit3.Properties.MaskSettings.Set("mask", "N00");
            this.YearEdit3.Size = new System.Drawing.Size(90, 22);
            this.YearEdit3.StyleController = this.styleController1;
            this.YearEdit3.TabIndex = 33;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(12, 14);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(21, 16);
            this.labelControl9.StyleController = this.styleController1;
            this.labelControl9.TabIndex = 32;
            this.labelControl9.Text = "Рік:";
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // GrpComboBox
            // 
            this.GrpComboBox.Location = new System.Drawing.Point(137, 36);
            this.GrpComboBox.Name = "GrpComboBox";
            this.GrpComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.GrpComboBox.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.GrpComboBox.Properties.DisplayMember = "Name";
            this.GrpComboBox.Properties.ShowFooter = false;
            this.GrpComboBox.Properties.ShowHeader = false;
            this.GrpComboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.GrpComboBox.Properties.ValueMember = "GrpId";
            this.GrpComboBox.Size = new System.Drawing.Size(320, 22);
            this.GrpComboBox.StyleController = this.styleController1;
            this.GrpComboBox.TabIndex = 35;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(137, 14);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(87, 16);
            this.labelControl11.StyleController = this.styleController1;
            this.labelControl11.TabIndex = 34;
            this.labelControl11.Text = "Група товарів:";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton2.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.ImageOptions.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(483, 88);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(125, 30);
            this.simpleButton2.TabIndex = 36;
            this.simpleButton2.Text = "Сформувати звіт";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // checkedComboBoxEdit1
            // 
            this.checkedComboBoxEdit1.EditValue = "";
            this.checkedComboBoxEdit1.Location = new System.Drawing.Point(483, 36);
            this.checkedComboBoxEdit1.Name = "checkedComboBoxEdit1";
            this.checkedComboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedComboBoxEdit1.Properties.IncrementalSearch = true;
            this.checkedComboBoxEdit1.Size = new System.Drawing.Size(336, 22);
            this.checkedComboBoxEdit1.StyleController = this.styleController1;
            this.checkedComboBoxEdit1.TabIndex = 59;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(483, 15);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 58;
            this.labelControl1.Text = "Контрагенти:";
            // 
            // checkedComboBoxEdit2
            // 
            this.checkedComboBoxEdit2.EditValue = "";
            this.checkedComboBoxEdit2.Location = new System.Drawing.Point(12, 96);
            this.checkedComboBoxEdit2.Name = "checkedComboBoxEdit2";
            this.checkedComboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.checkedComboBoxEdit2.Properties.IncrementalSearch = true;
            this.checkedComboBoxEdit2.Size = new System.Drawing.Size(445, 22);
            this.checkedComboBoxEdit2.StyleController = this.styleController1;
            this.checkedComboBoxEdit2.TabIndex = 61;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 75);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(122, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 60;
            this.labelControl2.Text = "Групи контрагентів: ";
            // 
            // REP_51BS
            // 
            this.REP_51BS.DataSource = typeof(SP_Sklad.SkladData.REP_51_Result);
            // 
            // frmReport51
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 572);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.PerYearPanel);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("frmReport51.IconOptions.Image")));
            this.Name = "frmReport51";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Динаміка продаж товарів";
            this.Load += new System.EventHandler(this.frmMaterialPriceHIstory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(lineSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.PerYearPanel.ResumeLayout(false);
            this.PerYearPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.YearEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GrpComboBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkedComboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.REP_51BS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private System.Windows.Forms.BindingSource REP_51BS;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        public DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Panel PerYearPanel;
        private DevExpress.XtraEditors.SpinEdit YearEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.StyleController styleController1;
        public DevExpress.XtraEditors.LookUpEdit GrpComboBox;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        public DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckedComboBoxEdit checkedComboBoxEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}