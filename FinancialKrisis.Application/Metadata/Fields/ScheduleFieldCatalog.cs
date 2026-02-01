using FinancialKrisis.Application.Enums;
using FinancialKrisis.Common.Records;
using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Application.Metadata;

public class ScheduleFieldCatalog
{
    public static readonly Dictionary<FieldKey, GrammarMetadata> Fields = new()
    {
        { Schedule.Fields.PlannedTransaction, new("Transação planejada", GrammaticalGender.Feminine) },
        { Schedule.Fields.RecurrenceType, new("Tipo de recorrência", GrammaticalGender.Masculine) },
        { Schedule.Fields.StartDate, new("Data", GrammaticalGender.Feminine) },
        { Schedule.Fields.EndDate, new("Data de término", GrammaticalGender.Feminine) },
        { Schedule.Fields.Interval, new("Intervalo", GrammaticalGender.Masculine) },
        { Schedule.Fields.DaysOfWeek, new("Dias da semana", GrammaticalGender.Masculine) },
        { Schedule.Fields.DayOfMonth, new("Dia do mês", GrammaticalGender.Masculine) },
    };
}
