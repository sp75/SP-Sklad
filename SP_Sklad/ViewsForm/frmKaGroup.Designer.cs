namespace SP_Sklad.ViewsForm
{
    partial class frmKaGroup
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
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.KontragentGroupGrid = new DevExpress.XtraGrid.GridControl();
            this.KontragentGroupBS = new System.Windows.Forms.BindingSource(this.components);
            this.KontragentGroupGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGroupGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGroupBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGroupGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 366);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(571, 52);
            this.BottomPanel.TabIndex = 18;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(349, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вибрати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(459, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // KontragentGroupGrid
            // 
            this.KontragentGroupGrid.DataSource = this.KontragentGroupBS;
            this.KontragentGroupGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KontragentGroupGrid.Location = new System.Drawing.Point(0, 0);
            this.KontragentGroupGrid.MainView = this.KontragentGroupGridView;
            this.KontragentGroupGrid.Name = "KontragentGroupGrid";
            this.KontragentGroupGrid.Size = new System.Drawing.Size(571, 366);
            this.KontragentGroupGrid.TabIndex = 19;
            this.KontragentGroupGrid.UseEmbeddedNavigator = true;
            this.KontragentGroupGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.KontragentGroupGridView});
            // 
            // KontragentGroupBS
            // 
            this.KontragentGroupBS.DataSource = typeof(SP_Sklad.SkladData.KontragentGroup);
            this.KontragentGroupBS.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.KontragentGroupBS_AddingNew);
            // 
            // KontragentGroupGridView
            // 
            this.KontragentGroupGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn30});
            this.KontragentGroupGridView.GridControl = this.KontragentGroupGrid;
            this.KontragentGroupGridView.Name = "KontragentGroupGridView";
            this.KontragentGroupGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.KontragentGroupGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.KontragentGroupGridView.OptionsFind.AlwaysVisible = true;
            this.KontragentGroupGridView.OptionsView.ShowGroupPanel = false;
            this.KontragentGroupGridView.RowDeleted += new DevExpress.Data.RowDeletedEventHandler(this.WhRemainGridView_RowDeleted);
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Назва групи";
            this.gridColumn30.FieldName = "Name";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 0;
            this.gridColumn30.Width = 395;
            // 
            // frmKaGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 418);
            this.Controls.Add(this.KontragentGroupGrid);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmKaGroup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Групи контрагентів";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKaGroup_FormClosed);
            this.Load += new System.EventHandler(this.frmKaGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGroupGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGroupBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KontragentGroupGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl KontragentGroupGrid;
        public DevExpress.XtraGrid.Views.Grid.GridView KontragentGroupGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private System.Windows.Forms.BindingSource KontragentGroupBS;
    }
}