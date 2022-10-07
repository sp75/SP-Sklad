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

        private v_BankStatementsDet bs_det_row => BankStatementsDetGridView.GetFocusedRow() as v_BankStatementsDet;
    
        public frmBankStatements(Guid? doc_id = null)
        {
            is_new_record = false;
            _doc_id = doc_id;
            _db = new BaseEntities();
            user_settings = new UserSettingsRepository(DBHelper.CurrentUser.UserId, _db);

            InitializeComponent();

        }
  
        private void GetOk()
        {
         //   OkButton.Enabled =  AccountEdit.EditValue != DBNull.Value;
            //barSubItem1.Enabled =  BankProvidingComboBox.EditValue != DBNull.Value;
            EditMaterialBtn.Enabled = BankStatementsDetBS.Count > 0;
            DelMaterialBtn.Enabled = BankStatementsDetBS.Count > 0;
        }

        private void RefreshDet()
        {
           // using (var t_db = new BaseEntities())
          //  {
                var list = _db.v_BankStatementsDet.AsNoTracking().Where(w => w.BankStatementId == _doc_id).OrderBy(o => o.TransactionDate).ToList();

                int top_row = BankStatementsDetGridView.TopRowIndex;
                BankStatementsDetBS.DataSource = list;
                BankStatementsDetGridView.TopRowIndex = top_row;
        //    }

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
                                PayerEGRPOU = row["OKPO_A"].ToString(),
                                PayerAccount = row["COUNT_A"].ToString(),
                                PayerName = row["NAME_A"].ToString(),
                                PayerBankMFO = row["MFO_A"].ToString(),
                                Reason = row["N_P"].ToString(),
                                PaySum = Convert.ToDecimal(row["SUMMA"]),
                                TransactionDate = Convert.ToDateTime(row["DATE"] + " " + row["TIME"]),
                                Checked = 0,
                                BankProvidingId = 2,
                                DocNum = row["N_D"].ToString(),
                                RecipientAccount = row["COUNT_B"].ToString(),
                                RecipientBankMFO = row["MFO_B"].ToString(),
                                RecipientEGRPOU = row["OKPO_B"].ToString(),
                                RecipientName = row["NAME_B"].ToString()
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
            var row = BankStatementsDetGridView.GetFocusedRow() as v_ProductionPlanDet;
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
                if (!ExecuteDocument())
                {
                    TurnDocCheckBox.Checked = false;
                    _db.SaveChanges();

                    RefreshDet();

                    MessageBox.Show("Не всі позиції рознесено !");

                    return;
                }
            }

            is_new_record = false;

            Close();
        }

        private bool ExecuteDocument()
        {
            if (TurnDocCheckBox.Checked)
            {
                var list = _db.BankStatementsDet.Where(w => w.BankStatementId == bs.Id && w.Checked == 0 && w.CTypeId != null).ToList();

                foreach (var item in list)
                {
                    var doc_type = item.PaySum > 0 ? 1 : -1;
                    string doc_setting_name = doc_type == -1 ? "pay_doc_out" : doc_type == 1 ? "pay_doc_in" : "pay_doc";
                    List<int> ka_list = item.KaId.HasValue ? new List<int> { item.KaId.Value } : _db.Kagent.Where(w => w.OKPO == item.PayerEGRPOU).Select(s => s.KaId).ToList();
                    var pay_sum = Math.Round(Math.Abs(item.PaySum.Value) / ka_list.Count(), 2, MidpointRounding.AwayFromZero);
                    var pay_sum_dev = Math.Abs(item.PaySum.Value) - (pay_sum * ka_list.Count());

                    var payer_account = _db.KAgentAccount.FirstOrDefault(w => w.AccNum == item.PayerAccount && w.Kagent.KType == 3);
                    if(payer_account == null)
                    {
                        continue;
                    }

                    var recipient_bank = _db.Banks.FirstOrDefault(b => b.MFO == item.RecipientBankMFO);
                    if(recipient_bank == null)
                    {
                        continue;
                    }


                    var recipient_account = _db.KAgentAccount.FirstOrDefault(w => w.AccNum == item.RecipientAccount);

                    if (recipient_account == null)
                    {
                        var ka = _db.Kagent.FirstOrDefault(w => w.OKPO == item.RecipientEGRPOU);
                        if (ka == null)
                        {

                            var new_ka = _db.Kagent.Add(new Kagent
                            {
                                Name = item.RecipientName,
                                OKPO = item.RecipientEGRPOU,
                                KaKind = 6,
                                Id = Guid.NewGuid(),
                                KType = 0,
                                KAgentAccount = new List<KAgentAccount>() { new KAgentAccount { AccNum = item.RecipientAccount, Banks = recipient_bank, TypeId = 1 } }
                            });
                        }
                        else
                        {
                            _db.KAgentAccount.Add(new KAgentAccount
                            {
                                AccNum = item.RecipientAccount,
                                Banks = recipient_bank,
                                KAId = ka.KaId,
                                TypeId = 1
                            });
                        }

                        _db.SaveChanges();

                        recipient_account = _db.KAgentAccount.FirstOrDefault(w => w.AccNum == item.RecipientAccount);
                    }


                    try
                    {
                        for (int i = 0; i < ka_list.Count(); i++)
                        {
                            var _pd = _db.PayDoc.Add(new PayDoc()
                            {
                                Id = Guid.NewGuid(),
                                DocType = doc_type,
                                DocNum = new BaseEntities().GetDocNum(doc_setting_name).FirstOrDefault(), //Номер документа
                                Total = i == 0 ? pay_sum + pay_sum_dev : pay_sum,
                                Checked = 1,
                                OnDate = item.TransactionDate.Value,
                                WithNDS = 1,  // З НДС
                                PTypeId = 2,  // Вид оплати
                                CashId = null,  // Каса 
                                AccId = payer_account.AccId, // Acount
                                CTypeId = item.CTypeId.Value,
                                CurrId = 2,  //Валюта по умолчанию
                                OnValue = 1,  //Курс валюти
                                MPersonId = bs.PersonId,
                                KaId = ka_list[i],
                                UpdatedBy = DBHelper.CurrentUser.UserId,
                                EntId = payer_account.KAId,
                                ReportingDate = item.TransactionDate.Value,
                                KaAccId = recipient_account.AccId
                            });

                            _db.SaveChanges();

                            _db.SetDocRel(bs.Id, _pd.Id);
                        }

                        item.Checked = ka_list.Any() ? 1 : 0;
                    }
                    catch
                    {
                        item.Checked = 0;
                    }
                }
            }

            _db.SaveChanges();

            return !_db.BankStatementsDet.AsNoTracking().Any(a => a.Checked == 0);

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
           
            if (bs_det_row != null)
            {
                var det = _db.BankStatementsDet.Find(bs_det_row.Id);
                if (det != null)
                {
                    _db.BankStatementsDet.Remove(det);
                }
                _db.SaveChanges();
                BankStatementsDetGridView.DeleteSelectedRows();
            }
            GetOk();
        }


        private void WaybillDetInGridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            var wbd = _db.BankStatementsDet.Find(bs_det_row.Id);

            if (e.Column.FieldName == "CTypeId")
            {
                wbd.CTypeId = Convert.ToInt32(e.Value);
            }

            _db.SaveChanges();
            RefreshDet();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }


        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          /*  if (wbd_row != null)
            {
                IHelper.ShowMatRSV(wbd_row.MatId, _db);
            }*/
        }


        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void frmProductionPlans_Shown(object sender, EventArgs e)
        {
            BankStatementsDetGridView.Appearance.Row.Font = new Font(user_settings.GridFontName, (float)user_settings.GridFontSize);
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
            if (e.Button.Index == 0)
            {

                using (var frm = new frmKagents(1, bs_det_row.PayerEGRPOU))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        var d = _db.BankStatementsDet.Find(bs_det_row.Id);

                        d.KaId = frm.focused_row.KaId;

                        _db.SaveChanges();
                        RefreshDet();
                    }
                }
            }
            if(e.Button.Index == 1)
            {
                var d = _db.BankStatementsDet.Find(bs_det_row.Id);

                d.KaId = null;

                _db.SaveChanges();
                RefreshDet();
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ofdDBF.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in ofdDBF.FileNames)
                {
                    using (FileStream fs = new FileStream(file, FileMode.Open))
                    {
                        fs.Seek(29, SeekOrigin.Begin);
                        fs.WriteByte(201); // '101 - Dos 866, а 201 - Win 1251
                        fs.Position = 0;

                        string EGRPOU = "", FOP = "", Account = "";

                        DbfLoaderCore loader = new DbfLoaderCore(fs, Encoding.GetEncoding(1251));

                        foreach (DbfRecord dbf_row in loader.Records)
                        {
                            var row = dbf_row.Attributes.ToDictionary(x => x.Name, x => x.Value);

                            if (row["FIELD0"].ToString() == "Код Клієнта" && EGRPOU =="")
                            {
                                EGRPOU = row["FIELD1"].ToString();
                            }
                            if (row["FIELD0"].ToString() == "Назва Клієнта"&& FOP=="")
                            {
                                FOP = row["FIELD1"].ToString();
                            }

                            if (row["FIELD0"].ToString() == "Рахунок Клієнта" && Account == "")
                            {
                                Account = row["FIELD1"].ToString().Substring(0,29);
                            }


                            if (int.TryParse(row["FIELD0"].ToString(), out int res))
                            {
                                decimal PaySum = 0;
                                if (!string.IsNullOrEmpty(row["FIELD3"].ToString()))
                                {
                                    PaySum = Convert.ToDecimal(row["FIELD3"]) * -1;
                                }
                                if (!string.IsNullOrEmpty(row["FIELD4"].ToString()))
                                {
                                    PaySum = Convert.ToDecimal(row["FIELD4"]);
                                }

                                _db.BankStatementsDet.Add(new BankStatementsDet
                                {
                                    Id = Guid.NewGuid(),
                                    BankStatementId = bs.Id,
                                    PayerEGRPOU = EGRPOU,
                                    PayerAccount = Account,
                                    PayerName = FOP,
                                    PayerBankMFO = row["FIELD8"].ToString(),
                                    Reason = row["FIELD9"].ToString(),
                                    PaySum = PaySum,
                                    TransactionDate = Convert.ToDateTime(row["FIELD2"].ToString()),
                                    Checked = 0,
                                    BankProvidingId = 1,
                                    DocNum = row["FIELD1"].ToString()
                                });
                            }
                            _db.SaveChanges();
                        }

                        RefreshDet();
                    }
                }
            }
        }

        private void ChargeTypesEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }

        private void frmBankStatements_Load(object sender, EventArgs e)
        {
            PersonComboBox.Properties.DataSource = DBHelper.Persons;
            repositoryItemLookUpEdit1.DataSource = DBHelper.ChargeTypes;
            //   AccountEdit.Properties.DataSource = _db.EnterpriseAccount.Where(w => w.KaId == UserSession.EnterpriseId).ToList();

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
                bs = _db.BankStatements.FirstOrDefault(f => f.Id == _doc_id);
            }

            if (bs != null)
            {
                _doc_id = bs.Id;

                bs.SessionId = (Guid?)UserSession.SessionId;
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

        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void repositoryItemLookUpEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if(e.Button.Index == 1)
            {
                var d = _db.BankStatementsDet.Find(bs_det_row.Id);

                var f = IHelper.ShowDirectList((object)d.CTypeId, 6);
                d.CTypeId = f == null ? null : (int?)f;

                _db.SaveChanges();

                RefreshDet();
            }
        }
    }
}
