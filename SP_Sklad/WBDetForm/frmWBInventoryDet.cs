﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.EditForm;
using SP_Sklad.SkladData;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWBInventoryDet : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }

        public frmWBInventoryDet(BaseEntities db, int? PosId, WaybillList wb)
        {
            InitializeComponent();
            _db = db;
            _PosId = PosId;
            _wb = wb;

            WHComboBox.Properties.DataSource = DBHelper.WhList;
        }

        private void frmWBInventoryDet_Load(object sender, EventArgs e)
        {
            NdsEdit.Enabled = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db).AccessEditPrice;
            _wbd = _db.WaybillDet.Find(_PosId);

            if (_wbd == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    Amount = 0,
                    Num = _wb.WaybillDet.Count() + 1,
                    OnDate = _wb.OnDate,
                    CurrId = _wb.CurrId,
                    OnValue = 1,
                    WId = _wb.WaybillMove.SourceWid
                };
            }
            
            WaybillDetBS.DataSource = _wbd;

            MatComboBox.Properties.DataSource = _db.WhMatGet(0, _wbd.WId, 0, DBHelper.ServerDateTime(), 1, "*", 1, "", DBHelper.CurrentUser.UserId, 0).ToList();

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (MatComboBox.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value && DiscountEdit.EditValue != DBNull.Value && NdsEdit.EditValue != DBNull.Value);

            OkButton.Enabled = recult;
         
            SummOblicEdit.EditValue = (AmountEdit.EditValue != DBNull.Value ? Convert.ToDecimal(AmountEdit.EditValue) : 0) *  (PriceEdit.EditValue != DBNull.Value ? Convert.ToDecimal(PriceEdit.EditValue) : 0) ;
            SummFactEdit.EditValue = DiscountEdit.Value * NdsEdit.Value;
            SummAllEdit.EditValue = (decimal)SummFactEdit.EditValue - (decimal)SummOblicEdit.EditValue;

            return recult;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Detached)
            {
                _db.WaybillDet.Add(_wbd);
            }

            _db.SaveChanges();
        }

        private void MatComboBox_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            AmountAllEdit.EditValue = DiscountEdit.Value - (AmountEdit.EditValue != DBNull.Value ? Convert.ToDecimal(AmountEdit.EditValue) : 0);
            GetOk();
        }

        private void calcEdit2_EditValueChanged(object sender, EventArgs e)
        {
            SummEdit.EditValue = NdsEdit.Value - (PriceEdit.EditValue != DBNull.Value ? Convert.ToDecimal(PriceEdit.EditValue) : 0);
            GetOk();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            

        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
           if(MatComboBox.ContainsFocus)
           {
               SetValue();
           }
        }

        private void SetValue()
        {
            var item = MatComboBox.GetSelectedDataRow() as WhMatGet_Result;

            if (item == null)
            {
                return;
            }

            _wbd.MatId = item.MatId;
            _wbd.Amount = item.Remain ?? 0;
            _wbd.Price = item.AvgPrice ?? 0;
            _wbd.Discount = item.Remain ?? 0;
            _wbd.Nds = item.AvgPrice ?? 0;
            _wbd.BasePrice = item.AvgPrice ?? 0;

            WaybillDetBS.DataSource = _wbd;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(_wbd.MatId);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(_wbd.MatId, _db);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial(_wbd.MatId);
        }

        private void MatComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                using (var f = new frmRemainsWhView() { WhName = WHComboBox.Text })
                {
                    f.ucWhMat.OnDateEdit.Enabled = false;
                    f.ucWhMat.WhCheckedComboBox.Enabled = false;
                    f.ucWhMat.by_grp = false;
                    f.ucWhMat.focused_tree_node_num = _wbd.WId.Value;
                    f.ucWhMat.GrpNameGridColumn.GroupIndex = 0;
                    f.ucWhMat.isDirectList = true;

                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        _wbd.MatId = f.ucWhMat.focused_wh_mat.MatId;
                        MatComboBox.EditValue = _wbd.MatId;
                        SetValue();
                    }
                }
            }
        }
    }
}
