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
using System.Data.Entity;
using SkladEngine.DBFunction;
using SkladEngine.DBFunction.Models;

namespace SP_Sklad.WBDetForm
{


    public partial class frmWBMoveDet : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }
        private List<GetPosIn_Result> pos_in { get; set; }
        private GetMaterialsOnWh_Result mat_remain { get; set; }
        private int _ka_id { get; set; }
        private List<GetMaterialsOnWh_Result> _materials_on_wh { get; set; }

        public frmWBMoveDet(BaseEntities db, int? PosId, WaybillList wb)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;
            _ka_id = 0;
        }
/*
        public class MaterialsByWh
        {
            public int MatId { get; set; }
            public int WId { get; set; }
            public string Name { get; set; }
            public string MsrName { get; set; }
            public decimal Remain { get; set; }
            public decimal Rsv { get; set; }
        }

        private List<MaterialsByWh> GetMaterialsOnWh(int wh_id)
        {
            return _db.Database.SqlQuery<MaterialsByWh>(@"SELECT 
         m.MatId,
         pr.WId,
         m.Name,
         ms.ShortName as MsrName,
         sum( pr.remain) Remain,
         sum( pr.Rsv ) Rsv 
		 
  FROM v_PosRemains pr
  inner join Materials m on m.MatId =pr.MatId
  inner join Measures ms on ms.MId = m.MId
  where pr.WId = {0} and pr.Remain > 0
  group by m.MatId, m.Name , ms.ShortName ,pr.WId", wh_id).ToList();
        }
        */
        private void frmWBMoveDet_Load(object sender, EventArgs e)
        {
            WHComboBox.Properties.DataSource = DBHelper.WhList;

            if (_PosId == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    Amount = 0,
                    OnValue = _wb.OnValue,
                    WId = _wb.WaybillMove != null ? (int?)_wb.WaybillMove.SourceWid : null,
                    Discount = 0,
                    Nds = _wb.Nds,
                    Num = _wb.WaybillDet.Count() + 1
                };
            }
            else
            {
                _wbd = _db.WaybillDet.Find(_PosId);
            }

            int wh_id = _wb.WaybillMove != null ? _wb.WaybillMove.SourceWid : 0;

            _materials_on_wh = new MaterialRemain(UserSession.UserId).GetMaterialsOnWh(wh_id); 

            MatComboBox.Properties.DataSource = _materials_on_wh; 


            if (_wbd != null)
            {
                WaybillDetBS.DataSource = _wbd;

                if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Unchanged)
                {
                    var w_mat_turn = _db.WMatTurn.AsNoTracking().Where(w => w.SourceId == _wbd.PosId).ToList();

                    _db.DeleteWhere<WMatTurn>(w => w.SourceId == _wbd.PosId);

                    GetContent();
                    GetPos();
                    foreach (var item in w_mat_turn)
                    {
                        if (pos_in.Any(a => a.PosId == item.PosId))
                        {
                            pos_in.FirstOrDefault(a => a.PosId == item.PosId).Amount = item.Amount;
                        }
                    }

                    if (w_mat_turn.Count == 0)
                    {
                        SetAmount();
                    }
                }
            }

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (MatComboBox.EditValue != DBNull.Value && Convert.ToInt32(MatComboBox.EditValue) > 0 && WHComboBox.EditValue != DBNull.Value && AmountEdit.EditValue != DBNull.Value);

            RSVCheckBox.Checked = (recult  && mat_remain != null  && AmountEdit.Value <= mat_remain.CurRemain /*&& pos_in.Sum(s => s.FullRemain) >= AmountEdit.Value*/);

            if (RSVCheckBox.Checked)
            {
                if (pos_in != null)
                {
                    foreach (var item in pos_in)
                    {
                        if (item.FullRemain < item.Amount)
                        {
                            RSVCheckBox.Checked = false;
                            break;
                        }
                    }
                }
            }

            OkButton.Enabled = recult;

            return recult;
        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (GetMaterialsOnWh_Result)MatComboBox.GetSelectedDataRow();
            if (row == null)
            {
                return;
            }

            if (MatComboBox.ContainsFocus)
            {
                _wbd.MatId = row.MatId;
                GetContent();
                pos_in = null;
            //    GetPos();
            //  SetAmount();
            }

            labelControl24.Text = row.MsrName;
        }

        private void GetContent()
        {
            if (_wbd.WId == null || _wbd.MatId == 0)
            {
                return;
            }
  
            var row = (GetMaterialsOnWh_Result)MatComboBox.GetSelectedDataRow();

            mat_remain = _materials_on_wh.FirstOrDefault(w => w.MatId == _wbd.MatId);

            if (mat_remain != null)
            {
                RemainWHEdit.EditValue = row.CurRemain;
                RsvEdit.EditValue = row.Rsv;
                CurRemainEdit.EditValue = mat_remain.Remain; // не вірно показує томущо тільки по складу вибираем
            }
        }

        private void GetPos()
        {
            using (var db = new BaseEntities())
            {
                pos_in = db.GetPosIn(_wb.OnDate, _wbd.MatId, _wbd.WId, _ka_id, DBHelper.CurrentUser.UserId).OrderBy(o => o.OnDate).ToList();
            }
        }

        private void SetAmount()
        {
            if (pos_in == null || mat_remain == null)
            {
                return;
            }

            decimal? sum_amount = pos_in.Sum(s => s.Amount);
            decimal? sum_full_remain = pos_in.Sum(s => s.FullRemain);

            if (pos_in.Count > 0 && AmountEdit.Value <= mat_remain.CurRemain && sum_amount != AmountEdit.Value)
            {
                sum_amount = AmountEdit.Value;
                bool stop = false;
                foreach (var item in pos_in)
                {
                    decimal? remain = item.FullRemain;
                    if (!stop)
                    {
                        if (remain >= sum_amount)
                        {
                            item.Amount = sum_amount;
                            sum_amount -= remain;
                            stop = true;
                        }
                        else
                        {
                            item.Amount = remain;
                            sum_amount -= remain;
                        }
                    }
                    else item.Amount = 0;
                }
                RSVCheckBox.Checked = (sum_amount <= 0);

            }
            else
            {
                RSVCheckBox.Checked = false;
            }

            if (AmountEdit.Value <= sum_full_remain)
            {
                RSVCheckBox.Checked = false;
            }

            GetOk();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (pos_in == null)
            {
                GetPos();
                SetAmount();
            }

            if (RSVCheckBox.Checked && !_db.WMatTurn.Any(w => w.SourceId == _wbd.PosId) && _db.UserAccessWh.Any(a => a.UserId == DBHelper.CurrentUser.UserId && a.WId == _wbd.WId && a.UseReceived))
            {
                using (var d = new BaseEntities())
                {
                    d.DeleteWhere<WaybillDet>(w => w.PosId == _wbd.PosId);

                    foreach (var item in pos_in.Where(w => w.Amount > 0))
                    {
                        var wbd = d.WaybillDet.Add(new WaybillDet()
                        {
                            WbillId = _wb.WbillId,
                            Price = item.Price * item.OnValue,
                            BasePrice = item.BasePrice * item.OnValue,
                            Nds = item.Nds,
                            CurrId = _wb.CurrId,
                            OnDate = _wb.OnDate,
                            WId = item.WId,
                            Num = _wbd.Num,
                            Amount = item.Amount.Value,
                            MatId = item.MatId,
                            OnValue = _wb.OnValue

                        });

                        wbd.WMatTurn1.Add(new WMatTurn
                        {
                            PosId = item.PosId,
                            WId = item.WId,
                            MatId = item.MatId,
                            OnDate = _wb.OnDate,
                            TurnType = 2,
                            Amount = Convert.ToDecimal(item.Amount),
                            //  SourceId = wbd.PosId
                        });

                        d.SaveChanges();

                    }
                }
            }

            Close();
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            SetAmount();
            GetOk();
        }

        private void RSVCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RSVCheckBox.Checked && RSVCheckBox.ContainsFocus)
            {
                GetOk();
            }
        }

        private void frmWBMoveDet_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Modified)
            {
                _db.Entry<WaybillDet>(_wbd).Reload();
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
           
        }

        private void MatEditBtn_Click(object sender, EventArgs e)
        {
           
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

                        GetContent();
                        GetPos();
                        SetAmount();
                    }
                }
            }
        }

        private void AmountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                if (pos_in == null)
                {
                    GetPos();
                    SetAmount();
                }

                using (var pos = new frmInParty(pos_in))
                {
                    pos.Text = "Прибуткові партії: " + MatComboBox.Text;
                    pos.ShowDialog();
                    _wbd.Amount = pos_in.Sum(s => s.Amount).Value;
                    AmountEdit.Value = _wbd.Amount;
                }

                GetOk();
            }
        }
    }
}
