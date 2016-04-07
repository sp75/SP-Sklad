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
        private static List<PayType> _pay_type;
        private static List<CashDesks> _cash_desks;

        public static List<PersonList> Persons
        {
            get
            {
                if (_person == null)
                {
                    _person = new BaseEntities().Kagent.Where(w => w.KType == 2).Select(s => new PersonList() { KaId = s.KaId, Name = s.Name }).ToList();
                }
                return _person;
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

        /*    public static void Person()
            {
                _person = new BaseEntities().Kagent.Where(w => w.KType == 2).Select(s => new Kagent() { KaId = s.KaId, Name = s.Name }).ToList();
            }*/

       

    }

    public class PersonList
    {
        public int KaId { get; set; }
        public String Name { get; set; }
    }
}
