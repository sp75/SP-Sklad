namespace SP_Sklad.TradeForm
{
    partial class frmSalesList
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
            this.GetTradeWayBillListBS = new System.Windows.Forms.BindingSource(this.components);
            this.WBGridControl = new DevExpress.XtraGrid.GridControl();
            this.WbGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WbSummPayGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WbBalansGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.CheckedItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GetTradeWayBillListBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WBGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WbGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckedItemImageComboBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 494);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(967, 52);
            this.BottomPanel.TabIndex = 19;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(745, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вибрати";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(855, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // GetTradeWayBillListBS
            // 
            this.GetTradeWayBillListBS.DataSource = typeof(SP_Sklad.SkladData.GetRetailWayBillList_Result);
            // 
            // WBGridControl
            // 
            this.WBGridControl.DataSource = this.GetTradeWayBillListBS;
            this.WBGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WBGridControl.Location = new System.Drawing.Point(0, 0);
            this.WBGridControl.MainView = this.WbGridView;
            this.WBGridControl.Name = "WBGridControl";
            this.WBGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.CheckedItemImageComboBox});
            this.WBGridControl.Size = new System.Drawing.Size(967, 494);
            this.WBGridControl.TabIndex = 20;
            this.WBGridControl.UseEmbeddedNavigator = true;
            this.WBGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.WbGridView});
            // 
            // WbGridView
            // 
            this.WbGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.WbGridView.Appearance.Row.Options.UseFont = true;
            this.WbGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.WbSummPayGridColumn,
            this.WbBalansGridColumn,
            this.gridColumn44,
            this.gridColumn38,
            this.gridColumn1});
            this.WbGridView.GridControl = this.WBGridControl;
            this.WbGridView.Name = "WbGridView";
            this.WbGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.WbGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.WbGridView.OptionsBehavior.ReadOnly = true;
            this.WbGridView.OptionsFind.AlwaysVisible = true;
            this.WbGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.WbGridView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "№";
            this.gridColumn3.FieldName = "Num";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 0;
            this.gridColumn3.Width = 109;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Дата";
            this.gridColumn4.DisplayFormat.FormatString = "g";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "OnDate";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 1;
            this.gridColumn4.Width = 177;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Торгова точка";
            this.gridColumn5.FieldName = "KaName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 196;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Сума";
            this.gridColumn6.FieldName = "SummAll";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 4;
            this.gridColumn6.Width = 76;
            // 
            // WbSummPayGridColumn
            // 
            this.WbSummPayGridColumn.Caption = "Оплачено";
            this.WbSummPayGridColumn.FieldName = "SummPay";
            this.WbSummPayGridColumn.Name = "WbSummPayGridColumn";
            this.WbSummPayGridColumn.Visible = true;
            this.WbSummPayGridColumn.VisibleIndex = 5;
            this.WbSummPayGridColumn.Width = 89;
            // 
            // WbBalansGridColumn
            // 
            this.WbBalansGridColumn.Caption = "Баланс";
            this.WbBalansGridColumn.FieldName = "Balans";
            this.WbBalansGridColumn.Name = "WbBalansGridColumn";
            this.WbBalansGridColumn.Width = 72;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "Сума у нац. валюті";
            this.gridColumn44.FieldName = "SummInCurr";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.Width = 83;
            // 
            // gridColumn38
            // 
            this.gridColumn38.Caption = "Покупець";
            this.gridColumn38.FieldName = "CustomerName";
            this.gridColumn38.Name = "gridColumn38";
            this.gridColumn38.Visible = true;
            this.gridColumn38.VisibleIndex = 2;
            this.gridColumn38.Width = 167;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 25, 8),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -25, 44)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            // 
            // CheckedItemImageComboBox
            // 
            this.CheckedItemImageComboBox.AutoHeight = false;
            this.CheckedItemImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CheckedItemImageComboBox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 24),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 22),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 25)});
            this.CheckedItemImageComboBox.Name = "CheckedItemImageComboBox";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Фіксальний чек";
            this.gridColumn1.FieldName = "FiscalCode";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            this.gridColumn1.Width = 135;
            // 
            // frmSalesList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 546);
            this.Controls.Add(this.WBGridControl);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmSalesList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Відкладені чеки";
            this.Load += new System.EventHandler(this.frmDeferredCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GetTradeWayBillListBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WBGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WbGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckedItemImageComboBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.BindingSource GetTradeWayBillListBS;
        public DevExpress.XtraGrid.GridControl WBGridControl;
        public DevExpress.XtraGrid.Views.Grid.GridView WbGridView;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        public DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox CheckedItemImageComboBox;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn WbSummPayGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn WbBalansGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}