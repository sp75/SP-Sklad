namespace SP_Sklad.ViewsForm
{
    partial class frmEditSortingReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditSortingReport));
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.RemainOnWhGrid = new DevExpress.XtraGrid.GridControl();
            this.ReportSortedFieldsBS = new System.Windows.Forms.BindingSource(this.components);
            this.WhRemainGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.GridImageList = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RemainOnWhGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportSortedFieldsBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhRemainGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 334);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(438, 52);
            this.BottomPanel.TabIndex = 17;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(345, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(81, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Так";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // RemainOnWhGrid
            // 
            this.RemainOnWhGrid.DataSource = this.ReportSortedFieldsBS;
            this.RemainOnWhGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RemainOnWhGrid.Location = new System.Drawing.Point(0, 0);
            this.RemainOnWhGrid.MainView = this.WhRemainGridView;
            this.RemainOnWhGrid.Name = "RemainOnWhGrid";
            this.RemainOnWhGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.RemainOnWhGrid.Size = new System.Drawing.Size(438, 334);
            this.RemainOnWhGrid.TabIndex = 18;
            this.RemainOnWhGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.WhRemainGridView});
            // 
            // ReportSortedFieldsBS
            // 
            this.ReportSortedFieldsBS.DataSource = typeof(SP_Sklad.SkladData.ReportSortedFields);
            // 
            // WhRemainGridView
            // 
            this.WhRemainGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn30,
            this.gridColumn31,
            this.gridColumn32});
            this.WhRemainGridView.GridControl = this.RemainOnWhGrid;
            this.WhRemainGridView.Name = "WhRemainGridView";
            this.WhRemainGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.WhRemainGridView.OptionsView.ShowGroupPanel = false;
            this.WhRemainGridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.WhRemainGridView_CellValueChanged);
            this.WhRemainGridView.DoubleClick += new System.EventHandler(this.WhRemainGridView_DoubleClick);
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Назва поля";
            this.gridColumn30.FieldName = "Caption";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.OptionsColumn.AllowEdit = false;
            this.gridColumn30.OptionsColumn.ReadOnly = true;
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 1;
            this.gridColumn30.Width = 259;
            // 
            // gridColumn31
            // 
            this.gridColumn31.Caption = "№";
            this.gridColumn31.FieldName = "Idx";
            this.gridColumn31.Name = "gridColumn31";
            this.gridColumn31.OptionsColumn.AllowEdit = false;
            this.gridColumn31.OptionsColumn.ReadOnly = true;
            this.gridColumn31.Visible = true;
            this.gridColumn31.VisibleIndex = 0;
            this.gridColumn31.Width = 34;
            // 
            // gridColumn32
            // 
            this.gridColumn32.Caption = "Порядок";
            this.gridColumn32.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gridColumn32.FieldName = "OrderDirection";
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.Visible = true;
            this.gridColumn32.VisibleIndex = 2;
            this.gridColumn32.Width = 127;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Не сортувати", 0, -1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("По збільшенню", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("За зменшенням", 2, 1)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.GridImageList;
            // 
            // GridImageList
            // 
            this.GridImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GridImageList.ImageStream")));
            this.GridImageList.TransparentColor = System.Drawing.Color.White;
            this.GridImageList.Images.SetKeyName(0, "arrow.png");
            this.GridImageList.Images.SetKeyName(1, "sort.png");
            // 
            // frmEditSortingReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 386);
            this.Controls.Add(this.RemainOnWhGrid);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmEditSortingReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Налаштування сортування звіту";
            this.Load += new System.EventHandler(this.frmRemainOnWh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RemainOnWhGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportSortedFieldsBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhRemainGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraGrid.GridControl RemainOnWhGrid;
        public DevExpress.XtraGrid.Views.Grid.GridView WhRemainGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private System.Windows.Forms.BindingSource ReportSortedFieldsBS;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        public System.Windows.Forms.ImageList GridImageList;
    }
}