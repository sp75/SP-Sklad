namespace SP_Sklad.ViewsForm
{
    partial class frmDocumentViews
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentViews));
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.DocumentGridControl = new DevExpress.XtraGrid.GridControl();
            this.KagentListSource = new DevExpress.Data.Linq.LinqInstantFeedbackSource();
            this.DocumentGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colWType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.GridImageList = new DevExpress.Utils.ImageCollection(this.components);
            this.colNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSummInCurr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ImageList = new DevExpress.Utils.ImageCollection(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageList)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 516);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1392, 52);
            this.BottomPanel.TabIndex = 18;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(1170, 10);
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
            this.simpleButton1.Location = new System.Drawing.Point(1280, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // DocumentGridControl
            // 
            this.DocumentGridControl.DataSource = this.KagentListSource;
            this.DocumentGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentGridControl.Location = new System.Drawing.Point(0, 24);
            this.DocumentGridControl.MainView = this.DocumentGridView;
            this.DocumentGridControl.Name = "DocumentGridControl";
            this.DocumentGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.DocumentGridControl.Size = new System.Drawing.Size(1392, 492);
            this.DocumentGridControl.TabIndex = 19;
            this.DocumentGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DocumentGridView});
            this.DocumentGridControl.Click += new System.EventHandler(this.DocumentGridControl_Click);
            // 
            // KagentListSource
            // 
            this.KagentListSource.AreSourceRowsThreadSafe = true;
            this.KagentListSource.DefaultSorting = "OnDate";
            this.KagentListSource.DesignTimeElementType = typeof(SP_Sklad.SkladData.v_KAgentDocs);
            this.KagentListSource.KeyExpression = "Id";
            this.KagentListSource.GetQueryable += new System.EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.KagentListSource_GetQueryable);
            // 
            // DocumentGridView
            // 
            this.DocumentGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.DocumentGridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.DocumentGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F);
            this.DocumentGridView.Appearance.Row.Options.UseFont = true;
            this.DocumentGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colWType,
            this.colNum,
            this.colOnDate,
            this.colName,
            this.colSummInCurr,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.DocumentGridView.GridControl = this.DocumentGridControl;
            this.DocumentGridView.Name = "DocumentGridView";
            this.DocumentGridView.OptionsBehavior.ReadOnly = true;
            this.DocumentGridView.OptionsView.ShowDetailButtons = false;
            this.DocumentGridView.OptionsView.ShowFooter = true;
            this.DocumentGridView.DoubleClick += new System.EventHandler(this.KontragentGroupGridView_DoubleClick);
            // 
            // colWType
            // 
            this.colWType.Caption = "Тип документа";
            this.colWType.ColumnEdit = this.repositoryItemImageComboBox1;
            this.colWType.FieldName = "WType";
            this.colWType.Name = "colWType";
            this.colWType.OptionsColumn.AllowEdit = false;
            this.colWType.Visible = true;
            this.colWType.VisibleIndex = 0;
            this.colWType.Width = 244;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Видаткова накладна", -1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Прибуткова накладна", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Прибутковий касовий ордер", 3, 11),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Видатковий касовий ордер", -3, 12),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Рахунок", 2, 14),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Додаткові витрати", -2, 13),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Акти списання товару", -5, 17),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Акти наданих послуг", 29, 15),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Продажі", -25, 6),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Повернення продаж", 25, 7)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.GridImageList;
            // 
            // GridImageList
            // 
            this.GridImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("GridImageList.ImageStream")));
            this.GridImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.waybill_in, "waybill_in", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.GridImageList.Images.SetKeyName(0, "waybill_in");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.waybill_out, "waybill_out", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.GridImageList.Images.SetKeyName(1, "waybill_out");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.wb_order_in, "wb_order_in", typeof(global::SP_Sklad.Properties.Resources), 2);
            this.GridImageList.Images.SetKeyName(2, "wb_order_in");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.wb_order_out, "wb_order_out", typeof(global::SP_Sklad.Properties.Resources), 3);
            this.GridImageList.Images.SetKeyName(3, "wb_order_out");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.wb_return_in, "wb_return_in", typeof(global::SP_Sklad.Properties.Resources), 4);
            this.GridImageList.Images.SetKeyName(4, "wb_return_in");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.wb_return_out, "wb_return_out", typeof(global::SP_Sklad.Properties.Resources), 5);
            this.GridImageList.Images.SetKeyName(5, "wb_return_out");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.purchase, "purchase", typeof(global::SP_Sklad.Properties.Resources), 6);
            this.GridImageList.Images.SetKeyName(6, "purchase");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.wb_return_sale, "wb_return_sale", typeof(global::SP_Sklad.Properties.Resources), 7);
            this.GridImageList.Images.SetKeyName(7, "wb_return_sale");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.credit_adjustmen, "credit_adjustmen", typeof(global::SP_Sklad.Properties.Resources), 8);
            this.GridImageList.Images.SetKeyName(8, "credit_adjustmen");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.debt_adjustment, "debt_adjustment", typeof(global::SP_Sklad.Properties.Resources), 9);
            this.GridImageList.Images.SetKeyName(9, "debt_adjustment");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.project_manager_2, "project_manager_2", typeof(global::SP_Sklad.Properties.Resources), 10);
            this.GridImageList.Images.SetKeyName(10, "project_manager_2");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.pay_doc_in, "pay_doc_in", typeof(global::SP_Sklad.Properties.Resources), 11);
            this.GridImageList.Images.SetKeyName(11, "pay_doc_in");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.pay_doc_out, "pay_doc_out", typeof(global::SP_Sklad.Properties.Resources), 12);
            this.GridImageList.Images.SetKeyName(12, "pay_doc_out");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.pay_doc_out_2, "pay_doc_out_2", typeof(global::SP_Sklad.Properties.Resources), 13);
            this.GridImageList.Images.SetKeyName(13, "pay_doc_out_2");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.invoice, "invoice", typeof(global::SP_Sklad.Properties.Resources), 14);
            this.GridImageList.Images.SetKeyName(14, "invoice");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.act_services_provider, "act_services_provider", typeof(global::SP_Sklad.Properties.Resources), 15);
            this.GridImageList.Images.SetKeyName(15, "act_services_provider");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.move_stock, "move_stock", typeof(global::SP_Sklad.Properties.Resources), 16);
            this.GridImageList.Images.SetKeyName(16, "move_stock");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.WBWriteOff_2, "WBWriteOff_2", typeof(global::SP_Sklad.Properties.Resources), 17);
            this.GridImageList.Images.SetKeyName(17, "WBWriteOff_2");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.WBWriteOn_4, "WBWriteOn_4", typeof(global::SP_Sklad.Properties.Resources), 18);
            this.GridImageList.Images.SetKeyName(18, "WBWriteOn_4");
            // 
            // colNum
            // 
            this.colNum.Caption = "№";
            this.colNum.FieldName = "Num";
            this.colNum.Name = "colNum";
            this.colNum.Visible = true;
            this.colNum.VisibleIndex = 2;
            this.colNum.Width = 153;
            // 
            // colOnDate
            // 
            this.colOnDate.Caption = "Дата";
            this.colOnDate.FieldName = "OnDate";
            this.colOnDate.Name = "colOnDate";
            this.colOnDate.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.Date;
            this.colOnDate.Visible = true;
            this.colOnDate.VisibleIndex = 3;
            this.colOnDate.Width = 186;
            // 
            // colName
            // 
            this.colName.Caption = "Контрагент";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 4;
            this.colName.Width = 309;
            // 
            // colSummInCurr
            // 
            this.colSummInCurr.Caption = "Сума документа";
            this.colSummInCurr.DisplayFormat.FormatString = "0.00";
            this.colSummInCurr.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSummInCurr.FieldName = "SummInCurr";
            this.colSummInCurr.Name = "colSummInCurr";
            this.colSummInCurr.OptionsColumn.AllowEdit = false;
            this.colSummInCurr.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SummInCurr", "{0:0.00}")});
            this.colSummInCurr.Visible = true;
            this.colSummInCurr.VisibleIndex = 5;
            this.colSummInCurr.Width = 133;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Тип оплати";
            this.gridColumn1.FieldName = "PayTypeName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 6;
            this.gridColumn1.Width = 143;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.FieldName = "DocShortName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ShowCaption = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 70;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Сума операції";
            this.gridColumn3.FieldName = "TurnoverSumm";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TurnoverSumm", "{0:0.##}")});
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 7;
            this.gridColumn3.Width = 129;
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.ImageList;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem5,
            this.barButtonItem3});
            this.barManager1.MaxItemId = 8;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3, true)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Звіт по контрагенту";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ImageOptions.ImageIndex = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Переглянути  друковану форму локумента";
            this.barButtonItem2.Id = 5;
            this.barButtonItem2.ImageOptions.ImageIndex = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "Перейти до документа";
            this.barButtonItem5.Id = 6;
            this.barButtonItem5.ImageOptions.ImageIndex = 2;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Обновити";
            this.barButtonItem3.Id = 7;
            this.barButtonItem3.ImageOptions.ImageIndex = 3;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1392, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 568);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1392, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 544);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1392, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 544);
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.InsertImage(global::SP_Sklad.Properties.Resources.xls_export, "xls_export", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.ImageList.Images.SetKeyName(0, "xls_export");
            this.ImageList.InsertImage(global::SP_Sklad.Properties.Resources.preview_2, "preview_2", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.ImageList.Images.SetKeyName(1, "preview_2");
            this.ImageList.InsertImage(global::SP_Sklad.Properties.Resources.walking_16x16, "walking_16x16", typeof(global::SP_Sklad.Properties.Resources), 2);
            this.ImageList.Images.SetKeyName(2, "walking_16x16");
            this.ImageList.InsertImage(global::SP_Sklad.Properties.Resources.refresh_office, "refresh_office", typeof(global::SP_Sklad.Properties.Resources), 3);
            this.ImageList.Images.SetKeyName(3, "refresh_office");
            // 
            // bar1
            // 
            this.bar1.BarName = "Main menu";
            this.bar1.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3, true)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Main menu";
            // 
            // frmDocumentViews
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1392, 568);
            this.Controls.Add(this.DocumentGridControl);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = global::SP_Sklad.Properties.Resources.user_documens;
            this.Name = "frmDocumentViews";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Документи";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKaGroup_FormClosed);
            this.Load += new System.EventHandler(this.frmKaGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DocumentGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl DocumentGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView DocumentGridView;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.Data.Linq.LinqInstantFeedbackSource KagentListSource;
        private DevExpress.XtraGrid.Columns.GridColumn colWType;
        private DevExpress.XtraGrid.Columns.GridColumn colNum;
        private DevExpress.XtraGrid.Columns.GridColumn colOnDate;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colSummInCurr;
        private DevExpress.Utils.ImageCollection GridImageList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.Utils.ImageCollection ImageList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}