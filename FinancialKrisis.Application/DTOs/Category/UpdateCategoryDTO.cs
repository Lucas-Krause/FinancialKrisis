namespace FinancialKrisis.Application.DTOs;

public class UpdateCategoryDTO : IUpdateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
