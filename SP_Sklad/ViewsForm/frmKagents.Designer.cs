namespace SP_Sklad.ViewsForm
{
    partial class frmKagents
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKagents));
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.KontragentGrid = new DevExpress.XtraGrid.GridControl();
            this.KontragentBS = new System.Windows.Forms.BindingSource(this.components);
            this.KontragentGroupGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.GridImageList = new System.Windows.Forms.ImageList(this.components);
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGroupGridView)).BeginInit();
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
            this.BottomPanel.Size = new System.Drawing.Size(1006, 52);
            this.BottomPanel.TabIndex = 18;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(784, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вибрати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(894, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // KontragentGrid
            // 
            this.KontragentGrid.DataSource = this.KontragentBS;
            this.KontragentGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KontragentGrid.Location = new System.Drawing.Point(0, 0);
            this.KontragentGrid.MainView = this.KontragentGroupGridView;
            this.KontragentGrid.Name = "KontragentGrid";
            this.KontragentGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.KontragentGrid.Size = new System.Drawing.Size(1006, 516);
            this.KontragentGrid.TabIndex = 19;
            this.KontragentGrid.UseEmbeddedNavigator = true;
            this.KontragentGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.KontragentGroupGridView});
            // 
            // KontragentBS
            // 
            this.KontragentBS.DataSource = typeof(SP_Sklad.SkladData.Kagent);
            // 
            // KontragentGroupGridView
            // 
            this.KontragentGroupGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.KontragentGroupGridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.KontragentGroupGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F);
            this.KontragentGroupGridView.Appearance.Row.Options.UseFont = true;
            this.KontragentGroupGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn2,
            this.gridColumn30,
            this.gridColumn1});
            this.KontragentGroupGridView.GridControl = this.KontragentGrid;
            this.KontragentGroupGridView.Name = "KontragentGroupGridView";
            this.KontragentGroupGridView.OptionsBehavior.Editable = false;
            this.KontragentGroupGridView.OptionsBehavior.ReadOnly = true;
            this.KontragentGroupGridView.OptionsFind.AlwaysVisible = true;
            this.KontragentGroupGridView.OptionsView.ShowGroupPanel = false;
            this.KontragentGroupGridView.DoubleClick += new System.EventHandler(this.KontragentGroupGridView_DoubleClick);
            // 
            // gridColumn2
            // 
            this.gridColumn2.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gridColumn2.FieldName = "KType";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.ShowCaption = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 29;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 3, 1)});
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
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Назва контрагента";
            this.gridColumn30.FieldName = "Name";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 1;
            this.gridColumn30.Width = 550;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Група контрагентів";
            this.gridColumn1.FieldName = "KontragentGroup.Name";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 409;
            // 
            // frmKagents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 568);
            this.Controls.Add(this.KontragentGrid);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmKagents";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Контрагенти";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKaGroup_FormClosed);
            this.Load += new System.EventHandler(this.frmKaGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGroupGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl KontragentGrid;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private System.Windows.Forms.BindingSource KontragentBS;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Views.Grid.GridView KontragentGroupGridView;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        public System.Windows.Forms.ImageList GridImageList;
    }
}