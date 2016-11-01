using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraRichEdit.Layout;

namespace SP_Sklad.Common
{
    public class entity
    {
        public int MainTabs { get; set; }
        public int FunId { get; set; }
    }
   
   public class History
    {
       private static List<entity> HistoryList { get; set; }
       private static int cur_index { get; set; }
       private static entity cur_entity { get; set; }
       public static bool is_enable { get; set; }

       static History()
       {
           HistoryList = new List<entity>();
           cur_index = 0;
           is_enable = true;
       }

       public static void AddEntry(entity newData)
       {
           if (!is_enable)
           {
               return;
           }
        /*   if (HistoryList.IndexOf(newData) != -1)
           {
               HistoryList.RemoveAt(HistoryList.IndexOf(newData));
           }*/

           HistoryList.Add(newData);
           cur_entity = newData;
           cur_index = HistoryList.Count - 1;
       }

       public static entity Next()
       {
           var index = HistoryList.IndexOf(cur_entity);
           cur_entity = index < HistoryList.Count - 1 ? HistoryList.ElementAt(index + 1) : cur_entity;

           return cur_entity;

       }

       public static entity Previous()
       {
           var index = HistoryList.IndexOf(cur_entity);
           cur_entity = index >= 1 ? HistoryList.ElementAt(index - 1) : cur_entity;

           return cur_entity;
       }
    }
}
