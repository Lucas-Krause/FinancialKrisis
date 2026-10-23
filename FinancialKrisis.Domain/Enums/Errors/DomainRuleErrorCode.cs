namespace FinancialKrisis.Domain.Enums;

public enum DomainRuleErrorCode
{
    RequiredField,
    NegativeValue,
    ValueMustBePositive,
    MaxLengthExceeded,
    EntityNotFound,
    EntityAlreadyActive,
    EntityAlreadyInactive,
    WeeklyScheduleRequiresDaysOfWeek,
    MonthlyScheduleRequiresValidDayOfMonth,
}
