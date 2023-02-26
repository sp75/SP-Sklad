namespace SP_Sklad.WBForm
{
    partial class frmCashboxRefund
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
            this.SumAllEdit = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl7 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.PutCashlessSumEdit = new DevExpress.XtraEditors.CalcEdit();
            this.PutCashSumEdit = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.PayBtn = new DevExpress.XtraEditors.SimpleButton();
            this.WaybillDetOutGridControl = new DevExpress.XtraGrid.GridControl();
            this.WaybillDetInBS = new System.Windows.Forms.BindingSource(this.components);
            this.WBDetReInGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.SumAllEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).BeginInit();
            this.panelControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PutCashlessSumEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PutCashSumEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetOutGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetInBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WBDetReInGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // SumAllEdit
            // 
            this.SumAllEdit.Location = new System.Drawing.Point(12, 27);
            this.SumAllEdit.Name = "SumAllEdit";
            this.SumAllEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 16F);
            this.SumAllEdit.Properties.Appearance.Options.UseFont = true;
            this.SumAllEdit.Properties.DisplayFormat.FormatString = "0.00";
            this.SumAllEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.SumAllEdit.Properties.ReadOnly = true;
            this.SumAllEdit.Properties.ShowCloseButton = true;
            this.SumAllEdit.Size = new System.Drawing.Size(232, 32);
            this.SumAllEdit.TabIndex = 45;
            this.SumAllEdit.TabStop = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 6);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(108, 16);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Сума повернення:";
            // 
            // panelControl7
            // 
            this.panelControl7.Controls.Add(this.labelControl1);
            this.panelControl7.Controls.Add(this.SumAllEdit);
            this.panelControl7.Controls.Add(this.labelControl3);
            this.panelControl7.Controls.Add(this.PutCashlessSumEdit);
            this.panelControl7.Controls.Add(this.PutCashSumEdit);
            this.panelControl7.Controls.Add(this.labelControl4);
            this.panelControl7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl7.Location = new System.Drawing.Point(0, 0);
            this.panelControl7.Name = "panelControl7";
            this.panelControl7.Size = new System.Drawing.Size(790, 74);
            this.panelControl7.TabIndex = 71;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(546, 6);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(161, 16);
            this.labelControl3.TabIndex = 48;
            this.labelControl3.Text = "Сума повернення (картка):";
            // 
            // PutCashlessSumEdit
            // 
            this.PutCashlessSumEdit.Location = new System.Drawing.Point(546, 27);
            this.PutCashlessSumEdit.Name = "PutCashlessSumEdit";
            this.PutCashlessSumEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 16F);
            this.PutCashlessSumEdit.Properties.Appearance.Options.UseFont = true;
            this.PutCashlessSumEdit.Properties.DisplayFormat.FormatString = "0.00";
            this.PutCashlessSumEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.PutCashlessSumEdit.Properties.ShowCloseButton = true;
            this.PutCashlessSumEdit.Size = new System.Drawing.Size(232, 32);
            this.PutCashlessSumEdit.TabIndex = 47;
            this.PutCashlessSumEdit.TabStop = false;
            this.PutCashlessSumEdit.EditValueChanged += new System.EventHandler(this.PutCashlessSumEdit_EditValueChanged);
            // 
            // PutCashSumEdit
            // 
            this.PutCashSumEdit.Cursor = System.Windows.Forms.Cursors.Default;
            this.PutCashSumEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PutCashSumEdit.Location = new System.Drawing.Point(281, 27);
            this.PutCashSumEdit.Name = "PutCashSumEdit";
            this.PutCashSumEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 16F);
            this.PutCashSumEdit.Properties.Appearance.Options.UseFont = true;
            this.PutCashSumEdit.Properties.DisplayFormat.FormatString = "0.00";
            this.PutCashSumEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.PutCashSumEdit.Properties.ShowCloseButton = true;
            this.PutCashSumEdit.Size = new System.Drawing.Size(232, 32);
            this.PutCashSumEdit.TabIndex = 46;
            this.PutCashSumEdit.TabStop = false;
            this.PutCashSumEdit.EditValueChanged += new System.EventHandler(this.PutSumEdit_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(281, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(167, 16);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "Сума повернення  (готівка):";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.PayBtn);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 427);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(790, 90);
            this.panelControl2.TabIndex = 72;
            // 
            // PayBtn
            // 
            this.PayBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PayBtn.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.PayBtn.Appearance.Options.UseFont = true;
            this.PayBtn.Location = new System.Drawing.Point(567, 13);
            this.PayBtn.Name = "PayBtn";
            this.PayBtn.Size = new System.Drawing.Size(211, 65);
            this.PayBtn.TabIndex = 83;
            this.PayBtn.Text = "Повернути кошти (F9)";
            this.PayBtn.Click += new System.EventHandler(this.PayBtn_Click);
            // 
            // WaybillDetOutGridControl
            // 
            this.WaybillDetOutGridControl.DataSource = this.WaybillDetInBS;
            this.WaybillDetOutGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WaybillDetOutGridControl.Location = new System.Drawing.Point(0, 74);
            this.WaybillDetOutGridControl.MainView = this.WBDetReInGridView;
            this.WaybillDetOutGridControl.Name = "WaybillDetOutGridControl";
            this.WaybillDetOutGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCalcEdit1,
            this.repositoryItemCheckEdit1});
            this.WaybillDetOutGridControl.Size = new System.Drawing.Size(790, 353);
            this.WaybillDetOutGridControl.TabIndex = 73;
            this.WaybillDetOutGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.WBDetReInGridView});
            // 
            // WaybillDetInBS
            // 
            this.WaybillDetInBS.DataSource = typeof(SP_Sklad.SkladData.GetWaybillDetIn_Result);
            // 
            // WBDetReInGridView
            // 
            this.WBDetReInGridView.Appearance.FocusedRow.BackColor = System.Drawing.Color.LemonChiffon;
            this.WBDetReInGridView.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.WBDetReInGridView.Appearance.FocusedRow.Options.UseBackColor = true;
            this.WBDetReInGridView.Appearance.FocusedRow.Options.UseForeColor = true;
            this.WBDetReInGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.WBDetReInGridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.WBDetReInGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.WBDetReInGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.WBDetReInGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 14F);
            this.WBDetReInGridView.Appearance.Row.Options.UseFont = true;
            this.WBDetReInGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn7,
            this.gridColumn10,
            this.gridColumn6,
            this.gridColumn8});
            this.WBDetReInGridView.GridControl = this.WaybillDetOutGridControl;
            this.WBDetReInGridView.Name = "WBDetReInGridView";
            this.WBDetReInGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.WBDetReInGridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.WBDetReInGridView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "#";
            this.gridColumn2.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn2.FieldName = "Checked";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowMove = false;
            this.gridColumn2.OptionsColumn.AllowSize = false;
            this.gridColumn2.OptionsColumn.ShowCaption = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 40;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.repositoryItemCheckEdit1.ValueChecked = 1;
            this.repositoryItemCheckEdit1.ValueUnchecked = 0;
            this.repositoryItemCheckEdit1.CheckedChanged += new System.EventHandler(this.repositoryItemCheckEdit1_CheckedChanged);
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Назва товару/послуги";
            this.gridColumn3.FieldName = "MatName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 300;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Кількість";
            this.gridColumn4.ColumnEdit = this.repositoryItemCalcEdit1;
            this.gridColumn4.FieldName = "Amount";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 80;
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AllowMouseWheel = false;
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.Mask.UseMaskAsDisplayFormat = true;
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Од. вим.";
            this.gridColumn5.FieldName = "MsrName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 2;
            this.gridColumn5.Width = 69;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Ціна без знижки";
            this.gridColumn7.DisplayFormat.FormatString = "0.00";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "BasePrice";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 3;
            this.gridColumn7.Width = 105;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Знижка, %";
            this.gridColumn10.DisplayFormat.FormatString = "0.00";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn10.FieldName = "Discount";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.OptionsColumn.AllowEdit = false;
            this.gridColumn10.OptionsColumn.ReadOnly = true;
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            this.gridColumn10.Width = 95;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Ціна";
            this.gridColumn6.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn6.FieldName = "Price";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Width = 73;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Сума";
            this.gridColumn8.FieldName = "Total";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.ReadOnly = true;
            this.gridColumn8.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0:0.##}")});
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 6;
            this.gridColumn8.Width = 76;
            // 
            // frmCashboxRefund
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 517);
            this.Controls.Add(this.WaybillDetOutGridControl);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCashboxRefund";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Повернення товару";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCashboxRefund_FormClosed);
            this.Load += new System.EventHandler(this.frmCashboxCheckoutcs_Load);
            this.Shown += new System.EventHandler(this.frmCashboxCheckout_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.SumAllEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl7)).EndInit();
            this.panelControl7.ResumeLayout(false);
            this.panelControl7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PutCashlessSumEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PutCashSumEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetOutGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetInBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WBDetReInGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.CalcEdit SumAllEdit;
        private DevExpress.XtraEditors.CalcEdit PutCashSumEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CalcEdit PutCashlessSumEdit;
        public DevExpress.XtraEditors.SimpleButton PayBtn;
        private DevExpress.XtraGrid.GridControl WaybillDetOutGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView WBDetReInGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private System.Windows.Forms.BindingSource WaybillDetInBS;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
    }
}