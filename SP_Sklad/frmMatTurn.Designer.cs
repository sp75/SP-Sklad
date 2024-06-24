namespace SP_Sklad
{
    partial class frmMatTurn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMatTurn));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ImageList = new DevExpress.Utils.ImageCollection(this.components);
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.DocListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.KagentComboBox = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.wTypeList = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.wbEndDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.wbStartDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colWType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.GridImageList = new DevExpress.Utils.ImageCollection(this.components);
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNum = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colOnDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colKaName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colRemain = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colPrice = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCurrName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KagentComboBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wTypeList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridImageList)).BeginInit();
            this.SuspendLayout();
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
            this.barButtonItem5});
            this.barManager1.MaxItemId = 7;
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
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5)});
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Звіт про рух товару";
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
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1139, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 584);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1139, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 560);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1139, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 560);
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
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5, true)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // DocListBindingSource
            // 
            this.DocListBindingSource.DataSource = typeof(SP_Sklad.SkladData.GetMatMove_Result);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.OkButton);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 530);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1139, 54);
            this.panelControl2.TabIndex = 33;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(1052, 12);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 30);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "Так";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.KagentComboBox);
            this.panelControl1.Controls.Add(this.labelControl4);
            this.panelControl1.Controls.Add(this.wTypeList);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.wbEndDate);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.wbStartDate);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 24);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1139, 44);
            this.panelControl1.TabIndex = 43;
            // 
            // KagentComboBox
            // 
            this.KagentComboBox.Location = new System.Drawing.Point(793, 11);
            this.KagentComboBox.Name = "KagentComboBox";
            this.KagentComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.KagentComboBox.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.KagentComboBox.Properties.DisplayMember = "Name";
            this.KagentComboBox.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch;
            this.KagentComboBox.Properties.ShowFooter = false;
            this.KagentComboBox.Properties.ShowHeader = false;
            this.KagentComboBox.Properties.ValueMember = "KaId";
            this.KagentComboBox.Size = new System.Drawing.Size(334, 22);
            this.KagentComboBox.StyleController = this.styleController1;
            this.KagentComboBox.TabIndex = 10;
            this.KagentComboBox.EditValueChanged += new System.EventHandler(this.wbStartDate_EditValueChanged);
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(727, 14);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Конрагент";
            // 
            // wTypeList
            // 
            this.wTypeList.Location = new System.Drawing.Point(460, 11);
            this.wTypeList.Name = "wTypeList";
            this.wTypeList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wTypeList.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.wTypeList.Properties.DisplayMember = "Name";
            this.wTypeList.Properties.ShowFooter = false;
            this.wTypeList.Properties.ShowHeader = false;
            this.wTypeList.Properties.ValueMember = "Id";
            this.wTypeList.Size = new System.Drawing.Size(241, 22);
            this.wTypeList.StyleController = this.styleController1;
            this.wTypeList.TabIndex = 6;
            this.wTypeList.EditValueChanged += new System.EventHandler(this.wbStartDate_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(364, 14);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(90, 16);
            this.labelControl3.StyleController = this.styleController1;
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Тип документів";
            // 
            // wbEndDate
            // 
            this.wbEndDate.EditValue = null;
            this.wbEndDate.Location = new System.Drawing.Point(218, 11);
            this.wbEndDate.Name = "wbEndDate";
            this.wbEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbEndDate.Size = new System.Drawing.Size(122, 22);
            this.wbEndDate.StyleController = this.styleController1;
            this.wbEndDate.TabIndex = 3;
            this.wbEndDate.EditValueChanged += new System.EventHandler(this.wbStartDate_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(198, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(14, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "по";
            // 
            // wbStartDate
            // 
            this.wbStartDate.EditValue = null;
            this.wbStartDate.Location = new System.Drawing.Point(68, 11);
            this.wbStartDate.Name = "wbStartDate";
            this.wbStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbStartDate.Size = new System.Drawing.Size(124, 22);
            this.wbStartDate.StyleController = this.styleController1;
            this.wbStartDate.TabIndex = 1;
            this.wbStartDate.EditValueChanged += new System.EventHandler(this.wbStartDate_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Період з";
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.DocListBindingSource;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 68);
            this.gridControl1.MainView = this.bandedGridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.gridControl1.Size = new System.Drawing.Size(1139, 462);
            this.gridControl1.TabIndex = 44;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand3,
            this.gridBand2});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colWType,
            this.colNum,
            this.colOnDate,
            this.colKaName,
            this.colCurrName,
            this.colPrice,
            this.colRemain,
            this.bandedGridColumn1,
            this.bandedGridColumn2,
            this.bandedGridColumn3});
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.bandedGridView1.OptionsBehavior.Editable = false;
            this.bandedGridView1.OptionsView.ShowFooter = true;
            this.bandedGridView1.OptionsView.ShowGroupPanel = false;
            this.bandedGridView1.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.bandedGridView1_PopupMenuShowing);
            // 
            // gridBand1
            // 
            this.gridBand1.Caption = "Документ";
            this.gridBand1.Columns.Add(this.colWType);
            this.gridBand1.Columns.Add(this.bandedGridColumn1);
            this.gridBand1.Columns.Add(this.colNum);
            this.gridBand1.Columns.Add(this.colOnDate);
            this.gridBand1.Columns.Add(this.colKaName);
            this.gridBand1.Name = "gridBand1";
            this.gridBand1.VisibleIndex = 0;
            this.gridBand1.Width = 559;
            // 
            // colWType
            // 
            this.colWType.ColumnEdit = this.repositoryItemImageComboBox1;
            this.colWType.FieldName = "WType";
            this.colWType.Name = "colWType";
            this.colWType.OptionsColumn.AllowEdit = false;
            this.colWType.OptionsColumn.AllowIncrementalSearch = false;
            this.colWType.OptionsColumn.AllowMove = false;
            this.colWType.OptionsColumn.AllowSize = false;
            this.colWType.OptionsColumn.ReadOnly = true;
            this.colWType.OptionsColumn.ShowCaption = false;
            this.colWType.OptionsColumn.ShowInCustomizationForm = false;
            this.colWType.Visible = true;
            this.colWType.Width = 25;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Прибуткова накладна", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Повернення від клієнта", 6, 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Видаткова накладна", -1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Повернення постачальнику", -6, 5),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Введення залишків товарів", 5, 12),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Акти списання товару", -5, 11),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Накладна переміщення", 4, 13),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Виготовлення продукції", -20, 9),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Обвалка сировини", -22, 8),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Підготовка сировини", -24, 10),
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
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.deboneing, "deboneing", typeof(global::SP_Sklad.Properties.Resources), 8);
            this.GridImageList.Images.SetKeyName(8, "deboneing");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.manufacturing_products, "manufacturing_products", typeof(global::SP_Sklad.Properties.Resources), 9);
            this.GridImageList.Images.SetKeyName(9, "manufacturing_products");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.preparation, "preparation", typeof(global::SP_Sklad.Properties.Resources), 10);
            this.GridImageList.Images.SetKeyName(10, "preparation");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.WBWriteOff_2, "WBWriteOff_2", typeof(global::SP_Sklad.Properties.Resources), 11);
            this.GridImageList.Images.SetKeyName(11, "WBWriteOff_2");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.WBWriteOn_4, "WBWriteOn_4", typeof(global::SP_Sklad.Properties.Resources), 12);
            this.GridImageList.Images.SetKeyName(12, "WBWriteOn_4");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.move_stock, "move_stock", typeof(global::SP_Sklad.Properties.Resources), 13);
            this.GridImageList.Images.SetKeyName(13, "move_stock");
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "bandedGridColumn1";
            this.bandedGridColumn1.FieldName = "TypeName";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.ShowCaption = false;
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 40;
            // 
            // colNum
            // 
            this.colNum.Caption = "№";
            this.colNum.FieldName = "Num";
            this.colNum.Name = "colNum";
            this.colNum.Visible = true;
            this.colNum.Width = 81;
            // 
            // colOnDate
            // 
            this.colOnDate.Caption = "Дата";
            this.colOnDate.DisplayFormat.FormatString = "g";
            this.colOnDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colOnDate.FieldName = "OnDate";
            this.colOnDate.Name = "colOnDate";
            this.colOnDate.Visible = true;
            this.colOnDate.Width = 113;
            // 
            // colKaName
            // 
            this.colKaName.Caption = "Контрагент";
            this.colKaName.FieldName = "KAgentName";
            this.colKaName.Name = "colKaName";
            this.colKaName.Visible = true;
            this.colKaName.Width = 300;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "Позиція";
            this.gridBand3.Columns.Add(this.bandedGridColumn3);
            this.gridBand3.Columns.Add(this.bandedGridColumn2);
            this.gridBand3.Columns.Add(this.colRemain);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 1;
            this.gridBand3.Width = 418;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.Caption = "Кількість";
            this.bandedGridColumn3.DisplayFormat.FormatString = "0.0000";
            this.bandedGridColumn3.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.bandedGridColumn3.FieldName = "Amount";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Amount", "{0:0.0000}")});
            this.bandedGridColumn3.Visible = true;
            this.bandedGridColumn3.Width = 70;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "Склад";
            this.bandedGridColumn2.FieldName = "WhName";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 234;
            // 
            // colRemain
            // 
            this.colRemain.Caption = "Поточний залишок";
            this.colRemain.DisplayFormat.FormatString = "0.00";
            this.colRemain.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRemain.FieldName = "Remain";
            this.colRemain.Name = "colRemain";
            this.colRemain.Visible = true;
            this.colRemain.Width = 114;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "Ціна";
            this.gridBand2.Columns.Add(this.colPrice);
            this.gridBand2.Columns.Add(this.colCurrName);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 2;
            this.gridBand2.Width = 154;
            // 
            // colPrice
            // 
            this.colPrice.Caption = "В валюті";
            this.colPrice.DisplayFormat.FormatString = "0.00";
            this.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.Width = 88;
            // 
            // colCurrName
            // 
            this.colCurrName.Caption = "Валюта";
            this.colCurrName.FieldName = "CurrName";
            this.colCurrName.Name = "colCurrName";
            this.colCurrName.Visible = true;
            this.colCurrName.Width = 66;
            // 
            // frmMatTurn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 584);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = global::SP_Sklad.Properties.Resources.move_warehouse;
            this.Name = "frmMatTurn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Рух товару: ";
            this.Load += new System.EventHandler(this.frmMatTurn_Load);
            this.Shown += new System.EventHandler(this.frmMatTurn_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KagentComboBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wTypeList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridImageList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.BindingSource DocListBindingSource;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit wTypeList;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit wbEndDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit wbStartDate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNum;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colOnDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKaName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colPrice;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCurrName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colRemain;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraEditors.LookUpEdit KagentComboBox;
        private DevExpress.Utils.ImageCollection ImageList;
        private DevExpress.Utils.ImageCollection GridImageList;
    }
}