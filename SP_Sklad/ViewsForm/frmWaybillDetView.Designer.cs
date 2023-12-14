namespace SP_Sklad.ViewsForm
{
    partial class frmWaybillDetView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWaybillDetView));
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.WaybillDetGridControl = new DevExpress.XtraGrid.GridControl();
            this.WaybillDetSource = new DevExpress.Data.Linq.LinqInstantFeedbackSource();
            this.WaybillDetGridView = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.colWType = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn3 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.colOnDate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colNum = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSummPay = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.colSummInCurr = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumn4 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
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
            this.BottomPanel.Size = new System.Drawing.Size(1455, 52);
            this.BottomPanel.TabIndex = 18;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(1237, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "Вибрати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(1343, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Закрити";
            // 
            // WaybillDetGridControl
            // 
            this.WaybillDetGridControl.DataSource = this.WaybillDetSource;
            this.WaybillDetGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WaybillDetGridControl.Location = new System.Drawing.Point(0, 0);
            this.WaybillDetGridControl.MainView = this.WaybillDetGridView;
            this.WaybillDetGridControl.Name = "WaybillDetGridControl";
            this.WaybillDetGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.repositoryItemCheckEdit1});
            this.WaybillDetGridControl.Size = new System.Drawing.Size(1455, 516);
            this.WaybillDetGridControl.TabIndex = 19;
            this.WaybillDetGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.WaybillDetGridView});
            // 
            // WaybillDetSource
            // 
            this.WaybillDetSource.AreSourceRowsThreadSafe = true;
            this.WaybillDetSource.DefaultSorting = "OnDate DESC";
            this.WaybillDetSource.DesignTimeElementType = typeof(SP_Sklad.SkladData.v_WaybillDet);
            this.WaybillDetSource.KeyExpression = "PosId";
            this.WaybillDetSource.GetQueryable += new System.EventHandler<DevExpress.Data.Linq.GetQueryableEventArgs>(this.KagentListSource_GetQueryable);
            // 
            // WaybillDetGridView
            // 
            this.WaybillDetGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.WaybillDetGridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.WaybillDetGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.WaybillDetGridView.Appearance.Row.Options.UseFont = true;
            this.WaybillDetGridView.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBand2,
            this.gridBand3});
            this.WaybillDetGridView.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.colWType,
            this.colNum,
            this.colOnDate,
            this.colName,
            this.colSummInCurr,
            this.colSummPay,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.bandedGridColumn1,
            this.bandedGridColumn2,
            this.bandedGridColumn3,
            this.bandedGridColumn4});
            this.WaybillDetGridView.GridControl = this.WaybillDetGridControl;
            this.WaybillDetGridView.Name = "WaybillDetGridView";
            this.WaybillDetGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.WaybillDetGridView.OptionsBehavior.ReadOnly = true;
            this.WaybillDetGridView.OptionsFind.AlwaysVisible = true;
            this.WaybillDetGridView.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.True;
            this.WaybillDetGridView.DoubleClick += new System.EventHandler(this.WaybillDetGridView_DoubleClick);
            // 
            // gridBand2
            // 
            this.gridBand2.Caption = "Документ";
            this.gridBand2.Columns.Add(this.bandedGridColumn2);
            this.gridBand2.Columns.Add(this.colWType);
            this.gridBand2.Columns.Add(this.gridColumn3);
            this.gridBand2.Columns.Add(this.colName);
            this.gridBand2.Columns.Add(this.bandedGridColumn1);
            this.gridBand2.Columns.Add(this.bandedGridColumn3);
            this.gridBand2.Name = "gridBand2";
            this.gridBand2.VisibleIndex = 0;
            this.gridBand2.Width = 603;
            // 
            // bandedGridColumn2
            // 
            this.bandedGridColumn2.Caption = "Статус";
            this.bandedGridColumn2.ColumnEdit = this.repositoryItemCheckEdit1;
            this.bandedGridColumn2.FieldName = "WbChecked";
            this.bandedGridColumn2.Name = "bandedGridColumn2";
            this.bandedGridColumn2.OptionsColumn.AllowEdit = false;
            this.bandedGridColumn2.Visible = true;
            this.bandedGridColumn2.Width = 76;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.ValueChecked = 1;
            this.repositoryItemCheckEdit1.ValueUnchecked = 0;
            // 
            // colWType
            // 
            this.colWType.Caption = "Тип";
            this.colWType.ColumnEdit = this.repositoryItemImageComboBox1;
            this.colWType.FieldName = "WType";
            this.colWType.Name = "colWType";
            this.colWType.OptionsColumn.AllowEdit = false;
            this.colWType.Visible = true;
            this.colWType.Width = 92;
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
            // gridColumn3
            // 
            this.gridColumn3.Caption = "№";
            this.gridColumn3.FieldName = "WbNum";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.Width = 98;
            // 
            // colName
            // 
            this.colName.Caption = "Контрагент";
            this.colName.FieldName = "KaName";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.Visible = true;
            this.colName.Width = 337;
            // 
            // bandedGridColumn1
            // 
            this.bandedGridColumn1.Caption = "Виконав";
            this.bandedGridColumn1.FieldName = "PersonName";
            this.bandedGridColumn1.Name = "bandedGridColumn1";
            this.bandedGridColumn1.Width = 114;
            // 
            // bandedGridColumn3
            // 
            this.bandedGridColumn3.Caption = "Примітка";
            this.bandedGridColumn3.FieldName = "Notes";
            this.bandedGridColumn3.Name = "bandedGridColumn3";
            this.bandedGridColumn3.Width = 106;
            // 
            // gridBand3
            // 
            this.gridBand3.Caption = "Позиція по документу";
            this.gridBand3.Columns.Add(this.colOnDate);
            this.gridBand3.Columns.Add(this.gridColumn2);
            this.gridBand3.Columns.Add(this.colNum);
            this.gridBand3.Columns.Add(this.gridColumn1);
            this.gridBand3.Columns.Add(this.colSummPay);
            this.gridBand3.Columns.Add(this.colSummInCurr);
            this.gridBand3.Columns.Add(this.bandedGridColumn4);
            this.gridBand3.Name = "gridBand3";
            this.gridBand3.VisibleIndex = 1;
            this.gridBand3.Width = 827;
            // 
            // colOnDate
            // 
            this.colOnDate.Caption = "Дата";
            this.colOnDate.DisplayFormat.FormatString = "g";
            this.colOnDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colOnDate.FieldName = "OnDate";
            this.colOnDate.Name = "colOnDate";
            this.colOnDate.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.Date;
            this.colOnDate.Visible = true;
            this.colOnDate.Width = 168;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Товар";
            this.gridColumn2.FieldName = "MatName";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.Width = 192;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Од. виміру";
            this.gridColumn1.FieldName = "MsrName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.Width = 117;
            // 
            // colNum
            // 
            this.colNum.Caption = "К-сть";
            this.colNum.FieldName = "Amount";
            this.colNum.Name = "colNum";
            this.colNum.Visible = true;
            // 
            // colSummPay
            // 
            this.colSummPay.Caption = "Ціна без знижки";
            this.colSummPay.DisplayFormat.FormatString = "0.00";
            this.colSummPay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSummPay.FieldName = "BasePrice";
            this.colSummPay.Name = "colSummPay";
            this.colSummPay.OptionsColumn.AllowEdit = false;
            this.colSummPay.Visible = true;
            this.colSummPay.Width = 100;
            // 
            // colSummInCurr
            // 
            this.colSummInCurr.Caption = "Група товарів";
            this.colSummInCurr.DisplayFormat.FormatString = "0.00";
            this.colSummInCurr.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSummInCurr.FieldName = "GrpName";
            this.colSummInCurr.Name = "colSummInCurr";
            this.colSummInCurr.OptionsColumn.AllowEdit = false;
            this.colSummInCurr.Width = 86;
            // 
            // bandedGridColumn4
            // 
            this.bandedGridColumn4.Caption = "Склад";
            this.bandedGridColumn4.FieldName = "WhName";
            this.bandedGridColumn4.Name = "bandedGridColumn4";
            this.bandedGridColumn4.Visible = true;
            this.bandedGridColumn4.Width = 175;
            // 
            // frmWaybillDetView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 568);
            this.Controls.Add(this.WaybillDetGridControl);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmWaybillDetView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список корегувань документів";
            this.Load += new System.EventHandler(this.frmKaGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillDetGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl WaybillDetGridControl;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.Data.Linq.LinqInstantFeedbackSource WaybillDetSource;
        public System.Windows.Forms.ImageList imageList1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView WaybillDetGridView;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colNum;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colOnDate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSummInCurr;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colSummPay;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colWType;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn gridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn colName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn3;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumn4;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        public DevExpress.XtraEditors.SimpleButton OkButton;
    }
}