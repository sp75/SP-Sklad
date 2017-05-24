using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP_Sklad.SkladData;

namespace SP_Sklad.Common
{
    public class WBForm : DevExpress.XtraEditors.XtraForm
    {
        private int _wtype { get; set; }

        public int? _wbill_id { get; set; }
        public Guid? doc_id { get; set; }
        public WaybillList wb { get; set; }
        public bool is_new_record { get; set; }
        private UserSettingsRepository user_settings { get; set; }

        public WBForm()
        {
            is_new_record = false;
        }

    }
}
