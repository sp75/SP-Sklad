namespace SP_Sklad.ViewsForm
{
    partial class frmReport53
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
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.OnDateGroupBox = new System.Windows.Forms.Panel();
            this.dateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.dateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.OnDateDBEdit = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.WhComboBox = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            this.OnDateGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateDBEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateDBEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhComboBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 203);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(436, 52);
            this.BottomPanel.TabIndex = 18;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(12, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(83, 30);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Таблиця";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(324, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Створити звіт";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // OnDateGroupBox
            // 
            this.OnDateGroupBox.Controls.Add(this.WhComboBox);
            this.OnDateGroupBox.Controls.Add(this.labelControl10);
            this.OnDateGroupBox.Controls.Add(this.dateEdit2);
            this.OnDateGroupBox.Controls.Add(this.dateEdit1);
            this.OnDateGroupBox.Controls.Add(this.labelControl2);
            this.OnDateGroupBox.Controls.Add(this.OnDateDBEdit);
            this.OnDateGroupBox.Controls.Add(this.labelControl1);
            this.OnDateGroupBox.Controls.Add(this.labelControl3);
            this.OnDateGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OnDateGroupBox.Location = new System.Drawing.Point(0, 0);
            this.OnDateGroupBox.Name = "OnDateGroupBox";
            this.OnDateGroupBox.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.OnDateGroupBox.Size = new System.Drawing.Size(436, 203);
            this.OnDateGroupBox.TabIndex = 26;
            // 
            // dateEdit2
            // 
            this.dateEdit2.EditValue = new System.DateTime(2016, 3, 3, 16, 47, 59, 0);
            this.dateEdit2.Location = new System.Drawing.Point(186, 80);
            this.dateEdit2.Name = "dateEdit2";
            this.dateEdit2.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateEdit2.Properties.Appearance.Options.UseFont = true;
            this.dateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
            this.dateEdit2.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit2.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEdit2.Properties.MaskSettings.Set("mask", "d");
            this.dateEdit2.Properties.MinValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateEdit2.Size = new System.Drawing.Size(107, 22);
            this.dateEdit2.TabIndex = 9;
            // 
            // dateEdit1
            // 
            this.dateEdit1.EditValue = new System.DateTime(2016, 3, 3, 16, 47, 59, 0);
            this.dateEdit1.Location = new System.Drawing.Point(186, 48);
            this.dateEdit1.Name = "dateEdit1";
            this.dateEdit1.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dateEdit1.Properties.Appearance.Options.UseFont = true;
            this.dateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
            this.dateEdit1.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dateEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.dateEdit1.Properties.MaskSettings.Set("mask", "d");
            this.dateEdit1.Properties.MinValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateEdit1.Size = new System.Drawing.Size(107, 22);
            this.dateEdit1.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(8, 81);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(135, 16);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "Замовлення на дату 2:";
            // 
            // OnDateDBEdit
            // 
            this.OnDateDBEdit.EditValue = new System.DateTime(2016, 3, 3, 16, 47, 59, 0);
            this.OnDateDBEdit.Location = new System.Drawing.Point(186, 14);
            this.OnDateDBEdit.Name = "OnDateDBEdit";
            this.OnDateDBEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.OnDateDBEdit.Properties.Appearance.Options.UseFont = true;
            this.OnDateDBEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.OnDateDBEdit.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.False;
            this.OnDateDBEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.OnDateDBEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.OnDateDBEdit.Properties.MaskSettings.Set("mask", "d");
            this.OnDateDBEdit.Properties.MinValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.OnDateDBEdit.Size = new System.Drawing.Size(107, 22);
            this.OnDateDBEdit.TabIndex = 9;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(8, 49);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(124, 16);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Замовлення на дату:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(8, 15);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(172, 16);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Залишки на складах на дату:";
            // 
            // WhComboBox
            // 
            this.WhComboBox.Location = new System.Drawing.Point(186, 120);
            this.WhComboBox.Name = "WhComboBox";
            this.WhComboBox.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.WhComboBox.Properties.Appearance.Options.UseFont = true;
            this.WhComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.WhComboBox.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.WhComboBox.Properties.DisplayMember = "Name";
            this.WhComboBox.Properties.ShowFooter = false;
            this.WhComboBox.Properties.ShowHeader = false;
            this.WhComboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.WhComboBox.Properties.ValueMember = "WId";
            this.WhComboBox.Size = new System.Drawing.Size(238, 22);
            this.WhComboBox.TabIndex = 28;
            // 
            // labelControl10
            // 
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Location = new System.Drawing.Point(8, 123);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(40, 16);
            this.labelControl10.TabIndex = 27;
            this.labelControl10.Text = "Склад:";
            // 
            // frmReport53
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 255);
            this.Controls.Add(this.OnDateGroupBox);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmReport53";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Звіт для планування виробництва";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKaGroup_FormClosed);
            this.Load += new System.EventHandler(this.frmKaGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            this.OnDateGroupBox.ResumeLayout(false);
            this.OnDateGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateDBEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateDBEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WhComboBox.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        public System.Windows.Forms.Panel OnDateGroupBox;
        private DevExpress.XtraEditors.DateEdit OnDateDBEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.DateEdit dateEdit2;
        private DevExpress.XtraEditors.DateEdit dateEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LookUpEdit WhComboBox;
        private DevExpress.XtraEditors.LabelControl labelControl10;
    }
}