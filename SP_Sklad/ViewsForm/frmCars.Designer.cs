namespace SP_Sklad.ViewsForm
{
    partial class frmCars
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
            this.CarsGrid = new DevExpress.XtraGrid.GridControl();
            this.CarsBS = new System.Windows.Forms.BindingSource(this.components);
            this.CarsGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CarsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarsBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarsGridView)).BeginInit();
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
            // CarsGrid
            // 
            this.CarsGrid.DataSource = this.CarsBS;
            this.CarsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CarsGrid.Location = new System.Drawing.Point(0, 0);
            this.CarsGrid.MainView = this.CarsGridView;
            this.CarsGrid.Name = "CarsGrid";
            this.CarsGrid.Size = new System.Drawing.Size(571, 366);
            this.CarsGrid.TabIndex = 19;
            this.CarsGrid.UseEmbeddedNavigator = true;
            this.CarsGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.CarsGridView});
            // 
            // CarsBS
            // 
            this.CarsBS.DataSource = typeof(SP_Sklad.SkladData.Cars);
            this.CarsBS.AddingNew += new System.ComponentModel.AddingNewEventHandler(this.KontragentGroupBS_AddingNew);
            // 
            // CarsGridView
            // 
            this.CarsGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn30,
            this.gridColumn1});
            this.CarsGridView.GridControl = this.CarsGrid;
            this.CarsGridView.Name = "CarsGridView";
            this.CarsGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.CarsGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.CarsGridView.OptionsFind.AlwaysVisible = true;
            this.CarsGridView.OptionsView.ShowGroupPanel = false;
            this.CarsGridView.RowDeleted += new DevExpress.Data.RowDeletedEventHandler(this.WhRemainGridView_RowDeleted);
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Назва";
            this.gridColumn30.FieldName = "Name";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 0;
            this.gridColumn30.Width = 307;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Номер ";
            this.gridColumn1.FieldName = "Number";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 246;
            // 
            // frmCars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 418);
            this.Controls.Add(this.CarsGrid);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmCars";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Машини";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKaGroup_FormClosed);
            this.Load += new System.EventHandler(this.frmKaGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CarsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarsBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl CarsGrid;
        public DevExpress.XtraGrid.Views.Grid.GridView CarsGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private System.Windows.Forms.BindingSource CarsBS;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
    }
}