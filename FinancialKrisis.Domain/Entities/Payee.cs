namespace FinancialKrisis.Domain.Entities;

public class Payee
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public bool IsActive { get; private set; }

    private Payee() { }

    public Payee(string pName)
    {
        Id = Guid.NewGuid();
        Name = pName;
        IsActive = true;
    }

    public void Rename(string pNewName)
    {
        Name = pNewName;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
