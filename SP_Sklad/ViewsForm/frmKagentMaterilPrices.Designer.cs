namespace SP_Sklad.ViewsForm
{
    partial class frmKagentMaterilPrices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKagentMaterilPrices));
            this.KagentMaterilPricesGrid = new DevExpress.XtraGrid.GridControl();
            this.KagentMaterilPricesSource = new DevExpress.Data.Linq.LinqInstantFeedbackSource();
            this.KagentMaterilPricesGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMatId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMatName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKaId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKaName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.KagentMaterilPricesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KagentMaterilPricesGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // KagentMaterilPricesGrid
            // 
            this.KagentMaterilPricesGrid.DataSource = this.KagentMaterilPricesSource;
            this.KagentMaterilPricesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KagentMaterilPricesGrid.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.KagentMaterilPricesGrid.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.KagentMaterilPricesGrid.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.KagentMaterilPricesGrid.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.KagentMaterilPricesGrid.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.KagentMaterilPricesGrid.Location = new System.Drawing.Point(0, 0);
            this.KagentMaterilPricesGrid.MainView = this.KagentMaterilPricesGridView;
            this.KagentMaterilPricesGrid.Name = "KagentMaterilPricesGrid";
            this.KagentMaterilPricesGrid.Size = new System.Drawing.Size(985, 442);
            this.KagentMaterilPricesGrid.TabIndex = 19;
            this.KagentMaterilPricesGrid.UseEmbeddedNavigator = true;
            this.KagentMaterilPricesGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.KagentMaterilPricesGridView});
            // 
            // KagentMaterilPricesSource
            // 
            this.KagentMaterilPricesSource.AreSourceRowsThreadSafe = true;
            this.KagentMaterilPricesSource.DesignTimeElementType = typeof(SP_Sklad.SkladData.v_KagentMaterilPrices);
            this.KagentMaterilPricesSource.KeyExpression = "MatId;KaId";
            this.KagentMaterilPricesSource.GetQueryable += new System.EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.KagentMaterilPricesSource_GetQueryable);
            // 
            // KagentMaterilPricesGridView
            // 
            this.KagentMaterilPricesGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMatId,
            this.colMatName,
            this.colKaId,
            this.colKaName,
            this.colPrice,
            this.colDiscount,
            this.gridColumn1,
            this.gridColumn2});
            this.KagentMaterilPricesGridView.GridControl = this.KagentMaterilPricesGrid;
            this.KagentMaterilPricesGridView.GroupCount = 1;
            this.KagentMaterilPricesGridView.Name = "KagentMaterilPricesGridView";
            this.KagentMaterilPricesGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.KagentMaterilPricesGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.KagentMaterilPricesGridView.OptionsFind.AlwaysVisible = true;
            this.KagentMaterilPricesGridView.OptionsView.ShowFooter = true;
            this.KagentMaterilPricesGridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colMatName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.KagentMaterilPricesGridView.AsyncCompleted += new System.EventHandler(this.KagentMaterilPricesGridView_AsyncCompleted);
            // 
            // colMatId
            // 
            this.colMatId.Caption = "Код товару";
            this.colMatId.FieldName = "MatId";
            this.colMatId.Name = "colMatId";
            this.colMatId.Width = 72;
            // 
            // colMatName
            // 
            this.colMatName.Caption = "Назва товару";
            this.colMatName.FieldName = "MatName";
            this.colMatName.Name = "colMatName";
            this.colMatName.Visible = true;
            this.colMatName.VisibleIndex = 1;
            this.colMatName.Width = 254;
            // 
            // colKaId
            // 
            this.colKaId.Caption = "Код контрагента";
            this.colKaId.FieldName = "KaId";
            this.colKaId.Name = "colKaId";
            this.colKaId.Width = 105;
            // 
            // colKaName
            // 
            this.colKaName.Caption = "Контрагент";
            this.colKaName.FieldName = "KaName";
            this.colKaName.Name = "colKaName";
            this.colKaName.Visible = true;
            this.colKaName.VisibleIndex = 1;
            this.colKaName.Width = 387;
            // 
            // colPrice
            // 
            this.colPrice.Caption = "Ціна";
            this.colPrice.DisplayFormat.FormatString = "0.00";
            this.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 3;
            this.colPrice.Width = 88;
            // 
            // colDiscount
            // 
            this.colDiscount.Caption = "Знижка, %";
            this.colDiscount.DisplayFormat.FormatString = "0.00";
            this.colDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDiscount.FieldName = "Discount";
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.Visible = true;
            this.colDiscount.VisibleIndex = 4;
            this.colDiscount.Width = 99;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Група товарів";
            this.gridColumn1.FieldName = "MatGrpName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 203;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Група контрагентів";
            this.gridColumn2.FieldName = "KaGrpName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 183;
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("OkButton.ImageOptions.Image")));
            this.OkButton.Location = new System.Drawing.Point(12, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(96, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Експорт";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 442);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(985, 52);
            this.BottomPanel.TabIndex = 18;
            // 
            // frmKagentMaterilPrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 494);
            this.Controls.Add(this.KagentMaterilPricesGrid);
            this.Controls.Add(this.BottomPanel);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("frmKagentMaterilPrices.IconOptions.Image")));
            this.Name = "frmKagentMaterilPrices";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Актуальні ціни на товари по контрагентам";
            ((System.ComponentModel.ISupportInitialize)(this.KagentMaterilPricesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KagentMaterilPricesGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl KagentMaterilPricesGrid;
        public DevExpress.XtraGrid.Views.Grid.GridView KagentMaterilPricesGridView;
        private DevExpress.Data.Linq.LinqInstantFeedbackSource KagentMaterilPricesSource;
        private DevExpress.XtraGrid.Columns.GridColumn colMatId;
        private DevExpress.XtraGrid.Columns.GridColumn colMatName;
        private DevExpress.XtraGrid.Columns.GridColumn colKaId;
        private DevExpress.XtraGrid.Columns.GridColumn colKaName;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colDiscount;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.PanelControl BottomPanel;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}