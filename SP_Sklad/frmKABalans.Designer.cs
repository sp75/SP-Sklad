﻿namespace SP_Sklad
{
    partial class frmKaBalans
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKaBalans));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.DocListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colWType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.GridImageList = new System.Windows.Forms.ImageList(this.components);
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNum = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colOnDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colKaName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colSummAll = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colOnValue = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colCurrName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSummInCurr = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSaldo = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.wTypeList = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.wbEndDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.wbStartDate = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wTypeList.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.OkButton);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 408);
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
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.White;
            this.ImageList.Images.SetKeyName(0, "Документ.bmp");
            this.ImageList.Images.SetKeyName(1, "Попередн_й перегляд.bmp");
            this.ImageList.Images.SetKeyName(2, "Перейти до  документа.bmp");
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
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 462);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1176, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 24);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 438);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1176, 24);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 438);
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
            this.gridControl1.Size = new System.Drawing.Size(1176, 340);
            this.gridControl1.TabIndex = 37;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridView1});
            // 
            // bandedGridView1
            // 
            this.bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand1,
            this.gridBand2});
            this.bandedGridView1.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colWType,
            this.colNum,
            this.colOnDate,
            this.colKaName,
            this.colOnValue,
            this.colCurrName,
            this.colSummAll,
            this.colSummInCurr,
            this.colSaldo,
            this.bandedGridColumn1});
            this.bandedGridView1.GridControl = this.gridControl1;
            this.bandedGridView1.Name = "bandedGridView1";
            this.bandedGridView1.OptionsBehavior.AllowIncrementalSearch = true;
            this.bandedGridView1.OptionsBehavior.Editable = false;
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
            this.gridBand1.Width = 700;
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
            this.colWType.Width = 20;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 6, 8),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -1, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -6, 7),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 3, 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -3, 5),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -2, 6),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -16, 9),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 16, 10),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -25, 40),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 25, 39)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.GridImageList;
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
            this.GridImageList.Images.SetKeyName(22, "Так.bmp");
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
            this.GridImageList.Images.SetKeyName(37, "1335611633_link_break.png");
            this.GridImageList.Images.SetKeyName(38, "1335611569_link.png");
            this.GridImageList.Images.SetKeyName(39, "Без имени-2.png");
            this.GridImageList.Images.SetKeyName(40, "free-icon-payment-terminal-3777466.png");
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "bandedGridColumn1";
            this.bandedGridColumn1.FieldName = "DocShortName";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.OptionsColumn.ShowCaption = false;
            this.bandedGridColumn1.Visible = true;
            this.bandedGridColumn1.Width = 46;
            // 
            // colNum
            // 
            this.colNum.Caption = "№";
            this.colNum.FieldName = "Num";
            this.colNum.Name = "colNum";
            this.colNum.Visible = true;
            this.colNum.Width = 74;
            // 
            // colOnDate
            // 
            this.colOnDate.Caption = "Дата";
            this.colOnDate.DisplayFormat.FormatString = "g";
            this.colOnDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colOnDate.FieldName = "OnDate";
            this.colOnDate.Name = "colOnDate";
            this.colOnDate.Visible = true;
            this.colOnDate.Width = 117;
            // 
            // colKaName
            // 
            this.colKaName.Caption = "Контрагент";
            this.colKaName.FieldName = "KaName";
            this.colKaName.Name = "colKaName";
            this.colKaName.Visible = true;
            this.colKaName.Width = 443;
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "Разом по дукументу";
            this.gridBand2.Columns.Add(this.colSummAll);
            this.gridBand2.Columns.Add(this.colOnValue);
            this.gridBand2.Columns.Add(this.colCurrName);
            this.gridBand2.Columns.Add(this.colSummInCurr);
            this.gridBand2.Columns.Add(this.colSaldo);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 1;
            this.gridBand2.Width = 451;
            // 
            // colSummAll
            // 
            this.colSummAll.Caption = "Сума в валюті";
            this.colSummAll.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSummAll.FieldName = "SummAll";
            this.colSummAll.Name = "colSummAll";
            this.colSummAll.Visible = true;
            this.colSummAll.Width = 97;
            // 
            // colOnValue
            // 
            this.colOnValue.Caption = "Курс";
            this.colOnValue.DisplayFormat.FormatString = "0.00";
            this.colOnValue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colOnValue.FieldName = "OnValue";
            this.colOnValue.Name = "colOnValue";
            this.colOnValue.Visible = true;
            this.colOnValue.Width = 67;
            // 
            // colCurrName
            // 
            this.colCurrName.Caption = "Валюта";
            this.colCurrName.FieldName = "CurrName";
            this.colCurrName.Name = "colCurrName";
            this.colCurrName.Visible = true;
            this.colCurrName.Width = 60;
            // 
            // colSummInCurr
            // 
            this.colSummInCurr.AppearanceCell.BackColor = System.Drawing.Color.AliceBlue;
            this.colSummInCurr.AppearanceCell.Options.UseBackColor = true;
            this.colSummInCurr.Caption = "Сума в облік. валюті";
            this.colSummInCurr.DisplayFormat.FormatString = "0.00";
            this.colSummInCurr.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSummInCurr.FieldName = "SummInCurr";
            this.colSummInCurr.Name = "colSummInCurr";
            this.colSummInCurr.Visible = true;
            this.colSummInCurr.Width = 119;
            // 
            // colSaldo
            // 
            this.colSaldo.AppearanceCell.BackColor = System.Drawing.Color.AliceBlue;
            this.colSaldo.AppearanceCell.Options.UseBackColor = true;
            this.colSaldo.Caption = "Залишок";
            this.colSaldo.DisplayFormat.FormatString = "0.00";
            this.colSaldo.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSaldo.FieldName = "Saldo";
            this.colSaldo.Name = "colSaldo";
            this.colSaldo.Visible = true;
            this.colSaldo.Width = 108;
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
            // frmKaBalans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 462);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "frmKaBalans";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Банс з контрагентом";
            this.Load += new System.EventHandler(this.frmKABalans_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wTypeList.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wbStartDate.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        public System.Windows.Forms.ImageList ImageList;
        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.BindingSource DocListBindingSource;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNum;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colOnDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colKaName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSummAll;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSummInCurr;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colCurrName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colOnValue;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSaldo;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        public System.Windows.Forms.ImageList GridImageList;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit wTypeList;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit wbEndDate;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit wbStartDate;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
    }
}