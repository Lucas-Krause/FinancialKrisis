using FinancialKrisis.Domain.ValueObjects;

namespace FinancialKrisis.Domain.Entities;

public partial class Payee
{
    public void ChangeName(string pNewName)
    {
        Name = new EntityName(pNewName, typeof(Payee));
    }
}
