namespace SP_Sklad.ViewsForm
{
    partial class frmSetDiscountCard
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
            this.DiscountCartEdit = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.DiscountCartEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.labelControl4.Location = new System.Drawing.Point(12, 12);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(89, 33);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Номер:";
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.OkButton.Appearance.ForeColor = System.Drawing.Color.Green;
            this.OkButton.Appearance.Options.UseFont = true;
            this.OkButton.Appearance.Options.UseForeColor = true;
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(348, 117);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(98, 46);
            this.OkButton.TabIndex = 10;
            this.OkButton.Text = "Так";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // DiscountCartEdit
            // 
            this.DiscountCartEdit.EditValue = "";
            this.DiscountCartEdit.Location = new System.Drawing.Point(12, 51);
            this.DiscountCartEdit.Name = "DiscountCartEdit";
            this.DiscountCartEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.DiscountCartEdit.Properties.Appearance.Options.UseFont = true;
            this.DiscountCartEdit.Size = new System.Drawing.Size(434, 40);
            this.DiscountCartEdit.TabIndex = 11;
            this.DiscountCartEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AmountEdit_KeyPress);
            // 
            // frmSetDiscountCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 175);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.DiscountCartEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSetDiscountCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Вибір дисконтної картки";
            this.Shown += new System.EventHandler(this.frmSetDiscountCard_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DiscountCartEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.TextEdit DiscountCartEdit;
    }
}