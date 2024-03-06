using SkladEngine.DBFunction.Models;
using SP.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladEngine.DBFunction
{
    public class Finances
    {
        private int _user_id { get; set; }

        public Finances(int user_id)
        {
            _user_id = user_id;
        }

        public List<MoneyTurnoverView> MoneyTurnover(int? fun_id, DateTime? from_date, DateTime? to_date, int? turn_type, int? curr_id, int? ka_id, int? person_id)
        {
            using (var db = SPDatabase.SPBase())
            {
                return db.Database.SqlQuery<MoneyTurnoverView>("SELECT * FROM MoneyTurnover({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", fun_id, from_date, to_date, turn_type, curr_id, ka_id, person_id, _user_id).ToList();
            }
        }

    }
}
