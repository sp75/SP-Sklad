namespace SP_Sklad.ViewsForm
{
    partial class frmProductionMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductionMonitor));
            this.ProductionMonitorGrid = new DevExpress.XtraGrid.GridControl();
            this.ProductionMonitorBS = new System.Windows.Forms.BindingSource(this.components);
            this.ProductionMonitorGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTechProcessName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTechProcessStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colManufacturingTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMatName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colWhName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toastNotificationsManager1 = new DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.standaloneBarDockControl1 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl2 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl3 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl4 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl5 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl6 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl7 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.standaloneBarDockControl8 = new DevExpress.XtraBars.StandaloneBarDockControl();
            this.PrintItemBtn = new DevExpress.XtraBars.BarButtonItem();
            this.EditTechProcBtn = new DevExpress.XtraBars.BarButtonItem();
            this.DelTechProcBtn = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.BottomPopupMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.BarImageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toastNotificationsManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPopupMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // ProductionMonitorGrid
            // 
            this.ProductionMonitorGrid.DataSource = this.ProductionMonitorBS;
            this.ProductionMonitorGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProductionMonitorGrid.Location = new System.Drawing.Point(0, 0);
            this.ProductionMonitorGrid.MainView = this.ProductionMonitorGridView;
            this.ProductionMonitorGrid.Name = "ProductionMonitorGrid";
            this.ProductionMonitorGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1});
            this.ProductionMonitorGrid.Size = new System.Drawing.Size(1485, 580);
            this.ProductionMonitorGrid.TabIndex = 19;
            this.ProductionMonitorGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ProductionMonitorGridView});
            // 
            // ProductionMonitorBS
            // 
            this.ProductionMonitorBS.DataSource = typeof(SP_Sklad.SkladData.v_ProductionMonitor);
            // 
            // ProductionMonitorGridView
            // 
            this.ProductionMonitorGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ProductionMonitorGridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.ProductionMonitorGridView.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.ProductionMonitorGridView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.ProductionMonitorGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ProductionMonitorGridView.Appearance.Row.Options.UseFont = true;
            this.ProductionMonitorGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNum,
            this.colOnDate,
            this.colTechProcessName,
            this.colTechProcessStartDate,
            this.colManufacturingTime,
            this.colMatName,
            this.colWhName,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.ProductionMonitorGridView.GridControl = this.ProductionMonitorGrid;
            this.ProductionMonitorGridView.Name = "ProductionMonitorGridView";
            this.ProductionMonitorGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.ProductionMonitorGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.ProductionMonitorGridView.OptionsBehavior.ReadOnly = true;
            this.ProductionMonitorGridView.OptionsFind.AlwaysVisible = true;
            this.ProductionMonitorGridView.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.ProductionMonitorGridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.ProductionMonitorGridView.OptionsView.ShowFooter = true;
            this.ProductionMonitorGridView.OptionsView.ShowGroupPanel = false;
            this.ProductionMonitorGridView.RowHeight = 25;
            this.ProductionMonitorGridView.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.ProductionMonitorGridView_CustomDrawCell);
            this.ProductionMonitorGridView.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.ProductionMonitorGridView_RowStyle);
            this.ProductionMonitorGridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.ProductionMonitorGridView_PopupMenuShowing);
            // 
            // colNum
            // 
            this.colNum.Caption = "№ виробництва";
            this.colNum.FieldName = "Num";
            this.colNum.Name = "colNum";
            this.colNum.Visible = true;
            this.colNum.VisibleIndex = 0;
            this.colNum.Width = 144;
            // 
            // colOnDate
            // 
            this.colOnDate.Caption = "Дата виробництва";
            this.colOnDate.DisplayFormat.FormatString = "d";
            this.colOnDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colOnDate.FieldName = "OnDate";
            this.colOnDate.Name = "colOnDate";
            this.colOnDate.Visible = true;
            this.colOnDate.VisibleIndex = 1;
            this.colOnDate.Width = 138;
            // 
            // colTechProcessName
            // 
            this.colTechProcessName.Caption = "Технологічний процес";
            this.colTechProcessName.FieldName = "TechProcessName";
            this.colTechProcessName.Name = "colTechProcessName";
            this.colTechProcessName.Visible = true;
            this.colTechProcessName.VisibleIndex = 6;
            this.colTechProcessName.Width = 211;
            // 
            // colTechProcessStartDate
            // 
            this.colTechProcessStartDate.Caption = "Початок техпроцесу";
            this.colTechProcessStartDate.DisplayFormat.FormatString = "t";
            this.colTechProcessStartDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTechProcessStartDate.FieldName = "TechProcessStartDate";
            this.colTechProcessStartDate.Name = "colTechProcessStartDate";
            this.colTechProcessStartDate.Visible = true;
            this.colTechProcessStartDate.VisibleIndex = 3;
            this.colTechProcessStartDate.Width = 110;
            // 
            // colManufacturingTime
            // 
            this.colManufacturingTime.Caption = "Закінчення техпроцесу";
            this.colManufacturingTime.DisplayFormat.FormatString = "t";
            this.colManufacturingTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colManufacturingTime.FieldName = "TechProcessEndDate";
            this.colManufacturingTime.Name = "colManufacturingTime";
            this.colManufacturingTime.Visible = true;
            this.colManufacturingTime.VisibleIndex = 4;
            this.colManufacturingTime.Width = 110;
            // 
            // colMatName
            // 
            this.colMatName.Caption = "Рецепт";
            this.colMatName.FieldName = "MatName";
            this.colMatName.Name = "colMatName";
            this.colMatName.Visible = true;
            this.colMatName.VisibleIndex = 2;
            this.colMatName.Width = 353;
            // 
            // colWhName
            // 
            this.colWhName.Caption = "Цех";
            this.colWhName.FieldName = "WhName";
            this.colWhName.Name = "colWhName";
            this.colWhName.Width = 215;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Стан";
            this.gridColumn1.ColumnEdit = this.repositoryItemProgressBar1;
            this.gridColumn1.FieldName = "Pct";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 8;
            this.gridColumn1.Width = 211;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.EndColor = System.Drawing.Color.Empty;
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            this.repositoryItemProgressBar1.ShowTitle = true;
            this.repositoryItemProgressBar1.StartColor = System.Drawing.Color.Empty;
            this.repositoryItemProgressBar1.Step = 1;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Тривалість";
            this.gridColumn2.FieldName = "Duration";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.UnboundDataType = typeof(System.TimeSpan);
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 5;
            this.gridColumn2.Width = 112;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Рама";
            this.gridColumn3.FieldName = "RamaName";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 7;
            this.gridColumn3.Width = 71;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // toastNotificationsManager1
            // 
            this.toastNotificationsManager1.ApplicationId = "6e72351a-d7e6-4b1a-9073-cf2f678a2e68";
            this.toastNotificationsManager1.ApplicationName = "SP-Sklad";
            this.toastNotificationsManager1.Notifications.AddRange(new DevExpress.XtraBars.ToastNotifications.IToastNotificationProperties[] {
            new DevExpress.XtraBars.ToastNotifications.ToastNotification("1a7e37b8-6aba-4e9c-9a03-28d08312b44e", global::SP_Sklad.Properties.Resources.info_32x32, "Інтенсивка", "Тест", "Тест", DevExpress.XtraBars.ToastNotifications.ToastNotificationSound.Reminder, DevExpress.XtraBars.ToastNotifications.ToastNotificationDuration.Default, DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.ImageAndText04)});
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 600000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl1);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl2);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl3);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl4);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl5);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl6);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl7);
            this.barManager1.DockControls.Add(this.standaloneBarDockControl8);
            this.barManager1.Form = this;
            this.barManager1.Images = this.BarImageList;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.PrintItemBtn,
            this.EditTechProcBtn,
            this.DelTechProcBtn,
            this.barButtonItem1});
            this.barManager1.MaxItemId = 26;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1485, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 580);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1485, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 580);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1485, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 580);
            // 
            // standaloneBarDockControl1
            // 
            this.standaloneBarDockControl1.AutoSize = true;
            this.standaloneBarDockControl1.CausesValidation = false;
            this.standaloneBarDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl1.Location = new System.Drawing.Point(0, 23);
            this.standaloneBarDockControl1.Manager = this.barManager1;
            this.standaloneBarDockControl1.Name = "standaloneBarDockControl1";
            this.standaloneBarDockControl1.Size = new System.Drawing.Size(1485, 0);
            this.standaloneBarDockControl1.Text = "standaloneBarDockControl1";
            // 
            // standaloneBarDockControl2
            // 
            this.standaloneBarDockControl2.AutoSize = true;
            this.standaloneBarDockControl2.CausesValidation = false;
            this.standaloneBarDockControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl2.Location = new System.Drawing.Point(0, 23);
            this.standaloneBarDockControl2.Manager = this.barManager1;
            this.standaloneBarDockControl2.Name = "standaloneBarDockControl2";
            this.standaloneBarDockControl2.Size = new System.Drawing.Size(1485, 0);
            this.standaloneBarDockControl2.Text = "standaloneBarDockControl2";
            // 
            // standaloneBarDockControl3
            // 
            this.standaloneBarDockControl3.AutoSize = true;
            this.standaloneBarDockControl3.CausesValidation = false;
            this.standaloneBarDockControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl3.Location = new System.Drawing.Point(0, 23);
            this.standaloneBarDockControl3.Manager = this.barManager1;
            this.standaloneBarDockControl3.Name = "standaloneBarDockControl3";
            this.standaloneBarDockControl3.Size = new System.Drawing.Size(1485, 0);
            this.standaloneBarDockControl3.Text = "standaloneBarDockControl3";
            // 
            // standaloneBarDockControl4
            // 
            this.standaloneBarDockControl4.AutoSize = true;
            this.standaloneBarDockControl4.CausesValidation = false;
            this.standaloneBarDockControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl4.Location = new System.Drawing.Point(0, 23);
            this.standaloneBarDockControl4.Manager = this.barManager1;
            this.standaloneBarDockControl4.Name = "standaloneBarDockControl4";
            this.standaloneBarDockControl4.Size = new System.Drawing.Size(1485, 0);
            this.standaloneBarDockControl4.Text = "standaloneBarDockControl4";
            // 
            // standaloneBarDockControl5
            // 
            this.standaloneBarDockControl5.AutoSize = true;
            this.standaloneBarDockControl5.CausesValidation = false;
            this.standaloneBarDockControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl5.Location = new System.Drawing.Point(0, 23);
            this.standaloneBarDockControl5.Manager = this.barManager1;
            this.standaloneBarDockControl5.Name = "standaloneBarDockControl5";
            this.standaloneBarDockControl5.Size = new System.Drawing.Size(1485, 0);
            this.standaloneBarDockControl5.Text = "standaloneBarDockControl5";
            // 
            // standaloneBarDockControl6
            // 
            this.standaloneBarDockControl6.AutoSize = true;
            this.standaloneBarDockControl6.CausesValidation = false;
            this.standaloneBarDockControl6.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl6.Location = new System.Drawing.Point(0, 23);
            this.standaloneBarDockControl6.Manager = this.barManager1;
            this.standaloneBarDockControl6.Name = "standaloneBarDockControl6";
            this.standaloneBarDockControl6.Size = new System.Drawing.Size(1485, 0);
            this.standaloneBarDockControl6.Text = "standaloneBarDockControl6";
            // 
            // standaloneBarDockControl7
            // 
            this.standaloneBarDockControl7.AutoSize = true;
            this.standaloneBarDockControl7.CausesValidation = false;
            this.standaloneBarDockControl7.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl7.Location = new System.Drawing.Point(0, 23);
            this.standaloneBarDockControl7.Manager = this.barManager1;
            this.standaloneBarDockControl7.Name = "standaloneBarDockControl7";
            this.standaloneBarDockControl7.Size = new System.Drawing.Size(1485, 0);
            this.standaloneBarDockControl7.Text = "standaloneBarDockControl7";
            // 
            // standaloneBarDockControl8
            // 
            this.standaloneBarDockControl8.CausesValidation = false;
            this.standaloneBarDockControl8.Dock = System.Windows.Forms.DockStyle.Top;
            this.standaloneBarDockControl8.Location = new System.Drawing.Point(0, 0);
            this.standaloneBarDockControl8.Manager = this.barManager1;
            this.standaloneBarDockControl8.Name = "standaloneBarDockControl8";
            this.standaloneBarDockControl8.Size = new System.Drawing.Size(1485, 23);
            this.standaloneBarDockControl8.Text = "standaloneBarDockControl8";
            // 
            // PrintItemBtn
            // 
            this.PrintItemBtn.Caption = "Друк/Попередній перегляд";
            this.PrintItemBtn.Id = 6;
            this.PrintItemBtn.ImageOptions.ImageIndex = 6;
            this.PrintItemBtn.Name = "PrintItemBtn";
            this.PrintItemBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.PrintItemBtn_ItemClick);
            // 
            // EditTechProcBtn
            // 
            this.EditTechProcBtn.Caption = "Властивості";
            this.EditTechProcBtn.Id = 8;
            this.EditTechProcBtn.ImageOptions.ImageIndex = 2;
            this.EditTechProcBtn.Name = "EditTechProcBtn";
            // 
            // DelTechProcBtn
            // 
            this.DelTechProcBtn.Caption = "Видалити ";
            this.DelTechProcBtn.Id = 9;
            this.DelTechProcBtn.ImageOptions.ImageIndex = 3;
            this.DelTechProcBtn.Name = "DelTechProcBtn";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Перейти до документа";
            this.barButtonItem1.Id = 12;
            this.barButtonItem1.ImageOptions.ImageIndex = 9;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // BottomPopupMenu
            // 
            this.BottomPopupMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.PrintItemBtn)});
            this.BottomPopupMenu.Manager = this.barManager1;
            this.BottomPopupMenu.Name = "BottomPopupMenu";
            // 
            // BarImageList
            // 
            this.BarImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("BarImageList.ImageStream")));
            this.BarImageList.TransparentColor = System.Drawing.Color.White;
            this.BarImageList.Images.SetKeyName(0, "Add.bmp");
            this.BarImageList.Images.SetKeyName(1, "Copy.bmp");
            this.BarImageList.Images.SetKeyName(2, "edit.bmp");
            this.BarImageList.Images.SetKeyName(3, "Delete.bmp");
            this.BarImageList.Images.SetKeyName(4, "refresh.bmp");
            this.BarImageList.Images.SetKeyName(5, "Провести документ.bmp");
            this.BarImageList.Images.SetKeyName(6, "Попередн_й перегляд.bmp");
            this.BarImageList.Images.SetKeyName(7, "Склади.bmp");
            this.BarImageList.Images.SetKeyName(8, "Замовлене кл_єнтами.bmp");
            this.BarImageList.Images.SetKeyName(9, "Перейти до  документа.bmp");
            this.BarImageList.Images.SetKeyName(10, "weighing-scale.png");
            // 
            // frmProductionMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1485, 580);
            this.Controls.Add(this.standaloneBarDockControl1);
            this.Controls.Add(this.standaloneBarDockControl2);
            this.Controls.Add(this.standaloneBarDockControl3);
            this.Controls.Add(this.standaloneBarDockControl4);
            this.Controls.Add(this.standaloneBarDockControl5);
            this.Controls.Add(this.standaloneBarDockControl6);
            this.Controls.Add(this.standaloneBarDockControl7);
            this.Controls.Add(this.standaloneBarDockControl8);
            this.Controls.Add(this.ProductionMonitorGrid);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("frmProductionMonitor.IconOptions.Image")));
            this.Name = "frmProductionMonitor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Монітор виробництва";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProductionMonitor_FormClosed);
            this.Load += new System.EventHandler(this.frmKaGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductionMonitorGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toastNotificationsManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPopupMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.GridControl ProductionMonitorGrid;
        public DevExpress.XtraGrid.Views.Grid.GridView ProductionMonitorGridView;
        private System.Windows.Forms.BindingSource ProductionMonitorBS;
        private DevExpress.XtraGrid.Columns.GridColumn colNum;
        private DevExpress.XtraGrid.Columns.GridColumn colOnDate;
        private DevExpress.XtraGrid.Columns.GridColumn colTechProcessName;
        private DevExpress.XtraGrid.Columns.GridColumn colTechProcessStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colManufacturingTime;
        private DevExpress.XtraGrid.Columns.GridColumn colMatName;
        private DevExpress.XtraGrid.Columns.GridColumn colWhName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager toastNotificationsManager1;
        private System.Windows.Forms.Timer timer2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem PrintItemBtn;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl2;
        private DevExpress.XtraBars.BarButtonItem EditTechProcBtn;
        private DevExpress.XtraBars.BarButtonItem DelTechProcBtn;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl3;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl6;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl7;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl8;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl1;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl4;
        private DevExpress.XtraBars.StandaloneBarDockControl standaloneBarDockControl5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.PopupMenu BottomPopupMenu;
        public System.Windows.Forms.ImageList BarImageList;
    }
}