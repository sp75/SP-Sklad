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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.colNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSummAll = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSummInCurr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSummPay = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocumentGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 516);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1372, 52);
            this.BottomPanel.TabIndex = 18;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(1150, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вибрати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(1260, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // DocumentGridControl
            // 
            this.DocumentGridControl.DataSource = this.KagentListSource;
            this.DocumentGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentGridControl.Location = new System.Drawing.Point(0, 0);
            this.DocumentGridControl.MainView = this.DocumentGridView;
            this.DocumentGridControl.Name = "DocumentGridControl";
            this.DocumentGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.DocumentGridControl.Size = new System.Drawing.Size(1372, 516);
            this.DocumentGridControl.TabIndex = 19;
            this.DocumentGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.DocumentGridView});
            this.DocumentGridControl.Click += new System.EventHandler(this.DocumentGridControl_Click);
            // 
            // KagentListSource
            // 
            this.KagentListSource.AreSourceRowsThreadSafe = true;
            this.KagentListSource.DefaultSorting = "OnDate DESC";
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
            this.colSummAll,
            this.colSummInCurr,
            this.colSummPay});
            this.DocumentGridView.GridControl = this.DocumentGridControl;
            this.DocumentGridView.Name = "DocumentGridView";
            this.DocumentGridView.OptionsBehavior.ReadOnly = true;
            this.DocumentGridView.OptionsFind.AlwaysVisible = true;
            this.DocumentGridView.OptionsView.ShowAutoFilterRow = true;
            this.DocumentGridView.OptionsView.ShowDetailButtons = false;
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
            this.colWType.Width = 259;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Видаткова накладна", -1, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Прибуткова накладна", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Прибутковий касовий ордер", 3, 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Видатковий касовий ордер", -3, 5),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Рахунок", 2, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Додаткові витрати", -2, 6),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Акти списання товару", -5, 10),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Акти наданих послуг", 29, 14)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.imageList1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "ПриходНакл.bmp");
            this.imageList1.Images.SetKeyName(1, "Счета.bmp");
            this.imageList1.Images.SetKeyName(2, "РасходНакл.bmp");
            this.imageList1.Images.SetKeyName(3, "Счет-фактуры.bmp");
            this.imageList1.Images.SetKeyName(4, "ВходПлатежи.bmp");
            this.imageList1.Images.SetKeyName(5, "ИсходПлатежи.bmp");
            this.imageList1.Images.SetKeyName(6, "ДопРасход.bmp");
            this.imageList1.Images.SetKeyName(7, "Возврат Поставщику.bmp");
            this.imageList1.Images.SetKeyName(8, "Возврат от клиетна.bmp");
            this.imageList1.Images.SetKeyName(9, "Введення залишк_в товар_в.bmp");
            this.imageList1.Images.SetKeyName(10, "Акти списання товару.bmp");
            this.imageList1.Images.SetKeyName(11, "Накладн_ перем_щення.bmp");
            this.imageList1.Images.SetKeyName(12, "Бази даних.bmp");
            this.imageList1.Images.SetKeyName(13, "Акти iнвентаризацiї.bmp");
            this.imageList1.Images.SetKeyName(14, "ActServices.png");
            // 
            // colNum
            // 
            this.colNum.Caption = "№";
            this.colNum.FieldName = "Num";
            this.colNum.Name = "colNum";
            this.colNum.Visible = true;
            this.colNum.VisibleIndex = 1;
            this.colNum.Width = 163;
            // 
            // colOnDate
            // 
            this.colOnDate.Caption = "Дата";
            this.colOnDate.FieldName = "OnDate";
            this.colOnDate.Name = "colOnDate";
            this.colOnDate.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.Date;
            this.colOnDate.Visible = true;
            this.colOnDate.VisibleIndex = 2;
            this.colOnDate.Width = 198;
            // 
            // colName
            // 
            this.colName.Caption = "Контрагент";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 3;
            this.colName.Width = 377;
            // 
            // colSummAll
            // 
            this.colSummAll.Caption = "Сума";
            this.colSummAll.FieldName = "SummAll";
            this.colSummAll.Name = "colSummAll";
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
            // colSummPay
            // 
            this.colSummPay.Caption = "Сума оплати";
            this.colSummPay.DisplayFormat.FormatString = "0.00";
            this.colSummPay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSummPay.FieldName = "SummPay";
            this.colSummPay.Name = "colSummPay";
            this.colSummPay.OptionsColumn.AllowEdit = false;
            this.colSummPay.Visible = true;
            this.colSummPay.VisibleIndex = 5;
            this.colSummPay.Width = 194;
            // 
            // frmDocumentViews
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 568);
            this.Controls.Add(this.DocumentGridControl);
            this.Controls.Add(this.BottomPanel);
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
            this.ResumeLayout(false);

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
        private DevExpress.XtraGrid.Columns.GridColumn colSummAll;
        private DevExpress.XtraGrid.Columns.GridColumn colSummInCurr;
        private DevExpress.XtraGrid.Columns.GridColumn colSummPay;
        public System.Windows.Forms.ImageList imageList1;
    }
}