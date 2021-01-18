namespace SP_Sklad.ViewsForm
{
    partial class frmSetCustomDiscount
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
            this.AmountEdit = new DevExpress.XtraEditors.CalcEdit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.labelControl4.Location = new System.Drawing.Point(12, 12);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(165, 40);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Знижка, %";
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.OkButton.Appearance.ForeColor = System.Drawing.Color.Green;
            this.OkButton.Appearance.Options.UseFont = true;
            this.OkButton.Appearance.Options.UseForeColor = true;
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(255, 126);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(98, 46);
            this.OkButton.TabIndex = 10;
            this.OkButton.Text = "Так";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // AmountEdit
            // 
            this.AmountEdit.Location = new System.Drawing.Point(12, 58);
            this.AmountEdit.Name = "AmountEdit";
            this.AmountEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.AmountEdit.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.AmountEdit.Properties.Appearance.Options.UseFont = true;
            this.AmountEdit.Properties.Appearance.Options.UseForeColor = true;
            this.AmountEdit.Properties.DisplayFormat.FormatString = "0.0000";
            this.AmountEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.AmountEdit.Properties.ShowCloseButton = true;
            this.AmountEdit.Size = new System.Drawing.Size(341, 46);
            this.AmountEdit.TabIndex = 45;
            this.AmountEdit.TabStop = false;
            this.AmountEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AmountEdit_KeyPress);
            // 
            // frmSetCustomDiscount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 184);
            this.Controls.Add(this.AmountEdit);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.OkButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSetCustomDiscount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Застосувати знижку";
            this.Shown += new System.EventHandler(this.frmSetDiscountCard_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.CalcEdit AmountEdit;
    }
}