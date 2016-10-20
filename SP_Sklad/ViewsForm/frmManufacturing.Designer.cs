namespace SP_Sklad.ViewsForm
{
    partial class frmManufacturing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManufacturing));
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.WBGridControl = new DevExpress.XtraGrid.GridControl();
            this.WbGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.GridImageList = new System.Windows.Forms.ImageList(this.components);
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CheckedItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn37 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn38 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn39 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn40 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn41 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WBGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WbGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckedItemImageComboBox)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 498);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1268, 52);
            this.BottomPanel.TabIndex = 17;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(1046, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вибрати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(1156, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // WBGridControl
            // 
            this.WBGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WBGridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.WBGridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.WBGridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.WBGridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.WBGridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.WBGridControl.Location = new System.Drawing.Point(0, 0);
            this.WBGridControl.MainView = this.WbGridView;
            this.WBGridControl.Name = "WBGridControl";
            this.WBGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1,
            this.CheckedItemImageComboBox});
            this.WBGridControl.Size = new System.Drawing.Size(1268, 498);
            this.WBGridControl.TabIndex = 18;
            this.WBGridControl.UseEmbeddedNavigator = true;
            this.WBGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.WbGridView});
            // 
            // WbGridView
            // 
            this.WbGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 10F);
            this.WbGridView.Appearance.Row.Options.UseFont = true;
            this.WbGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn37,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn38,
            this.gridColumn39,
            this.gridColumn40,
            this.gridColumn41});
            this.WbGridView.GridControl = this.WBGridControl;
            this.WbGridView.Name = "WbGridView";
            this.WbGridView.OptionsBehavior.Editable = false;
            this.WbGridView.OptionsBehavior.ReadOnly = true;
            this.WbGridView.OptionsView.EnableAppearanceEvenRow = true;
            this.WbGridView.OptionsView.EnableAppearanceOddRow = true;
            this.WbGridView.OptionsView.ShowGroupPanel = false;
            this.WbGridView.DoubleClick += new System.EventHandler(this.WbGridView_DoubleClick);
            // 
            // gridColumn1
            // 
            this.gridColumn1.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gridColumn1.FieldName = "WType";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowFocus = false;
            this.gridColumn1.OptionsColumn.AllowSize = false;
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.OptionsColumn.ShowCaption = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 25;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", -20, 3)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.GridImageList;
            // 
            // GridImageList
            // 
            this.GridImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GridImageList.ImageStream")));
            this.GridImageList.TransparentColor = System.Drawing.Color.White;
            this.GridImageList.Images.SetKeyName(0, "Так.bmp");
            this.GridImageList.Images.SetKeyName(1, "н_чого.bmp");
            this.GridImageList.Images.SetKeyName(2, "Частково оброблений.bmp");
            this.GridImageList.Images.SetKeyName(3, "exec16.png");
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = " ";
            this.gridColumn2.ColumnEdit = this.CheckedItemImageComboBox;
            this.gridColumn2.FieldName = "Checked";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowFocus = false;
            this.gridColumn2.OptionsColumn.AllowSize = false;
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.OptionsColumn.ShowCaption = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 25;
            // 
            // CheckedItemImageComboBox
            // 
            this.CheckedItemImageComboBox.AutoHeight = false;
            this.CheckedItemImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.CheckedItemImageComboBox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 0, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 1, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", 2, 2)});
            this.CheckedItemImageComboBox.Name = "CheckedItemImageComboBox";
            this.CheckedItemImageComboBox.SmallImages = this.GridImageList;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Серія";
            this.gridColumn3.FieldName = "Num";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 61;
            // 
            // gridColumn37
            // 
            this.gridColumn37.Caption = "К-сть";
            this.gridColumn37.DisplayFormat.FormatString = "0.0000";
            this.gridColumn37.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn37.FieldName = "AmountIn";
            this.gridColumn37.Name = "gridColumn37";
            this.gridColumn37.Visible = true;
            this.gridColumn37.VisibleIndex = 5;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Дата";
            this.gridColumn4.FieldName = "OnDate";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 83;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Товар";
            this.gridColumn5.FieldName = "MatName";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 257;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Од. виміру";
            this.gridColumn6.FieldName = "MsrName";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 83;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Затрати";
            this.gridColumn7.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn7.FieldName = "SummAll";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 82;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Цех";
            this.gridColumn8.FieldName = "FromWh";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 69;
            // 
            // gridColumn38
            // 
            this.gridColumn38.Caption = "Статус документу";
            this.gridColumn38.FieldName = "DocStatus";
            this.gridColumn38.Name = "gridColumn38";
            this.gridColumn38.Visible = true;
            this.gridColumn38.VisibleIndex = 9;
            // 
            // gridColumn39
            // 
            this.gridColumn39.Caption = "Вихід";
            this.gridColumn39.DisplayFormat.FormatString = "0.0000";
            this.gridColumn39.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn39.FieldName = "AmountOut";
            this.gridColumn39.Name = "gridColumn39";
            this.gridColumn39.Visible = true;
            this.gridColumn39.VisibleIndex = 10;
            // 
            // gridColumn40
            // 
            this.gridColumn40.Caption = "Собівартість";
            this.gridColumn40.DisplayFormat.FormatString = "0.00";
            this.gridColumn40.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn40.FieldName = "Price";
            this.gridColumn40.Name = "gridColumn40";
            this.gridColumn40.Visible = true;
            this.gridColumn40.VisibleIndex = 11;
            // 
            // gridColumn41
            // 
            this.gridColumn41.Caption = "Дата закінчення";
            this.gridColumn41.FieldName = "WriteOnDate";
            this.gridColumn41.Name = "gridColumn41";
            this.gridColumn41.Visible = true;
            this.gridColumn41.VisibleIndex = 12;
            // 
            // frmManufacturing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 550);
            this.Controls.Add(this.WBGridControl);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmManufacturing";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Виготовлення продукції";
            this.Load += new System.EventHandler(this.frmManufacturing_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WBGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WbGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CheckedItemImageComboBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl WBGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView WbGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        public DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox CheckedItemImageComboBox;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn37;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn38;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn39;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn40;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn41;
        public System.Windows.Forms.ImageList GridImageList;
    }
}