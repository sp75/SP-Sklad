namespace SP_Sklad.EditForm
{
    partial class frmTaraGroupEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTaraGroupEdit));
            this.styleController1 = new DevExpress.XtraEditors.StyleController(this.components);
            this.TaraGroupDS = new System.Windows.Forms.BindingSource(this.components);
            this.DirTreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.GrpIdEdit = new DevExpress.XtraEditors.TreeListLookUpEdit();
            this.treeListLookUpEdit1TreeList = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.checkEdit4 = new DevExpress.XtraEditors.CheckEdit();
            this.checkEdit3 = new DevExpress.XtraEditors.CheckEdit();
            this.textEdit10 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.xtraTabPage4 = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.textEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.ImageList = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TaraGroupDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirTreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrpIdEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit10.Properties)).BeginInit();
            this.xtraTabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageList)).BeginInit();
            this.SuspendLayout();
            // 
            // styleController1
            // 
            this.styleController1.Appearance.Font = new System.Drawing.Font("Tahoma", 10F);
            this.styleController1.Appearance.Options.UseFont = true;
            // 
            // TaraGroupDS
            // 
            this.TaraGroupDS.DataSource = typeof(SP_Sklad.SkladData.TaraGroup);
            // 
            // DirTreeList
            // 
            this.DirTreeList.Appearance.FocusedCell.BackColor = System.Drawing.Color.LightSkyBlue;
            this.DirTreeList.Appearance.FocusedCell.Options.UseBackColor = true;
            this.DirTreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.DirTreeList.Dock = System.Windows.Forms.DockStyle.Left;
            this.DirTreeList.ImageIndexFieldName = "ImgIdx";
            this.DirTreeList.KeyFieldName = "Id";
            this.DirTreeList.Location = new System.Drawing.Point(0, 0);
            this.DirTreeList.Name = "DirTreeList";
            this.DirTreeList.OptionsBehavior.Editable = false;
            this.DirTreeList.OptionsView.ShowColumns = false;
            this.DirTreeList.OptionsView.ShowHorzLines = false;
            this.DirTreeList.OptionsView.ShowIndicator = false;
            this.DirTreeList.OptionsView.ShowVertLines = false;
            this.DirTreeList.ParentFieldName = "ParentId";
            this.DirTreeList.SelectImageList = this.ImageList;
            this.DirTreeList.Size = new System.Drawing.Size(210, 274);
            this.DirTreeList.TabIndex = 35;
            this.DirTreeList.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.DirTreeList_FocusedNodeChanged);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Назва";
            this.treeListColumn1.FieldName = "Text";
            this.treeListColumn1.MinWidth = 34;
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(210, 0);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(10, 274);
            this.splitterControl1.TabIndex = 42;
            this.splitterControl1.TabStop = false;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.OkButton);
            this.panelControl2.Controls.Add(this.simpleButton1);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 274);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(793, 54);
            this.panelControl2.TabIndex = 43;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(575, 13);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(98, 30);
            this.OkButton.TabIndex = 3;
            this.OkButton.Text = "Застосувати";
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(682, 13);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(93, 30);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Відмінити";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabControl1.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(210, 0);
            this.xtraTabControl1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.xtraTabControl1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(583, 274);
            this.xtraTabControl1.TabIndex = 44;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage4});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.groupControl2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(583, 252);
            this.xtraTabPage1.Text = "Основна інформація";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.GrpIdEdit);
            this.groupControl2.Controls.Add(this.checkEdit4);
            this.groupControl2.Controls.Add(this.checkEdit3);
            this.groupControl2.Controls.Add(this.textEdit10);
            this.groupControl2.Controls.Add(this.labelControl3);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(583, 252);
            this.groupControl2.TabIndex = 17;
            this.groupControl2.Tag = "";
            this.groupControl2.Text = " Основна інформація про групу товарів";
            // 
            // GrpIdEdit
            // 
            this.GrpIdEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpIdEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.TaraGroupDS, "PId", true));
            this.GrpIdEdit.Location = new System.Drawing.Point(19, 135);
            this.GrpIdEdit.Name = "GrpIdEdit";
            this.GrpIdEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.GrpIdEdit.Properties.DisplayMember = "Name";
            this.GrpIdEdit.Properties.NullText = "";
            this.GrpIdEdit.Properties.TreeList = this.treeListLookUpEdit1TreeList;
            this.GrpIdEdit.Properties.ValueMember = "GrpId";
            this.GrpIdEdit.Size = new System.Drawing.Size(546, 22);
            this.GrpIdEdit.StyleController = this.styleController1;
            this.GrpIdEdit.TabIndex = 51;
            // 
            // treeListLookUpEdit1TreeList
            // 
            this.treeListLookUpEdit1TreeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn2});
            this.treeListLookUpEdit1TreeList.KeyFieldName = "GrpId";
            this.treeListLookUpEdit1TreeList.Location = new System.Drawing.Point(42, 145);
            this.treeListLookUpEdit1TreeList.Name = "treeListLookUpEdit1TreeList";
            this.treeListLookUpEdit1TreeList.OptionsView.ShowColumns = false;
            this.treeListLookUpEdit1TreeList.OptionsView.ShowHorzLines = false;
            this.treeListLookUpEdit1TreeList.OptionsView.ShowIndicator = false;
            this.treeListLookUpEdit1TreeList.OptionsView.ShowVertLines = false;
            this.treeListLookUpEdit1TreeList.ParentFieldName = "PId";
            this.treeListLookUpEdit1TreeList.SelectImageList = this.ImageList;
            this.treeListLookUpEdit1TreeList.Size = new System.Drawing.Size(466, 200);
            this.treeListLookUpEdit1TreeList.TabIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "treeListColumn2";
            this.treeListColumn2.FieldName = "Name";
            this.treeListColumn2.MinWidth = 34;
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 0;
            this.treeListColumn2.Width = 93;
            // 
            // checkEdit4
            // 
            this.checkEdit4.Location = new System.Drawing.Point(19, 193);
            this.checkEdit4.Name = "checkEdit4";
            this.checkEdit4.Properties.Caption = "Не підлегла жодній товарній групі";
            this.checkEdit4.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.checkEdit4.Properties.RadioGroupIndex = 1;
            this.checkEdit4.Size = new System.Drawing.Size(282, 20);
            this.checkEdit4.StyleController = this.styleController1;
            this.checkEdit4.TabIndex = 50;
            this.checkEdit4.TabStop = false;
            this.checkEdit4.CheckedChanged += new System.EventHandler(this.checkEdit4_CheckedChanged);
            // 
            // checkEdit3
            // 
            this.checkEdit3.EditValue = true;
            this.checkEdit3.Location = new System.Drawing.Point(19, 109);
            this.checkEdit3.Name = "checkEdit3";
            this.checkEdit3.Properties.Caption = "Підлегла товарній групі";
            this.checkEdit3.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.checkEdit3.Properties.RadioGroupIndex = 1;
            this.checkEdit3.Size = new System.Drawing.Size(354, 20);
            this.checkEdit3.StyleController = this.styleController1;
            this.checkEdit3.TabIndex = 49;
            this.checkEdit3.CheckedChanged += new System.EventHandler(this.checkEdit3_CheckedChanged);
            // 
            // textEdit10
            // 
            this.textEdit10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEdit10.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.TaraGroupDS, "Name", true));
            this.textEdit10.Location = new System.Drawing.Point(19, 56);
            this.textEdit10.Name = "textEdit10";
            this.textEdit10.Size = new System.Drawing.Size(400, 22);
            this.textEdit10.StyleController = this.styleController1;
            this.textEdit10.TabIndex = 34;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(19, 34);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(85, 16);
            this.labelControl3.StyleController = this.styleController1;
            this.labelControl3.TabIndex = 31;
            this.labelControl3.Text = "Найменування";
            // 
            // xtraTabPage4
            // 
            this.xtraTabPage4.Controls.Add(this.groupControl5);
            this.xtraTabPage4.Name = "xtraTabPage4";
            this.xtraTabPage4.Size = new System.Drawing.Size(583, 252);
            this.xtraTabPage4.Text = "Примітка";
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.textEdit1);
            this.groupControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl5.Location = new System.Drawing.Point(0, 0);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Padding = new System.Windows.Forms.Padding(5);
            this.groupControl5.Size = new System.Drawing.Size(583, 252);
            this.groupControl5.TabIndex = 16;
            this.groupControl5.Tag = "";
            this.groupControl5.Text = "Примітка";
            // 
            // textEdit1
            // 
            this.textEdit1.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.TaraGroupDS, "Notes", true));
            this.textEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEdit1.Location = new System.Drawing.Point(7, 28);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(569, 217);
            this.textEdit1.TabIndex = 36;
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.InsertImage(global::SP_Sklad.Properties.Resources.info1, "info1", typeof(global::SP_Sklad.Properties.Resources), 0);
            this.ImageList.Images.SetKeyName(0, "info1");
            this.ImageList.InsertImage(global::SP_Sklad.Properties.Resources.making_notes, "making_notes", typeof(global::SP_Sklad.Properties.Resources), 1);
            this.ImageList.Images.SetKeyName(1, "making_notes");
            this.ImageList.InsertImage(global::SP_Sklad.Properties.Resources.open_16x16, "open_16x16", typeof(global::SP_Sklad.Properties.Resources), 2);
            this.ImageList.Images.SetKeyName(2, "open_16x16");
            // 
            // frmTaraGroupEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 328);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.DirTreeList);
            this.Controls.Add(this.panelControl2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTaraGroupEdit";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSvcGroupEdit";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSvcGroupEdit_FormClosed);
            this.Load += new System.EventHandler(this.frmSvcGroupEdit_Load);
            this.Shown += new System.EventHandler(this.frmSvcGroupEdit_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.styleController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TaraGroupDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DirTreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GrpIdEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeListLookUpEdit1TreeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit10.Properties)).EndInit();
            this.xtraTabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImageList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.StyleController styleController1;
        private System.Windows.Forms.BindingSource TaraGroupDS;
        private DevExpress.XtraTreeList.TreeList DirTreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.SplitterControl splitterControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.TreeListLookUpEdit GrpIdEdit;
        private DevExpress.XtraTreeList.TreeList treeListLookUpEdit1TreeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.CheckEdit checkEdit4;
        private DevExpress.XtraEditors.CheckEdit checkEdit3;
        private DevExpress.XtraEditors.TextEdit textEdit10;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage4;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraEditors.MemoEdit textEdit1;
        private DevExpress.Utils.ImageCollection ImageList;
    }
}