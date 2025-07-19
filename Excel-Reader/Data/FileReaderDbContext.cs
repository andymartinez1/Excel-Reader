using Excel_Reader.Models;
using Microsoft.EntityFrameworkCore;

namespace Excel_Reader.Data;

public class FileReaderDbContext : DbContext
{
    private DbSet<ExcelData> ExcelData { get; set; }
}
