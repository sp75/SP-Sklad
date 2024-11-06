namespace SP_Sklad
{
    partial class frmUserSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserSettings));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.CalcEdit();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.FontEdit();
            this.DiscountCheckBox = new DevExpress.XtraEditors.CheckEdit();
            this.UserBS = new System.Windows.Forms.BindingSource(this.components);
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit3 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiscountCheckBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Controls.Add(this.simpleButton2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 248);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(519, 54);
            this.panelControl2.TabIndex = 42;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.simpleButton1.Location = new System.Drawing.Point(295, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(113, 30);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "Застосувати";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(414, 12);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(93, 30);
            this.simpleButton2.TabIndex = 4;
            this.simpleButton2.Text = "Відмінити";
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(12, 124);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(42, 16);
            this.labelControl11.StyleController = this.styleController1;
            this.labelControl11.TabIndex = 60;
            this.labelControl11.Text = "Шрифт";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(237, 124);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(38, 16);
            this.labelControl10.StyleController = this.styleController1;
            this.labelControl10.TabIndex = 58;
            this.labelControl10.Text = "Розмір";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.comboBoxEdit1.Location = new System.Drawing.Point(237, 146);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(114, 22);
            this.comboBoxEdit1.StyleController = this.styleController1;
            this.comboBoxEdit1.TabIndex = 59;
            this.comboBoxEdit1.EditValueChanged += new System.EventHandler(this.comboBoxEdit1_EditValueChanged);
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.EditValue = "Tahoma";
            this.comboBoxEdit2.Location = new System.Drawing.Point(12, 146);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Properties.DropDownRows = 7;
            this.comboBoxEdit2.Size = new System.Drawing.Size(186, 22);
            this.comboBoxEdit2.StyleController = this.styleController1;
            this.comboBoxEdit2.TabIndex = 61;
            this.comboBoxEdit2.EditValueChanged += new System.EventHandler(this.comboBoxEdit2_EditValueChanged);
            // 
            // DiscountCheckBox
            // 
            this.DiscountCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.UserBS, "InternalEditor", true));
            this.DiscountCheckBox.Location = new System.Drawing.Point(12, 12);
            this.DiscountCheckBox.Name = "DiscountCheckBox";
            this.DiscountCheckBox.Properties.AutoWidth = true;
            this.DiscountCheckBox.Properties.Caption = "Внутрішній редактор";
            this.DiscountCheckBox.Size = new System.Drawing.Size(144, 20);
            this.DiscountCheckBox.StyleController = this.styleController1;
            this.DiscountCheckBox.TabIndex = 57;
            // 
            // UserBS
            // 
            this.UserBS.DataSource = typeof(SP_Sklad.SkladData.Users);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 57);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(81, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 56;
            this.labelControl2.Text = "Формат звітів";
            // 
            // textEdit3
            // 
            this.textEdit3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.UserBS, "ReportFormat", true));
            this.textEdit3.Location = new System.Drawing.Point(12, 79);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textEdit3.Properties.Items.AddRange(new object[] {
            "xlsx",
            "pdf"});
            this.textEdit3.Size = new System.Drawing.Size(181, 22);
            this.textEdit3.StyleController = this.styleController1;
            this.textEdit3.TabIndex = 55;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 213);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(278, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 62;
            this.labelControl1.Text = "* Після внесення змін перезапустіть програму!";
            // 
            // frmUserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 302);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl11);
            this.Controls.Add(this.labelControl10);
            this.Controls.Add(this.comboBoxEdit1);
            this.Controls.Add(this.comboBoxEdit2);
            this.Controls.Add(this.DiscountCheckBox);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.textEdit3);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("frmUserSettings.IconOptions.Image")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmUserSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Налаштувння користувача";
            this.Load += new System.EventHandler(this.frmUserSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DiscountCheckBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.BindingSource UserBS;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.CalcEdit comboBoxEdit1;
        private DevExpress.XtraEditors.FontEdit comboBoxEdit2;
        private DevExpress.XtraEditors.CheckEdit DiscountCheckBox;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.ComboBoxEdit textEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}