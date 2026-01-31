namespace FinancialKrisis.Application.DTOs;

public class CreatePlannedTransactionDTO : CreateFinancialMovementDTO
{
    public DateTime PlannedDateTime { get; set; }
}
