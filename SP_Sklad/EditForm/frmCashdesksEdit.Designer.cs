namespace SP_Sklad.EditForm
{
    partial class frmCashdesksEdit
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.DefCheckBox = new DevExpress.XtraEditors.CheckEdit();
            this.CashDesksBS = new System.Windows.Forms.BindingSource(this.components);
            this.NotesTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.KagentComboBox = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.LicenseKeyEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit43 = new DevExpress.XtraEditors.CalcEdit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DefCheckBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashDesksBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotesTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KagentComboBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LicenseKeyEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit43.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 330);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(480, 52);
            this.BottomPanel.TabIndex = 21;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(258, 10);
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
            this.simpleButton1.Location = new System.Drawing.Point(368, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // DefCheckBox
            // 
            this.DefCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CashDesksBS, "Def", true));
            this.DefCheckBox.Location = new System.Drawing.Point(22, 295);
            this.DefCheckBox.Name = "DefCheckBox";
            this.DefCheckBox.Properties.Caption = "Використовувати за змовчуванням";
            this.DefCheckBox.Properties.ValueChecked = 1;
            this.DefCheckBox.Properties.ValueUnchecked = 0;
            this.DefCheckBox.Size = new System.Drawing.Size(256, 20);
            this.DefCheckBox.StyleController = this.styleController1;
            this.DefCheckBox.TabIndex = 47;
            // 
            // CashDesksBS
            // 
            this.CashDesksBS.DataSource = typeof(SP_Sklad.SkladData.CashDesks);
            // 
            // NotesTextEdit
            // 
            this.NotesTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NotesTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CashDesksBS, "Name", true));
            this.NotesTextEdit.Location = new System.Drawing.Point(22, 34);
            this.NotesTextEdit.Name = "NotesTextEdit";
            this.NotesTextEdit.Size = new System.Drawing.Size(446, 22);
            this.NotesTextEdit.StyleController = this.styleController1;
            this.NotesTextEdit.TabIndex = 46;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(22, 12);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 16);
            this.labelControl3.TabIndex = 45;
            this.labelControl3.Text = "Назва";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(22, 75);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(134, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 53;
            this.labelControl2.Text = "Належить контрагенту";
            // 
            // KagentComboBox
            // 
            this.KagentComboBox.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CashDesksBS, "KaId", true));
            this.KagentComboBox.Location = new System.Drawing.Point(22, 97);
            this.KagentComboBox.Name = "KagentComboBox";
            editorButtonImageOptions1.Image = global::SP_Sklad.Properties.Resources.kontragents_folder;
            this.KagentComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.KagentComboBox.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.KagentComboBox.Properties.DataSource = this.CashDesksBS;
            this.KagentComboBox.Properties.DisplayMember = "Name";
            this.KagentComboBox.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch;
            this.KagentComboBox.Properties.ShowFooter = false;
            this.KagentComboBox.Properties.ShowHeader = false;
            this.KagentComboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.KagentComboBox.Properties.ValueMember = "KaId";
            this.KagentComboBox.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.KTypeLookUpEdit_Properties_ButtonClick);
            this.KagentComboBox.Size = new System.Drawing.Size(446, 24);
            this.KagentComboBox.StyleController = this.styleController1;
            this.KagentComboBox.TabIndex = 54;
            this.KagentComboBox.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.EntEdit_ButtonClick);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(22, 144);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(144, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 55;
            this.labelControl1.Text = "Ключ ліцензії (checkbox)";
            // 
            // LicenseKeyEdit
            // 
            this.LicenseKeyEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LicenseKeyEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CashDesksBS, "LicenseKey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LicenseKeyEdit.Location = new System.Drawing.Point(22, 166);
            this.LicenseKeyEdit.Name = "LicenseKeyEdit";
            this.LicenseKeyEdit.Size = new System.Drawing.Size(446, 22);
            this.LicenseKeyEdit.StyleController = this.styleController1;
            this.LicenseKeyEdit.TabIndex = 56;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(22, 208);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(126, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 57;
            this.labelControl4.Text = "Код каси (OpenStore)";
            // 
            // textEdit43
            // 
            this.textEdit43.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CashDesksBS, "OpenStoreId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit43.Location = new System.Drawing.Point(22, 230);
            this.textEdit43.Name = "textEdit43";
            this.textEdit43.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textEdit43.Size = new System.Drawing.Size(182, 22);
            this.textEdit43.StyleController = this.styleController1;
            this.textEdit43.TabIndex = 58;
            // 
            // frmCashdesksEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 382);
            this.Controls.Add(this.textEdit43);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.LicenseKeyEdit);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.KagentComboBox);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.DefCheckBox);
            this.Controls.Add(this.NotesTextEdit);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.BottomPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Image = global::SP_Sklad.Properties.Resources.receipt_2;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCashdesksEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCashdesksEdit";
            this.Load += new System.EventHandler(this.frmCashdesksEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DefCheckBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CashDesksBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotesTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KagentComboBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LicenseKeyEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit43.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.BindingSource CashDesksBS;
        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.CheckEdit DefCheckBox;
        private DevExpress.XtraEditors.TextEdit NotesTextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit KagentComboBox;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit LicenseKeyEdit;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CalcEdit textEdit43;
    }
}