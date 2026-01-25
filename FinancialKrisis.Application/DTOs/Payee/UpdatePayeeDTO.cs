namespace FinancialKrisis.Application.DTOs;

public class UpdatePayeeDTO : IUpdateDTO
{
    public Guid Id { get; set; }
    public Optional<string> Name { get; set; } = Optional<string>.Undefined();
}
