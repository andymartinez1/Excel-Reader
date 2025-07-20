using Excel_Reader.Data;
using Excel_Reader.Services;
using Excel_Reader.Views;

using var db = new FileReaderDbContext();
var service = new ExcelReaderService(db);
var ui = new UserInterface(service);

db.Database.EnsureDeleted();
db.Database.EnsureCreated();

var file = "X:\\C#\\.NET-Console-Apps\\Excel-Reader\\Excel-Reader\\Finances.xlsx";

service.ConvertExcelData(file);
ui.ShowAllExcelData();
