using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using SkladEngine.DBFunction;
using SP_Sklad.WBForm;

namespace SP_Sklad.SkladData
{

    public static class DBHelper
    {
        private static List<PersonList> _persons;
        private static List<PersonList> _cashiers;
        private static List<PayType> _pay_type;
        private static List<CashDesks> _cash_desks;
        private static List<ChargeType> _charge_type;
        private static LoginUser _current_user;
   //     private static List<KagentList> _kagents;
        private static List<v_Kagent> _retail_outlets;
        private static List<Currency> _currency;
        private static Enterprise _enterprise;
        private static List<Enterprise> _enterprise_list;
        private static CommonParams _common_param;
        private static List<Measures> _measures;
        private static List<Countries> _counters;
        private static List<DocType> _doc_type;
        private static List<Packaging> _packaging;
        private static List<WhList> _wh_list;
        private static Currency _national_currency;
        private static List<UserRoles> _user_roles;
        private static WeighingScales weighing_scales_1;
        private static WeighingScales weighing_scales_2;
        private static List<GetKagentList_Result> _kagents_by_wrker;

        public static void ClearDBHelper()
        {
            _persons = null;
            _cashiers = null;
            _pay_type = null;
            _cash_desks = null;
            _charge_type = null;
            _current_user = null;
            _retail_outlets = null;
            _currency = null;
            _enterprise = null;
            _enterprise_list = null;
            _common_param = null;
            _measures = null;
            _counters = null;
            _doc_type = null;
            _packaging = null;
            _wh_list = null;
            _national_currency = null;
            _user_roles = null;
            weighing_scales_1 = null;
            weighing_scales_2 = null;
            _kagents_by_wrker = null;
        }

        public static List<UserRoles> UserRolesList
        {
            get
            {
                if (_user_roles == null)
                {
                    _user_roles = new BaseEntities().UserRoles.Where(w => w.UserId == UserSession.UserId).ToList();
                }
                return _user_roles;
            }
        }
        public static bool is_main_cacher => UserRolesList.Any(a => a.RoleId == 2);
        public static bool is_cacher => UserRolesList.Any(a => a.RoleId == 3);
        public static bool is_admin => UserRolesList.Any(a => a.RoleId == 1 );
        public static bool is_buh => UserRolesList.Any(a => a.RoleId == 4);

        public static List<Countries> CountersList
        {
            get
            {
                if (_counters == null)
                {
                    _counters = new BaseEntities().Countries.ToList();
                }
                return _counters;
            }
        }

        public static List<DocType> DocTypeList
        {
            get
            {
                if (_doc_type == null)
                {
                    _doc_type = new BaseEntities().DocType.ToList();
                }
                return _doc_type;
            }
        }

        public static List<Measures> MeasuresList
        {
            get
            {
                if (_measures == null)
                {
                    _measures = new BaseEntities().Measures.ToList();
                }
                return _measures;
            }
        }

        public static CommonParams CommonParam
        {
            get
            {
                if (_common_param == null)
                {
                    _common_param = new BaseEntities().CommonParams.FirstOrDefault();
                }
                return _common_param;
            }
            set
            {
                _common_param = value;
            }
        }

        public static LoginUser CurrentUser
        {
            get
            {
                if (_current_user != null)
                {
                    return _current_user;
                }

                return _current_user = new BaseEntities().Users.Where(w => w.UserId == UserSession.UserId).ToList().Select(s => new LoginUser
                {
                    UserId = s.UserId,
                    Name = s.Name,
                    Pass = s.Pass,
                    FullName = s.FullName,
                    SysName = s.SysName,
                    ShowBalance = s.ShowBalance,
                    ShowPrice = s.ShowPrice,
                    EnableEditDate = s.EnableEditDate,
                    KaId = s.Kagent.Any() ? (int?)s.Kagent.Select(sk => sk.KaId).FirstOrDefault() : (int?)null,
                    ReportFormat = s.ReportFormat,
                    InternalEditor = s.InternalEditor

                }).FirstOrDefault();
            }
            set
            {
                _current_user = value;
            }
        }

    /*    public static List<KagentList> Kagents
        {
            get
            {
                if (_kagents == null)
                {
                    //_kagents = new BaseEntities().KagentList.ToList();
                    using (var _db = DB.SkladBase())
                    {
                        var ent = DBHelper.EnterpriseList.ToList().Select(s => (int?)s.KaId);

                        _kagents = (from k in _db.KagentList
                                    join ew in _db.EnterpriseWorker on k.KaId equals ew.WorkerId into gj
                                    from subfg in gj.DefaultIfEmpty()
                                    where (subfg.EnterpriseId == null || ent.Contains(subfg.EnterpriseId)) && (k.Archived == 0 || k.Archived == null) && k.Deleted == 0
                                    select k
                                 ).Distinct().ToList();
                    }
                }
                return _kagents;
            }
        }*/

        public static List<GetKagentList_Result> KagentsWorkerList
        {
            get
            {
                if (_kagents_by_wrker == null)
                {
                    using (var _db = DB.SkladBase())
                    {
                        _kagents_by_wrker = _db.GetKagentList(CurrentUser.KaId).ToList();
                    }
                }
                return _kagents_by_wrker;
            }
        }

        public static void ReloadKagents()
        {
            _kagents_by_wrker = null;
            _retail_outlets = null;
        }

        public static List<v_Kagent> TradingPoints
        {
            get
            {
                using (var _db = DB.SkladBase())
                {
                    return _retail_outlets = (from k in _db.v_Kagent
                                              join ew in _db.EnterpriseWorker on k.KaId equals ew.WorkerId into gj
                                              from subfg in gj.DefaultIfEmpty()
                                              where k.KaKind == 5 && subfg.EnterpriseId == DBHelper.Enterprise.KaId && k.Archived == 0
                                              select k
                             ).ToList();
                }
            }
        }


        public static IEnumerable<Kontragent> KagentsList
        {
            get
            {
                return new List<Kontragent>() { new Kontragent { KaId = 0, Name = "Усі" } }.Concat(KagentsWorkerList.Select(s => new Kontragent
                {
                    KaId = s.KaId,
                    Name = s.Name
                })).ToList();
            }
        }

        public static List<PersonList> Persons
        {
            get
            {
                if (_persons == null)
                {
                    _persons = KagentsWorkerList.Where(w => w.KType == 2).Select(s => new PersonList
                    {
                        KaId = s.KaId,
                        Name = s.Name
                    }).OrderBy(o => o.Name).ToList();
                }
                return _persons;
            }
        }

        public static List<PersonList> Cashiers
        {
            get
            {
                if (_cashiers == null)
                {
                    _cashiers = KagentsWorkerList.Where(w => w.JobType == 4).Select(s => new PersonList
                    {
                        KaId = s.KaId,
                        Name = s.Name
                    }).OrderBy(o => o.Name).ToList();
                }
                return _cashiers;
            }
        }


        public static List<Enterprise> EnterpriseList
        {
            get
            {
                if (_enterprise_list == null)
                {
                    using (var db = new BaseEntities())
                    {
                        _enterprise_list = db.Kagent.Where(w => w.KType == 3 && w.Deleted == 0 && (w.Archived == null || w.Archived == 0))
                            .Join(db.EnterpriseWorker.Where(ew => ew.WorkerId == CurrentUser.KaId), w => w.KaId, ew => ew.EnterpriseId, (w, ew) => new Enterprise
                            {
                                KaId = w.KaId,
                                Name = w.Name,
                                NdsPayer = w.NdsPayer
                            }).ToList();
                    }
                }
                return _enterprise_list;
            }
        }

        public static Enterprise Enterprise
        {
            get
            {
                if (_enterprise == null)
                {
                    _enterprise = new BaseEntities().Kagent.Where(w => w.KType == 3 && w.KaId == UserSession.EnterpriseId).Select(s => new Enterprise
                    {
                        KaId = s.KaId,
                        Name = s.Name,
                        NdsPayer = s.NdsPayer
                    }).FirstOrDefault();
                }
                return _enterprise;
            }

            set
            {
                _enterprise = value;
            }
        }

        public static List<PayType> PayTypes
        {
            get
            {
                if (_pay_type == null)
                {
                    _pay_type = new BaseEntities().PayType.ToList();
                }
                return _pay_type;
            }
        }
        public static List<CashDesks> CashDesks
        {
            get
            {
                if (_cash_desks == null)
                {
                    _cash_desks = new BaseEntities().GetUserAccessCashDesks(CurrentUser.UserId).ToList().Where(w => w.Allow == 1)/*.Where(w => w.EnterpriseId == Enterprise.KaId)*/.Select(s => new CashDesks
                    {
                        CashId = s.CashId,
                        Name = s.Name,
                        Def = s.Def,
                        EnterpriseId = s.EnterpriseId
                    }).ToList();

                    /*  if (!_cash_desks.Any())
                      {
                          _cash_desks = new BaseEntities().CashDesks.ToList();
                      }*/

                }
                return _cash_desks;
            }

            set
            {
                _cash_desks = value;
            }
        }
        public static List<CashDesks> AllCashDesks
        {
            get
            {
                return new BaseEntities().CashDesks.ToList();
            }
        }


        public static List<ChargeType> ChargeTypes
        {
            get
            {
                if (_charge_type == null)
                {
                    _charge_type = new BaseEntities().ChargeType.Where(w => w.Deleted == 0).ToList();
                }
                return _charge_type;
            }
        }

        public static List<WhList> WhList
        {
            get
            {
                if (_wh_list == null)
                {
                    _wh_list = new BaseEntities().Warehouse.Where(w => w.Deleted == 0 && w.UserAccessWh.Any(u => u.UserId == CurrentUser.UserId)).Select(s => new WhList
                    {
                        WId = s.WId,
                        Name = s.Name,
                        Def = s.Def
                    }).ToList();

                    if (!_wh_list.Any(a => a.Def == 1))
                    {
                        var w = _wh_list.FirstOrDefault();
                        if (w != null)
                        {
                            w.Def = 1;
                        }
                    }
                }
                return _wh_list;
            }

            set
            {
                _wh_list = value;
            }

        }

        public static List<WhList> GetWhList(int UserId, BaseEntities _db)
        {
            return _wh_list = _db.Warehouse.Where(w => w.Deleted == 0 && w.UserAccessWh.Any(u => u.UserId == UserId)).Select(s => new WhList
            {
                WId = s.WId,
                Name = s.Name,
                Def = s.Def
            }).ToList();
        }

        public static List<Packaging> Packaging
        {
            get
            {
                if (_packaging == null)
                {
                    _packaging = new BaseEntities().Packaging.ToList();
                }
                return _packaging;
            }
        }

        public static List<Currency> Currency
        {
            get
            {
                if (_currency == null)
                {
                    _currency = new BaseEntities().Currency.ToList();
                }
                return _currency;
            }
        }

        public static Currency NationalCurrency
        {
            get
            {
                if (_national_currency == null)
                {
                    _national_currency = Currency.FirstOrDefault(w => w.Def == 1);
                }
                return _national_currency;
            }
        }

        public static WeighingScales WeighingScales_1
        {
            get
            {
                if (weighing_scales_1 == null)
                {
                    weighing_scales_1 = new BaseEntities().WeighingScales.FirstOrDefault(f => f.Id == Settings.Default.weighing_scales);
                }
                return weighing_scales_1;
            }
        }

        public static WeighingScales WeighingScales_2
        {
            get
            {
                if (weighing_scales_2 == null)
                {
                    weighing_scales_2 = new BaseEntities().WeighingScales.FirstOrDefault(f => f.Id == Settings.Default.weighing_scales_2);
                }
                return weighing_scales_2;
            }
        }


        public static DateTime ServerDateTime()
        {
            return new BaseEntities().Database.SqlQuery<DateTime>("SELECT getdate()").FirstOrDefault();
        }

        public static int? StornoOrder(BaseEntities db, int wbill_id)
        {
            var result = db.StornoWayBill(wbill_id).FirstOrDefault();

            if (result != null && (result.Checked == 1 || result.Checked == 2))
            {
                if (result.ErrorMessage != "False")
                {
                    MessageBox.Show(result.ErrorMessage, "Відміна проводки", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Resources.not_storno_wb, "Відміна проводки", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            return result.Checked;
        }

        public static ExecuteWayBill_Result ExecuteOrder(BaseEntities db, int wbill_id)
        {
            var result = db.ExecuteWayBill(wbill_id, null, DBHelper.CurrentUser.KaId).ToList().FirstOrDefault();
            if (result != null && result.Checked == 0)
            {
                if (result.ErrorMessage != "False")
                {
                    MessageBox.Show(result.ErrorMessage, "Проведення документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Resources.not_execute_wb, "Проведення документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return result;
        }

        public static bool CheckInDate(WaybillList wb, BaseEntities db, DateTime date)
        {
            bool r = true;
            var query = (from wmt1 in db.WMatTurn
                         from wmt2 in db.WMatTurn
                         from wbd in db.WaybillDet
                         from m in db.Materials
                         where wbd.WbillId == wb.WbillId && m.MatId == wbd.MatId && wbd.PosId == wmt1.SourceId && wmt1.PosId == wmt2.SourceId && wmt1.TurnType != wmt2.TurnType
                         orderby wmt2.OnDate descending
                         select new
                         {
                             wmt2.OnDate,
                             m.Name
                         }
                         ).ToList().FirstOrDefault();



            /*
                            select first 1 distinct wmt2.ondate, m.name
             from WMATTURN wmt1, WMATTURN wmt2, waybilldet wbd , materials m
             where wbd.wbillid=:WBILLID and m.matid = wbd.matid and wbd.posid=wmt1.sourceid
                 and wmt1.posid=wmt2.sourceid  and wmt1.turntype <> wmt2.turntype
            order by wmt2.ondate desc
             */

            if (query != null && date < query.OnDate)
            {
                if (MessageBox.Show("Дата документа не може бути меншою за дату прибуткової партії! \nПозиція: " + query.Name + " \nДата: " + query.OnDate.ToString() + " \nЗмінити дату докомента на " + query.OnDate.ToString() + "?", "Зміна дати докуманта", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    wb.OnDate = query.OnDate;
                    db.SaveChanges();
                }
                else
                {
                    r = false;
                }
            }

            return r;
        }

        public static bool CheckOutDate(WaybillList wb, BaseEntities db, DateTime date)
        {
            bool r = true;
            var query = (from wbd in db.WaybillDet
                         from m in db.Materials
                         where wbd.WbillId == wb.WbillId && m.MatId == wbd.MatId
                         orderby wbd.OnDate descending
                         select new
                         {
                             wbd.OnDate,
                             m.Name
                         }
                         ).FirstOrDefault();

            /*
                            select first 1 distinct wbd.ondate, m.name
 from waybilldet wbd , materials m
 where wbd.wbillid=:WBILLID and m.matid = wbd.matid 
order by wbd.ondate desc
             */
            if (query != null && date < query.OnDate)
            {
                if (MessageBox.Show("Дата документа не може бути меншою за дату видаткової партії! \nПозиція: " + query.Name + " \nДата: " + query.OnDate.ToString() + " \nЗмінити дату докомента на " + query.OnDate.ToString() + "?", "Зміна дати докуманта", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    wb.OnDate = query.OnDate.Value;
                    db.SaveChanges();
                }
                else
                {
                    r = false;
                }
            }

            return r;
        }
        public static bool CheckOrderedInSuppliers(int wbill_id, BaseEntities db)
        {
            bool r = true;
            var query = db.GetOrderedInSuppliers(wbill_id).ToList();

            if (query.Any())
            {
                MessageBox.Show(string.Format("Неможливо провести накладну, так як у ний присутні товари замовлені у постачальника, але ще не оприходувані на склад.\n{0}", String.Join("\n", query.Select(s => s.Name))), "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                r = false;
            }

            return r;
        }

        public static bool CheckExpedition(int wbill_id, BaseEntities db)
        {
            bool r = true;
            var query = db.ExpeditionDet.Where(w=> w.WbillId == wbill_id && w.Checked == 1).ToList();

            if (query.Any())
            {
                MessageBox.Show(string.Format("Неможливо сторнувати накладну, так як вона добавлена в експедицію #{0}", String.Join("\n", query.FirstOrDefault().Expedition.Num)), "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                r = false;
            }

            return r;
        }

        public static void UpdateSessionWaybill(int wb_id, bool clear = false)
        {
            using (var db = new BaseEntities())
            {
                var wb = db.WaybillList.FirstOrDefault(f => f.WbillId == wb_id);
                if (wb != null)
                {
                    wb.SessionId = clear == true ? (wb.SessionId == UserSession.SessionId ? null : wb.SessionId) : (Guid?)UserSession.SessionId;
                    wb.UpdatedBy = UserSession.UserId;
                    wb.UpdatedAt = DateTime.Now;
                    db.SaveChanges();
                }
            }
        }

        public static void ClearSessionWaybill()
        {
            using (var db = new BaseEntities())
            {
                var wb = db.WaybillList.Where(f => f.SessionId == UserSession.SessionId).ToList();
                foreach (var item in wb)
                {
                    item.SessionId = null;
                }
                db.SaveChanges();
            }
        }

        public static bool CanViewUserTreeNode(string ClassName)
        {
            using (var db = new BaseEntities())
            {
                var fun_id = db.Functions.FirstOrDefault(w => w.ClassName == ClassName).FunId;

                return db.UserTreeAccess.Any(w => w.FunId == fun_id && w.UserId == DBHelper.CurrentUser.UserId);
            }
        }

        public static bool CheckIntermediateWeighing(int wbill_id, BaseEntities db)
        {
            bool r = true;

            if (db.IntermediateWeighing.Any(a => a.WbillId == wbill_id && a.Checked == 0))
            {
                MessageBox.Show("Не всі проміжкові зважування проведені !");
                r = false;
            }


            return r;
        }

        public static List<MaterialsByWh> GetMaterialOnWh(int mat_id, int wid, BaseEntities db)
        {
            return db.Database.SqlQuery<MaterialsByWh>(@"SELECT 
         m.MatId,
         pr.WId,
         m.Name,
         ms.ShortName as MsrName,
         sum( pr.remain) Remain,
         sum( pr.Rsv ) Rsv 
		 
  FROM [v_PosRemains] pr
  inner join [Materials] m on m.MatId =pr.MatId
  inner join [Measures] ms on ms.MId = m.MId
  where m.MatId = {0} and pr.WId = {1}
  group by m.MatId, m.Name , ms.ShortName ,pr.WId", mat_id, wid).ToList();
        }


        public static bool RsvItem(int pos_id, BaseEntities _db)
        {
            var cur_wbd = _db.WaybillDet.FirstOrDefault(w => w.PosId == pos_id);

            bool is_rsv = true;

            var pos_in = _db.GetPosIn(cur_wbd.OnDate, cur_wbd.MatId, cur_wbd.WId, 0, DBHelper.CurrentUser.UserId).OrderBy(o => o.OnDate).ToList();

            var mat_remain = new MaterialRemain(UserSession.UserId).GetRemainingMaterialInWh(cur_wbd.WId.Value, cur_wbd.MatId);

            if (mat_remain == null || pos_in == null)
            {
                return false;
            }

            decimal? sum_amount = pos_in.Sum(s => s.Amount);
            decimal? sum_full_remain = pos_in.Sum(s => s.FullRemain);

            if (pos_in.Count > 0 && cur_wbd.Amount <= mat_remain.CurRemain && sum_amount != cur_wbd.Amount)
            {
                sum_amount = cur_wbd.Amount;
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

                is_rsv = (sum_amount <= 0);
            }
            else
            {
                is_rsv = false;
            }


            if (is_rsv && !_db.WMatTurn.Any(w => w.SourceId == cur_wbd.PosId) && _db.UserAccessWh.Any(a => a.UserId == DBHelper.CurrentUser.UserId && a.WId == cur_wbd.WId && a.UseReceived))
            {
                using (var d = new BaseEntities())
                {
                    d.DeleteWhere<WaybillDet>(w => w.PosId == cur_wbd.PosId);

                    foreach (var item in pos_in.Where(w => w.Amount > 0))
                    {
                        var wbd = d.WaybillDet.Add(new WaybillDet()
                        {
                            WbillId = cur_wbd.WbillId,
                            Price = item.Price,
                            BasePrice = item.BasePrice,
                            Nds = item.Nds,
                            CurrId = item.CurrId,
                            OnDate = cur_wbd.OnDate,
                            WId = item.WId,
                            Num = cur_wbd.Num,
                            Amount = item.Amount.Value,
                            MatId = item.MatId,

                        });

                        wbd.WMatTurn1.Add(new WMatTurn
                        {
                            PosId = item.PosId,
                            WId = item.WId,
                            MatId = item.MatId,
                            OnDate = cur_wbd.OnDate.Value,
                            TurnType = 2,
                            Amount = Convert.ToDecimal(item.Amount),
                            //  SourceId = wbd.PosId
                        });

                        d.SaveChanges();

                    }
                }
            }

            return is_rsv;
        }

        public static void CreateWBWriteOff(int wbill_id)
        {
            var _wbill_ids = new List<int?>();
            using (var db = DB.SkladBase())
            {
                var wb_det = db.WaybillDet.Where(w => w.WbillId == wbill_id && w.WMatTurn.Any()).ToList();
                if (wb_det.Any())
                {
                    foreach (var wid in wb_det.GroupBy(g => g.WId).Select(s => s.Key).ToList())
                    {
                        var wb = db.WaybillList.Add(new WaybillList()
                        {
                            Id = Guid.NewGuid(),
                            WType = -5,
                            DefNum = 1,
                            OnDate = DBHelper.ServerDateTime(),
                            Num = new BaseEntities().GetDocNum("wb_write_off").FirstOrDefault(),
                            CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                            OnValue = 1,
                            PersonId = DBHelper.CurrentUser.KaId,
                            WaybillMove = new WaybillMove { SourceWid = wid.Value },
                            Nds = 0,
                            UpdatedBy = DBHelper.CurrentUser.UserId,
                            EntId = DBHelper.Enterprise.KaId
                        });

                        db.SaveChanges();
                        _wbill_ids.Add(wb.WbillId);
                        db.Commission.Add(new Commission { WbillId = wb.WbillId, KaId = DBHelper.CurrentUser.KaId });

                        foreach (var det_item in wb_det.Where(w => w.WId == wid))
                        {
                            var pos_in = db.GetPosIn(wb.OnDate, det_item.MatId, det_item.WId, 0, DBHelper.CurrentUser.UserId).Where(w => w.PosId == det_item.PosId).OrderBy(o => o.OnDate).FirstOrDefault();
                            if (pos_in != null && db.UserAccessWh.Any(a => a.UserId == DBHelper.CurrentUser.UserId && a.WId == det_item.WId && a.UseReceived))
                            {

                                var _wbd = db.WaybillDet.Add(new WaybillDet()
                                {
                                    WbillId = wb.WbillId,
                                    Num = wb.WaybillDet.Count() + 1,
                                    Amount = pos_in.CurRemain.Value,
                                    OnValue = det_item.OnValue,
                                    WId = det_item.WId,
                                    Nds = wb.Nds,
                                    CurrId = wb.CurrId,
                                    OnDate = wb.OnDate,
                                    MatId = det_item.MatId,
                                    Price = det_item.Price,
                                    BasePrice = det_item.Price
                                });
                                db.SaveChanges();

                                db.WMatTurn.Add(new WMatTurn
                                {
                                    PosId = pos_in.PosId,
                                    WId = _wbd.WId.Value,
                                    MatId = _wbd.MatId,
                                    OnDate = _wbd.OnDate.Value,
                                    TurnType = 2,
                                    Amount = _wbd.Amount,
                                    SourceId = _wbd.PosId
                                });
                            }
                        }

                    }

                }

                db.SaveChanges();
            }

            foreach (var item in _wbill_ids)
            {
                using (var frm = new frmWBWriteOff(item))
                {
                    frm.is_new_record = true;
                    frm.ShowDialog();
                }
            }

        }

    }
    public class MaterialsByWh
    {
        public int MatId { get; set; }
        public int WId { get; set; }
        public string Name { get; set; }
        public string MsrName { get; set; }
        public decimal Remain { get; set; }
        public decimal Rsv { get; set; }
    }


    public class PersonList
    {
        public int KaId { get; set; }
        public String Name { get; set; }
    }

    public class Enterprise
    {
        public int KaId { get; set; }
        public String Name { get; set; }
        public int NdsPayer { get; set; }
    }

    public class LoginUser : Users
    {
        public int? KaId { get; set; }
    }

    public class WhList
    {
        public int WId { get; set; }
        public String Name { get; set; }
        public int Def { get; set; }
    }

    public class Kontragent
    {
        public int KaId { get; set; }
        public String Name { get; set; }
    }

}
