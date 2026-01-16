namespace FinancialKrisis.Application.DTOs;

public class CreateSubCategoryDTO
{
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
}
