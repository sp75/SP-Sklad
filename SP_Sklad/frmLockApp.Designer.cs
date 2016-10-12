namespace SP_Sklad
{
    partial class frmLockApp
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
            this.passtextEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.UserIDEdit = new DevExpress.XtraEditors.TextEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            ((System.ComponentModel.ISupportInitialize)(this.passtextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserIDEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // passtextEdit
            // 
            this.passtextEdit.Location = new System.Drawing.Point(102, 93);
            this.passtextEdit.Name = "passtextEdit";
            this.passtextEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.passtextEdit.Properties.Appearance.Options.UseFont = true;
            this.passtextEdit.Properties.PasswordChar = '*';
            this.passtextEdit.Size = new System.Drawing.Size(236, 30);
            this.passtextEdit.TabIndex = 40;
            this.passtextEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passtextEdit_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.labelControl2.Location = new System.Drawing.Point(18, 96);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 23);
            this.labelControl2.TabIndex = 39;
            this.labelControl2.Text = "Пароль";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.labelControl1.Location = new System.Drawing.Point(18, 47);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(46, 23);
            this.labelControl1.TabIndex = 38;
            this.labelControl1.Text = "Логін";
            // 
            // UserIDEdit
            // 
            this.UserIDEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::SP_Sklad.Properties.Settings.Default, "user_id", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.UserIDEdit.Location = new System.Drawing.Point(102, 44);
            this.UserIDEdit.Name = "UserIDEdit";
            this.UserIDEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 14F);
            this.UserIDEdit.Properties.Appearance.Options.UseFont = true;
            this.UserIDEdit.Size = new System.Drawing.Size(236, 30);
            this.UserIDEdit.TabIndex = 41;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.OkButton);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 144);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(361, 45);
            this.panelControl2.TabIndex = 42;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(273, 9);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(76, 26);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "Так";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.UserIDEdit);
            this.groupControl1.Controls.Add(this.passtextEdit);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(361, 144);
            this.groupControl1.TabIndex = 43;
            this.groupControl1.Text = "Тимчасове блокування";
            // 
            // frmLockApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 189);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLockApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLockApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLockApp_FormClosing);
            this.Load += new System.EventHandler(this.frmLockApp_Load);
            this.Shown += new System.EventHandler(this.frmLockApp_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.passtextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserIDEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit passtextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit UserIDEdit;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.GroupControl groupControl1;
    }
}