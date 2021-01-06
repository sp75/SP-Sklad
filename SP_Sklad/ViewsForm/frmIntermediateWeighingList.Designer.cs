namespace SP_Sklad.ViewsForm
{
    partial class frmIntermediateWeighingList
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
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.GetWayBillMakeDetBS = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.pivotGridField1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField2 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField3 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.pivotGridField4 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GetWayBillMakeDetBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 509);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1021, 52);
            this.BottomPanel.TabIndex = 19;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(799, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вибрати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(909, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // GetWayBillMakeDetBS
            // 
            this.GetWayBillMakeDetBS.DataSource = typeof(SP_Sklad.WBDetForm.frmIntermediateWeighingDet.make_det);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.pivotGridControl1);
            this.panelControl1.Controls.Add(this.textEdit1);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1021, 509);
            this.panelControl1.TabIndex = 23;
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridControl1.Appearance.Cell.Options.UseFont = true;
            this.pivotGridControl1.Appearance.DataHeaderArea.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridControl1.Appearance.DataHeaderArea.Options.UseFont = true;
            this.pivotGridControl1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.pivotGridControl1.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridControl1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.pivotGridControl1.Appearance.FocusedCell.Options.UseFont = true;
            this.pivotGridControl1.Appearance.RowHeaderArea.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridControl1.Appearance.RowHeaderArea.Options.UseFont = true;
            this.pivotGridControl1.Appearance.SelectedCell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridControl1.Appearance.SelectedCell.Options.UseFont = true;
            this.pivotGridControl1.DataSource = this.bindingSource1;
            this.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pivotGridControl1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.pivotGridField1,
            this.pivotGridField2,
            this.pivotGridField3,
            this.pivotGridField4});
            this.pivotGridControl1.Location = new System.Drawing.Point(2, 53);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.OptionsSelection.MultiSelect = false;
            this.pivotGridControl1.OptionsView.ShowColumnHeaders = false;
            this.pivotGridControl1.OptionsView.ShowDataHeaders = false;
            this.pivotGridControl1.OptionsView.ShowFilterHeaders = false;
            this.pivotGridControl1.OptionsView.ShowRowGrandTotalHeader = false;
            this.pivotGridControl1.OptionsView.ShowRowGrandTotals = false;
            this.pivotGridControl1.Size = new System.Drawing.Size(1017, 454);
            this.pivotGridControl1.TabIndex = 36;
            this.pivotGridControl1.CellDoubleClick += new DevExpress.XtraPivotGrid.PivotCellEventHandler(this.pivotGridControl1_CellDoubleClick);
            this.pivotGridControl1.CellClick += new DevExpress.XtraPivotGrid.PivotCellEventHandler(this.pivotGridControl1_CellClick);
            this.pivotGridControl1.FocusedCellChanged += new System.EventHandler(this.pivotGridControl1_FocusedCellChanged);
            // 
            // bindingSource1
            // 
            this.bindingSource1.DataSource = typeof(SP_Sklad.WBDetForm.frmIntermediateWeighingDet.make_det);
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.AllowedAreas = DevExpress.XtraPivotGrid.PivotGridAllowedAreas.RowArea;
            this.pivotGridField1.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridField1.Appearance.Header.Options.UseFont = true;
            this.pivotGridField1.Appearance.Value.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridField1.Appearance.Value.Options.UseFont = true;
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField1.AreaIndex = 0;
            this.pivotGridField1.Caption = "Сировина";
            this.pivotGridField1.FieldName = "MatName";
            this.pivotGridField1.Name = "pivotGridField1";
            this.pivotGridField1.Options.ReadOnly = true;
            this.pivotGridField1.Width = 358;
            // 
            // pivotGridField2
            // 
            this.pivotGridField2.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridField2.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField2.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridField2.Appearance.Header.Options.UseFont = true;
            this.pivotGridField2.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField2.AreaIndex = 1;
            this.pivotGridField2.Caption = "Од.";
            this.pivotGridField2.FieldName = "MsrName";
            this.pivotGridField2.Name = "pivotGridField2";
            this.pivotGridField2.Width = 60;
            // 
            // pivotGridField3
            // 
            this.pivotGridField3.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridField3.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField3.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridField3.Appearance.Header.Options.UseFont = true;
            this.pivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.pivotGridField3.AreaIndex = 0;
            this.pivotGridField3.Caption = "Зважено";
            this.pivotGridField3.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.pivotGridField3.FieldName = "AmountIntermediateWeighing";
            this.pivotGridField3.Name = "pivotGridField3";
            // 
            // pivotGridField4
            // 
            this.pivotGridField4.Appearance.Cell.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridField4.Appearance.Cell.Options.UseFont = true;
            this.pivotGridField4.Appearance.Header.Font = new System.Drawing.Font("Tahoma", 10F);
            this.pivotGridField4.Appearance.Header.Options.UseFont = true;
            this.pivotGridField4.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.pivotGridField4.AreaIndex = 0;
            this.pivotGridField4.Caption = "#";
            this.pivotGridField4.FieldName = "Rn";
            this.pivotGridField4.Name = "pivotGridField4";
            this.pivotGridField4.Width = 90;
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(64, 16);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(278, 22);
            this.textEdit1.StyleController = this.styleController1;
            this.textEdit1.TabIndex = 32;
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 19);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 16);
            this.labelControl3.StyleController = this.styleController1;
            this.labelControl3.TabIndex = 27;
            this.labelControl3.Text = "Рецепт:";
            // 
            // frmIntermediateWeighingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 561);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmIntermediateWeighingList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список сировини для зважування";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmUserGroup_FormClosed);
            this.Load += new System.EventHandler(this.frmUserGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GetWayBillMakeDetBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.BindingSource GetWayBillMakeDetBS;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField2;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField3;
        private System.Windows.Forms.BindingSource bindingSource1;
        private DevExpress.XtraPivotGrid.PivotGridField pivotGridField4;
    }
}