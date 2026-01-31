using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Application.DTOs;

public class UpdatePlannedTransactionDTO : UpdateFinancialMovementDTO
{
    public Optional<RecurrenceType> RecurrenceType { get; set; } = Optional<RecurrenceType>.Undefined();

    public Optional<DateTime> StartDate { get; set; } = Optional<DateTime>.Undefined();
    public Optional<DateTime> EndDate { get; set; } = Optional<DateTime>.Undefined();

    public Optional<int> Interval { get; set; } = Optional<int>.Undefined();

    public Optional<IReadOnlyCollection<DayOfWeek>> DaysOfWeek { get; set; } = Optional<IReadOnlyCollection<DayOfWeek>>.Undefined();

    public Optional<int> DayOfMonth { get; set; } = Optional<int>.Undefined();
}
