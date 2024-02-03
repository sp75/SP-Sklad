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
            this.GridImageList = new System.Windows.Forms.ImageList(this.components);
            this.gridColumn15 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox6 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
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
            this.RefrechItemBtn = new DevExpress.XtraBars.BarButtonItem();
            this.MoveToDocBtn = new DevExpress.XtraBars.BarButtonItem();
            this.PrintDocBtn = new DevExpress.XtraBars.BarButtonItem();
            this.BottomPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.BarImageList = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RelDocGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetRelDocListBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RelDocGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarImageList)).BeginInit();
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
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 37),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 38)});
            this.repositoryItemImageComboBox7.Name = "repositoryItemImageComboBox7";
            this.repositoryItemImageComboBox7.SmallImages = this.GridImageList;
            // 
            // GridImageList
            // 
            this.GridImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GridImageList.ImageStream")));
            this.GridImageList.TransparentColor = System.Drawing.Color.White;
            this.GridImageList.Images.SetKeyName(0, "ПриходНакл.bmp");
            this.GridImageList.Images.SetKeyName(1, "Счета.bmp");
            this.GridImageList.Images.SetKeyName(2, "РасходНакл.bmp");
            this.GridImageList.Images.SetKeyName(3, "Счет-фактуры.bmp");
            this.GridImageList.Images.SetKeyName(4, "ВходПлатежи.bmp");
            this.GridImageList.Images.SetKeyName(5, "ИсходПлатежи.bmp");
            this.GridImageList.Images.SetKeyName(6, "ДопРасход.bmp");
            this.GridImageList.Images.SetKeyName(7, "Возврат Поставщику.bmp");
            this.GridImageList.Images.SetKeyName(8, "Возврат от клиетна.bmp");
            this.GridImageList.Images.SetKeyName(9, "Заказ от клиента.bmp");
            this.GridImageList.Images.SetKeyName(10, "Заказ поставщикам.bmp");
            this.GridImageList.Images.SetKeyName(11, "Договор купли.bmp");
            this.GridImageList.Images.SetKeyName(12, "Договор продажу.bmp");
            this.GridImageList.Images.SetKeyName(13, "Прайс лист.bmp");
            this.GridImageList.Images.SetKeyName(14, "Введення залишк_в товар_в.bmp");
            this.GridImageList.Images.SetKeyName(15, "Акти списання товару.bmp");
            this.GridImageList.Images.SetKeyName(16, "Товари.bmp");
            this.GridImageList.Images.SetKeyName(17, "Накладн_ перем_щення.bmp");
            this.GridImageList.Images.SetKeyName(18, "Бази даних.bmp");
            this.GridImageList.Images.SetKeyName(19, "Add.bmp");
            this.GridImageList.Images.SetKeyName(20, "edit.bmp");
            this.GridImageList.Images.SetKeyName(21, "Delete.bmp");
            this.GridImageList.Images.SetKeyName(22, "execute.png");
            this.GridImageList.Images.SetKeyName(23, "н_.bmp");
            this.GridImageList.Images.SetKeyName(24, "н_чого.bmp");
            this.GridImageList.Images.SetKeyName(25, "Частково оброблений.bmp");
            this.GridImageList.Images.SetKeyName(26, "exec16.png");
            this.GridImageList.Images.SetKeyName(27, "Конрагент.bmp");
            this.GridImageList.Images.SetKeyName(28, "Службовц_.bmp");
            this.GridImageList.Images.SetKeyName(29, "списати грош_.bmp");
            this.GridImageList.Images.SetKeyName(30, "зарахувати.bmp");
            this.GridImageList.Images.SetKeyName(31, "Акти iнвентаризацiї.bmp");
            this.GridImageList.Images.SetKeyName(32, "позначити.bmp");
            this.GridImageList.Images.SetKeyName(33, "пратнерка.png");
            this.GridImageList.Images.SetKeyName(34, "Отчеты.bmp");
            this.GridImageList.Images.SetKeyName(35, "Документы16x16.bmp");
            this.GridImageList.Images.SetKeyName(36, "податкова накладна.bmp");
            this.GridImageList.Images.SetKeyName(37, "disabled_link.png");
            this.GridImageList.Images.SetKeyName(38, "1335611569_link.png");
            this.GridImageList.Images.SetKeyName(39, "Послуги.bmp");
            this.GridImageList.Images.SetKeyName(40, "moneymove.png");
            this.GridImageList.Images.SetKeyName(41, "Debt adjustment.png");
            this.GridImageList.Images.SetKeyName(42, "credit adjustment.png");
            this.GridImageList.Images.SetKeyName(43, "Зарезервовано.bmp");
            this.GridImageList.Images.SetKeyName(44, "CostAnalysis_16x16.png");
            this.GridImageList.Images.SetKeyName(45, "crediting the funds to the account.png");
            this.GridImageList.Images.SetKeyName(46, "management2.png");
            this.GridImageList.Images.SetKeyName(47, "ActServices.png");
            this.GridImageList.Images.SetKeyName(48, "delivery-truck (1).png");
            this.GridImageList.Images.SetKeyName(49, "free-icon-point-of-sale-313166.png");
            this.GridImageList.Images.SetKeyName(50, "weighing-scale.png");
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
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -1, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 6, 8),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -6, 7),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 16, 10),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -16, 9),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 3, 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -3, 5),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 4, 17),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -20, 26),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -22, 33),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -5, 15),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 5, 14),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 9, 40),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 28, 46),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 32, 48),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 7, 31),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -25, 49),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 24, 50)});
            this.repositoryItemImageComboBox6.Name = "repositoryItemImageComboBox6";
            this.repositoryItemImageComboBox6.SmallImages = this.GridImageList;
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
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 24),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 22),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 25)});
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
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BarImageList)).EndInit();
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
        public System.Windows.Forms.ImageList GridImageList;
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
    }
}
