using SP.Base;
using SP.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.IntermediateWeighing
{
    public static class DBHelper
    {
        public static DateTime ServerDateTime()
        {
            return  Database.SPBase().Database.SqlQuery<DateTime>("SELECT getdate()").FirstOrDefault();
        }

        public static LoginUser CurrentUser
        {
            get
            {
                return Database.SPBase().Users.Where(w => w.UserId == 0).ToList().Select(s => new LoginUser
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

        }

        public class LoginUser : Users
        {
            public int? KaId { get; set; }
        }
    }
}
