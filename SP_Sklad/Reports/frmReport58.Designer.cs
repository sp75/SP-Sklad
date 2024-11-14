namespace SP_Sklad.ViewsForm
{
    partial class frmReport58
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.OnDateGroupBox = new System.Windows.Forms.Panel();
            this.EndDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.StartDateEdit = new DevExpress.XtraEditors.DateEdit();
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.RecipeComboBox = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            this.OnDateGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeComboBox.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 203);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(433, 52);
            this.BottomPanel.TabIndex = 18;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Location = new System.Drawing.Point(9, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(118, 30);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Показати списком";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(321, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Створити звіт";
            this.OkButton.Visible = false;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // OnDateGroupBox
            // 
            this.OnDateGroupBox.Controls.Add(this.RecipeComboBox);
            this.OnDateGroupBox.Controls.Add(this.labelControl3);
            this.OnDateGroupBox.Controls.Add(this.EndDateEdit);
            this.OnDateGroupBox.Controls.Add(this.labelControl1);
            this.OnDateGroupBox.Controls.Add(this.StartDateEdit);
            this.OnDateGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OnDateGroupBox.Location = new System.Drawing.Point(0, 0);
            this.OnDateGroupBox.Name = "OnDateGroupBox";
            this.OnDateGroupBox.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.OnDateGroupBox.Size = new System.Drawing.Size(433, 203);
            this.OnDateGroupBox.TabIndex = 26;
            // 
            // EndDateEdit
            // 
            this.EndDateEdit.EditValue = new System.DateTime(2016, 3, 3, 16, 47, 59, 0);
            this.EndDateEdit.Location = new System.Drawing.Point(170, 34);
            this.EndDateEdit.Name = "EndDateEdit";
            this.EndDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.EndDateEdit.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.EndDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.EndDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.EndDateEdit.Properties.MaskSettings.Set("mask", "g");
            this.EndDateEdit.Properties.MinValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.EndDateEdit.Size = new System.Drawing.Size(143, 22);
            this.EndDateEdit.StyleController = this.styleController1;
            this.EndDateEdit.TabIndex = 31;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(44, 16);
            this.labelControl1.StyleController = this.styleController1;
            this.labelControl1.TabIndex = 28;
            this.labelControl1.Text = "Період:";
            // 
            // StartDateEdit
            // 
            this.StartDateEdit.EditValue = new System.DateTime(2016, 3, 3, 16, 47, 59, 0);
            this.StartDateEdit.Location = new System.Drawing.Point(12, 34);
            this.StartDateEdit.Name = "StartDateEdit";
            this.StartDateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StartDateEdit.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.StartDateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.StartDateEdit.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.StartDateEdit.Properties.MaskSettings.Set("mask", "g");
            this.StartDateEdit.Properties.MinValue = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.StartDateEdit.Size = new System.Drawing.Size(152, 22);
            this.StartDateEdit.StyleController = this.styleController1;
            this.StartDateEdit.TabIndex = 29;
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // RecipeComboBox
            // 
            this.RecipeComboBox.Location = new System.Drawing.Point(12, 99);
            this.RecipeComboBox.Name = "RecipeComboBox";
            editorButtonImageOptions1.Image = global::SP_Sklad.Properties.Resources.recipes_1;
            editorButtonImageOptions1.ImageIndex = 14;
            this.RecipeComboBox.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.RecipeComboBox.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MatName", "Назва"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Amount", "К-сть")});
            this.RecipeComboBox.Properties.DisplayMember = "MatName";
            this.RecipeComboBox.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSearch;
            this.RecipeComboBox.Properties.ShowFooter = false;
            this.RecipeComboBox.Properties.ShowHeader = false;
            this.RecipeComboBox.Properties.ValueMember = "RecId";
            this.RecipeComboBox.Size = new System.Drawing.Size(409, 24);
            this.RecipeComboBox.StyleController = this.styleController1;
            this.RecipeComboBox.TabIndex = 33;
            this.RecipeComboBox.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RecipeComboBox_ButtonClick);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 77);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 16);
            this.labelControl3.StyleController = this.styleController1;
            this.labelControl3.TabIndex = 32;
            this.labelControl3.Text = "Рецепт:";
            // 
            // frmReport58
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 255);
            this.Controls.Add(this.OnDateGroupBox);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmReport58";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Обвалка сировини за період";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmKaGroup_FormClosed);
            this.Load += new System.EventHandler(this.frmKaGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            this.OnDateGroupBox.ResumeLayout(false);
            this.OnDateGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EndDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EndDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StartDateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RecipeComboBox.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        public System.Windows.Forms.Panel OnDateGroupBox;
        public DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.DateEdit EndDateEdit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit StartDateEdit;
        private DevExpress.XtraEditors.StyleController styleController1;
        private DevExpress.XtraEditors.LookUpEdit RecipeComboBox;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}