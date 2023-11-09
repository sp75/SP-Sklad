namespace SP_Sklad.ViewsForm
{
    partial class frmNumKeyboard
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
            this.AmountEdit = new DevExpress.XtraEditors.CalcEdit();
            this.panel1 = new System.Windows.Forms.Panel();
            this.numKeyboardUserControl2 = new SP_Sklad.UserControls.NumKeyboardUserControl();
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AmountEdit
            // 
            this.AmountEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.AmountEdit.Location = new System.Drawing.Point(10, 10);
            this.AmountEdit.Name = "AmountEdit";
            this.AmountEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 20F);
            this.AmountEdit.Properties.Appearance.Options.UseFont = true;
            this.AmountEdit.Properties.DisplayFormat.FormatString = "0.0000";
            this.AmountEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.AmountEdit.Size = new System.Drawing.Size(304, 40);
            this.AmountEdit.TabIndex = 1;
            this.AmountEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AmountEdit_KeyPress);
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.AmountEdit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.panel1.Size = new System.Drawing.Size(324, 50);
            this.panel1.TabIndex = 47;
            // 
            // numKeyboardUserControl2
            // 
            this.numKeyboardUserControl2._amount_edit = this.AmountEdit;
            this.numKeyboardUserControl2.AutoSize = true;
            this.numKeyboardUserControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numKeyboardUserControl2.Location = new System.Drawing.Point(0, 50);
            this.numKeyboardUserControl2.Name = "numKeyboardUserControl2";
            this.numKeyboardUserControl2.Size = new System.Drawing.Size(324, 297);
            this.numKeyboardUserControl2.TabIndex = 22;
            this.numKeyboardUserControl2.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // frmNumKeyboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(324, 347);
            this.Controls.Add(this.numKeyboardUserControl2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmNumKeyboard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Цифрова клавіатура";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmTestComPort_FormClosed);
            this.Load += new System.EventHandler(this.frmNumKeyboard_Load);
            this.Shown += new System.EventHandler(this.frmNumKeyboard_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public UserControls.NumKeyboardUserControl numKeyboardUserControl2;
        public DevExpress.XtraEditors.CalcEdit AmountEdit;
        private System.Windows.Forms.Panel panel1;
    }
}