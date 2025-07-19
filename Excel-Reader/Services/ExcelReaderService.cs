using Excel_Reader.Models;
using OfficeOpenXml;

namespace Excel_Reader.Services;

public class ExcelReaderService
{
    public void GetExcelData()
    {
        var filePath = @"X:\C#\.NET-Console-Apps\Excel-Reader\Excel-Reader\Finances.xls";

        using (var excel = new ExcelPackage(new FileInfo(filePath)))
        {
            ExcelPackage.License.SetNonCommercialPersonal("Test");
            var sheet = excel.Workbook.Worksheets["Finance"];
            GetExcelDataList<ExcelData>(sheet);
        }
    }

    public List<T> GetExcelDataList<T>(ExcelWorksheet sheet)
        where T : class
    {
        var excelDataList = new List<T>();

        // Getting column headers
        var columnInfo = Enumerable
            .Range(1, sheet.Dimension.Columns)
            .ToList()
            .Select(n => new { Index = n, ColumnName = sheet.Cells[1, n].Value.ToString() });

        for (var row = 2; row < sheet.Dimension.Rows; row++)
        {
            var obj = Activator.CreateInstance<T>();
            foreach (var prop in typeof(T).GetProperties())
            {
                var col = columnInfo.SingleOrDefault(c => c.ColumnName == prop.Name).Index;
                var value = sheet.Cells[row, col].Value.ToString();
                var propType = prop.PropertyType;
                prop.SetValue(obj, Convert.ChangeType(value, propType));
            }

            excelDataList.Add(obj);
        }

        return excelDataList;
    }
}
