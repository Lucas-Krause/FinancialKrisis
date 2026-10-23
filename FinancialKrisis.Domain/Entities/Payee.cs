namespace FinancialKrisis.Domain.Entities;

public partial class Payee : ActivatableEntity
{
    public Payee(string pName)
    {
        Id = Guid.NewGuid();
        ChangeName(pName);
    }
}
