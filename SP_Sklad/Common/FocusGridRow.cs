using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Sklad.Common
{
   public class FocusGridRow
    {
        private GridView _grid_view { get; set; }

        private object prev_focused_id ;
        private int prev_top_row_index = 0;
        private int prev_rowHandle = 0;
        private bool _restore = false;
        public object find_id { get; set; }

        public FocusGridRow(GridView gridView)
        {
            _grid_view = gridView;
        }

        public void SetRowFocus( string FieldName)
        {
            if(!_restore)
            {
                return;
            }

            int rowHandle = _grid_view.LocateByValue(FieldName, prev_focused_id, OnRowSearchComplete);

            if (rowHandle != DevExpress.Data.DataController.OperationInProgress)
            {
                FocusRow(_grid_view, rowHandle);
            }
            else
            {
                _grid_view.FocusedRowHandle = prev_rowHandle;
            }

            _restore = false;
        }
        public void SetPrevData(int focused_row_id, bool restore)
        {
            prev_rowHandle = _grid_view.FocusedRowHandle;
            _restore = restore;

            if (find_id != null)
            {
                prev_top_row_index = -1;
                prev_focused_id = find_id;
                find_id = null;
            }
            else
            {
                prev_top_row_index = _grid_view.TopRowIndex;
                prev_focused_id = focused_row_id;
            }
        }

        void OnRowSearchComplete(object rh)
        {
            int rowHandle = (int)rh;
            if (_grid_view.IsValidRowHandle(rowHandle))
            {
                FocusRow(_grid_view, rowHandle);
            }
        }

        public void FocusRow(GridView view, int rowHandle)
        {
            view.TopRowIndex = prev_top_row_index == -1 ? rowHandle : prev_top_row_index;
            view.FocusedRowHandle = rowHandle;
        }

    }
}
