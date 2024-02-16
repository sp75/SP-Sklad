using SP.Base;
using SP.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Reports.Comon
{
    public class BaseReport
    {
        public SPBaseModel _db { get; set; }

        public BaseReport()
        {
            _db = SPDatabase.SPBase();
        }


        public string GetTemlate(int rep_id)
        {
            return _db.Reports.FirstOrDefault(w => w.RepId == rep_id).TemlateName;
        }

        public string GetWBTemlate(int w_type)
        {
            return _db.DocType.FirstOrDefault(w => w.Id == w_type).TemlateName;
        }
    }
}
