using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Domain.Entities;

public partial class Account : ActivatableEntity, INamedEntity
{
    public Account(string pName, string pAccountNumber, decimal pInitialBalance)
    {
        Id = Guid.NewGuid();
        ChangeName(pName);
        ChangeAccountNumber(pAccountNumber);
        ChangeInitialBalance(pInitialBalance);
    }
}
