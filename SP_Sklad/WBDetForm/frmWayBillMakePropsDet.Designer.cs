namespace SP_Sklad.WBDetForm
{
    partial class frmWayBillMakePropsDet
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
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.MatComboBox = new DevExpress.XtraEditors.LookUpEdit();
            this.WayBillMakePropsBS = new System.Windows.Forms.BindingSource(this.components);
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.AmountEdit = new DevExpress.XtraEditors.CalcEdit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MatComboBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WayBillMakePropsBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.OkButton);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 105);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(444, 53);
            this.panelControl2.TabIndex = 31;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(226, 12);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(98, 30);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "Застосувати";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(333, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(93, 30);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Відмінити";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(35, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 32;
            this.labelControl1.Text = "Назва";
            // 
            // MatComboBox
            // 
            this.MatComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MatComboBox.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WayBillMakePropsBS, "MatId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MatComboBox.Location = new System.Drawing.Point(23, 44);
            this.MatComboBox.Name = "MatComboBox";
            this.MatComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.MatComboBox.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.MatComboBox.Properties.DisplayMember = "Name";
            this.MatComboBox.Properties.ShowFooter = false;
            this.MatComboBox.Properties.ShowHeader = false;
            this.MatComboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.MatComboBox.Properties.ValueMember = "MatId";
            this.MatComboBox.Size = new System.Drawing.Size(270, 22);
            this.MatComboBox.StyleController = this.styleController1;
            this.MatComboBox.TabIndex = 34;
            // 
            // WayBillMakePropsBS
            // 
            this.WayBillMakePropsBS.DataSource = typeof(SP_Sklad.SkladData.WayBillMakeProps);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(318, 22);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 35;
            this.labelControl2.Text = "К-сть";
            // 
            // AmountEdit
            // 
            this.AmountEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WayBillMakePropsBS, "Amount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AmountEdit.Location = new System.Drawing.Point(318, 44);
            this.AmountEdit.Name = "AmountEdit";
            this.AmountEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.AmountEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.AmountEdit.Properties.ShowCloseButton = true;
            this.AmountEdit.Size = new System.Drawing.Size(110, 22);
            this.AmountEdit.StyleController = this.styleController1;
            this.AmountEdit.TabIndex = 36;
            // 
            // frmWayBillMakePropsDet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 158);
            this.Controls.Add(this.AmountEdit);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.MatComboBox);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmWayBillMakePropsDet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Додаткові атрибути";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmWayBillMakePropsDet_FormClosed);
            this.Load += new System.EventHandler(this.frmWayBillMakePropsDet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MatComboBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WayBillMakePropsBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit MatComboBox;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.CalcEdit AmountEdit;
        private System.Windows.Forms.BindingSource WayBillMakePropsBS;

    }
}