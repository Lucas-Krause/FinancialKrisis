namespace FinancialKrisis.Application.DTOs;

public class CreateTransactionDTO
{
    public string Identifier { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public Guid AccountId { get; set; }
    public Guid? PayeeId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? SubCategoryId { get; set; }
    public decimal Amount { get; set; }
}
