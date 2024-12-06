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
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGroupGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 516);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1176, 52);
            this.BottomPanel.TabIndex = 18;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(954, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вибрати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(1064, 10);
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
            this.KontragentGrid.Size = new System.Drawing.Size(1176, 516);
            this.KontragentGrid.TabIndex = 19;
            this.KontragentGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.KontragentGroupGridView});
            // 
            // KontragentBS
            // 
            this.KontragentBS.DataSource = typeof(SP_Sklad.SkladData.v_Kagent);
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
            this.gridColumn1,
            this.gridColumn4,
            this.gridColumn3});
            this.KontragentGroupGridView.GridControl = this.KontragentGrid;
            this.KontragentGroupGridView.Name = "KontragentGroupGridView";
            this.KontragentGroupGridView.OptionsBehavior.Editable = false;
            this.KontragentGroupGridView.OptionsBehavior.ReadOnly = true;
            this.KontragentGroupGridView.OptionsFind.AlwaysVisible = true;
            this.KontragentGroupGridView.OptionsSelection.MultiSelect = true;
            this.KontragentGroupGridView.OptionsView.ShowAutoFilterRow = true;
            this.KontragentGroupGridView.OptionsView.ShowDetailButtons = false;
            this.KontragentGroupGridView.DoubleClick += new System.EventHandler(this.KontragentGroupGridView_DoubleClick);
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Тип контрагента";
            this.gridColumn2.ColumnEdit = this.repositoryItemImageComboBox1;
            this.gridColumn2.FieldName = "KType";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 226;
            // 
            // repositoryItemImageComboBox1
            // 
            this.repositoryItemImageComboBox1.AutoHeight = false;
            this.repositoryItemImageComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemImageComboBox1.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Юридичні особи", 0, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Фізичні особи", 1, 3),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Службовці", 2, 4),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Власні підприємства", 3, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Торгові точки", 4, 2)});
            this.repositoryItemImageComboBox1.Name = "repositoryItemImageComboBox1";
            this.repositoryItemImageComboBox1.SmallImages = this.imageCollection1;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.businessman, "businessman", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.imageCollection1.Images.SetKeyName(0, "businessman");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.enterprise, "enterprise", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.imageCollection1.Images.SetKeyName(1, "enterprise");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.store_16, "store_16", typeof(global::SP_Sklad.Properties.Resources), 2);
            this.imageCollection1.Images.SetKeyName(2, "store_16");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.supplier, "supplier", typeof(global::SP_Sklad.Properties.Resources), 3);
            this.imageCollection1.Images.SetKeyName(3, "supplier");
            this.imageCollection1.InsertImage(global::SP_Sklad.Properties.Resources.user_valet, "user_valet", typeof(global::SP_Sklad.Properties.Resources), 4);
            this.imageCollection1.Images.SetKeyName(4, "user_valet");
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Назва контрагента";
            this.gridColumn30.FieldName = "Name";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.BeginsWith;
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 1;
            this.gridColumn30.Width = 413;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Група контрагентів";
            this.gridColumn1.FieldName = "GroupName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 268;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Примітка";
            this.gridColumn3.FieldName = "Notes";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 111;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Категорія цін";
            this.gridColumn4.FieldName = "PriceName";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 133;
            // 
            // frmKagents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 568);
            this.Controls.Add(this.KontragentGrid);
            this.Controls.Add(this.BottomPanel);
            this.IconOptions.Image = global::SP_Sklad.Properties.Resources.kontragents_folder;
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
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
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
        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}