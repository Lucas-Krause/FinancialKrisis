namespace FinancialKrisis.Application.DTOs;

public class CreateAccountDTO
{
    public string Name { get; set; } = string.Empty;
    public decimal InitialBalance { get; set; }
}
