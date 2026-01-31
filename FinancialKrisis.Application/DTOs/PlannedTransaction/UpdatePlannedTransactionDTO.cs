namespace FinancialKrisis.Application.DTOs;

public class UpdatePlannedTransactionDTO : UpdateFinancialMovementDTO
{
    public Optional<DateTime> PlannedDateTime { get; set; } = Optional<DateTime>.Undefined();
}
