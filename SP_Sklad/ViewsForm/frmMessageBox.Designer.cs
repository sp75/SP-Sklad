namespace SP_Sklad.ViewsForm
{
    partial class frmMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMessageBox));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.checkEdit5 = new DevExpress.XtraEditors.CheckEdit();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.MessageText = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit5.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.checkEdit5);
            this.panelControl2.Controls.Add(this.OkButton);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 85);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(556, 52);
            this.panelControl2.TabIndex = 21;
            // 
            // checkEdit5
            // 
            this.checkEdit5.Location = new System.Drawing.Point(12, 15);
            this.checkEdit5.Name = "checkEdit5";
            this.checkEdit5.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.checkEdit5.Properties.Appearance.Options.UseFont = true;
            this.checkEdit5.Properties.Caption = "Не показувати це повідомлення";
            this.checkEdit5.Size = new System.Drawing.Size(248, 20);
            this.checkEdit5.TabIndex = 65;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.OkButton.Location = new System.Drawing.Point(371, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(84, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Так";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.No;
            this.simpleButton1.Location = new System.Drawing.Point(461, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(83, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Ні";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.pictureBox1.Size = new System.Drawing.Size(84, 85);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // MessageText
            // 
            this.MessageText.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.MessageText.Appearance.Options.UseFont = true;
            this.MessageText.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.MessageText.Dock = System.Windows.Forms.DockStyle.Top;
            this.MessageText.Location = new System.Drawing.Point(84, 0);
            this.MessageText.Name = "MessageText";
            this.MessageText.Size = new System.Drawing.Size(472, 19);
            this.MessageText.TabIndex = 59;
            this.MessageText.Text = "MessageText ";
            // 
            // frmMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 137);
            this.Controls.Add(this.MessageText);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMessageBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMessageBox";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMessageBox_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit5.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.CheckEdit checkEdit5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl MessageText;
    }
}