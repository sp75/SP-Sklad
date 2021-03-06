﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SP_Sklad.SkladData;

namespace SP_Sklad.EditForm
{
    public partial class frmChargeTypeEdit : XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _CTypeId { get; set; }
        private ChargeType ct { get; set; }

        public frmChargeTypeEdit(int? CTypeId = null)
        {
            InitializeComponent();

            _CTypeId = CTypeId;
            _db = DB.SkladBase();
        }

        private void frmChargeTypeEdit_Load(object sender, EventArgs e)
        {
            if (_CTypeId == null)
            {
                ct = _db.ChargeType.Add(new ChargeType
                {
                    Deleted = 0,
                    Name = ""
                });

                Text = "Додати нову статтю витрат:";
            }
            else
            {
                ct = _db.ChargeType.Find(_CTypeId);

                Text = "Властвості статті витрат:";
            }

            ChargeTypeBS.DataSource = ct;

            DefCheckBox.Enabled = !DefCheckBox.Checked;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }
    }
}
