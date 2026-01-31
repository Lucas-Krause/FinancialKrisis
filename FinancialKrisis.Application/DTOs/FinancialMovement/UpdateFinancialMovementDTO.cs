using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Application.DTOs;

public class UpdateFinancialMovementDTO : IUpdateDTO
{
    public Guid Id { get; set; }

    public Optional<decimal> Amount { get; set; } = Optional<decimal>.Undefined();
    public Optional<FinancialMovementDirection> Direction { get; set; } = Optional<FinancialMovementDirection>.Undefined();

    public Optional<string> Memo { get; set; } = Optional<string>.Undefined();
    public Optional<string> Identifier { get; set; } = Optional<string>.Undefined();

    public Optional<Guid> PayeeId { get; set; } = Optional<Guid>.Undefined();
    public Optional<Guid> CategoryId { get; set; } = Optional<Guid>.Undefined();
    public Optional<Guid> SubcategoryId { get; set; } = Optional<Guid>.Undefined();
}
