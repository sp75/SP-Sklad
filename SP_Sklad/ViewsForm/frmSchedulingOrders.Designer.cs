namespace SP_Sklad.ViewsForm
{
    partial class frmSchedulingOrders
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
            this.SchedulingGridControl = new DevExpress.XtraGrid.GridControl();
            this.SchedulingOrdersBS = new System.Windows.Forms.BindingSource(this.components);
            this.SchedulingGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRecId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.SchedulingGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SchedulingOrdersBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SchedulingGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SchedulingGridControl
            // 
            this.SchedulingGridControl.DataSource = this.SchedulingOrdersBS;
            this.SchedulingGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SchedulingGridControl.Location = new System.Drawing.Point(0, 0);
            this.SchedulingGridControl.MainView = this.SchedulingGridView;
            this.SchedulingGridControl.Name = "SchedulingGridControl";
            this.SchedulingGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1});
            this.SchedulingGridControl.Size = new System.Drawing.Size(1063, 499);
            this.SchedulingGridControl.TabIndex = 1;
            this.SchedulingGridControl.UseEmbeddedNavigator = true;
            this.SchedulingGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.SchedulingGridView});
            // 
            // SchedulingOrdersBS
            // 
            this.SchedulingOrdersBS.DataSource = typeof(SP_Sklad.SkladData.SchedulingOrders);
            // 
            // SchedulingGridView
            // 
            this.SchedulingGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.SchedulingGridView.Appearance.Row.Options.UseFont = true;
            this.SchedulingGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRecId,
            this.colAmount});
            this.SchedulingGridView.GridControl = this.SchedulingGridControl;
            this.SchedulingGridView.Name = "SchedulingGridView";
            this.SchedulingGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.SchedulingGridView.OptionsNavigation.AutoFocusNewRow = true;
            this.SchedulingGridView.OptionsView.ShowGroupPanel = false;
            this.SchedulingGridView.RowDeleted += new DevExpress.Data.RowDeletedEventHandler(this.SchedulingGridView_RowDeleted);
            this.SchedulingGridView.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.SchedulingGridView_RowUpdated);
            // 
            // colRecId
            // 
            this.colRecId.Caption = "Рецепт";
            this.colRecId.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colRecId.FieldName = "RecId";
            this.colRecId.Name = "colRecId";
            this.colRecId.Visible = true;
            this.colRecId.VisibleIndex = 0;
            this.colRecId.Width = 863;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MatName", "Name1")});
            this.repositoryItemLookUpEdit1.DisplayMember = "MatName";
            this.repositoryItemLookUpEdit1.ImmediatePopup = true;
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.ShowFooter = false;
            this.repositoryItemLookUpEdit1.ShowHeader = false;
            this.repositoryItemLookUpEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.repositoryItemLookUpEdit1.ValueMember = "RecId";
            // 
            // colAmount
            // 
            this.colAmount.Caption = "Замовлено";
            this.colAmount.FieldName = "Amount";
            this.colAmount.Name = "colAmount";
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 1;
            this.colAmount.Width = 182;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 499);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1063, 52);
            this.BottomPanel.TabIndex = 16;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(951, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Ок";
            // 
            // frmSchedulingOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 551);
            this.Controls.Add(this.SchedulingGridControl);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmSchedulingOrders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Планування замовлень від кліентів";
            this.Load += new System.EventHandler(this.frmSchedulingOrders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SchedulingGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SchedulingOrdersBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SchedulingGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl SchedulingGridControl;
        public DevExpress.XtraGrid.Views.Grid.GridView SchedulingGridView;
        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private System.Windows.Forms.BindingSource SchedulingOrdersBS;
        private DevExpress.XtraGrid.Columns.GridColumn colRecId;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
    }
}