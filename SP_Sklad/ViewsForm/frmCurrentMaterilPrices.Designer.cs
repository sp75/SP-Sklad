namespace SP_Sklad.ViewsForm
{
    partial class frmCurrentMaterilPrices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCurrentMaterilPrices));
            this.MaterilPricesGrid = new DevExpress.XtraGrid.GridControl();
            this.KagentMaterilPricesSource = new DevExpress.Data.Linq.LinqInstantFeedbackSource();
            this.MaterilPricesGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMatId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMatName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.RefrechItemBtn = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.BarCodeBtnEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl2 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl3 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl4 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl5 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl7 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.BarImageList = new DevExpress.Utils.ImageCollection(this.components);
            this.NewItemBtn = new DevExpress.XtraBars.BarButtonItem();
            this.CopyItemBtn = new DevExpress.XtraBars.BarButtonItem();
            this.EditItemBtn = new DevExpress.XtraBars.BarButtonItem();
            this.DeleteItemBtn = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem12 = new DevExpress.XtraBars.BarButtonItem();
            this.SelectAllBtn = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.MaterilPricesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaterilPricesGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarCodeBtnEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarImageList)).BeginInit();
            this.SuspendLayout();
            // 
            // MaterilPricesGrid
            // 
            this.MaterilPricesGrid.DataSource = this.KagentMaterilPricesSource;
            this.MaterilPricesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MaterilPricesGrid.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.MaterilPricesGrid.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.MaterilPricesGrid.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.MaterilPricesGrid.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.MaterilPricesGrid.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.MaterilPricesGrid.Location = new System.Drawing.Point(0, 24);
            this.MaterilPricesGrid.MainView = this.MaterilPricesGridView;
            this.MaterilPricesGrid.Name = "MaterilPricesGrid";
            this.MaterilPricesGrid.Size = new System.Drawing.Size(1331, 493);
            this.MaterilPricesGrid.TabIndex = 19;
            this.MaterilPricesGrid.UseEmbeddedNavigator = true;
            this.MaterilPricesGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.MaterilPricesGridView});
            // 
            // KagentMaterilPricesSource
            // 
            this.KagentMaterilPricesSource.AreSourceRowsThreadSafe = true;
            this.KagentMaterilPricesSource.DesignTimeElementType = typeof(SP_Sklad.SkladData.v_MaterilPrices);
            this.KagentMaterilPricesSource.KeyExpression = "MatId;PTypeId";
            this.KagentMaterilPricesSource.GetQueryable += new System.EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.KagentMaterilPricesSource_GetQueryable);
            // 
            // MaterilPricesGridView
            // 
            this.MaterilPricesGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMatId,
            this.colMatName,
            this.colPrice,
            this.gridColumn1,
            this.colPriceTypeName,
            this.gridColumn3});
            this.MaterilPricesGridView.GridControl = this.MaterilPricesGrid;
            this.MaterilPricesGridView.Name = "MaterilPricesGridView";
            this.MaterilPricesGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.MaterilPricesGridView.OptionsBehavior.AutoExpandAllGroups = true;
            this.MaterilPricesGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.MaterilPricesGridView.OptionsView.ShowFooter = true;
            this.MaterilPricesGridView.AsyncCompleted += new System.EventHandler(this.KagentMaterilPricesGridView_AsyncCompleted);
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
            this.colMatName.VisibleIndex = 0;
            this.colMatName.Width = 196;
            // 
            // colPrice
            // 
            this.colPrice.Caption = "Ціна";
            this.colPrice.DisplayFormat.FormatString = "0.00";
            this.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 4;
            this.colPrice.Width = 72;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Група товарів";
            this.gridColumn1.FieldName = "MatGrpName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 170;
            // 
            // colPriceTypeName
            // 
            this.colPriceTypeName.Caption = "Цінова катерогія";
            this.colPriceTypeName.FieldName = "PriceTypeName";
            this.colPriceTypeName.Name = "colPriceTypeName";
            this.colPriceTypeName.Visible = true;
            this.colPriceTypeName.VisibleIndex = 3;
            this.colPriceTypeName.Width = 152;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Артикул";
            this.gridColumn3.FieldName = "Artikul";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl2);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl3);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl4);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl5);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl7);
            this.barManager1.Form = this;
            this.barManager1.Images = this.BarImageList;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.NewItemBtn,
            this.CopyItemBtn,
            this.EditItemBtn,
            this.DeleteItemBtn,
            this.RefrechItemBtn,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barButtonItem12,
            this.barButtonItem11,
            this.barEditItem2,
            this.SelectAllBtn});
            this.barManager1.MaxItemId = 57;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.BarCodeBtnEdit});
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 3";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(144, 121);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.RefrechItemBtn),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem11, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barEditItem2)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 3";
            // 
            // RefrechItemBtn
            // 
            this.RefrechItemBtn.Caption = "Обновити";
            this.RefrechItemBtn.Id = 4;
            this.RefrechItemBtn.ImageOptions.ImageIndex = 0;
            this.RefrechItemBtn.Name = "RefrechItemBtn";
            this.RefrechItemBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RefrechItemBtn_ItemClick);
            // 
            // barButtonItem11
            // 
            this.barButtonItem11.Caption = "Експорт";
            this.barButtonItem11.Id = 38;
            this.barButtonItem11.ImageOptions.ImageIndex = 1;
            this.barButtonItem11.Name = "barButtonItem11";
            this.barButtonItem11.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem11_ItemClick);
            // 
            // barEditItem2
            // 
            this.barEditItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barEditItem2.Caption = "Штрих-код";
            this.barEditItem2.Edit = this.BarCodeBtnEdit;
            this.barEditItem2.Id = 51;
            this.barEditItem2.Name = "barEditItem2";
            this.barEditItem2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barEditItem2.Size = new System.Drawing.Size(250, 0);
            // 
            // BarCodeBtnEdit
            // 
            this.BarCodeBtnEdit.AutoHeight = false;
            this.BarCodeBtnEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Search)});
            this.BarCodeBtnEdit.Name = "BarCodeBtnEdit";
            this.BarCodeBtnEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.BarCodeBtnEdit_ButtonClick);
            this.BarCodeBtnEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BarCodeBtnEdit_KeyDown);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1331, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 517);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1331, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 493);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1331, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 493);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.AutoSize = true;
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 24);
            this.standaloneBarDockControl1.Manager = this.barManager1;
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(1331, 0);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // standaloneBarDockControl2
            // 
            this.standaloneBarDockControl2.AutoSize = true;
            this.standaloneBarDockControl2.CausesValidation = false;
            this.standaloneBarDockControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl2.Location = new System.Drawing.Point(0, 24);
            this.standaloneBarDockControl2.Manager = this.barManager1;
            this.standaloneBarDockControl2.Name = "standaloneBarDockControl2";
            this.standaloneBarDockControl2.Size = new System.Drawing.Size(1331, 0);
            this.standaloneBarDockControl2.Text = "standaloneBarDockControl2";
            // 
            // standaloneBarDockControl3
            // 
            this.standaloneBarDockControl3.AutoSize = true;
            this.standaloneBarDockControl3.CausesValidation = false;
            this.standaloneBarDockControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl3.Location = new System.Drawing.Point(0, 24);
            this.standaloneBarDockControl3.Manager = this.barManager1;
            this.standaloneBarDockControl3.Name = "standaloneBarDockControl3";
            this.standaloneBarDockControl3.Size = new System.Drawing.Size(1331, 0);
            this.standaloneBarDockControl3.Text = "standaloneBarDockControl3";
            // 
            // standaloneBarDockControl4
            // 
            this.standaloneBarDockControl4.AutoSize = true;
            this.standaloneBarDockControl4.CausesValidation = false;
            this.standaloneBarDockControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl4.Location = new System.Drawing.Point(0, 24);
            this.standaloneBarDockControl4.Manager = this.barManager1;
            this.standaloneBarDockControl4.Name = "standaloneBarDockControl4";
            this.standaloneBarDockControl4.Size = new System.Drawing.Size(1331, 0);
            this.standaloneBarDockControl4.Text = "standaloneBarDockControl4";
            // 
            // standaloneBarDockControl5
            // 
            this.standaloneBarDockControl5.AutoSize = true;
            this.standaloneBarDockControl5.CausesValidation = false;
            this.standaloneBarDockControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl5.Location = new System.Drawing.Point(0, 24);
            this.standaloneBarDockControl5.Manager = this.barManager1;
            this.standaloneBarDockControl5.Name = "standaloneBarDockControl5";
            this.standaloneBarDockControl5.Size = new System.Drawing.Size(1331, 0);
            this.standaloneBarDockControl5.Text = "standaloneBarDockControl5";
            // 
            // standaloneBarDockControl7
            // 
            this.standaloneBarDockControl7.AutoSize = true;
            this.standaloneBarDockControl7.CausesValidation = false;
            this.standaloneBarDockControl7.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl7.Location = new System.Drawing.Point(0, 24);
            this.standaloneBarDockControl7.Manager = this.barManager1;
            this.standaloneBarDockControl7.Name = "standaloneBarDockControl7";
            this.standaloneBarDockControl7.Size = new System.Drawing.Size(1331, 0);
            this.standaloneBarDockControl7.Text = "standaloneBarDockControl7";
            // 
            // BarImageList
            // 
            this.BarImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("BarImageList.ImageStream")));
            this.BarImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.BarImageList.InsertImage(global::SP_Sklad.Properties.Resources.refresh_office, "refresh_office", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.BarImageList.Images.SetKeyName(0, "refresh_office");
            this.BarImageList.InsertImage(global::SP_Sklad.Properties.Resources.xls_export, "xls_export", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.BarImageList.Images.SetKeyName(1, "xls_export");
            // 
            // NewItemBtn
            // 
            this.NewItemBtn.Caption = "Додати";
            this.NewItemBtn.Id = 0;
            this.NewItemBtn.ImageOptions.ImageIndex = 19;
            this.NewItemBtn.Name = "NewItemBtn";
            this.NewItemBtn.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // CopyItemBtn
            // 
            this.CopyItemBtn.Caption = "Додати на підставі";
            this.CopyItemBtn.Id = 1;
            this.CopyItemBtn.ImageOptions.ImageIndex = 27;
            this.CopyItemBtn.Name = "CopyItemBtn";
            // 
            // EditItemBtn
            // 
            this.EditItemBtn.Caption = "Властивості";
            this.EditItemBtn.Id = 2;
            this.EditItemBtn.ImageOptions.ImageIndex = 26;
            this.EditItemBtn.Name = "EditItemBtn";
            // 
            // DeleteItemBtn
            // 
            this.DeleteItemBtn.Caption = "Видалити";
            this.DeleteItemBtn.Id = 3;
            this.DeleteItemBtn.ImageOptions.ImageIndex = 16;
            this.DeleteItemBtn.Name = "DeleteItemBtn";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Інформація про рух товару";
            this.barButtonItem4.Id = 30;
            this.barButtonItem4.ImageOptions.ImageIndex = 6;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Інформація про резерв товару";
            this.barButtonItem5.Id = 31;
            this.barButtonItem5.ImageOptions.ImageIndex = 11;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem12
            // 
            this.barButtonItem12.Caption = "Знайти на складі";
            this.barButtonItem12.Id = 32;
            this.barButtonItem12.ImageOptions.ImageIndex = 22;
            this.barButtonItem12.Name = "barButtonItem12";
            // 
            // SelectAllBtn
            // 
            this.SelectAllBtn.Caption = "Виділити всі рядки";
            this.SelectAllBtn.Id = 54;
            this.SelectAllBtn.ImageOptions.ImageIndex = 31;
            this.SelectAllBtn.Name = "SelectAllBtn";
            // 
            // frmCurrentMaterilPrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 517);
            this.Controls.Add(this.standaloneBarDockControl1);
            this.Controls.Add(this.standaloneBarDockControl2);
            this.Controls.Add(this.standaloneBarDockControl3);
            this.Controls.Add(this.standaloneBarDockControl4);
            this.Controls.Add(this.standaloneBarDockControl5);
            this.Controls.Add(this.standaloneBarDockControl7);
            this.Controls.Add(this.MaterilPricesGrid);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = global::SP_Sklad.Properties.Resources.pricing;
            this.Name = "frmCurrentMaterilPrices";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Актуальні ціни на товари по ціновим категоріям";
            this.Shown += new System.EventHandler(this.frmKagentMaterilPrices_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.MaterilPricesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaterilPricesGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarCodeBtnEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarImageList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.GridControl MaterilPricesGrid;
        public DevExpress.XtraGrid.Views.Grid.GridView MaterilPricesGridView;
        private DevExpress.Data.Linq.LinqInstantFeedbackSource KagentMaterilPricesSource;
        private DevExpress.XtraGrid.Columns.GridColumn colMatId;
        private DevExpress.XtraGrid.Columns.GridColumn colMatName;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceTypeName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem NewItemBtn;
        private DevExpress.XtraBars.BarButtonItem CopyItemBtn;
        private DevExpress.XtraBars.BarButtonItem EditItemBtn;
        private DevExpress.XtraBars.BarButtonItem DeleteItemBtn;
        private DevExpress.XtraBars.BarButtonItem RefrechItemBtn;
        private DevExpress.XtraBars.BarButtonItem barButtonItem11;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit BarCodeBtnEdit;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl7;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl2;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl3;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl4;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem12;
        private DevExpress.XtraBars.BarButtonItem SelectAllBtn;
        private DevExpress.Utils.ImageCollection BarImageList;
    }
}