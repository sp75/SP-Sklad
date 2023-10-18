namespace SP_Sklad.ViewsForm
{
    partial class frmBarCode
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
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.BarCodeEdit = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.BarCodeEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(12, 12);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(142, 33);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Штрих код:";
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.OkButton.Appearance.Options.UseFont = true;
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(255, 129);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(98, 46);
            this.OkButton.TabIndex = 10;
            this.OkButton.Text = "Так";
            // 
            // BarCodeEdit
            // 
            this.BarCodeEdit.Location = new System.Drawing.Point(12, 51);
            this.BarCodeEdit.Name = "BarCodeEdit";
            this.BarCodeEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 17F);
            this.BarCodeEdit.Properties.Appearance.Options.UseFont = true;
            this.BarCodeEdit.Size = new System.Drawing.Size(341, 34);
            this.BarCodeEdit.TabIndex = 62;
            this.BarCodeEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BarCodeEdit_KeyPress);
            // 
            // frmBarCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 187);
            this.Controls.Add(this.BarCodeEdit);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmBarCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Знайти по штрих коду";
            this.Shown += new System.EventHandler(this.frmSetDiscountCard_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.BarCodeEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        public DevExpress.XtraEditors.TextEdit BarCodeEdit;
    }
}