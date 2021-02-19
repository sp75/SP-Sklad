namespace SP_Sklad.ViewsForm
{
    partial class frmSetWBNote
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
            this.NoteEdit = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.NoteEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.labelControl4.Location = new System.Drawing.Point(12, 12);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(121, 33);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Примітка:";
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.OkButton.Appearance.ForeColor = System.Drawing.Color.Green;
            this.OkButton.Appearance.Options.UseFont = true;
            this.OkButton.Appearance.Options.UseForeColor = true;
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(348, 229);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(98, 46);
            this.OkButton.TabIndex = 10;
            this.OkButton.Text = "Так";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // NoteEdit
            // 
            this.NoteEdit.EditValue = "";
            this.NoteEdit.Location = new System.Drawing.Point(12, 51);
            this.NoteEdit.Name = "NoteEdit";
            this.NoteEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.NoteEdit.Properties.Appearance.Options.UseFont = true;
            this.NoteEdit.Size = new System.Drawing.Size(434, 159);
            this.NoteEdit.TabIndex = 11;
            this.NoteEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AmountEdit_KeyPress);
            // 
            // frmSetWBNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 287);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.NoteEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSetWBNote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавити примітку";
            this.Shown += new System.EventHandler(this.frmSetDiscountCard_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.NoteEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.MemoEdit NoteEdit;
    }
}