namespace SP_Sklad.EditForm
{
    partial class frmRemainsWhView
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
            this.BottomPanel = new DevExpress.XtraEditors.PanelControl();
            this.OkButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.ucWhMat = new SP_Sklad.UserControls.Warehouse.ucWhMat();
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).BeginInit();
            this.BottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.OkButton);
            this.BottomPanel.Controls.Add(this.simpleButton1);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 608);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(1484, 52);
            this.BottomPanel.TabIndex = 16;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(1262, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Вибрати";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton1.Location = new System.Drawing.Point(1372, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(100, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Відмінити";
            // 
            // ucWhMat
            // 
            this.ucWhMat.by_grp = false;
            this.ucWhMat.custom_mat_list = null;
            this.ucWhMat.disc_card = null;
            this.ucWhMat.display_child_groups = false;
            this.ucWhMat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucWhMat.focused_tree_node_num = 0;
            this.ucWhMat.isDirectList = false;
            this.ucWhMat.isMatList = false;
            this.ucWhMat.Location = new System.Drawing.Point(0, 0);
            this.ucWhMat.Name = "ucWhMat";
            this.ucWhMat.resut = null;
            this.ucWhMat.set_tree_node = null;
            this.ucWhMat.Size = new System.Drawing.Size(1484, 608);
            this.ucWhMat.TabIndex = 17;
            this.ucWhMat.wb = null;
            this.ucWhMat.wh_mat_list = null;
            this.ucWhMat.WhMatGridViewDoubleClick += new System.EventHandler(this.ucWhMat_WhMatGridViewDoubleClick);
            // 
            // frmRemainsWhView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 660);
            this.Controls.Add(this.ucWhMat);
            this.Controls.Add(this.BottomPanel);
            this.Name = "frmRemainsWhView";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Залишки на складах";
            this.Load += new System.EventHandler(this.frmWhCatalog_Load);
            this.Shown += new System.EventHandler(this.frmWhCatalog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.BottomPanel)).EndInit();
            this.BottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl BottomPanel;
        public DevExpress.XtraEditors.SimpleButton OkButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public UserControls.Warehouse.ucWhMat ucWhMat;
    }
}