namespace SP_Sklad.EditForm
{
    partial class frmDiscountCardEdit
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
            this.components = new System.ComponentModel.Container();
            this.BanksPersonsBS = new System.Windows.Forms.BindingSource(this.components);
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit10 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.MemoEdit();
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.BanksPersonsBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit10.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BanksPersonsBS
            // 
            this.BanksPersonsBS.DataSource = typeof(SP_Sklad.SkladData.BanksPersons);
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(12, 171);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(386, 22);
            this.textEdit3.StyleController = this.styleController1;
            this.textEdit3.TabIndex = 67;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(12, 72);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(42, 16);
            this.labelControl12.StyleController = this.styleController1;
            this.labelControl12.TabIndex = 66;
            this.labelControl12.Text = "Адреса";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(12, 149);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(76, 16);
            this.labelControl6.StyleController = this.styleController1;
            this.labelControl6.TabIndex = 65;
            this.labelControl6.Text = "Кор. рахунок";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 15);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(40, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 64;
            this.labelControl4.Text = "Назва:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(431, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 63;
            this.labelControl2.Text = "МФО";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(12, 37);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(386, 22);
            this.textEdit1.StyleController = this.styleController1;
            this.textEdit1.TabIndex = 61;
            // 
            // textEdit10
            // 
            this.textEdit10.Location = new System.Drawing.Point(431, 37);
            this.textEdit10.Name = "textEdit10";
            this.textEdit10.Size = new System.Drawing.Size(171, 22);
            this.textEdit10.StyleController = this.styleController1;
            this.textEdit10.TabIndex = 60;
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(12, 94);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(590, 42);
            this.textEdit2.StyleController = this.styleController1;
            this.textEdit2.TabIndex = 62;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 365);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(659, 52);
            this.BottomPanel.TabIndex = 68;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(437, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Застосувати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(547, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // frmDiscountCardEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 417);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.textEdit3);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.textEdit10);
            this.Controls.Add(this.textEdit2);
            this.Name = "frmDiscountCardEdit";
            this.Text = "Редагувати дисконтну картку";
            ((System.ComponentModel.ISupportInitialize)(this.BanksPersonsBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit10.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource BanksPersonsBS;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.TextEdit textEdit10;
        private DevExpress.XtraEditors.MemoEdit textEdit2;
        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}