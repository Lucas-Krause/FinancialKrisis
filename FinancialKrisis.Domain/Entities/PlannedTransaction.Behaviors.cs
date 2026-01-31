using FinancialKrisis.Domain.Enums;

namespace FinancialKrisis.Domain.Entities;

public partial class PlannedTransaction
{
    public void ChangePlannedDateTime(DateTime pDateTime)
    {
        ValidatePlannedDateTime(pDateTime);
        PlannedDateTime = pDateTime;
    }

    public void MarkAsFulfilled()
    {
        Status = PlannedTransactionStatus.Fulfilled;
    }

    public void MarkAsIgnored()
    {
        Status = PlannedTransactionStatus.Ignored;
    }
}
