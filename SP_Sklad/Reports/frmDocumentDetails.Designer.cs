
namespace SP_Sklad.Reports
{
    partial class frmDocumentDetails
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
            this.ucDocumentDetails1 = new SP_Sklad.UserControls.ucDocumentDetails();
            this.SuspendLayout();
            // 
            // ucDocumentDetails1
            // 
            this.ucDocumentDetails1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucDocumentDetails1.Location = new System.Drawing.Point(0, 0);
            this.ucDocumentDetails1.Name = "ucDocumentDetails1";
            this.ucDocumentDetails1.Size = new System.Drawing.Size(1411, 654);
            this.ucDocumentDetails1.TabIndex = 0;
            // 
            // frmDocumentDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 654);
            this.Controls.Add(this.ucDocumentDetails1);
            this.Name = "frmDocumentDetails";
            this.Text = "1. Реєстр по документах (розгорнутий)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDocumentDetails_FormClosed);
            this.Shown += new System.EventHandler(this.frmDocumentDetails_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ucDocumentDetails ucDocumentDetails1;
    }
}