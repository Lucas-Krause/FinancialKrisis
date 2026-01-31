using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Application.DTOs;

public abstract class CreateFinancialMovementDTO
{
    public Guid AccountId { get; set; }

    public decimal Amount { get; set; }
    public FinancialMovementDirection Direction { get; set; }

    public string? Identifier { get; set; }
    public string? Memo { get; set; }

    public Guid? PayeeId { get; set; }
    public Guid? CategoryId { get; set; }
    public Guid? SubcategoryId { get; set; }
}
