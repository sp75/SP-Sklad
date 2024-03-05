namespace update
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.AutoUpdateCheckEdit = new DevExpress.XtraEditors.CheckEdit();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AutoUpdateCheckEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(408, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 32;
            this.button1.Text = "Обновити";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(471, 23);
            this.progressBar1.TabIndex = 34;
            // 
            // AutoUpdateCheckEdit
            // 
            this.AutoUpdateCheckEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::update.Properties.Settings.Default, "auto_update", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.AutoUpdateCheckEdit.EditValue = global::update.Properties.Settings.Default.auto_update;
            this.AutoUpdateCheckEdit.Location = new System.Drawing.Point(12, 54);
            this.AutoUpdateCheckEdit.Name = "AutoUpdateCheckEdit";
            this.AutoUpdateCheckEdit.Properties.Caption = "Більше не запитувати";
            this.AutoUpdateCheckEdit.Properties.ValueChecked = 1;
            this.AutoUpdateCheckEdit.Properties.ValueUnchecked = 0;
            this.AutoUpdateCheckEdit.Size = new System.Drawing.Size(191, 20);
            this.AutoUpdateCheckEdit.TabIndex = 35;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(290, 51);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 23);
            this.button3.TabIndex = 36;
            this.button3.Text = "Обновити з WEB";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 85);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.AutoUpdateCheckEdit);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.IconOptions.Image = ((System.Drawing.Image)(resources.GetObject("Form1.IconOptions.Image")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Обновити SP-Sklad";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.AutoUpdateCheckEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private DevExpress.XtraEditors.CheckEdit AutoUpdateCheckEdit;
        private System.Windows.Forms.Button button3;
    }
}

