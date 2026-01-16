namespace FinancialKrisis.Domain.Entities;

public class Account
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string AccountNumber { get; private set; } = null!;
    public decimal InitialBalance { get; private set; }
    public bool IsActive { get; private set; }

    private Account() { }

    public Account(string pName, string pAccountNumber, decimal pInitialBalance)
    {
        Id = Guid.NewGuid();
        Name = pName;
        AccountNumber = pAccountNumber;
        InitialBalance = pInitialBalance;
        IsActive = true;
    }

    public void Rename(string pNewName)
    {
        Name = pNewName;
    }

    public void ChangeAccountNumber(string pNewAccountNumber)
    {
        AccountNumber = pNewAccountNumber;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
