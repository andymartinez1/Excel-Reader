using Excel_Reader.Data;
using Excel_Reader.Models;
using OfficeOpenXml;
using Spectre.Console;

namespace Excel_Reader.Services;

public class ExcelReaderService
{
    private readonly FileReaderDbContext _context;

    public ExcelReaderService(FileReaderDbContext context)
    {
        _context = context;
        ExcelPackage.License.SetNonCommercialPersonal("Andrew Martinez");
    }

    public void ConvertExcelData(string excelFilePath)
    {
        using (var excel = new ExcelPackage(excelFilePath))
        {
            var sheet = excel.Workbook.Worksheets[0];

            var rowCount = sheet.Dimension.End.Row;

            for (var row = 2; row <= rowCount; row++)
            {
                var financeData = new Finances
                {
                    Date = DateTime.Parse(sheet.Cells[row, 1].Value.ToString()!),
                    Description = sheet.Cells[row, 2].Value.ToString()!,
                    Category = sheet.Cells[row, 3].Value.ToString()!,
                    Amount = double.Parse(sheet.Cells[row, 4].Value.ToString()!),
                    Type = sheet.Cells[row, 5].Value.ToString()!,
                };

                _context.FinancesList.Add(financeData);
            }

            _context.SaveChanges();
            AnsiConsole.MarkupLine("[green]Data saved[/]");
        }
    }

    public List<Finances> GetAllData()
    {
        return _context.FinancesList.ToList();
    }
}
