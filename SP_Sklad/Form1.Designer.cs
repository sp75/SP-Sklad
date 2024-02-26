
namespace SP_Sklad
{
    partial class Form1
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
            this.ucWBFilterPanel1 = new SP_Sklad.UserControls.ucWBFilterPanel();
            this.SuspendLayout();
            // 
            // ucWBFilterPanel1
            // 
            this.ucWBFilterPanel1.AutoSize = true;
            this.ucWBFilterPanel1.Location = new System.Drawing.Point(12, 12);
            this.ucWBFilterPanel1.Name = "ucWBFilterPanel1";
            this.ucWBFilterPanel1.Size = new System.Drawing.Size(1273, 84);
            this.ucWBFilterPanel1.TabIndex = 0;
            this.ucWBFilterPanel1.Title = "Кліент";
            this.ucWBFilterPanel1.TextChanged += new System.EventHandler(this.ucWBFilterPanel1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1315, 450);
            this.Controls.Add(this.ucWBFilterPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserControls.ucWBFilterPanel ucWBFilterPanel1;
    }
}