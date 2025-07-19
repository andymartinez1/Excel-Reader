using System.ComponentModel.DataAnnotations;

namespace Excel_Reader.Models;

public class ExcelData
{
    [Key]
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public int Amount { get; set; }

    public FinanceType Type { get; set; }
}

public enum FinanceType
{
    Income,
    Expense,
}
