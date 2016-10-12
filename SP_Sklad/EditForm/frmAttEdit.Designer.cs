namespace SP_Sklad.EditForm
{
    partial class frmAttEdit
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
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.WaybillListBS = new System.Windows.Forms.BindingSource(this.components);
            this.ReceivedTextEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.AttDateEdit = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillListBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceivedTextEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttDateEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 149);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(455, 52);
            this.BottomPanel.TabIndex = 21;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(220, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Застосувати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(330, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Видалити";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // WaybillListBS
            // 
            this.WaybillListBS.DataSource = typeof(SP_Sklad.SkladData.WaybillList);
            // 
            // ReceivedTextEdit
            // 
            this.ReceivedTextEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReceivedTextEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WaybillListBS, "Received", true));
            this.ReceivedTextEdit.Location = new System.Drawing.Point(24, 39);
            this.ReceivedTextEdit.Name = "ReceivedTextEdit";
            this.ReceivedTextEdit.Size = new System.Drawing.Size(406, 22);
            this.ReceivedTextEdit.StyleController = this.styleController1;
            this.ReceivedTextEdit.TabIndex = 45;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Location = new System.Drawing.Point(24, 17);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(79, 16);
            this.labelControl3.TabIndex = 44;
            this.labelControl3.Text = "Через кого:";
            // 
            // textEdit1
            // 
            this.textEdit1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WaybillListBS, "AttNum", true));
            this.textEdit1.Location = new System.Drawing.Point(24, 100);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(169, 22);
            this.textEdit1.StyleController = this.styleController1;
            this.textEdit1.TabIndex = 46;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(24, 78);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(42, 16);
            this.labelControl7.StyleController = this.styleController1;
            this.labelControl7.TabIndex = 47;
            this.labelControl7.Text = "Номер:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(220, 78);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(76, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 48;
            this.labelControl1.Text = "Дата видачі:";
            // 
            // AttDateEdit
            // 
            this.AttDateEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.WaybillListBS, "AttDate", true));
            this.AttDateEdit.EditValue = null;
            this.AttDateEdit.Location = new System.Drawing.Point(220, 100);
            this.AttDateEdit.Name = "AttDateEdit";
            this.AttDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.AttDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.AttDateEdit.Size = new System.Drawing.Size(210, 22);
            this.AttDateEdit.StyleController = this.styleController1;
            this.AttDateEdit.TabIndex = 49;
            // 
            // frmAttEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 201);
            this.Controls.Add(this.AttDateEdit);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.ReceivedTextEdit);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.BottomPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAttEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Доручення";
            this.Load += new System.EventHandler(this.frmAttEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WaybillListBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReceivedTextEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AttDateEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.BindingSource WaybillListBS;
        private DevExpress.XtraEditors.TextEdit ReceivedTextEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit AttDateEdit;
    }
}