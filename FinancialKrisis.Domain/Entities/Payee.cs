using FinancialKrisis.Domain.Common;

namespace FinancialKrisis.Domain.Entities;

public class Payee : IActivatable
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public bool IsActive { get; private set; }

    private Payee() { }

    public Payee(string pName)
    {
        if (string.IsNullOrWhiteSpace(pName))
            throw new ArgumentException("Payee name is required.");
        Id = Guid.NewGuid();
        Name = pName;
        IsActive = true;
    }

    public void Rename(string pNewName)
    {
        if (string.IsNullOrWhiteSpace(pNewName))
            throw new ArgumentException("Payee name is required.");
        Name = pNewName;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
