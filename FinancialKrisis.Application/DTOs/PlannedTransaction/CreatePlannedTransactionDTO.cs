using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Application.DTOs;

public class CreatePlannedTransactionDTO : CreateFinancialMovementDTO
{
    public RecurrenceType RecurrenceType { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public int Interval { get; set; } = 1;

    public IReadOnlyCollection<DayOfWeek>? DaysOfWeek { get; set; }

    public int? DayOfMonth { get; set; }
}
