namespace FinancialKrisis.Application.DTOs;

public class CreateSubcategoryDTO : ICreateDTO
{
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
}
