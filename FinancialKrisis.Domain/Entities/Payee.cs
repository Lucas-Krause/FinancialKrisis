using FinancialKrisis.Domain.Interfaces;

namespace FinancialKrisis.Domain.Entities;

public partial class Payee : ActivatableEntity, INamedEntity
{
    public Payee(string pName)
    {
        Id = Guid.NewGuid();
        ChangeName(pName);
    }
}
