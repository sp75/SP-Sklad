using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Properties;

namespace SP_Sklad.SkladData
{

    public static class DBHelper
    {
        private static List<PersonList> _persons;
        private static List<PersonList> _company;
        private static List<PayType> _pay_type;
        private static List<CashDesks> _cash_desks;
        private static List<ChargeType> _charge_type;
        private static LoginUser _current_user;
        private static List<KagentList> _kagents;
        private static List<Currency> _currency;
        private static Enterprise _enterprise;
        private static List<Enterprise> _enterprise_list;
        private static CommonParams _common_param;
        private static List<Measures> _measures;
        private static List<Countries> _counters;
        private static List<DocType> _doc_type;

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
        }

        public static LoginUser CurrentUser
        {
            get
            {
                if (_current_user != null)
                {
                    return _current_user;
                }

                return _current_user = new BaseEntities().Users.Where(w => w.UserId == mainForm.user_id).ToList().Select(s => new LoginUser
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

        public static List<KagentList> Kagents
        {
            get
            {
                if (_kagents == null)
                {
                    _kagents = new BaseEntities().KagentList.ToList();
                }
                return _kagents;
            }
        }
        public static List<PersonList> Persons
        {
            get
            {
                if (_persons == null)
                {
                    _persons = new BaseEntities().Kagent.Where(w => w.KType == 2).Select(s => new PersonList() { KaId = s.KaId, Name = s.Name }).ToList();
                }
                return _persons;
            }
        }


        public static List<Enterprise> EnterpriseList
        {
            get
            {
                if (_enterprise_list == null)
                {
                    _enterprise_list = new BaseEntities().Kagent.Where(w => w.KType == 3).Select(s => new Enterprise { KaId = s.KaId, Name = s.Name, NdsPayer = s.NdsPayer }).ToList();
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
                    _enterprise = new BaseEntities().Kagent.Where(w => w.KType == 3 &&  w.KaId == mainForm.enterprise_id).Select(s => new Enterprise { KaId = s.KaId, Name = s.Name, NdsPayer = s.NdsPayer }).FirstOrDefault();
                }
                return _enterprise;
            }

            set
            {
                _enterprise = value;
            }
        }

        public static List<PersonList> Company
        {
            get
            {
                if (_company != null)
                {
                    _company = new BaseEntities().Kagent.Where(w => w.KType == 3).Select(s => new PersonList() { KaId = s.KaId, Name = s.Name }).ToList();
                }
                return _company;
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
                    _cash_desks = new BaseEntities().CashDesks.ToList();
                }
                return _cash_desks;
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
        public static List<WhList> WhList()
        {
            return new BaseEntities().Warehouse.Where(w => w.Deleted == 0).Select(s => new WhList { WId = s.WId, Name = s.Name, Def= s.Def }).ToList();
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


        public static DateTime ServerDateTime()
        {
            return new BaseEntities().Database.SqlQuery<DateTime>("SELECT getdate()").FirstOrDefault();
        }

        public static int? StornoOrder(BaseEntities db, int wbill_id)
        {
            var result = db.StornoWayBill(wbill_id).FirstOrDefault();


            if (result != null && result.Value == 1)
            {
                MessageBox.Show(Resources.not_storno_wb, "Відміна проводки", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return result.Value;
        }

        public static ExecuteWayBill_Result ExecuteOrder(BaseEntities db, int wbill_id)
        {
            var result = db.ExecuteWayBill(wbill_id, null).ToList().FirstOrDefault();
            if (result != null && result.Checked == 0)
            {
                MessageBox.Show(Resources.not_execute_wb, "Проведення документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                         ).FirstOrDefault();

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
}
