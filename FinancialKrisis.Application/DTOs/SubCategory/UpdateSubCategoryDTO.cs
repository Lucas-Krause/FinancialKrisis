namespace FinancialKrisis.Application.DTOs;

public class UpdateSubcategoryDTO : IUpdateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
}
