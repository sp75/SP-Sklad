using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Map.Native;
using SP_Sklad.Common;
using SP_Sklad.SkladData;
using SP_Sklad.ViewsForm;
using SP_Sklad.WBDetForm;
using EntityState = System.Data.Entity.EntityState;


namespace SP_Sklad.WBForm
{
    public partial class frmBankStatements : DevExpress.XtraEditors.XtraForm
    {
        public BaseEntities _db { get; set; }
        public Guid? _doc_id { get; set; }
        private BankStatements bs { get; set; }
        public bool is_new_record { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        private v_BankStatementsDet bs_det_row => WaybillDetInGridView.GetFocusedRow() as v_BankStatementsDet;
    
        public frmBankStatements(Guid? doc_id = null)
        {
            is_new_record = false;
            _doc_id = doc_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();

        }

        private void frmProductionPlans_Load(object sender, EventArgs e)
        {
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            AccountEdit.Properties.DataSource = _db.EnterpriseAccount.Where(w => w.KaId == UserSession.EnterpriseId).ToList();

            ChargeTypesEdit.Properties.DataSource = DBHelper.ChargeTypes;

            if (_doc_id == null)
            {
                is_new_record = true;

                bs = _db.BankStatements.Add(new BankStatements
                {
                    Id = Guid.NewGuid(),
                    Checked = 0,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = "",
                    PersonId = DBHelper.CurrentUser.KaId,
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = DBHelper.Enterprise.KaId
                });

                _db.SaveChanges();
            }
            else
            {
                bs = _db.BankStatements.FirstOrDefault(f => f.Id == _doc_id );
            }

            if (bs != null)
            {
                _doc_id = bs.Id;

                bs.SessionId =  (Guid?)UserSession.SessionId;
                bs.UpdatedBy = UserSession.UserId;
                bs.UpdatedAt = DateTime.Now;
                _db.SaveChanges();

                if (is_new_record)
                {
                    bs.Num = new BaseEntities().GetDocNum("bank_statements").FirstOrDefault();
                }

                BankStatementsBS.DataSource = bs;
            }

            RefreshDet();
        }

        private void GetOk()
        {
            //OkButton.Enabled = BankProvidingComboBox.EditValue != DBNull.Value;
            //barSubItem1.Enabled =  BankProvidingComboBox.EditValue != DBNull.Value;
            EditMaterialBtn.Enabled = WaybillDetInGridView.RowCount > 0;
            DelMaterialBtn.Enabled = WaybillDetInGridView.RowCount > 0;
        }

        private void RefreshDet()
        {

            var list = _db.v_BankStatementsDet.Where(w => w.BankStatementId == _doc_id).OrderBy(o => o.TransactionDate).ToList();

            int top_row = WaybillDetInGridView.TopRowIndex;
            BankStatementsDetBS.DataSource = list;
            WaybillDetInGridView.TopRowIndex = top_row;
            //   entityServerModeSource1.QueryableSource = _db.v_BankStatementsDet.Where(w=> w.BankStatementId == _doc_id).OrderBy(o=> o.TransactionDate);

            GetOk();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ofdDBF.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in ofdDBF.FileNames)
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    {
                        DbfLoaderCore loader = new DbfLoaderCore(fs);

                        foreach (DbfRecord dbf_row in loader.Records)
                        {
                            var row = dbf_row.Attributes.ToDictionary(x => x.Name, x => x.Value);

                            _db.BankStatementsDet.Add(new BankStatementsDet
                            {
                                Id = Guid.NewGuid(),
                                BankStatementId = bs.Id,
                                EGRPOU = row["OKPO_A"].ToString(),
                                Account = row["COUNT_A"].ToString(),
                                FOP = row["NAME_A"].ToString(),
                                MFO = row["MFO_A"].ToString(),
                                Reason = row["N_P"].ToString(),
                                PaySum = Convert.ToDecimal(row["SUMMA"]),
                                TransactionDate = Convert.ToDateTime(row["DATE"] + " " + row["TIME"]),
                                Checked = 0,
                                BankProvidingId = 2
                            });
                        }
                        _db.SaveChanges();

                        RefreshDet();
                    }
                }
            }
        }

        private void EditMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = WaybillDetInGridView.GetFocusedRow() as v_ProductionPlanDet;
       //     new frmProductionPlanDet(_db, row.Id, bs).ShowDialog();

            RefreshDet();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            bs.UpdatedAt = DateTime.Now;

            var BankStatements = _db.BankStatements.Find(_doc_id);
            if (BankStatements != null && BankStatements.SessionId != UserSession.SessionId)
            {
                throw new Exception("Не можливо зберегти документ, тільки перегляд.");
            }

            _db.SaveChanges();


            if (TurnDocCheckBox.Checked)
            {
                ExecuteDocument();
            }

            is_new_record = false;

            Close();
        }

        private void ExecuteDocument()
        {
            if (TurnDocCheckBox.Checked)
            {
                var list = _db.BankStatementsDet.Where(w => w.BankStatementId == bs.Id && w.Checked == 0).ToList();

                foreach (var item in list)
                {
                    var doc_type = item.PaySum > 0 ? 1 : -1;
                    string doc_setting_name = doc_type == -1 ? "pay_doc_out" : doc_type == 1 ? "pay_doc_in" : "pay_doc";
                    List<int> ka_list = item.KaId.HasValue ? new List<int> { item.KaId.Value } : _db.Kagent.Where(w => w.OKPO == item.EGRPOU).Select(s => s.KaId).ToList();

                    foreach (var ka in ka_list)
                    {
                        var _pd = _db.PayDoc.Add(new PayDoc()
                        {
                            Id = Guid.NewGuid(),
                            DocType = doc_type,
                            DocNum = new BaseEntities().GetDocNum(doc_setting_name).FirstOrDefault(), //Номер документа
                            Total = (item.PaySum.Value < 0 ? item.PaySum.Value * -1 : item.PaySum.Value) / ka_list.Count(),
                            Checked = 1,
                            OnDate = item.TransactionDate.Value,
                            WithNDS = 1,  // З НДС
                            PTypeId = 2,  // Вид оплати
                            CashId = null,  // Каса 
                            AccId = (int?)AccountEdit.EditValue, // Acount
                            CTypeId = (int)ChargeTypesEdit.EditValue,
                            CurrId = 2,  //Валюта по умолчанию
                            OnValue = 1,  //Курс валюти
                            MPersonId = bs.PersonId,
                            KaId = ka,
                            UpdatedBy = DBHelper.CurrentUser.UserId,
                            EntId = DBHelper.Enterprise.KaId,
                            ReportingDate = item.TransactionDate.Value,
                        });

                        _db.SaveChanges();

                        _db.SetDocRel(bs.Id, _pd.Id);
                    }

                    item.Checked = 1;
                }
            }

        }

        private void frmProductionPlans_FormClosed(object sender, FormClosedEventArgs e)
        {
            _db.UndoAllChanges();

            bs.SessionId = (bs.SessionId == UserSession.SessionId ? null : bs.SessionId);
            bs.UpdatedBy = UserSession.UserId;
            bs.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            if (is_new_record)
            {
                _db.DeleteWhere<ProductionPlans>(w => w.Id == _doc_id);
            }

            _db.Dispose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DelMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = WaybillDetInGridView.GetFocusedRow() as v_ProductionPlanDet;
            if (row != null)
            {
                var det = _db.ProductionPlanDet.Find(row.Id);
                if (det != null)
                {
                    _db.ProductionPlanDet.Remove(det);
                }
                _db.SaveChanges();
                WaybillDetInGridView.DeleteSelectedRows();
            }
            GetOk();
        }

  
        private void WaybillDetInGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
          /*  var row = WaybillDetInGridView.GetFocusedRow() as v_ProductionPlanDet;
            var wbd = _db.ProductionPlanDet.Find(row.Id);
            if (e.Column.FieldName == "Amount")
            {

                wbd.Amount = Convert.ToDecimal(e.Value);
                var real_amount = wbd.Amount.Value - wbd.Remain.Value;
                var tmp_amount = (real_amount / (row.ResipeOut == 0 ? 100.00m : row.ResipeOut)) * 100; // real_amount + (real_amount - (real_amount * row.ResipeOut / 100));
                wbd.Total = Math.Ceiling(tmp_amount / row.RecipeAmount) * row.RecipeAmount;
            }

            if (e.Column.FieldName == "Total")
            {
                wbd.Total = Convert.ToDecimal(e.Value);
            }

            _db.SaveChanges();
            RefreshDet();*/
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void WaybillDetInGridView_DoubleClick(object sender, EventArgs e)
        {
            if (IHelper.isRowDublClick(sender)) EditMaterialBtn.PerformClick();
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          /*  if (wbd_row != null)
            {
                IHelper.ShowMatRSV(wbd_row.MatId, _db);
            }*/
        }


        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void frmProductionPlans_Shown(object sender, EventArgs e)
        {
            WaybillDetInGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
        }

        private void OnDateDBEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                bs.OnDate = DBHelper.ServerDateTime();
                OnDateDBEdit.DateTime = bs.OnDate;
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _db.DeleteWhere<BankStatementsDet>(w => w.BankStatementId == bs.Id);
            RefreshDet();
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            using (var frm = new frmKagents(1, bs_det_row.EGRPOU))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var d = _db.BankStatementsDet.Find(bs_det_row.Id);

                    d.KaId = frm.focused_row.KaId;

                    _db.SaveChanges();
                    RefreshDet();
                }
            }

         //   d.KaId = (int)IHelper.ShowDirectList(null, 1);

        }
    }
}
