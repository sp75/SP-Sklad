namespace SP_Sklad
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.UserTreeImgList = new System.Windows.Forms.ImageList(this.components);
            this.GridImageList = new System.Windows.Forms.ImageList(this.components);
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem2 = new DevExpress.XtraBars.BarSubItem();
            this.CurDateEditBarItem = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTimeEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit();
            this.barEditItem2 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.gridSplitContainer1 = new DevExpress.XtraGrid.GridSplitContainer();
            this.xtraTabPage7 = new DevExpress.XtraTab.XtraTabPage();
            this.serviceUserControl1 = new SP_Sklad.MainTabs.ServiceUserControl();
            this.xtraTabPage6 = new DevExpress.XtraTab.XtraTabPage();
            this.directoriesUserControl1 = new SP_Sklad.MainTabs.DirectoriesUserControl();
            this.xtraTabPage5 = new DevExpress.XtraTab.XtraTabPage();
            this.reportUserControl1 = new SP_Sklad.MainTabs.ReportUserControl();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.whUserControl = new SP_Sklad.MainTabs.WarehouseUserControl();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.manufacturingUserControl1 = new SP_Sklad.MainTabs.ManufacturingUserControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.docsUserControl1 = new SP_Sklad.MainTabs.DocsUserControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.sharedImageCollection1 = new DevExpress.Utils.SharedImageCollection(this.components);
            this.financesUserControl1 = new SP_Sklad.MainTabs.FinancesUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).BeginInit();
            this.gridSplitContainer1.SuspendLayout();
            this.xtraTabPage7.SuspendLayout();
            this.xtraTabPage6.SuspendLayout();
            this.xtraTabPage5.SuspendLayout();
            this.xtraTabPage4.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1.ImageSource)).BeginInit();
            this.SuspendLayout();
            // 
            // UserTreeImgList
            // 
            this.UserTreeImgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("UserTreeImgList.ImageStream")));
            this.UserTreeImgList.TransparentColor = System.Drawing.Color.White;
            this.UserTreeImgList.Images.SetKeyName(0, "Склад+Торговля.bmp");
            this.UserTreeImgList.Images.SetKeyName(1, "Документы16x16.bmp");
            this.UserTreeImgList.Images.SetKeyName(2, "OpenFolder.bmp");
            this.UserTreeImgList.Images.SetKeyName(3, "ПриходНакл.bmp");
            this.UserTreeImgList.Images.SetKeyName(4, "Счета.bmp");
            this.UserTreeImgList.Images.SetKeyName(5, "РасходНакл.bmp");
            this.UserTreeImgList.Images.SetKeyName(6, "податкова накладна.bmp");
            this.UserTreeImgList.Images.SetKeyName(7, "Счет-фактуры.bmp");
            this.UserTreeImgList.Images.SetKeyName(8, "ВходПлатежи.bmp");
            this.UserTreeImgList.Images.SetKeyName(9, "ИсходПлатежи.bmp");
            this.UserTreeImgList.Images.SetKeyName(10, "ДопРасход.bmp");
            this.UserTreeImgList.Images.SetKeyName(11, "Возврат Поставщику.bmp");
            this.UserTreeImgList.Images.SetKeyName(12, "Возврат от клиетна.bmp");
            this.UserTreeImgList.Images.SetKeyName(13, "Заказ от клиента.bmp");
            this.UserTreeImgList.Images.SetKeyName(14, "Заказ поставщикам.bmp");
            this.UserTreeImgList.Images.SetKeyName(15, "Договор купли.bmp");
            this.UserTreeImgList.Images.SetKeyName(16, "Договор продажу.bmp");
            this.UserTreeImgList.Images.SetKeyName(17, "Прайс лист.bmp");
            this.UserTreeImgList.Images.SetKeyName(18, "Склади.bmp");
            this.UserTreeImgList.Images.SetKeyName(19, "Накладн_ перем_щення.bmp");
            this.UserTreeImgList.Images.SetKeyName(20, "Введення залишк_в товар_в.bmp");
            this.UserTreeImgList.Images.SetKeyName(21, "Акти списання товару.bmp");
            this.UserTreeImgList.Images.SetKeyName(22, "Акти iнвентаризацiї.bmp");
            this.UserTreeImgList.Images.SetKeyName(23, "Отчеты.bmp");
            this.UserTreeImgList.Images.SetKeyName(24, "Справочники.bmp");
            this.UserTreeImgList.Images.SetKeyName(25, "Конрагент.bmp");
            this.UserTreeImgList.Images.SetKeyName(26, "Товари.bmp");
            this.UserTreeImgList.Images.SetKeyName(27, "Послуги.bmp");
            this.UserTreeImgList.Images.SetKeyName(28, "korganizer.png");
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
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar2,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barSubItem1,
            this.barButtonItem1,
            this.barButtonItem2,
            this.barSubItem2,
            this.barEditItem1,
            this.barEditItem2,
            this.CurDateEditBarItem,
            this.barButtonItem3});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 8;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTimeEdit1,
            this.repositoryItemCalcEdit1,
            this.repositoryItemDateEdit});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.Text = "Tools";
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.CurDateEditBarItem),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3)});
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "barSubItem1";
            this.barSubItem1.Id = 0;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "barButtonItem1";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "barButtonItem2";
            this.barButtonItem2.Id = 2;
            this.barButtonItem2.Name = "barButtonItem2";
            // 
            // barSubItem2
            // 
            this.barSubItem2.Caption = "barSubItem2";
            this.barSubItem2.Id = 3;
            this.barSubItem2.Name = "barSubItem2";
            // 
            // CurDateEditBarItem
            // 
            this.CurDateEditBarItem.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.CurDateEditBarItem.Caption = "CurDateEditItem";
            this.CurDateEditBarItem.Edit = this.repositoryItemDateEdit;
            this.CurDateEditBarItem.EditWidth = 150;
            this.CurDateEditBarItem.Id = 6;
            this.CurDateEditBarItem.Name = "CurDateEditBarItem";
            // 
            // repositoryItemDateEdit
            // 
            this.repositoryItemDateEdit.AutoHeight = false;
            this.repositoryItemDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit.Name = "repositoryItemDateEdit";
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "barButtonItem3";
            this.barButtonItem3.Id = 7;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(1188, 49);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 679);
            this.barDockControlBottom.Size = new System.Drawing.Size(1188, 21);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 49);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 630);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1188, 49);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 630);
            // 
            // barEditItem1
            // 
            this.barEditItem1.Caption = "barEditItem1";
            this.barEditItem1.Edit = this.repositoryItemTimeEdit1;
            this.barEditItem1.Id = 4;
            this.barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemTimeEdit1
            // 
            this.repositoryItemTimeEdit1.AutoHeight = false;
            this.repositoryItemTimeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemTimeEdit1.Name = "repositoryItemTimeEdit1";
            // 
            // barEditItem2
            // 
            this.barEditItem2.Caption = "barEditItem2";
            this.barEditItem2.Edit = this.repositoryItemCalcEdit1;
            this.barEditItem2.EditWidth = 150;
            this.barEditItem2.Id = 5;
            this.barEditItem2.Name = "barEditItem2";
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // defaultLookAndFeel1
            // 
            this.defaultLookAndFeel1.LookAndFeel.SkinName = "Office 2010 Silver";
            // 
            // gridSplitContainer1
            // 
            this.gridSplitContainer1.Grid = null;
            this.gridSplitContainer1.Location = new System.Drawing.Point(79, 32);
            this.gridSplitContainer1.Name = "gridSplitContainer1";
            this.gridSplitContainer1.Size = new System.Drawing.Size(400, 200);
            this.gridSplitContainer1.TabIndex = 0;
            // 
            // xtraTabPage7
            // 
            this.xtraTabPage7.Controls.Add(this.serviceUserControl1);
            this.xtraTabPage7.Image = global::SP_Sklad.Properties.Resources._1324534473_police;
            this.xtraTabPage7.Name = "xtraTabPage7";
            this.xtraTabPage7.Size = new System.Drawing.Size(1182, 578);
            this.xtraTabPage7.TabPageWidth = 100;
            this.xtraTabPage7.Text = "Сервіс";
            // 
            // serviceUserControl1
            // 
            this.serviceUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serviceUserControl1.Location = new System.Drawing.Point(0, 0);
            this.serviceUserControl1.Name = "serviceUserControl1";
            this.serviceUserControl1.Size = new System.Drawing.Size(1182, 578);
            this.serviceUserControl1.TabIndex = 0;
            // 
            // xtraTabPage6
            // 
            this.xtraTabPage6.Controls.Add(this.directoriesUserControl1);
            this.xtraTabPage6.Image = global::SP_Sklad.Properties.Resources._1324534381_findjob;
            this.xtraTabPage6.Name = "xtraTabPage6";
            this.xtraTabPage6.Size = new System.Drawing.Size(1182, 578);
            this.xtraTabPage6.TabPageWidth = 100;
            this.xtraTabPage6.Text = "Довідники";
            // 
            // directoriesUserControl1
            // 
            this.directoriesUserControl1.custom_mat_list = null;
            this.directoriesUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directoriesUserControl1.isDirectList = false;
            this.directoriesUserControl1.isMatList = false;
            this.directoriesUserControl1.Location = new System.Drawing.Point(0, 0);
            this.directoriesUserControl1.Name = "directoriesUserControl1";
            this.directoriesUserControl1.Size = new System.Drawing.Size(1182, 578);
            this.directoriesUserControl1.TabIndex = 0;
            this.directoriesUserControl1.wb = null;
            // 
            // xtraTabPage5
            // 
            this.xtraTabPage5.Controls.Add(this.reportUserControl1);
            this.xtraTabPage5.Image = global::SP_Sklad.Properties.Resources.company;
            this.xtraTabPage5.Name = "xtraTabPage5";
            this.xtraTabPage5.Size = new System.Drawing.Size(1182, 578);
            this.xtraTabPage5.TabPageWidth = 100;
            this.xtraTabPage5.Text = "Звіти";
            // 
            // reportUserControl1
            // 
            this.reportUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportUserControl1.Location = new System.Drawing.Point(0, 0);
            this.reportUserControl1.Name = "reportUserControl1";
            this.reportUserControl1.Size = new System.Drawing.Size(1182, 578);
            this.reportUserControl1.TabIndex = 0;
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.financesUserControl1);
            this.xtraTabPage4.Image = global::SP_Sklad.Properties.Resources.bank;
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(1182, 578);
            this.xtraTabPage4.TabPageWidth = 100;
            this.xtraTabPage4.Text = "Фінанси";
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.whUserControl);
            this.xtraTabPage3.Image = global::SP_Sklad.Properties.Resources._1324534557_mine_копия;
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(1182, 578);
            this.xtraTabPage3.TabPageWidth = 100;
            this.xtraTabPage3.Text = "Склад";
            // 
            // whUserControl
            // 
            this.whUserControl.custom_mat_list = null;
            this.whUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.whUserControl.isDirectList = false;
            this.whUserControl.isMatList = false;
            this.whUserControl.Location = new System.Drawing.Point(0, 0);
            this.whUserControl.Name = "whUserControl";
            this.whUserControl.resut = null;
            this.whUserControl.Size = new System.Drawing.Size(1182, 578);
            this.whUserControl.TabIndex = 0;
            this.whUserControl.wb = null;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.manufacturingUserControl1);
            this.xtraTabPage2.Image = global::SP_Sklad.Properties.Resources.factory;
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(1182, 578);
            this.xtraTabPage2.TabPageWidth = 100;
            this.xtraTabPage2.Text = "Виробництво";
            // 
            // manufacturingUserControl1
            // 
            this.manufacturingUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manufacturingUserControl1.Location = new System.Drawing.Point(0, 0);
            this.manufacturingUserControl1.Name = "manufacturingUserControl1";
            this.manufacturingUserControl1.Size = new System.Drawing.Size(1182, 578);
            this.manufacturingUserControl1.TabIndex = 0;
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.docsUserControl1);
            this.xtraTabPage1.Image = global::SP_Sklad.Properties.Resources.administration;
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(1182, 578);
            this.xtraTabPage1.TabPageWidth = 100;
            this.xtraTabPage1.Text = "Документи";
            // 
            // docsUserControl1
            // 
            this.docsUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.docsUserControl1.Location = new System.Drawing.Point(0, 0);
            this.docsUserControl1.Name = "docsUserControl1";
            this.docsUserControl1.Size = new System.Drawing.Size(1182, 578);
            this.docsUserControl1.TabIndex = 0;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 49);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(1188, 630);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3,
            this.xtraTabPage4,
            this.xtraTabPage5,
            this.xtraTabPage6,
            this.xtraTabPage7});
            // 
            // sharedImageCollection1
            // 
            // 
            // 
            // 
            this.sharedImageCollection1.ImageSource.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("sharedImageCollection1.ImageSource.ImageStream")));
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(0, "_нформац_я про товар.bmp");
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(1, "Рух товар_в.bmp");
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(2, "_нформац_я про резерв товару.bmp");
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(3, "Аналоги.bmp");
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(4, "Заказ поставщикам.bmp");
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(5, "view_settings.bmp");
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(6, "Наявн_сть на сладах.bmp");
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(7, "Парт_ї.bmp");
            this.sharedImageCollection1.ImageSource.Images.SetKeyName(8, "Склади.bmp");
            this.sharedImageCollection1.ParentControl = this;
            // 
            // financesUserControl1
            // 
            this.financesUserControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.financesUserControl1.Location = new System.Drawing.Point(0, 0);
            this.financesUserControl1.Name = "financesUserControl1";
            this.financesUserControl1.Size = new System.Drawing.Size(1182, 578);
            this.financesUserControl1.TabIndex = 0;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 700);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "mainForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTimeEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSplitContainer1)).EndInit();
            this.gridSplitContainer1.ResumeLayout(false);
            this.xtraTabPage7.ResumeLayout(false);
            this.xtraTabPage6.ResumeLayout(false);
            this.xtraTabPage5.ResumeLayout(false);
            this.xtraTabPage4.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1.ImageSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sharedImageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarSubItem barSubItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarSubItem barSubItem2;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTimeEdit repositoryItemTimeEdit1;
        private DevExpress.XtraBars.BarEditItem barEditItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
        private DevExpress.XtraBars.BarEditItem CurDateEditBarItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit;
        private DevExpress.XtraGrid.GridSplitContainer gridSplitContainer1;
        public System.Windows.Forms.ImageList UserTreeImgList;
        public System.Windows.Forms.ImageList GridImageList;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage5;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage6;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage7;
        private MainTabs.DocsUserControl docsUserControl1;
        private MainTabs.WarehouseUserControl whUserControl;
        private MainTabs.ManufacturingUserControl manufacturingUserControl1;
        private MainTabs.DirectoriesUserControl directoriesUserControl1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private MainTabs.ReportUserControl reportUserControl1;
        private DevExpress.Utils.SharedImageCollection sharedImageCollection1;
        private MainTabs.ServiceUserControl serviceUserControl1;
        private MainTabs.FinancesUserControl financesUserControl1;
    }
}

