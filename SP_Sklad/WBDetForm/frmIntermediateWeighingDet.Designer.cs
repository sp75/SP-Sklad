namespace SP_Sklad.WBDetForm
{
    partial class frmIntermediateWeighingDet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntermediateWeighingDet));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.CalcAmount = new DevExpress.XtraEditors.TextEdit();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.TotalEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl29 = new DevExpress.XtraEditors.LabelControl();
            this.IntermediateWeighingEdit = new DevExpress.XtraEditors.TextEdit();
            this.ByRecipeEdit = new DevExpress.XtraEditors.TextEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.calcEdit1 = new DevExpress.XtraEditors.CalcEdit();
            this.IntermediateWeighingDetBS = new System.Windows.Forms.BindingSource(this.components);
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.AmountEdit = new DevExpress.XtraEditors.CalcEdit();
            this.MatComboBox = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CalcAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntermediateWeighingEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ByRecipeEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntermediateWeighingDetBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatComboBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(394, 246);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(98, 26);
            this.OkButton.TabIndex = 5;
            this.OkButton.Text = "Застосувати";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(498, 246);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(93, 26);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "Відмінити";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Controls.Add(this.OkButton);
            this.panelControl2.Controls.Add(this.panel1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(598, 288);
            this.panelControl2.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupControl3);
            this.panel1.Controls.Add(this.panelControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(594, 224);
            this.panel1.TabIndex = 25;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Controls.Add(this.CalcAmount);
            this.groupControl3.Controls.Add(this.labelControl18);
            this.groupControl3.Controls.Add(this.TotalEdit);
            this.groupControl3.Controls.Add(this.labelControl17);
            this.groupControl3.Controls.Add(this.labelControl29);
            this.groupControl3.Controls.Add(this.IntermediateWeighingEdit);
            this.groupControl3.Controls.Add(this.ByRecipeEdit);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(5, 90);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(584, 129);
            this.groupControl3.TabIndex = 26;
            this.groupControl3.Tag = "";
            this.groupControl3.Text = "Підсумок";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl4.Location = new System.Drawing.Point(12, 32);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(154, 16);
            this.labelControl4.StyleController = this.styleController1;
            this.labelControl4.TabIndex = 29;
            this.labelControl4.Text = "В середньому на закладку";
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // CalcAmount
            // 
            this.CalcAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CalcAmount.Enabled = false;
            this.CalcAmount.Location = new System.Drawing.Point(183, 29);
            this.CalcAmount.Name = "CalcAmount";
            this.CalcAmount.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.CalcAmount.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.CalcAmount.Properties.Appearance.Options.UseFont = true;
            this.CalcAmount.Properties.Appearance.Options.UseForeColor = true;
            this.CalcAmount.Properties.DisplayFormat.FormatString = "0.0000";
            this.CalcAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.CalcAmount.Size = new System.Drawing.Size(126, 22);
            this.CalcAmount.StyleController = this.styleController1;
            this.CalcAmount.TabIndex = 28;
            // 
            // labelControl18
            // 
            this.labelControl18.Location = new System.Drawing.Point(309, 97);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(131, 16);
            this.labelControl18.StyleController = this.styleController1;
            this.labelControl18.TabIndex = 27;
            this.labelControl18.Text = "Залишилось зважити:";
            // 
            // TotalEdit
            // 
            this.TotalEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TotalEdit.Enabled = false;
            this.TotalEdit.Location = new System.Drawing.Point(446, 94);
            this.TotalEdit.Name = "TotalEdit";
            this.TotalEdit.Properties.DisplayFormat.FormatString = "0.00";
            this.TotalEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TotalEdit.Size = new System.Drawing.Size(126, 22);
            this.TotalEdit.StyleController = this.styleController1;
            this.TotalEdit.TabIndex = 26;
            // 
            // labelControl17
            // 
            this.labelControl17.Location = new System.Drawing.Point(341, 65);
            this.labelControl17.Name = "labelControl17";
            this.labelControl17.Size = new System.Drawing.Size(99, 16);
            this.labelControl17.StyleController = this.styleController1;
            this.labelControl17.TabIndex = 24;
            this.labelControl17.Text = "Всього зважено:";
            // 
            // labelControl29
            // 
            this.labelControl29.Location = new System.Drawing.Point(347, 32);
            this.labelControl29.Name = "labelControl29";
            this.labelControl29.Size = new System.Drawing.Size(93, 16);
            this.labelControl29.StyleController = this.styleController1;
            this.labelControl29.TabIndex = 23;
            this.labelControl29.Text = "По виробницву:";
            // 
            // IntermediateWeighingEdit
            // 
            this.IntermediateWeighingEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IntermediateWeighingEdit.Enabled = false;
            this.IntermediateWeighingEdit.Location = new System.Drawing.Point(446, 62);
            this.IntermediateWeighingEdit.Name = "IntermediateWeighingEdit";
            this.IntermediateWeighingEdit.Properties.DisplayFormat.FormatString = "0.00";
            this.IntermediateWeighingEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.IntermediateWeighingEdit.Size = new System.Drawing.Size(126, 22);
            this.IntermediateWeighingEdit.StyleController = this.styleController1;
            this.IntermediateWeighingEdit.TabIndex = 21;
            // 
            // ByRecipeEdit
            // 
            this.ByRecipeEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ByRecipeEdit.Enabled = false;
            this.ByRecipeEdit.Location = new System.Drawing.Point(446, 29);
            this.ByRecipeEdit.Name = "ByRecipeEdit";
            this.ByRecipeEdit.Properties.DisplayFormat.FormatString = "0.00";
            this.ByRecipeEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ByRecipeEdit.Size = new System.Drawing.Size(126, 22);
            this.ByRecipeEdit.StyleController = this.styleController1;
            this.ByRecipeEdit.TabIndex = 20;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.calcEdit1);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.AmountEdit);
            this.panelControl1.Controls.Add(this.MatComboBox);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(5, 5);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(584, 85);
            this.panelControl1.TabIndex = 0;
            // 
            // calcEdit1
            // 
            this.calcEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.IntermediateWeighingDetBS, "TaraAmount", true));
            this.calcEdit1.Location = new System.Drawing.Point(446, 50);
            this.calcEdit1.Name = "calcEdit1";
            this.calcEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcEdit1.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.calcEdit1.Properties.ShowCloseButton = true;
            this.calcEdit1.Size = new System.Drawing.Size(126, 22);
            this.calcEdit1.StyleController = this.styleController1;
            this.calcEdit1.TabIndex = 45;
            // 
            // IntermediateWeighingDetBS
            // 
            this.IntermediateWeighingDetBS.DataSource = typeof(SP_Sklad.SkladData.IntermediateWeighingDet);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(411, 53);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 16);
            this.labelControl2.StyleController = this.styleController1;
            this.labelControl2.TabIndex = 44;
            this.labelControl2.Text = "Тара";
            // 
            // AmountEdit
            // 
            this.AmountEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.IntermediateWeighingDetBS, "Amount", true));
            this.AmountEdit.Location = new System.Drawing.Point(446, 15);
            this.AmountEdit.Name = "AmountEdit";
            this.AmountEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("AmountEdit.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F12), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, true)});
            this.AmountEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.AmountEdit.Properties.ShowCloseButton = true;
            this.AmountEdit.Size = new System.Drawing.Size(126, 22);
            this.AmountEdit.StyleController = this.styleController1;
            this.AmountEdit.TabIndex = 43;
            this.AmountEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.AmountEdit_ButtonClick);
            this.AmountEdit.EditValueChanged += new System.EventHandler(this.AmountEdit_EditValueChanged);
            this.AmountEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AmountEdit_KeyPress);
            this.AmountEdit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AmountEdit_MouseUp);
            // 
            // MatComboBox
            // 
            this.MatComboBox.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.IntermediateWeighingDetBS, "MatId", true));
            this.MatComboBox.Location = new System.Drawing.Point(85, 15);
            this.MatComboBox.Name = "MatComboBox";
            this.MatComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.MatComboBox.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MatName", "Назва")});
            this.MatComboBox.Properties.DisplayMember = "MatName";
            this.MatComboBox.Properties.ShowFooter = false;
            this.MatComboBox.Properties.ShowHeader = false;
            this.MatComboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.MatComboBox.Properties.ValueMember = "MatId";
            this.MatComboBox.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RecipeComboBox_Properties_ButtonClick);
            this.MatComboBox.Size = new System.Drawing.Size(280, 22);
            this.MatComboBox.StyleController = this.styleController1;
            this.MatComboBox.TabIndex = 23;
            this.MatComboBox.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.MatComboBox_ButtonClick);
            this.MatComboBox.EditValueChanged += new System.EventHandler(this.RecipeComboBox_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Location = new System.Drawing.Point(12, 18);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(67, 16);
            this.labelControl3.StyleController = this.styleController1;
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Сировина:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(406, 18);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(34, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 39;
            this.labelControl1.Text = "Кі-сть";
            // 
            // frmIntermediateWeighingDet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 288);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmIntermediateWeighingDet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Додати/Редагувати товар";
            this.Load += new System.EventHandler(this.frmPlannedCalculationDetDet_Load);
            this.Shown += new System.EventHandler(this.frmIntermediateWeighingDet_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CalcAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntermediateWeighingEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ByRecipeEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntermediateWeighingDetBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AmountEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MatComboBox.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LookUpEdit MatComboBox;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.BindingSource IntermediateWeighingDetBS;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CalcEdit AmountEdit;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.TextEdit TotalEdit;
        private DevExpress.XtraEditors.LabelControl labelControl17;
        private DevExpress.XtraEditors.LabelControl labelControl29;
        private DevExpress.XtraEditors.TextEdit IntermediateWeighingEdit;
        private DevExpress.XtraEditors.TextEdit ByRecipeEdit;
        private DevExpress.XtraEditors.CalcEdit calcEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit CalcAmount;
    }
}