namespace FinancialKrisis.Domain.Entities;

public partial class Payee
{
    public void ChangeName(string pNewName)
    {
        ValidateName(pNewName);
        Name = pNewName;
    }
}
