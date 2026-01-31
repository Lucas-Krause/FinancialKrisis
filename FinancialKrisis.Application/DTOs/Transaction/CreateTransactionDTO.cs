namespace FinancialKrisis.Application.DTOs;

public class CreateTransactionDTO : CreateFinancialMovementDTO
{
    public DateTime DateTime { get; set; }
}
