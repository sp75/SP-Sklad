namespace SP_Sklad.EditForm
{
    partial class frmCarEdit
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
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.NameTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.NumTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.DriversLookUpEdit = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit43 = new DevExpress.XtraEditors.CalcEdit();
            this.calcEdit1 = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.calcEdit2 = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.calcEdit3 = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.calcEdit4 = new DevExpress.XtraEditors.CalcEdit();
            this.CarsBS = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NameTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DriversLookUpEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit43.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarsBS)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 316);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(451, 52);
            this.BottomPanel.TabIndex = 21;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(216, 10);
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
            this.simpleButton1.Location = new System.Drawing.Point(326, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // NameTextEdit
            // 
            this.NameTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CarsBS, "Name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NameTextEdit.Location = new System.Drawing.Point(24, 39);
            this.NameTextEdit.Name = "NameTextEdit";
            this.NameTextEdit.Size = new System.Drawing.Size(243, 22);
            this.NameTextEdit.StyleController = this.styleController1;
            this.NameTextEdit.TabIndex = 45;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(24, 17);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(100, 16);
            this.labelControl3.TabIndex = 44;
            this.labelControl3.Text = "Назва машини:";
            // 
            // NumTextEdit
            // 
            this.NumTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CarsBS, "Number", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.NumTextEdit.Location = new System.Drawing.Point(289, 39);
            this.NumTextEdit.Name = "NumTextEdit";
            this.NumTextEdit.Size = new System.Drawing.Size(141, 22);
            this.NumTextEdit.StyleController = this.styleController1;
            this.NumTextEdit.TabIndex = 46;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(289, 17);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(42, 16);
            this.labelControl7.StyleController = this.styleController1;
            this.labelControl7.TabIndex = 47;
            this.labelControl7.Text = "Номер:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 253);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 50;
            this.labelControl2.Text = "Водій";
            // 
            // DriversLookUpEdit
            // 
            this.DriversLookUpEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CarsBS, "DriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DriversLookUpEdit.Location = new System.Drawing.Point(24, 275);
            this.DriversLookUpEdit.Name = "DriversLookUpEdit";
            this.DriversLookUpEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Clear)});
            this.DriversLookUpEdit.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name1")});
            this.DriversLookUpEdit.Properties.DisplayMember = "Name";
            this.DriversLookUpEdit.Properties.NullText = "";
            this.DriversLookUpEdit.Properties.PopupSizeable = false;
            this.DriversLookUpEdit.Properties.ShowFooter = false;
            this.DriversLookUpEdit.Properties.ShowHeader = false;
            this.DriversLookUpEdit.Properties.ValueMember = "KaId";
            this.DriversLookUpEdit.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.DriversLookUpEdit_Properties_ButtonClick);
            this.DriversLookUpEdit.Size = new System.Drawing.Size(406, 22);
            this.DriversLookUpEdit.StyleController = this.styleController1;
            this.DriversLookUpEdit.TabIndex = 51;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 81);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(143, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 52;
            this.labelControl1.Text = "Вантажопідйомність, кг:";
            // 
            // textEdit43
            // 
            this.textEdit43.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CarsBS, "СarryingСapacity", true));
            this.textEdit43.Location = new System.Drawing.Point(24, 103);
            this.textEdit43.Name = "textEdit43";
            this.textEdit43.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textEdit43.Size = new System.Drawing.Size(157, 22);
            this.textEdit43.StyleController = this.styleController1;
            this.textEdit43.TabIndex = 59;
            // 
            // calcEdit1
            // 
            this.calcEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CarsBS, "Weight", true));
            this.calcEdit1.Location = new System.Drawing.Point(24, 161);
            this.calcEdit1.Name = "calcEdit1";
            this.calcEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcEdit1.Size = new System.Drawing.Size(157, 22);
            this.calcEdit1.StyleController = this.styleController1;
            this.calcEdit1.TabIndex = 60;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(24, 139);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(69, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 61;
            this.labelControl4.Text = "Вага ТЗ, кг:";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(289, 81);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(78, 16);
            this.labelControl5.StyleController = this.styleController1;
            this.labelControl5.TabIndex = 63;
            this.labelControl5.Text = "Довжина ТЗ:";
            // 
            // calcEdit2
            // 
            this.calcEdit2.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CarsBS, "Length", true));
            this.calcEdit2.Location = new System.Drawing.Point(289, 103);
            this.calcEdit2.Name = "calcEdit2";
            this.calcEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcEdit2.Size = new System.Drawing.Size(137, 22);
            this.calcEdit2.StyleController = this.styleController1;
            this.calcEdit2.TabIndex = 62;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(289, 139);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(71, 16);
            this.labelControl6.StyleController = this.styleController1;
            this.labelControl6.TabIndex = 65;
            this.labelControl6.Text = "Ширина ТЗ:";
            // 
            // calcEdit3
            // 
            this.calcEdit3.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CarsBS, "Width", true));
            this.calcEdit3.Location = new System.Drawing.Point(289, 161);
            this.calcEdit3.Name = "calcEdit3";
            this.calcEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcEdit3.Size = new System.Drawing.Size(137, 22);
            this.calcEdit3.StyleController = this.styleController1;
            this.calcEdit3.TabIndex = 64;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(289, 203);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(64, 16);
            this.labelControl8.StyleController = this.styleController1;
            this.labelControl8.TabIndex = 67;
            this.labelControl8.Text = "Висота ТЗ:";
            // 
            // calcEdit4
            // 
            this.calcEdit4.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.CarsBS, "Height", true));
            this.calcEdit4.Location = new System.Drawing.Point(289, 225);
            this.calcEdit4.Name = "calcEdit4";
            this.calcEdit4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcEdit4.Size = new System.Drawing.Size(137, 22);
            this.calcEdit4.StyleController = this.styleController1;
            this.calcEdit4.TabIndex = 66;
            // 
            // CarsBS
            // 
            this.CarsBS.DataSource = typeof(SP_Sklad.SkladData.Cars);
            // 
            // frmCarEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 368);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.calcEdit4);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.calcEdit3);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.calcEdit2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.calcEdit1);
            this.Controls.Add(this.textEdit43);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.DriversLookUpEdit);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.NumTextEdit);
            this.Controls.Add(this.NameTextEdit);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.BottomPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconOptions.Image = global::SP_Sklad.Properties.Resources.truck_2;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCarEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Машина";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCarEdit_FormClosed);
            this.Load += new System.EventHandler(this.frmAttEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NameTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DriversLookUpEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit43.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CarsBS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.BindingSource CarsBS;
        private DevExpress.XtraEditors.TextEdit NameTextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit NumTextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LookUpEdit DriversLookUpEdit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CalcEdit textEdit43;
        private DevExpress.XtraEditors.CalcEdit calcEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CalcEdit calcEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.CalcEdit calcEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.CalcEdit calcEdit4;
    }
}