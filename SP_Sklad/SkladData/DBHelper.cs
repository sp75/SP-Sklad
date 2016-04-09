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
        private static List<PersonList> _person;
        private static List<PersonList> _company;
        private static List<PayType> _pay_type;
        private static List<CashDesks> _cash_desks;
        private static List<ChargeType> _charge_type;
        private static LoginUser _current_user;
        public static LoginUser CurrentUser
        {
            get
            {
                if (_current_user != null)
                {
                    return _current_user;
                }

                return _current_user = new BaseEntities().Users.Where(w => w.Name == "admin" && w.Pass == "1").Select(s => new LoginUser
                {
                    UserId = s.UserId,
                    Name = s.Name,
                    Pass = s.Pass,
                    FullName = s.FullName,
                    SysName = s.SysName,
                    ShowBalance = s.ShowBalance,
                    ShowPrice = s.ShowPrice,
                    EnableEditDate = s.EnableEditDate,
                    KaId = s.Kagent.FirstOrDefault().KaId
                }).FirstOrDefault();
            }
        }

        public static List<PersonList> Persons
        {
            get
            {
                if (_person != null)
                {
                    return _person;
                }
                return new BaseEntities().Kagent.Where(w => w.KType == 2).Select(s => new PersonList() { KaId = s.KaId, Name = s.Name }).ToList();
            }
        }
        public static List<PersonList> Company
        {
            get
            {
                if ( _company != null )
                {
                    return _company;
                }
                return _company = new BaseEntities().Kagent.Where(w => w.KType == 3).Select(s => new PersonList() { KaId = s.KaId, Name = s.Name }).ToList();
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
                    _charge_type = new BaseEntities().ChargeType.ToList();
                }
                return _charge_type;
            }
        }
 
        public static List<WAREHOUSE> WhList()
        {
            return new BaseEntities().WAREHOUSE.Where(w => w.DELETED == 0).ToList();
        }

        public static DateTime ServerDateTime()
        {
            return new BaseEntities().Database.SqlQuery<DateTime>("SELECT getdate()").FirstOrDefault();
        }

        public static int? StornoOrder(BaseEntities db, int wbill_id)
        {
            var result = db.StornoWayBill(wbill_id).FirstOrDefault();

            if (result == 1)
            {
                MessageBox.Show(Resources.not_storno_wb, "Відміна проводки", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return result;
        }

        public static ExecuteWayBill_Result ExecuteOrder(BaseEntities db, int wbill_id)
        {
            var result = db.ExecuteWayBill(wbill_id, null).FirstOrDefault();
            if (result != null && result.Checked == 0)
            {
                MessageBox.Show(Resources.not_execute_wb, "Проведення документа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return result;
        }

      

    }

    public class PersonList
    {
        public int KaId { get; set; }
        public String Name { get; set; }
    }

     public class LoginUser : Users
     {
         public int KaId { get; set; }
     }
}
