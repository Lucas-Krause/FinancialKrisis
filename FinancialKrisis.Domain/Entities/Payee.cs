namespace FinancialKrisis.Domain.Entities;

public partial class Payee : ActivatableEntity
{
    public Payee(string pName)
    {
        ValidateName(pName);

        Id = Guid.NewGuid();
        Name = pName;
    }
}
