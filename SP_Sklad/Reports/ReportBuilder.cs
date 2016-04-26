using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;

namespace SpreadsheetReportBuilder
{
    public static class ReportBuilder
    {
        private static int serviceRow = 1;
        private static int ROW_A1 = 1;

        public static MemoryStream GenerateReport(Dictionary<string, IList> dictionary, string templateFileName, string OutFile, bool protectionSheet)
        {
            DataSet dataSet = new DataSet();
            foreach (var obj in dictionary)
            {
                DataTable dataTable = new DataTable(obj.Key);
                if (obj.Value.Count > 0)
                {

                    object row = dictionary[obj.Key][0];
                    Type type = row.GetType();
                    PropertyInfo[] pi = type.GetProperties();
                    foreach (PropertyInfo info in pi)
                    {
                        PropertyInfo p = type.GetProperty(info.Name);
                        dataTable.Columns.Add(info.Name, p.GetValue(row, null).GetType());
                    }

                    foreach (Object Data in obj.Value)
                    {
                        Type personType = Data.GetType();
                        PropertyInfo[] pis = personType.GetProperties();
                        DataRow newRow = dataTable.NewRow();

                        foreach (PropertyInfo pin in pis)
                        {
                            PropertyInfo propertyInfo = personType.GetProperty(pin.Name);
                            newRow[pin.Name] = propertyInfo.GetValue(Data, null);
                        }
                        dataTable.Rows.Add(newRow);
                    }
                }
                dataTable.AcceptChanges();
                dataSet.Tables.Add(dataTable);
            }

            if (dataSet.Tables["_realation_"] != null)
            {
                foreach (DataRow dataRow in dataSet.Tables["_realation_"].Rows)
                {
                    String pk = dataRow["pk"].ToString();
                    String fk = dataRow["fk"].ToString();
                    String master_table = dataRow["master_table"].ToString();
                    String child_table = dataRow["child_table"].ToString();
                    dataSet.Relations.Add(child_table + "=>" + master_table, dataSet.Tables[master_table].Columns[pk], dataSet.Tables[child_table].Columns[fk]);
                }
            }
            dataSet.Tables.Remove("_realation_");


            return GenerateReport(dataSet, templateFileName, OutFile, protectionSheet);
        }

        //Создание отчета
        public static MemoryStream GenerateReport(DataSet dataSet, string Temleyt, string OutFile, bool protectionSheet)
        {
            MemoryStream ms = new MemoryStream();

            var workbook = new Workbook();

            // Load a workbook from the stream. 
            using (FileStream stream = new FileStream(Temleyt, FileMode.Open))
            {
                workbook.LoadDocument(stream, DocumentFormat.OpenXml);
            }

            foreach (var sheet in workbook.Worksheets)
            {
                // Получаем имена областей DefinedNames принадлежащих к текущему Sheet
                var def_names = workbook.DefinedNames.Where(w => w.Range != null).Where(w => w.Range.Worksheet.Name == sheet.Name).OrderBy(o => o.Range.TopRowIndex);

                //Выбираем все строки с SheetData которые не принадлежат ни одному из DefinedName
                var rows = new List<Row>();
                for (int i = sheet.Rows.LastUsedIndex; i >= 0; i--)
                {
                    rows.Add(sheet.Rows[i]);
                }
                foreach (DefinedName dn in def_names)
                {
                    rows = rows.Where(w => w.Index < dn.Range.TopRowIndex || w.Index > dn.Range.BottomRowIndex).ToList();
                }

                //Заполняем выбраные ячейки selctRow даными с dataSet согласно формулам
                foreach (Row row in rows)
                {
                    foreach (Cell cell in row.Where(w=> w.HasFormula))
                    {
                        SetValueToCellFormula(dataSet, null, cell);
                    }
                }

                foreach (DefinedName definedName in def_names)
                {
                    DataTable dataTable = GetTableByName(dataSet, definedName.Name);
                    if (dataTable != null && definedName.Range!=null)
                    {
                        var child_range = def_names.Where(w => w.Range.TopRowIndex > definedName.Range.TopRowIndex && w.Range.BottomRowIndex < definedName.Range.BottomRowIndex).OrderBy(o => o.Range.TopRowIndex).ToList();

                        SetValueFromDataTable(dataTable, definedName, workbook, sheet, child_range);
                    }
                }

              if (protectionSheet) //Защита листа случайно сгенерированым паролем
                {
                    if (!sheet.IsProtected)
                        sheet.Protect(Convert.ToString(new Random().Next(245, 12457)), WorksheetProtectionPermissions.Default);
                }

            }

            if (!String.IsNullOrEmpty(OutFile))
            {
                workbook.SaveDocument(OutFile);
          //      workbook.ExportToPdf(@"C:\Temp\1.pdf");
            }
            workbook.SaveDocument(ms, DocumentFormat.OpenXml);

            return ms;
        }


        private static void SetValueFromDataTable(DataTable dataTable, DefinedName defName, Workbook RecultDoc, Worksheet worksheet, List<DefinedName> child_range_list)
        {
            string RangeName = dataTable.TableName;
            int count_row = defName.Range.RowCount - 1;
            int master_bottom_row_index = defName.Range.BottomRowIndex;
           

            List<DataRow> ListDataRow = dataTable.Rows.Cast<DataRow>().Where(r => r.RowState != DataRowState.Deleted).ToList();

            foreach (DataRow dataRow in ListDataRow)
            {
                for (var i = 0; i < count_row; i++)
                {
                    worksheet.Rows[defName.Range.BottomRowIndex].Insert();
                    worksheet.Rows[defName.Range.BottomRowIndex - 1].CopyFrom(worksheet.Rows[defName.Range.TopRowIndex + i]);
                }

                foreach (var child_range in child_range_list)
                {
                    int ch_count = child_range != null ? child_range.Range.RowCount - 1 : 0;
                    int offset_butom = child_range != null ? master_bottom_row_index - child_range.Range.BottomRowIndex : 0;
                    worksheet.Rows.Remove(defName.Range.BottomRowIndex - offset_butom - ch_count, ch_count);

                    var child_rows = dataRow.GetChildRows(dataTable.DataSet.Relations[child_range.Name + "=>" + defName.Name]).ToList();
                    foreach (DataRow ch_row in child_rows)
                    {
                        for (var c = 0; c < ch_count; c++)
                        {
                            worksheet.Rows[defName.Range.BottomRowIndex - offset_butom].Insert();
                            worksheet.Rows[defName.Range.BottomRowIndex - offset_butom - 1].CopyFrom(worksheet.Rows[child_range.Range.TopRowIndex + c]);
                        }

                        var rr = defName.Range.Where(item => item.RowIndex >= defName.Range.TopRowIndex + count_row && item.HasFormula);
                        foreach (var item in rr)
                        {
                            SetValueToCellValue(ch_row.Table, ch_row, item);
                        }
                    }

                    var child_service_call =worksheet.Rows[defName.Range.BottomRowIndex - offset_butom].Where(w => !String.IsNullOrEmpty(w.DisplayText));
                    if (!child_rows.Any() || !child_service_call.Any()) //Удаляем системную строку если нет данных
                    {
                        worksheet.Rows.Remove(defName.Range.BottomRowIndex - offset_butom, 1);
                    }
                    else
                    {
                        int firstRow = defName.Range.BottomRowIndex - offset_butom - (ch_count * child_rows.Count()) ;
                        int lastRow = defName.Range.BottomRowIndex - offset_butom - serviceRow;

                        SetFrmulaInServiceCell(worksheet, child_service_call.Where(w => !w.HasFormula ), firstRow, lastRow, child_rows);
                    }
                }

                var range = defName.Range.Where(item => item.RowIndex >= defName.Range.TopRowIndex + count_row && item.RowIndex <defName.Range.BottomRowIndex && item.HasFormula);

                foreach (var cell in range)
                {
                    SetValueToCellValue(dataTable, dataRow, cell);
                }
             
            }

            var master_service_call = worksheet.Rows[defName.Range.BottomRowIndex].Where(w => !String.IsNullOrEmpty(w.DisplayText));
            if (!ListDataRow.Any() || !master_service_call.Any()) //Удаляем системную строку если нет данных
            {
                worksheet.Rows.Remove(defName.Range.BottomRowIndex , 1);
            }
            else
            {
                int firstRow = defName.Range.TopRowIndex ;
                int lastRow = defName.Range.BottomRowIndex - serviceRow ;

                SetFrmulaInServiceCell(worksheet, master_service_call.Where(w => !w.HasFormula), firstRow, lastRow, ListDataRow);
            }

            worksheet.Rows.Remove(defName.Range.TopRowIndex, count_row);
        }


        private static void SetFrmulaInServiceCell(Worksheet worksheet, IEnumerable<Cell> sevice_cell, int firstRow, int lastRow, List<DataRow> rows)
        {
            firstRow = firstRow + ROW_A1;
            lastRow = lastRow + ROW_A1;
            foreach (var i in sevice_cell)
            {
                var h = worksheet.Columns[i.ColumnIndex].Heading;


                switch (i.DisplayText)
                {
                    case "sum": i.Formula = String.Format("=SUM({0}:{1})", h + firstRow.ToString(), h + lastRow.ToString()); break;
                    case "min": i.Formula = String.Format("=MIN({0}:{1})", h + firstRow.ToString(), h + lastRow.ToString()); break;
                    case "max": i.Formula = String.Format("=MAX({0}:{1})", h + firstRow.ToString(), h + lastRow.ToString()); break;
                    case "avg": i.Formula = String.Format("=AVERAGE({0}:{1})", h + firstRow.ToString(), h + lastRow.ToString()); break;
                    case "count": i.SetValue(rows.Count()); break;
                }

                if (!i.HasFormula && rows.Any())
                {
                    var table = rows[0].Table;
                    foreach (DataColumn col in table.Columns)
                    {
                        var rows_col = rows.Select(s => s[col]);
                        var cell_text = i.DisplayText.ToLower();
                        var col_name = col.ColumnName.ToLower();

                        if ("sum_" + col_name == cell_text)
                        {
                            var sum = rows_col.Sum(s => Convert.ToDecimal(s));
                            i.SetValue(Convert.ChangeType(sum, col.DataType));
                        }
                        else if ("min_" + col_name == cell_text)
                        {
                            i.SetValue(rows_col.Min());
                        }
                        else if ("max_" + col_name == cell_text)
                        {
                            i.SetValue(rows_col.Max());
                        }
                        else if ("avg_" + col_name == cell_text)
                        {
                            var avg = rows_col.Average(s => Convert.ToDecimal(s));
                            i.SetValue(Convert.ChangeType(avg, col.DataType));
                        }
                    }
                }
            }
        }

        private static void SetValueToCellValue(DataTable dataTable, DataRow dataRow, Cell cell)
        {
            if (cell.Formula.Trim().Any())
            {
                foreach (DataColumn Dcol in dataTable.Columns)
                {
                    string teg = dataTable.TableName + "_" + Dcol.ColumnName;
                    var pattern = @"(\W|^)(" + teg + @")(\W|$)";

                    if (teg.ToLower() == cell.Formula.Trim('=').ToLower())
                    {
                        cell.SetValue(dataRow[Dcol.ColumnName]);
                    }
                    else
                    {
                        if (Regex.IsMatch(cell.Formula, pattern))
                        {
                            SetValueToCellFormula(dataTable.DataSet, dataRow, cell);
                        }
                    }
                }
            }
        }

        private static void SetValueToCellFormula(DataSet dataSet, DataRow dataRow, Cell cell)
        {
            if (cell.HasFormula)
            {
                string RangeName = "", formula = cell.Formula.Trim();

                if (dataRow != null) RangeName = dataRow.Table.TableName;
                foreach (DataTable Table in dataSet.Tables)
                {
                    foreach (DataColumn Dcol in Table.Columns)
                    {
                        string val;
                        string teg = Table.TableName + "_" + Dcol.ColumnName;

                        if (teg.ToLower() == cell.Formula.Trim('=').ToLower())
                        {
                            if (dataRow != null)
                            {
                                cell.SetValue(dataRow[Dcol.ColumnName]);
                            }
                            else
                            {
                                cell.SetValue(Table.Rows[0][Dcol.ColumnName]);
                            }
                            return;
                        }

                        var pattern = @"(\W|^)(" + teg + @")(\W|$)";
                        if (Regex.IsMatch(formula, pattern))
                        {
                            bool str = (Dcol.DataType.Name == "String" || Dcol.DataType.Name == "DateTime");
                            bool Dec = (Dcol.DataType.Name == "Decimal" || Dcol.DataType.Name == "Double");
                            bool Int = (Dcol.DataType.Name == "Int32");
                            bool nullDB;
                            if (RangeName == Table.TableName)
                            {
                                val = dataRow[Dcol.ColumnName].ToString();
                                nullDB = Convert.IsDBNull(dataRow[Dcol.ColumnName]);
                            }
                            else
                            {
                                val = Table.Rows[0][Dcol.ColumnName].ToString();
                                nullDB = Convert.IsDBNull(Table.Rows[0][Dcol.ColumnName]);
                            }

                            if (str) val = "\"" + val.Replace("\"", "\"\"") + "\"";

                            if ((Dec || Int) && val.Length == 0) val = "0";


                            formula = Regex.Replace(formula, pattern, "${1}" + val + "${3}"); //formula = formula.Replace(teg, val);

                            if ((Dec || Int) && formula == "0" && nullDB) formula = "";
                        }
                    }
                }

                cell.Formula = formula;
            }
        }

        private static void ConvertStrFormulaToValue(Cell cell)
        {
            if (!String.IsNullOrEmpty(cell.Formula) && cell.Formula.Contains("&"))
            {
                string recult = "";

                string[] split = cell.Formula.Split(new Char[] { '&' });

                foreach (string s in split)
                {
                    recult += s.Trim('\"');
                }
                cell.Value = recult;
            }
        }

        private static DataTable GetTableByName(DataSet dataSet, string TableName)
        {
            DataTable rez = null;
            foreach (DataTable Table in dataSet.Tables)
            {
                if (Table.TableName == TableName)
                {
                    rez = Table;
                    break;
                }
            }
            return rez;
        }

    }
}
