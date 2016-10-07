namespace SP_Sklad.WBDetForm
{
    partial class frmTechProcDet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTechProcDet));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.NowDateBtn = new DevExpress.XtraEditors.SimpleButton();
            this.NotesTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.OnDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.AmountEdit = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.TechProcessCB = new DevExpress.XtraEditors.LookUpEdit();
            this.WhBtn = new DevExpress.XtraEditors.SimpleButton();
            this.PersonComboBox = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NotesTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TechProcessCB.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PersonComboBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(481, 172);
            this.panel1.TabIndex = 24;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.NowDateBtn);
            this.panelControl1.Controls.Add(this.NotesTextEdit);
            this.panelControl1.Controls.Add(this.labelControl22);
            this.panelControl1.Controls.Add(this.OnDateEdit);
            this.panelControl1.Controls.Add(this.AmountEdit);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.TechProcessCB);
            this.panelControl1.Controls.Add(this.WhBtn);
            this.panelControl1.Controls.Add(this.PersonComboBox);
            this.panelControl1.Controls.Add(this.labelControl6);
            this.panelControl1.Controls.Add(this.simpleButton2);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(5, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(471, 162);
            this.panelControl1.TabIndex = 0;
            // 
            // NowDateBtn
            // 
            this.NowDateBtn.Image = ((System.Drawing.Image)(resources.GetObject("NowDateBtn.Image")));
            this.NowDateBtn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.NowDateBtn.Location = new System.Drawing.Point(436, 46);
            this.NowDateBtn.Name = "NowDateBtn";
            this.NowDateBtn.Size = new System.Drawing.Size(22, 22);
            this.NowDateBtn.TabIndex = 28;
            this.NowDateBtn.Click += new System.EventHandler(this.NowDateBtn_Click);
            // 
            // NotesTextEdit
            // 
            this.NotesTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NotesTextEdit.Location = new System.Drawing.Point(83, 124);
            this.NotesTextEdit.Name = "NotesTextEdit";
            this.NotesTextEdit.Size = new System.Drawing.Size(349, 22);
            this.NotesTextEdit.StyleController = this.styleController1;
            this.NotesTextEdit.TabIndex = 27;
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(245, 49);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(34, 16);
            this.labelControl22.StyleController = this.styleController1;
            this.labelControl22.TabIndex = 25;
            this.labelControl22.Text = "Дата:";
            // 
            // OnDateEdit
            // 
            this.OnDateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OnDateEdit.EditValue = null;
            this.OnDateEdit.Location = new System.Drawing.Point(285, 46);
            this.OnDateEdit.Name = "OnDateEdit";
            this.OnDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.OnDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.OnDateEdit.Properties.DisplayFormat.FormatString = "";
            this.OnDateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.OnDateEdit.Properties.EditFormat.FormatString = "";
            this.OnDateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.OnDateEdit.Properties.Mask.EditMask = "";
            this.OnDateEdit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.OnDateEdit.Size = new System.Drawing.Size(145, 22);
            this.OnDateEdit.StyleController = this.styleController1;
            this.OnDateEdit.TabIndex = 26;
            // 
            // AmountEdit
            // 
            this.AmountEdit.Location = new System.Drawing.Point(83, 46);
            this.AmountEdit.Name = "AmountEdit";
            this.AmountEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.AmountEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.AmountEdit.Properties.ShowCloseButton = true;
            this.AmountEdit.Size = new System.Drawing.Size(110, 22);
            this.AmountEdit.StyleController = this.styleController1;
            this.AmountEdit.TabIndex = 24;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 87);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(53, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 22;
            this.labelControl2.Text = "Виконав:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 127);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 21;
            this.labelControl1.Text = "Примітка";
            // 
            // TechProcessCB
            // 
            this.TechProcessCB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TechProcessCB.Location = new System.Drawing.Point(83, 11);
            this.TechProcessCB.Name = "TechProcessCB";
            this.TechProcessCB.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TechProcessCB.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.TechProcessCB.Properties.DisplayMember = "Name";
            this.TechProcessCB.Properties.ShowFooter = false;
            this.TechProcessCB.Properties.ShowHeader = false;
            this.TechProcessCB.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.TechProcessCB.Properties.ValueMember = "ProcId";
            this.TechProcessCB.Size = new System.Drawing.Size(347, 22);
            this.TechProcessCB.StyleController = this.styleController1;
            this.TechProcessCB.TabIndex = 19;
            // 
            // WhBtn
            // 
            this.WhBtn.Image = global::SP_Sklad.Properties.Resources.Службовц_;
            this.WhBtn.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.WhBtn.Location = new System.Drawing.Point(436, 84);
            this.WhBtn.Name = "WhBtn";
            this.WhBtn.Size = new System.Drawing.Size(22, 22);
            this.WhBtn.TabIndex = 18;
            this.WhBtn.Click += new System.EventHandler(this.WhBtn_Click);
            // 
            // PersonComboBox
            // 
            this.PersonComboBox.Location = new System.Drawing.Point(83, 84);
            this.PersonComboBox.Name = "PersonComboBox";
            this.PersonComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PersonComboBox.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Назва")});
            this.PersonComboBox.Properties.DisplayMember = "Name";
            this.PersonComboBox.Properties.ShowFooter = false;
            this.PersonComboBox.Properties.ShowHeader = false;
            this.PersonComboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.PersonComboBox.Properties.ValueMember = "KaId";
            this.PersonComboBox.Size = new System.Drawing.Size(347, 22);
            this.PersonComboBox.StyleController = this.styleController1;
            this.PersonComboBox.TabIndex = 17;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 49);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(35, 16);
            this.labelControl6.StyleController = this.styleController1;
            this.labelControl6.TabIndex = 16;
            this.labelControl6.Text = "Вихід:";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Image = global::SP_Sklad.Properties.Resources._нформац_я_про_рух_товар_в;
            this.simpleButton2.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton2.Location = new System.Drawing.Point(436, 11);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(22, 22);
            this.simpleButton2.TabIndex = 13;
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Location = new System.Drawing.Point(12, 14);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(55, 16);
            this.labelControl3.StyleController = this.styleController1;
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Процес:";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.OkButton);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 172);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(481, 53);
            this.panelControl2.TabIndex = 29;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(263, 12);
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
            this.simpleButton1.Location = new System.Drawing.Point(370, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(93, 30);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Відмінити";
            // 
            // frmTechProcDet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 225);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTechProcDet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Властивості технолгічного процесу";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTechProcDet_FormClosed);
            this.Load += new System.EventHandler(this.frmTechProcDet_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NotesTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TechProcessCB.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PersonComboBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit TechProcessCB;
        private DevExpress.XtraEditors.SimpleButton WhBtn;
        private DevExpress.XtraEditors.LookUpEdit PersonComboBox;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CalcEdit AmountEdit;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private DevExpress.XtraEditors.DateEdit OnDateEdit;
        private DevExpress.XtraEditors.TextEdit NotesTextEdit;
        private DevExpress.XtraEditors.SimpleButton NowDateBtn;
    }
}