using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace BlazorExcelViewer.Pages.Viewer;

public class NPOIHelper
{
    public readonly List<DataTable> DataTables = new();
    private FileStream _file;

    private IWorkbook _workbook;

    public NPOIHelper(string filename)
    {
        Filename = filename;
    }

    private string Filename { get; }

    private void SetWorkbook()
    {
        _file?.Close();
        _file = new FileStream(Filename, FileMode.Open, FileAccess.Read);
        {
            if (Filename.IndexOf(".xlsx") > 0)
                _workbook = new XSSFWorkbook(_file);
            else if (Filename.IndexOf(".xls") > 0)
                _workbook = new HSSFWorkbook(_file);

            _workbook.MissingCellPolicy = MissingCellPolicy.RETURN_NULL_AND_BLANK;
        }
    }

    public void Run()
    {
        SetWorkbook();
        HandleSheets();
    }

    private void HandleSheets()
    {
        for (var i = 0; i < _workbook.NumberOfSheets; i++)
        {
            var sheet = _workbook.GetSheetAt(i);

            DataTables.Add(SheetToTable(sheet));
        }
    }

    private DataTable SheetToTable(ISheet sheet)
    {
        var table = new DataTable();

        if (sheet.LastRowNum == 0)
            return table;

        GenerateIdAndColumn(sheet, table);

        for (var r = sheet.FirstRowNum; r <= sheet.LastRowNum; r++)
        {
            var row = sheet.GetRow(r);
            var dataRow = table.NewRow();

            // Assign the first column "ID" to be index.
            // Index start from 1.
            dataRow["ID"] = r + 1;

            if (row == null)
            {
                table.Rows.Add(dataRow);
                continue;
            }

            for (int c = row.FirstCellNum; c < row.LastCellNum; c++)
            {
                var columnName = CellReference.ConvertNumToColString(c);

                if (table.Columns.Contains(columnName) == false)
                {
                    var column = new DataColumn();
                    column.ColumnName = columnName;
                    table.Columns.Add(column);
                }

                if (row.GetCell(c) != null) dataRow[columnName] = row.GetCell(c).ToString();
            }

            table.Rows.Add(dataRow);
        }

        return table;
    }

    private void GenerateIdAndColumn(ISheet sheet, DataTable table)
    {
        var rows = Enumerable.Range(0, sheet.LastRowNum).Select(sheet.GetRow);
        var maxCellNum = rows.Max(r => r?.LastCellNum ?? 0);

        if (maxCellNum > 0)
            table.Columns.Add(new DataColumn
            {
                ColumnName = "ID"
            });

        for (var i = 0; i < maxCellNum; i++)
            table.Columns.Add(new DataColumn
            {
                ColumnName = CellReference.ConvertNumToColString(i)
            });
    }

    public List<string> GetSheet()
    {
        SetWorkbook();
        var sheets = new List<string>();

        for (var i = 0; i < _workbook.NumberOfSheets; i++) sheets.Add(_workbook.GetSheetAt(i).SheetName);

        return sheets;
    }
}