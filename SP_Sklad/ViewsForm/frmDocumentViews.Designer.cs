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
            this.colNum = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOnDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSummAll = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSummInCurr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSummPay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocTypeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.GridImageList = new System.Windows.Forms.ImageList(this.components);
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
            this.colSummPay,
            this.colDocTypeName});
            this.DocumentGridView.GridControl = this.DocumentGridControl;
            this.DocumentGridView.Name = "DocumentGridView";
            this.DocumentGridView.OptionsBehavior.Editable = false;
            this.DocumentGridView.OptionsBehavior.ReadOnly = true;
            this.DocumentGridView.OptionsFind.AlwaysVisible = true;
            this.DocumentGridView.OptionsView.ShowAutoFilterRow = true;
            this.DocumentGridView.OptionsView.ShowDetailButtons = false;
            this.DocumentGridView.DoubleClick += new System.EventHandler(this.KontragentGroupGridView_DoubleClick);
            // 
            // colWType
            // 
            this.colWType.FieldName = "WType";
            this.colWType.Name = "colWType";
            this.colWType.Visible = true;
            this.colWType.VisibleIndex = 0;
            this.colWType.Width = 95;
            // 
            // colNum
            // 
            this.colNum.Caption = "№";
            this.colNum.FieldName = "Num";
            this.colNum.Name = "colNum";
            this.colNum.Visible = true;
            this.colNum.VisibleIndex = 2;
            this.colNum.Width = 188;
            // 
            // colOnDate
            // 
            this.colOnDate.Caption = "Дата";
            this.colOnDate.FieldName = "OnDate";
            this.colOnDate.Name = "colOnDate";
            this.colOnDate.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.Date;
            this.colOnDate.Visible = true;
            this.colOnDate.VisibleIndex = 3;
            this.colOnDate.Width = 193;
            // 
            // colName
            // 
            this.colName.Caption = "Контрагент";
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 4;
            this.colName.Width = 344;
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
            this.colSummInCurr.FieldName = "SummInCurr";
            this.colSummInCurr.Name = "colSummInCurr";
            this.colSummInCurr.Visible = true;
            this.colSummInCurr.VisibleIndex = 5;
            this.colSummInCurr.Width = 173;
            // 
            // colSummPay
            // 
            this.colSummPay.Caption = "Сума оплати";
            this.colSummPay.FieldName = "SummPay";
            this.colSummPay.Name = "colSummPay";
            this.colSummPay.Visible = true;
            this.colSummPay.VisibleIndex = 6;
            this.colSummPay.Width = 151;
            // 
            // colDocTypeName
            // 
            this.colDocTypeName.Caption = "Тип документа";
            this.colDocTypeName.FieldName = "DocTypeName";
            this.colDocTypeName.Name = "colDocTypeName";
            this.colDocTypeName.Visible = true;
            this.colDocTypeName.VisibleIndex = 1;
            this.colDocTypeName.Width = 203;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Юридичні особи", 0, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Фізичні особи", 1, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Службовці", 2, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Власні підприємства", 3, 1)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.GridImageList;
            // 
            // GridImageList
            // 
            this.GridImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GridImageList.ImageStream")));
            this.GridImageList.TransparentColor = System.Drawing.Color.White;
            this.GridImageList.Images.SetKeyName(0, "Товари.bmp");
            this.GridImageList.Images.SetKeyName(1, "Конрагент.bmp");
            this.GridImageList.Images.SetKeyName(2, "Службовц_.bmp");
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
        public System.Windows.Forms.ImageList GridImageList;
        private DevExpress.Data.Linq.LinqInstantFeedbackSource KagentListSource;
        private DevExpress.XtraGrid.Columns.GridColumn colWType;
        private DevExpress.XtraGrid.Columns.GridColumn colNum;
        private DevExpress.XtraGrid.Columns.GridColumn colOnDate;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colSummAll;
        private DevExpress.XtraGrid.Columns.GridColumn colSummInCurr;
        private DevExpress.XtraGrid.Columns.GridColumn colSummPay;
        private DevExpress.XtraGrid.Columns.GridColumn colDocTypeName;
    }
}