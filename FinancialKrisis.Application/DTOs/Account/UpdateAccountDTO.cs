namespace FinancialKrisis.Application.DTOs;

public class UpdateAccountDTO : IUpdateDTO
{
    public Guid Id { get; set; }
    public Optional<string> Name { get; set; } = Optional<string>.Undefined();
    public Optional<string> AccountNumber { get; set; } = Optional<string>.Undefined();
    public Optional<decimal> InitialBalance { get; set; } = Optional<decimal>.Undefined();
}
