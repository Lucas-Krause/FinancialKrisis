namespace FinancialKrisis.Application.DTOs;

public class UpdatePayeeDTO : IUpdateDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
