﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmInParty : Form
    {
        public frmInParty(List<GetPosIn_Result> pos_in)
        {
            InitializeComponent();

            WaybillDetOutGridControl.DataSource = pos_in;
        }

        private void frmInParty_Load(object sender, EventArgs e)
        {
            WaybillDetOutGridView.ExpandAllGroups();
        }

        private void WaybillDetOutGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var dr = WaybillDetOutGridView.GetRow(e.RowHandle) as GetPosIn_Result;
            if (dr == null)
            {
                return;
            }

            if (e.Column.FieldName == "GetAll")
            {
                if (dr.GetAll == 1) dr.Amount = dr.CurRemain;
                else dr.Amount = 0;
            }

            if (e.Column.FieldName == "Amount")
            {
                dr.GetAll = 0;
            }

            WaybillDetOutGridView.RefreshRow(e.RowHandle);
        }
    }
}