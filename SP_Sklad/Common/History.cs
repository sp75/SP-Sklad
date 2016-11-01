using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraRichEdit.Layout;

namespace SP_Sklad.Common
{
    public class HistoryEntity
    {
        public int MainTabs { get; set; }
        public int FunId { get; set; }
        public string Name { get; set; }
    }
   
   public class History
    {
       private static List<HistoryEntity> HistoryList { get; set; }
       private static int cur_index { get; set; }
       public static bool is_enable { get; set; }

       static History()
       {
           HistoryList = new List<HistoryEntity>();
           cur_index = -1;
           is_enable = true;
       }

       public static void AddEntry(HistoryEntity newData)
       {
           if (!is_enable)
           {
               return;
           }

           if (cur_index == -1)
           {
               cur_index = 0;
           }
           else
           {
               cur_index += 1;
               if (cur_index < HistoryList.Count  )
               {
                   HistoryList.RemoveRange(cur_index, HistoryList.Count - cur_index);
               }
           }

           HistoryList.Add(newData);
       }

       public static HistoryEntity Next()
       {
           var cur_entity = cur_index < HistoryList.Count - 1 ? HistoryList.ElementAt(cur_index + 1) : null;
           if (cur_entity != null) cur_index += 1;

           return cur_entity;

       }

       public static HistoryEntity Previous()
       {
           var cur_entity = cur_index >= 1 ? HistoryList.ElementAt(cur_index - 1) : null;
           if (cur_entity != null) cur_index -= 1;

           return cur_entity;
       }
    }
}
