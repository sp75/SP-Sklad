namespace SP_Sklad.UserControls
{
    partial class ucDocumentPaymentGrid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDocumentPaymentGrid));
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
            this.DocumentPaymentGridControl = new DevExpress.XtraGrid.GridControl();
            this.DocumentPaymentGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn89 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox30 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.gridColumn91 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox31 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridColumn92 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn93 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn96 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn97 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn98 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn100 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn101 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageEdit();
            this.repositoryItemImageComboBox32 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPopupMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentPaymentGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentPaymentGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox32)).BeginInit();
            this.SuspendLayout();
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
            this.RefrechItemBtn.ImageOptions.ImageIndex = 0;
            this.RefrechItemBtn.Name = "RefrechItemBtn";
            // 
            // MoveToDocBtn
            // 
            this.MoveToDocBtn.Caption = "Перейти до документа";
            this.MoveToDocBtn.Id = 12;
            this.MoveToDocBtn.ImageOptions.ImageIndex = 2;
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
            // DocumentPaymentGridControl
            // 
            this.DocumentPaymentGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentPaymentGridControl.Location = new System.Drawing.Point(0, 0);
            this.DocumentPaymentGridControl.MainView = this.DocumentPaymentGridView;
            this.DocumentPaymentGridControl.Name = "DocumentPaymentGridControl";
            this.DocumentPaymentGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageEdit1,
            this.repositoryItemImageComboBox32,
            this.repositoryItemImageComboBox31,
            this.repositoryItemImageComboBox30});
            this.DocumentPaymentGridControl.Size = new System.Drawing.Size(948, 329);
            this.DocumentPaymentGridControl.TabIndex = 6;
            this.DocumentPaymentGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DocumentPaymentGridView});
            // 
            // DocumentPaymentGridView
            // 
            this.DocumentPaymentGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.DocumentPaymentGridView.Appearance.Row.Options.UseFont = true;
            this.DocumentPaymentGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn89,
            this.gridColumn91,
            this.gridColumn92,
            this.gridColumn93,
            this.gridColumn96,
            this.gridColumn97,
            this.gridColumn98,
            this.gridColumn100,
            this.gridColumn101});
            this.DocumentPaymentGridView.GridControl = this.DocumentPaymentGridControl;
            this.DocumentPaymentGridView.Name = "DocumentPaymentGridView";
            this.DocumentPaymentGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.DocumentPaymentGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.DocumentPaymentGridView.OptionsBehavior.ReadOnly = true;
            this.DocumentPaymentGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.DocumentPaymentGridView.OptionsView.EnableAppearanceOddRow = true;
            this.DocumentPaymentGridView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn89
            // 
            this.gridColumn89.ColumnEdit = this.repositoryItemImageComboBox30;
            this.gridColumn89.FieldName = "DocType";
            this.gridColumn89.Name = "gridColumn89";
            this.gridColumn89.OptionsColumn.AllowFocus = false;
            this.gridColumn89.OptionsColumn.AllowSize = false;
            this.gridColumn89.OptionsColumn.FixedWidth = true;
            this.gridColumn89.OptionsColumn.ShowCaption = false;
            this.gridColumn89.Visible = true;
            this.gridColumn89.VisibleIndex = 0;
            this.gridColumn89.Width = 25;
            // 
            // repositoryItemImageComboBox30
            // 
            this.repositoryItemImageComboBox30.AutoHeight = false;
            this.repositoryItemImageComboBox30.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox30.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -1, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 3)});
            this.repositoryItemImageComboBox30.Name = "repositoryItemImageComboBox30";
            this.repositoryItemImageComboBox30.SmallImages = this.imageCollection1;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.new_document, "new_document", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.imageCollection1.Images.SetKeyName(0, "new_document");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.checked_green, "checked_green", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.imageCollection1.Images.SetKeyName(1, "checked_green");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.pay_doc_out, "pay_doc_out", typeof(global::SP_Sklad.Properties.Resources), 2);
            this.imageCollection1.Images.SetKeyName(2, "pay_doc_out");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.pay_doc_in, "pay_doc_in", typeof(global::SP_Sklad.Properties.Resources), 3);
            this.imageCollection1.Images.SetKeyName(3, "pay_doc_in");
            // 
            // gridColumn91
            // 
            this.gridColumn91.Caption = " ";
            this.gridColumn91.ColumnEdit = this.repositoryItemImageComboBox31;
            this.gridColumn91.FieldName = "Checked";
            this.gridColumn91.Name = "gridColumn91";
            this.gridColumn91.OptionsColumn.AllowFocus = false;
            this.gridColumn91.OptionsColumn.AllowSize = false;
            this.gridColumn91.OptionsColumn.FixedWidth = true;
            this.gridColumn91.OptionsColumn.ShowCaption = false;
            this.gridColumn91.Visible = true;
            this.gridColumn91.VisibleIndex = 1;
            this.gridColumn91.Width = 25;
            // 
            // repositoryItemImageComboBox31
            // 
            this.repositoryItemImageComboBox31.AutoHeight = false;
            this.repositoryItemImageComboBox31.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox31.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 1)});
            this.repositoryItemImageComboBox31.Name = "repositoryItemImageComboBox31";
            this.repositoryItemImageComboBox31.SmallImages = this.imageCollection1;
            // 
            // gridColumn92
            // 
            this.gridColumn92.Caption = "№";
            this.gridColumn92.FieldName = "DocNum";
            this.gridColumn92.Name = "gridColumn92";
            this.gridColumn92.Visible = true;
            this.gridColumn92.VisibleIndex = 2;
            this.gridColumn92.Width = 68;
            // 
            // gridColumn93
            // 
            this.gridColumn93.Caption = "Дата";
            this.gridColumn93.FieldName = "OnDate";
            this.gridColumn93.Name = "gridColumn93";
            this.gridColumn93.Visible = true;
            this.gridColumn93.VisibleIndex = 3;
            this.gridColumn93.Width = 93;
            // 
            // gridColumn96
            // 
            this.gridColumn96.Caption = "Стаття витрат";
            this.gridColumn96.FieldName = "ChargeName";
            this.gridColumn96.Name = "gridColumn96";
            this.gridColumn96.Visible = true;
            this.gridColumn96.VisibleIndex = 4;
            this.gridColumn96.Width = 103;
            // 
            // gridColumn97
            // 
            this.gridColumn97.Caption = "Каса";
            this.gridColumn97.FieldName = "CashdName";
            this.gridColumn97.Name = "gridColumn97";
            this.gridColumn97.Visible = true;
            this.gridColumn97.VisibleIndex = 6;
            this.gridColumn97.Width = 94;
            // 
            // gridColumn98
            // 
            this.gridColumn98.Caption = "Сума";
            this.gridColumn98.FieldName = "Total";
            this.gridColumn98.Name = "gridColumn98";
            this.gridColumn98.Visible = true;
            this.gridColumn98.VisibleIndex = 8;
            this.gridColumn98.Width = 66;
            // 
            // gridColumn100
            // 
            this.gridColumn100.Caption = "Рахунок";
            this.gridColumn100.FieldName = "AccNum";
            this.gridColumn100.Name = "gridColumn100";
            this.gridColumn100.Visible = true;
            this.gridColumn100.VisibleIndex = 7;
            this.gridColumn100.Width = 126;
            // 
            // gridColumn101
            // 
            this.gridColumn101.Caption = "Вид оплати";
            this.gridColumn101.FieldName = "PayTypeName";
            this.gridColumn101.Name = "gridColumn101";
            this.gridColumn101.Visible = true;
            this.gridColumn101.VisibleIndex = 5;
            this.gridColumn101.Width = 69;
            // 
            // repositoryItemImageEdit1
            // 
            this.repositoryItemImageEdit1.AutoHeight = false;
            this.repositoryItemImageEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageEdit1.Name = "repositoryItemImageEdit1";
            // 
            // repositoryItemImageComboBox32
            // 
            this.repositoryItemImageComboBox32.AutoHeight = false;
            this.repositoryItemImageComboBox32.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox32.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -1, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 6, 8),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -6, 7),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -16, 9),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 16, 10)});
            this.repositoryItemImageComboBox32.Name = "repositoryItemImageComboBox32";
            // 
            // ucDocumentPaymentGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DocumentPaymentGridControl);
            this.Controls.Add(this.standaloneBarDockControl1);
            this.Controls.Add(this.standaloneBarDockControl2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ucDocumentPaymentGrid";
            this.Size = new System.Drawing.Size(948, 329);
            this.Load += new System.EventHandler(this.ucDocumentPaymentGrid_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPopupMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentPaymentGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentPaymentGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox32)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private DevExpress.XtraGrid.GridControl DocumentPaymentGridControl;
        public DevExpress.XtraGrid.Views.Grid.GridView DocumentPaymentGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn89;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn91;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox31;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn92;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn93;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn96;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn97;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn98;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn100;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn101;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageEdit repositoryItemImageEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox32;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
