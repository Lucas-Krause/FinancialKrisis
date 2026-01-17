namespace FinancialKrisis.Application.DTOs;

public class UpdateTransactionDTO
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public Guid AccountId { get; set; }
    public Guid? PayeeId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? SubcategoryId { get; set; }
    public decimal Amount { get; set; }
}
