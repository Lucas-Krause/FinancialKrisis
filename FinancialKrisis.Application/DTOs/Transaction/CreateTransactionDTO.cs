using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Application.DTOs;

public class CreateTransactionDTO
{
    public decimal Amount { get; set; }
    public string? Identifier { get; set; }
    public string? Memo { get; set; }
    public DateTime DateTime { get; set; }

    public Guid AccountId { get; set; }

    public Guid? CategoryId { get; set; }
    public Guid? SubcategoryId { get; set; }
    public Guid? PayeeId { get; set; }

    public TransactionDirection Direction { get; set; }
}
