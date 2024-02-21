namespace SP_Sklad.UserControls
{
    partial class ucRelDocGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucRelDocGrid));
            this.RelDocGridControl = new DevExpress.XtraGrid.GridControl();
            this.GetRelDocListBS = new System.Windows.Forms.BindingSource(this.components);
            this.RelDocGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox7 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.GridImageList = new DevExpress.Utils.ImageCollection(this.components);
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox6 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.DocTypeImageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.gridColumn16 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridColumn17 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn18 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn19 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn20 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl2 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.BarImageList = new DevExpress.Utils.ImageCollection(this.components);
            this.RefrechItemBtn = new DevExpress.XtraBars.BarButtonItem();
            this.MoveToDocBtn = new DevExpress.XtraBars.BarButtonItem();
            this.PrintDocBtn = new DevExpress.XtraBars.BarButtonItem();
            this.BottomPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RelDocGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetRelDocListBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RelDocGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocTypeImageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarImageList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPopupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // RelDocGridControl
            // 
            this.RelDocGridControl.DataSource = this.GetRelDocListBS;
            this.RelDocGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RelDocGridControl.Location = new System.Drawing.Point(0, 0);
            this.RelDocGridControl.MainView = this.RelDocGridView;
            this.RelDocGridControl.Name = "RelDocGridControl";
            this.RelDocGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox2,
            this.repositoryItemImageComboBox6,
            this.repositoryItemImageComboBox7});
            this.RelDocGridControl.Size = new System.Drawing.Size(948, 329);
            this.RelDocGridControl.TabIndex = 1;
            this.RelDocGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.RelDocGridView});
            // 
            // GetRelDocListBS
            // 
            this.GetRelDocListBS.DataSource = typeof(SP_Sklad.SkladData.GetRelDocList_Result);
            // 
            // RelDocGridView
            // 
            this.RelDocGridView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.RelDocGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn14,
            this.gridColumn15,
            this.gridColumn16,
            this.gridColumn17,
            this.gridColumn18,
            this.gridColumn19,
            this.gridColumn20,
            this.gridColumn21});
            this.RelDocGridView.GridControl = this.RelDocGridControl;
            this.RelDocGridView.Name = "RelDocGridView";
            this.RelDocGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.RelDocGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.RelDocGridView.OptionsBehavior.ReadOnly = true;
            this.RelDocGridView.OptionsView.ShowGroupPanel = false;
            this.RelDocGridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.RelDocGridView_PopupMenuShowing);
            // 
            // gridColumn14
            // 
            this.gridColumn14.Caption = "gridColumn14";
            this.gridColumn14.ColumnEdit = this.repositoryItemImageComboBox7;
            this.gridColumn14.FieldName = "RelType";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.OptionsColumn.AllowEdit = false;
            this.gridColumn14.OptionsColumn.AllowFocus = false;
            this.gridColumn14.OptionsColumn.AllowMove = false;
            this.gridColumn14.OptionsColumn.AllowSize = false;
            this.gridColumn14.OptionsColumn.FixedWidth = true;
            this.gridColumn14.OptionsColumn.ReadOnly = true;
            this.gridColumn14.OptionsColumn.ShowCaption = false;
            this.gridColumn14.OptionsColumn.TabStop = false;
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 0;
            this.gridColumn14.Width = 25;
            // 
            // repositoryItemImageComboBox7
            // 
            this.repositoryItemImageComboBox7.AutoHeight = false;
            this.repositoryItemImageComboBox7.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox7.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 3)});
            this.repositoryItemImageComboBox7.Name = "repositoryItemImageComboBox7";
            this.repositoryItemImageComboBox7.SmallImages = this.GridImageList;
            // 
            // GridImageList
            // 
            this.GridImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("GridImageList.ImageStream")));
            this.GridImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.new_document, "new_document", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.GridImageList.Images.SetKeyName(0, "new_document");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.checked_green, "checked_green", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.GridImageList.Images.SetKeyName(1, "checked_green");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.checked_blue, "checked_blue", typeof(global::SP_Sklad.Properties.Resources), 2);
            this.GridImageList.Images.SetKeyName(2, "checked_blue");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.doc_link, "doc_link", typeof(global::SP_Sklad.Properties.Resources), 3);
            this.GridImageList.Images.SetKeyName(3, "doc_link");
            this.GridImageList.InsertImage(global::SP_Sklad.Properties.Resources.disabled_link, "disabled_link", typeof(global::SP_Sklad.Properties.Resources), 4);
            this.GridImageList.Images.SetKeyName(4, "disabled_link");
            // 
            // gridColumn15
            // 
            this.gridColumn15.Caption = "gridColumn15";
            this.gridColumn15.ColumnEdit = this.repositoryItemImageComboBox6;
            this.gridColumn15.FieldName = "DocType";
            this.gridColumn15.Name = "gridColumn15";
            this.gridColumn15.OptionsColumn.AllowEdit = false;
            this.gridColumn15.OptionsColumn.AllowFocus = false;
            this.gridColumn15.OptionsColumn.AllowMove = false;
            this.gridColumn15.OptionsColumn.AllowSize = false;
            this.gridColumn15.OptionsColumn.FixedWidth = true;
            this.gridColumn15.OptionsColumn.ReadOnly = true;
            this.gridColumn15.OptionsColumn.ShowCaption = false;
            this.gridColumn15.OptionsColumn.TabStop = false;
            this.gridColumn15.Visible = true;
            this.gridColumn15.VisibleIndex = 1;
            this.gridColumn15.Width = 25;
            // 
            // repositoryItemImageComboBox6
            // 
            this.repositoryItemImageComboBox6.AutoHeight = false;
            this.repositoryItemImageComboBox6.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox6.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -1, 18),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 6, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -6, 20),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 16, 19),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -16, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 3, 15),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -3, 16),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 4, 3),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -20, 14),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -22, 9),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -5, 21),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 5, 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 13),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 9, 40),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 28, 24),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 32, 11),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 7, 12),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -25, 23),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 24, 22),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -24, 7),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 20, 28)});
            this.repositoryItemImageComboBox6.Name = "repositoryItemImageComboBox6";
            this.repositoryItemImageComboBox6.SmallImages = this.DocTypeImageCollection;
            // 
            // DocTypeImageCollection
            // 
            this.DocTypeImageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("DocTypeImageCollection.ImageStream")));
            this.DocTypeImageCollection.TransparentColor = System.Drawing.Color.Transparent;
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.waybill_in, "waybill_in", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.DocTypeImageCollection.Images.SetKeyName(0, "waybill_in");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.wb_order_in, "wb_order_in", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.DocTypeImageCollection.Images.SetKeyName(1, "wb_order_in");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.wb_return_in, "wb_return_in", typeof(global::SP_Sklad.Properties.Resources), 2);
            this.DocTypeImageCollection.Images.SetKeyName(2, "wb_return_in");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.move_stock, "move_stock", typeof(global::SP_Sklad.Properties.Resources), 3);
            this.DocTypeImageCollection.Images.SetKeyName(3, "move_stock");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.WBWriteOn_4, "WBWriteOn_4", typeof(global::SP_Sklad.Properties.Resources), 4);
            this.DocTypeImageCollection.Images.SetKeyName(4, "WBWriteOn_4");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.wb_return_sale, "wb_return_sale", typeof(global::SP_Sklad.Properties.Resources), 5);
            this.DocTypeImageCollection.Images.SetKeyName(5, "wb_return_sale");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.act_services_provider, "act_services_provider", typeof(global::SP_Sklad.Properties.Resources), 6);
            this.DocTypeImageCollection.Images.SetKeyName(6, "act_services_provider");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.preparation, "preparation", typeof(global::SP_Sklad.Properties.Resources), 7);
            this.DocTypeImageCollection.Images.SetKeyName(7, "preparation");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.contract_out, "contract_out", typeof(global::SP_Sklad.Properties.Resources), 8);
            this.DocTypeImageCollection.Images.SetKeyName(8, "contract_out");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.deboneing, "deboneing", typeof(global::SP_Sklad.Properties.Resources), 9);
            this.DocTypeImageCollection.Images.SetKeyName(9, "deboneing");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.debt_adjustment, "debt_adjustment", typeof(global::SP_Sklad.Properties.Resources), 10);
            this.DocTypeImageCollection.Images.SetKeyName(10, "debt_adjustment");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.expedition, "expedition", typeof(global::SP_Sklad.Properties.Resources), 11);
            this.DocTypeImageCollection.Images.SetKeyName(11, "expedition");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.inventory_act, "inventory_act", typeof(global::SP_Sklad.Properties.Resources), 12);
            this.DocTypeImageCollection.Images.SetKeyName(12, "inventory_act");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.invoice, "invoice", typeof(global::SP_Sklad.Properties.Resources), 13);
            this.DocTypeImageCollection.Images.SetKeyName(13, "invoice");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.manufacturing_products, "manufacturing_products", typeof(global::SP_Sklad.Properties.Resources), 14);
            this.DocTypeImageCollection.Images.SetKeyName(14, "manufacturing_products");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.pay_doc_in, "pay_doc_in", typeof(global::SP_Sklad.Properties.Resources), 15);
            this.DocTypeImageCollection.Images.SetKeyName(15, "pay_doc_in");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.pay_doc_out, "pay_doc_out", typeof(global::SP_Sklad.Properties.Resources), 16);
            this.DocTypeImageCollection.Images.SetKeyName(16, "pay_doc_out");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.pay_doc_out_2, "pay_doc_out_2", typeof(global::SP_Sklad.Properties.Resources), 17);
            this.DocTypeImageCollection.Images.SetKeyName(17, "pay_doc_out_2");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.waybill_out, "waybill_out", typeof(global::SP_Sklad.Properties.Resources), 18);
            this.DocTypeImageCollection.Images.SetKeyName(18, "waybill_out");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.wb_order_out, "wb_order_out", typeof(global::SP_Sklad.Properties.Resources), 19);
            this.DocTypeImageCollection.Images.SetKeyName(19, "wb_order_out");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.wb_return_out, "wb_return_out", typeof(global::SP_Sklad.Properties.Resources), 20);
            this.DocTypeImageCollection.Images.SetKeyName(20, "wb_return_out");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.WBWriteOff_2, "WBWriteOff_2", typeof(global::SP_Sklad.Properties.Resources), 21);
            this.DocTypeImageCollection.Images.SetKeyName(21, "WBWriteOff_2");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.weightedpies_16x16, "weightedpies_16x16", typeof(global::SP_Sklad.Properties.Resources), 22);
            this.DocTypeImageCollection.Images.SetKeyName(22, "weightedpies_16x16");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.purchase, "purchase", typeof(global::SP_Sklad.Properties.Resources), 23);
            this.DocTypeImageCollection.Images.SetKeyName(23, "purchase");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.management_raw_materials, "management_raw_materials", typeof(global::SP_Sklad.Properties.Resources), 24);
            this.DocTypeImageCollection.Images.SetKeyName(24, "management_raw_materials");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.price_lict, "price_lict", typeof(global::SP_Sklad.Properties.Resources), 25);
            this.DocTypeImageCollection.Images.SetKeyName(25, "price_lict");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.price_settings, "price_settings", typeof(global::SP_Sklad.Properties.Resources), 26);
            this.DocTypeImageCollection.Images.SetKeyName(26, "price_settings");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.project_manager_2, "project_manager_2", typeof(global::SP_Sklad.Properties.Resources), 27);
            this.DocTypeImageCollection.Images.SetKeyName(27, "project_manager_2");
            this.DocTypeImageCollection.InsertImage(global::SP_Sklad.Properties.Resources.production_planning, "production_planning", typeof(global::SP_Sklad.Properties.Resources), 28);
            this.DocTypeImageCollection.Images.SetKeyName(28, "production_planning");
            // 
            // gridColumn16
            // 
            this.gridColumn16.Caption = "gridColumn16";
            this.gridColumn16.ColumnEdit = this.repositoryItemImageComboBox2;
            this.gridColumn16.FieldName = "Checked";
            this.gridColumn16.Name = "gridColumn16";
            this.gridColumn16.OptionsColumn.AllowEdit = false;
            this.gridColumn16.OptionsColumn.AllowFocus = false;
            this.gridColumn16.OptionsColumn.AllowMove = false;
            this.gridColumn16.OptionsColumn.AllowSize = false;
            this.gridColumn16.OptionsColumn.FixedWidth = true;
            this.gridColumn16.OptionsColumn.ReadOnly = true;
            this.gridColumn16.OptionsColumn.ShowCaption = false;
            this.gridColumn16.OptionsColumn.TabStop = false;
            this.gridColumn16.Visible = true;
            this.gridColumn16.VisibleIndex = 2;
            this.gridColumn16.Width = 25;
            // 
            // repositoryItemImageComboBox2
            // 
            this.repositoryItemImageComboBox2.AutoHeight = false;
            this.repositoryItemImageComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox2.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 2)});
            this.repositoryItemImageComboBox2.Name = "repositoryItemImageComboBox2";
            this.repositoryItemImageComboBox2.SmallImages = this.GridImageList;
            // 
            // gridColumn17
            // 
            this.gridColumn17.Caption = "Номер";
            this.gridColumn17.FieldName = "Num";
            this.gridColumn17.Name = "gridColumn17";
            this.gridColumn17.Visible = true;
            this.gridColumn17.VisibleIndex = 3;
            this.gridColumn17.Width = 71;
            // 
            // gridColumn18
            // 
            this.gridColumn18.Caption = "Дата";
            this.gridColumn18.DisplayFormat.FormatString = "g";
            this.gridColumn18.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn18.FieldName = "OnDate";
            this.gridColumn18.Name = "gridColumn18";
            this.gridColumn18.Visible = true;
            this.gridColumn18.VisibleIndex = 4;
            this.gridColumn18.Width = 151;
            // 
            // gridColumn19
            // 
            this.gridColumn19.Caption = "Тип документу";
            this.gridColumn19.FieldName = "DocTypeName";
            this.gridColumn19.Name = "gridColumn19";
            this.gridColumn19.Visible = true;
            this.gridColumn19.VisibleIndex = 5;
            this.gridColumn19.Width = 218;
            // 
            // gridColumn20
            // 
            this.gridColumn20.Caption = "Сума";
            this.gridColumn20.DisplayFormat.FormatString = "0.00";
            this.gridColumn20.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn20.FieldName = "Summ";
            this.gridColumn20.Name = "gridColumn20";
            this.gridColumn20.Visible = true;
            this.gridColumn20.VisibleIndex = 6;
            this.gridColumn20.Width = 116;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "Валюта";
            this.gridColumn21.FieldName = "CurrencyName";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 7;
            this.gridColumn21.Width = 110;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl2);
            this.barManager1.Form = this;
            this.barManager1.Images = this.BarImageList;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.RefrechItemBtn,
            this.MoveToDocBtn,
            this.PrintDocBtn});
            this.barManager1.MaxItemId = 30;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(948, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 329);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(948, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 329);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(948, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 329);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.AutoSize = true;
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl1.Manager = this.barManager1;
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(948, 0);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // standaloneBarDockControl2
            // 
            this.standaloneBarDockControl2.AutoSize = true;
            this.standaloneBarDockControl2.CausesValidation = false;
            this.standaloneBarDockControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl2.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl2.Manager = this.barManager1;
            this.standaloneBarDockControl2.Name = "standaloneBarDockControl2";
            this.standaloneBarDockControl2.Size = new System.Drawing.Size(948, 0);
            this.standaloneBarDockControl2.Text = "standaloneBarDockControl2";
            // 
            // BarImageList
            // 
            this.BarImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("BarImageList.ImageStream")));
            this.BarImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.BarImageList.InsertImage(global::SP_Sklad.Properties.Resources.walking_16x16, "walking_16x16", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.BarImageList.Images.SetKeyName(0, "walking_16x16");
            this.BarImageList.InsertImage(global::SP_Sklad.Properties.Resources.preview_2, "preview_2", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.BarImageList.Images.SetKeyName(1, "preview_2");
            this.BarImageList.InsertImage(global::SP_Sklad.Properties.Resources.refresh, "refresh", typeof(global::SP_Sklad.Properties.Resources), 2);
            this.BarImageList.Images.SetKeyName(2, "refresh");
            // 
            // RefrechItemBtn
            // 
            this.RefrechItemBtn.Caption = "Обновити";
            this.RefrechItemBtn.Id = 4;
            this.RefrechItemBtn.ImageOptions.ImageIndex = 2;
            this.RefrechItemBtn.Name = "RefrechItemBtn";
            // 
            // MoveToDocBtn
            // 
            this.MoveToDocBtn.Caption = "Перейти до документа";
            this.MoveToDocBtn.Id = 12;
            this.MoveToDocBtn.ImageOptions.ImageIndex = 0;
            this.MoveToDocBtn.Name = "MoveToDocBtn";
            this.MoveToDocBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MoveToDocBtn_ItemClick);
            // 
            // PrintDocBtn
            // 
            this.PrintDocBtn.Caption = "Друк/Попередній перегляд";
            this.PrintDocBtn.Id = 13;
            this.PrintDocBtn.ImageOptions.ImageIndex = 1;
            this.PrintDocBtn.Name = "PrintDocBtn";
            this.PrintDocBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PrintDocBtn_ItemClick);
            // 
            // BottomPopupMenu
            // 
            this.BottomPopupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.MoveToDocBtn),
            new DevExpress.XtraBars.LinkPersistInfo(this.PrintDocBtn)});
            this.BottomPopupMenu.Manager = this.barManager1;
            this.BottomPopupMenu.Name = "BottomPopupMenu";
            // 
            // ucRelDocGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.standaloneBarDockControl1);
            this.Controls.Add(this.standaloneBarDockControl2);
            this.Controls.Add(this.RelDocGridControl);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ucRelDocGrid";
            this.Size = new System.Drawing.Size(948, 329);
            this.Load += new System.EventHandler(this.ucRelDocGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RelDocGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetRelDocListBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RelDocGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocTypeImageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarImageList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPopupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl RelDocGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView RelDocGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn15;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn16;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn17;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn18;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn19;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn20;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl2;
        public DevExpress.XtraBars.BarButtonItem RefrechItemBtn;
        private DevExpress.XtraBars.BarButtonItem MoveToDocBtn;
        private DevExpress.XtraBars.BarButtonItem PrintDocBtn;
        private DevExpress.XtraBars.PopupMenu BottomPopupMenu;
        private System.Windows.Forms.BindingSource GetRelDocListBS;
        private DevExpress.Utils.ImageCollection BarImageList;
        private DevExpress.Utils.ImageCollection DocTypeImageCollection;
        private DevExpress.Utils.ImageCollection GridImageList;
    }
}
