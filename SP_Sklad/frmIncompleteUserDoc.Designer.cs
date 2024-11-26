namespace SP_Sklad
{
    partial class frmIncompleteUserDoc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIncompleteUserDoc));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.DocListBindingSource = new System.Windows.Forms.BindingSource(this.components);
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
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.wTypeList = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.wbEndDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.wbStartDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.GridImageList = new DevExpress.Utils.ImageCollection(this.components);
            this.PersonDocListSource = new DevExpress.Data.Linq.LinqInstantFeedbackSource();
            this.DocumentGridControl = new DevExpress.XtraGrid.GridControl();
            this.DocumentGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colWType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.colNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSummAll = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSummInCurr = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wTypeList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.OkButton);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 484);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(1176, 54);
            this.panelControl2.TabIndex = 32;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(1089, 12);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 30);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "Так";
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // DocListBindingSource
            // 
            this.DocListBindingSource.DataSource = typeof(SP_Sklad.SkladData.GetDocList_Result);
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
            this.barDockControlTop.Size = new System.Drawing.Size(1176, 24);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 538);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1176, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 514);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1176, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 514);
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
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5, true)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.wTypeList);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.wbEndDate);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.wbStartDate);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 24);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1176, 44);
            this.panelControl1.TabIndex = 42;
            this.panelControl1.Visible = false;
            // 
            // wTypeList
            // 
            this.wTypeList.Location = new System.Drawing.Point(419, 11);
            this.wTypeList.Name = "wTypeList";
            this.wTypeList.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wTypeList.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.wTypeList.Properties.DisplayMember = "Name";
            this.wTypeList.Properties.ShowFooter = false;
            this.wTypeList.Properties.ShowHeader = false;
            this.wTypeList.Properties.ValueMember = "Id";
            this.wTypeList.Size = new System.Drawing.Size(234, 20);
            this.wTypeList.TabIndex = 6;
            this.wTypeList.EditValueChanged += new System.EventHandler(this.wbStartDate_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(335, 14);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(78, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Тип документів";
            // 
            // wbEndDate
            // 
            this.wbEndDate.EditValue = null;
            this.wbEndDate.Location = new System.Drawing.Point(185, 11);
            this.wbEndDate.Name = "wbEndDate";
            this.wbEndDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbEndDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbEndDate.Size = new System.Drawing.Size(100, 20);
            this.wbEndDate.TabIndex = 3;
            this.wbEndDate.EditValueChanged += new System.EventHandler(this.wbStartDate_EditValueChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(167, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(12, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "по";
            // 
            // wbStartDate
            // 
            this.wbStartDate.EditValue = null;
            this.wbStartDate.Location = new System.Drawing.Point(61, 11);
            this.wbStartDate.Name = "wbStartDate";
            this.wbStartDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbStartDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.wbStartDate.Size = new System.Drawing.Size(100, 20);
            this.wbStartDate.TabIndex = 1;
            this.wbStartDate.EditValueChanged += new System.EventHandler(this.wbStartDate_EditValueChanged);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(42, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Період з";
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
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.deboneing, "deboneing", typeof(global::SP_Sklad.Properties.Resources), 19);
            this.GridImageList.Images.SetKeyName(19, "deboneing");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.manufacturing_products, "manufacturing_products", typeof(global::SP_Sklad.Properties.Resources), 20);
            this.GridImageList.Images.SetKeyName(20, "manufacturing_products");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.inventory_act, "inventory_act", typeof(global::SP_Sklad.Properties.Resources), 21);
            this.GridImageList.Images.SetKeyName(21, "inventory_act");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.weightedpies_16x16, "weightedpies_16x16", typeof(global::SP_Sklad.Properties.Resources), 22);
            this.GridImageList.Images.SetKeyName(22, "weightedpies_16x16");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.price_settings, "price_settings", typeof(global::SP_Sklad.Properties.Resources), 23);
            this.GridImageList.Images.SetKeyName(23, "price_settings");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.bank_statements, "bank_statements", typeof(global::SP_Sklad.Properties.Resources), 24);
            this.GridImageList.Images.SetKeyName(24, "bank_statements");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.expedition, "expedition", typeof(global::SP_Sklad.Properties.Resources), 25);
            this.GridImageList.Images.SetKeyName(25, "expedition");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.management_raw_materials, "management_raw_materials", typeof(global::SP_Sklad.Properties.Resources), 26);
            this.GridImageList.Images.SetKeyName(26, "management_raw_materials");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.money_in_2, "money_in_2", typeof(global::SP_Sklad.Properties.Resources), 27);
            this.GridImageList.Images.SetKeyName(27, "money_in_2");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.money_in1, "money_in1", typeof(global::SP_Sklad.Properties.Resources), 28);
            this.GridImageList.Images.SetKeyName(28, "money_in1");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.money_move_2, "money_move_2", typeof(global::SP_Sklad.Properties.Resources), 29);
            this.GridImageList.Images.SetKeyName(29, "money_move_2");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.money_out, "money_out", typeof(global::SP_Sklad.Properties.Resources), 30);
            this.GridImageList.Images.SetKeyName(30, "money_out");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.money_out_2, "money_out_2", typeof(global::SP_Sklad.Properties.Resources), 31);
            this.GridImageList.Images.SetKeyName(31, "money_out_2");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.preparation, "preparation", typeof(global::SP_Sklad.Properties.Resources), 32);
            this.GridImageList.Images.SetKeyName(32, "preparation");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.production_planning, "production_planning", typeof(global::SP_Sklad.Properties.Resources), 33);
            this.GridImageList.Images.SetKeyName(33, "production_planning");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.money_edit, "money_edit", typeof(global::SP_Sklad.Properties.Resources), 34);
            this.GridImageList.Images.SetKeyName(34, "money_edit");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.actives, "actives", typeof(global::SP_Sklad.Properties.Resources), 35);
            this.GridImageList.Images.SetKeyName(35, "actives");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.discount, "discount", typeof(global::SP_Sklad.Properties.Resources), 36);
            this.GridImageList.Images.SetKeyName(36, "discount");
            // 
            // PersonDocListSource
            // 
            this.PersonDocListSource.AreSourceRowsThreadSafe = true;
            this.PersonDocListSource.DefaultSorting = "OnDate DESC";
            this.PersonDocListSource.DesignTimeElementType = typeof(SP_Sklad.SkladData.v_UserDocs);
            this.PersonDocListSource.KeyExpression = "DocId";
            this.PersonDocListSource.GetQueryable += new System.EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.PersonDocListSource_GetQueryable);
            // 
            // DocumentGridControl
            // 
            this.DocumentGridControl.DataSource = this.PersonDocListSource;
            this.DocumentGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentGridControl.Location = new System.Drawing.Point(0, 68);
            this.DocumentGridControl.MainView = this.DocumentGridView;
            this.DocumentGridControl.Name = "DocumentGridControl";
            this.DocumentGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.DocumentGridControl.Size = new System.Drawing.Size(1176, 416);
            this.DocumentGridControl.TabIndex = 47;
            this.DocumentGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DocumentGridView});
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
            this.colSummAll,
            this.colSummInCurr});
            this.DocumentGridView.GridControl = this.DocumentGridControl;
            this.DocumentGridView.Name = "DocumentGridView";
            this.DocumentGridView.OptionsBehavior.ReadOnly = true;
            this.DocumentGridView.OptionsFind.AlwaysVisible = true;
            this.DocumentGridView.OptionsView.ShowAutoFilterRow = true;
            this.DocumentGridView.OptionsView.ShowDetailButtons = false;
            // 
            // colWType
            // 
            this.colWType.Caption = "Тип документа";
            this.colWType.ColumnEdit = this.repositoryItemImageComboBox1;
            this.colWType.FieldName = "DocType";
            this.colWType.Name = "colWType";
            this.colWType.OptionsColumn.AllowEdit = false;
            this.colWType.Visible = true;
            this.colWType.VisibleIndex = 0;
            this.colWType.Width = 259;
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
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Введення залишків товарів", 5, 18),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Виготовлення продукції", -20, 20),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Обвалка сировини", -22, 19),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Накладна переміщення", 4, 16),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Повернення від клієнта", 6, 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Повернення постачальнику", -6, 5),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Замовлення вiд клiєнтiв", -16, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Замовлення постачальникам", 16, 3),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Проміжкові зважування", 24, 22),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Акти iнвентаризацiї", 7, 21),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Продажі", -25, 6),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Повернення продаж", 25, 7),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Управління проектами", 30, 10),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Встановлення цін товарів", 31, 23),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Планування виробницва", 20, 33),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Фiнансовi операцiї (переміщення грошей)", -9, 31),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Фiнансовi операцiї (переміщення грошей)", 9, 27),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Фiнансовi операцiї (коригування залишку)", 18, 34),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Експедиція", 32, 25),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 33, 28),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Акції", 36, 36)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.GridImageList;
            // 
            // colNum
            // 
            this.colNum.Caption = "№";
            this.colNum.FieldName = "Num";
            this.colNum.Name = "colNum";
            this.colNum.OptionsColumn.AllowEdit = false;
            this.colNum.Visible = true;
            this.colNum.VisibleIndex = 1;
            this.colNum.Width = 163;
            // 
            // colOnDate
            // 
            this.colOnDate.Caption = "Дата";
            this.colOnDate.DisplayFormat.FormatString = "g";
            this.colOnDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colOnDate.FieldName = "OnDate";
            this.colOnDate.Name = "colOnDate";
            this.colOnDate.OptionsColumn.AllowEdit = false;
            this.colOnDate.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.Date;
            this.colOnDate.Visible = true;
            this.colOnDate.VisibleIndex = 2;
            this.colOnDate.Width = 198;
            // 
            // colName
            // 
            this.colName.Caption = "Контрагент";
            this.colName.FieldName = "KaName";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 3;
            this.colName.Width = 377;
            // 
            // colSummAll
            // 
            this.colSummAll.Caption = "Тип документа";
            this.colSummAll.FieldName = "DocType";
            this.colSummAll.Name = "colSummAll";
            this.colSummAll.OptionsColumn.AllowEdit = false;
            this.colSummAll.Visible = true;
            this.colSummAll.VisibleIndex = 5;
            this.colSummAll.Width = 121;
            // 
            // colSummInCurr
            // 
            this.colSummInCurr.Caption = "Сума в нац. валюті";
            this.colSummInCurr.DisplayFormat.FormatString = "0.00";
            this.colSummInCurr.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSummInCurr.FieldName = "SummInCurr";
            this.colSummInCurr.Name = "colSummInCurr";
            this.colSummInCurr.OptionsColumn.AllowEdit = false;
            this.colSummInCurr.Visible = true;
            this.colSummInCurr.VisibleIndex = 4;
            this.colSummInCurr.Width = 156;
            // 
            // frmIncompleteUserDoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 538);
            this.Controls.Add(this.DocumentGridControl);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = global::SP_Sklad.Properties.Resources.user_documens;
            this.Name = "frmIncompleteUserDoc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Не завршені документи";
            this.Load += new System.EventHandler(this.frmKABalans_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wTypeList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.BindingSource DocListBindingSource;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit wTypeList;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit wbEndDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit wbStartDate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.Utils.ImageCollection ImageList;
        private DevExpress.Utils.ImageCollection GridImageList;
        private DevExpress.Data.Linq.LinqInstantFeedbackSource PersonDocListSource;
        private DevExpress.XtraGrid.GridControl DocumentGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView DocumentGridView;
        private DevExpress.XtraGrid.Columns.GridColumn colWType;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn colNum;
        private DevExpress.XtraGrid.Columns.GridColumn colOnDate;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colSummAll;
        private DevExpress.XtraGrid.Columns.GridColumn colSummInCurr;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
    }
}