using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Entities;

public partial class PlannedTransaction
{
    public void ChangeSchedule(PlannedSchedule pSchedule)
    {
        Schedule = pSchedule;
    }
}
