using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.Reports
{
    class TemlateList
    {
        public static String wb_in { get { return "WayBill_In.xlsx"; } }
        public static String wb_out { get { return "WayBill_Out.xlsm"; } }
        public static String rep_1 { get { return "MatInShort(1).xlsx"; } }
        public static String rep_2 { get { return "MatOutShort(2).xlsx"; } }
        public static String rep_3 { get { return "MatOut.xlsx"; } }
        public static String rep_4 { get { return "MatIn.xlsx"; } }
        public static String rep_5 { get { return "Creditors.xlsx"; } }
        public static String rep_6 { get { return "Debtors.xlsx"; } }
        public static String rep_7 { get { return "RepMatRest.xlsx"; } }
    }
}
