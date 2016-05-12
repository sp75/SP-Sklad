using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SP_Sklad.Common
{
    class IHelper
    {
        static public bool isRowDublClick(object grid_view)
        {
            GridView view = (GridView)grid_view;
            if (view == null)
            {
                return false;
            }

            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = view.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                return true;
            }
            else return false;
        }
    }
}
