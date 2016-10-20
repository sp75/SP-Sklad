namespace SP_Sklad.ViewsForm
{
    partial class frmRemainOnWh
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
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.RemainOnWhGrid = new DevExpress.XtraGrid.GridControl();
            this.WhRemainGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn31 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn32 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn33 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RemainOnWhGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhRemainGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 242);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(870, 52);
            this.BottomPanel.TabIndex = 17;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(648, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вибрати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(758, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // RemainOnWhGrid
            // 
            this.RemainOnWhGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RemainOnWhGrid.Location = new System.Drawing.Point(0, 0);
            this.RemainOnWhGrid.MainView = this.WhRemainGridView;
            this.RemainOnWhGrid.Name = "RemainOnWhGrid";
            this.RemainOnWhGrid.Size = new System.Drawing.Size(870, 242);
            this.RemainOnWhGrid.TabIndex = 18;
            this.RemainOnWhGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.WhRemainGridView});
            // 
            // WhRemainGridView
            // 
            this.WhRemainGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn30,
            this.gridColumn31,
            this.gridColumn32,
            this.gridColumn33});
            this.WhRemainGridView.GridControl = this.RemainOnWhGrid;
            this.WhRemainGridView.Name = "WhRemainGridView";
            this.WhRemainGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.WhRemainGridView.OptionsBehavior.Editable = false;
            this.WhRemainGridView.OptionsBehavior.ReadOnly = true;
            this.WhRemainGridView.OptionsView.ShowGroupPanel = false;
            this.WhRemainGridView.DoubleClick += new System.EventHandler(this.WhRemainGridView_DoubleClick);
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Склад";
            this.gridColumn30.FieldName = "Name";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 0;
            this.gridColumn30.Width = 395;
            // 
            // gridColumn31
            // 
            this.gridColumn31.Caption = "Залишок";
            this.gridColumn31.FieldName = "Remain";
            this.gridColumn31.Name = "gridColumn31";
            this.gridColumn31.Visible = true;
            this.gridColumn31.VisibleIndex = 1;
            this.gridColumn31.Width = 149;
            // 
            // gridColumn32
            // 
            this.gridColumn32.Caption = "В резерві";
            this.gridColumn32.FieldName = "Rsv";
            this.gridColumn32.Name = "gridColumn32";
            this.gridColumn32.Visible = true;
            this.gridColumn32.VisibleIndex = 2;
            this.gridColumn32.Width = 156;
            // 
            // gridColumn33
            // 
            this.gridColumn33.Caption = "Усього на складі";
            this.gridColumn33.FieldName = "CurRemain";
            this.gridColumn33.Name = "gridColumn33";
            this.gridColumn33.Visible = true;
            this.gridColumn33.VisibleIndex = 3;
            this.gridColumn33.Width = 152;
            // 
            // frmRemainOnWh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 294);
            this.Controls.Add(this.RemainOnWhGrid);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmRemainOnWh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Наявність на складах:";
            this.Load += new System.EventHandler(this.frmRemainOnWh_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RemainOnWhGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhRemainGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl RemainOnWhGrid;
        public DevExpress.XtraGrid.Views.Grid.GridView WhRemainGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn31;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn32;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn33;
    }
}