namespace SP_Sklad.EditForm
{
    partial class frmDiscountCardEdit
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
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.MemoEdit();
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.KASaldoEdit = new DevExpress.XtraEditors.CalcEdit();
            this.KagentComboBox = new DevExpress.XtraEditors.LookUpEdit();
            this.DiscCardsBS = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KASaldoEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KagentComboBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiscCardsBS)).BeginInit();
            this.SuspendLayout();
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(12, 162);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(52, 16);
            this.labelControl12.StyleController = this.styleController1;
            this.labelControl12.TabIndex = 66;
            this.labelControl12.Text = "Примітка";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(37, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 64;
            this.labelControl4.Text = "Номер";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(330, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(97, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 63;
            this.labelControl2.Text = "Дата закінчення";
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.DiscCardsBS, "Num", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit1.Location = new System.Drawing.Point(12, 37);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(295, 22);
            this.textEdit1.StyleController = this.styleController1;
            this.textEdit1.TabIndex = 61;
            // 
            // textEdit2
            // 
            this.textEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.DiscCardsBS, "Notes", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textEdit2.Location = new System.Drawing.Point(12, 184);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(489, 42);
            this.textEdit2.StyleController = this.styleController1;
            this.textEdit2.TabIndex = 62;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 265);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(530, 52);
            this.BottomPanel.TabIndex = 68;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(308, 10);
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
            this.simpleButton1.Location = new System.Drawing.Point(418, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // dateEdit2
            // 
            this.dateEdit2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dateEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.DiscCardsBS, "ExpireDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateEdit2.EditValue = null;
            this.dateEdit2.Location = new System.Drawing.Point(325, 37);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.dateEdit2.Properties.Mask.EditMask = "";
            this.dateEdit2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dateEdit2.Size = new System.Drawing.Size(176, 22);
            this.dateEdit2.StyleController = this.styleController1;
            this.dateEdit2.TabIndex = 69;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 86);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 70;
            this.labelControl1.Text = "Власник";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(330, 86);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(64, 16);
            this.labelControl3.StyleController = this.styleController1;
            this.labelControl3.TabIndex = 71;
            this.labelControl3.Text = "Знижка, %";
            // 
            // KASaldoEdit
            // 
            this.KASaldoEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.DiscCardsBS, "OnValue", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.KASaldoEdit.Location = new System.Drawing.Point(325, 108);
            this.KASaldoEdit.Name = "KASaldoEdit";
            this.KASaldoEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.KASaldoEdit.Properties.DisplayFormat.FormatString = "0.00";
            this.KASaldoEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.KASaldoEdit.Properties.EditFormat.FormatString = "0.00";
            this.KASaldoEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.KASaldoEdit.Size = new System.Drawing.Size(176, 22);
            this.KASaldoEdit.StyleController = this.styleController1;
            this.KASaldoEdit.TabIndex = 72;
            // 
            // KagentComboBox
            // 
            this.KagentComboBox.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.DiscCardsBS, "KaId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.KagentComboBox.Location = new System.Drawing.Point(12, 108);
            this.KagentComboBox.Name = "KagentComboBox";
            this.KagentComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
            this.KagentComboBox.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.KagentComboBox.Properties.DisplayMember = "Name";
            this.KagentComboBox.Properties.ShowFooter = false;
            this.KagentComboBox.Properties.ShowHeader = false;
            this.KagentComboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.KagentComboBox.Properties.ValueMember = "KaId";
            this.KagentComboBox.Size = new System.Drawing.Size(295, 22);
            this.KagentComboBox.StyleController = this.styleController1;
            this.KagentComboBox.TabIndex = 73;
            this.KagentComboBox.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.KagentComboBox_ButtonClick);
            // 
            // DiscCardsBS
            // 
            this.DiscCardsBS.DataSource = typeof(SP_Sklad.SkladData.DiscCards);
            // 
            // frmDiscountCardEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 317);
            this.Controls.Add(this.KagentComboBox);
            this.Controls.Add(this.KASaldoEdit);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.dateEdit2);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.textEdit2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDiscountCardEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редагувати дисконтну картку";
            this.Load += new System.EventHandler(this.frmDiscountCardEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KASaldoEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KagentComboBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiscCardsBS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource DiscCardsBS;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.MemoEdit textEdit2;
        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CalcEdit KASaldoEdit;
        private DevExpress.XtraEditors.LookUpEdit KagentComboBox;
    }
}