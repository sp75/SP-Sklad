using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
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
            OkButton.Enabled =  AccountEdit.EditValue != DBNull.Value;
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
                            var reason = row["N_P"].ToString();
                            var str_commision = reason.Split(new string[] { "грн." }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(w => w.IndexOf("Ком бан") > 0)?.Replace("Ком бан", "")?.Trim();

                            decimal.TryParse(str_commision, NumberStyles.Currency, CultureInfo.CreateSpecificCulture("en-GB"), out decimal bank_Commission);

                            // var ddd = reason.IndexOf(",") - (reason.IndexOf("cmps:") + 5;
                            var sssss = reason.IndexOf("cmps:") + 5;

                            var POSTerminalCode = reason.IndexOf("cmps:") >= 0 ? reason.Substring(reason.IndexOf("cmps:") + 5, reason.IndexOf(",") - (reason.IndexOf("cmps:") + 5)).Trim() : "";

                            _db.BankStatementsDet.Add(new BankStatementsDet
                            {
                                Id = Guid.NewGuid(),
                                BankStatementId = bs.Id,
                                PayerEGRPOU = row["OKPO_A"].ToString(),
                                PayerAccount = row["COUNT_A"].ToString(),
                                PayerAccountName = row["NAME_A"].ToString(),
                                PayerBankMFO = row["MFO_A"].ToString(),
                                Reason = reason,
                                PaySum = Convert.ToDecimal(row["SUMMA"]),
                                TransactionDate = Convert.ToDateTime(row["DATE"] + " " + row["TIME"]),
                                Checked = 0,
                                BankProvidingId = 2,
                                DocNum = row["N_D"].ToString(),
                                RecipientAccount = row["COUNT_B"].ToString(),
                                RecipientBankMFO = row["MFO_B"].ToString(),
                                RecipientEGRPOU = row["OKPO_B"].ToString(),
                                RecipientAccountName = row["NAME_B"].ToString(),
                                BankCommission = bank_Commission,
                                KaId = !string.IsNullOrEmpty(POSTerminalCode) ? _db.Kagent.FirstOrDefault(w => w.POSTerminalCode == POSTerminalCode)?.KaId : null,
                                PayerBankName = row["BANK_A"].ToString(),
                                RecipientBankName = row["BANK_B"].ToString()
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
                    /*      var doc_type = item.PaySum > 0 ? 1 : -1;
                          string doc_setting_name = doc_type == -1 ? "pay_doc_out" : doc_type == 1 ? "pay_doc_in" : "pay_doc";
                          List<int> ka_list = item.KaId.HasValue ? new List<int> { item.KaId.Value } : _db.Kagent.Where(w => w.OKPO == item.PayerEGRPOU && w.KType != 3).Select(s => s.KaId).ToList();
                          var pay_sum = Math.Round(Math.Abs(item.PaySum.Value) / ka_list.Count(), 2, MidpointRounding.AwayFromZero);
                          var pay_sum_dev = Math.Abs(item.PaySum.Value) - (pay_sum * ka_list.Count());*/

                    var payer_account = _db.KAgentAccount.FirstOrDefault(w => w.AccNum == item.PayerAccount && w.Kagent.KType == 3);
                    if (payer_account == null)
                    {
                        continue;
                    }

                    var recipient_bank = _db.Banks.FirstOrDefault(b => b.MFO == item.RecipientBankMFO);
                    if (recipient_bank == null)
                    {
                        continue;
                    }


                    var recipient_account = _db.KAgentAccount.FirstOrDefault(w => w.AccNum == item.RecipientAccount);

                    if (recipient_account == null)
                    {
                        var ka = item.RecipientEGRPOU != item.PayerEGRPOU ? _db.Kagent.FirstOrDefault(w => w.OKPO == item.RecipientEGRPOU) : _db.Kagent.FirstOrDefault(w => w.OKPO == item.RecipientEGRPOU && w.KType == 3);
                        if (ka == null)
                        {

                            var new_ka = _db.Kagent.Add(new Kagent
                            {
                                Name = item.RecipientBankName,
                                OKPO = item.RecipientEGRPOU,
                                KaKind = 6,
                                Id = Guid.NewGuid(),
                                KType = 0,
                                KAgentAccount = new List<KAgentAccount>() { new KAgentAccount { AccNum = item.RecipientAccount, Banks = recipient_bank, TypeId = 1, Name = item.RecipientAccountName } }
                            });
                        }
                        else
                        {
                            _db.KAgentAccount.Add(new KAgentAccount
                            {
                                AccNum = item.RecipientAccount,
                                Banks = recipient_bank,
                                KAId = ka.KaId,
                                TypeId = 1,
                                Name = item.RecipientAccountName
                            });
                        }

                        _db.SaveChanges();

                        recipient_account = _db.KAgentAccount.FirstOrDefault(w => w.AccNum == item.RecipientAccount);
                    }

                    if (item.PaySum > 0 && recipient_account.Kagent.KType != 3 && item.Reason.IndexOf("cmps:") >= 0) //якщо приход з і платник не власне підприємство
                    {
                        item.Checked = AddAcquiringPayDoc(item, payer_account, recipient_account);
                    }
                    else if (item.PaySum < 0 && recipient_account.Kagent.KType != 3 && item.RecipientEGRPOU != item.PayerEGRPOU)
                    {
                        var _pd_additional_costs = _db.PayDoc.Add(new PayDoc
                        {
                            Id = Guid.NewGuid(),
                            Checked = 1,
                            DocNum = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault(),
                            OnDate = item.TransactionDate.Value,
                            Total = Math.Abs(item.PaySum.Value),
                            CTypeId = item.CTypeId.Value,
                            WithNDS = 1,// З НДС
                            PTypeId = 2,// Безнал
                            AccId = payer_account.AccId,
                            CashId = null,// Каса 
                            CurrId = 2, //Валюта по умолчанию
                            OnValue = 1,//Курс валюти
                            MPersonId = DBHelper.CurrentUser.KaId,
                            DocType = -2, //Додаткові витрати
                            UpdatedBy = UserSession.UserId,
                            EntId = UserSession.EnterpriseId,
                            ReportingDate = item.TransactionDate.Value,
                            KaAccId = recipient_account.AccId,
                            KaId = recipient_account.KAId,
                            Reason = item.Reason
                        });

                        _db.SaveChanges();

                        _db.SetDocRel(bs.Id, _pd_additional_costs.Id);

                        item.Checked = 1;
                    }
                    else if (item.RecipientEGRPOU == item.PayerEGRPOU || ( payer_account.Kagent.KType == 3 && recipient_account.Kagent.KType == 3))
                    {
                        var doc_num = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault();
                        var oper_id = Guid.NewGuid();

                        var _pd_from = _db.PayDoc.Add(new PayDoc
                        {
                            Id = Guid.NewGuid(),
                            Checked = 1,
                            DocNum = doc_num,
                            OnDate = item.TransactionDate.Value,
                            Total = Math.Abs(item.PaySum.Value),
                            CTypeId = 1,// За товар
                            WithNDS = 1,// З НДС
                            PTypeId = 2,// Безнал
                            CashId = null,
                            CurrId = 2, //Валюта по умолчанию
                            OnValue = 1,//Курс валюти
                            MPersonId = DBHelper.CurrentUser.KaId,
                            DocType = -3,
                            UpdatedBy = DBHelper.CurrentUser.UserId,
                            EntId = DBHelper.Enterprise.KaId,
                            OperId = oper_id,
                            Reason = item.Reason,
                            AccId = item.PaySum.Value < 0 ? payer_account.AccId : recipient_account.AccId
                        });

                        var _pd_to = _db.PayDoc.Add(new PayDoc
                        {
                            Id = Guid.NewGuid(),
                            Checked = 1,
                            DocNum = doc_num,
                            OnDate = item.TransactionDate.Value,
                            Total = Math.Abs(item.PaySum.Value),
                            CTypeId = 1,// За товар
                            WithNDS = 1,// З НДС
                            PTypeId = 2,// Безнал
                            CashId = null,
                            CurrId = 2, //Валюта по умолчанию
                            OnValue = 1,//Курс валюти
                            MPersonId = DBHelper.CurrentUser.KaId,
                            DocType = 3,
                            UpdatedBy = DBHelper.CurrentUser.UserId,
                            EntId = DBHelper.Enterprise.KaId,
                            OperId = oper_id,
                            Reason = item.Reason,
                            AccId = item.PaySum.Value > 0 ? recipient_account.AccId : payer_account.AccId
                        });

                        _db.SetDocRel(bs.Id, _pd_from.Id);
                        _db.SetDocRel(bs.Id, _pd_to.Id);

                        item.Checked = 1;

                        _db.SaveChanges();
                    }

                }
            }

            _db.SaveChanges();

            return !_db.BankStatementsDet.AsNoTracking().Any(a => a.Checked == 0);
        }

        private int AddAcquiringPayDoc(BankStatementsDet item, KAgentAccount payer_account, KAgentAccount recipient_account)
        {
        //    var doc_type = item.PaySum > 0 ? 1 : -1;
           // string doc_setting_name = doc_type == -1 ? "pay_doc_out" : doc_type == 1 ? "pay_doc_in" : "pay_doc";
            List<int> ka_list = item.KaId.HasValue ? new List<int> { item.KaId.Value } : _db.Kagent.Where(w => w.OKPO == item.PayerEGRPOU && w.KType != 3).Select(s => s.KaId).ToList();
            var pay_sum = Math.Round(Math.Abs(item.PaySum.Value) / ka_list.Count(), 2, MidpointRounding.AwayFromZero);
            var pay_sum_dev = Math.Abs(item.PaySum.Value) - (pay_sum * ka_list.Count());
            try
            {
                var _pd = _db.PayDoc.Add(new PayDoc
                {
                    Id = Guid.NewGuid(),
                    Checked = 1,
                    DocNum = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault(),
                    OnDate = item.TransactionDate.Value,
                    Total = item.PaySum.Value,
                    CTypeId = 1,// За товар
                    WithNDS = 1,// З НДС
                    PTypeId = 2,// Безнал
                    CashId = null,// Каса 
                    CurrId = 2, //Валюта 
                    OnValue = 1,//Курс валюти
                    MPersonId = DBHelper.CurrentUser.KaId,
                    DocType = 11,//Зарахування коштів на рахунок
                    UpdatedBy = DBHelper.CurrentUser.UserId,
                    EntId = payer_account.KAId,
                    OperId = Guid.NewGuid(),
                    AccId = payer_account.AccId,
                    KaAccId = recipient_account.AccId,
                    ReportingDate = item.TransactionDate.Value,
                    BankCommission = item.BankCommission,
                    Reason = item.Reason
                });

                _db.SaveChanges();
                _db.SetDocRel(bs.Id, _pd.Id);

            /*    if (item.BankCommission > 0)// Додаткові витрати за комісію банку
                {
                    var _pd_additional_costs = new PayDoc 
                    {
                        Id = Guid.NewGuid(),
                        Checked = 1,
                        DocNum = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault(),
                        OnDate = item.TransactionDate.Value,
                        Total = item.BankCommission.Value,
                        CTypeId = item.CTypeId.Value,
                        WithNDS = 1,// З НДС
                        PTypeId = 2,// Безнал
                        AccId = payer_account.AccId,
                        CashId = null,// Каса 
                        CurrId = 2, //Валюта по умолчанию
                        OnValue = 1,//Курс валюти
                        MPersonId = DBHelper.CurrentUser.KaId,
                        DocType = -2, //Додаткові витрати, комісія банку
                        UpdatedBy = UserSession.UserId,
                        EntId = UserSession.EnterpriseId,
                        ReportingDate = item.TransactionDate.Value,
                        OperId = _pd.OperId
                    };

                    _db.SaveChanges();

                    _db.SetDocRel(bs.Id, _pd_additional_costs.Id);
                }*/

                for (int i = 0; i < ka_list.Count(); i++)  //Списуем борг прибутковими касовими ордерами за товар по кліентах за рахунок надходження з еквайрингу
                {
                    var _pd_in = _db.PayDoc.Add(new PayDoc()  
                    {
                        Id = Guid.NewGuid(),
                        DocType = 1,
                        DocNum = new BaseEntities().GetDocNum("pay_doc_in").FirstOrDefault(), //Номер документа
                        Total = i == 0 ? pay_sum + pay_sum_dev : pay_sum,
                        Checked = 1,
                        OnDate = item.TransactionDate.Value,
                        WithNDS = 1,  // З НДС
                        PTypeId = 2,  // Вид оплати Безнал
                        CashId = null,  // Каса 
                        AccId = bs.AccId, // Acount
                        CTypeId = item.CTypeId.Value,
                        CurrId = 2,  //Валюта по умолчанию
                        OnValue = 1,  //Курс валюти
                        MPersonId = bs.PersonId,
                        KaId = ka_list[i],
                        UpdatedBy = DBHelper.CurrentUser.UserId,
                        EntId = payer_account.KAId,
                        ReportingDate = item.TransactionDate.Value,
                        Reason = item.Reason
                        // KaAccId = recipient_account.AccId
                    });

                    _db.SaveChanges();

                    _db.SetDocRel(bs.Id, _pd_in.Id);
                }
            }
            catch
            {
                return 0;
            }

            return 1;
        }

     /*   private void AddAdditionalCosts(decimal pay_sum)
        {
            var _pd_additional_costs = new PayDoc
            {
                Id = Guid.NewGuid(),
                Checked = 1,
                DocNum = new BaseEntities().GetDocNum("pay_doc").FirstOrDefault(),
                OnDate = item.TransactionDate.Value,
                Total = 0,
                CTypeId = item.CTypeId.Value,
                WithNDS = 1,// З НДС
                PTypeId = 2,// Безнал
                AccId = payer_account.AccId,
                CashId = null,// Каса 
                CurrId = 2, //Валюта по умолчанию
                OnValue = 1,//Курс валюти
                MPersonId = DBHelper.CurrentUser.KaId,
                DocType = -2, //Додаткові витрати, комісія банку
                UpdatedBy = UserSession.UserId,
                EntId = UserSession.EnterpriseId,
                ReportingDate = item.TransactionDate.Value,
                OperId = _pd.OperId,
            };

            _db.SaveChanges();

            _db.SetDocRel(bs.Id, _pd_additional_costs.Id);
        }*/

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

                        string EGRPOU = "", PayerAccountName = "", Account = "";

                        DbfLoaderCore loader = new DbfLoaderCore(fs, Encoding.GetEncoding(1251));

                        foreach (DbfRecord dbf_row in loader.Records)
                        {
                            var row = dbf_row.Attributes.ToDictionary(x => x.Name, x => x.Value);

                            if (row["FIELD0"].ToString() == "Код Клієнта" && EGRPOU =="")
                            {
                                EGRPOU = row["FIELD1"].ToString();
                            }
                            if (row["FIELD0"].ToString() == "Назва Клієнта"&& PayerAccountName == "")
                            {
                                PayerAccountName = row["FIELD1"].ToString();
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
                                    PayerAccountName = PayerAccountName,
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
            AccountEdit.Properties.DataSource = _db.EnterpriseAccount.Where(w => w.KaId == UserSession.EnterpriseId).ToList();

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

        private void AccountEdit_EditValueChanged(object sender, EventArgs e)
        {
            GetOk();
        }
    }
}
