namespace SP_Sklad
{
    partial class frmLogHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogHistory));
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.OprLogGridControl = new DevExpress.XtraGrid.GridControl();
            this.OprLogGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn42 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.GridImageList = new System.Windows.Forms.ImageList(this.components);
            this.gridColumn43 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn44 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.gridColumn45 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn46 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OprLogGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OprLogGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.OkButton);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(0, 415);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(1182, 52);
            this.panelControl4.TabIndex = 12;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(1087, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(83, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вихід";
            // 
            // OprLogGridControl
            // 
            this.OprLogGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OprLogGridControl.Location = new System.Drawing.Point(0, 0);
            this.OprLogGridControl.MainView = this.OprLogGridView;
            this.OprLogGridControl.Name = "OprLogGridControl";
            this.OprLogGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.repositoryItemMemoEdit1});
            this.OprLogGridControl.Size = new System.Drawing.Size(1182, 415);
            this.OprLogGridControl.TabIndex = 13;
            this.OprLogGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.OprLogGridView});
            // 
            // OprLogGridView
            // 
            this.OprLogGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn42,
            this.gridColumn43,
            this.gridColumn44,
            this.gridColumn45,
            this.gridColumn46});
            this.OprLogGridView.GridControl = this.OprLogGridControl;
            this.OprLogGridView.Name = "OprLogGridView";
            this.OprLogGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.OprLogGridView.OptionsBehavior.Editable = false;
            this.OprLogGridView.OptionsBehavior.ReadOnly = true;
            this.OprLogGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.OprLogGridView.OptionsView.ShowGroupPanel = false;
            this.OprLogGridView.CalcRowHeight += new DevExpress.XtraGrid.Views.Grid.RowHeightEventHandler(this.OprLogGridView_CalcRowHeight);
            this.OprLogGridView.DoubleClick += new System.EventHandler(this.OprLogGridView_DoubleClick);
            // 
            // gridColumn42
            // 
            this.gridColumn42.Caption = "Подія";
            this.gridColumn42.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gridColumn42.FieldName = "OpCode";
            this.gridColumn42.Name = "gridColumn42";
            this.gridColumn42.Visible = true;
            this.gridColumn42.VisibleIndex = 0;
            this.gridColumn42.Width = 52;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "U", 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "I", 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "D", 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "E", 3),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "S", 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "V", 5)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.GridImageList;
            // 
            // GridImageList
            // 
            this.GridImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GridImageList.ImageStream")));
            this.GridImageList.TransparentColor = System.Drawing.Color.White;
            this.GridImageList.Images.SetKeyName(0, "Add.bmp");
            this.GridImageList.Images.SetKeyName(1, "edit.bmp");
            this.GridImageList.Images.SetKeyName(2, "Delete.bmp");
            this.GridImageList.Images.SetKeyName(3, "execute.png");
            this.GridImageList.Images.SetKeyName(4, "storno.png");
            this.GridImageList.Images.SetKeyName(5, "н_чого.bmp");
            // 
            // gridColumn43
            // 
            this.gridColumn43.Caption = "Дата події";
            this.gridColumn43.DisplayFormat.FormatString = "g";
            this.gridColumn43.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn43.FieldName = "OnDate";
            this.gridColumn43.Name = "gridColumn43";
            this.gridColumn43.Visible = true;
            this.gridColumn43.VisibleIndex = 2;
            this.gridColumn43.Width = 141;
            // 
            // gridColumn44
            // 
            this.gridColumn44.Caption = "Дані до події";
            this.gridColumn44.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn44.FieldName = "DataBefore";
            this.gridColumn44.Name = "gridColumn44";
            this.gridColumn44.Visible = true;
            this.gridColumn44.VisibleIndex = 3;
            this.gridColumn44.Width = 311;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // gridColumn45
            // 
            this.gridColumn45.Caption = "Дані після події";
            this.gridColumn45.ColumnEdit = this.repositoryItemMemoEdit1;
            this.gridColumn45.FieldName = "DataAfter";
            this.gridColumn45.Name = "gridColumn45";
            this.gridColumn45.Visible = true;
            this.gridColumn45.VisibleIndex = 4;
            this.gridColumn45.Width = 359;
            // 
            // gridColumn46
            // 
            this.gridColumn46.Caption = "Користувач";
            this.gridColumn46.FieldName = "Name";
            this.gridColumn46.Name = "gridColumn46";
            this.gridColumn46.Visible = true;
            this.gridColumn46.VisibleIndex = 1;
            this.gridColumn46.Width = 328;
            // 
            // frmLogHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 467);
            this.Controls.Add(this.OprLogGridControl);
            this.Controls.Add(this.panelControl4);
            this.Name = "frmLogHistory";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Історія змін запису";
            this.Load += new System.EventHandler(this.frmLogHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OprLogGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OprLogGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl4;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraGrid.GridControl OprLogGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView OprLogGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn42;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn43;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn44;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn45;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn46;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private System.Windows.Forms.ImageList GridImageList;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
    }
}