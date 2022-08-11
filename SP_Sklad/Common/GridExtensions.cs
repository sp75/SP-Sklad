using DevExpress.Data;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SP_Sklad.Common
{
    public static class GridViewExtensions
    {
        public static ArrayList GetValuesInAsyncMode(this GridView view, int startRowHandle, int endRowHandle, string column)
        {
            startRowHandle = Math.Min(startRowHandle, endRowHandle);
            endRowHandle = Math.Max(startRowHandle, endRowHandle);
            ArrayList result = new ArrayList();
            for (int i = startRowHandle; i < endRowHandle; i++)
            {
                object o = view.GetRowCellValue(i, column);
                while (o is NotLoadedObject)
                {
                    Application.DoEvents();
                    o = view.GetRowCellValue(i, column);
                }
                result.Add(o);
            }
            return result;
        }

        public static object GetValuesInAsyncMode(this GridView view, int RowHandle, string fieldName)
        {
            object result = view.GetRowCellValue(RowHandle, fieldName);
            while (result is NotLoadedObject)
            {
                Application.DoEvents();
                result = view.GetRowCellValue(RowHandle, fieldName);
            }
            return result;
        }

        public static object GetFocusedRowInAsyncMode(this GridView view)
        {
            object f_row = view.GetFocusedRow();
            while (f_row is NotLoadedObject)
            {
                Application.DoEvents();
                f_row = view.GetFocusedRow();
            }
            return f_row;
        }

        public static object GetValuesInAsyncMode(ref object CellValue )
        {
          //  var o = CellValue;
        //    object result = null;
            while (CellValue is NotLoadedObject)
            {
                Application.DoEvents();
              //  o = CellValue;
            //    result = o;
            }
            return CellValue;
        }
    }

}
