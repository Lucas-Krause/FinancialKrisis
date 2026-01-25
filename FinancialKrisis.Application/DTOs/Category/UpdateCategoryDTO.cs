namespace FinancialKrisis.Application.DTOs;

public class UpdateCategoryDTO : IUpdateDTO
{
    public Guid Id { get; set; }
    public Optional<string> Name { get; set; } = Optional<string>.Undefined();
}
