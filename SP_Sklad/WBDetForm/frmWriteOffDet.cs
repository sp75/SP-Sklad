﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SkladEngine.DBFunction;
using SkladEngine.DBFunction.Models;
using SP_Sklad.Common;
using SP_Sklad.EditForm;
using SP_Sklad.SkladData;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWriteOffDet : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }
        private List<GetPosIn_Result> pos_in { get; set; }

        private List<GetMatRemainByWh_Result> mat_remain { get; set; }
        private decimal? CurRemainInWh
        {
            get
            {
                return mat_remain.FirstOrDefault(w => w.WId == _wbd.WId)?.CurRemain;
            }
        }

        public decimal? amount { get; set; }
        public int? mat_id { get; set; }
        public int _ka_id { get; set; }

        public frmWriteOffDet(BaseEntities db, int? PosId, WaybillList wb)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;
            _ka_id = 0;
        }

        private void frmWriteOffDet_Load(object sender, EventArgs e)
        {
            WHComboBox.Properties.DataSource = DBHelper.WhList;
            MatComboBox.Properties.DataSource = _db.v_Materials.AsNoTracking().Where(w => w.Archived == 0).ToList();

            if (_wb.WType == -5 || _wb.WType == -22)
            {
                WHComboBox.Enabled = false;
            }
            if (_wb.WType == -22)
            {
                MatComboBox.Enabled = false;
            }

            if (_PosId == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    Num = _wb.WaybillDet.Count() + 1,
                    Amount = amount != null ? (decimal)amount : 0,
                    OnValue = _wb.OnValue,
                    WId = _wb.WaybillMove != null ? _wb.WaybillMove.SourceWid : _wb.WayBillMake != null ? _wb.WayBillMake.SourceWId : DBHelper.WhList.FirstOrDefault(w => w.Def == 1).WId,
                    Nds = _wb.Nds,
                    CurrId = _wb.CurrId,
                    OnDate = _wb.OnDate,
                    MatId = mat_id != null ? (int)mat_id : 0
                };
            }
            else
            {
                _wbd = _db.WaybillDet.Find(_PosId);
            }

            if (_wbd != null)
            {
                PriceEdit.DataBindings.Add(new Binding("EditValue", _wbd, "Price", true, DataSourceUpdateMode.OnValidation));
                MatComboBox.DataBindings.Add(new Binding("EditValue", _wbd, "MatId"));
                WHComboBox.DataBindings.Add(new Binding("EditValue", _wbd, "WId", true, DataSourceUpdateMode.OnPropertyChanged));
                AmountEdit.DataBindings.Add(new Binding("EditValue", _wbd, "Amount"));

                var w_mat_turn = _db.WMatTurn.AsNoTracking().Where(w => w.SourceId == _wbd.PosId).ToList();
                if (w_mat_turn.Count > 0)
                {
                    _db.DeleteWhere<WMatTurn>(w => w.SourceId == _wbd.PosId);

                    GetContent();

                    foreach (var item in w_mat_turn)
                    {
                        var pos = pos_in.FirstOrDefault(a => a.PosId == item.PosId);
                        if (pos != null)
                        {
                            pos.Amount = item.Amount;

                            if (item.Amount == pos.FullRemain)
                            {
                                pos.GetAll = 1;
                            }
                        }
                    }
                }
                else
                {
                    GetContent();
                    SetAmount();
                }
            }

            GetOk();
        }

        bool GetOk()
        {
            bool recult = ((int)MatComboBox.EditValue > 0 && WHComboBox.EditValue != DBNull.Value && AmountEdit.Value > 0);

            OkButton.Enabled = recult;

            RSVCheckBox.Checked = (OkButton.Enabled && pos_in != null && mat_remain != null && pos_in.Count > 0 && AmountEdit.Value <= CurRemainInWh);
            if (RSVCheckBox.Checked)
            {
                foreach (var item in pos_in)
                {
                    if (item.CurRemain < item.Amount)
                    {
                        RSVCheckBox.Checked = false;
                        break;
                    }
                }
            }

            BotAmountEdit.Text = AmountEdit.Text;

            decimal summ = (_wbd.Price ?? 0) * AmountEdit.Value;
            SummAllEdit.EditValue = summ;

            return recult;
        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (v_Materials)MatComboBox.GetSelectedDataRow();
            if (row == null)
            {
                return;
            }

            if (MatComboBox.ContainsFocus)
            {
                _wbd.MatId = row.MatId;
                GetContent();
                SetAmount();
            }

            labelControl24.Text = row.ShortName;
            labelControl27.Text = row.ShortName;

        }

        public void GetContent()
        {
            if (_wbd.WId == null || _wbd.MatId == 0)
            {
                return;
            }

            mat_remain = new MaterialRemain(UserSession.UserId).GetMatRemainByWh(_wbd.MatId).ToList();

            RemainWHEdit.EditValue = CurRemainInWh;
            RsvEdit.EditValue = mat_remain.Sum(s => s.Rsv);
            CurRemainEdit.EditValue = mat_remain.Sum(s => s.CurRemain);


            GetPos();

            if (pos_in.Any())
            {
                _wbd.Price = pos_in.First().Price * pos_in.First().OnValue;
                _wbd.BasePrice = pos_in.First().BasePrice * pos_in.First().OnValue;
                _wbd.Nds = pos_in.First().Nds;
            }
        }

        private void GetPos()
        {
            using (var db = new BaseEntities())
            {
                AmountEdit.Enabled = false;
                pos_in = db.GetPosIn(_wb.OnDate, _wbd.MatId, _wbd.WId, 0, DBHelper.CurrentUser.UserId).AsNoTracking().Where(w => w.CurRemain > 0).AsNoTracking().OrderBy(o => o.OnDate).ToList();
                AmountEdit.Properties.Buttons[1].Enabled = pos_in.Any();

                AmountEdit.Enabled = true;
            }
        }

        private void SetAmount()
        {
            if (pos_in == null || mat_remain == null)
            {
                GetOk();
                return;
            }

            decimal? sum_amount = pos_in.Sum(s => s.Amount);
            decimal? sum_full_remain = pos_in.Sum(s => s.FullRemain);

            if (pos_in.Count > 0 && AmountEdit.Value <= CurRemainInWh && sum_amount != AmountEdit.Value)
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
            else RSVCheckBox.Checked = false;

          
            decimal? selamount = pos_in.Sum(s => s.Amount);
            decimal? sum = pos_in.Sum(s => s.Amount * s.Price * s.OnValue);

            if (selamount > 0)
            {
                _wbd.Price = sum / selamount;
                _wbd.BasePrice = _wbd.Price * _wbd.OnValue;
            }
            else
            {
                _wbd.Price = 0;
                _wbd.BasePrice = 0;
            }
            
            if (AmountEdit.Value <= sum_full_remain) RSVCheckBox.Checked = false;

            GetOk();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (_wb.WType == -20)
            {
                _wbd.Discount = _wbd.Amount;
            }

            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Detached)
            {
                _db.WaybillDet.Add(_wbd);
            }
            _db.SaveChanges();

            if (RSVCheckBox.Checked && !_db.WMatTurn.Any(w => w.SourceId == _wbd.PosId) && _db.UserAccessWh.Any(a => a.UserId == DBHelper.CurrentUser.UserId && a.WId == _wbd.WId && a.UseReceived))
            {
                using (var db = new BaseEntities())
                {
                    foreach (var item in pos_in.Where(w => w.Amount > 0))
                    {
                        db.WMatTurn.Add(new WMatTurn
                        {
                            PosId = item.PosId,
                            WId = item.WId,
                            MatId = item.MatId,
                            OnDate = _wbd.OnDate.Value,
                            TurnType = 2,
                            Amount = Convert.ToDecimal(item.Amount),
                            SourceId = _wbd.PosId
                        });

                    }

                    try
                    {
                        db.SaveChanges();
                        Close();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException exp)
                    {
                        db.UndoAllChanges();
                        var b_exp = exp.GetBaseException();

                        XtraMessageBox.Show((b_exp != null ? b_exp.Message : exp.Message), "Залишки по партіям не актуальні", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (AmountEdit.ContainsFocus)
            {
                SetAmount();
            }
        }

        private void RSVCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RSVCheckBox.Checked && RSVCheckBox.ContainsFocus) GetOk();
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (WHComboBox.ContainsFocus)
            {
                _wbd.WId = (int)WHComboBox.EditValue;
                GetContent();
                SetAmount();
            }
        }

        private void frmWriteOffDet_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Modified)
            {
                _db.Entry<WaybillDet>(_wbd).Reload();
            }
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
                        SetAmount();
                    }
                }
            }
        }

        private void WHComboBox_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);

                GetContent();
                SetAmount();
            }

            if (e.Button.Index == 2)
            {
                var result = IHelper.ShowRemainByWH(MatComboBox.EditValue, WHComboBox.EditValue, 2);
                WHComboBox.EditValue = result.wid;

                GetContent();
                SetAmount();
            }
        }

        private void AmountEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (pos_in == null || !pos_in.Any())
            {
                return;
            }


            if (e.Button.Index == 1)
            {
                using (var pos = new frmInParty(pos_in))
                {
                    pos.Text = "Прибуткові партії: " + MatComboBox.Text;
                    pos.ShowDialog();
                    _wbd.Amount = pos_in.Sum(s => s.Amount).Value;
                    AmountEdit.Value = _wbd.Amount;

                    SetAmount();
                    GetOk();
                }
            }
        }
    }
}
