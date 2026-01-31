namespace FinancialKrisis.Domain.Entities;

public partial class Transaction
{
    public void ChangeDateTime(DateTime pDateTime)
    {
        ValidateDateTime(pDateTime);
        DateTime = pDateTime;
    }
}
