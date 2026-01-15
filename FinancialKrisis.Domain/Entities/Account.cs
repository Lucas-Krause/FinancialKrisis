namespace FinancialKrisis.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public decimal InitialBalance { get; private set; }
    public bool IsActive { get; private set; }

    private Account() { } // EF Core

    public Account(string pName, decimal pInitialBalance)
    {
        Id = Guid.NewGuid();
        Name = pName;
        InitialBalance = pInitialBalance;
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
