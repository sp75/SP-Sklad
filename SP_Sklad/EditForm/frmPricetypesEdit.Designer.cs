namespace SP_Sklad.EditForm
{
    partial class frmPricetypesEdit
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
            this.NotesTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.DefCheckBox = new DevExpress.XtraEditors.CheckEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.calcEdit2 = new DevExpress.XtraEditors.CalcEdit();
            this.lookUpEdit3 = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEdit2 = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.ExtraTypeLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.calcEdit1 = new DevExpress.XtraEditors.CalcEdit();
            this.checkEdit4 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit3 = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.PriceTypesBS = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NotesTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefCheckBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExtraTypeLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PriceTypesBS)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 324);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(639, 52);
            this.BottomPanel.TabIndex = 19;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(408, 11);
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
            this.simpleButton1.Location = new System.Drawing.Point(514, 11);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // NotesTextEdit
            // 
            this.NotesTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NotesTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.PriceTypesBS, "Name", true));
            this.NotesTextEdit.Location = new System.Drawing.Point(23, 38);
            this.NotesTextEdit.Name = "NotesTextEdit";
            this.NotesTextEdit.Size = new System.Drawing.Size(591, 22);
            this.NotesTextEdit.StyleController = this.styleController1;
            this.NotesTextEdit.TabIndex = 40;
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Location = new System.Drawing.Point(23, 16);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 16);
            this.labelControl3.TabIndex = 39;
            this.labelControl3.Text = "Назва";
            // 
            // DefCheckBox
            // 
            this.DefCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.PriceTypesBS, "Def", true));
            this.DefCheckBox.Location = new System.Drawing.Point(23, 270);
            this.DefCheckBox.Name = "DefCheckBox";
            this.DefCheckBox.Properties.Caption = "Основна цінова категорія  (використовувати за змовчуванням)";
            this.DefCheckBox.Properties.ValueChecked = 1;
            this.DefCheckBox.Properties.ValueUnchecked = 0;
            this.DefCheckBox.Size = new System.Drawing.Size(397, 19);
            this.DefCheckBox.TabIndex = 41;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.calcEdit2);
            this.groupControl2.Controls.Add(this.lookUpEdit3);
            this.groupControl2.Controls.Add(this.lookUpEdit2);
            this.groupControl2.Controls.Add(this.lookUpEdit1);
            this.groupControl2.Controls.Add(this.ExtraTypeLookUpEdit);
            this.groupControl2.Controls.Add(this.calcEdit1);
            this.groupControl2.Controls.Add(this.checkEdit4);
            this.groupControl2.Controls.Add(this.checkEdit3);
            this.groupControl2.Controls.Add(this.labelControl12);
            this.groupControl2.Location = new System.Drawing.Point(23, 82);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(591, 149);
            this.groupControl2.TabIndex = 42;
            this.groupControl2.Tag = "";
            this.groupControl2.Text = "Спосіб ціноутворення";
            // 
            // calcEdit2
            // 
            this.calcEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.PriceTypesBS, "OnValue", true));
            this.calcEdit2.EditValue = "";
            this.calcEdit2.Location = new System.Drawing.Point(123, 92);
            this.calcEdit2.Name = "calcEdit2";
            this.calcEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcEdit2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.calcEdit2.Size = new System.Drawing.Size(101, 22);
            this.calcEdit2.StyleController = this.styleController1;
            this.calcEdit2.TabIndex = 75;
            // 
            // lookUpEdit3
            // 
            this.lookUpEdit3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.PriceTypesBS, "PPTypeId", true));
            this.lookUpEdit3.Location = new System.Drawing.Point(415, 48);
            this.lookUpEdit3.Name = "lookUpEdit3";
            this.lookUpEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit3.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.lookUpEdit3.Properties.DisplayMember = "Name";
            this.lookUpEdit3.Properties.ShowFooter = false;
            this.lookUpEdit3.Properties.ShowHeader = false;
            this.lookUpEdit3.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEdit3.Properties.ValueMember = "PTypeId";
            this.lookUpEdit3.Size = new System.Drawing.Size(143, 22);
            this.lookUpEdit3.StyleController = this.styleController1;
            this.lookUpEdit3.TabIndex = 74;
            // 
            // lookUpEdit2
            // 
            this.lookUpEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.PriceTypesBS, "PPTypeId", true));
            this.lookUpEdit2.Location = new System.Drawing.Point(415, 94);
            this.lookUpEdit2.Name = "lookUpEdit2";
            this.lookUpEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit2.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.lookUpEdit2.Properties.DisplayMember = "Name";
            this.lookUpEdit2.Properties.ShowFooter = false;
            this.lookUpEdit2.Properties.ShowHeader = false;
            this.lookUpEdit2.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEdit2.Properties.ValueMember = "PTypeId";
            this.lookUpEdit2.Size = new System.Drawing.Size(143, 22);
            this.lookUpEdit2.StyleController = this.styleController1;
            this.lookUpEdit2.TabIndex = 73;
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.PriceTypesBS, "PPTypeId", true));
            this.lookUpEdit1.Location = new System.Drawing.Point(415, 48);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.lookUpEdit1.Properties.DisplayMember = "Name";
            this.lookUpEdit1.Properties.ShowFooter = false;
            this.lookUpEdit1.Properties.ShowHeader = false;
            this.lookUpEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lookUpEdit1.Properties.ValueMember = "PlId";
            this.lookUpEdit1.Size = new System.Drawing.Size(143, 22);
            this.lookUpEdit1.StyleController = this.styleController1;
            this.lookUpEdit1.TabIndex = 72;
            // 
            // ExtraTypeLookUpEdit
            // 
            this.ExtraTypeLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.PriceTypesBS, "ExtraType", true));
            this.ExtraTypeLookUpEdit.Location = new System.Drawing.Point(248, 48);
            this.ExtraTypeLookUpEdit.Name = "ExtraTypeLookUpEdit";
            this.ExtraTypeLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ExtraTypeLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.ExtraTypeLookUpEdit.Properties.DisplayMember = "Name";
            this.ExtraTypeLookUpEdit.Properties.ShowFooter = false;
            this.ExtraTypeLookUpEdit.Properties.ShowHeader = false;
            this.ExtraTypeLookUpEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.ExtraTypeLookUpEdit.Properties.ValueMember = "Id";
            this.ExtraTypeLookUpEdit.Size = new System.Drawing.Size(152, 22);
            this.ExtraTypeLookUpEdit.StyleController = this.styleController1;
            this.ExtraTypeLookUpEdit.TabIndex = 71;
            this.ExtraTypeLookUpEdit.EditValueChanged += new System.EventHandler(this.ExtraTypeLookUpEdit_EditValueChanged);
            // 
            // calcEdit1
            // 
            this.calcEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.PriceTypesBS, "OnValue", true));
            this.calcEdit1.EditValue = "";
            this.calcEdit1.Location = new System.Drawing.Point(123, 48);
            this.calcEdit1.Name = "calcEdit1";
            this.calcEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcEdit1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.calcEdit1.Size = new System.Drawing.Size(101, 22);
            this.calcEdit1.StyleController = this.styleController1;
            this.calcEdit1.TabIndex = 68;
            // 
            // checkEdit4
            // 
            this.checkEdit4.Location = new System.Drawing.Point(26, 93);
            this.checkEdit4.Name = "checkEdit4";
            this.checkEdit4.Properties.Caption = "Знижка, %";
            this.checkEdit4.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.checkEdit4.Properties.RadioGroupIndex = 1;
            this.checkEdit4.Size = new System.Drawing.Size(91, 20);
            this.checkEdit4.StyleController = this.styleController1;
            this.checkEdit4.TabIndex = 67;
            this.checkEdit4.TabStop = false;
            // 
            // checkEdit3
            // 
            this.checkEdit3.EditValue = true;
            this.checkEdit3.Location = new System.Drawing.Point(26, 47);
            this.checkEdit3.Name = "checkEdit3";
            this.checkEdit3.Properties.Caption = "Націнка, %";
            this.checkEdit3.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.checkEdit3.Properties.RadioGroupIndex = 1;
            this.checkEdit3.Size = new System.Drawing.Size(91, 20);
            this.checkEdit3.StyleController = this.styleController1;
            this.checkEdit3.TabIndex = 66;
            this.checkEdit3.CheckedChanged += new System.EventHandler(this.checkEdit3_CheckedChanged);
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(248, 95);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(118, 16);
            this.labelControl12.StyleController = this.styleController1;
            this.labelControl12.TabIndex = 58;
            this.labelControl12.Text = "від цінової категорії";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.DefCheckBox);
            this.panelControl1.Controls.Add(this.groupControl2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.NotesTextEdit);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(639, 324);
            this.panelControl1.TabIndex = 43;
            // 
            // PriceTypesBS
            // 
            this.PriceTypesBS.DataSource = typeof(SP_Sklad.SkladData.PriceTypes);
            // 
            // frmPricetypesEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 376);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.BottomPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPricetypesEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Властвості цінової категорії:";
            this.Load += new System.EventHandler(this.frmPricetypesEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NotesTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefCheckBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExtraTypeLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PriceTypesBS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.TextEdit NotesTextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckEdit DefCheckBox;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.CheckEdit checkEdit4;
        private DevExpress.XtraEditors.CheckEdit checkEdit3;
        private System.Windows.Forms.BindingSource PriceTypesBS;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.CalcEdit calcEdit1;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit2;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.LookUpEdit ExtraTypeLookUpEdit;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit3;
        private DevExpress.XtraEditors.CalcEdit calcEdit2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
    }
}