namespace SP_Sklad.EditForm
{
    partial class frmMatListEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMatListEdit));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.PriceEdit = new DevExpress.XtraEditors.CalcEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.AmountEdit = new DevExpress.XtraEditors.CalcEdit();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.timer1 = new System.Windows.Forms.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PriceEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panelControl1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(484, 209);
            this.panelControl2.TabIndex = 29;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(2, 2);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(480, 205);
            this.panelControl1.TabIndex = 29;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.simpleButton3);
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Controls.Add(this.PriceEdit);
            this.panelControl3.Controls.Add(this.labelControl4);
            this.panelControl3.Controls.Add(this.AmountEdit);
            this.panelControl3.Controls.Add(this.OkButton);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl3.Location = new System.Drawing.Point(2, 2);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(476, 201);
            this.panelControl3.TabIndex = 29;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton3.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton3.Image")));
            this.simpleButton3.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.simpleButton3.Location = new System.Drawing.Point(198, 56);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(40, 40);
            this.simpleButton3.TabIndex = 27;
            this.simpleButton3.ToolTip = "Обновити";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.labelControl5.Location = new System.Drawing.Point(283, 17);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(64, 33);
            this.labelControl5.TabIndex = 11;
            this.labelControl5.Text = "Ціна:";
            // 
            // PriceEdit
            // 
            this.PriceEdit.Enabled = false;
            this.PriceEdit.Location = new System.Drawing.Point(283, 56);
            this.PriceEdit.Name = "PriceEdit";
            this.PriceEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.PriceEdit.Properties.Appearance.Options.UseFont = true;
            this.PriceEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.PriceEdit.Size = new System.Drawing.Size(174, 40);
            this.PriceEdit.TabIndex = 10;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.labelControl4.Location = new System.Drawing.Point(23, 17);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(65, 33);
            this.labelControl4.TabIndex = 9;
            this.labelControl4.Text = "Вага:";
            // 
            // AmountEdit
            // 
            this.AmountEdit.Location = new System.Drawing.Point(23, 56);
            this.AmountEdit.Name = "AmountEdit";
            this.AmountEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.AmountEdit.Properties.Appearance.Options.UseFont = true;
            this.AmountEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.AmountEdit.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.AmountEdit_Properties_ButtonClick);
            this.AmountEdit.Size = new System.Drawing.Size(169, 40);
            this.AmountEdit.TabIndex = 8;
            this.AmountEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AmountEdit_KeyPress);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.OkButton.Appearance.ForeColor = System.Drawing.Color.Green;
            this.OkButton.Appearance.Options.UseFont = true;
            this.OkButton.Appearance.Options.UseForeColor = true;
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(359, 137);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(98, 46);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "Так";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMatListEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 209);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMatListEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редагувати позицію";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMatListEdit_FormClosed);
            this.Load += new System.EventHandler(this.frmMatListEdit_Load);
            this.Shown += new System.EventHandler(this.frmMatListEdit_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PriceEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        public DevExpress.XtraEditors.CalcEdit PriceEdit;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraEditors.CalcEdit AmountEdit;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
    }
}