namespace SP_Sklad.ViewsForm
{
    partial class frmCustomInfo
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
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.KontragentGrid = new DevExpress.XtraGrid.GridControl();
            this.KontragentGroupGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemImageComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGroupGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 365);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1103, 52);
            this.BottomPanel.TabIndex = 19;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(998, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(93, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Закрити";
            // 
            // KontragentGrid
            // 
            this.KontragentGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KontragentGrid.Location = new System.Drawing.Point(0, 0);
            this.KontragentGrid.MainView = this.KontragentGroupGridView;
            this.KontragentGrid.Name = "KontragentGrid";
            this.KontragentGrid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemImageComboBox1});
            this.KontragentGrid.Size = new System.Drawing.Size(1103, 365);
            this.KontragentGrid.TabIndex = 20;
            this.KontragentGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.KontragentGroupGridView});
            // 
            // KontragentGroupGridView
            // 
            this.KontragentGroupGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.KontragentGroupGridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.KontragentGroupGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 16F);
            this.KontragentGroupGridView.Appearance.Row.Options.UseFont = true;
            this.KontragentGroupGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn30,
            this.gridColumn1});
            this.KontragentGroupGridView.GridControl = this.KontragentGrid;
            this.KontragentGroupGridView.Name = "KontragentGroupGridView";
            this.KontragentGroupGridView.OptionsBehavior.Editable = false;
            this.KontragentGroupGridView.OptionsBehavior.ReadOnly = true;
            this.KontragentGroupGridView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Назва";
            this.gridColumn30.FieldName = "Title";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 0;
            this.gridColumn30.Width = 732;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Значення";
            this.gridColumn1.FieldName = "Value";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 252;
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
            // 
            // frmCustomInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 417);
            this.Controls.Add(this.KontragentGrid);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmCustomInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCustomInfo";
            this.Shown += new System.EventHandler(this.frmCustomInfo_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGroupGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemImageComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl KontragentGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView KontragentGroupGridView;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBox1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}