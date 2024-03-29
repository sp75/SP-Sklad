﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Text.RegularExpressions;
using System.Globalization;

namespace SP.Stuffing
{
    public partial class frmWeightEdit : DevExpress.XtraEditors.XtraForm
    {
        private ComPortHelper com_port { get; set; }

        public frmWeightEdit(String MatName, int weigher_index = 1)
        {
            InitializeComponent();
            AmountEdit.EditValue = 0;
            PriceEdit.EditValue = 0;
            com_port = new ComPortHelper(weigher_index);
            Text = MatName;
        }

        private void AmountEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (com_port.weight > 0)
            {
                timer1.Stop();
                AmountEdit.EditValue = com_port.weight;
            //    textBox1.Text = com_port.received_tmp;
                com_port.Close();
            }
        }

        private void frmMatListEdit_Load(object sender, EventArgs e)
        {
            using (var s = new UserSettingsRepository(UserSession.UserId, new SkladData.BaseEntities()))
            {
                AmountEdit.ReadOnly = !(s.AccessEditWeight == "1");
            }

            GetWeight();
        }

        void GetWeight()
        {
           
            try
            {
                com_port.Open();
            }
            catch { }

            timer1.Start();
        }

        private void frmMatListEdit_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            com_port.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            com_port.Close();

            GetWeight();
        }

        private void frmMatListEdit_Shown(object sender, EventArgs e)
        {
            AmountEdit.Focus();
        }

        private void AmountEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 )
            {
                OkButton.PerformClick();
            }
        }

    }
}