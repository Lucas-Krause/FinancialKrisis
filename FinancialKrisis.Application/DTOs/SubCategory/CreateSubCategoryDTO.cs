namespace FinancialKrisis.Application.DTOs;

public class CreateSubcategoryDTO
{
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
}
