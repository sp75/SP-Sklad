namespace SP_Sklad.UserControls
{
    partial class ucDocumentDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDocumentDetails));
            this.WaybillDetGridControl = new DevExpress.XtraGrid.GridControl();
            this.WayBillIDetSource = new DevExpress.Data.Linq.LinqInstantFeedbackSource();
            this.WaybillDetGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WbSummPayGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WbBalansGridColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPersonName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CheckedItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.repositoryItemImageComboBox42 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.StartDateEditItem = new DevExpress.XtraBars.BarEditItem();
            this.StartDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.EndDateEditItem = new DevExpress.XtraBars.BarEditItem();
            this.EndDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.RefrechItemBtn = new DevExpress.XtraBars.BarButtonItem();
            this.ExportToExcelBtn = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.EditItemBtn = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.WbListPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckedItemImageComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox42)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateEdit.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDateEdit.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WbListPopupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // WaybillDetGridControl
            // 
            this.WaybillDetGridControl.DataSource = this.WayBillIDetSource;
            this.WaybillDetGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WaybillDetGridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.WaybillDetGridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.WaybillDetGridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.WaybillDetGridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.WaybillDetGridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.WaybillDetGridControl.Location = new System.Drawing.Point(0, 24);
            this.WaybillDetGridControl.MainView = this.WaybillDetGridView;
            this.WaybillDetGridControl.Name = "WaybillDetGridControl";
            this.WaybillDetGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.CheckedItemImageComboBox,
            this.repositoryItemImageComboBox42});
            this.WaybillDetGridControl.Size = new System.Drawing.Size(1375, 671);
            this.WaybillDetGridControl.TabIndex = 0;
            this.WaybillDetGridControl.UseEmbeddedNavigator = true;
            this.WaybillDetGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.WaybillDetGridView});
            // 
            // WayBillIDetSource
            // 
            this.WayBillIDetSource.AreSourceRowsThreadSafe = true;
            this.WayBillIDetSource.DefaultSorting = "WbOnDate DESC";
            this.WayBillIDetSource.DesignTimeElementType = typeof(SP_Sklad.SkladData.v_WaybillDet);
            this.WayBillIDetSource.KeyExpression = "PosId";
            this.WayBillIDetSource.GetQueryable += new System.EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.WayBillInSource_GetQueryable);
            this.WayBillIDetSource.DismissQueryable += new System.EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.WayBillInSource_DismissQueryable);
            // 
            // WaybillDetGridView
            // 
            this.WaybillDetGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.WaybillDetGridView.Appearance.Row.Options.UseFont = true;
            this.WaybillDetGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.WbSummPayGridColumn,
            this.WbBalansGridColumn,
            this.gridColumn7,
            this.gridColumn40,
            this.gridColumn42,
            this.colPersonName,
            this.gridColumn2,
            this.gridColumn8,
            this.gridColumn9,
            this.gridColumn10});
            this.WaybillDetGridView.GridControl = this.WaybillDetGridControl;
            this.WaybillDetGridView.Name = "WaybillDetGridView";
            this.WaybillDetGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.WaybillDetGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.WaybillDetGridView.OptionsBehavior.ReadOnly = true;
            this.WaybillDetGridView.OptionsView.ShowFooter = true;
            this.WaybillDetGridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.WbGridView_PopupMenuShowing);
            this.WaybillDetGridView.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.WbGridView_FocusedRowObjectChanged);
            this.WaybillDetGridView.ColumnFilterChanged += new System.EventHandler(this.WbGridView_ColumnFilterChanged);
            this.WaybillDetGridView.AsyncCompleted += new System.EventHandler(this.WbGridView_AsyncCompleted);
            this.WaybillDetGridView.DoubleClick += new System.EventHandler(this.WbGridView_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Тип документа";
            this.gridColumn1.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gridColumn1.FieldName = "WType";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowSize = false;
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.OptionsColumn.ShowCaption = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 25;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Прибуткова накладна", 1, 9),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Видаткова накладна", -1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Повернення від клієнта", 6, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Повернення постачальнику", -6, 2)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.imageCollection1;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.waybill_out, "waybill_out", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.imageCollection1.Images.SetKeyName(0, "waybill_out");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.wb_return_in, "wb_return_in", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.imageCollection1.Images.SetKeyName(1, "wb_return_in");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.wb_return_out, "wb_return_out", typeof(global::SP_Sklad.Properties.Resources), 2);
            this.imageCollection1.Images.SetKeyName(2, "wb_return_out");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.copy_2, "copy_2", typeof(global::SP_Sklad.Properties.Resources), 3);
            this.imageCollection1.Images.SetKeyName(3, "copy_2");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.edit_2, "edit_2", typeof(global::SP_Sklad.Properties.Resources), 4);
            this.imageCollection1.Images.SetKeyName(4, "edit_2");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.delete_16x16, "delete_16x16", typeof(global::SP_Sklad.Properties.Resources), 5);
            this.imageCollection1.Images.SetKeyName(5, "delete_16x16");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.execute_16x16_red, "execute_16x16_red", typeof(global::SP_Sklad.Properties.Resources), 6);
            this.imageCollection1.Images.SetKeyName(6, "execute_16x16_red");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.preview_2, "preview_2", typeof(global::SP_Sklad.Properties.Resources), 7);
            this.imageCollection1.Images.SetKeyName(7, "preview_2");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.refresh_office, "refresh_office", typeof(global::SP_Sklad.Properties.Resources), 8);
            this.imageCollection1.Images.SetKeyName(8, "refresh_office");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.waybill_in, "waybill_in", typeof(global::SP_Sklad.Properties.Resources), 9);
            this.imageCollection1.Images.SetKeyName(9, "waybill_in");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.showproduct_16x16, "showproduct_16x16", typeof(global::SP_Sklad.Properties.Resources), 10);
            this.imageCollection1.Images.SetKeyName(10, "showproduct_16x16");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.wb_info, "wb_info", typeof(global::SP_Sklad.Properties.Resources), 11);
            this.imageCollection1.Images.SetKeyName(11, "wb_info");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.RelDoc3, "RelDoc3", typeof(global::SP_Sklad.Properties.Resources), 12);
            this.imageCollection1.Images.SetKeyName(12, "RelDoc3");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.pay_wb, "pay_wb", typeof(global::SP_Sklad.Properties.Resources), 13);
            this.imageCollection1.Images.SetKeyName(13, "pay_wb");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.WBWriteOff_2, "WBWriteOff_2", typeof(global::SP_Sklad.Properties.Resources), 14);
            this.imageCollection1.Images.SetKeyName(14, "WBWriteOff_2");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.pay_doc_out, "pay_doc_out", typeof(global::SP_Sklad.Properties.Resources), 15);
            this.imageCollection1.Images.SetKeyName(15, "pay_doc_out");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.def_settings_grid, "def_settings_grid", typeof(global::SP_Sklad.Properties.Resources), 16);
            this.imageCollection1.Images.SetKeyName(16, "def_settings_grid");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.historyitem_16x16, "historyitem_16x16", typeof(global::SP_Sklad.Properties.Resources), 17);
            this.imageCollection1.Images.SetKeyName(17, "historyitem_16x16");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.xls_export, "xls_export", typeof(global::SP_Sklad.Properties.Resources), 18);
            this.imageCollection1.Images.SetKeyName(18, "xls_export");
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "№";
            this.gridColumn3.FieldName = "WbNum";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 1;
            this.gridColumn3.Width = 86;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Дата";
            this.gridColumn4.DisplayFormat.FormatString = "g";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "WbOnDate";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.Date;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 2;
            this.gridColumn4.Width = 149;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Контрагент";
            this.gridColumn5.FieldName = "KaName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 164;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Всього";
            this.gridColumn6.FieldName = "Total";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total", "{0:0.##}")});
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 10;
            this.gridColumn6.Width = 134;
            // 
            // WbSummPayGridColumn
            // 
            this.WbSummPayGridColumn.Caption = "К-сть";
            this.WbSummPayGridColumn.FieldName = "Amount";
            this.WbSummPayGridColumn.Name = "WbSummPayGridColumn";
            this.WbSummPayGridColumn.Visible = true;
            this.WbSummPayGridColumn.VisibleIndex = 8;
            this.WbSummPayGridColumn.Width = 112;
            // 
            // WbBalansGridColumn
            // 
            this.WbBalansGridColumn.Caption = "Ціна";
            this.WbBalansGridColumn.FieldName = "BasePrice";
            this.WbBalansGridColumn.Name = "WbBalansGridColumn";
            this.WbBalansGridColumn.Visible = true;
            this.WbBalansGridColumn.VisibleIndex = 9;
            this.WbBalansGridColumn.Width = 103;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Підприємство";
            this.gridColumn7.FieldName = "EntName";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Width = 86;
            // 
            // gridColumn40
            // 
            this.gridColumn40.Caption = "Примітка";
            this.gridColumn40.FieldName = "Notes";
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.Width = 113;
            // 
            // gridColumn42
            // 
            this.gridColumn42.Caption = "Група контрагентів";
            this.gridColumn42.FieldName = "KagentGroupName";
            this.gridColumn42.Name = "gridColumn42";
            this.gridColumn42.Visible = true;
            this.gridColumn42.VisibleIndex = 3;
            this.gridColumn42.Width = 142;
            // 
            // colPersonName
            // 
            this.colPersonName.Caption = "Виконавець";
            this.colPersonName.FieldName = "PersonName";
            this.colPersonName.Name = "colPersonName";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Товар";
            this.gridColumn2.FieldName = "MatName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 6;
            this.gridColumn2.Width = 96;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Од. вим.";
            this.gridColumn8.FieldName = "MsrName";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            this.gridColumn8.Width = 98;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "Склад";
            this.gridColumn9.FieldName = "WhName";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 11;
            this.gridColumn9.Width = 121;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "Група товарів";
            this.gridColumn10.FieldName = "GrpName";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            this.gridColumn10.Width = 120;
            // 
            // CheckedItemImageComboBox
            // 
            this.CheckedItemImageComboBox.AutoHeight = false;
            this.CheckedItemImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CheckedItemImageComboBox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1)});
            this.CheckedItemImageComboBox.Name = "CheckedItemImageComboBox";
            this.CheckedItemImageComboBox.SmallImages = this.imageCollection1;
            // 
            // repositoryItemImageComboBox42
            // 
            this.repositoryItemImageComboBox42.AutoHeight = false;
            this.repositoryItemImageComboBox42.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox42.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 48),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, -1)});
            this.repositoryItemImageComboBox42.Name = "repositoryItemImageComboBox42";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Images = this.imageCollection1;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.EditItemBtn,
            this.RefrechItemBtn,
            this.barButtonItem1,
            this.ExportToExcelBtn,
            this.StartDateEditItem,
            this.EndDateEditItem});
            this.barManager1.MaxItemId = 39;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.StartDateEdit,
            this.EndDateEdit});
            // 
            // bar1
            // 
            this.bar1.BarName = "Custom 1";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.StartDateEditItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.EndDateEditItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.RefrechItemBtn),
            new DevExpress.XtraBars.LinkPersistInfo(this.ExportToExcelBtn, true)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DrawDragBorder = false;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Custom 1";
            // 
            // StartDateEditItem
            // 
            this.StartDateEditItem.Caption = "Період";
            this.StartDateEditItem.Edit = this.StartDateEdit;
            this.StartDateEditItem.EditWidth = 120;
            this.StartDateEditItem.Id = 37;
            this.StartDateEditItem.Name = "StartDateEditItem";
            this.StartDateEditItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.Caption;
            // 
            // StartDateEdit
            // 
            this.StartDateEdit.AutoHeight = false;
            this.StartDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StartDateEdit.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StartDateEdit.Name = "StartDateEdit";
            this.StartDateEdit.EditValueChanged += new System.EventHandler(this.StartDateEdit_EditValueChanged);
            // 
            // EndDateEditItem
            // 
            this.EndDateEditItem.Caption = "-";
            this.EndDateEditItem.Edit = this.EndDateEdit;
            this.EndDateEditItem.EditWidth = 120;
            this.EndDateEditItem.Id = 38;
            this.EndDateEditItem.Name = "EndDateEditItem";
            this.EndDateEditItem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.Caption;
            // 
            // EndDateEdit
            // 
            this.EndDateEdit.AutoHeight = false;
            this.EndDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.EndDateEdit.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.EndDateEdit.Name = "EndDateEdit";
            this.EndDateEdit.EditValueChanged += new System.EventHandler(this.StartDateEdit_EditValueChanged);
            // 
            // RefrechItemBtn
            // 
            this.RefrechItemBtn.Caption = "Обновити";
            this.RefrechItemBtn.Id = 4;
            this.RefrechItemBtn.ImageOptions.ImageIndex = 8;
            this.RefrechItemBtn.Name = "RefrechItemBtn";
            this.RefrechItemBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.RefrechItemBtn_ItemClick);
            // 
            // ExportToExcelBtn
            // 
            this.ExportToExcelBtn.Caption = "Експорт в Excel...";
            this.ExportToExcelBtn.Id = 35;
            this.ExportToExcelBtn.ImageOptions.ImageIndex = 18;
            this.ExportToExcelBtn.Name = "ExportToExcelBtn";
            this.ExportToExcelBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ExportToExcelBtn_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1375, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 695);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1375, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 671);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1375, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 671);
            // 
            // EditItemBtn
            // 
            this.EditItemBtn.Caption = "Властивості";
            this.EditItemBtn.Id = 2;
            this.EditItemBtn.ImageOptions.ImageIndex = 4;
            this.EditItemBtn.Name = "EditItemBtn";
            this.EditItemBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.EditItemBtn_ItemClick);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Встановити налаштування сітки по замовчуванню";
            this.barButtonItem1.Id = 34;
            this.barButtonItem1.ImageOptions.ImageIndex = 16;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // WbListPopupMenu
            // 
            this.WbListPopupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.RefrechItemBtn, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.ExportToExcelBtn),
            new DevExpress.XtraBars.LinkPersistInfo(this.EditItemBtn, true)});
            this.WbListPopupMenu.Manager = this.barManager1;
            this.WbListPopupMenu.Name = "WbListPopupMenu";
            this.WbListPopupMenu.BeforePopup += new System.ComponentModel.CancelEventHandler(this.WbListPopupMenu_BeforePopup);
            // 
            // ucDocumentDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WaybillDetGridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ucDocumentDetails";
            this.Size = new System.Drawing.Size(1375, 695);
            this.Load += new System.EventHandler(this.WayBillInUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckedItemImageComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox42)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateEdit.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDateEdit.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WbListPopupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public DevExpress.XtraGrid.GridControl WaybillDetGridControl;
        public DevExpress.XtraGrid.Views.Grid.GridView WaybillDetGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        public DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox CheckedItemImageComboBox;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn WbSummPayGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn WbBalansGridColumn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn42;
        private DevExpress.XtraGrid.Columns.GridColumn colPersonName;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox42;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem EditItemBtn;
        public DevExpress.XtraBars.BarButtonItem RefrechItemBtn;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu WbListPopupMenu;
        private DevExpress.Data.Linq.LinqInstantFeedbackSource WayBillIDetSource;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem ExportToExcelBtn;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraBars.BarEditItem StartDateEditItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit StartDateEdit;
        private DevExpress.XtraBars.BarEditItem EndDateEditItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit EndDateEdit;
    }
}
