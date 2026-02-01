namespace FinancialKrisis.Application.DTOs;

public class CreateAccountDTO : ICreateDTO
{
    public string Name { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public decimal InitialBalance { get; set; }
}
