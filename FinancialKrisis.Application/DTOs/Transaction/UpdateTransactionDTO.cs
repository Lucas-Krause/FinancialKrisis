namespace FinancialKrisis.Application.DTOs;

public class UpdateTransactionDTO : UpdateFinancialMovementDTO
{
    public Optional<DateTime> DateTime { get; set; } = Optional<DateTime>.Undefined();
}
