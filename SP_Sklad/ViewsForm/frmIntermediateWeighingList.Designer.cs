namespace SP_Sklad.ViewsForm
{
    partial class frmIntermediateWeighingList
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
            this.UsersGroupGrid = new DevExpress.XtraGrid.GridControl();
            this.GetWayBillMakeDetBS = new System.Windows.Forms.BindingSource(this.components);
            this.UsersGroupGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn30 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGroupGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetWayBillMakeDetBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGroupGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 516);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(774, 52);
            this.BottomPanel.TabIndex = 19;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(552, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вибрати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(662, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // UsersGroupGrid
            // 
            this.UsersGroupGrid.DataSource = this.GetWayBillMakeDetBS;
            this.UsersGroupGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UsersGroupGrid.Location = new System.Drawing.Point(0, 0);
            this.UsersGroupGrid.MainView = this.UsersGroupGridView;
            this.UsersGroupGrid.Name = "UsersGroupGrid";
            this.UsersGroupGrid.Size = new System.Drawing.Size(774, 516);
            this.UsersGroupGrid.TabIndex = 20;
            this.UsersGroupGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UsersGroupGridView});
            // 
            // GetWayBillMakeDetBS
            // 
            this.GetWayBillMakeDetBS.DataSource = typeof(SP_Sklad.SkladData.GetWayBillMakeDet_Result);
            // 
            // UsersGroupGridView
            // 
            this.UsersGroupGridView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.UsersGroupGridView.Appearance.HeaderPanel.Options.UseFont = true;
            this.UsersGroupGridView.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F);
            this.UsersGroupGridView.Appearance.Row.Options.UseFont = true;
            this.UsersGroupGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn30,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3});
            this.UsersGroupGridView.GridControl = this.UsersGroupGrid;
            this.UsersGroupGridView.Name = "UsersGroupGridView";
            this.UsersGroupGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.UsersGroupGridView.OptionsBehavior.Editable = false;
            this.UsersGroupGridView.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseDownFocused;
            this.UsersGroupGridView.OptionsBehavior.ReadOnly = true;
            this.UsersGroupGridView.OptionsFind.AlwaysVisible = true;
            this.UsersGroupGridView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn30
            // 
            this.gridColumn30.Caption = "Сировина";
            this.gridColumn30.FieldName = "MatName";
            this.gridColumn30.Name = "gridColumn30";
            this.gridColumn30.Visible = true;
            this.gridColumn30.VisibleIndex = 0;
            this.gridColumn30.Width = 350;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Од.";
            this.gridColumn1.FieldName = "MsrName";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 78;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "За рецептом";
            this.gridColumn2.FieldName = "AmountByRecipe";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 97;
            // 
            // gridColumn3
            // 
            this.gridColumn3.AppearanceCell.BackColor = System.Drawing.Color.Azure;
            this.gridColumn3.AppearanceCell.Options.UseBackColor = true;
            this.gridColumn3.Caption = "Зважено";
            this.gridColumn3.FieldName = "AmountIntermediateWeighing";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 102;
            // 
            // frmIntermediateWeighingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 568);
            this.Controls.Add(this.UsersGroupGrid);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmIntermediateWeighingList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список сировини для зважування";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmUserGroup_FormClosed);
            this.Load += new System.EventHandler(this.frmUserGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UsersGroupGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GetWayBillMakeDetBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGroupGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl UsersGroupGrid;
        public DevExpress.XtraGrid.Views.Grid.GridView UsersGroupGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn30;
        private System.Windows.Forms.BindingSource GetWayBillMakeDetBS;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
    }
}