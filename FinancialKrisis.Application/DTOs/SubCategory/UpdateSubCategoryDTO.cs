namespace FinancialKrisis.Application.DTOs;

public class UpdateSubcategoryDTO : IUpdateDTO
{
    public Guid Id { get; set; }
    public Optional<string> Name { get; set; } = Optional<string>.Undefined();
    public Optional<Guid> CategoryId { get; set; } = Optional<Guid>.Undefined();
}
