namespace SP_Sklad.EditForm
{
    partial class frmWarehouseEdit
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
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.WarehouseDS = new System.Windows.Forms.BindingSource(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.NotesTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.DefCheckBox = new DevExpress.XtraEditors.CheckEdit();
            this.UsersGridControl = new DevExpress.XtraGrid.GridControl();
            this.UserListBS = new System.Windows.Forms.BindingSource(this.components);
            this.UsersGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.textEdit43 = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WarehouseDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotesTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefCheckBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserListBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit43.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 509);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(728, 52);
            this.BottomPanel.TabIndex = 16;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(506, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Застосувати";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(616, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(21, 14);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 16);
            this.labelControl3.TabIndex = 28;
            this.labelControl3.Text = "Назва";
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(21, 76);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(121, 16);
            this.labelControl6.StyleController = this.styleController1;
            this.labelControl6.TabIndex = 30;
            this.labelControl6.Text = "Місце розташування";
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WarehouseDS, "Addr", true));
            this.textEdit1.Location = new System.Drawing.Point(21, 98);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(337, 35);
            this.textEdit1.TabIndex = 31;
            // 
            // WarehouseDS
            // 
            this.WarehouseDS.DataSource = typeof(SP_Sklad.SkladData.Warehouse);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 148);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 32;
            this.labelControl1.Text = "Примітка";
            // 
            // memoEdit1
            // 
            this.memoEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WarehouseDS, "Notes", true));
            this.memoEdit1.Location = new System.Drawing.Point(21, 170);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(337, 37);
            this.memoEdit1.TabIndex = 33;
            // 
            // NotesTextEdit
            // 
            this.NotesTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WarehouseDS, "Name", true));
            this.NotesTextEdit.Location = new System.Drawing.Point(21, 36);
            this.NotesTextEdit.Name = "NotesTextEdit";
            this.NotesTextEdit.Size = new System.Drawing.Size(337, 22);
            this.NotesTextEdit.StyleController = this.styleController1;
            this.NotesTextEdit.TabIndex = 34;
            // 
            // DefCheckBox
            // 
            this.DefCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DefCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WarehouseDS, "Def", true));
            this.DefCheckBox.Location = new System.Drawing.Point(21, 473);
            this.DefCheckBox.Name = "DefCheckBox";
            this.DefCheckBox.Properties.Caption = "Основний склад (використовувати за змовчуванням)";
            this.DefCheckBox.Properties.ValueChecked = 1;
            this.DefCheckBox.Properties.ValueUnchecked = 0;
            this.DefCheckBox.Size = new System.Drawing.Size(337, 20);
            this.DefCheckBox.StyleController = this.styleController1;
            this.DefCheckBox.TabIndex = 35;
            // 
            // UsersGridControl
            // 
            this.UsersGridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UsersGridControl.DataSource = this.UserListBS;
            this.UsersGridControl.Location = new System.Drawing.Point(374, 36);
            this.UsersGridControl.MainView = this.UsersGridView;
            this.UsersGridControl.Name = "UsersGridControl";
            this.UsersGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.UsersGridControl.Size = new System.Drawing.Size(342, 457);
            this.UsersGridControl.TabIndex = 36;
            this.UsersGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.UsersGridView});
            // 
            // UserListBS
            // 
            this.UserListBS.DataSource = typeof(SP_Sklad.SkladData.Warehouse);
            // 
            // UsersGridView
            // 
            this.UsersGridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn6,
            this.gridColumn7});
            this.UsersGridView.GridControl = this.UsersGridControl;
            this.UsersGridView.Name = "UsersGridView";
            this.UsersGridView.OptionsBehavior.AllowIncrementalSearch = true;
            this.UsersGridView.OptionsView.ShowGroupPanel = false;
            this.UsersGridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.UsersGridView_CellValueChanged);
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Службовець";
            this.gridColumn6.FieldName = "Name";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.ReadOnly = true;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 0;
            this.gridColumn6.Width = 371;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "Доступ";
            this.gridColumn7.ColumnEdit = this.repositoryItemCheckEdit1;
            this.gridColumn7.FieldName = "WhAccess";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 70;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(374, 14);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(102, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 37;
            this.labelControl2.Text = "Доступ до складу";
            // 
            // checkEdit1
            // 
            this.checkEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WarehouseDS, "RecyclingWarehouse", true));
            this.checkEdit1.Location = new System.Drawing.Point(21, 440);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Cклад для зіпсованих товарів";
            this.checkEdit1.Properties.ValueChecked = 1;
            this.checkEdit1.Properties.ValueUnchecked = 0;
            this.checkEdit1.Size = new System.Drawing.Size(337, 20);
            this.checkEdit1.StyleController = this.styleController1;
            this.checkEdit1.TabIndex = 38;
            // 
            // textEdit43
            // 
            this.textEdit43.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WarehouseDS, "OpenStoreId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit43.Location = new System.Drawing.Point(21, 254);
            this.textEdit43.Name = "textEdit43";
            this.textEdit43.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textEdit43.Size = new System.Drawing.Size(182, 22);
            this.textEdit43.StyleController = this.styleController1;
            this.textEdit43.TabIndex = 60;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(21, 232);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(139, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 59;
            this.labelControl4.Text = "Код складу (OpenStore)";
            // 
            // frmWarehouseEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 561);
            this.Controls.Add(this.textEdit43);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.checkEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.UsersGridControl);
            this.Controls.Add(this.DefCheckBox);
            this.Controls.Add(this.NotesTextEdit);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.textEdit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Image = global::SP_Sklad.Properties.Resources.warehouse_2;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWarehouseEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Властвості складу:";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmWarehouseEdit_FormClosed);
            this.Load += new System.EventHandler(this.frmWarehouseEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WarehouseDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotesTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefCheckBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserListBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsersGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit43.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.MemoEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.TextEdit NotesTextEdit;
        private System.Windows.Forms.BindingSource WarehouseDS;
        private DevExpress.XtraEditors.CheckEdit DefCheckBox;
        private DevExpress.XtraGrid.GridControl UsersGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView UsersGridView;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.BindingSource UserListBS;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.CalcEdit textEdit43;
        private DevExpress.XtraEditors.LabelControl labelControl4;
    }
}